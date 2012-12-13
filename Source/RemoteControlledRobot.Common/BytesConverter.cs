namespace RemoteControlledRobot.Common
{
    public static class BytesConverter
    {
         public static int ToInt32(byte[] bytes)
         {
             return ((int)bytes[0] << 24) + ((int)bytes[1] << 16) + ((int)bytes[2] << 8) + ((int)bytes[3]);
         }
    }
}