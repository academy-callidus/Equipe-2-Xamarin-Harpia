using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models
{
    internal class PrinterInfo
    {
        public string serialNo { get; }
        public string deviceModel { get; }
        public string firmwareVersion { get; }
        public string head { get; }
        public string printedDistance { get; }
        public string paper { get; }
    }
}
