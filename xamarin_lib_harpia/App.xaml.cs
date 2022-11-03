using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_lib_harpia
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new QrcodePage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
