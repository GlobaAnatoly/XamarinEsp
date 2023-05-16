using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndroidApp
{
    public interface IClassicBluetooth
    {
        Task ConnectAsync(string deviceAddress);
        Task SendDataAsync(byte[] data);
        Task<List<BluetoothDeviceItem>> DiscoverDevicesAsync();
    }
}
