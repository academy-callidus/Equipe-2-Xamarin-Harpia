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
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly AdvancePaperService AdvancePaperService;
        private readonly GenerateStatusService GenerateStatusService;

        public MainPage()
        {
            InitializeComponent();
            LoadDemoDetails();
            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            AdvancePaperService = new AdvancePaperService(connection);
            GenerateStatusService = new GenerateStatusService(connection);
        }
        /// <summary>
        /// Adds a new icon to Homepage (Frame containing image and text)
        /// Adds to the new icon a function that will be executed when clicking it
        /// </summary>
        private void AddDemo(string nome, string iconPath, Func<Task> onTap)
        {

            Frame page = new Frame
            {
                // Looks up in the dictionary of App.xaml the color corresponding to "backgroundColor"
                BackgroundColor = (Color)Application.Current.Resources["backgroundColor"],
               
                HasShadow = false,
                BorderColor = (Color)Application.Current.Resources["borderColor"],
                Margin = new Thickness(-1),
                Content = new StackLayout
                {
                    Padding = new Thickness(10, 0, 10, 0), //left, top, right, bottom
                    
                    Children =
                    {
                        new Xamarin.Forms.Image
                        {
                            Source = iconPath,
                            Margin = new Thickness(5, 0, 5, 0),
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


        /// <summary>
        /// Load all buttons that will be displayed on the Homepage
        /// </summary>
        public void LoadDemoDetails()
        {
            Pages.Children.Clear();
            AddDemo("Teste Completo", "tesImg.png", RunFullTest());
            AddDemo("QR Code", "function_qr.png", NavigateTo(nameof(QrcodePage)));
            AddDemo("Bar Code", "function_barcode.png", NavigateTo(nameof(BarcodePage)));
            AddDemo("Texto", "function_text.png", NavigateTo(nameof(TextPage)));
            AddDemo("Formulário", "function_tab.png", NavigateTo(nameof(TablePage)));
            AddDemo("Imagem", "function_pic.png", NavigateTo(nameof(ImagePage)));
            AddDemo("PayGo", "function_payment.png", NavigateTo(nameof(PaygoPage)));
            AddDemo("Avançar Papel", "function_threeline.png", RunAdvancePaper());
            AddDemo("Exibe Status", "function_status.png", RunPrintStatus());
        }

        /// <summary>
        /// Creates a new instance of FullTestService and returns an asynchronous 
        /// function that performs all types of printing in the application
        /// </summary>
        private Func<Task> RunFullTest()
        {
            var service = new FullTestService();

            return new Func<Task>(async () => await Task.Run(() =>
            {
                try
                {
                    service.RunAllTests();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }));
        }
        /// <summary>
        /// Return a function that navigates to the specified page, implicitly converting string to uri
        /// </summary>
        private Func<Task> NavigateTo(string url)
        {
            return new Func<Task>(async () => await Shell.Current.GoToAsync(url));
        }

        async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        /// <summary>
        /// Returns an asynchronous function that advances the machine`s paper
        /// </summary>
        private Func<Task> RunAdvancePaper()
        {
            return new Func<Task>(async () => await Task.Run(() =>
            {
                try
                {
                    AdvancePaperService.Execute();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }));

        }

        private Func<Task> RunPrintStatus()
        {
            return new Func<Task>(async () => await Task.Run(() => GenerateStatusService.PrintStatus(this)));
        }
    }
}
