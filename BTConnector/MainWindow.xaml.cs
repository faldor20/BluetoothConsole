﻿using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace BTConnector
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		ListBox UI_bluetoothDevicesListBox = new ListBox();
		public MainWindow()
		{
			InitializeComponent();
			StackPanel stackPanel = new StackPanel();
			this.Content = stackPanel;
			var data = new ProgramData();
			// Create the Button 

			TestUI(stackPanel);

			var bluetoothStatus = CheckIfDeviceHasSupportedBluetooth();

			var UI_BluetoothStatus = new TextBox()
			{
				Text = bluetoothStatus.description,
					HorizontalAlignment = HorizontalAlignment.Right,
					VerticalAlignment = VerticalAlignment.Top,
					Margin = new Thickness(10),
					FontSize = 16,
					FontFamily = new FontFamily("Segoe"),
					FontWeight = FontWeights.Medium,

			};

			if (bluetoothStatus.supported == false)
			{
				UI_BluetoothStatus.Background = Brushes.Tomato;
			}

			stackPanel.Children.Add(UI_BluetoothStatus);

			if (bluetoothStatus.supported)
			{
				UI_BluetoothStatus.Background = Brushes.LightGreen;

				var BTCLI = new BluetoothClient();

				Button UI_But_Scan = new Button();

				UI_But_Scan.Click += (sender, e) => UI_bluetoothDevicesListBox.ItemsSource = BTCLI.DiscoverDevicesInRange();

				UI_But_Scan.Content = "ScanDevices";
				UI_But_Scan.HorizontalAlignment = HorizontalAlignment.Left;
				UI_But_Scan.Margin = new Thickness(150);
				UI_But_Scan.VerticalAlignment = VerticalAlignment.Top;
				UI_But_Scan.Width = 75;

				stackPanel.Children.Add(UI_But_Scan);
				stackPanel.Children.Add(UI_bluetoothDevicesListBox);

				var UI_ConnectToDevice = new Button()
				{
					Content = "Connect To Device",
						HorizontalAlignment = HorizontalAlignment.Center,
						Margin = new Thickness(30),
						VerticalAlignment = VerticalAlignment.Bottom,
						Width = 75,

				};
				UI_ConnectToDevice.Click += (sender, e) =>
				{
					ConnectToBluetoothDevice(UI_bluetoothDevicesListBox.SelectedItem as BluetoothDeviceInfo, BTCLI);
				};
			}

		}
		void UpdateBluetoothDevicelist(System.Collections.IEnumerable list)
		{
			UI_bluetoothDevicesListBox.ItemsSource = list;
		}
		void ConnectToBluetoothDevice(BluetoothDeviceInfo deviceInfo, BluetoothClient BTClient)
		{
			var paired = BluetoothSecurity.PairRequest(deviceInfo.DeviceAddress, "0000");
			if (paired)
			{
				deviceInfo.SetServiceState(BluetoothService.SerialPort, true); //TODO: find out what service it should be
			}
			else
			{
				throw new System.Exception("pairing failed");
			}
			//this is a good example https://stackoverflow.com/questions/23690243/32feet-net-connecting-to-bluetooth-speakers
		}
		public static(bool supported, string description) CheckIfDeviceHasSupportedBluetooth()
		{

			BluetoothRadio[] bluetoothdRadios = BluetoothRadio.AllRadios;
			if (bluetoothdRadios.Length == 0)
			{

				return (false, "There are no bluetooth radios in this system");
			}
			foreach (var radio in bluetoothdRadios)
			{

				if (radio.Mode == 0) { return (false, "bluetooth is off"); }
				if (!BluetoothRadio.IsSupported) return (false, "Bluetooth radio is not supported");
				else if (BluetoothRadio.IsSupported) return (true, "Bluetooth radio is supported and enabled working");

			}
			return (false, "state is not accounted for, check with developer");
		}

		void TestUI(StackPanel stackPanel)
		{
			var listBox = new ListBox();
			var hey = new string[] { "hey", "hi", "potato", "stuff" };
			listBox.ItemsSource = hey;
			stackPanel.Children.Add(listBox);
		}
	}
}