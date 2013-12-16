﻿using System;
using System.Linq;

namespace TwitterCopy.Common
{
    public class AgeCalculator
    {
        public static byte? Age(DateTime? date)
        {
            if (!date.HasValue)
            {
                return null;
            }

            var birthDate = date.Value;
            var now = DateTime.Now;

            var age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
            {
                age--;
            }

            return (byte)age;
        }
    }
}