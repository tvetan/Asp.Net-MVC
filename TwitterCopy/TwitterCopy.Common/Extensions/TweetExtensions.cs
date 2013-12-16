using System;
using System.Linq;
using TwitterCopy.Models;

namespace TwitterCopy.Common.Extensions
{
    public static class TweetExtensions
    {
        public static string FormatTimeSinceCreated(this Tweet tweet)
        {
            DateTime today = DateTime.Now;
            TimeSpan span = today - tweet.CreatedOn;
            if (span.TotalHours > 24)
            {
                return span.Days.ToString() + "d";
            }

            if (span.TotalMinutes > 60)
            {
                return span.Hours.ToString() + "h";
            }

            if (span.TotalSeconds > 60)
            {
                return span.Minutes.ToString() + "m";
            }

            return span.Seconds.ToString() + "s";            
        }
    }
}
