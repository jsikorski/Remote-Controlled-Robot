using RemoteControlledRobot.Common;

namespace RemoteControlledRobot.Robot.MessagesHandlers
{
    public class BeepMessagesHandler : IMessagesHandler
    {
        private readonly RobotEventAggregator _robotEventAggregator;

        public MessageType MessageType
        {
            get { return MessageType.Beep; }
        }

        public BeepMessagesHandler(RobotEventAggregator robotEventAggregator)
        {
            _robotEventAggregator = robotEventAggregator;
        }

        public void Handle(byte[] data)
        {
            _robotEventAggregator.TriggerBeep();
        }
    }
}