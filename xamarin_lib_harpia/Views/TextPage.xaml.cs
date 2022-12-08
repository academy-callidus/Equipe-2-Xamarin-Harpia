using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Exceptions;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextPage : ContentPage
    {
        private string[] mStrings = { "CP437", "CP850", "CP860", "CP863", "CP865", "CP857", "CP737", "Windows-1252", "CP866", "CP852", "CP858", "CP874", "CP855", "CP862", "CP864", "GB18030", "BIG5", "KSC5601", "utf-8", "utf-16", "utf-32", "unicodeFFFE"};
        private string welcomeText = "欢迎光临(Simplified Chinese) 歡迎光臨（traditional chinese） Welcome(English) 어서 오세요.(Korean) いらっしゃいませ(Japanese) Willkommen in der(Germany) Souhaits de bienvenue(France) ยินดีต้อนรับสู่(Thai) Добро пожаловать(Russian) Benvenuti a(Italian) vítejte v(Czech) BEM - vindo Ao Brasil(Portutuese) مرحبا بكم في(Arabic)";
        private string charSetOption;
        private int record = 21;

        private TextService textService;
        
        /// <summary>
        /// Initializing text of the labels and TextService object
        /// </summary>
        public TextPage()
        {
            InitializeComponent();
            
            charSetOption = mStrings[record]; // receiving the charset type 

            CharSetLabel.Text = charSetOption;
            EditorLabel.Text = welcomeText;
            TextSizeLabel.Text = "12";
            
            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            textService = new TextService(connection);
        }

        /// <summary>
        /// Changes the charset option taking the input from TextPage
        /// </summary>
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
                CharSetLabel.Text = option;
            }
        }

        /// <summary>
        /// Slider method to change the value of TextSize with a slider
        /// </summary>
        void SliderChanged(object sender, EventArgs e)
        {
            TextSizeLabel.Text = Math.Round(TextSizeSlider.Value).ToString();
        }

        /// <summary>
        /// Method to catch all values and instantiate a Text object 
        /// </summary>
        public Text GetTextEntity()
        {
            var charsetLabel = this.FindByName<Label>("CharSetLabel");
            var isBoldCheckBox = this.FindByName<CheckBox>("isBoldCheckBox");
            var isUnderlineCheckBox = this.FindByName<CheckBox>("isUnderlineCheckBox");
            var textSizeLabel = this.FindByName<Label>("TextSizeLabel");
            var contentLabel = this.FindByName<Editor>("EditorLabel");

            string charsetOption = charsetLabel.Text.ToString();
            bool isBold = isBoldCheckBox.IsChecked == true;
            bool isUnderline = isUnderlineCheckBox.IsChecked == true;
            int textSize = int.Parse(textSizeLabel.Text.ToString());
            string content = contentLabel.Text.ToString();

            return new Text(content, isBold, isUnderline, charsetOption, textSize, this.record); ;
        }

        /// <summary>
        /// Send the Text object to TextService
        /// </summary>
        void OnPrint(object sender, EventArgs e)
        {
            try
            {
                var wasSucessful = textService.Execute(GetTextEntity());
            }
            catch (PrinterConnectionException exception)
            {
                DisplayAlert("Erro de conexão", exception.Message, "Ok");
            }
        }
    }
}