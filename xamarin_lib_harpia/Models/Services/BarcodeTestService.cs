using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Utils;

namespace xamarin_lib_harpia.Models.Services
{
    internal class BarcodeTestService
    {

        readonly TectoySunmiPrint printerInstance = TectoySunmiPrint.getInstance();
        private int height {
            set;
            get;
        }
        private int width { set; get; }
        private int advanceLines { set; get; }
        private int barcodeModel { set; get; } // adicionar BarcodeModelEnum

        public BarcodeTestService(int width, int height, int advanceLines, int barcodeModel)
        {
            this.width = width;
            this.height = height;
            this.advanceLines =advanceLines;
            this.barcodeModel =barcodeModel;
        }

       
        public void PrintTextBelow(string text)
        {
            
        }

        public void PrintTextAbove(string text)
        {

        }

        public void PrintTextBelowAndAbove(string text)
        {

        }



    }
}
