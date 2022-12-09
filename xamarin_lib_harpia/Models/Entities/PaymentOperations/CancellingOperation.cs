using System.Collections.Generic;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Models.Entities.PaymentOperations
{
    public class CancellingOperation : Operation
    {
        private PaygoCanceling canceling;
        public CancellingOperation(PaygoCanceling canceling)
        {
            this.canceling = canceling;
        }

        public override bool Execute(
            IPayment payment,
            IPrinterConnection connection,
            PaygoTransaction transaction)
        {
            List<Invoice> invoices = payment.CancelPayment(transaction, canceling);
            var result = connection.PrintInvoices(invoices);
            return result;
        }
    }
}
