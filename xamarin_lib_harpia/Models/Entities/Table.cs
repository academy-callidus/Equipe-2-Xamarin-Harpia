using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.BarcodeModels;

namespace xamarin_lib_harpia.Models.Entities
{
    //in String[] colsTextArr, in int[] colsWidthArr, in int[] colsAlign
    public class Table
    {
        public string[] ColumnsText;
        public int[] ColumnsWidth;
        public AlignmentEnum[] ColumnsAlign;

        public Table(string[] columnsText, int[] columnsWidth, AlignmentEnum[] columnsalign)
        {
            ColumnsAlign = columnsalign;
            ColumnsText = columnsText;
            ColumnsWidth = columnsWidth;
        }

    }
}
