using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePage : ContentPage
    {
        private ImageService Service;
        private readonly string[] Alignments = { "Esquerda", "Centro", "Direita" };
        public ImagePage()
        {
            InitializeComponent();
            DefaultOptions();
            IPrinterConnection connection = DependencyService.Get<IPrinterConnection>();
            Service = new ImageService(connection);
        }

        /// <summary>
        /// Defines the text at the left side of the arrow
        /// </summary>
        private void DefaultOptions()
        {
            var AlignLabel = this.FindByName<Label>("AlignLabel");

            AlignLabel.Text = Alignments[0];
        }

        /// <summary>
        /// It is used to display a chart containing the alignment's options
        /// </summary>
        
        private async void OnAlignment(object sender, EventArgs e)
        {
            var AlignLabel = this.FindByName<Label>("AlignLabel");
            var AlignOption= await DisplayActionSheet(
                "Alinhamento",
                "cancelar",
                null,
                Alignments
            );
            if (AlignOption == null || AlignOption == "" || AlignOption == "cancelar") return;
            AlignLabel.Text = AlignOption;
        }

        /// <summary>
        /// This function is used to print the image
        /// </summary>
        async void OnPrint(object sender, EventArgs e)
        {
            var source = ImagePrint.Source;

            string resource = "";
            // It may be necessary to implement other possible sources besides local files
            if (source is FileImageSource)
            {
                Console.WriteLine("File Source");
                resource = ((FileImageSource)source).File;
            }

            var wasSuccessful = Service.Execute(resource);
            if(!wasSuccessful) await DisplayAlert("Impressão de imagem", "Erro ao realizar impressão!", "OK");

        }

    }
}