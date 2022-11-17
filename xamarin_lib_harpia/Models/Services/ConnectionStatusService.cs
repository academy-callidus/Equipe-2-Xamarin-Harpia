using System;
using System.Collections.Generic;
using System.Text;

namespace xamarin_lib_harpia.Models.Services
{
    internal class ConnectionStatusService
    {
        private readonly IPrinterConnection Connection;
        private string Subtitle {get;set;}
        public ConnectionStatusService(IPrinterConnection connection)
        {
            Connection = connection;
        }
        /// <summary>
        /// Função que tenta iniciar uma conexão com a impressora e checa se foi bem sucessido ou não
        /// </summary>
        /// <returns>Se a conexão foi bem sucedida ou não</returns>
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
                Console.WriteLine(exp.Message);
                return false;
            } 
        }
        /// <summary>
        /// Executa a função de teste de conexão e verifica o resultado, retornando a string a ser exibida na Titlebar
        /// </summary>
        /// <returns>Textos equivalentes a conexão bem sucessidade ou não</returns>
        public string ConnectionStatus()
        {
            Subtitle = Execute() ? "Conectado" : "Sem impressora";
            return Subtitle;
        }
    }
}
