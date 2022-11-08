using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Services.BarcodeModels
{
    internal class EAN8 : NumericalOnlyBarcodeModel
    {
        public EAN8() : base("EAN8")
        {
        }

        public override bool IsValid(string content)
        {
            return base.IsValid(content) && content.Length == 8;
        }
    }
}
