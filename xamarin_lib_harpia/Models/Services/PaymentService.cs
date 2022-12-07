using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Services.Payments;

namespace xamarin_lib_harpia.Models.Services
{
    public class PaymentService
    {
        private readonly IPrinterConnection Connection;

        public PaymentService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        public bool Execute(Operation operation) // TODO: add payment info as a parameter
        {
            return operation.Execute(Connection);
        }
    }
}
