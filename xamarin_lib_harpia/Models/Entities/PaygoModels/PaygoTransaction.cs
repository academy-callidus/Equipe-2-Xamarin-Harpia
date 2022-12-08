using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities.PaygoModels
{
    internal class PaygoTransaction
    {
        public decimal Value;
        public int InstallmentNumber;
        public int InstallmentType;
        public int PaymentType;
        public int PurchaserOption;
        public bool Manual;
        public bool Complete;
        public bool Client;
        public bool AltInterface;

        public PaygoTransaction(decimal value, int installmentNumber, int installmentType, int paymentType, int purchaserOption, bool manual, bool complete, bool client, bool altInterface)
        {
            Value = value;
            InstallmentNumber = installmentNumber;
            InstallmentType = installmentType;
            PaymentType = paymentType;
            PurchaserOption = purchaserOption;
            Manual = manual;
            Complete = complete;
            Client = client;
            AltInterface = altInterface;
        }
    }
}
