using System.Collections.Generic;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Models.Entities.PaymentOperations
{
    public class CancellingOperation : Operation
    {
        public override bool Execute(
            IPayment payment,
            IPrinterConnection connection,
            PaygoTransaction transaction)
        {
            List<Invoice> invoices = payment.CancelPayment(transaction);
            var result = connection.PrintInvoices(invoices);
            return result;
        }
    }
}
