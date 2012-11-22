using System;
using System.Collections;
using RemoteControlledRobot.Robot.MessagesHandlers;
using VikingErik.NetMF.MicroLinq;

namespace RemoteControlledRobot.Robot
{
    public class MessagesController
    {
        private readonly IEnumerable _messagesHandlers;

        public MessagesController()
        {
            _messagesHandlers = new ArrayList();
        }

        public void ProcessMessage(byte[] data)
        {
            var messageType = (MessageType) data[0];
            var messageHandler = GetMessageHandler(messageType);
            var messageData = ExtractMessageData(data);

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