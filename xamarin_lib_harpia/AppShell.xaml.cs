using Xamarin.Forms;
using xamarin_lib_harpia.Views;

namespace xamarin_lib_harpia
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(PrinterInfoPage), typeof(PrinterInfoPage));
            Routing.RegisterRoute(nameof(BarcodePage), typeof(BarcodePage));
            Routing.RegisterRoute(nameof(QrcodePage), typeof(QrcodePage));
            Routing.RegisterRoute(nameof(TextPage), typeof(TextPage));
            Routing.RegisterRoute(nameof(TablePage), typeof(TablePage));
            Routing.RegisterRoute(nameof(ImagePage), typeof(ImagePage));
        }
    }
}