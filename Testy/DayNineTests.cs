using Adwencik_2k23.Handlers._9;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DayNineTests
    {
        [TestMethod]
        public void DayNine_Gorsza()
        {
            //Arrange
            var parsed = InputLoader.Load("nine");
            var expected = 114;

            //Act
            var result = DayNine.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DayNine_Lepsza()
        {
            //Arrange
            var parsed = InputLoader.Load("nine2");
            var expected = 5;

            //Act
            var result = DayNine.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}