using System;

namespace xamarin_lib_harpia.Models.Services
{
    internal class ImageService
    {
        private IPrinterConnection Connection;

        public ImageService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Try printing an image resource based on it's string name
        /// </summary>
        public bool Execute(string resource)
        {
            try
            {
                var response = Connection.PrintImage(resource);
                return response;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}
