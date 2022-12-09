using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public class TableService
    {
        private IPrinterConnection Connection;
        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public TableService(IPrinterConnection connection)
        {
     
            Connection = connection;
        }


        public bool Execute(Table table)
        {
            try
            {
                var response = Connection.PrintTable(table);
                return response;
            }
            catch (Exception exception)
            {
                Logger.Warn($"Table - {exception.Message}");
                return false;
            }
        }
    }
}
