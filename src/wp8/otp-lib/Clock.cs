using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroGear.OTP
{

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
                long currentTimeSeconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond / 1000;
                return currentTimeSeconds / interval;
            }
        }
    }
}
