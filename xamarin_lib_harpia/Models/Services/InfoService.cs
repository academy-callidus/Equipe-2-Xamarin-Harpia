using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        /// <summary>
        /// Gets the serial number of the currently connected printer (if any)
        /// </summary>
        /// <returns>Printer Serial Number</returns>
        public string GetSerialNo()
        {
            var response = Connection.GetPrinterSerialNo();
            return response;
        }

        /// <summary>
        /// Gets a string representation of the device model of the currently connected printer (if any)
        /// </summary>
        /// <returns>Device Model</returns>
        public string GetDeviceModel()
        {
            var response = Connection.GetPrinterModel();
            return response;
        }

        /// <summary>
        /// Gets the firmware version of the currently connected printer (if any)
        /// </summary>
        /// <returns>Firmware Version</returns>
        public string GetFirmwareVersion()
        {
            var response = Connection.GetFirmwareVersion();
            return response;
        }

        /// <summary>
        /// Gets a string representation of the printer head of the currently connected printer (if any)
        /// </summary>
        /// <returns>Printer head</returns>
        public string GetHead()
        {
            //TO-DO
            var response = "";
            return response;
        }

        /// <summary>
        /// Gets the total distance of paper used in printing by the currently connected printer (if any)
        /// </summary>
        /// <returns>Printer paper distance, in millimeters</returns>
        public async Task<int> GetPrintedDistanceAsync()
        {
            var response = await Connection.GetPrintedLength();
            return int.Parse(response);
        }

        /// <summary>
        /// Gets the paper length used by the currently connected printer (if any)
        /// </summary>
        /// <returns>Paper length, in millimeters</returns>
        public int GetPaper()
        {
            var response = Connection.GetPrinterPaper();
            return response;
        }

        /// <summary>
        /// Gets the version name of the package used to connect with the printer
        /// </summary>
        /// <returns>Connection Package version name</returns>
        public string GetVersionName()
        {
            var response = Connection.GetServiceVersion();
            return response;
        }

        /// <summary>
        /// Gets the version code of the package used to connect with the printer
        /// </summary>
        /// <returns>Connection Package version code</returns>
        public string GetVersionCode()
        {
            var response = Connection.GetServiceVersionCode();
            return response;
        }
    }
}
