using Adwencik_2k23.Handlers._4;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DayFourTests
    {
        [TestMethod]
        public void DayThree_Gorsza()
        {
            //Arrange
            var parsed = InputLoader.Load("four");
            var expected = 13;

            //Act
            var result = DayFour.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DayThree_Lepsza()
        {
            //Arrange
            var parsed = InputLoader.Load("four");
            var expected = 30;

            //Act
            var result = DayFour.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}