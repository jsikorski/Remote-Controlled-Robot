using System.Text;
using Gralin.NETMF.Nordic;
using Microsoft.SPOT.Hardware;

namespace RemoteControlledRobot.Robot
{
    public class NrfController
    {
        public event NRF24L01Plus.OnDataRecievedHandler OnDataReceived;
        readonly NRF24L01Plus _nrf24L01Plus = new NRF24L01Plus();
        private byte[] _destinationAddress;

        public void Initialize(string sourceAddress, string destinationAddress)
        {
            _nrf24L01Plus.Initialize(SPI.SPI_module.SPI1, (Cpu.Pin)11, (Cpu.Pin)1, (Cpu.Pin)43);

            var fezMiniRobotAddress = Encoding.UTF8.GetBytes(sourceAddress);
            const int channel = 10;
            _nrf24L01Plus.Configure(fezMiniRobotAddress, channel);

            _destinationAddress = Encoding.UTF8.GetBytes(destinationAddress);
            
            _nrf24L01Plus.OnDataReceived += data => OnDataReceived.Invoke(data);
            _nrf24L01Plus.Enable();
        }

        public void SendObstacleDetected()
        {
            _nrf24L01Plus.SendTo(_destinationAddress, new byte[] {15});
        }
    }
}