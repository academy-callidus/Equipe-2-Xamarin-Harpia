using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Exceptions
{
    public class PrintTableException : Exception
    {
        public PrintTableException() : base("Erro na impressão de formulário.") { }
    }
}
