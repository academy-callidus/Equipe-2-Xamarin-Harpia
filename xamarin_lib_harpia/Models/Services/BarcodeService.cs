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

        public async Task<bool> Execute(Barcode barcode)
        {
            if (!barcode.IsValid()) return false;
            try
            {
                var response = await Connection.PrintBarcode(barcode);
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
