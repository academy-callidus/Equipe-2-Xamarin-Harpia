namespace xamarin_lib_harpia.Models.Services.BarcodeModels
{
    internal class UPCA : NumericalOnlyBarcodeModel
    {
        public UPCA() : base("UPC-A")
        {
        }

        public override bool IsValid(string content)
        {
            return base.IsValid(content) && content.Length == 12;
        }
    }
}
