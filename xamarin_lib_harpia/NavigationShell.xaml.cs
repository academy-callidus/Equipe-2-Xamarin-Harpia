using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xamarin_lib_harpia.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarin_lib_harpia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationShell : Shell
    {
        public NavigationShell()
        {
            InitializeComponent();
     
        }
    }
}