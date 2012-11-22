using System;
using System.Text;
using System.Threading;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide.Geom;
using GHIElectronics.NETMF.Glide.UI;
using Gralin.NETMF.Nordic;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Button = GHIElectronics.NETMF.Glide.UI.Button;

namespace RemoteControlledRobot.Controller
{
    public class Program
    {
        static Window window;
        private static NRF24L01Plus _nrf24L01Plus;

        public static void Main()
        {
            // Create the Window XML string.
            string xml;
            xml = "<Glide Version=\"" + Glide.Version + "\">";
            xml += "<Window Name=\"window\" Width=\"320\" Height=\"240\" BackColor=\"FFFFFF\">";
            xml += "</Window>";
            xml += "</Glide>";

            GlideTouch.Initialize();

            // Resize any loaded Window to the LCD's size.
            Glide.FitToScreen = true;

            // Load the Window XML string.
            window = GlideLoader.LoadWindow(xml);

            //var button = new Button("Button", 130, 0, 0, 200, 32) { Text = "Click me!" }; ;
            //button.TapEvent += ButtonOnPressEvent;
            ////button.OnTouchDown(new TouchEventArgs(new Point(0, 0)));
            //window.AddChild(button);

            Slider slider = new Slider("Slider", 130, 10, 10, 300, 70);
            slider.ValueChangedEvent += SliderOnValueChangedEvent;
            window.AddChild(slider);

            var messageBox = new MessageBox("AAA", 128, 0, 0, 320, 240);
            window.AddChild(messageBox);

            // Assign the Window to MainWindow; rendering it to the LCD.
            Glide.MainWindow = window;

            _nrf24L01Plus = new NRF24L01Plus();
            _nrf24L01Plus.Initialize(SPI.SPI_module.SPI2, (Cpu.Pin)75, (Cpu.Pin)48, (Cpu.Pin)26);

            var fezDominoAddress = Encoding.UTF8.GetBytes("COBRA");
            const int channel = 10;
            _nrf24L01Plus.Configure(fezDominoAddress, channel);

            _nrf24L01Plus.Enable();

            Thread.Sleep(Timeout.Infinite);
        }

        private static void SliderOnValueChangedEvent(object sender)
        {
            Debug.Print((sender as Slider).Value.ToString());
        }

        private static void ButtonOnPressEvent(object sender)
        {
            var fezMiniAddress = Encoding.UTF8.GetBytes("MINI.");
            _nrf24L01Plus.SendTo(fezMiniAddress, new byte[] { 1, 2, 3 });
        }
    }
}
