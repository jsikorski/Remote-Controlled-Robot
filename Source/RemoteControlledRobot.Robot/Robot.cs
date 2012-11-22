using System;
using GHIElectronics.NETMF.FEZ;
using Microsoft.SPOT.Hardware;

namespace RemoteControlledRobot.Robot
{
    public class Robot
    {
        private static readonly OutputPort Led = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.Di4, true);

        private readonly MessagesReceiver _messagesReceiver;
        private readonly MessagesController _messagesController;

        public Robot()
        {
            _messagesReceiver = new MessagesReceiver();
            _messagesController = new MessagesController();
        }

        public void Start()
        {
            _messagesReceiver.OnDataReceived += data => _messagesController.ProcessMessage(data);
            _messagesReceiver.Initialize();
            SignalStarted();
        }

        private static void SignalStarted()
        {
            Led.Write(false);
        }
    }
}