namespace BTConnector
{
    static class ComPortHandler
    {
        public static string GetMatchingPort()
        {

            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            foreach (var portName in ports)
            {
                if (portName.Contains("SPP")) return portName;
            }
            throw new System.Exception("could not find port");
        }
        public static string GetData()
        {
            string output;
            try
            {
                output = ComPortHandler.GetMatchingPort();
            }
            catch (System.Exception)
            {
                output = "could not find port";
            }
            return output;
        }

    }
}