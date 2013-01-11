using System;
using System.Collections;
using RemoteControlledRobot.Common;
using RemoteControlledRobot.Robot.MessagesHandlers;
using VikingErik.NetMF.MicroLinq;

namespace RemoteControlledRobot.Robot
{
    public class MessagesController
    {
        private readonly NrfController _nrfController;
        private readonly IEnumerable _messagesHandlers;

        public MessagesController(RobotEventAggregator robotEventAggregator, NrfController nrfController)
        {
            _nrfController = nrfController;
            _messagesHandlers = new ArrayList
                {
                    new SpeedMessagesHandler(robotEventAggregator),
                    new DirectionMessagesHandler(robotEventAggregator),
                    new BeepMessagesHandler(robotEventAggregator)
                };
        }

        public void Start()
        {
            _nrfController.OnDataReceived += ProcessMessage;
        }

        private void ProcessMessage(byte[] data)
        {
            var messageType = (MessageType) data[0];
            var messageHandler = GetMessageHandler(messageType);
            var messageData = ExtractMessageData(data);

            // Handle raw data - without message type byte
            ((IMessagesHandler) messageHandler).Handle(messageData);
        }

        private object GetMessageHandler(MessageType messageType)
        {
            var messageHandler =  _messagesHandlers.FirstOrDefault(
                handler => ((IMessagesHandler) handler).MessageType == messageType);

            if (messageHandler == null)
                throw new ArgumentException("Invalid message type.");

            return messageHandler;
        }

        private static byte[] ExtractMessageData(byte[] data)
        {
            int messageDataLength = data.Length - 1;
            var messageData = new byte[messageDataLength];
            Array.Copy(data, 1, messageData, 0, messageDataLength);
            return messageData;
        }
    }
}