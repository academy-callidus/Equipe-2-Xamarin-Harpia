using System;
using System.Collections;
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
        ArrayList datalist = new ArrayList();

        public TablePage()
        {
            InitializeComponent();
        }

        void initList(ArrayList data)
        {
            TableItem ti = new TableItem();
            data.Add(ti);
        }
    }

     class TableItem
    {
        public string[] Text { get; set; }
        public int[] Weigth { get; set; }
        public string[] Align { get; set; }

        public TableItem()
        {
            Text = new string[] { "texto" , "texto", "texto" };
            Weight = new int[] { 1, 1, 1 };
            Align = new string[] { "Esquerda", "Esquerda", "Esquerda" };
        }
    }
}