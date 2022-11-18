using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models
{
    internal class PrinterInfo
    {
        public string SerialNo { get; set; }
        public string DeviceModel { get; set; }
        public string FirmwareVersion { get; set; }
        public string Head { get; set; }
        public string PrintedDistance { get; set; }
        public string Paper { get; set; }

        public PrinterInfo(string serialNo, string deviceModel, string firmwareVersion, string head, string printedDistance, string paper)
        {
            SerialNo = serialNo;
            DeviceModel = deviceModel;
            FirmwareVersion = firmwareVersion;
            Head = head;
            PrintedDistance = printedDistance;
            Paper = paper;
        }
    }
}
