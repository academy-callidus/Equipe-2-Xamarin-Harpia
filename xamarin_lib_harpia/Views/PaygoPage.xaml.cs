using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaygoPage : ContentPage
    {
        public PaygoPage()
        {
            InitializeComponent();
        }

        private async void PaymentTypeTapped(object sender, EventArgs e)
        {
            string[] paymentTypes = { "Não definido", "Crédito", "Débito", "Carteira Digital" };
            string option = await DisplayActionSheet("Tipo de Pagamento", "cancelar", null, paymentTypes);

            if ((option != "cancelar") && (option != null))
            {
                PaymentTypeLabel.Text = option;
            }
        }

        private async void PurchaserTapped(object sender, EventArgs e)
        {
            string[] purchaserOptions = { "PROVEDOR DESCONHECIDO", "LIBERCARD", "ELAVON", "CIELO", "RV", "BIN", "FDCORBAN", "REDE", "INFOCARDS", "CREDSYSTEM", "NDDCARD", "VERO", "GLOBAL", "GAX", "STONE", "DMCARD", "CTF", "TICKETLOG", "GETNET", "VCMAIS", "SAFRA", "PAGSEGURO", "CONDUCTOR" };
            string option = await DisplayActionSheet("Adquirentes habilitados", "cancelar", null, purchaserOptions);

            if ((option != "cancelar") && (option != null))
            {
                PurchaserOptionLabel.Text = option;
            }
        }

        private async void InstallmentTypeTapped(object sender, EventArgs e)
        {
            string[] installmentsOptions = { "Não definido", "À Vista", "Emissor", "Estabelecimento" };
            string option = await DisplayActionSheet("Tipo de parcelamento", "cancelar", null, installmentsOptions);

            if ((option != "cancelar") && (option != null))
            {
                InstallmentTypeLabel.Text = option;
            }
        }
        private async void OnCancelClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new CancelingPage());
        }

        void ValueEntryChangedToInt(object sender, EventArgs e)
        {
            if ((ValueEntry.Text != "") && (ValueEntry.Text != null))
            {
                double num = Math.Truncate(Single.Parse(ValueEntry.Text.ToString()));
                ValueEntry.Text = num.ToString();
            }
        }

        void ParcelEntryChangedToInt(object sender, EventArgs e)
        {
            if ((ValueEntry.Text != "") && (ValueEntry.Text != null))
            {
                double num = Math.Truncate(Single.Parse(ParcelEntry.Text.ToString()));
                ParcelEntry.Text = num.ToString();
            }
        }

    }
}