using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using xamarin_lib_harpia.Droid;
using xamarin_lib_harpia.Models;
using xamarin_lib_harpia.Models.Services;
using BluetoothPrinter.Droid;
using xamarin_lib_harpia.Models.Entities;
using Java.Util;
using Java.Net;
using System.Net.Sockets;
using System.Linq;

[assembly: Xamarin.Forms.Dependency(typeof(PrinterBluetoothConnection))]
namespace BluetoothPrinter.Droid
{
    public class PrinterBluetoothConnection : IPrinterConnection
    {
        private BluetoothDevice _connectedDevice;
        public PrinterBluetoothConnection()
        {
        }

        // método através do qual uma lista de bytes será utilizada para enviar comandos à impressora
        public async void sendRawData(byte[] data, BluetoothDevice device)
        {
            BluetoothSocket socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
            try
            { 
             await socket.ConnectAsync();
             await socket.OutputStream.WriteAsync(data.ToArray(), 0, data.Length);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DeviceInfo> GetAvailableDevices()
        {
            if (BluetoothAdapter.DefaultAdapter != null && BluetoothAdapter.DefaultAdapter.IsEnabled)
            {
                List<DeviceInfo> result = new List<DeviceInfo>();
                foreach (var pairedDevice in BluetoothAdapter.DefaultAdapter.BondedDevices)
                {
                    result.Add(new DeviceInfo
                    {
                        Title = pairedDevice.Name,
                        MacAddress = pairedDevice.Address
                    });
                }
                return result;
            }
            return null;
        }

        public DeviceInfo GetCurrentDevice()
        {
            if (_connectedDevice != null)
            {
                return new DeviceInfo
                {
                    Title = _connectedDevice.Name,
                    MacAddress = _connectedDevice.Address
                };
            }
            return null;
        }

        public bool SetCurrentDevice(string printerName)
        {
            if (BluetoothAdapter.DefaultAdapter != null && BluetoothAdapter.DefaultAdapter.IsEnabled)
            {
                foreach (var pairedDevice in BluetoothAdapter.DefaultAdapter.BondedDevices)
                {
                    if (pairedDevice.Name == printerName)
                    {
                        _connectedDevice = pairedDevice;
                        return true;
                    }
                }
            }
            return false;
        }

        public void PrintQR(string content)
        {
            SendCommandToPrinter("qr", content, _connectedDevice);
        }

        public void PrintText(string content)
        {
            SendCommandToPrinter("plain", content, _connectedDevice);
        }

        async void SendCommandToPrinter(string type, string content, BluetoothDevice device)
        {
            if (string.IsNullOrEmpty(content)) return;
            Printer print = new Printer();
            if (device != null)
            {
                await print.Print(type, content, device);
            }
            else
            {
                throw new Exception("No selected device.");
            }
        }

        public bool InitConnection()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public bool PrintBarcode(Barcode barcode)
        {
            throw new NotImplementedException();
        }

        public void SetAlignment(AlignmentEnum alignment)
        {
            throw new NotImplementedException();
        }

        public bool CutPaper()
        {
            throw new NotImplementedException();
        }

        public bool Print3Line()
        {
            throw new NotImplementedException();
        }
    }
}
