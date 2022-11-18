using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using xamarin_lib_harpia.Models.Entities;
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

        /// <summary>
        /// Return ESC/POS commands to print a barcode
        /// </summary>
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
            stream.AddRange(dimensions); // Setting barcode dimensions (width, height, alignment)
            stream.AddRange(model); // Setting the barcode model
            stream.AddRange(barcodeByteArray); // Setting the barcode content
            if (barcode.CutPaper) stream.AddRange(CutPaper());
            return stream.ToArray();
        }
        /// <summary>
        /// Recives a QRcode object and translates it to bytecode
        /// </summary>
        public static byte[] GetQrcodeBytes(QRcode qrcode)
        {
            byte[] modulesize = new byte[] { GS, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x43, (byte)qrcode.ImpSize};

            byte[] errorlevel = new byte[] { GS, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x45, (byte)(48 + (int)qrcode.Correction) };

            var stream_code = new List<byte>();
            byte[] d = TextToByte(qrcode.Content);
            int len = d.Length + 3;
            byte[] c = new byte[] { 0x1D, 0x28, 0x6B, (byte)len, (byte)(len >> 8), 0x31, 0x50, 0x30 };
            stream_code.AddRange(c);
            for (int i = 0; i < d.Length && i < len; i++)
            {
                stream_code.Add(d[i]);
            }
            var code = stream_code.ToArray();


            if (qrcode.ImpQuant == 0)
            {
                var stream = new List<byte>();
                stream.AddRange(modulesize);
                stream.AddRange(errorlevel);
                stream.AddRange(code);
                stream.AddRange(getBytesForPrintQRCode(true));
                if (qrcode.CutPaper) stream.AddRange(CutPaper());
                return stream.ToArray();
            }
            else
            {
                byte[] double_qr = new byte[] { 0x1B, 0x5C, 0x18, 0x00 };
                var stream = new List<byte>();
                stream.AddRange(modulesize);
                stream.AddRange(errorlevel);
                stream.AddRange(code);
                stream.AddRange(getBytesForPrintQRCode(false));
                stream.AddRange(code);
                stream.AddRange(double_qr);
                stream.AddRange(getBytesForPrintQRCode(true));
                if (qrcode.CutPaper) stream.AddRange(CutPaper());
                return stream.ToArray();
            }

        }
        /// <summary>
        /// Return bytes to the GetQrcodeBytes method depending if the QRcode is single or double
        /// </summary>
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

        /// <summary>
        /// Send bytecode command to printer depending on the text object attributes received
        /// </summary>
        public static byte[] GetTextBytes(Text text)
        {
            var stream = new List<byte>();

            if (text.IsBold) stream.AddRange(BoldOn());
            else stream.AddRange(BoldOff());

            if (text.IsUnderline) stream.AddRange(UnderlineWithOneDotWidthOn());
            else stream.AddRange(UnderlineOff());

            stream.AddRange(new byte[] { 0x1C,0x43,0xFF });
            stream.AddRange(SetFontSize(text.TextSize));
            stream.AddRange(TextToByteEncoding(text.Content, "utf-8"));
            stream.AddRange(NextLine(3));

            return stream.ToArray();
        } 

        /// <summary>
        /// bytecode command to set bold text on
        /// </summary>
        public static byte[] BoldOn()
        {
            byte[] result = new byte[] { ESC, 69, 0xf };
            return result;
        }

        /// <summary>
        /// bytecode command to set bold text off
        /// </summary>
        public static byte[] BoldOff()
        {
            byte[] result = new byte[] { ESC, 69, 0 };
            return result;
        }

        /// <summary>
        /// bytecode command to set underline text on
        /// </summary>
        public static byte[] UnderlineWithOneDotWidthOn()
        {
            byte[] result = new byte[] { ESC, 45, 1 };
            return result;
        }

        /// <summary>
        /// bytecode command to set underline text off
        /// </summary>
        public static byte[] UnderlineOff()
        {
            byte[] result = new byte[] { ESC, 45, 0 };
            return result;
        }

        /// <summary>
        /// Return ESC/POS commands to break lines
        /// </summary>
        public static byte[] NextLine(int lineNum)
        {
            byte[] result = new byte[lineNum];
            for (int i = 0; i < lineNum; i++)
            {
                result[i] = LF;
            }
            return result;
        }

        /// <summary>
        /// bytecode commant to set text size 
        /// </summary>
        public static byte[] SetFontSize(int fontSize)
        {
            byte[] result = new byte[] { 0x1D, 0x21, (byte)(fontSize - 12) };
            return result;
        }

        /// <summary>
        /// Return ESC/POS commands to set left alignment
        /// </summary>
        public static byte[] AlignLeft()
        {
            return new byte[] { ESC, 97, 0 };
        }

        /// <summary>
        /// Return ESC/POS commands to set center alignment
        /// </summary>
        public static byte[] AlignCenter()
        {
            return new byte[] { ESC, 97, 1 };
        }

        /// <summary>
        /// Return ESC/POS commands to set right alignment
        /// </summary>
        public static byte[] AlignRight()
        {
            return new byte[] { ESC, 97, 2 };
        }

        /// <summary>
        /// Return ESC/POS commands to cut paper
        /// </summary>
        public static byte[] CutPaper()
        {
            return new byte[] { 0x1d, 0x56, 0x01 };
        }

        /// <summary>
        /// Return ESC/POS commands to transform text to byte (ASCII)
        /// </summary>
        public static byte[] TextToByte(string content)
        {
            return System.Text.Encoding.ASCII.GetBytes(content);
        }

        /// <summary>
        /// Return ESC/POS commands to transform text to byte (Others)
        /// </summary>
        public static byte[] TextToByteEncoding(string content, string encoding)
        {
            if (encoding.Equals("utf-8")) return System.Text.Encoding.UTF8.GetBytes(content);
            return System.Text.Encoding.GetEncoding(encoding).GetBytes(content);
        }
    }
}
