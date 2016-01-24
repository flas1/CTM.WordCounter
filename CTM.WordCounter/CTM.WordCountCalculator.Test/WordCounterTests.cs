using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace CTM.WordCountCalculator.Test
{
    [TestFixture]
    public class WordCounterTests
    {
        [Test]
        public void CountWordsReturnCorrectCountIgnoringCaseTest()
        {
            // Arrange
            var word1 = Guid.NewGuid().ToString();
            var word2 = Guid.NewGuid().ToString();
            var word3 = "ABCd";
            var word3AllLowercase = "abcd";

            var words = new List<string>
            {
                word1,
                word2,
                word3,
                word3AllLowercase,
                word2,
                word2
            };

            // Act
            var counter = new WordCounter();
            var result = counter.CountWords(words);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result[word1]);
            Assert.AreEqual(3, result[word2]);
            Assert.AreEqual(2, result[word3AllLowercase]);
            Assert.IsFalse(result.ContainsKey(word3));
        }
    }
}
