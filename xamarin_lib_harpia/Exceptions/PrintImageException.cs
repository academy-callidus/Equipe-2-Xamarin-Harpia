using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Exceptions
{
    public class PrintImageException : Exception
    {
        public PrintImageException() : base("Erro na impressão de imagem.") { } 
    }
}
