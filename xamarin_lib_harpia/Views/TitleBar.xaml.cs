using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            Title = "Exemple";
            Subtitle = true ? "Conectado" : "sem impressora";

            NavTitle.SetBinding(Label.TextProperty, new Binding("Title", source: this));
            NavSubtitle.SetBinding(Label.TextProperty, new Binding("Subtitle", source: this));
        }
    }
}