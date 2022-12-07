using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Services.Payments
{
    public abstract class Operation
    {
        public abstract bool Execute(IPrinterConnection connection); // TODO: add parameters
    }
}
