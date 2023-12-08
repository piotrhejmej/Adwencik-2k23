using Adwencik_2k23.Handlers._8;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DayEightTests
    {
        [TestMethod]
        public void DayEight_Gorsza()
        {
            //Arrange
            var parsed = InputLoader.Load("eight");
            var expected = 6;

            //Act
            var result = DayEight.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DayEight_Lepsza()
        {
            //Arrange
            var parsed = InputLoader.Load("eight");
            var expected = 6;

            //Act
            var result = DayEight.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}