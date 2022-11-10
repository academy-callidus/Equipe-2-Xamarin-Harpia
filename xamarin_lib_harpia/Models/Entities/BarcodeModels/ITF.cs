using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    internal class ITF : NumericalOnlyBarcodeModel
    {
        public ITF() : base(5, "ITF", BarcodeFormat.ITF)
        {
        }
    }
}
