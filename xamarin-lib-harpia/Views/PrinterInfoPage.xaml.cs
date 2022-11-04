using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


using xamarin_lib_harpia.Models;

namespace xamarin_lib_harpia.Views
{
    public partial class PrinterInfoPage : ContentPage
    {

        public string Subtitle { get; }
        private PrinterInfo printerInfo;

        public PrinterInfoPage()
        {
            InitializeComponent();

            Subtitle = false ? "Conectado" : "Sem impressora";

            NavTitle.SetBinding(Label.TextProperty, new Binding("Title", source: this));
            NavSubtitle.SetBinding(Label.TextProperty, new Binding("Subtitle", source: this));
        }

        private void DisplayPrinterInfo(object printer)
        {

        }
    }
}