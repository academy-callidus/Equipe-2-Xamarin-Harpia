using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        /// <summary>
        /// Asynchronously loads all printer data and instantiates a new PrinterInfo model object, 
        /// firing a PropertyChanged event to update the view.
        /// </summary>
        public async void LoadPrinterInfo()
        {
            var SerialNo = service.GetSerialNo();
            var DeviceModel = service.GetDeviceModel();
            var FirmwareVersion = service.GetFirmwareVersion().Trim();
            var Head = service.GetHead();
            var PrintedDistance = await service.GetPrintedDistanceAsync() + "mm";
            var Paper = service.GetPaper() == 1 ? "58mm" : "80mm";
            PrinterInfo = new PrinterInfo(SerialNo, DeviceModel, FirmwareVersion, Head, PrintedDistance, Paper);
        }

        /// <summary>
        /// Synchronously loads all package data and instantiates a new PackageInfo model object,
        /// firing a PropertyChanged event to update the view.
        /// </summary>
        public void LoadPackageInfo()
        {
            var versionName = service.GetVersionName();
            var versionCode = service.GetVersionCode();
            PackageInfo = new PackageInfo(versionName, versionCode);
        }

        /// <summary>
        /// Helper method to fire PropertyChange events on class member setter methods.
        /// </summary>
        /// <param name="propertyName">String representation of the property being set.</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PrinterInfo PrinterInfo { 
            get { return printerInfo; }
            set 
            {
                printerInfo = value;
                OnPropertyChanged();
            }
        }
        public PackageInfo PackageInfo {
            get { return packageInfo; }
            set
            {
                packageInfo = value;
                OnPropertyChanged();
            }
        }
    }
}
