using Xamarin.Forms;

namespace xamarin_lib_harpia.Views
{
    public partial class SettingsPage : ContentPage
    {
        public string Subtitle { get; }
        public SettingsPage()
        {
            InitializeComponent();

            Subtitle = false ? "Conectado" : "Sem impressora";

            NavTitle.SetBinding(Label.TextProperty, new Binding("Title", source: this));
            NavSubtitle.SetBinding(Label.TextProperty, new Binding("Subtitle", source: this));
        }


    }
}