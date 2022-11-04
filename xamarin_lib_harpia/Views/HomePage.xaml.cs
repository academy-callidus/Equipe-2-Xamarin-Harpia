using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Models;
using xamarin_lib_harpia.Views;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        

        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadDemoDetails();

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
            tapGestureRecognizer.Tapped += async (s, e) => {
                await Shell.Current.GoToAsync(navigateTo);
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
            }) ;

            Pages.Children.Add(flexLayout, col, row);
        }

        public void LoadDemoDetails()
        {
            Pages.Children.Clear();
            AddDemo("Teste", "tesImg.png", nameof(QrcodePage));
            AddDemo("QrCode", "function_qr.png", nameof(QrcodePage));
            AddDemo("BarCode", "function_barcode.png", nameof(BarcodePage));
            AddDemo("Text", "function_text.png", nameof(TextPage));


        }
    }
}