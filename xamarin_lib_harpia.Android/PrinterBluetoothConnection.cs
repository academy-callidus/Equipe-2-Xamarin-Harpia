using Android.Bluetooth;
using System;
using System.Collections.Generic;
using xamarin_lib_harpia.Models;
using xamarin_lib_harpia.Models.Entities;
using xamarin_lib_harpia.Utils;
using Java.Util;
using Android.Content;
using Android.Runtime;

namespace BluetoothPrinter.Droid
{
    [Obsolete("PrinterBluetoothConnection is deprecrated. Please use 'PrinterConnection' instead.", true)]
    public class PrinterBluetoothConnection
    {
        private BluetoothManager BluetoothManager;
        private BluetoothDevice _connectedDevice;
        public PrinterBluetoothConnection()
        {
            BluetoothManager = Android.App.Application.Context.GetSystemService(Context.BluetoothService).JavaCast<BluetoothManager>();
            _connectedDevice = null;
        }

        public void SendRawData(byte[] data)
        {
            var socket = _connectedDevice.CreateRfcommSocketToServiceRecord(
                UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")
            );
            try
            {
                socket.Connect();
                socket.OutputStream.Write(data, 0, data.Length);
                socket.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public List<DeviceInfo> GetAvailableDevices()
        {
            if (BluetoothManager.Adapter != null && BluetoothManager.Adapter.IsEnabled)
            {
                List<DeviceInfo> result = new List<DeviceInfo>();
                foreach (var pairedDevice in BluetoothManager.Adapter.BondedDevices)
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
           
            if (BluetoothManager.Adapter != null && BluetoothManager.Adapter.IsEnabled)
            {
                foreach (var pairedDevice in BluetoothManager.Adapter.BondedDevices)
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

        public bool InitConnection()
        {
            try
            {
                if (IsConnected()) return true;
                var availableDevices = GetAvailableDevices();
                if (availableDevices.Count == 0) return false;
                var device = availableDevices[0];
                SetCurrentDevice(device.Title);
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                if (!IsConnected()) return false;
                _connectedDevice = null;
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        public bool IsConnected()
        {
            return _connectedDevice != null;
        }

        public bool PrintBarcode(Barcode barcode)
        {
            InitConnection();
            if (!IsConnected()) return false;
            byte[] barcodeCommands = CommandUtils.GetBarcodeBytes(barcode);
            try
            {
                SendRawData(barcodeCommands);
                SendRawData(new byte[] { 0x0A });
                CloseConnection();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                CloseConnection();
                return false;
            }
        }

      
        public bool PrintQRCode(QRcode qrcode)
        {
            InitConnection();
            if (!IsConnected()) return false;
            byte[] qrcodeCommands = CommandUtils.GetQrcodeBytes(qrcode);
            try
            {
                SendRawData(qrcodeCommands);
                SendRawData(new byte[] { 0x0A });
                CloseConnection();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                CloseConnection();
                return false;
            }
        }

        public bool PrintText(Text text)
        {
            InitConnection();
            if (!IsConnected()) return false;
            byte[] textCommands = CommandUtils.GetTextBytes(text);
            try
            {
                SendRawData(textCommands);
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
    }
}