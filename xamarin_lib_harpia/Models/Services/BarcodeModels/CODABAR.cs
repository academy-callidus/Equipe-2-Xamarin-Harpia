
using System.Text.RegularExpressions;

namespace xamarin_lib_harpia.Models.Services.BarcodeModels
{
    internal class CODABAR : BarcodeModel
    {
        public CODABAR() : base("CODABAR")
        {
        }

        public override bool IsValid(string content)
        {
            if (!base.IsValid(content)) return false;
            Regex regex = new Regex(@"^[a-dA-D][0-9\-\$\/\+]+[a-dA-D]$");
            return regex.IsMatch(content);
        }
    }
}
