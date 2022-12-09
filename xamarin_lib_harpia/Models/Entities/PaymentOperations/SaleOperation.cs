using System;
using System.Collections.Generic;
using xamarin_lib_harpia.Models.Services;

namespace xamarin_lib_harpia.Models.Entities.PaymentOperations
{
    public class SaleOperation : Operation
    {
        public override bool Execute(
            IPayment payment,
            IPrinterConnection connection,
            PaygoTransaction transaction)
        {

            List<Invoice> invoices = payment.MakeSaleTransition(transaction);
            connection.PrintInvoices(invoices);
            return true;
        }
    }
}
