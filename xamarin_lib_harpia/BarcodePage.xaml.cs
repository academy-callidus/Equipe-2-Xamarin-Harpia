using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace xamarin_lib_harpia

{
    public class BarcodeModel
    {
        public string Model { get; }
        public BarcodeFormat Format { get; }

        public BarcodeModel(string model, BarcodeFormat format)
        {
            this.Model = model;
            this.Format = format;
        }

        public static string[] ToOptions(List<BarcodeModel> models)
        {
            string[] options = new string[models.Count];
            for (int index = 0; index < models.Count; index++)
            {
                options[index] = models[index].Model;
            }
            return options;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodePage : ContentPage
    {
        private List<BarcodeModel> BarcodeModels;
        private string[] BarcodeHRIs = { "Acima do QRCode", "Abaixo do QRCode", "Acima e abaixo do QRCode" };
        public BarcodePage()
        {
            InitializeComponent();
            InitializeModels();
            InitializeValues();
        }

        private void InitializeModels()
        {
            BarcodeModels = new List<BarcodeModel>();
            BarcodeModels.Add(new BarcodeModel("UPC-A", BarcodeFormat.UPC_A));
            BarcodeModels.Add(new BarcodeModel("UPC-E", BarcodeFormat.UPC_E));
            BarcodeModels.Add(new BarcodeModel("EAN13", BarcodeFormat.EAN_13));
            BarcodeModels.Add(new BarcodeModel("EAN8", BarcodeFormat.EAN_8));
            BarcodeModels.Add(new BarcodeModel("CODE39", BarcodeFormat.CODE_39));
            BarcodeModels.Add(new BarcodeModel("ITF", BarcodeFormat.ITF));
            BarcodeModels.Add(new BarcodeModel("CODABAR", BarcodeFormat.CODABAR));
            BarcodeModels.Add(new BarcodeModel("CODE93", BarcodeFormat.CODE_93));
            BarcodeModels.Add(new BarcodeModel("CODE128A", BarcodeFormat.CODE_128));
            BarcodeModels.Add(new BarcodeModel("CODE128B", BarcodeFormat.QR_CODE));
            BarcodeModels.Add(new BarcodeModel("CODE128C", BarcodeFormat.QR_CODE));
        }

        private void InitializeValues()
        {
            var barcodeLabel = this.FindByName<Label>("BarcodeLabel");
            var modelLabel = this.FindByName<Label>("ModelLabel");
            var HRILabel = this.FindByName<Label>("HRILabel");
            barcodeLabel.Text = "Exemplo";
            modelLabel.Text = BarcodeModels[0].Model;
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
            var barcodePreview = this.FindByName<ZXingBarcodeImageView>("BarcodeImageView");
            var barcodeContent = await DisplayPromptAsync(
                null, 
                "Digite o conteúdo do código de barras", 
                initialValue: barcodeLabel.Text
            );
            if (barcodeContent == null || barcodeContent == "") return;
            barcodeLabel.Text = barcodeContent;
            barcodePreview.BarcodeValue = barcodeContent;
        }

        private async void OnModelChange(object sender, EventArgs e)
        {
            var modelLabel = this.FindByName<Label>("ModelLabel");
            var barcodePreview = this.FindByName<ZXingBarcodeImageView>("BarcodeImageView");
            var barcodeModel = await DisplayActionSheet(
                "Modelos de Barcode", 
                null, 
                null, 
                BarcodeModel.ToOptions(BarcodeModels)
            );
            if (barcodeModel == null || barcodeModel == "") return;
            modelLabel.Text = barcodeModel;
            var barcodeFormat = BarcodeModels.Find(model => model.Model == barcodeModel);
            if(barcodeFormat == null) return;
            barcodePreview.BarcodeFormat = barcodeFormat.Format;
        }

        private async void OnHRIChange(object sender, EventArgs e)
        {
            var HRILabel = this.FindByName<Label>("HRILabel");
            var barcodeHRI = await DisplayActionSheet("HRI posição", null, null, BarcodeHRIs);
            if (barcodeHRI == null || barcodeHRI == "") return;
            HRILabel.Text = barcodeHRI;
        }

        private void OnPrint(object sender, EventArgs e)
        {
            // TODO Print barcode
        }
    }
}