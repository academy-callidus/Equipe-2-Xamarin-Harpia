using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class Text
    {
        private string Content { get; set; }
        private int Record { get; set; }
        private bool IsBold { get; set; }
        private bool IsUnderline { get; set; }
        private bool IsK1 { get; set; }
        private float Width { get; set; }
        private float Height { get; set; }
        private string[] MStrings { get; set; }

        public Text(string content, int record, bool isBold, bool isUnderline, bool isK1, float width, float height, string[] mStrings)
        {
            Content = content;
            Record = record;
            IsBold = isBold;
            IsUnderline = isUnderline;
            IsK1 = isK1;
            Width = width;
            Height = height;
            MStrings = mStrings;
        }
    }
}
