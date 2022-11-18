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
            if (!qrcode.IsValid()) return false;
            try
            {
                var response = Connection.PrintQRCode(qrcode);
                return response;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }

}
