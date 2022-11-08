using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using xamarin_lib_harpia.Models.Services.BarcodeModels;

namespace xamarin_lib_harpia.Models.Entities
{
    internal class Barcode
    {
        private string Content { get; set; }
        private string HRIPosition { get; set; }
        private BarcodeModel Model { get; set; }
        private float Width { get; set; }
        private float Height { get; set; }
        private bool CutPaper { get; set; }

        public Barcode(string content, string hRIPosition, BarcodeModel model, float width, float height, bool cutPaper)
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
