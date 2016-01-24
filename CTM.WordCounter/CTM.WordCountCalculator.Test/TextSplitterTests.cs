using CTM.WordCounter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.WordCountCalculator.Test
{
    [TestFixture]
    public class TextSplitterTests
    {
        [Test]
        public void SanitizeAndSplitIntoWordReturnsListWithOnlyWords()
        {
            // Arrange
            var text = "Lorem! ££ ipsum* dolor.. sit amet, at   elitr habemus consequat quo\n\rLorem";

            // act
            var splitter = new TextSplitter();
            var result = splitter.SanitizeAndSplitIntoWords(text).ToList();

            //
            Assert.AreEqual(11, result.Count);
            Assert.IsTrue(result.Contains("Lorem"));
            Assert.IsTrue(result.Contains("ipsum"));
            Assert.IsTrue(result.Contains("dolor"));
            Assert.IsTrue(result.Contains("sit"));
            Assert.IsTrue(result.Contains("amet"));
            Assert.IsTrue(result.Contains("at"));
            Assert.IsTrue(result.Contains("elitr"));
            Assert.IsTrue(result.Contains("habemus"));
            Assert.IsTrue(result.Contains("consequat"));
            Assert.IsTrue(result.Contains("quo"));
            Assert.IsTrue(result.Contains("Lorem"));
        }
    }
}
