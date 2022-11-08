using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public class BarcodeService
    {
        private IPrinterConnection Connection;

        public BarcodeService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        public bool execute(Barcode barcode)
        {
    
            return true;
        }
    }
}
