using System;
using System.Collections.Generic;
using System.Text;
using ZXing;

namespace xamarin_lib_harpia.Models
{
    internal class BarcodeModel
    {
        public string Model { get; }
        public BarcodeFormat Format { get; }

        public BarcodeModel(string model, BarcodeFormat format)
        {
            this.Model = model;
            this.Format = format;
        }

        public static string[] ToOptions(List<BarcodeModel> models)
        {
            string[] options = new string[models.Count];
            for (int index = 0; index < models.Count; index++)
            {
                options[index] = models[index].Model;
            }
            return options;
        }
    }
}
