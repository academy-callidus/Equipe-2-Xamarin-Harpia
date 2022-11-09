using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using xamarin_lib_harpia.Views;
using BluetoothPrinter;

namespace xamarin_lib_harpia.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadDemoDetails();

        }

        public string BluetoothConnection()
        {
            var devices = DependencyService.Get<IBluetoothPrinterService>().GetAvailableDevices();
            string action = "";
            if (devices != null && devices.Count > 0)
            {
                var choices = devices.Select(d => d.Title).ToArray();
                action = choices[0];
            }
            return action;
        }

        private void AddDemo(string nome, string path, string navigateTo)
        {
            int itens = Pages.Children.Count();
            int row;
            int col = itens % 2 == 0 ? 1 : 0;


            if (itens % 2 == 0)
            {
                row = itens > 1 ? itens - 1 : 0;
            }
            else
            {
                row = itens > 1 ? itens - 2 : 0;
            }


            FlexLayout flexLayout = new FlexLayout
            {
                Direction = FlexDirection.Column,
                BackgroundColor = (Color)Application.Current.Resources["backgroundColor"],
                AlignItems = FlexAlignItems.Center
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            if (navigateTo != null)
            {
                tapGestureRecognizer.Tapped += async (s, e) => {
                    await Shell.Current.GoToAsync(navigateTo);
                };
            }

            flexLayout.GestureRecognizers.Add(tapGestureRecognizer);
            Image image = new Image
            {
                Source = path,
                WidthRequest = 110,
                HeightRequest = 110
            };
            flexLayout.Children.Add(image);

            flexLayout.Children.Add(new Label
            {
                Text = nome,
                TextColor = Color.Red
            });

            Pages.Children.Add(flexLayout, col, row);
        }



        public void LoadDemoDetails()
        {
            Pages.Children.Clear();
            AddDemo("Teste", "tesImg.png", null);
            AddDemo("QrCode", "function_qr.png", nameof(QrcodePage));
            AddDemo("BarCode", "function_barcode.png", nameof(BarcodePage));
            AddDemo("Text", "function_text.png", nameof(TextPage));
            AddDemo("Blutest", "bluPrinter.png", nameof(TestConnection));
        }

        async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }


    }
}
