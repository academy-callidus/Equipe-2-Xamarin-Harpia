using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    internal class EAN13 : NumericalOnlyBarcodeModel
    {
        public EAN13() : base(2, "EAN13", BarcodeFormat.EAN_13)
        {
        }

        public override bool IsValid(string content)
        {
            return base.IsValid(content) && content.Length == 13;
        }
    }
}
