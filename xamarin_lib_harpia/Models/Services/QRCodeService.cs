using System;
using System.Collections.Generic;
using System.Text;
using ZXing.QrCode.Internal;

namespace xamarin_lib_harpia.Models.Services
{
    public class QRCodeService
    {
        private IPrinterConnection Connection;

        public QRCodeService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        public bool execute(QRCode qrcode)
        {

            return true;
        }

        public bool PrintQRCode(QRCode qrcode)
        {

            return true;
        }
        public bool PrintDoubleQRCode(QRCode qrcode)
        {

            return true;
        }
    }
}
