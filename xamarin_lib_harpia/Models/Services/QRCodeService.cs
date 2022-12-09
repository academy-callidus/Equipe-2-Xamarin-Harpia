using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public class QRCodeService
    {
        private IPrinterConnection Connection;
        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public QRCodeService(IPrinterConnection connection)
        {
            Connection = connection;
        }
        /// <summary>
        /// Try to start a connection with the printer, check whether it was successful or not and 
        /// return the bytecode reponse from the PrintQRcode method
        /// </summary>
        public bool Execute(QRcode qrcode)
        {
            if (!qrcode.IsValid()) {
                Logger.Info("QRCode inválido!");
                return false;
                };
            try
            {
                var response = Connection.PrintQRCode(qrcode);
                return response;
            }
            catch(Exception exception)
            {
                Logger.Warn($"QRCode - {exception.Message}");
                return false;
            }
        }
    }

}
