using Xamarin.Forms;

namespace xamarin_lib_harpia.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
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