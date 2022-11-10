using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class Text
    {
        private string Content 
        { 
            get { return Content; } 
            set { Content = value; } 
        }
        private int Record 
        { 
            get { return Record } 
            set { Record = value; } 
        }
        private bool IsBold 
        { 
            get { return IsBold; }
            set { IsBold = value; } 
        }
        private bool IsUnderline 
        { 
            get { return IsUnderline; } 
            set { IsUnderline = value; } 
        }
        private bool IsK1 
        { 
            get { return IsK1; } 
            set { IsK1 = value; }
        }
        private float Width 
        {
            get { return Width; }
            set { Width = value; }
        }
        private float Height 
        { 
            get { return Height; } 
            set { Height = value; } 
        }
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
