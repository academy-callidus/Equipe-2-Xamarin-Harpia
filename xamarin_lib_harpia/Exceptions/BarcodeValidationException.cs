using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Exceptions
{
    public class BarcodeValidationException : Exception
    {
        public BarcodeValidationException() : base("Barcode Inválido.") { }
    }
}
