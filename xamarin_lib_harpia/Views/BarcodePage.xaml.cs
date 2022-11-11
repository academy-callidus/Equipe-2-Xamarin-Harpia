using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Models.Services;
using xamarin_lib_harpia.Models.BarcodeModels;

namespace xamarin_lib_harpia.Views

{
    public partial class BarcodePage : ContentPage
    {
        private List<BarcodeModel> BarcodeModels;
        private BarcodeService BarcodeService;
        private readonly string[] BarcodeHRIs = { "Acima do QRCode", "Abaixo do QRCode", "Acima e abaixo do QRCode" };
        private readonly string CANCEL_OPTION_TEXT = "Cancelar";
        private readonly string DEFAULT_BARCODE_VALUE = "201705070507";

        public BarcodePage()
        {
            InitializeComponent();
            InitializeModels();
            InitializeValues();
            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            BarcodeService = new BarcodeService(connection);
        }

        private void InitializeModels()
        {
            BarcodeModels = new List<BarcodeModel>
            {
                new UPCA(),
                new UPCE(),
                new EAN13(),
                new EAN8(),
                new CODE39(),
                new ITF(),
                new CODABAR(),
                new CODE93(),
                new CODE128A(),
                new CODE128B(),
                new CODE128C()
            };
        }

        private void InitializeValues()
        {
            var barcodePreview = this.FindByName<ZXingBarcodeImageView>("BarcodeImageView");
            var barcodeLabel = this.FindByName<Label>("BarcodeLabel");
            var modelLabel = this.FindByName<Label>("ModelLabel");
            var HRILabel = this.FindByName<Label>("HRILabel");
            barcodeLabel.Text = DEFAULT_BARCODE_VALUE;
            modelLabel.Text = BarcodeModels[0].Name;
            HRILabel.Text = BarcodeHRIs[0];

            barcodePreview.BarcodeFormat = BarcodeModels[0].Format;
            barcodePreview.BarcodeValue = DEFAULT_BARCODE_VALUE;
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
            if (barcodePreview == null) return;
            barcodePreview.BarcodeValue = barcodeContent;
        }

        private async void OnModelChange(object sender, EventArgs e)
        {
            var modelLabel = this.FindByName<Label>("ModelLabel");
            var barcodePreview = this.FindByName<ZXingBarcodeImageView>("BarcodeImageView");
            var barcodeModel = await DisplayActionSheet(
                "Modelos de Barcode",
                CANCEL_OPTION_TEXT,
                null,
                BarcodeModel.ToOptions(BarcodeModels)
            );
            if (barcodeModel == null || barcodeModel == "" || barcodeModel == CANCEL_OPTION_TEXT) return;
            modelLabel.Text = barcodeModel;
            var barcodeFormat = BarcodeModels.Find(model => model.Name == barcodeModel);
            if (barcodeFormat == null) return;
            barcodePreview.BarcodeFormat = barcodeFormat.Format;
        }

        private async void OnHRIChange(object sender, EventArgs e)
        {
            var HRILabel = this.FindByName<Label>("HRILabel");
            var barcodeHRI = await DisplayActionSheet(
                "HRI posição",
                CANCEL_OPTION_TEXT,
                null,
                BarcodeHRIs
            );
            if (barcodeHRI == null || barcodeHRI == "" || barcodeHRI == CANCEL_OPTION_TEXT) return;
            HRILabel.Text = barcodeHRI;
        }

        private Barcode GetBarcodeEntity()
        {
            var contentLabel = this.FindByName<Label>("BarcodeLabel");
            var HRILabel = this.FindByName<Label>("HRILabel");
            var modelLabel = this.FindByName<Label>("ModelLabel");
            var widthLabel = this.FindByName<Label>("WidthLabel");
            var heightLabel = this.FindByName<Label>("HeightLabel");
            var cutLabel = this.FindByName<Switch>("CutLabel");

            var barcodeFormat = BarcodeModels.Find(model => model.Name == modelLabel.Text);
            var width = Int32.Parse(widthLabel.Text);
            var height = Int32.Parse(heightLabel.Text);

            return new Barcode(contentLabel.Text, HRILabel.Text, barcodeFormat, width, height, cutLabel.IsToggled);
        }

        private Barcode GetBarcodeEntity()
        {
            var contentLabel = this.FindByName<Label>("BarcodeLabel");
            var HRILabel = this.FindByName<Label>("HRILabel");
            var modelLabel = this.FindByName<Label>("ModelLabel");
            var widthLabel = this.FindByName<Label>("WidthLabel");
            var heightLabel = this.FindByName<Label>("HeightLabel");
            var cutLabel = this.FindByName<Switch>("CutLabel");

            var barcodeFormat = BarcodeModels.Find(model => model.Name == modelLabel.Text);
            var width = Int32.Parse(widthLabel.Text);
            var height = Int32.Parse(heightLabel.Text);

            return new Barcode(contentLabel.Text, HRILabel.Text, barcodeFormat, width, height, cutLabel.IsToggled);
        }

        private async void OnPrint(object sender, EventArgs e)
        {
            
            var wasSuccessful = BarcodeService.Execute(GetBarcodeEntity());
            if (!wasSuccessful) await DisplayAlert("Impressão de Barcode", "Erro ao realizar impressão!", "OK");
            Console.WriteLine(GetBarcodeEntity());
        }
    }
}