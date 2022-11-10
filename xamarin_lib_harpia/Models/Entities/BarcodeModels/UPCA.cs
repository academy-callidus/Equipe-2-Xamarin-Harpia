using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    internal class UPCA : NumericalOnlyBarcodeModel
    {
        public UPCA() : base(0, "UPC-A", BarcodeFormat.UPC_A)
        {
        }

        public override bool IsValid(string content)
        {
            return base.IsValid(content) && content.Length == 12;
        }
    }
}
