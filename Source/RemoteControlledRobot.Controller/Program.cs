using System.Text;
using System.Threading;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide.UI;
using Gralin.NETMF.Nordic;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace RemoteControlledRobot.Controller
{
    public class Program
    {
        static Window window;
        private static NRF24L01Plus _nrf24L01Plus;

        public static void Main()
        {
            Window controllerWindow =
                GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.ControllerWindow));

            GlideTouch.Initialize();
            Glide.FitToScreen = true;

            Glide.MainWindow = controllerWindow;

            _nrf24L01Plus = new NRF24L01Plus();
            _nrf24L01Plus.Initialize(SPI.SPI_module.SPI2, (Cpu.Pin)75, (Cpu.Pin)48, (Cpu.Pin)26);

            var fezDominoAddress = Encoding.UTF8.GetBytes("COBRA");
            const int channel = 10;
            _nrf24L01Plus.Configure(fezDominoAddress, channel);

            _nrf24L01Plus.Enable();

            Thread.Sleep(Timeout.Infinite);
        }

        private static byte _lastMessageId;

        private static void SliderOnValueChangedEvent(object sender)
        {
            Debug.Print((sender as Slider).Value.ToString());

            _lastMessageId++;

            var fezMiniAddress = Encoding.UTF8.GetBytes("ROBOT");
            _nrf24L01Plus.SendTo(fezMiniAddress, new byte[] { 2, 0, 0, 0, _lastMessageId, 50 });
        }

        private static void ButtonOnPressEvent(object sender)
        {
            var fezMiniAddress = Encoding.UTF8.GetBytes("ROBOT");
            _nrf24L01Plus.SendTo(fezMiniAddress, new byte[] { 1, 2, 3 });
        }
    }
}
