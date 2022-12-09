using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Models.Entities.PaygoModels;
using xamarin_lib_harpia.ViewModels;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaygoPage : ContentPage
    {

        // private PaygoService service;
        private PaygoViewModel viewModel;

        public PaygoPage()
        {
            viewModel = new PaygoViewModel();
            InitializeComponent();
            TransactionInfo.BindingContext = viewModel;
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
    }
}