    namespace RemoteControlledRobot.Common
{
    public static class IntConverter
    {
         public static byte[] ToBytes(int value)
         {
             return new[]
                 {
                     (byte) (value >> 24 & 0xff),
                     (byte) (value >> 16 & 0xff),
                     (byte) (value >> 8 & 0xff),
                     (byte) (value & 0xff)
                 };
         }
    }
}