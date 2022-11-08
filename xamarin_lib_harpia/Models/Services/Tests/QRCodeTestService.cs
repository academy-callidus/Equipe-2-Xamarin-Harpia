using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services.Tests
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

        public void PrintQRCode(AlignmentEnum align, string dataContent_1, string data_Content_2)
        {

        }
    }
}
