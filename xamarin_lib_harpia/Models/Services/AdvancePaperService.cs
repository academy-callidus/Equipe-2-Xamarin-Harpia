using System;
using System.Threading.Tasks;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public class AdvancePaperService
    {
        private IPrinterConnection Connection;

        public AdvancePaperService(IPrinterConnection connection)
        {
            Connection = connection;
        }
        public bool Execute()
        {
            try
            {
                var response = Connection.AdvancePaper();
                return response;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Here");
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}