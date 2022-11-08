using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Services.BarcodeModels
{
    internal abstract class BarcodeModel
    {
        private string Name { get; set; }

        public BarcodeModel(string name)
        {
            Name = name;
        }

        public virtual bool IsValid(string content)
        {
            if (content == null) return false;
            return true;
        }
    }
}
