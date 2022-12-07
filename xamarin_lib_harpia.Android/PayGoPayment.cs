using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Droid
{
    [assembly: Xamarin.Forms.Dependency(typeof(Payment))]
    public class PayGoPayment implements IPayment
    {
        public bool MakeAdminTransition()
        {
            return false;
        }

        public bool MakeSaleTransition()
        {
            return false;
        }

        public bool CancelPayment()
        {
            return false;
        }
    }
}