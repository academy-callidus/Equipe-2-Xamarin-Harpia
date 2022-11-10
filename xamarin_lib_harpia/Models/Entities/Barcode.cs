using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class Barcode
    {
        public string Content { get; set; }
        public string HRIPosition { get; set; }
        private BarcodeModelEnum Model { get; set; }
        private float Width { get; set; }
        private float Height { get; set; }
        private bool CutPaper { get; set; }

        public Barcode(string content, string hRIPosition, BarcodeModelEnum model, float width, float height, bool cutPaper)
        {
            Content = content;
            HRIPosition = hRIPosition;
            Model = model;
            Width = width;
            Height = height;
            CutPaper = cutPaper;
        }
    }
}
