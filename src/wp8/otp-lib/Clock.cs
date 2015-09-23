using System;

namespace AeroGear.OTP
{
    /// <summary>
    /// A clock that ticks every 30 seconds a different tick
    /// </summary>
    public class Clock
    {

        private readonly int interval;

        public Clock()
        {
            interval = 30;
        }

        public Clock(int interval)
        {
            this.interval = interval;
        }

        public virtual long CurrentInterval
        {
            get
            {
                long currentTimeSeconds = (long) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds / 1000;
                return currentTimeSeconds / interval;
            }
        }
    }
}
