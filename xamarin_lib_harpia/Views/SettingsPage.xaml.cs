using Xamarin.Forms;

namespace xamarin_lib_harpia.Views
{
    public partial class SettingsPage : ContentPage
    {
        public string Subtitle { get; }
        public SettingsPage()
        {
            InitializeComponent();

            Subtitle = false ? "Conectado" : "sem impressora";

            //NavTitle.SetBinding(Label.TextProperty, new Binding("Title", source: this));
            //NavSubtitle.SetBinding(Label.TextProperty, new Binding("Subtitle", source: this));
            
        }

        private async void OnConnectionClicked(object sender, System.EventArgs e)
        {
            string result = await DisplayActionSheet("Método de conexão", "Cancelar", null, "API");
        }
        private async void OnInfoClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PrinterInfoPage));
        }
    }
}