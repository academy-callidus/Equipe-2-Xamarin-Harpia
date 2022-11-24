using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Models.Entities;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TablePage : ContentPage
    {
        private int rows = 1;
        private List<List<Entry>> Contents;
        private List<List<Entry>> Widths;
        private List<List<Label>> Alignments;
        private TableService TableService;

        public TablePage()
        {
            InitializeComponent();
            Contents = new List<List<Entry>>();
            Widths = new List<List<Entry>>();
            Alignments = new List<List<Label>>();

            MainStack.BackgroundColor = Color.FromHex("#181A26");
            TableStack.Children.Add(InitTable());
            MainStack.Children.Add(Buttons());

            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            TableService = new TableService(connection);
        }

        private StackLayout InitTable()
        {
            StackLayout stack = new StackLayout() {
                BackgroundColor = Color.FromHex("#181A26"), 
                Padding=new Thickness(0, 5, 0, 5) 
            };

            stack.Children.Add(RowFlex());
            stack.Children.Add(ContentFlex());
            stack.Children.Add(WeightFlex());
            stack.Children.Add(AlignStack());

            return stack;
        }

        private FlexLayout RowFlex()
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

        private FlexLayout ContentFlex()
        {
            var content1 = new Entry() { Text = "texto", TextColor = Color.White, WidthRequest = 60 };
            var content2 = new Entry() { Text = "texto", TextColor = Color.White, WidthRequest = 60 };
            var content3 = new Entry() { Text = "texto", TextColor = Color.White, WidthRequest = 60 };
            FlexLayout flexConteudo = new FlexLayout()
            {
                JustifyContent = FlexJustify.SpaceAround,
                Children =
                {
                    new Label() { Text="Conteúdo", TextColor=Color.White, FontSize=18, Padding=new Thickness(10, 8, 0, 0) },
                    content1,
                    content2,
                    content3,
                }
            };
            Contents.Add(new List<Entry>() { content1, content2, content3 });
            return flexConteudo;
        }

        private FlexLayout WeightFlex()
        {
            var width1 = new Entry() { Text = "1", TextColor = Color.White, WidthRequest = 60, Keyboard = Keyboard.Numeric, HorizontalTextAlignment = TextAlignment.Center };
            var width2 = new Entry() { Text = "1", TextColor = Color.White, WidthRequest = 60, Keyboard = Keyboard.Numeric, HorizontalTextAlignment = TextAlignment.Center };
            var width3 = new Entry() { Text = "1", TextColor = Color.White, WidthRequest = 60, Keyboard = Keyboard.Numeric, HorizontalTextAlignment = TextAlignment.Center };
            FlexLayout flexWeight = new FlexLayout()
            {
                JustifyContent = FlexJustify.SpaceAround,
                Children =
                {
                    new Label() { Text="weight%", TextColor=Color.White, FontSize=18, Padding=new Thickness(20, 8, 0, 0), HorizontalOptions=LayoutOptions.End },
                    width1,
                    width2,
                    width3
                }
            };
            Widths.Add(new List<Entry>() { width1, width2, width3 });
            return flexWeight;
        }

        private StackLayout AlignStack()
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

            Alignments.Add(new List<Label>() { alignLabel1, alignLabel2, alignLabel3 });

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

        private StackLayout Buttons()
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

        private List<Table> GetTablesEntity()
        {
            List<Table> tables = new List<Table>();
            for (int i = 0; i < rows; i++)
            {
                var content1 = Contents[i][0].Text;
                var content2 = Contents[i][1].Text;
                var content3 = Contents[i][2].Text;

                var contents = new string[]{ content1, content2, content3 };

                var width1 = Int32.Parse(Widths[i][0].Text) + 9;
                var width2 = Int32.Parse(Widths[i][1].Text) + 9;
                var width3 = Int32.Parse(Widths[i][2].Text) + 9;

                var widths = new int[]{ width1, width2, width3};
                
                var align1 = Alignments[i][0].Text == "Esquerda" ? AlignmentEnum.LEFT :
                    Alignments[i][0].Text == "Centro" ? AlignmentEnum.CENTER : AlignmentEnum.RIGHT;
                var align2 = Alignments[i][1].Text == "Esquerda" ? AlignmentEnum.LEFT :
                    Alignments[i][1].Text == "Centro" ? AlignmentEnum.CENTER : AlignmentEnum.RIGHT;
                var align3 = Alignments[i][2].Text == "Esquerda" ? AlignmentEnum.LEFT :
                    Alignments[i][2].Text == "Centro" ? AlignmentEnum.CENTER : AlignmentEnum.RIGHT;

                var alignments = new AlignmentEnum[] { align1, align2, align3 };
                tables.Add(new Table(contents, widths, alignments));
            }
            return tables;
        }

        private async void OnPrint(object sender, EventArgs e)
        {
            var tables = GetTablesEntity();
            var wasSuccessful = true;
            foreach (var table in tables)
            {
                wasSuccessful = TableService.Execute(table);
                if (!wasSuccessful)
                    break;
            }
            if (!wasSuccessful)
                if (!wasSuccessful) await DisplayAlert("Impressão de Formulário", "Erro ao realizar impressão!", "OK");
        }
    }
}