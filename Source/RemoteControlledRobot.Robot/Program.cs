using System;
using System.Text;
using System.Threading;
using GHIElectronics.NETMF.FEZ;
using Gralin.NETMF.Nordic;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using RemoteControlledRobot.RobotApi;

namespace RemoteControlledRobot.Robot
{
    public class Program
    {
        private static readonly OutputPort Led = new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di4, true);
        private static FEZ_Components.ReflectiveSensor LeftSensor =
           new FEZ_Components.ReflectiveSensor(FEZ_Pin.AnalogIn.An7, 20);
        private static FEZ_Components.ReflectiveSensor RightSensor =
            new FEZ_Components.ReflectiveSensor(FEZ_Pin.AnalogIn.An6, 20);

        public static void Main()
        {
            //RobotPiezoController.Beep(1000);

            //while (true)
            //{
            //    if (LeftSensor.GetState() == FEZ_Components.ReflectiveSensor.DetectingState.ReflectionDeteced ||
            //        RightSensor.GetState() == FEZ_Components.ReflectiveSensor.DetectingState.ReflectionDeteced)
            //    {
            //        RobotPiezoController.Beep(1000);
            //        Thread.Sleep(1000);
            //    }
            //}


            //    RobotMovementController.Move(100, 0);
        //    Thread.Sleep(2500);
        //    RobotMovementController.Stop();
        

        //    var nrf24L01Plus = new NRF24L01Plus();
        //    nrf24L01Plus.Initialize(SPI.SPI_module.SPI1, (Cpu.Pin)11, (Cpu.Pin)1, (Cpu.Pin)43);

        //    var fezMiniAddress = Encoding.UTF8.GetBytes("MINI.");
        //    const int channel = 10;
        //    nrf24L01Plus.Configure(fezMiniAddress, channel);

        //    nrf24L01Plus.OnDataReceived += Nrf24L01PlusOnOnDataReceived;
        //    nrf24L01Plus.Enable();

            var robot = new Robot();
            robot.Start();

            Led.Write(false);
            Thread.Sleep(Timeout.Infinite);
        }

        //private static void Nrf24L01PlusOnOnDataReceived(byte[] data)
        //{
        //    Debug.Print("Recieved...");
        //    RobotPiezoController.Beep(1000);
        //    RobotMovementController.Move(100, -100);
        //    Thread.Sleep(500);
        //}
    }
}
