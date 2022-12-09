using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Exceptions
{
    public class AdvancePaperException : Exception
    {
        public AdvancePaperException() : base("Erro ao avançar papel.") { }
    }
}
