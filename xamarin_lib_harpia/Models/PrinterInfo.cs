using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models
{
    internal class PrinterInfo
    {
        public string SerialNo { get; }
        public string DeviceModel { get; }
        public string FirmwareVersion { get; }
        public string Head { get; }
        public string PrintedDistance { get; }
        public string Paper { get; }
    }
}
