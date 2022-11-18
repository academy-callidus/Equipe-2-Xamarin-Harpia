using System;
using System.Threading.Tasks;
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

        /// <summary>
        /// Try printing a barcode if it has a valid format
        /// </summary>
        public bool Execute(Barcode barcode)
        {
            if (!barcode.IsValid()) return false;
            try
            {
                var response = Connection.PrintBarcode(barcode);
                return response;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}
