using xamarin_lib_harpia.Models.Entities.PaymentOperations;
using xamarin_lib_harpia.Models.Entities;
using System;

namespace xamarin_lib_harpia.Models.Services
{
    public class PaymentService
    {
        private readonly IPrinterConnection Connection;
        private readonly IPayment Payment;

        public PaymentService(IPrinterConnection connection, IPayment payment)
        {
            Connection = connection;
            Payment = payment;
        }

        public bool Execute(Operation operation, PaygoTransaction transaction)
        {
            try
            {
                return operation.Execute(Payment, Connection, transaction);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
