using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    internal class EAN8 : NumericalOnlyBarcodeModel
    {
        public EAN8() : base(3, "EAN8", BarcodeFormat.EAN_8)
        {
        }

        public override bool IsValid(string content)
        {
            return base.IsValid(content) && content.Length == 8;
        }
    }
}
