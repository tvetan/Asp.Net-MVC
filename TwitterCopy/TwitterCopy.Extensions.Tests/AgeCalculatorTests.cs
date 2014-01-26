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
        public void AgeShouldReturnProperAgeIfBirthDayBefore10Years()
        {
            this.CreateBirthdayAndAssert(-10, 0, 0, 10);
        }

        [TestMethod]
        public void AgeShouldReturnProperAgeIfBirthDayBefore10YearsAnd1Day()
        {
            this.CreateBirthdayAndAssert(-10, -1, 0, 10);
        }

        [TestMethod]
        public void AgeShouldReturnProperAgeIfBirthDayBefore10YearsAndAfter1Day()
        {
            this.CreateBirthdayAndAssert(-10, 1, 0, 9);
        }

        [TestMethod]
        public void AgeShouldReturnProperAgeIfBirthDayBefore10Years1MonthAndAfter1Day()
        {
            this.CreateBirthdayAndAssert(-10, -1, 0, 10);
        }

        [TestMethod]
        public void AgeShouldReturnProperAgeIfBirthDayBefore10YearsAfter1Day1Month()
        {
            this.CreateBirthdayAndAssert(-10, -1, 1, 10);
        }

        [TestMethod]
        public void AgeShouldReturnProperAgeIfBirthDayToday()
        {
            this.CreateBirthdayAndAssert(0, 0, 0, 0);
        }

        private void CreateBirthdayAndAssert(int year, int month, int day, byte expectedAge)
        {
            DateTime? birthDate = DateTime.Now.AddYears(year).AddMonths(month).AddDays(day);

            byte? age = AgeCalculator.Age(birthDate);

            Assert.AreEqual(expectedAge, age);
        }
    }
}