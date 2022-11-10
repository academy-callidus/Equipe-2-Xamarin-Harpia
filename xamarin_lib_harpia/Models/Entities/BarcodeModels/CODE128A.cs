using ZXing;

namespace xamarin_lib_harpia.Models.BarcodeModels
{
    internal class CODE128A : BarcodeModel
    {
        public CODE128A() : base(8, "CODE128A", BarcodeFormat.CODE_128)
        {
        }
    }
}
