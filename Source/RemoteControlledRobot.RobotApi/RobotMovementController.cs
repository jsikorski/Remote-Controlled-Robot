using System;
using System.Threading;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Math = System.Math;

namespace RemoteControlledRobot.RobotApi
{
    public static class RobotMovementController
    {
        private static readonly PWM Pwm1 = new PWM((PWM.Pin)FEZ_Pin.PWM.Di5);
        private static readonly OutputPort Dir1 = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.Di3, false);

        private static readonly PWM Pwm2 = new PWM((PWM.Pin)FEZ_Pin.PWM.Di6);
        private static readonly OutputPort Dir2 = new OutputPort((Cpu.Pin)FEZ_Pin.Digital.Di9, false);

        private static sbyte _lastSpeedLeft;
        private static sbyte _lastSpeedRight;

        public static void Move(sbyte speedLeft, sbyte speedRight)
        {
            if (speedLeft > 100 || speedLeft < -100 || speedRight > 100 || speedRight < -100)
                throw new ArgumentException();

            _lastSpeedLeft = speedLeft;
            _lastSpeedRight = speedRight;

            if (speedLeft < 0)
            {
                Dir1.Write(true);
                Pwm1.Set(1000, (byte)(100 - Math.Abs(speedLeft)));
            }
            else
            {
                Dir1.Write(false);
                Pwm1.Set(1000, (byte)speedLeft);
            }

            if (speedRight < 0)
            {
                Dir2.Write(true);
                Pwm2.Set(1000, (byte)(100 - Math.Abs(speedRight)));

            }
            else
            {
                Dir2.Write(false);
                Pwm2.Set(1000, (byte)speedRight);
            }
        }

        public static void MoveRamp(sbyte speedLeft, sbyte speedRight, byte rampingDelay)
        {
            sbyte tempSpeedLeft = _lastSpeedLeft;
            sbyte tempSpeedRight = _lastSpeedRight;

            while ((speedLeft != tempSpeedLeft) ||
                    (speedRight != tempSpeedRight))
            {
                if (tempSpeedLeft > speedLeft)
                    tempSpeedLeft--;
                if (tempSpeedLeft < speedLeft)
                    tempSpeedLeft++;

                if (tempSpeedRight > speedRight)
                    tempSpeedRight--;
                if (tempSpeedRight < speedRight)
                    tempSpeedRight++;

                Move(tempSpeedLeft, tempSpeedRight);
                Thread.Sleep(rampingDelay);
            }
        }

        public static void Stop()
        {
            Move(0, 0);
        }
    }
}
