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
        ObservableCollection<TableItem> datalist = new ObservableCollection<TableItem>();
        public ObservableCollection<TableItem> Datalist { get { return datalist; } }

        public TablePage()
        {
            InitializeComponent();
            InitList(Datalist);
            TableListView.ItemsSource = Datalist;
        }

        void InitList(ObservableCollection<TableItem> data)
        {
            TableItem ti = new TableItem(data);
            data.Add(ti);
        }

        void AddItem(object sender, EventArgs e)
        {
            Datalist.Add(new TableItem(Datalist));
        }
    }

     public class TableItem
    {
        public string RowText { get; set; }

        public TableItem(ObservableCollection<TableItem> data)
        {
            RowText = "Row." + (data.Count + 1).ToString();
        }
    }
}