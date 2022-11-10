using System.Collections.Generic;
using xamarin_lib_harpia.Models.BarcodeModels;

namespace xamarin_lib_harpia.Models.Entities
{
    public class Barcode
    {
        public string Content { get; set; }
        public string HRIPosition { get; set; }
        public BarcodeModel Model { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool CutPaper { get; set; }

        public Barcode(string content, string hRIPosition, BarcodeModel model, int width, int height, bool cutPaper)
        {
            Content = content;
            HRIPosition = hRIPosition;
            Model = model;
            Width = width;
            Height = height;
            CutPaper = cutPaper;
        }

        public bool IsValid()
        {
            if(Model == null || Content == null) return false;
            return Model.IsValid(Content);
        }

        public override string ToString()
        {
            return $"Barcode[Content={Content}, HRIPosition={HRIPosition}, Model={Model.Name}, Width={Width}, Height={Height}, CutPaper={CutPaper}]";
        }
    }
}
