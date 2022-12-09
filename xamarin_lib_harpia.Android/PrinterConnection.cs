using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Content;
using BluetoothPrinter.Droid;
using Woyou.Aidlservice.Jiuiv5;
using xamarin_lib_harpia.Models.Services;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Exceptions;
using xamarin_lib_harpia.Utils;
using System.Reflection;
using Android.Graphics.Drawables;
using System.IO;
using System.Runtime.Remoting.Contexts;
using ZXing.QrCode.Internal;
using Android.Graphics;
using ZXing.OneD;

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
            catch (Exception)
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
                SunmiPrinterService.Service.SetFontSize(16, null);
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
            }catch(Exception)
            {
                return false;
            }
        }

      
        public bool PrintQRCode(QRcode qrcode)
        {
            if (!IsConnected()) return false;
            try
            {
                SunmiPrinterService.Service.SetFontSize(16, null);

                SunmiPrinterService.Service.SetAlignment((int) qrcode.Alignment, null);
                SunmiPrinterService.Service.PrintText("QR Code\n", null);
                SunmiPrinterService.Service.PrintText("--------------------------------\n", null);
                SendRawData(CommandUtils.GetQrcodeBytes(qrcode));
                LineWrap();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PrintText(Text text)
        {
            if (!IsConnected()) throw new PrinterConnectionException();
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
            catch (Exception)
            {
                throw new PrintTextException();
            }
        }

        public bool PrintImage(Image image)
        {
            if (!IsConnected()) return false;
            try
            {
                var context = Application.Context;

                SunmiPrinterService.Service.SetAlignment((int)image.Alignment, null);
                SunmiPrinterService.Service.PrintText("Imagem\n", null);
                SunmiPrinterService.Service.PrintText("--------------------------------\n", null);

                
               using (var drawable = Xamarin.Forms.Platform.Android.ResourceManager.GetDrawable(context, image.Resource))
                using (var bitmap = ((BitmapDrawable)drawable).Bitmap)
                {
                    SunmiPrinterService.Service.PrintBitmap(ScaleImage(bitmap), null);
                }
                if (image.CutPaper) SendRawData(CommandUtils.CutPaper());
                LineWrap();
                return true;
            }
            catch (Exception _)
            {
                return false;
            }
        }

        public bool PrintTable(Table table)
        {
            if (!IsConnected()) return false;
            try
            {
                SunmiPrinterService.Service.SetFontSize(24, null);
                SunmiPrinterService.Service.PrintColumnsText(table.ColumnsText, table.ColumnsWidth, table.GetAlignmentsAsInteger(), null);
                LineWrap(1);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public bool AdvancePaper()
        {
            if (!IsConnected()) return false;
            try
            {
                LineWrap();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void LineWrap(int lines = 3)
        {
            SunmiPrinterService.Service.LineWrap(lines, null);
        }

        public string GetPrinterSerialNo()
        {
            return IsConnected() ? SunmiPrinterService.Service.GetPrinterSerialNo() : string.Empty;
        }

        public string GetPrinterModel()
        {
            var model = SysProp.GetProp("ro.product.model");

            return model ?? string.Empty;
        }

        public string GetFirmwareVersion()
        {
            return IsConnected() ? SunmiPrinterService.Service.GetPrinterVersion() : string.Empty;
        }

        public string GetServiceVersion()
        {
            return IsConnected() ? SunmiPrinterService.Service.GetServiceVersion() : string.Empty;
        }

        public int GetPrinterPaper()
        {
            // TO-DO
            return 1;
        }

        public Task<string> GetPrintedLength()
        {
            var cb = new Callback();
            SunmiPrinterService.Service.GetPrintedLength(cb);


            return cb.Result.Task;
        }

        public string GetServiceVersionName()
        {
            var versionName = SysProp.GetProp("ro.version.sunmi_versionname");

            return versionName ?? string.Empty;
        }

        public string GetServiceVersionCode()
        {
            var packageInfo = Application.Context.ApplicationContext.PackageManager.GetPackageInfo("woyou.aidlservice.jiuiv5", 0);
            var versionCode = AndroidX.Core.Content.PM.PackageInfoCompat.GetLongVersionCode(packageInfo);

            return versionCode.ToString();
        }

        private Bitmap ScaleImage(Bitmap bitmap1)
        {
            int width =  (int)(bitmap1.Width * 0.5);
            int height = (int)(bitmap1.Height * 0.5);
            return Bitmap.CreateScaledBitmap(bitmap1, width, height, false);
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

    class Callback: ICallbackStub
    {
        public TaskCompletionSource<String> Result;

        public Callback()
        {
            Result = new TaskCompletionSource<string>();
        }

        public override void OnRunResult(bool isSuccess)
        {
            //throw new NotImplementedException();
        }

        public override void OnReturnString(string result)
        {
            Result.TrySetResult(result);
        }

        public override void OnRaiseException(int code, string msg)
        {
            //throw new NotImplementedException();
        }
    }

    static class SysProp
    {
        // Lazy load the SystemProperties class
        private static readonly Lazy<Java.Lang.Class> _class =
            new Lazy<Java.Lang.Class>(() =>
                Java.Lang.Class.ForName("android.os.SystemProperties")
            );

        // Get the set method when we need it
        private static readonly Lazy<Java.Lang.Reflect.Method> _SetMethod =
            new Lazy<Java.Lang.Reflect.Method>(() =>
                _class.Value.GetDeclaredMethod("set",
                    Java.Lang.Class.FromType(typeof(Java.Lang.String)),
                    Java.Lang.Class.FromType(typeof(Java.Lang.String)))
                );

        // Get the get method when we need it
        private static readonly Lazy<Java.Lang.Reflect.Method> _GetMethod =
            new Lazy<Java.Lang.Reflect.Method>(() =>
                _class.Value.GetDeclaredMethod("get",
                    Java.Lang.Class.FromType(typeof(Java.Lang.String)))
                );

        private static Java.Lang.Reflect.Method SetMethod
        {
            get { return _SetMethod.Value; }
        }
        private static Java.Lang.Reflect.Method GetMethod
        {
            get { return _GetMethod.Value; }
        }

        /// <summary>
        /// Calls the get method of the android.os.SystemProperties class
        /// </summary>
        /// <param name="PropertyName">The name of the system property to get the value for</param>
        /// <returns>The value of the specified property or null if it does not exists</returns>
        public static string GetProp(string PropertyName)
        {
            // Invoking a static method, first parameter is null
            var r = GetMethod.Invoke(null, new Java.Lang.String(PropertyName));

            return r.ToString();
        }

        /// <summary>
        /// Calls the set method of the android.os.SystemProperties class
        /// </summary>
        /// <param name="PropertyName">The name of the system property to get the value for</param>
        /// <param name="PropertyValue">The value to set for the system property</param>
        /// <returns>The previous value of the specified property or null if it does not exists</returns>
        public static string SetProp(string PropertyName, string PropertyValue)
        {
            // Invoking a static method, first parameter is null
            var r = SetMethod.Invoke(null,
                new Java.Lang.String(PropertyName),
                new Java.Lang.String(PropertyValue));

            return r.ToString();
        }

    }
}