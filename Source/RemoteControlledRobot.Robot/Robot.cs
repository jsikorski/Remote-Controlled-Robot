using System;
using GHIElectronics.NETMF.FEZ;
using Microsoft.SPOT.Hardware;
using RemoteControlledRobot.RobotApi;

namespace RemoteControlledRobot.Robot
{
    public class Robot
    {
        private const int DefaultPiezzoBeepDuration = 1000;
        private static readonly OutputPort Led = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.Di4, true);
        private readonly RobotEventAggregator _robotEventAggregator;

        private int _currentSpeed;

        public Robot(RobotEventAggregator robotEventAggregator)
        {
            _robotEventAggregator = robotEventAggregator;
        }

        public void Start()
        {
            _robotEventAggregator.OnSpeedUpdated += UpdateSpeed;
            _robotEventAggregator.OnBeep += Beep;

            SignalStarted();
        }

        private void UpdateSpeed(int speed)
        {
            _currentSpeed = speed;
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            var speedLeft = (sbyte)_currentSpeed;
            var speedRight = (sbyte)_currentSpeed;
            RobotMovementController.Move(speedLeft, speedRight);
        }

        private void Beep()
        {
            RobotPiezoController.Beep(DefaultPiezzoBeepDuration);
        }

        private static void SignalStarted()
        {
            Led.Write(false);
        }
    }
}