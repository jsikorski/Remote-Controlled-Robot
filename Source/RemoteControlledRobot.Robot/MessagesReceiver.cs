using System.Text;
using Gralin.NETMF.Nordic;
using Microsoft.SPOT.Hardware;

namespace RemoteControlledRobot.Robot
{
    public class MessagesReceiver
    {
        readonly NRF24L01Plus _nrf24L01Plus = new NRF24L01Plus();

        public void Initialize()
        {
            _nrf24L01Plus.Initialize(SPI.SPI_module.SPI1, (Cpu.Pin)11, (Cpu.Pin)1, (Cpu.Pin)43);

            var fezMiniRobotAddress = Encoding.UTF8.GetBytes("ROBOT");
            const int channel = 10;
            _nrf24L01Plus.Configure(fezMiniRobotAddress, channel);

            _nrf24L01Plus.OnDataReceived += data => OnDataReceived.Invoke(data);
            _nrf24L01Plus.Enable();
        }

        public event NRF24L01Plus.OnDataRecievedHandler OnDataReceived;
    }
}