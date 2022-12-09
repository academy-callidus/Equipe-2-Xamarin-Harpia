using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Exceptions
{
    public class QrcodeValidationException : Exception
    {
        public QrcodeValidationException() : base("Qrcode inválido.") { }
    }
}
