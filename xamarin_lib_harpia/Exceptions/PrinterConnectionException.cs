using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Exceptions
{
    public class PrinterConnectionException : Exception
    {
        public PrinterConnectionException() : base("Não foi possível se conectar com a impressora.") { }
    }
}
