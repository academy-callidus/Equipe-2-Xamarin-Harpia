using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Exceptions
{
    public class PrintQrcodeException : Exception
    {
        public PrintQrcodeException() : base("Erro na impressão de barcode.") { }
    }
}
