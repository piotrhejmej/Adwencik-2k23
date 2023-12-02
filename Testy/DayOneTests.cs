using Adwencik_2k23.Handlers._1;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DayOneTests
    {
        [TestMethod]
        public void DayOne_Gorsza()
        {
            //Arrange
            var expected = 142;
            var parsed = InputLoader.Load("one");

            //Act
            var result = DayOne.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DayOne_Lepsza()
        {
            //Arrange
            var expected = 281;
            var parsed = InputLoader.Load("one");

            //Act
            var result = DayOne.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}