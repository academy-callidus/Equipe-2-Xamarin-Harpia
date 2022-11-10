using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    internal class CODE128C : BarcodeModel
    {
        public CODE128C() : base(10,"CODE128C", BarcodeFormat.CODE_128)
        {
        }
    }
}
