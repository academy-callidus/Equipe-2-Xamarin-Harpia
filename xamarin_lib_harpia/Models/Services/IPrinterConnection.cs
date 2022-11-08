using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public interface IPrinterConnection
    {
        bool InitConnection();
        // PrinterInfo getStatus();
        bool IsConnected();
        bool PrintBarcode(Barcode barcode);
        //bool PrintQRCode(QRCode qrcode);
        //bool PrintDoubleQRCode(QRCode qrcode);
        //bool PrintText(Text text);
        void SetAlignment(AlignmentEnum alignment);
        //bool PrintBoldText(Text text);
        bool CutPaper();
        bool Print3Line();
    }
}
