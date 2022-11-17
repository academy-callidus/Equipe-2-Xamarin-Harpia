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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome">Nome a ser exibido no ícone da Homepage</param>
        /// <param name="path">Caminho onde se encontra o ícone</param>
        /// <param name="onTap">Função que será executada ao clicar no ícone</param>
        private void AddDemo(string nome, string iconPath, Func<Task> onTap)
        {

            Frame page = new Frame
            {
                BackgroundColor = (Color)Application.Current.Resources["backgroundColor"],
               
                HasShadow = false,
                BorderColor = (Color)Application.Current.Resources["borderColor"],
                Margin = new Thickness(-1),
                Content = new StackLayout
                {
                    Padding = new Thickness(10, 0, 10, 0), //left top right bottom
                    
                    Children =
                    {
                        new Image
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
        /// Carrega todos os botões que serão exibidos na Homepage
        /// </summary>
        public void LoadDemoDetails()
        {
            Pages.Children.Clear();
            AddDemo("Teste Completo", "tesImg.png", RunFullTest());
            AddDemo("QR Code", "function_qr.png", NavigateTo(nameof(QrcodePage)));
            AddDemo("Bar Code", "function_barcode.png", NavigateTo(nameof(BarcodePage)));
            AddDemo("Texto", "function_text.png", NavigateTo(nameof(TextPage)));
        }

        /// <summary>
        /// Cria uma nova instância de FullTestService e retorna uma função assíncrona
        /// </summary>
        /// <returns>Função assíncrona que executa todos os tipos de impressão da aplicação</returns>
        private Func<Task> RunFullTest()
        {
            var service = new FullTestService();

            return new Func<Task>(async () => await Task.Run(() => service.RunAllTests()));
        }
        /// <summary>
        /// Navega para a página especificada, convertendo implicitamente string para uri
        /// </summary>
        /// <param name="url">Próxima página</param>
        /// <returns>Função que navage assincronamente para uma nova página</returns>
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
