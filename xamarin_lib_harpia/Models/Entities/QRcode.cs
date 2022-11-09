using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class QRcode
    {
        private string v1;
        private int v2;
        private int v3;
        private int v4;
        private QrCodeCorrectionEnum cORRECTION_H;
        private AlignmentEnum cENTER;

        private string Content { get; set; }
        private int ImpQuant { get; set; }
        private int ImpSize { get; set; }
        private QrCodeCorrectionEnum Correction { get; set; }
        private AlignmentEnum Alignment { get; set; }
        private bool CutPaper { get; set; }
        /*public QRcode(string content, int impquant, int impsize, QrCodeCorrectionEnum correction, AlignmentEnum alignment, bool cutPaper)
        {
            Content = content;
            ImpQuant = impquant;
            ImpSize = impsize;
            Correction = correction;
            Alignment = alignment;
            CutPaper = cutPaper;
        }*/
        public QRcode()
        {
            this.Correction = new QrCodeCorrectionEnum();
        }

    }
}
