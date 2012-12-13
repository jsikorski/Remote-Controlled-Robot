using System;
using System.Runtime.CompilerServices;
using RemoteControlledRobot.Common;

namespace RemoteControlledRobot.Robot.MessagesHandlers
{
    public class SpeedMessagesHandler : IMessagesHandler
    {
        private readonly RobotEventAggregator _robotEventAggregator;

        private int _lastMessageIndex;

        public SpeedMessagesHandler(RobotEventAggregator robotEventAggregator)
        {
            _robotEventAggregator = robotEventAggregator;
        }

        public MessageType MessageType
        {
            get { return MessageType.Speed; }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Handle(byte[] data)
        {
            int messageIndex = GetMessageIndex(data);
            if (messageIndex < _lastMessageIndex)
                return;

            int speed = data[4] - 100;
            _robotEventAggregator.TriggerUpdateSpeed(speed);

            _lastMessageIndex = messageIndex;
        }

        private int GetMessageIndex(byte[] data)
        {
            var messageIndexData = new byte[4];
            Array.Copy(data, messageIndexData, 4);
            return BytesConverter.ToInt32(messageIndexData);
        }
    }
}