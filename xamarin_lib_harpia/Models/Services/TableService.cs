using System;
using System.Collections.Generic;
using System.Text;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public class TableService
    {
        private IPrinterConnection Connection;

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
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}
