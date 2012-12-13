using System;
using System.Text;
using Gralin.NETMF.Nordic;
using Microsoft.SPOT.Hardware;
using RemoteControlledRobot.Common;

namespace RemoteControlledRobot.Controller
{
    public class NrfPeerToPeerController
    {
        private const int ConnectionChannel = 10;
        private readonly NRF24L01Plus _nrf24L01Plus;
        private byte[] _destinationAddress;

        private int _lastVelocityMessageId;

        public NrfPeerToPeerController()
        {
            _nrf24L01Plus = new NRF24L01Plus();
        }

        public void Initialize(string sourceAddress, string destinationAddress)
        {
            _nrf24L01Plus.Initialize(SPI.SPI_module.SPI2, (Cpu.Pin)75, (Cpu.Pin)48, (Cpu.Pin)26);

            var fezDominoAddress = Encoding.UTF8.GetBytes(sourceAddress);
            _nrf24L01Plus.Configure(fezDominoAddress, ConnectionChannel);

            _destinationAddress = Encoding.UTF8.GetBytes(destinationAddress);

            _nrf24L01Plus.Enable();
        }

        public void SendSpeed(byte value)
        {
            SendMessage(value, ref _lastVelocityMessageId);
        }

        private void SendMessage(byte value, ref int lastMessageId)
        {
            byte[] messageId = IntConverter.ToBytes(lastMessageId);
            var message = new[] {(byte) MessageType.Speed, messageId[0], messageId[1], messageId[2], messageId[3], value};
            SendMessage(message);

            lastMessageId++;
        }

        private void SendMessage(byte[] message)
        {
            _nrf24L01Plus.SendTo(_destinationAddress, message);
        }

        public void SendBeep()
        {
            var message = new[] {(byte) MessageType.Beep};
            SendMessage(message);
        }
    }
}