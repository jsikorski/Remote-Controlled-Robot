using System.Threading;

namespace RemoteControlledRobot.Robot
{
    public class Program
    {
        public static void Main()
        {
            var robotEventAggregator = new RobotEventAggregator();

            var nrfController = new NrfController();
            var messagesController = new MessagesController(robotEventAggregator, nrfController);
            var robot = new Robot(robotEventAggregator, nrfController);

            robot.Start();
            messagesController.Start();

            nrfController.Initialize("ROBOT", "CONTR");

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
