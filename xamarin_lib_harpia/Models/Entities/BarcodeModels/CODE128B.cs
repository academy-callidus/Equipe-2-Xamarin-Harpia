using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    internal class CODE128B : BarcodeModel
    {
        public CODE128B() : base(9, "CODE128B", BarcodeFormat.CODE_128)
        {
        }
    }
}
