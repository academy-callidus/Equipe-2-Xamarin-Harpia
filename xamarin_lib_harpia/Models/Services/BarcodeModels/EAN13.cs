using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Services.BarcodeModels
{
    internal class EAN13 : NumericalOnlyBarcodeModel
    {
        public EAN13() : base("EAN13")
        {
        }

        public override bool IsValid(string content)
        {
            return base.IsValid(content) && content.Length == 13;
        }
    }
}
