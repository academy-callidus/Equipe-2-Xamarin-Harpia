using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_lib_harpia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodePage : ContentPage
    {
        private string[] BarcodeModels = { "UPC-A", "UPC-E", "EAN13", "EAN8" };
        private string[] BarcodeHRIs = { "Acima do QRCode", "Abaixo do QRCode", "Acima e abaixo do QRCode" };
        public BarcodePage()
        {
            InitializeComponent();
            InitializeValues();
        }

        private void InitializeValues()
        {
            var barcodeLabel = this.FindByName<Label>("BarcodeLabel");
            var modelLabel = this.FindByName<Label>("ModelLabel");
            var HRILabel = this.FindByName<Label>("HRILabel");
            barcodeLabel.Text = "Exemplo";
            modelLabel.Text = BarcodeModels[0];
            HRILabel.Text = BarcodeHRIs[0];
        }

        private void OnWidthChange(object sender, ValueChangedEventArgs e)
        {
            var label = this.FindByName<Label>("WidthLabel");
            label.Text = Math.Round(e.NewValue).ToString();
        }

        private void OnHeightChange(object sender, ValueChangedEventArgs e)
        {
            var label = this.FindByName<Label>("HeightLabel");
            label.Text = Math.Round(e.NewValue).ToString();
        }

        private async void OnBarcodeChange(object sender, EventArgs e)
        {
            var barcodeLabel = this.FindByName<Label>("BarcodeLabel");
            var barcodeContent = await DisplayPromptAsync(null, "Digite o conteúdo do código de barras", initialValue: barcodeLabel.Text);
            if (barcodeContent != null && barcodeContent != "") {
                barcodeLabel.Text = barcodeContent;
            }
        }

        private async void OnModelChange(object sender, EventArgs e)
        {
            var modelLabel = this.FindByName<Label>("ModelLabel");
            var barcodeModel = await DisplayActionSheet("Modelos de Barcode", "Cancelar", null, BarcodeModels);
            modelLabel.Text = barcodeModel;
        }

        private async void OnHRIChange(object sender, EventArgs e)
        {
            var HRILabel = this.FindByName<Label>("HRILabel");
            var barcodeHRI = await DisplayActionSheet("HRI posição", "Cancelar", null, BarcodeHRIs);
            HRILabel.Text = barcodeHRI;
        }
    }
}