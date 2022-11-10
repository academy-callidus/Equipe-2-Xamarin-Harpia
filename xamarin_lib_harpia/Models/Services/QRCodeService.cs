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
        public bool Execute(QRcode qrcode)
        {
            /*if (HasCut)
            {
                if (true isK1 = true && height > 1856)
                {
                    //QRCodeService QRService = new QRCodeService();
                    //QRService.setAlign(1);
                    //QRService.text("QrCode\n");
                    //QRService.text("--------------------------------\n");
                    //QRService.printQr(mTextView1.getText().toString(), print_size, error_level);
                    //QRService.print3Line();
                    //QRService.cutpaper(KTectoySunmiPrinter.HALF_CUTTING, 10);
                }
                else
                {
                    //TectoySunmiPrint.getInstance().setAlign(TectoySunmiPrint.Alignment_CENTER);
                    //TectoySunmiPrint.getInstance().printText("QrCode\n");
                    //TectoySunmiPrint.getInstance().printText("--------------------------------\n");
                    //TectoySunmiPrint.getInstance().printQr(mTextView1.getText().toString(), print_size, error_level);
                    //TectoySunmiPrint.getInstance().print3Line();
                }
            } else
            {
                if (true isK1 = true && height > 1856)
                {
                    //kPrinterPresenter.setAlign(1);
                    //kPrinterPresenter.text("QrCode\n");
                    //kPrinterPresenter.text("--------------------------------\n");
                    //kPrinterPresenter.printQr(mTextView1.getText().toString(), print_size, error_level);
                    //kPrinterPresenter.print3Line();
                    //kPrinterPresenter.cutpaper(KTectoySunmiPrinter.HALF_CUTTING, 10);
                }
                else
                {
                    //TectoySunmiPrint.getInstance().setAlign(TectoySunmiPrint.Alignment_CENTER);
                    //TectoySunmiPrint.getInstance().printText("QrCode\n");
                    //TectoySunmiPrint.getInstance().printText("--------------------------------\n");
                    //TectoySunmiPrint.getInstance().printQr(mTextView1.getText().toString(), print_size, error_level);
                    //TectoySunmiPrint.getInstance().print3Line();
                    //TectoySunmiPrint.getInstance().cutpaper();
                }
            }*/
            return true;
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
