using xamarin_lib_harpia.Models.Entities.PaymentOperations;
using xamarin_lib_harpia.Models.Entities;
using System;
using NLog;

namespace xamarin_lib_harpia.Models.Services
{
    public class PaymentService
    {
        private readonly IPrinterConnection Connection;
        private readonly IPayment Payment;
        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public PaymentService(IPrinterConnection connection, IPayment payment)
        {
            Connection = connection;
            Payment = payment;
        }

        public bool Execute(Operation operation, PaygoTransaction transaction)
        {
            try
            {
                Logger.Info($"PaymentService: Executed");
                return operation.Execute(Payment, Connection, transaction);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
