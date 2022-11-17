using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Services
{
    internal class ConnectionStatusService
    {
        private readonly IPrinterConnection Connection;
        private string Subtititle {get;set;}
        public ConnectionStatusService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        public bool Execute()
        {
            bool isConnected;
            try
            {
                Connection.InitConnection();
                isConnected = Connection.IsConnected();
                Connection.CloseConnection();
                return isConnected;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return false;
            } 
        }

        public string ConnectionStatus()
        {
            Subtititle = Execute() ? "Conectado" : "Sem impressora";
            return Subtititle;
        }
    }
}
