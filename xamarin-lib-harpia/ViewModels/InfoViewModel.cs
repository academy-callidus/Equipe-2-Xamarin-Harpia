using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using xamarin_lib_harpia.Models;

namespace xamarin_lib_harpia.ViewModels
{
    internal class InfoViewModel : INotifyPropertyChanged
    {
        PrinterInfo printerInfo;
        PackageInfo packageInfo;

        public event PropertyChangedEventHandler PropertyChanged;

        public InfoViewModel()
        {
            // Carregar dados da impressora atualmente conectada
            printerInfo = new PrinterInfo();

            // Carregar dados da dependência da Sunmi
            packageInfo = new PackageInfo();
        }

        public PrinterInfo PrinterInfo { get { return printerInfo; } }
        public PackageInfo PackageInfo { get { return packageInfo; } }
    }
}
