using System.Collections.Generic;
using System.Threading.Tasks;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public interface IPrinterConnection
    {

        void SendRawData(byte[] data);
        bool InitConnection();
        bool CloseConnection();
        // PrinterInfo getStatus();
        bool IsConnected();
        bool PrintBarcode(Barcode barcode);
        bool PrintQRCode(QRcode qrcode);
        bool PrintText(Text text);
    }
}
