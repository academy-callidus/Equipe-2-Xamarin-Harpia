using System.Collections.Generic;
using xamarin_lib_harpia.Models.Services;
using NLog;

namespace xamarin_lib_harpia.Models.Entities.PaymentOperations
{
    public class AdministrativeOperation : Operation
    {
        private ILogger Logger = LogManager.GetCurrentClassLogger();
        public override bool Execute(
            IPayment payment, 
            IPrinterConnection connection, 
            PaygoTransaction transaction)
        {
            Logger.Info("AdministrativeOperation: Executed");
            List<Invoice> invoices = payment.MakeAdminTransition(transaction);
            connection.PrintInvoices(invoices);
            return true;
        }
    }
}
