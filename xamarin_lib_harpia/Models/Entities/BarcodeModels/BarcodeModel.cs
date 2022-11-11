using System.Collections.Generic;
using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    public abstract class BarcodeModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public BarcodeFormat Format { get; set; }

        public BarcodeModel(int id, string name, BarcodeFormat format)
        {
            ID = id;
            Name = name;
            Format = format;
        }

        public virtual bool IsValid(string content)
        {
            if (content == null) return false;
            return true;
        }

        public static string[] ToOptions(List<BarcodeModel> models)
        {
            string[] options = new string[models.Count];
            for (int index = 0; index < models.Count; index++)
            {
                options[index] = models[index].Name;
            }
            return options;
        }
    }
}
