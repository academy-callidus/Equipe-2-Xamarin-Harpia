using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_lib_harpia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public class DemoDetails
    {
        private int titleId;

        private int iconResID;

        public DemoDetails(String titleId, String descriptionId)
        {
            this.titleId = titleId;
            this.iconResID = descriptionId;
        }
    }

    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void LoadDemoDetails()
        {
            private readonly DemoDetails[] demos =
        {
            new DemoDetails()
        }
        }
    }

}