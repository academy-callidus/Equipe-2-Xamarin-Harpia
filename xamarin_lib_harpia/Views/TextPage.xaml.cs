using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextPage : ContentPage
    {
        private string[] mStrings = { "CP437", "CP850", "CP860", "CP863", "CP865", "CP857", "CP737", "Windows-1252", "CP866", "CP852", "CP858", "CP874", "CP855", "CP862", "CP864", "GB18030", "BIG5", "KSC5601", "utf-8" };
        private string welcomeText = "欢迎光临(Simplified Chinese) 歡迎光臨（traditional chinese） Welcome(English) 어서 오세요.(Korean) いらっしゃいませ(Japanese) Willkommen in der(Germany) Souhaits de bienvenue(France) ยินดีต้อนรับสู่(Thai) Добро пожаловать(Russian) Benvenuti a(Italian) vítejte v(Czech) BEM - vindo Ao Brasil(Portutuese) مرحبا بكم في(Arabic)";

        private string charSetOption;
        private string textSize;
        private bool isBold = false;
        private bool isUnderline = false;
        private int record;
        private TextService textService;

        public TextPage()
        {
            InitializeComponent();

            record = 15;
            charSetOption = mStrings[15];
            CharSetText.Text = charSetOption;
            Editor.Text = welcomeText;
            TextSizeLabel.Text = "24";

            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            textService = new TextService(connection);
        }

        async void OnClickCharSet(object sender, EventArgs e)
        {
            string option = await DisplayActionSheet("char set", "cancelar", null, mStrings);

            for (int i = 0; i < mStrings.Length; i++)
            {
                if (mStrings[i].Equals(option)) record = i;
            }

            if (option != "cancelar" && option != null)
            {
                charSetOption = option;
                CharSetText.Text = option;
            }
        }

        void SliderChanged(object sender, EventArgs e)
        {
            textSize = Math.Round(Slider.Value).ToString();
            TextSizeLabel.Text = Math.Round(Slider.Value).ToString();
        }

        void BoldChanged(object sender, CheckedChangedEventArgs e)
        {
            isBold = e.Value;
        }

        void UnderlineChanged(object sender, CheckedChangedEventArgs e)
        {
            isUnderline = e.Value;
        }

        public Text GetTextEntity()
        {
            string content = Editor.Text.ToString();
            string charsetOption = CharSetText.Text.ToString();
            int textSize = int.Parse(TextSizeLabel.Text.ToString());

            Text text = new Text(content, this.isBold, this.isUnderline, charsetOption, textSize, this.record);
            return text;
        }

        async void OnPrint(object sender, EventArgs e)
        {
            var wasSuccessful = textService.Execute(GetTextEntity());
            if (!wasSuccessful) await DisplayAlert("Impressão de Texto", "Erro ao realizar impressão!", "OK");
        }

        void Teste(object sender, EventArgs e)
        {
            int[] encodes = { 437, 850, 860, 863, 865, 857, 737, 1252, 866, 852, 858, 874, 855, 862, 864, 54936, 950, 949, 65001 };
            foreach(int i in encodes)
            {
                System.Text.Encoding.GetEncoding(i).GetBytes("Teste");
            }
        
            DisplayAlert("Teste", "teste", "ok");
        }
    }
}