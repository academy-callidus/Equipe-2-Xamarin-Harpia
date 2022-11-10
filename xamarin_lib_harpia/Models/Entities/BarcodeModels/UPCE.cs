using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    internal class UPCE : NumericalOnlyBarcodeModel
    {
        public UPCE() : base(1, "UPC-E", BarcodeFormat.UPC_E)
        {
        }

        public override bool IsValid(string content)
        {
            return base.IsValid(content) && content.Length == 6;
        }
    }
}
