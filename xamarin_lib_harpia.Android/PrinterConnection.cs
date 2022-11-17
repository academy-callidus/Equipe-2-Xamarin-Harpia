using System;
using xamarin_lib_harpia.Models.Services;
using BluetoothPrinter.Droid;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Utils;
using Android.Content;
using Android.OS;
using Woyou.Aidlservice.Jiuiv5;

[assembly: Xamarin.Forms.Dependency(typeof(PrinterConnection))]
namespace BluetoothPrinter.Droid
{
    public class PrinterConnection : IPrinterConnection
    {
        private SunmiPrinterService SunmiPrinterService { get; set; }

        public PrinterConnection()
        {
            SunmiPrinterService = new SunmiPrinterService();
            InitConnection();
        }

        public void SendRawData(byte[] data)
        {
            try
            {
                SunmiPrinterService.Service.SendRAWData(data, null);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

      
        public bool InitConnection()
        {
            try
            {
                var intent = new Intent();
                intent.SetPackage("woyou.aidlservice.jiuiv5");
                intent.SetAction("woyou.aidlservice.jiuiv5.IWoyouService");
                Android.App.Application.Context.StartService(intent);
                Android.App.Application.Context.BindService(intent, SunmiPrinterService, Bind.AutoCreate);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool CloseConnection()
        {
            if (!IsConnected()) return true;
            SunmiPrinterService.Service = null;
            return true;
        }

        public bool IsConnected()
        {
            return SunmiPrinterService.Service != null;
        }

        public bool PrintBarcode(Barcode barcode)
        {
            if (!IsConnected()) return false;

            try
            {
                int position = barcode.HRIPosition == "Acima do QRCode" ? 1 :
                barcode.HRIPosition == "Abaixo do QRCode" ? 2 :
                barcode.HRIPosition == "Acima e abaixo do QRCode" ? 3 :
                0;
                var modelId = barcode.Model.ID > 7 ? 8 : barcode.Model.ID;

                SunmiPrinterService.Service.SetAlignment((int)AlignmentEnum.CENTER, null);
                SunmiPrinterService.Service.PrintText("Barcode\n", null);
                SunmiPrinterService.Service.PrintText("--------------------------------\n", null);
                SendRawData(CommandUtils.GetBarcodeBytes(barcode));
                LineWrap();
                return true;
            }catch(Exception _)
            {
                return false;
            }
        }

      
        public bool PrintQRCode(QRcode qrcode)
        {
            if (!IsConnected()) return false;
            try
            {
                
                SunmiPrinterService.Service.SetAlignment((int) qrcode.Alignment, null);
                SunmiPrinterService.Service.PrintText("QR Code\n", null);
                SunmiPrinterService.Service.PrintText("--------------------------------\n", null);
                SendRawData(CommandUtils.GetQrcodeBytes(qrcode));
                LineWrap();
                return true;
            }
            catch (Exception _)
            {
                return false;
            }
        }

        public bool PrintText(Text text)
        {
            if (!IsConnected()) return false;
            try
            {
                SendRawData(text.IsBold ? CommandUtils.BoldOn() : CommandUtils.BoldOff());
                SendRawData(text.IsUnderline ? CommandUtils.UnderlineWithOneDotWidthOn() : CommandUtils.UnderlineOff());
                SunmiPrinterService.Service.SetFontSize(text.TextSize, null);
                SunmiPrinterService.Service.PrintText(text.Content, null);
                SendRawData(CommandUtils.UnderlineOff());
                SendRawData(CommandUtils.BoldOff());
                LineWrap();
                return true;
            }
            catch (Exception _)
            {
                return false;
            }
        }

        private void LineWrap(int lines = 3)
        {
            SunmiPrinterService.Service.LineWrap(lines, null);
        }

    }

    public class SunmiPrinterService : Java.Lang.Object, IServiceConnection
    {
        public IWoyouService Service { get; set; }
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Service = IWoyouServiceStub.AsInterface(service);
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            Service = null;
        }
    }
}