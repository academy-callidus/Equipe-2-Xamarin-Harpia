using System.Collections.Generic;
using System.Threading.Tasks;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public interface IPrinterConnection
    {

        DeviceInfo GetCurrentDevice();
        List<DeviceInfo> GetAvailableDevices();
        void SendRawData(byte[] data);
        bool SetCurrentDevice(string printerName);
        void PrintText(string content);
        void PrintQR(string content);
        bool InitConnection();
        bool CloseConnection();
        // PrinterInfo getStatus();
        bool IsConnected();
        bool PrintBarcode(Barcode barcode);
        bool PrintQRCode(QRcode qrcode);
        bool PrintDoubleQRCode(QRcode qrcode);

        //bool PrintText(Text text);
        void SetAlignment(AlignmentEnum alignment);
        //bool PrintBoldText(Text text);
        bool CutPaper();
    }
}
