using Adwencik_2k23.Handlers._2;
using Adwencik_2k23.Handlers._3;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DayThreeTests
    {
        [TestMethod]
        public void DayThree_Gorsza()
        {
            //Arrange
            var parsed = InputLoader.Load("three");
            var expected = 4361;

            //Act
            var result = DayThree.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DayThree_Lepsza()
        {
            //Arrange
            var parsed = InputLoader.Load("three");
            var expected = 467835;

            //Act
            var result = DayThree.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}