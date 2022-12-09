using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Models.Entities.PaymentOperations;
using xamarin_lib_harpia.Models.Services;
using xamarin_lib_harpia.ViewModels;
using ZXing.Net.Mobile.Forms;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CancelingPage : ContentPage
    {

        private PaymentService Service;
        private PaygoViewModel viewModel;
        private CancelingViewModel CancelingViewModel;
        private PaygoTransaction transaction;

        public CancelingPage(PaygoTransaction transaction)
        {
            InitializeComponent();
            InitializeValues();
            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            this.transaction = transaction;
            //CancelingService = new CancelingService(connection);
        }

        public void InitializeValues()
        {
            var NSULabel = this.FindByName<Label>("NSULabel");
            var CodeLabel = this.FindByName<Label>("CodeLabel");
            var DateLabel = this.FindByName<Label>("DateLabel");
            var PriceLabel = this.FindByName<Label>("PriceLabel");

            NSULabel.Text = "NSU";
            CodeLabel.Text = "Código";
            DateLabel.Text = "Código";
            PriceLabel.Text = "Código";

        }

        /// <summary>
        /// Changes the transaction NSU taking the input from the Canceling screen
        /// </summary>
        private async void OnNSUChange(object sender, EventArgs e)
        {
            var NSULabel = this.FindByName<Label>("NSULabel");
            var NSUContent = await DisplayPromptAsync(null, "Informe o NSU da transação", placeholder: "NSU");
            if (NSUContent != null && NSUContent != "")
            {
                NSULabel.Text = NSUContent;
            }
        }

        /// <summary>
        /// Changes the authorization code taking the input from the Canceling screen
        /// </summary>
        private async void OnCodeChange(object sender, EventArgs e)
        {
            var CodeLabel = this.FindByName<Label>("CodeLabel");
            var CodeContent = await DisplayPromptAsync(null, "Digite o Código de Autorização", placeholder: "Código");
            if (CodeContent != null && CodeContent != "")
            {
                CodeLabel.Text = CodeContent;
            }
        }

        /// <summary>
        /// Changes the transaction date taking the input from the Canceling screen
        /// </summary>
        private async void OnDateChange(object sender, EventArgs e)
        {
            var DateLabel = this.FindByName<Label>("DateLabel");
            var DateContent = await DisplayPromptAsync(null, "Digite a Data da Transação", placeholder: "Código");
            if (DateContent != null && DateContent != "")
            {
                DateLabel.Text = DateContent;
            }
        }

        /// <summary>
        /// Changes the transaction price taking the input from the Canceling screen
        /// </summary>
        private async void OnPriceChange(object sender, EventArgs e)
        {
            var PriceLabel = this.FindByName<Label>("PriceLabel");
            var PriceContent = await DisplayPromptAsync(null, "Digite o Valor da Transação", placeholder: "Código");
            if (PriceContent != null && PriceContent != "")
            {
                PriceLabel.Text = PriceContent;
            }
        }

        private async void OnBackCancel(object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        private PaygoCanceling GetCancelingEntity()
        {
            var nsu = this.FindByName<Label>("NSULabel");
            var CodeLabel = this.FindByName<Label>("CodeLabel");
            var code = Int32.Parse(CodeLabel.Text ?? "0");
            var date = this.FindByName<Label>("DateLabel");
            var PriceLabel = this.FindByName<Label>("PriceLabel");
            var price = float.Parse(PriceLabel.Text);

            return new PaygoCanceling(
                nsu: nsu.Text,
                code: code,
                date: date.Text,
                price: price);
        }

        private async void OnCanceling(object sender, EventArgs e)
        {
            var wasSuccesful = Service.Execute(new CancellingOperation(GetCancelingEntity()), transaction);
            if (!wasSuccesful) await DisplayAlert("Paygo", "Erro ao realizar pagamento (Admin)!", "OK");
        }
    }
}