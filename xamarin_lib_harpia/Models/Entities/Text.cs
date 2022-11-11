using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class Text
    {
        private int[] encodes = { 437, 850, 860, 863, 865, 857, 737, 1252, 866, 852, 858, 874, 855, 862, 864, 54936, 950, 949, 65001 };
        public string Content { get; set; }
        public bool IsBold { get; set; }
        public bool IsUnderline { get; set; }
        public string CharsetOption { get; set; }
        public int TextSize { get; set; }
        public int Record { get; set; }
        public int Encoding { get; set; }

        public Text(string content, bool isBold, bool isUnderline, string charsetOption, int textSize, int record)
        {
            Content = content;
            IsBold = isBold;
            IsUnderline = isUnderline;
            CharsetOption = charsetOption;
            TextSize = textSize;
            Record = record;
            Encoding = encodes[record];
        }
    }
}
