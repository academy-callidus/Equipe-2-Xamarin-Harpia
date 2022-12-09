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
    public partial class CancelingViewModel : INotifyPropertyChanged
    {
        public PaygoTransaction paygoTransaction;

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
