using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Utils;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services.Tests
{
    internal class BarcodeTestService
    {

        readonly TectoySunmiPrint printerInstance = TectoySunmiPrint.getInstance();

        private int Height
        {
            get;
            set;
        }
        private int Width { get; set; }
        private int AdvanceLines { get; set; }
        private BarcodeModelEnum BarcodeModel { get; set; } 

        public BarcodeTestService(int width, int height, int advanceLines, BarcodeModelEnum barcodeModel)
        {
            Width = width;
            Height = height;
            AdvanceLines = advanceLines;
            BarcodeModel = barcodeModel;
        }


        public void PrintTextBelow(string Text)
        {

        }

        public void PrintTextAbove(string Text)
        {

        }

        public void PrintTextBelowAndAbove(string Text)
        {

        }



    }
}
