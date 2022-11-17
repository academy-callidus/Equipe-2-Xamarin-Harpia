using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using xamarin_lib_harpia.Views;
using xamarin_lib_harpia.Models.Services;

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
            var devices = DependencyService.Get<IPrinterConnection>().GetAvailableDevices();
            string action = "";
            if (devices != null && devices.Count > 0)
            {
                var choices = devices.Select(d => d.Title).ToArray();
                action = choices[0];
            }
            return action;
        }

        private void AddDemo(string nome, string path, Func<Task> onTap)
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
            tapGestureRecognizer.Tapped += async (s, e) => {
                await onTap();
            };
            

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
            AddDemo("Teste", "tesImg.png", RunFullTest());
            AddDemo("QrCode", "function_qr.png", NavigateTo(nameof(QrcodePage)));
            AddDemo("BarCode", "function_barcode.png", NavigateTo(nameof(BarcodePage)));
            AddDemo("Texto", "function_text.png", NavigateTo(nameof(TextPage)));
        }

        private Func<Task> RunFullTest()
        {
            var service = new FullTestService();

            return new Func<Task>(async () => await Task.Run(() => service.RunAllTests()));
        }

        private Func<Task> NavigateTo(string url)
        {
            return new Func<Task>(async () => await Shell.Current.GoToAsync(url));
        }

        async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }


    }
}
