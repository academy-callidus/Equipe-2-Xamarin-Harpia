using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablePage : ContentPage
    {
        private int rows = 1;
        public TablePage()
        {
            InitializeComponent();

            MainStack.BackgroundColor = Color.FromHex("#181A26");
            TableStack.Children.Add(InitTable());
            MainStack.Children.Add(Buttons());
        }

        StackLayout InitTable()
        {
            StackLayout stack = new StackLayout() { BackgroundColor = Color.FromHex("#181A26"), Padding=new Thickness(0, 5, 0, 5) };

            stack.Children.Add(RowFlex());
            stack.Children.Add(ContentFlex());
            stack.Children.Add(WeightFlex());
            stack.Children.Add(AlignStack());

            return stack;
        }

        FlexLayout RowFlex()
        {
            FlexLayout flexRow = new FlexLayout()
            {
                BackgroundColor = Color.FromHex("#464851"),
                HeightRequest = 30,
                Children =
                {
                    new Label() { Text = "Row." + rows.ToString(), FontSize = 16, TextColor = Color.White, Padding=new Thickness(0, 4, 0, 0)},
                }
            };

            return flexRow;
        }

        FlexLayout ContentFlex()
        {
            FlexLayout flexConteudo = new FlexLayout()
            {
                JustifyContent = FlexJustify.SpaceAround,
                Children =
                {
                    new Label() { Text="Conteúdo", TextColor=Color.White, FontSize=18, Padding=new Thickness(10, 8, 0, 0) },
                    new Entry() { Text="texto", TextColor=Color.White, WidthRequest=60 },
                    new Entry() { Text="texto", TextColor=Color.White, WidthRequest=60 },
                    new Entry() { Text="texto", TextColor=Color.White, WidthRequest=60 },
                }
            };

            return flexConteudo;
        }

        FlexLayout WeightFlex()
        {
            FlexLayout flexWeight = new FlexLayout()
            {
                JustifyContent = FlexJustify.SpaceAround,
                Children =
                {
                    new Label() { Text="weight%", TextColor=Color.White, FontSize=18, Padding=new Thickness(20, 8, 0, 0), HorizontalOptions=LayoutOptions.End },
                    new Entry() { Text="1", TextColor=Color.White, WidthRequest=60, Keyboard=Keyboard.Numeric, HorizontalTextAlignment=TextAlignment.Center },
                    new Entry() { Text="1", TextColor=Color.White, WidthRequest=60, Keyboard=Keyboard.Numeric, HorizontalTextAlignment=TextAlignment.Center },
                    new Entry() { Text="1", TextColor=Color.White, WidthRequest=60, Keyboard=Keyboard.Numeric, HorizontalTextAlignment=TextAlignment.Center }
                }
            };

            return flexWeight;
        }

        StackLayout AlignStack()
        {
            StackLayout stackAlign = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(13, 0, 0, 0),
                Children =
                {
                    new Label() { Text="Alinhamento", TextColor=Color.White, FontSize=18 },
                }
            };

            Label alignLabel1 = new Label { Text = "Esquerda", TextColor = Color.Red, FontSize = 15, Padding=new Thickness(0, 4, 0, 0) };

            FlexLayout flex1 = new FlexLayout()
            {
                JustifyContent = FlexJustify.Center,
                WidthRequest = 70,
                Padding = new Thickness(0, 0, 5, 0),
                Children =
                {
                    alignLabel1
                }
            };

            Label alignLabel2 = new Label { Text = "Esquerda", TextColor = Color.Red, FontSize = 15, Padding=new Thickness(0, 4, 0, 0) };

            FlexLayout flex2 = new FlexLayout()
            {
                JustifyContent = FlexJustify.Center,
                WidthRequest = 70,
                Padding=new Thickness(0, 0, 5, 0),
                Children =
                {
                    alignLabel2
                }
            };

            Label alignLabel3 = new Label { Text = "Esquerda", TextColor = Color.Red, FontSize = 15, Padding=new Thickness(0, 4, 0, 0) };

            FlexLayout flex3 = new FlexLayout()
            {
                JustifyContent = FlexJustify.Center,
                WidthRequest = 70,
                Children =
                {
                    alignLabel3
                }
            };

            var tapGestureRecognizer1 = new TapGestureRecognizer();
            tapGestureRecognizer1.Tapped += async (s, e) =>
            {
                string[] aligns = { "Esquerda", "Centro", "Direita" };
                string option = await DisplayActionSheet("Alinhamento", "cancelar", null, aligns);

                if ((option != "cancelar") && (option != null))
                {
                    alignLabel1.Text = option;
                }
            };
            flex1.GestureRecognizers.Add(tapGestureRecognizer1);

            var tapGestureRecognizer2 = new TapGestureRecognizer();
            tapGestureRecognizer2.Tapped += async (s, e) =>
            {
                string[] aligns = { "Esquerda", "Centro", "Direita" };
                string option = await DisplayActionSheet("Alinhamento", "cancelar", null, aligns);

                if ((option != "cancelar") && (option != null))
                {
                    alignLabel2.Text = option;
                }
            };
            flex2.GestureRecognizers.Add(tapGestureRecognizer2);

            var tapGestureRecognizer3 = new TapGestureRecognizer();
            tapGestureRecognizer3.Tapped += async (s, e) =>
            {
                string[] aligns = { "Esquerda", "Centro", "Direita" };
                string option = await DisplayActionSheet("Alinhamento", "cancelar", null, aligns);

                if ((option != "cancelar") && (option != null))
                {
                    alignLabel3.Text = option;
                }
            };
            flex3.GestureRecognizers.Add(tapGestureRecognizer3);


            stackAlign.Children.Add(flex1);
            stackAlign.Children.Add(flex2);
            stackAlign.Children.Add(flex3);

            return stackAlign;
        }

        StackLayout Buttons()
        {
            StackLayout stack = new StackLayout() { Orientation = StackOrientation.Vertical };

            Button tableButton = new Button()
            {
                Text = "Adicionar",
                TextColor = Color.Gray,
                BackgroundColor = Color.White,
                HeightRequest = 60
            };

            tableButton.Clicked += (sender, e) =>
            {
                rows++;
                TableStack.Children.Add(InitTable());
            };
            stack.Children.Add(tableButton);

            return stack;
        }

        private void GetTableEntity()
        {

        }

        private async void OnPrint(object sender, EventArgs e)
        {
            await DisplayAlert("Alerta", "Imprimindo formulátio...", "Cancelar");
        }
    }
}