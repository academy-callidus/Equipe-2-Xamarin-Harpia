using NLog;
using System;
using System.Threading.Tasks;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Exceptions;

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
                throw new BarcodeValidationException();
            }
            var response = Connection.PrintBarcode(barcode);
            return response;
        }
    }
}
