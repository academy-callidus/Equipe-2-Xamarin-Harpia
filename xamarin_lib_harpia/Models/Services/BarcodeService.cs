using NLog;
using System;
using System.Threading.Tasks;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public class BarcodeService
    {
        private IPrinterConnection Connection;
        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public BarcodeService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Try printing a barcode if it has a valid format
        /// </summary>
        public bool Execute(Barcode barcode)
        {
            if (!barcode.IsValid())
            {
                Logger.Info("Barcode inválido!");
                return false;
            }
            try
            {
                var response = Connection.PrintBarcode(barcode);
                return response;
            }
            catch (Exception exception)
            {
                Logger.Warn($"Barcode - {exception.Message}");
                return false;
            }
        }
    }
}
