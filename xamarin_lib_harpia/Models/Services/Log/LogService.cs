using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using NLog;
using NLog.Config;

namespace xamarin_lib_harpia.Models.Services.Log
{
    public class LogService : ILogService
    {
        public void Initialize(Assembly assembly, string assemblyName)
        {
            string location = "xamarin_lib_harpia.Droid.NLog.config";
            Stream stream = assembly.GetManifestResourceStream(location);
            if(stream == null)
            {
                throw new Exception($"Falha ao carregar recursos em {location}");
            }

            LogManager.Configuration = new XmlLoggingConfiguration(System.Xml.XmlReader.Create(stream), null);
        }
    }
}
