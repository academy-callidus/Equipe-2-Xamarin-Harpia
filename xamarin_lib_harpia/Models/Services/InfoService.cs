using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    internal class InfoService
    {
        private IPrinterConnection Connection;

        public InfoService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        public string GetSerialNo()
        {
            var response = Connection.GetPrinterSerialNo();
            return response;
        }
        public string GetDeviceModel()
        {
            var response = Connection.GetPrinterModel();
            return response;
        }
        public string GetFirmwareVersion()
        {
            var response = Connection.GetFirmwareVersion();
            return response;
        }
        public string GetHead()
        {
            //TO-DO
            var response = "";
            return response;
        }

        public int GetPrintedDistance()
        {
            var response = Connection.GetPrintedLength();
            return response;
        }

        public int GetPaper()
        {
            var response = Connection.GetPrinterPaper();
            return response;
        }

        public string GetVersionName()
        {
            var response = Connection.GetServiceVersionName();
            return response;
        }

        public string GetVersionCode()
        {
            var response = Connection.GetServiceVersionCode();
            return response;
        }
    }
}
