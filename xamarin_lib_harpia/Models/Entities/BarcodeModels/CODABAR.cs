
using System.Text.RegularExpressions;
using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    internal class CODABAR : BarcodeModel
    {
        public CODABAR() : base(6, "CODABAR", BarcodeFormat.CODABAR)
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
