using RemoteControlledRobot.Common;

namespace RemoteControlledRobot.Robot.MessagesHandlers
{
    public interface IMessagesHandler
    {
        MessageType MessageType { get; }
        void Handle(byte[] data);
    }
}