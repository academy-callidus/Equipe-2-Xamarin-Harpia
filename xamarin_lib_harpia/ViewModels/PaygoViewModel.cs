using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms.Internals;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.ViewModels
{
    internal class PaygoViewModel : INotifyPropertyChanged
    {
        decimal value = 0.00M;
        int installmentNumber = 1;
        int paymentType = 0;
        int purchaserOption = 0;
        int installmentType = 0;

        bool manual = false;
        bool complete = false;
        bool client = false;
        bool altInterface = false;

        public readonly string[] paymentTypes = { 
            "Não definido", 
            "Crédito", 
            "Débito", 
            "Carteira Digital" 
        };
        public readonly string[] purchaserOptions = { 
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
        public readonly string[] installmentTypes = { 
            "Não definido", 
            "À Vista", 
            "Emissor", 
            "Estabelecimento" 
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public PaygoTransaction GetTransaction()
        {
            return new PaygoTransaction(
                value, 
                installmentNumber, 
                installmentType, 
                paymentType, 
                purchaserOption, 
                manual, 
                complete, 
                client, 
                altInterface
            );
        }

        public string Value
        {
            get => value.ToString();
            
            set
            {
                if (!string.IsNullOrEmpty(value)
                    && decimal.TryParse(value, out var newValue)
                    && newValue > 0M
                    && newValue != this.value)
                {
                    this.value = newValue;
                    OnPropertyChanged();
                }
            }
        }

        public string InstallmentNumber
        {
            get => installmentNumber.ToString();
            
            set
            {
                if(!string.IsNullOrEmpty(value)
                    && int.TryParse(value, out var newValue)
                    && newValue > 0
                    && newValue != this.installmentNumber)
                {
                    this.installmentNumber = newValue;
                    OnPropertyChanged();
                }
            }
        }

        public string PaymentType
        {
            get => this.paymentTypes[paymentType];

            set
            {
                int newValue = Array.FindIndex(paymentTypes, type => type == value);
                if (newValue != -1 && newValue != this.paymentType)
                {
                    this.paymentType = newValue;
                    OnPropertyChanged();
                }
            }
        }

        public string Purchaser
        {
            get => this.purchaserOptions[purchaserOption];

            set
            {
                int newValue = Array.FindIndex(purchaserOptions, type => type == value);
                if (newValue != -1 && newValue != this.purchaserOption)
                {
                    this.purchaserOption = newValue;
                    OnPropertyChanged();
                }
            }
        }

        public string InstallmentType
        {
            get => this.installmentTypes[installmentType];

            set
            {
                int newValue = Array.FindIndex(installmentTypes, type => type == value);
                if (newValue != -1 && newValue != this.installmentType)
                {
                    this.installmentType = newValue;
                    OnPropertyChanged();
                }
            }
        }

        public bool Manual
        {
            get => this.manual;
            set { this.manual = value; OnPropertyChanged(); }
        }

        public bool Complete
        {
            get => this.complete;
            set { this.complete = value; OnPropertyChanged(); }
        }

        public bool Client
        {
            get => this.client;
            set { this.client = value; OnPropertyChanged(); }
        }
        
        public bool AltInterface
        {
            get => this.altInterface;
            set { this.altInterface = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Helper method to fire PropertyChange events on class member setter methods.
        /// </summary>
        /// <param name="propertyName">String representation of the property being set.</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
