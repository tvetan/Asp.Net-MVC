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
        public void ValidDateShouldReturnValidAge()
        {
            DateTime? birthDate = new DateTime(2005, 3, 3);

            byte? age = AgeCalculator.Age(birthDate);

            Assert.IsNotNull(age);
        }
    }
}