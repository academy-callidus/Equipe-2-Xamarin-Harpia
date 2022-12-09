using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Exceptions
{
    public class PrintTextException : Exception
    {
        public PrintTextException() : base("Erro na impressão de texto.") { }
    }
}
