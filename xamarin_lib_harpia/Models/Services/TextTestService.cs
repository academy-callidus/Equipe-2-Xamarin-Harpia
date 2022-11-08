using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Utils;

namespace xamarin_lib_harpia.Models.Services
{
    internal class TextTestService
    {
        readonly TectoySunmiPrint printerInstance = TectoySunmiPrint.getInstance();
        //Tamanho da font

        private void PrintDivisor()
        {
            printerInstance.PrintText("----------------------------"); 
        }
        private void SetFontSize(int fontSize)
        {
            printerInstance.SetSize(fontSize);
        }
        

        // Alinhamento do texto
        public void AlignText() 
        {
            printerInstance.SetAlign(1); // alinhar center
            printerInstance.PrintText("ALINHAMENTO \n");
            PrintDivisor();
            printerInstance.SetAlign(2); // alinhar left
            printerInstance.PrintText("EQUIPE HARPIA - CALLIDUS ACADEMY");
            printerInstance.SetAlign(1); // alinhar center
            printerInstance.PrintText("EQUIPE HARPIA - CALLIDUS ACADEMY");
            printerInstance.SetAlign(3); // alinhar right
            printerInstance.PrintText("EQUIPE HARPIA - CALLIDUS ACADEMY");


        }

        public void TextPrintShapes()
        {
            /*
             text styles: bold, antiwhite, double height, double width, invert, italic, striketh rough, underline, custom size
             */
        }


    
    }
}
