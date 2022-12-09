using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Exceptions
{
    public class PrintBarcodeException : Exception
    {
        public PrintBarcodeException() : base("Erro na impressão de Barcode.") { }
    }
}
