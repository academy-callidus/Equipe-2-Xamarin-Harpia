using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public class TextService
    {
        private IPrinterConnection Connection;

        public TextService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        public bool execute(Text text)
        {

            return true;
        }
    }
}
