using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.WordCountCalculator.Test
{
    [TestFixture]
    public class WordCountCalculatorTests
    {
        private Mock<ITextSplitter> mockTextSplitter;
        private Mock<IPrimeNumberChecker> mockPrimeNumberChecker;
        private Mock<IWordCounter> mockWordCounter;

        [SetUp]
        public void Initialize()
        {
            mockTextSplitter = new Mock<ITextSplitter>(MockBehavior.Strict);
            mockPrimeNumberChecker = new Mock<IPrimeNumberChecker>(MockBehavior.Strict);
            mockWordCounter = new Mock<IWordCounter>(MockBehavior.Strict);
        }

        [TearDown]
        public void TearDown()
        {
            mockTextSplitter.VerifyAll();
            mockPrimeNumberChecker.VerifyAll();
            mockTextSplitter.VerifyAll();
        }

        [Test]
        public void CalculateAndUpdateWordCountCorrectlyCalculatesAndUpdatesCounterTest()
        {
            // Arrange

            var input = Guid.NewGuid().ToString();
            var wordsList = new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            mockTextSplitter.Setup(a => a.SanitizeAndSplitIntoWords(input)).Returns(wordsList);

            var wordCount = new Dictionary<string, int>
            {
                { wordsList[0], 4 },
                { wordsList[1], 5 },
            };

            mockWordCounter.Setup(a => a.CountWords(wordsList)).Returns(wordCount);

            mockPrimeNumberChecker.Setup(a => a.IsPrime(wordCount[wordCount.Keys.ElementAt(0)])).Returns(false);
            mockPrimeNumberChecker.Setup(a => a.IsPrime(wordCount[wordCount.Keys.ElementAt(1)])).Returns(true);

            // Act
            var calculator = new WordCountCalculator(mockTextSplitter.Object, mockWordCounter.Object, mockPrimeNumberChecker.Object);
            calculator.CalculateAndUpdateWordCount(input);
            var result = calculator.CountedWords.ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            var countedWord1 = result.Single(r => r.Word.Equals(wordsList[0], StringComparison.OrdinalIgnoreCase));
            Assert.AreEqual(wordCount[wordsList[0]], countedWord1.Count);
            Assert.IsFalse(countedWord1.IsPrime);

            var countedWord2 = result.Single(r => r.Word.Equals(wordsList[1], StringComparison.OrdinalIgnoreCase));
            Assert.AreEqual(wordCount[wordsList[1]], countedWord2.Count);
            Assert.IsTrue(countedWord2.IsPrime);

            // 2nd pass - Call calculate again to get an updated word count
            var input2 = Guid.NewGuid().ToString();
            var wordsList2 = new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            mockTextSplitter.Setup(a => a.SanitizeAndSplitIntoWords(input2)).Returns(wordsList2);

            var wordCount2 = new Dictionary<string, int>
            {
                { wordsList[0], 4 },
                { wordsList2[0], 3 },
            };

            mockWordCounter.Setup(a => a.CountWords(wordsList2)).Returns(wordCount2);

            mockPrimeNumberChecker.Setup(a => a.IsPrime(8)).Returns(false);
            mockPrimeNumberChecker.Setup(a => a.IsPrime(wordCount2[wordCount2.Keys.ElementAt(1)])).Returns(true);
            calculator.CalculateAndUpdateWordCount(input2);
            var result2 = calculator.CountedWords.ToList();

            Assert.AreEqual(3, result2.Count);
            Assert.AreEqual(8, result[0].Count);
            Assert.IsFalse(result[0].IsPrime);

            Assert.AreEqual(wordsList2[0], result2[2].Word);
            Assert.AreEqual(3, result2[2].Count);
            Assert.IsTrue(result2[2].IsPrime);
        }
    }
}