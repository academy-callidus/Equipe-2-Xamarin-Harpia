using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace xamarin_lib_harpia.Models.Services.BarcodeModels
{
    internal abstract class NumericalOnlyBarcodeModel : BarcodeModel
    {
        public NumericalOnlyBarcodeModel(string name) : base(name)
        {
        }

        public override bool IsValid(string content)
        {
            if (!base.IsValid(content)) return false;
            Regex regex = new Regex(@"^[0-9]+$");
            return regex.IsMatch(content);
        }
    }
}
