using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class Image
    {
        public string Resource { get; set; }
        public AlignmentEnum Alignment { get; set; }
        public bool CutPaper { get; set; }

        public Image(string resource, AlignmentEnum alignment, bool cutPaper)
        {
            Resource = resource;
            Alignment = alignment;
            CutPaper = cutPaper;
        }
    }
}
