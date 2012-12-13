using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide.UI;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace RemoteControlledRobot.Controller
{
    public class Program
    {
        private static readonly NrfPeerToPeerController NrfController = new NrfPeerToPeerController();

        public static void Main()
        {
            Window controllerWindow =
                GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.ControllerWindow));

            GlideTouch.Initialize();
            Glide.FitToScreen = true;

            Glide.MainWindow = controllerWindow;

            var speedSlider = (Slider) controllerWindow.GetChildByName("SpeedSlider");
            speedSlider.ValueChangedEvent += UpdateRobotSpeed;

            NrfController.Initialize("CONTR", "ROBOT");

            Thread.Sleep(Timeout.Infinite);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void UpdateRobotSpeed(object sender)
        {
            var slider = (Slider) sender;
            NrfController.SendSpeed((byte) slider.Value);
        }
    }
}
