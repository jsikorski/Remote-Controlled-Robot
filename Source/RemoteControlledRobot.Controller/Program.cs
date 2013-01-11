using System.Runtime.CompilerServices;
using System.Threading;
using GHIElectronics.NETMF.Glide;
using GHIElectronics.NETMF.Glide.Display;
using GHIElectronics.NETMF.Glide.UI;
using Button = GHIElectronics.NETMF.Glide.UI.Button;

namespace RemoteControlledRobot.Controller
{
    public class Program
    {
        private static readonly NrfPeerToPeerController NrfController = new NrfPeerToPeerController();
        private static Slider DirectionSlider { get; set; }
        private static Slider SpeedSlider { get; set; }

        public static void Main()
        {
            Window controllerWindow =
                GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.ControllerWindow));

            GlideTouch.Initialize();
            Glide.FitToScreen = true;

            Glide.MainWindow = controllerWindow;

            DirectionSlider = (Slider) controllerWindow.GetChildByName("DirectionSlider");
            DirectionSlider.ValueChangedEvent += UpdateRobotDirection;

            SpeedSlider = (Slider) controllerWindow.GetChildByName("SpeedSlider");
            SpeedSlider.ValueChangedEvent += UpdateRobotSpeed;

            var beepButton = (Button) controllerWindow.GetChildByName("BeepButton");
            beepButton.TapEvent += Beep;

            var stopButton = (Button) controllerWindow.GetChildByName("StopButton");
            stopButton.TapEvent += StopRobot;

            NrfController.Initialize("CONTR", "ROBOT");

            Thread.Sleep(Timeout.Infinite);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void UpdateRobotDirection(object sender)
        {
            var slider = (Slider)sender;
            NrfController.SendDirection((byte) slider.Value);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void UpdateRobotSpeed(object sender)
        {
            var slider = (Slider) sender;
            NrfController.SendSpeed((byte) slider.Value);
        }

        private static void Beep(object sender)
        {
            NrfController.SendBeep();
        }

        private static void StopRobot(object sender)
        {
            SpeedSlider.Value = 100;
            SpeedSlider.Invalidate();
        }
    }
}
