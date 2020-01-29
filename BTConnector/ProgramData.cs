using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace BTConnector
{
	public class ProgramData
	{
		public string comPortName { get; set; }
		public System.Collections.Generic.IReadOnlyCollection<BluetoothDeviceInfo> availableDevices { get; }
		public ProgramData()
		{
			this.comPortName = ComPortHandler.GetData();
		}
		public void GetBluetoothList()
		{
			var cli = new BluetoothClient();
			var peers = cli.DiscoverDevices();
			/* BluetoothDeviceInfo device = peers; //= ... select one of peer()...
			BluetoothAddress addr = device.DeviceAddress; */
		}
	}
}