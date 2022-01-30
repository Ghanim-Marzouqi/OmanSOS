namespace OmanSOS.Core.Helpers;

public class Timeout : System.Timers.Timer
{
    public Timeout(Action action, double delay)
    {
        AutoReset = false;
        Interval = delay;
        Elapsed += (sender, args) => action();
        Start();
    }
}
