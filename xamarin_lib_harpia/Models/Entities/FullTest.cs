using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using xamarin_lib_harpia.Models.BarcodeModels;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Models.Entities
{
    internal class FullTest
    {
        private readonly string[] BarcodeHRIs = { "Acima do QRCode", "Abaixo do QRCode", "Acima e abaixo do QRCode" };
        private IPrinterConnection Connection = DependencyService.Get<IPrinterConnection>();

        public readonly BarcodeModel[] BarcodeModels = 
        {
                new UPCA(),
                new UPCE(),
                new EAN13(),
                new EAN8(),
                new CODE39(),
                new ITF(),
                new CODABAR(),
                new CODE128A(),
                new CODE128B(),
                new CODE128C()
        };

        public void TestBarCode()
        {
            
            BarcodeService service = new BarcodeService(Connection);
          
            // add printText "Bar Code HRI Positions"
            service.Execute(new Barcode("7894900700046", BarcodeHRIs[0], BarcodeModels[0], 2, 162, false));
            service.Execute(new Barcode("7894900700046", BarcodeHRIs[1], BarcodeModels[0], 2, 162, false));
            service.Execute(new Barcode("7894900700046", BarcodeHRIs[2], BarcodeModels[0], 2, 162, false));
            
            // add printText "Bar Code UPCE format
            service.Execute(new Barcode("0123456", BarcodeHRIs[2], BarcodeModels[1], 2, 162, false));

            // add printText "EAN13"
            service.Execute(new Barcode("978020137962", BarcodeHRIs[2], BarcodeModels[2], 2, 162, false));
            // add printText "EAN8"
            service.Execute(new Barcode("9031101", BarcodeHRIs[2], BarcodeModels[3], 2, 162, false));
            // add printText "CODE39"
            service.Execute(new Barcode("ABC-1234", BarcodeHRIs[2], BarcodeModels[4], 2, 162, false));
            // add printText "ITF"
            service.Execute(new Barcode("123457", BarcodeHRIs[2], BarcodeModels[5], 2, 162, false));
            // add printText "CODABAR"
            service.Execute(new Barcode("a1bcd2/", BarcodeHRIs[2], BarcodeModels[6], 2, 162, false));
            // add printText "CODE128A"
            service.Execute(new Barcode("92781330", BarcodeHRIs[2], BarcodeModels[7], 2, 162, false));
            // add printText "CODE128B"
            service.Execute(new Barcode("harpia/2022", BarcodeHRIs[2], BarcodeModels[8], 2, 162, false));
            // add printText "CODE128C"
            service.Execute(new Barcode("PJJ123C", BarcodeHRIs[2], BarcodeModels[9], 2, 162, false));
        }
        
        public void TestAll()
        {

            TestBarCode();
        }
    }
}
