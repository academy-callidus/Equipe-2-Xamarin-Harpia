using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Entities;
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

        public bool InitConnection()
        {
            return true;
        }
        public bool IsConnected()
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

        /*public bool PrintText(Text text)
        {
            return true;
        }*/
        public void SetAlignment(AlignmentEnum alignment)
        {
            return true;
        }

        /*public bool PrintBoldText(Text text)
        {
            return true;
        }*/

        public bool CutPaper()
        {
            return true;
        }
        public bool Print3Line()
        {
            return true;
        }
    }

}
