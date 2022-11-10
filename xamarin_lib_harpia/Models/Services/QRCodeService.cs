using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xamarin_lib_harpia.Models.Entities;

namespace xamarin_lib_harpia.Models.Services
{
    public class QRCodeService
    {
        private IPrinterConnection Connection;

        public QRCodeService(IPrinterConnection connection)
        {
            Connection = connection;
        }
        public async Task<bool> Execute(QRcode qrcode)
        {
            if (!qrcode.IsValid()) return false;
            try
            {
                var response = await Connection.PrintQRCode(qrcode);
                return response;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public bool InitConnection()
        {
            return true;
        }
        public bool IsConnected()
        {
            return true;
        }

        public async Task<bool> PrintQRCode(QRcode qrcode) 
        {
            if (!IsConnected()) return false;
            InitConnection();
            byte[] qrcodeCommands = CommandUtils.GetQrcodeBytes(qrcode);
            try
            {
                await SendRawData(qrcodeCommands);
                CloseConnection();
                return true;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                CloseConnection();
                return false;
            }
            
        }
        public bool PrintDoubleQRCode(QRcode qrcode)
        {
            return true;
        }

        public void SetAlignment(AlignmentEnum alignment)
        {

        }

        public bool CutPaper()
        {
            return true;
        }
        public bool Print3Line()
        {
            return true;
        }
    }

}
