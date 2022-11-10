﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class QRcode
    {
        private string Content { get; set; }
        private int ImpQuant { get; set; }
        private int ImpSize { get; set; }
        private QrCodeCorrectionEnum Correction { get; set; }
        private AlignmentEnum Alignment { get; set; }
        private bool CutPaper { get; set; }


        public QRcode(string content, int impquant, int impsize, QrCodeCorrectionEnum correction, AlignmentEnum alignment, bool cutPaper)
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
        }

        public override string ToString()
        {
            return $"Qrcode[Content={Content}, Imp. Quantity={ImpQuant}, Imp. Size={ImpSize}, Correction Level={Correction}, Alignment={Alignment}, CutPaper={CutPaper}"
        }

    }
}