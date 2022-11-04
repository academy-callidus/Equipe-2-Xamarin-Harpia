using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_lib_harpia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextPage : ContentPage
    {
        private string[] mStrings = { "CP437", "CP850", "CP860", "CP863" };
        private string welcomeText = "欢迎光临(Simplified Chinese) 歡迎光臨（traditional chinese） Welcome(English) 어서 오세요.(Korean) いらっしゃいませ(Japanese) Willkommen in der(Germany) Souhaits de bienvenue(France) ยินดีต้อนรับสู่(Thai) Добро пожаловать(Russian) Benvenuti a(Italian) vítejte v(Czech) BEM - vindo Ao Brasil(Portutuese) مرحبا بكم في(Arabic)";

        private string charSetOption;
        private string textSize;
        private bool isBold;
        private bool isUnderline;

        public TextPage()
        {
            InitializeComponent();

            CharSetText.Text = "GB18030";
            Editor.Text = welcomeText;
            TextSizeLabel.Text = "24";
        }

        async void OnClickCharSet(object sender, EventArgs e)
        {
            string option = await DisplayActionSheet("char set", "cancelar", null, mStrings);

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
    }
}