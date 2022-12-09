using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Entities
{
    public class PaygoTransaction
    {
        private readonly string[] purchaserOptions = {
            "PROVEDOR DESCONHECIDO",
            "LIBERCARD",
            "ELAVON",
            "CIELO",
            "RV",
            "BIN",
            "FDCORBAN",
            "REDE",
            "INFOCARDS",
            "CREDSYSTEM",
            "NDDCARD",
            "VERO",
            "GLOBAL",
            "GAX",
            "STONE",
            "DMCARD",
            "CTF",
            "TICKETLOG",
            "GETNET",
            "VCMAIS",
            "SAFRA",
            "PAGSEGURO",
            "CONDUCTOR"
        };
        public decimal Value { get; }
        public int InstallmentNumber { get; }
        public int InstallmentType { get; }
        public int PaymentType { get; }
        public int PurchaserOption { get; }
        public bool Manual { get; }
        public bool Complete { get; }
        public bool Client { get; }
        public bool AltInterface { get; }

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

        public string GetPurchaserName()
        {
            return purchaserOptions[PurchaserOption];
        }
    }
}
