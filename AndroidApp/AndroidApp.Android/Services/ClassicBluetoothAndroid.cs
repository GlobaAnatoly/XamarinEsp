using Android.Content;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Bluetooth;
using AndroidApp.Droid.Services;
//using System.Runtime.CompilerServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(ClassicBluetoothAndroid))]
namespace AndroidApp.Droid.Services
{
    
    public class ClassicBluetoothAndroid : IClassicBluetooth
        {
            public Task ConnectAsync(string deviceAddress)
            {
                throw new NotImplementedException();
            }

            // ...

            [Obsolete]
            public async Task<List<BluetoothDeviceItem>> DiscoverDevicesAsync()
            {
                // Get the default Bluetooth adapter
                BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;

                // Check if the device has Bluetooth capabilities
                if (bluetoothAdapter == null)
                {
                    throw new NotSupportedException("This device does not have Bluetooth capabilities.");
                }

                // Check if Bluetooth is enabled
                if (!bluetoothAdapter.IsEnabled)
                {
                    throw new InvalidOperationException("Bluetooth is not enabled. Please enable Bluetooth and try again.");
                }

                // Discover Bluetooth devices
                var devices = new List<BluetoothDeviceItem>();
                var receiver = new BroadcastReceiver(devices);
                IntentFilter filter = new IntentFilter(BluetoothDevice.ActionFound);
                Android.App.Application.Context.RegisterReceiver(receiver, filter);

                // Start discovery
                bluetoothAdapter.StartDiscovery();

                // Wait for discovery to complete (12 seconds by default)
                await Task.Delay(2000);

                // Cancel discovery and unregister the receiver
                bluetoothAdapter.CancelDiscovery();
                Android.App.Application.Context.UnregisterReceiver(receiver);

                return devices;
            }

            public Task SendDataAsync(byte[] data)
            {
                throw new NotImplementedException();
            }

            private class BroadcastReceiver : Android.Content.BroadcastReceiver
            {
                private readonly List<BluetoothDeviceItem> _devices;

                public BroadcastReceiver(List<BluetoothDeviceItem> devices)
                {
                    _devices = devices;
                }

                [Obsolete]
                public override void OnReceive(Context context, Intent intent)
                {
                    string action = intent.Action;

                    if (BluetoothDevice.ActionFound.Equals(action))
                    {
                        BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                        _devices.Add(new BluetoothDeviceItem { Name = device.Name });
                    }
                }
            }
        }
    }
