using System;
using xamarin_lib_harpia.Models.Services;
using xamarin_lib_harpia.Droid;
using System.Collections.Generic;
using xamarin_lib_harpia.Models.Entities;
using Connection.Droid;
using BR.Com.Setis.Interfaceautomacao;
using Java.Text;

[assembly: Xamarin.Forms.Dependency(typeof(PaygoPayment))]
namespace xamarin_lib_harpia.Droid
{
    public class PaygoPayment : IPayment
    {
        private readonly string REAL_CODE = "986";
        public Transacoes Transactions { get; set; }
        public DadosAutomacao DataTransactions { get; set; }
        public EntradaTransacao InputTransaction { get; set; }
        public SaidaTransacao OutputTransaction { get; set; }
        public Confirmacoes Confirmation { get; set; }

        public PaygoPayment()
        {
            ResetAll();
        }

        private void InitPaygoInterface(PaygoTransaction transaction)
        {
            var versionName = new PrinterConnection().GetServiceVersionName();
            DataTransactions = new DadosAutomacao(
                "TecToy Automação", 
                "Automação", 
                versionName,
                true, 
                true, 
                transaction.Client, 
                transaction.Complete, 
                null
            );
            Transactions = Transacoes.ObtemInstancia(DataTransactions, Android.App.Application.Context);
        }
        private string GetRandomId()
        {
            return Guid.NewGuid().ToString();
        }

        private void ResetAll()
        {
            Transactions = null;
            DataTransactions = null;
            InputTransaction = null;
            OutputTransaction = null;
            Confirmation = null;
        }

        public List<Invoice> CancelPayment(PaygoTransaction transaction, PaygoCanceling canceling)
        {
            var id = GetRandomId();
            InitPaygoInterface(transaction);

            SimpleDateFormat format = new SimpleDateFormat("dd / MM / yyyy");

            InputTransaction = new EntradaTransacao(Operacoes.Cancelamento, id);
            InputTransaction.InformaValorTotal(transaction.Value.ToString());
            InputTransaction.InformaNsuTransacaoOriginal(canceling.Nsu);
            InputTransaction.InformaCodigoAutorizacaoOriginal(canceling.Code.ToString());
            try
            {
                InputTransaction.InformaDataHoraTransacaoOriginal(format.Parse(canceling.Date));
            }
            catch (Exception) { }
            InputTransaction.InformaValorTotal(transaction.Value.ToString());
            return MakeTransition(transaction);
        }

        public List<Invoice> MakeAdminTransition(PaygoTransaction transaction)
        {
            var id = GetRandomId();
            InitPaygoInterface(transaction);
            InputTransaction = new EntradaTransacao(Operacoes.Administrativa, id);
            return MakeTransition(transaction);
        }

        public List<Invoice> MakeSaleTransition(PaygoTransaction transaction)
        {
            var id = GetRandomId();
            InitPaygoInterface(transaction);
            InputTransaction = new EntradaTransacao(Operacoes.Venda, id);
            InputTransaction.InformaDocumentoFiscal(id);
            InputTransaction.InformaValorTotal(transaction.Value.ToString());
            return MakeTransition(transaction);
        }

        private List<Invoice> MakeTransition(PaygoTransaction transaction)
        {

            if (transaction.PaymentType == (int) PaymentEnum.UNDEFINED)
            {
                InputTransaction.InformaModalidadePagamento(ModalidadesPagamento.PagamentoCartao);
                InputTransaction.InformaTipoCartao(Cartoes.CartaoDesconhecido);
            }
            else if (transaction.PaymentType == (int) PaymentEnum.CREDIT_CARD)
            {
                InputTransaction.InformaModalidadePagamento(ModalidadesPagamento.PagamentoCartao);
                InputTransaction.InformaTipoCartao(Cartoes.CartaoCredito);
            }
            else if (transaction.PaymentType == (int) PaymentEnum.DEBIT_CARD)
            {
                InputTransaction.InformaModalidadePagamento(ModalidadesPagamento.PagamentoCartao);
                InputTransaction.InformaTipoCartao(Cartoes.CartaoDebito);
            }
            else if (transaction.PaymentType == (int) PaymentEnum.DIGITAL_CARD)
            {
                InputTransaction.InformaModalidadePagamento(ModalidadesPagamento.PagamentoCarteiraVirtual);
                InputTransaction.InformaTipoCartao(Cartoes.CartaoVoucher);
            }

            InputTransaction.InformaTipoFinanciamento(Financiamentos.AVista);
            InputTransaction.InformaNomeProvedor(transaction.GetPurchaserName());

            InputTransaction.InformaCodigoMoeda(REAL_CODE);
            Confirmation = new Confirmacoes();

            OutputTransaction = Transactions.RealizaTransacao(InputTransaction);

            if (OutputTransaction == null)
                return new List<Invoice>();
            
            Confirmation.InformaIdentificadorConfirmacaoTransacao(
                OutputTransaction.ObtemIdentificadorConfirmacaoTransacao()
            );

            int status = OutputTransaction != null ? OutputTransaction.ObtemResultadoTransacao() : -999999;
            if (status != 0) return new List<Invoice>();

            ViasImpressao vias = OutputTransaction.ObtemViasImprimir();
            List<Invoice> invoices = new List<Invoice>();

            if (transaction.Client)
            {
                if(vias == ViasImpressao.ViaCliente)
                {
                    var invoice = OutputTransaction.ObtemComprovanteDiferenciadoPortador();
                    if (invoice == null || invoice.Count <= 1)
                    {
                        invoice = OutputTransaction.ObtemComprovanteCompleto();
                        if (invoice == null) return invoices;
                    }
                    invoices.Add(new Invoice((List<string>) invoice));
                }

                if (vias == ViasImpressao.ViaEstabelecimento)
                {
                    var invoice = OutputTransaction.ObtemComprovanteDiferenciadoLoja();
                    if (invoice == null || invoice.Count <= 1)
                    {
                        invoice = OutputTransaction.ObtemComprovanteCompleto();
                        if (invoice == null) return invoices;
                    }
                    invoices.Add(new Invoice((List<string>)invoice));
                }
            }
            else
            {
                var invoice = OutputTransaction.ObtemComprovanteCompleto();
                if (invoice == null) return invoices;
                invoices.Add(new Invoice((List<string>)invoice));
            }

            ResetAll();
            return invoices;
        }
    }
}