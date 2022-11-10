using System.Collections.Generic;
using System.Threading.Tasks;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public interface IPrinterConnection
    {

        DeviceInfo GetCurrentDevice();
        List<DeviceInfo> GetAvailableDevices();
        Task SendRawData(byte[] data);
        bool SetCurrentDevice(string printerName);
        void PrintText(string content);
        void PrintQR(string content);
        bool InitConnection();
        bool CloseConnection();
        // PrinterInfo getStatus();
        bool IsConnected();
        Task<bool> PrintBarcode(Barcode barcode);
        //Task<bool> PrintQRCode(QRCode qrcode);
        //Task<bool> PrintDoubleQRCode(QRCode qrcode);
        //Task<bool> PrintText(Text text);
        void SetAlignment(AlignmentEnum alignment);
        //bool PrintBoldText(Text text);
        bool CutPaper();
    }
}
