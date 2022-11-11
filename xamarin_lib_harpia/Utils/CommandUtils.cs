using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using xamarin_lib_harpia.Models.Entities;
using System.Collections.Generic;
using System.Text;
using ZXing.QrCode.Internal;
using Xamarin.Forms.Xaml;

namespace xamarin_lib_harpia.Utils
{
    public class CommandUtils
    {
        public static byte ESC = 0x1B;// Escape
        public static byte FS = 0x1C;// Text delimiter
        public static byte GS = 0x1D;// Group separator
        public static byte DLE = 0x10;// data link escape
        public static byte EOT = 0x04;// End of transmission
        public static byte ENQ = 0x05;// Enquiry character
        public static byte SP = 0x20;// Spaces
        public static byte HT = 0x09;// Horizontal list
        public static byte LF = 0x0A;//Print and wrap (horizontal orientation)
        public static byte CR = 0x0D;// Home key
        public static byte FF = 0x0C;// Carriage control (print and return to the standard mode (in page mode))
        public static byte CAN = 0x18;// Canceled (cancel print data in page mode)

        private static byte[] GetBytesFromDecString(string decstring)
        {
            if (decstring == null || decstring.Equals("")) return null;
            decstring = decstring.Replace(" ", "");
            int size = decstring.Length / 2;
            char[] decarray = decstring.ToCharArray();
            byte[] rv = new byte[size];
            for (int i = 0; i < size; i++)
            {
                int pos = i * 2;
                rv[i] = (byte)(Convert.ToByte(decarray[pos]) * 10 + Convert.ToByte(decarray[pos + 1]));
            }
            return rv;
        }

        public static byte[] GetBarcodeBytes(Barcode barcode)
        {
            int position = barcode.HRIPosition == "Acima do QRCode" ? 1 :
                barcode.HRIPosition == "Abaixo do QRCode" ? 2 :
                barcode.HRIPosition == "Acima e abaixo do QRCode" ? 3 :
                0;
            byte[] dimensions = new byte[]{0x1D,0x66,0x01,0x1D,0x48,
                    (byte)position,
                    0x1D,0x77,
                    (byte)barcode.Width,
                    0x1D,0x68,
                    (byte)barcode.Height,
                    0x0A
            };
            byte[] barcodeByteArray;

            if (barcode.Model.ID == 10)
                barcodeByteArray = GetBytesFromDecString(barcode.Content);
            else
                barcodeByteArray = TextToByte(barcode.Content);

            byte[] model;
            if (barcode.Model.ID > 7)
                model = new byte[] { 0x1D, 0x6B, 0x49, (byte)(barcodeByteArray.Length + 2), 0x7B, (byte)(0x41 + barcode.Model.ID - 8) };
            else
                model = new byte[] { 0x1D, 0x6B, (byte)(barcode.Model.ID + 0x41), (byte)barcodeByteArray.Length };

            var stream = new List<byte>();
            stream.AddRange(dimensions); // Setting barcode dimensions (width, height, alingment)
            stream.AddRange(model); // Setting the barcode model
            stream.AddRange(barcodeByteArray); // Setting the barcode content
            if (barcode.CutPaper) stream.AddRange(CutPaper());
            return stream.ToArray();
        }
        public static byte[] GetQrcodeBytes(QRcode qrcode)
        {
            //modulesize
            byte[] modulesize = new byte[] { GS, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x43, (byte)qrcode.ImpSize};

            //errorlevel
            byte[] errorlevel = new byte[] { GS, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x45, (byte)(48 + (int)qrcode.Correction) };

            // code 
            byte[] d = TextToByte(qrcode.Content);
            int len = d.Length + 3;
            byte[] code = new byte[] { 0x1D, 0x28, 0x6B, (byte)len, (byte)(len >> 8), 0x31, 0x50, 0x30 };
            for (int i = 0; i < d.Length && i < len; i++)
            {
                code.addRange
            }

            if (qrcode.ImpQuant == 1)
            {
                MemoryStream stream = new MemoryStream();
                stream.Write(TextToByte("QrCode\n"), 0, TextToByte("QrCode\n").Length);
                stream.Write(TextToByte("--------------------------------\n"), 0, TextToByte("--------------------------------\n").Length);
                stream.Write(modulesize, 0, modulesize.Length);
                stream.Write(errorlevel, 0, errorlevel.Length);
                stream.Write(code, 0, code.Length);
                stream.Write(getBytesForPrintQRCode(true), 0, getBytesForPrintQRCode(true).Length);
                if (qrcode.CutPaper)
                {
                    stream.Write(CutPaper(), 0, CutPaper().Length);
                }
                return stream.ToArray();
            }
            else
            {
                byte[] double_qr = new byte[] { 0x1B, 0x5C, 0x18, 0x00 };
                MemoryStream stream = new MemoryStream();
                stream.Write(TextToByte("QrCode\n"), 0, TextToByte("QrCode\n").Length);
                stream.Write(TextToByte("--------------------------------\n"), 0, TextToByte("--------------------------------\n").Length);
                stream.Write(modulesize, 0, modulesize.Length);
                stream.Write(errorlevel, 0, errorlevel.Length);
                stream.Write(code, 0, code.Length);
                stream.Write(getBytesForPrintQRCode(false), 0, getBytesForPrintQRCode(false).Length);
                stream.Write(code, 0, code.Length);
                stream.Write(double_qr, 0, double_qr.Length);
                stream.Write(getBytesForPrintQRCode(true), 0, getBytesForPrintQRCode(true).Length);
                if (qrcode.CutPaper)
                {
                    stream.Write(CutPaper(), 0, CutPaper().Length);
                }
                return stream.ToArray();
            }

        }

        public static byte[] getBytesForPrintQRCode(bool single)
        {
            byte[] bytesforprint;
            if (single)
            {
                bytesforprint = new byte[9];
                bytesforprint[8] = 0x0A;
            }
            else
            {
                bytesforprint = new byte[8];
            }
            bytesforprint = new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x51, 0x30 };

            return bytesforprint;
        }

        public static byte[] AlignLeft()
        {
            return new byte[] { ESC, 97, 0 };
        }

        public static byte[] AlignCenter()
        {
            return new byte[] { ESC, 97, 1 };
        }

        public static byte[] AlignRight()
        {
            return new byte[] { ESC, 97, 2 };
        }

        public static byte[] CutPaper()
        {
            return new byte[] { 0x1d, 0x56, 0x01 };
        }

        public static byte[] TextToByte(string content)
        {
            return System.Text.Encoding.ASCII.GetBytes(content);
        }
    }
}
