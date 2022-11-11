using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class Text
    {
        public string Content { get; set; }
        public bool IsBold { get; set; }
        public bool IsUnderline { get; set; }
        public string CharsetOption { get; set; }
        public int TextSize { get; set; }
        public int Record { get; set; }

        public Text(string content, bool isBold, bool isUnderline, string charsetOption, int textSize, int record)
        {
            Content = content;
            IsBold = isBold;
            IsUnderline = isUnderline;
            CharsetOption = charsetOption;
            TextSize = textSize;
            Record = record;
        }
    }
}
