using NUnit.Framework;
using RemoteControlledRobot.Robot.Utils;

namespace RemoteControlledRobot.Tests
{
    public class IntConverterTests
    {
        [Test]
        public void valid_int_values_are_correctly_converter()
        {
            int value = 0;
            var expected = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            var actual = IntConverter.ToBytes(value);
            Assert.AreEqual(expected, actual);

            value = 1536;
            expected = new byte[] { 0x00, 0x00, 0x06, 0x00 };
            actual = IntConverter.ToBytes(value);
            Assert.AreEqual(expected, actual);

            value = 98537;
            expected = new byte[] { 0x00, 0x01, 0x80, 0xE9 };
            actual = IntConverter.ToBytes(value);
            Assert.AreEqual(expected, actual);

            value = 536200;
            expected = new byte[] { 0x00, 0x08, 0x2E, 0x88 };
            actual = IntConverter.ToBytes(value);
            Assert.AreEqual(expected, actual);
        }
    }
}