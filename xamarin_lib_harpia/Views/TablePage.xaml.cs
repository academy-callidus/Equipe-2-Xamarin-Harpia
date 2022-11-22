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
            StackLayout stack = new StackLayout() { BackgroundColor = Color.FromHex("#181A26") };

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
                    new Label() { Text="Conteúdo", TextColor=Color.White, FontSize=20, Padding=new Thickness(0, 8, 0, 0) },
                    new Entry() { Text="texto", TextColor=Color.White, WidthRequest=70 },
                    new Entry() { Text="texto", TextColor=Color.White, WidthRequest=70 },
                    new Entry() { Text="texto", TextColor=Color.White, WidthRequest=70 },
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
                    new Label() { Text="weight%", TextColor=Color.White, FontSize=20, Padding=new Thickness(0, 8, 0, 0) },
                    new Entry() { Text="1", TextColor=Color.White, WidthRequest=70, Keyboard=Keyboard.Numeric },
                    new Entry() { Text="1", TextColor=Color.White, WidthRequest=70, Keyboard=Keyboard.Numeric },
                    new Entry() { Text="1", TextColor=Color.White, WidthRequest=70, Keyboard=Keyboard.Numeric }
                }
            };

            return flexWeight;
        }

        StackLayout AlignStack()
        {
            StackLayout stackAlign = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label() { Text="Alinhamento", TextColor=Color.White, FontSize=20 },
                }
            };

            Label alignLabel1 = new Label { Text = "Esquerda", TextColor = Color.Red, FontSize = 15, WidthRequest=70, Padding=new Thickness(0, 4, 0, 0) };

            FlexLayout flex1 = new FlexLayout()
            {
                Children =
                {
                    alignLabel1
                }
            };

            Label alignLabel2 = new Label { Text = "Esquerda", TextColor = Color.Red, FontSize = 15, WidthRequest = 70, Padding=new Thickness(0, 4, 0, 0) };

            FlexLayout flex2 = new FlexLayout()
            {
                Children =
                {
                    alignLabel2
                }
            };

            Label alignLabel3 = new Label { Text = "Esquerda", TextColor = Color.Red, FontSize = 15, WidthRequest = 70, Padding=new Thickness(0, 4, 0, 0) };

            FlexLayout flex3 = new FlexLayout()
            {
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
                HeightRequest = 70
            };

            tableButton.Clicked += (sender, e) =>
            {
                rows++;
                TableStack.Children.Add(InitTable());
            };

            Button printButton = new Button()
            {
                Text = "Imprimir",
                TextColor = Color.White,
                BackgroundColor = Color.Red
            };

            stack.Children.Add(tableButton);
            stack.Children.Add(printButton);

            return stack;
        }
    }
}