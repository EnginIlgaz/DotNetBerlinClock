using BerlinClock.Interfaces;
using System;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        private IClock _berlinClock;
        public TimeConverter(IClock berlinClock)
        {
            _berlinClock = berlinClock;
        }
        public string ConvertTime(string aTime)
        {
            if (aTime == null)
                throw new ArgumentNullException("aTime");

            return _berlinClock.ConvertTime(aTime);
        }
    }
}
