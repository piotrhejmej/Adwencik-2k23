using Adwencik_2k23.Handlers._6;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DaySixTests
    {
        [TestMethod]
        public void DaySix_Gorsza()
        {
            //Arrange
            var parsed = InputLoader.Load("six");
            var expected = 288;

            //Act
            var result = DaySix.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DaySix_Lepsza()
        {
            //Arrange
            var parsed = InputLoader.Load("six");
            var expected = 71503;

            //Act
            var result = DaySix.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}