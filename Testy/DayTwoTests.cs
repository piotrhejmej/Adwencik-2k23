using Adwencik_2k23.Handlers._2;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DayTwoTests
    {
        [TestMethod]
        public void DayOne_Gorsza()
        {
            //Arrange
            var parsed = InputLoader.Load("two");
            var expected = 8;

            //Act
            var result = DayTwo.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DayOne_Lepsza()
        {
            //Arrange
            var parsed = InputLoader.Load("two");
            var expected = 2286;

            //Act
            var result = DayTwo.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}