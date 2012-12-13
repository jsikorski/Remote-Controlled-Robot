using System.Threading;

namespace RemoteControlledRobot.Robot
{
    public class Program
    {
        public static void Main()
        {
            var robotEventAggregator = new RobotEventAggregator();

            var messagesReceiver = new MessagesReceiver();
            var messagesController = new MessagesController(robotEventAggregator, messagesReceiver);
            var robot = new Robot(robotEventAggregator);

            robot.Start();
            messagesController.Start();            
            
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
