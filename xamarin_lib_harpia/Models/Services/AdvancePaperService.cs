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

        /// <summary>
        /// Advances the machine`s paper in three lines
        /// </summary>
        public bool Execute()
        {
            var response = Connection.AdvancePaper();
            return response;

        }
    }
}