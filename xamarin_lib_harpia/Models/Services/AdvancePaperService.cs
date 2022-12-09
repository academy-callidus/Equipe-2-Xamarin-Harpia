using NLog;
using System;
using System.Threading.Tasks;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public class AdvancePaperService
    {
        private IPrinterConnection Connection;
        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public AdvancePaperService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Advances the machine`s paper in three lines
        /// </summary>
        public bool Execute()
        {
            try
            {
                var response = Connection.AdvancePaper();
                return response;
            }
            catch (Exception exception)
            {
                Logger.Warn($"Paper - {exception.Message}");
                return false;
            }
        }
    }
}