using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class QRcode
    {
        public string Content { get; set; }
        public int ImpQuant { get; set; }
        public int ImpSize { get; set; }
        public QRCodeCorretionEnum Correction { get; set; }
        public AlignmentEnum Alignment { get; set; }
        public bool CutPaper { get; set; }


        public QRcode(string content, int impquant, int impsize, QRCodeCorretionEnum correction, AlignmentEnum alignment, bool cutPaper)
        {
            Content = content;
            ImpQuant = impquant;
            ImpSize = impsize;
            Correction = correction;
            Alignment = alignment;
            CutPaper = cutPaper;
        }

        public bool IsValid()
        {
            if(Content == null) return false;
            return true;
        }

        public override string ToString()
        {
            return $"Qrcode[Content={Content}, Imp. Quantity={ImpQuant}, Imp. Size={ImpSize}, Correction Level={Correction}, Alignment={Alignment}, CutPaper={CutPaper}]";
        }

    }
}
