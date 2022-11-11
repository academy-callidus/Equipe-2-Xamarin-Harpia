using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using xamarin_lib_harpia.Models;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class TestConnection : ContentPage
    {
        MainPage mainPage = new MainPage();
        public TestConnection()
        {
            InitializeComponent();
            SelectDevice(mainPage.BluetoothConnection());
        }


        private void printTextButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IPrinterConnection>().PrintTextt(printBox.Text);
        }

        void SelectDevice(string printerName)
        {
            if (DependencyService.Get<IPrinterConnection>().SetCurrentDevice(printerName))
            {
                var current = DependencyService.Get<IPrinterConnection>().GetCurrentDevice();
                if (current != null)
                {
                    printTextButton.IsEnabled = true;
                }
            }
        }
    }
}