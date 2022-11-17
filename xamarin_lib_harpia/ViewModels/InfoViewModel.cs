using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using xamarin_lib_harpia.Models;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.ViewModels
{
    internal class InfoViewModel : INotifyPropertyChanged
    {
        PrinterInfo printerInfo;
        PackageInfo packageInfo;

        private readonly InfoService service;

        public event PropertyChangedEventHandler PropertyChanged;

        public InfoViewModel()
        {
            var connection = DependencyService.Get<IPrinterConnection>();
            service = new InfoService(connection);

            // Carregar dados da impressora atualmente conectada
            printerInfo = new PrinterInfo();
            LoadPrinterInfo();

            // Carregar dados da dependência da Sunmi
            packageInfo = new PackageInfo();
            LoadPackageInfo();
        }

        public void LoadPrinterInfo()
        {
            printerInfo.SerialNo = service.GetSerialNo();
            printerInfo.FirmwareVersion = service.GetFirmwareVersion();
            printerInfo.Head = service.GetHead();
            printerInfo.PrintedDistance = service.GetPrintedDistance() + "mm";
            printerInfo.Paper = service.GetPaper() == 1 ? "58mm" : "80mm";
        }

        public void LoadPackageInfo()
        {
            packageInfo.versionName = service.GetVersionName();
            packageInfo.versionCode = service.GetVersionCode();
        }

        public PrinterInfo PrinterInfo { get { return printerInfo; } }
        public PackageInfo PackageInfo { get { return packageInfo; } }
    }
}
