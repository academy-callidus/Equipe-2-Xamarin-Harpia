using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Services
{
    internal class ConnectionStatusService
    {
        private readonly IPrinterConnection Connection;
        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private string Subtitle {get;set;}
        public ConnectionStatusService(IPrinterConnection connection)
        {
            Connection = connection;
        }
        /// <summary>
        /// Try to start a connection with the printer and check whether it was successful or not
        /// </summary>
        public bool Execute()
        {
            bool isConnected;
            try
            {
                Connection.InitConnection();
                isConnected = Connection.IsConnected();
                return isConnected;
            }
            catch (Exception exp)
            {
                Logger.Warn($"Connection - {exp.Message}");
                return false;
            } 
        }
        /// <summary>
        /// Checks the result of the connection and returns the string to be displayed in the Titlebar
        /// </summary>
        public string ConnectionStatus()
        {
            Subtitle = Execute() ? "Conectado" : "Sem impressora";
            return Subtitle;
        }
    }
}
