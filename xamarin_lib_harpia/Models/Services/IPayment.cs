using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Services
{
    public interface IPayment
    {
        bool MakeAdminTransition(); 
        bool MakeSaleTransition(); 
        bool CancelPayment();
    }
}
