using System;
using System.Threading.Tasks;
using xamarin_lib_harpia.Models.Entities;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace xamarin_lib_harpia.Models.Services
{
    public class GenerateStatusService
    {
        private IPrinterConnection Connection;

        public GenerateStatusService(IPrinterConnection connection)
        {
            Connection = connection;
        }

        public async void PrintStatus(Page page)
        {
            await page.DisplayToastAsync(Connection.ShowPrinterStatus(), 4000);
        }
    }
}