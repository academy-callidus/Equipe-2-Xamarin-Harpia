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
            LoadDemoDetails();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            

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

            Frame page = new Frame
            {
                BackgroundColor = (Color)Application.Current.Resources["backgroundColor"],
                WidthRequest = 153,
                HeightRequest = 140,
                BorderColor = (Color)Application.Current.Resources["borderColor"],
                Margin = new Thickness(-1),
                Content = new StackLayout
                {
                    
                    Children =
                    {
                        new Image
                        {
                            Source = path,
                            WidthRequest = 110,
                            HeightRequest = 110
                        },
                        new Label
                        {
                            Text = nome,
                            TextColor = Color.Red,
                            HorizontalOptions = LayoutOptions.Center,
                            
                        },
                    },
                }
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) => {
                await onTap();
            };

            page.GestureRecognizers.Add(tapGestureRecognizer);

            Pages.Children.Add(page);
        }



        public void LoadDemoDetails()
        {
            Pages.Children.Clear();
            AddDemo("Teste Completo", "tesImg.png", RunFullTest());
            AddDemo("QR Code", "function_qr.png", NavigateTo(nameof(QrcodePage)));
            AddDemo("Bar Code", "function_barcode.png", NavigateTo(nameof(BarcodePage)));
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
