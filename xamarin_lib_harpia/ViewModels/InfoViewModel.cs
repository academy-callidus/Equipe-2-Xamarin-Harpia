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
            LoadPrinterInfo();

            // Carregar dados da dependência da Sunmi
            LoadPackageInfo();
        }

        public async void LoadPrinterInfo()
        {
            var SerialNo = service.GetSerialNo();
            var DeviceModel = service.GetDeviceModel();
            var FirmwareVersion = service.GetFirmwareVersion();
            var Head = service.GetHead();
            var PrintedDistance = await service.GetPrintedDistanceAsync() + "mm";
            var Paper = service.GetPaper() == 1 ? "58mm" : "80mm";
            printerInfo = new PrinterInfo(SerialNo, DeviceModel, FirmwareVersion, Head, PrintedDistance, Paper);
        }

        public void LoadPackageInfo()
        {
            var versionName = service.GetVersionName();
            var versionCode = service.GetVersionCode();
            packageInfo = new PackageInfo(versionName, versionCode);
        }

        public PrinterInfo PrinterInfo { get { return printerInfo; } }
        public PackageInfo PackageInfo { get { return packageInfo; } }
    }
}
