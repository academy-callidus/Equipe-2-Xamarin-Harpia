using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Models.Entities.PaymentOperations
{
    public abstract class Operation
    {
        public abstract bool Execute(
            IPayment payment, 
            IPrinterConnection connection, 
            PaygoTransaction transaction);
    }
}
