using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using CTM.WordCounter;

namespace CTM.WordCountCalculator.Test
{
    [TestFixture]
    public class PrimeNumberCheckerTests
    {
        [Test]
        public void IsPrimeReturnsTrueForPrimeNumbersTest([Values(2, 3, 5,7, 5279, 67867979, 982451653)]int numberToCheck)
        {
            var checker = new PrimeNumberChecker();
            Assert.IsTrue(checker.IsPrime(numberToCheck));
        }

        [Test]
        public void IsPrimeReturnsFalseForNonPrimeNumbersTest([Values(-1, 0, 1, 4, 15, 21, 30, 333, 34535343)]int numberToCheck)
        {
            var checker = new PrimeNumberChecker();
            Assert.IsTrue(checker.IsPrime(numberToCheck));
        }
    }
}
