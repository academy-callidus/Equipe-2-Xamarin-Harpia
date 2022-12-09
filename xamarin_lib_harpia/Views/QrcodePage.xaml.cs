using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using xamarin_lib_harpia.Utils;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Models.Services;
using Xamarin.Forms.Internals;
using xamarin_lib_harpia.Exceptions;

namespace xamarin_lib_harpia.Views
{
    public partial class QrcodePage : ContentPage {
        private QRCodeService QRCodeService;
        private readonly string[] QrcodeQtdList = { "QrCode", "Dois QrCode" };
        private readonly string[] QrcodeSizeList = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        private readonly string[] QrcodeLevelList = { "Correção L (7%)", "Correção M (15%)", "Correção Q (25%)", "Correção H (30%)" };
        private readonly string[] QrcodeAlignList = { "Esquerda", "Centro", "Direita" };

        public QrcodePage()
        {
            InitializeComponent();
            InitializeValues();
            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            QRCodeService = new QRCodeService(connection);
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

        /// <summary>
        /// Changes the QRcode content taking the input from the QRcode screen
        /// </summary>
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

        /// <summary>
        /// Changes the QRcode quantity (one or two) taking the input from the QRcode screen
        /// </summary>
        private async void OnQtdChange(object sender, EventArgs e)
        {
            var qtdLabel = this.FindByName<Label>("QtdLabel");
            var qrcodeQtd = await DisplayActionSheet("Qtd. Impressão", "Cancelar", null, QrcodeQtdList);
            if ((qrcodeQtd != "Cancelar") && (qrcodeQtd!= null))
            {
                qtdLabel.Text = qrcodeQtd;
            }
        }

        /// <summary>
        /// Changes the QRcode size taking the input from the QRcode screen
        /// </summary>
        private async void OnSizeChange(object sender, EventArgs e)
        {
            var sizeLabel = this.FindByName<Label>("SizeLabel");
            var qrcodeSize = await DisplayActionSheet("QR-Code tamanho", "Cancelar", null, QrcodeSizeList);
            if ((qrcodeSize != "Cancelar") && (qrcodeSize != null)) 
            {
                sizeLabel.Text = qrcodeSize;
            }
        }
        /// <summary>
        /// Changes the QRcode correction level taking the input from the QRcode screen
        /// </summary>
        private async void OnLevelChange(object sender, EventArgs e)
        {
            var levelLabel = this.FindByName<Label>("LevelLabel");
            var qrcodeLevel = await DisplayActionSheet("Nível de correção", "Cancelar", null, QrcodeLevelList);
            if ((qrcodeLevel != "Cancelar") && (qrcodeLevel != null))
            { 
                levelLabel.Text = qrcodeLevel;
            }
        }
        /// <summary>
        /// Changes the QRcode alignment taking the input from the QRcode screen
        /// </summary>
        private async void OnAlignChange(object sender, EventArgs e)
        {
            var alignLabel = this.FindByName<Label>("AlignLabel");
            var qrcodeAlign = await DisplayActionSheet("Alinhamento", "Cancelar", null, QrcodeAlignList);
            if((qrcodeAlign != "Cancelar") && (qrcodeAlign != null))
            {
                alignLabel.Text = qrcodeAlign;
            }
        }

        /// <summary>
        /// Instantiates a QRcode with the data setted
        /// </summary>
        private QRcode GetQrcodeEntity()
        {
            // Content 
            var qrcodeLabel = this.FindByName<Label>("QrcodeLabel");

            // Quantity 
            var qtdLabel = this.FindByName<Label>("QtdLabel");
            var quantity = QrcodeQtdList.IndexOf(qtdLabel.Text);

            // Size
            var sizeLabel = this.FindByName<Label>("SizeLabel");
            var size = Int32.Parse(sizeLabel.Text);

            // Correction
            var levelLabel = this.FindByName<Label>("LevelLabel");
            QrCodeCorrectionEnum level;
            if(levelLabel.Text == "Correção L (7%)")
            {
                level = QrCodeCorrectionEnum.CORRECTION_L;
            } 
            else if(levelLabel.Text == "Correção M (15%)")
            {
                level = QrCodeCorrectionEnum.CORRECTION_M;
            } 
            else if(levelLabel.Text == "Correção Q (25%)")
            {
                level = QrCodeCorrectionEnum.CORRECTION_Q;
            } 
            else
            {
                level = QrCodeCorrectionEnum.CORRECTION_H;
            }

            // Alignment 
            var alignLabel = this.FindByName<Label>("AlignLabel");
            AlignmentEnum align;
            if(alignLabel.Text == "Esquerda")
            {
                align = AlignmentEnum.LEFT;
            } 
            else if(alignLabel.Text == "Centro")
            {
                align = AlignmentEnum.CENTER;
            } 
            else 
            {
                align = AlignmentEnum.RIGHT;
            }

            // Cut
            var cutLabel = this.FindByName<Switch>("CutLabel");

            return new QRcode(
                content: qrcodeLabel.Text,
                impquant: quantity,
                impsize: size,
                correction: level,
                alignment: align,
                cutPaper: cutLabel.IsToggled);
        }
        /// <summary>
        /// Send the QRcode entity to the QRcodeService class
        /// </summary>
        private async void OnPrint(object sender, EventArgs e)
        {
            try
            {
                var wasSuccessful = QRCodeService.Execute(GetQrcodeEntity());
            }
            catch (QrcodeValidationException exception)
            {
                await DisplayAlert("Erro", exception.Message, "ok");
            }
            catch (PrinterConnectionException exception)
            {
                await DisplayAlert("Erro de conexão", exception.Message, "ok");
            }
            catch (PrintQrcodeException exception)
            {
                await DisplayAlert("Erro de impressão", exception.Message, "ok");
            }
            catch (Exception)
            {
                await DisplayAlert("Erro", "Algo deu errado.", "ok");
            }
        }
    }
}