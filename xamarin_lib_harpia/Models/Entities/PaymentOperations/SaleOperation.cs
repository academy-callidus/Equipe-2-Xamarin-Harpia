using System;
using System.Collections.Generic;
using xamarin_lib_harpia.Models.Services;
using NLog;

namespace xamarin_lib_harpia.Models.Entities.PaymentOperations
{
    public class SaleOperation : Operation
    {
        private ILogger Logger = LogManager.GetCurrentClassLogger();
        public override bool Execute(
            IPayment payment,
            IPrinterConnection connection,
            PaygoTransaction transaction)
        {
            Logger.Info("SaleOperation: Executed");
            List<Invoice> invoices = payment.MakeSaleTransition(transaction);
            connection.PrintInvoices(invoices);
            return true;
        }
    }
}
