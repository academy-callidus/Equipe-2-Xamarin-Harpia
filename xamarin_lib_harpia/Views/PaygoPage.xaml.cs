using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Models.Services;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Models.Entities.PaymentOperations;
using xamarin_lib_harpia.ViewModels;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaygoPage : ContentPage
    {

        private PaymentService Service;
        private PaygoViewModel viewModel;

        public PaygoPage()
        {
            viewModel = new PaygoViewModel();
            InitializeComponent();
            TransactionInfo.BindingContext = viewModel;

            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            IPayment payment = DependencyService.Get<IPayment>();
            Service = new PaymentService(connection, payment);
        }

        // Tipo de Pagamento
        async void PaymentTypeTapped(object sender, EventArgs e)
        {
            string option = await DisplayActionSheet("Tipo de Pagamento", "cancelar", null, viewModel.paymentTypes);

            if (!string.IsNullOrEmpty(option) && option != "cancelar")
            {
                viewModel.PaymentType = option;
            }
        }

        // Adquirente
        async void PurchaserTapped(object sender, EventArgs e)
        {
            string option = await DisplayActionSheet("Adquirentes habilitados", "cancelar", null, viewModel.purchaserOptions);

            if (!string.IsNullOrEmpty(option) && option != "cancelar")
            {
                viewModel.Purchaser = option;
            }
        }

        // Tipo de Parcelamento
        async void InstallmentTypeTapped(object sender, EventArgs e)
        {
            string option = await DisplayActionSheet("Tipo de parcelamento", "cancelar", null, viewModel.installmentTypes);

            if (!string.IsNullOrEmpty(option) && option != "cancelar")
            {
                viewModel.InstallmentType = option;
            }
        }
        private async void OnCancelClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CancelingPage());
        }

        private async void OnPay(object sender, EventArgs e)
        {
            PaygoTransaction transaction = viewModel.GetTransaction();
            var wasSuccessful = Service.Execute(new SaleOperation(), transaction);
            if(!wasSuccessful) await DisplayAlert("Paygo", "Erro ao realizar pagamento (Venda)!", "OK");
        }

        private async void OnAdmin(object sender, EventArgs e)
        {
            PaygoTransaction transaction = viewModel.GetTransaction();
            var wasSuccessful = Service.Execute(new AdministrativeOperation(), transaction);
            if(!wasSuccessful) await DisplayAlert("Paygo", "Erro ao realizar pagamento (Admin)!", "OK");
        }

    }
}