using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public class TextService
    {
        private IPrinterConnection Connection;

        public TextService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Try to start a connection with the printer, check whether it was successful or not and 
        /// if was sucessful return the bytecode reponse from the PrintText method
        /// </summary>
        public bool Execute(Text text)
        {
            try
            {
                var response = Connection.PrintText(text);
                return response;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}
