using System;
using System.Threading;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace RemoteControlledRobot.RobotApi
{
    public class RobotPiezoController
    {
        private const int DefaultPiezoFrequency = 8200;

        private static readonly OutputCompare OutputCompare = new OutputCompare((Cpu.Pin)FEZ_Pin.Digital.An0, true, 5);
        private static readonly uint[] Timings = new uint[2];

        public static void Beep(int durationMSec, int freq = DefaultPiezoFrequency)
        {
            if (freq > 10000)
                throw new Exception("Frequency is too high");

            Timings[0] = Timings[1] = (uint)((long)1000000 / (freq / 2));
            OutputCompare.Set(true, Timings, 0, 2, true);
            Thread.Sleep(durationMSec);
            OutputCompare.Set(false);
        }
    }
}