using System.Text.RegularExpressions;

namespace xamarin_lib_harpia.Models.Services.BarcodeModels
{
    internal class CODE39 : BarcodeModel
    {
        public CODE39() : base("CODE39")
        {
        }

        public override bool IsValid(string content)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9 \-\.\$\/\+\%]{1,43}$");
            return base.IsValid(content) && regex.IsMatch(content);
        }
    }
}
