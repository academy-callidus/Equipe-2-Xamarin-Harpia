namespace xamarin_lib_harpia.Models.Services.BarcodeModels
{
    internal class UPCE : NumericalOnlyBarcodeModel
    {
        public UPCE() : base("UPC-E")
        {
        }

        public override bool IsValid(string content)
        {
            return base.IsValid(content) && content.Length == 6;
        }
    }
}
