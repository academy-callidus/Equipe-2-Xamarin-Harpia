using System.Collections.Generic;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Models.Entities.PaymentOperations
{
    public class AdministrativeOperation : Operation
    {
        public override bool Execute(
            IPayment payment, 
            IPrinterConnection connection, 
            PaygoTransaction transaction)
        {
            List<Invoice> invoices = payment.MakeAdminTransition(transaction);
            connection.PrintInvoices(invoices);
            return true;
        }
    }
}
