using Plugin.BLE;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace AndroidApp
{
   
    public partial class MainPage : ContentPage
    {
        // private ObservableCollection<BluetoothDeviceItem> _discoveredDevices = new ObservableCollection<BluetoothDeviceItem>();
        private IClassicBluetooth _classicBluetooth = DependencyService.Get<IClassicBluetooth>();
        private ObservableCollection<BluetoothDeviceItem> _devices;
        public MainPage()
        {
            InitializeComponent();
            //DevicesListView.ItemsSource = _discoveredDevices;
            //ScanButton.Clicked += OnScanButtonClicked;
            _devices = new ObservableCollection<BluetoothDeviceItem>();
            DevicesListView.ItemsSource = _devices;
            //  var  findDevices = new Button();
            //  findDevices.Text = "Find devices";
            //  IBluetoothLE bluetoothLE = CrossBluetoothLE.Current;
            //  var adapter = CrossBluetoothLE.Current.Adapter;
            //  var deviceList = new List<IDevice>();
            //  var deviceList2 = new List<IDevice>();
            //  IAdapter BluetoothAdapter = DependencyService.Get<IAdapter>(); ;
            ////  BluetoothAdapter.StartScanningForDevicesAsync();
            //  adapter.StartScanningForDevicesAsync();
            //  Task.Delay(2000);
            //  adapter.DeviceDiscovered += (s, a) => deviceList.Add(a.Device);
            //  //BluetoothAdapter.DeviceDiscovered += (s, a) => deviceList2.Add(a.Device);
            //  var label = new Label();
            //  label.Text = deviceList2.Count.ToString(); 
            //  //label.Text = bluetoothLE.Adapter.ConnectedDevices.Count.ToString();
            //  //label.Text += bluetoothLE.State.ToString();
            //  var collectionView = new CollectionView();
            //  collectionView.ItemsSource = bluetoothLE.Adapter.ConnectedDevices;
            //  Content = new StackLayout
            //  {

            //      Children =
            //      {
            //          findDevices,
            //          label, 
            //          collectionView
            //        ,

            //      }
            //  };
        }



        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            //var ble = CrossBluetoothLE.Current;
            //var adapter = CrossBluetoothLE.Current.Adapter;

            //adapter.DeviceDiscovered += (s, a) =>
            //{
            //    var deviceItem = new BluetoothDeviceItem { Name = a.Device.Name, Id = a.Device.Id };
            //    _discoveredDevices.Add(deviceItem);
            //};
            //if(_discoveredDevices.Count == 0) {
            //    var deviceItem = new BluetoothDeviceItem { Name = "Устройства не найдены" };
            //    _discoveredDevices.Add(deviceItem);
            //}
            //// Check if the adapter is already scanning for devices
            //if (!adapter.IsScanning)
            //{
            //    await adapter.StartScanningForDevicesAsync();
            //}
            _classicBluetooth = DependencyService.Get<IClassicBluetooth>();
            try
            {
                var button = (Xamarin.Forms.Button)sender;
                button.IsEnabled = false;
                // Discover Bluetooth devices
                var discoveredDevices = await _classicBluetooth.DiscoverDevicesAsync();
                Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                {
                    // Re-enable the button after the timer expires
                    _devices.Clear();
                    if (discoveredDevices.Count == 0)
                    {
                        _devices.Add(new BluetoothDeviceItem() { Name = "0 devices" });
                    }
                    foreach (var device in discoveredDevices)
                    {
                        _devices.Add(device);
                    }
                    button.IsEnabled = true;
                    
                    // Return false to stop the timer from recurring
                    return false;
                });
                // Clear the ObservableCollection and add the discovered devices
               
               
            }
            catch (Exception ex)
            {
                // Handle exceptions
                await DisplayAlert("Error", $"Exception while discovering devices: {ex.Message}", "OK");
            }
        }
        protected void FindDevices()
        {
            //BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
        }
        
    }
}
