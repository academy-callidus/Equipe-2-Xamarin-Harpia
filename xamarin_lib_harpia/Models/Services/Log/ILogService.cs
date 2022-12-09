using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace xamarin_lib_harpia.Models.Services.Log
{
    public interface ILogService
    {
        void Initialize(Assembly assembly, string assemblyName);
    }
}
