using Adwencik_2k23.Handlers._7;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DaySevenTests
    {
        [TestMethod]
        public void DaySeven_Gorsza()
        {
            //Arrange
            var parsed = InputLoader.Load("seven");
            var expected = 6440;

            //Act
            var result = DaySeven.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DaySeven_Lepsza()
        {
            //Arrange
            var parsed = InputLoader.Load("seven");
            var expected = 5905;

            //Act
            var result = DaySeven.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}