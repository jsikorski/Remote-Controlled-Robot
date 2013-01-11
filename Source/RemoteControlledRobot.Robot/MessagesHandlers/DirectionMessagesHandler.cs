using System;
using RemoteControlledRobot.Common;

namespace RemoteControlledRobot.Robot.MessagesHandlers
{
    public class DirectionMessagesHandler : IMessagesHandler
    {
        private readonly RobotEventAggregator _robotEventAggregator;
        private int _lastMessageIndex;

        public DirectionMessagesHandler(RobotEventAggregator robotEventAggregator)
        {
            _robotEventAggregator = robotEventAggregator;
        }

        public MessageType MessageType
        {
            get { return MessageType.Direction; }
        }

        public void Handle(byte[] data)
        {
            int messageIndex = GetMessageIndex(data);
            if (messageIndex < _lastMessageIndex)
                return;

            // [-1, 1]
            float direction = (data[4] - 100) / 100.0f;

            float left;
            float right;

            if (direction < 0)
            {
                left = 1.0f + direction;
                right = 1.0f;
            }
            else
            {
                left = 1.0f;
                right = 1.0f - direction;
            }

            _robotEventAggregator.TriggerUpdateDirection(left, right);

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