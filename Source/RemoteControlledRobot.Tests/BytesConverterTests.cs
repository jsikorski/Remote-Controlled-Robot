using NUnit.Framework;
using RemoteControlledRobot.Robot.Utils;

namespace RemoteControlledRobot.Tests
{
    public class BytesConverterTests
    {
        [Test]
         public void correct_bytes_array_is_correctly_converted()
         {
             var bytes = new byte[] { 0x00, 0x00, 0x00, 0x00 };
             var expected = 0;
             var actual = BytesConverter.ToInt32(bytes);
             Assert.AreEqual(expected, actual);

             bytes = new byte[] { 0x00, 0x00, 0x06, 0x00 };
             expected = 1536;
             actual = BytesConverter.ToInt32(bytes);
             Assert.AreEqual(expected, actual);

             bytes = new byte[] { 0x00, 0x01, 0x80, 0xE9 };
             expected = 98537;
             actual = BytesConverter.ToInt32(bytes);
             Assert.AreEqual(expected, actual);

             bytes = new byte[] { 0x00, 0x08, 0x2E, 0x88 };
             expected = 536200;
             actual = BytesConverter.ToInt32(bytes);
             Assert.AreEqual(expected, actual);
         }
    }
}