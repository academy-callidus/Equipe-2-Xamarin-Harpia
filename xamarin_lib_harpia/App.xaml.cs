using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Views;


namespace xamarin_lib_harpia
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HomePage();
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
