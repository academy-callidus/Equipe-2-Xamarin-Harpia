using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Services
{

    internal class QRCodeTestService
    {
        private int ModuleSize { set; get; }
        private int ErrorLevel { set; get; }

        public QRCodeTestService(int moduleSize, int errorLevel)
        {
            ModuleSize = moduleSize;
            ErrorLevel = errorLevel;
        }

        public void PrintQRCode(int align, string dataContent_1, string data_Content_2) // enum de alinhamento
        {

        }
    }
}
