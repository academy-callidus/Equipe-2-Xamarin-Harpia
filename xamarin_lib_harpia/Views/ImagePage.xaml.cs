using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_lib_harpia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagePage : ContentPage
    {
        NavigationPage navigationPage;
        private readonly string[] Alignments = { "Esquerda", "Centro", "Direita" };
        public ImagePage()
        {
            navigationPage = new NavigationPage();
            InitializeComponent();
            DefaultOptions();
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
        
        void OnPrint(object sender, EventArgs e)
        {

        }

    }
}