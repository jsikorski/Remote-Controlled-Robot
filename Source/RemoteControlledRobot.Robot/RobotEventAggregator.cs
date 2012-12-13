using Gralin.NETMF.Nordic;

namespace RemoteControlledRobot.Robot
{
    public delegate void RobotSpeedUpdatedHandler(int speed);
    public delegate void RobotDirectionUpdatedHandler(float left, float right);

    public class RobotEventAggregator
    {
        public event RobotSpeedUpdatedHandler OnSpeedUpdated;
        public event RobotDirectionUpdatedHandler OnDirectionUpdated;

        public void TriggerUpdateSpeed(int speed)
        {
            OnSpeedUpdated(speed);
        }

        public void TriggerUpdateDirection(float left, float right)
        {
            OnDirectionUpdated(left, right);
        }
    }
}