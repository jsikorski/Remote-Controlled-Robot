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
        private float _currentLeft = 1.0f;
        private float _currentRight = 1.0f;

        public Robot(RobotEventAggregator robotEventAggregator)
        {
            _robotEventAggregator = robotEventAggregator;
        }

        public void Start()
        {
            _robotEventAggregator.OnSpeedUpdated += UpdateSpeed;
            _robotEventAggregator.OnDirectionUpdated += UpdateDirection;
            _robotEventAggregator.OnBeep += Beep;

            SignalStarted();
        }

        private void UpdateSpeed(int speed)
        {
            // speed [0, 100]
            _currentSpeed = speed;
            UpdateMovement();
        }

        private void UpdateDirection(float left, float right)
        {
            // left, right [-1, 1]
            _currentLeft = left;
            _currentRight = right;
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            // Robot recieves values from -100 to 100
            var speedLeft = (sbyte)(_currentSpeed * _currentLeft);
            var speedRight = (sbyte)(_currentSpeed * _currentRight);

            if (speedLeft > 100 || speedLeft < -100 || speedRight > 100 || speedRight < -100)
            {
                RobotPiezoController.Beep(10000);
                speedLeft = 0;
                speedRight = 0;
            }

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