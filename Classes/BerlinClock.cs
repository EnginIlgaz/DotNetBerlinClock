using BerlinClock.Interfaces;
using System;
using System.Text;

namespace BerlinClock.Classes
{
    public class BerlinClock : IClock
    {
        public string ConvertTime(string aTime)
        {
            var time = ParseTime(aTime);
            
            var state = GetBerlinClockState(time);

            return state;
        }

        private string SecondsLine(int seconds)
        {
            return seconds % 2 == 0 ? "Y" : "O";
        }

        private string HoursTopLine(int hours)
        {
            int noOfLights = hours / 5;
            return HoursState(noOfLights);
        }

        private string HoursBottomLine(int hours)
        {
            int noOfLights = hours % 5;
            return HoursState(noOfLights);
        }

        private string MinutesTopLine(int minutes)
        {
            int noOfLights = minutes / 5;
            return "YYRYYRYYRYY".Substring(0, noOfLights) + new string('O', 11 - noOfLights);
        }

        private string MinutesBottomLine(int minutes)
        {
            int noOfLights = minutes % 5;
            return new string('Y', noOfLights).Insert(noOfLights, new string('O', 4 - noOfLights));
        }

        private string GetBerlinClockState(TimeSpan time)
        {
            var outputString = new StringBuilder();

            outputString.AppendLine(SecondsLine(time.Seconds));
            outputString.AppendLine(HoursTopLine(GetHourFrom(time)));
            outputString.AppendLine(HoursBottomLine(GetHourFrom(time)));
            outputString.AppendLine(MinutesTopLine(time.Minutes));
            outputString.Append(MinutesBottomLine(time.Minutes));

            return outputString.ToString();
        }

        private TimeSpan ParseTime(string time)
        {
            var components = time.Split(':');

            int hh, mm, ss;

            if (!int.TryParse(components[0], out hh))
                throw new ArgumentException("time", "Invalid hours value");

            if (!int.TryParse(components[1], out mm))
                throw new ArgumentException("time", "Invalid minutes value");

            if (!int.TryParse(components[2], out ss))
                throw new ArgumentException("time", "Invalid seconds value");

            TimeSpan parsedTime = new TimeSpan(hh, mm, ss);

            return parsedTime;
        }

        private string HoursState(int noOfLights)
        {
            return new string('R', noOfLights).Insert(noOfLights, new string('O', 4 - noOfLights));
        }

        private int GetHourFrom(TimeSpan time)
        {
            return TimeSpan.FromDays(1) - time == TimeSpan.Zero ? 24 : time.Hours;
        }
    }
}
