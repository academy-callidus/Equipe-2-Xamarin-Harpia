using System;
using ZXing;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_lib_harpia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrcodePage : ContentPage
    {
        private string[] QrcodeQtd = { "QrCode", "Dois QrCode"};
        private string[] QrcodeSize = { "1", "2", "3", "4"};
        private string[] QrcodeLevel = { "Correção L (7%)", "Correção M (15%)", "Correção Q (25%)", "Correção H (30%)"};
        private string[] QrcodeAlign = { "Esquerda", "Centro", "Direita" };

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
            QrcodeLabelZxing.BarcodeValue = "Exemplo";
            QrcodeLabel.Text = "Exemplo";
            QtdLabel.Text = QrcodeQtd[0];
            SizeLabel.Text = QrcodeSize[0];
            LevelLabel.Text = QrcodeLevel[0];
            AlignLabel.Text = QrcodeAlign[0];
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


        private async void OnQrcodeChange(object sender, EventArgs e)
        {
            var qrcodeLabel = this.FindByName<Label>("QrcodeLabel");
            var qrcodeLabelZxing = this.FindByName<ZXingBarcodeImageView>("BarcodeImageView");
            var qrcodeContent = await DisplayPromptAsync(null, "Digite o conteúdo do Qrcode", initialValue: qrcodeLabel.Text);
            if (qrcodeContent != null && qrcodeContent != "")
            {
                qrcodeLabelZxing.BarcodeValue = qrcodeContent;
                qrcodeLabel.Text = qrcodeContent;
            }
        }

        
        private async void OnQtdChange(object sender, EventArgs e)
        {
            var qtdLabel = this.FindByName<Label>("QtdLabel");
            var qrcodeQtd = await DisplayActionSheet("Qtd. Impressão", "Cancelar", null, QrcodeQtd);
            if (qrcodeQtd != "Cancelar")
            {
                qtdLabel.Text = qrcodeQtd;
            }
        }

        private async void OnSizeChange(object sender, EventArgs e)
        {
            var sizeLabel = this.FindByName<Label>("SizeLabel");
            var qrcodeSize = await DisplayActionSheet("QR-Code tamanho", "Cancelar", null, QrcodeSize);
            if (qrcodeSize!= "Cancelar")
            {
                sizeLabel.Text = qrcodeSize;
            }
        }
        private async void OnLevelChange(object sender, EventArgs e)
        {
            var levelLabel = this.FindByName<Label>("LevelLabel");
            var qrcodeLevel = await DisplayActionSheet("Nível de correção", "Cancelar", null, QrcodeLevel);
            if(qrcodeLevel != "Cancelar")
            {
                levelLabel.Text = qrcodeLevel;
            }
        }
        private async void OnAlignChange(object sender, EventArgs e)
        {
            var alignLabel = this.FindByName<Label>("AlignLabel");
            var qrcodeAlign = await DisplayActionSheet("Alinhamento", "Cancelar", null, QrcodeAlign);
            if(qrcodeAlign != "Cancelar")
            {
                alignLabel.Text = qrcodeAlign;
            }
        }
    }
}