using Gralin.NETMF.Nordic;

namespace RemoteControlledRobot.Robot
{
    public delegate void RobotSpeedUpdatedHandler(int speed);
    public delegate void RobotDirectionUpdatedHandler(float left, float right);
    public delegate void RobotBeepHandler();

    public class RobotEventAggregator
    {
        public event RobotSpeedUpdatedHandler OnSpeedUpdated;
        public event RobotDirectionUpdatedHandler OnDirectionUpdated;
        public event RobotBeepHandler OnBeep;

        public void TriggerUpdateSpeed(int speed)
        {
            OnSpeedUpdated(speed);
        }

        public void TriggerUpdateDirection(float left, float right)
        {
            OnDirectionUpdated(left, right);
        }

        public void TriggerBeep()
        {
            OnBeep();
        }
    }
}