namespace TwitterCopy.Extensions.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TwitterCopy.Common;

    [TestClass]
    public class AgeCalculatorTests
    {
        [TestMethod]
        public void NullDateShouldReturnNull()
        {
            DateTime? birthDate = null;

            byte? age = AgeCalculator.Age(birthDate);

            Assert.IsNull(age);
        }

        [TestMethod]
        public void AgeShouldReturnProperAgeIfDayAndMonthAreTheSame()
        {
            DateTime? birthDate = DateTime.Now.AddYears(-10);

            byte? age = AgeCalculator.Age(birthDate);
            byte expectedAge = 10;

            Assert.AreEqual(expectedAge, age);
        }
    }
}