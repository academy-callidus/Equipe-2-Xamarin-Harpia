using System;
using System.IO;
using xamarin_lib_harpia.Models.Entities;

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

        private static byte[] GetBytesFromDecString(String decstring)
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
                model = new byte[] { 0x1D, 0x6B, 0x49, (byte)(barcodeByteArray.Length + 2), 0x7B, (byte)(0x41 + barcode.Model.ID - 8)};
            else
                model = new byte[] { 0x1D, 0x6B, (byte)(barcode.Model.ID + 0x41), (byte)barcodeByteArray.Length };

            MemoryStream stream = new MemoryStream();
            stream.Write(dimensions, 0, dimensions.Length); // Setting barcode dimensions (width, height, alingment)
            stream.Write(model, 0, model.Length); // Setting the barcode model
            stream.Write(barcodeByteArray, 0, barcodeByteArray.Length); // Setting the barcode content
            return stream.ToArray();
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
