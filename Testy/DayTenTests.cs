using Adwencik_2k23.Handlers._10;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DayTenTests
    {
        [TestMethod]
        public void DayTen_Gorsza()
        {
            //Arrange
            var parsed = InputLoader.Load("ten");
            var expected = 80;

            //Act
            var result = DayTen.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DayTen_Lepsza()
        {
            //Arrange
            var parsed = InputLoader.Load("ten");
            var expected = 10;

            //Act
            var result = DayTen.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}