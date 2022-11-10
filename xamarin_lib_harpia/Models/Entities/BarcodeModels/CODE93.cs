using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    internal class CODE93 : BarcodeModel
    {
        public CODE93() : base(7, "CODE93", BarcodeFormat.CODE_93)
        {
        }
    }
}
