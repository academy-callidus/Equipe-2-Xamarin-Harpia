using System.Collections.Generic;
using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{

    /// <summary>
    /// Abstract class representing a barcode model
    /// </summary>
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

        /// <summary>
        /// Check if it is valid barcode content
        /// </summary>
        public virtual bool IsValid(string content)
        {
            if (content == null) return false;
            return true;
        }

        /// <summary>
        /// Get the model names from a list of Barcode Model classes
        /// </summary>
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
