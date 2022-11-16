using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleBar : StackLayout
    {
        public string Subtitle { get; }
        public string Title { get; }
        public TitleBar()
        {
            InitializeComponent();
            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            ConnectionStatusService Status = new ConnectionStatusService(connection);
            

            Title = "Example";
            Subtitle = Status.ConnectionStatus();

            NavTitle.SetBinding(Label.TextProperty, new Binding("Title", source: this));
            NavSubtitle.SetBinding(Label.TextProperty, new Binding("Subtitle", source: this));
        }
    }
}