using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public interface IPayment
    {
        List<Invoice> MakeAdminTransition(PaygoTransaction transaction);
        List<Invoice> MakeSaleTransition(PaygoTransaction transaction);
        List<Invoice> CancelPayment(PaygoTransaction transaction);
    }
}
