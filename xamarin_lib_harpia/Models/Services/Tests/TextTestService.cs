using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Utils;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services.Tests
{
    internal class TextTestService
    {
        readonly TectoySunmiPrint printerInstance = TectoySunmiPrint.getInstance();

        private void PrintDivisor()
        {
            printerInstance.PrintText("----------------------------");
        }

        //Tamanho da font
        private void SetFontSize(int fontSize)
        {
            printerInstance.SetSize(fontSize);
        }


        // Alinhamento do texto
        public void AlignText()
        {
            printerInstance.SetAlign(AlignmentEnum.CENTER);
            printerInstance.PrintText("ALINHAMENTO \n");
            PrintDivisor();
            printerInstance.SetAlign(AlignmentEnum.LEFT);
            printerInstance.PrintText("EQUIPE HARPIA - CALLIDUS ACADEMY");
            printerInstance.SetAlign(AlignmentEnum.CENTER) ;
            printerInstance.PrintText("EQUIPE HARPIA - CALLIDUS ACADEMY");
            printerInstance.SetAlign(AlignmentEnum.RIGHT);
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
