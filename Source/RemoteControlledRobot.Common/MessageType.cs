namespace RemoteControlledRobot.Common
{
    public enum MessageType : byte
    {
        Start = 0,
        Stop = 1,
        Speed = 2,
        Direction = 3,
        Beep = 4
    }
}