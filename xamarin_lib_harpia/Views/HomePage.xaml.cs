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
        public IList<DemoDetails> Demos { get; set; }
        

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
            int col = itens % 2 == 0 ? 0 : 1;

            if (itens % 2 != 0)
            {
                row = itens > 3 ? itens / 3 - 1 : 0;
            }
            else
            {
                row = itens > 3 ? itens / 3 - 2 : 0;
            }

            /*if(itens % 2 == 0)
            {
                row = itens > 1 ? itens - 1 : 0;  
            }
            else
            {
                row = itens > 1 ? itens - 2 : 0;
            }



            Button pageButton = new Button
        {
            ImageSource = path,
            Text = nome,
            TextColor = (Color)Application.Current.Resources["homepageTextColor"],
            BackgroundColor = (Color)Application.Current.Resources["backgroundColor"],

        };
        pageButton.Clicked += async (sender, args) => await Shell.Current.GoToAsync($"//{nameof(BarcodePage)}");*/

            FlexLayout flexLayout = new FlexLayout
            {
                Direction=FlexDirection.Column,
                BackgroundColor=(Color)Application.Current.Resources["backgroundColor"],
                AlignItems=FlexAlignItems.Center
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) => {
                await Shell.Current.GoToAsync(nameof(BarcodePage));
            };

            flexLayout.GestureRecognizers.Add(tapGestureRecognizer);

            flexLayout.Children.Add(new Image
            {
                Source = path,
                WidthRequest = 90,
                HeightRequest = 90,
            });

            flexLayout.Children.Add(new Label
            {
                Text=nome,
                TextColor= (Color)Application.Current.Resources["homepageTextColor"]
            });

            Pages.Children.Add(flexLayout, col, row);
        }

        public void LoadDemoDetails()
        {
            AddDemo("Teste", "teste.png", "PageTeste");
            AddDemo("BarCode", "teste.png", "PageTeste");
  

        }
    }
}