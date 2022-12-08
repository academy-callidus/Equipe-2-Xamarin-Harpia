using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Exceptions
{
    class PrintImageException : Exception
    {
        public PrintImageException() : base("Erro na impressão de imagem.") { } 
    }
}
