using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Services.Tests;

namespace xamarin_lib_harpia.Models.Entities
{
    internal class FullTest
    {
        readonly BarcodeTestService TestBarcode = new BarcodeTestService(0, 0, 0, 0);
        readonly QRCodeTestService TestQRCode = new QRCodeTestService(0, 0);
        readonly TextTestService TestText = new TextTestService();

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
