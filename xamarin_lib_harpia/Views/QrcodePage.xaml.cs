using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using xamarin_lib_harpia.Utils;
using Xamarin.Forms.Internals;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Views
{
    public partial class QrcodePage : ContentPage
    {
        private string[] QrcodeQtdList = { "QrCode", "Dois QrCode" };
        private string[] QrcodeSizeList = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        private string[] QrcodeLevelList = { "Correção L (7%)", "Correção M (15%)", "Correção Q (25%)", "Correção H (30%)" };
        private string[] QrcodeAlignList = { "Esquerda", "Centro", "Direita" };
        private QrCodeCorrectionEnum QrcodeLevel;
        private AlignmentEnum QrcodeAlign;
        private bool HasCut;
        private int print_num = 1;
        private int error_level = 3;

        public QrcodePage()
        {
            InitializeComponent();
            InitializeValues();
        }
        private void InitializeValues()
        {
            var QrcodeLabelZxing = this.FindByName<ZXingBarcodeImageView>("BarcodeImageView");
            var QrcodeLabel = this.FindByName<Label>("QrcodeLabel");
            var QtdLabel = this.FindByName<Label>("QtdLabel");
            var SizeLabel = this.FindByName<Label>("SizeLabel");
            var LevelLabel = this.FindByName<Label>("LevelLabel");
            var AlignLabel = this.FindByName<Label>("AlignLabel");
            QrcodeLabelZxing.BarcodeValue = "www.tectoySunmi.com.br";
            QrcodeLabel.Text = "www.tectoySunmi.com.br";
            QtdLabel.Text = QrcodeQtdList[0];
            SizeLabel.Text = QrcodeSizeList[0];
            LevelLabel.Text = QrcodeLevelList[0];
            AlignLabel.Text = QrcodeAlignList[0];
        }

        private async void OnQrcodeChange(object sender, EventArgs e)
        {
            var qrcodeLabel = this.FindByName<Label>("QrcodeLabel");
            var qrcodeLabelZxing = this.FindByName<ZXingBarcodeImageView>("BarcodeImageView");
            var qrcodeContent = await DisplayPromptAsync(null, "Digite o conteúdo do Qrcode", placeholder: "www.tectoySunmi.com.br");
            if (qrcodeContent != null && qrcodeContent != "")
            {
                qrcodeLabelZxing.BarcodeValue = qrcodeContent;
                qrcodeLabel.Text = qrcodeContent;
            }
        }


        private async void OnQtdChange(object sender, EventArgs e)
        {
            var qtdLabel = this.FindByName<Label>("QtdLabel");
            var qrcodeQtd = await DisplayActionSheet("Qtd. Impressão", "Cancelar", null, QrcodeQtdList);
            if (qrcodeQtd != "Cancelar")
            {
                print_num = QrcodeLevelList.IndexOf(qrcodeQtd) + 1;
                qtdLabel.Text = qrcodeQtd;
            }
        }

        private async void OnSizeChange(object sender, EventArgs e)
        {
            var sizeLabel = this.FindByName<Label>("SizeLabel");
            var qrcodeSize = await DisplayActionSheet("QR-Code tamanho", "Cancelar", null, QrcodeSizeList);
            if (qrcodeSize!= "Cancelar")
            {
                sizeLabel.Text = qrcodeSize;
            }
        }
        private async void OnLevelChange(object sender, EventArgs e)
        {
            var levelLabel = this.FindByName<Label>("LevelLabel");
            var qrcodeLevel = await DisplayActionSheet("Nível de correção", "Cancelar", null, QrcodeLevelList);
            if (qrcodeLevel != "Cancelar")
            {
                error_level = QrcodeLevelList.IndexOf(qrcodeLevel);
                levelLabel.Text = qrcodeLevel;
            }
        }
        private async void OnAlignChange(object sender, EventArgs e)
        {
            var alignLabel = this.FindByName<Label>("AlignLabel");
            var qrcodeAlign = await DisplayActionSheet("Alinhamento", "Cancelar", null, QrcodeAlignList);
            if(qrcodeAlign != "Cancelar")
            {
                alignLabel.Text = qrcodeAlign;
            }
        }

        private void OnCutPaper(object sender, ToggledEventArgs e)
        {
            if (HasCut) HasCut = false;
            else HasCut = true;

        }

        private QRcode GetQrcodeEntity()
        {
            var qrcodeLabel = this.FindByName<Label>("QrcodeLabel");
            var sizeLabel = this.FindByName<Label>("SizeLabel");

            var size = Int32.Parse(sizeLabel.Text);

            return new QRcode(
                content: qrcodeLabel.Text,
                impquant: print_num,
                impsize: size,
                correction: QrcodeLevel,
                alignment: QrcodeAlign,
                cutPaper: HasCut);
        }

        private async void OnPrint(object sender, EventArgs e)
        {
            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            var wasSuccessful = await new QRCodeService(connection).Execute(GetQrcodeEntity());

            if (!wasSuccessful)
            {
                await DisplayAlert("Impressão de Barcode", "Erro ao realizar impressão!", "OK");
            }

            Console.WriteLine(GetQrcodeEntity());
        }
    }
}