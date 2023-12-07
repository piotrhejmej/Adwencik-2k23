using Adwencik_2k23.Handlers._5;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DayFiveTests
    {
        [TestMethod]
        public void DayFive_Gorsza()
        {
            //Arrange
            var parsed = InputLoader.Load("five");
            var expected = 35;

            //Act
            var result = DayFive.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DayFive_Lepsza()
        {
            //Arrange
            var parsed = InputLoader.Load("five");
            var expected = 46;

            //Act
            var result = DayFive.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}