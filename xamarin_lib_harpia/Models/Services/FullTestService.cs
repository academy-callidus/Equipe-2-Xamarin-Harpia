using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Services
{
    internal class FullTestService
    {
        BarcodeTestService TestBarcode = new BarcodeTestService(0, 0, 0, 0);
        QRCodeTestService TestQRCode = new QRCodeTestService(0, 0);
        TextTestService TestText = new TextTestService();

        public void TestAll()
        {
            TestBarcode.PrintTextAbove("00000000");
            TestBarcode.PrintTextBelow("00000000");
            TestBarcode.PrintTextBelowAndAbove("00000000");

            TestQRCode.PrintQRCode(0, "www.tectoy.com.br", "harpia");

            TestText.AlignText();
            TestText.TextPrintShapes();


        }
    }
}
