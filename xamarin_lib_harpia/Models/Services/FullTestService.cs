using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using xamarin_lib_harpia.Models.BarcodeModels;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    internal class FullTestService
    {
        private readonly IPrinterConnection Connection = DependencyService.Get<IPrinterConnection>();

        private readonly TextService text;
        private readonly QRCodeService qrcode;
        private readonly BarcodeService barcode;

        public FullTestService()
        {
            this.text = new TextService(Connection);
            this.qrcode = new QRCodeService(Connection);
            this.barcode = new BarcodeService(Connection);
        }

        private readonly AlignmentEnum[] Alignments =
        {
            AlignmentEnum.LEFT,
            AlignmentEnum.RIGHT,
            AlignmentEnum.CENTER
        };

        private readonly string[] BarcodeHRIs =
        { 
            "Acima do QRCode", 
            "Abaixo do QRCode", 
            "Acima e abaixo do QRCode" 
        };

        private readonly BarcodeModel[] BarcodeModels =
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

        private readonly string[] QrcodeQtdList = { "QrCode", "Dois QrCode" };
        private readonly int[] QrcodeSizeList = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
        private readonly QrCodeCorrectionEnum[] QrcodeLevelList = 
        { 
            QrCodeCorrectionEnum.CORRECTION_L,  
            QrCodeCorrectionEnum.CORRECTION_M,
            QrCodeCorrectionEnum.CORRECTION_Q,
            QrCodeCorrectionEnum.CORRECTION_H
        };

        private string[] encodeStrings = {
            "CP437", "CP850", "CP860", "CP863", "CP865", 
            "CP857", "CP737", "Windows-1252", "CP866", 
            "CP852", "CP858", "CP874", "CP855", "CP862", 
            "CP864", "GB18030", "BIG5", "KSC5601", "utf-8", 
            "utf-16", "utf-32", "unicodeFFFE" };


        private void PrintTextTest()
        {
            int record = 21;
            string charSetOption = encodeStrings[21];
            int FontSize = 12;
                        
            // Test bold Text
            text.Execute(new Text("Testa bold", true, false, charSetOption, FontSize, record));
            // Test underline Text
            text.Execute(new Text("Testa underline", false, true, charSetOption, FontSize, record));
            // Test Bold e Underline
            text.Execute(new Text("Testa bold e underline", true, true, charSetOption, FontSize, record));
            // Test Text Size
            text.Execute(new Text("Testa tamanho do Texto igual a 16", false, false, charSetOption, 16, record));
            text.Execute(new Text("Testa tamanho do Texto igual a 24", false, false, charSetOption, 24, record));
            

        }

        private void PrintQRcodeTest()
        {
            string content = "www.tectoySunmi.com.br";

            // Test Aligment 
            for (int i = 0; i < Alignments.Length; i++)
            {
                qrcode.Execute(new QRcode(
                    content,
                    0, //index do elemento
                    QrcodeSizeList[1],
                    QrcodeLevelList[0],
                    Alignments[i],
                    false
                    ));
            }
            // Test Size 3 QRCode
            qrcode.Execute(new QRcode(
                    content,
                    0, //index do elemento
                    QrcodeSizeList[2],
                    QrcodeLevelList[0],
                    Alignments[0],
                    false
                    ));
            // Test Size 8 QRCode
            qrcode.Execute(new QRcode(
                    content,
                    0, //index do elemento
                    QrcodeSizeList[7],
                    QrcodeLevelList[0],
                    Alignments[0],
                    false
                    ));

            // Test QRCode Correction L
            qrcode.Execute(new QRcode(
                    content,
                    0, //index do elemento
                    QrcodeSizeList[1],
                    QrcodeLevelList[0],
                    Alignments[0],
                    false
                    ));
            // Test QRCode Corretion H
            qrcode.Execute(new QRcode(
                    content,
                    0, //index do elemento
                    QrcodeSizeList[1],
                    QrcodeLevelList[3],
                    Alignments[0],
                    false
                    ));

            // Test Dois QRCode
            qrcode.Execute(new QRcode(content, 1, QrcodeSizeList[1], QrcodeLevelList[0], Alignments[0], false));


        }

        private void PrintBarcodeTest()
        {
            // Test HRI Positions
            for (int i = 0; i < BarcodeHRIs.Length; i++)
                barcode.Execute(new Barcode("7894900700046", BarcodeHRIs[i], BarcodeModels[0], 2, 162, false));

            string[] testContent = {
                "0123456",
                "978020137962",
                "9031101",
                "ABC-1234", 
                "123457",
                "a1bcd2/",
                "92781330",
                "harpia/2022",
                "PJJ123C"
            };
            // Test Barcode Models
            for (int i = 1; i < BarcodeModels.Length; i++)
                barcode.Execute(new Barcode(testContent[i-1], BarcodeHRIs[2], BarcodeModels[i], 2, 162, false));   
        }

        public void RunAllTests()
        {
            PrintTextTest();
            PrintQRcodeTest();
            PrintBarcodeTest();
        }
    }
}
