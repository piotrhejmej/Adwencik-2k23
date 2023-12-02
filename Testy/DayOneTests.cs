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
            var dayOne = new DayOne();
            var expected = 142;
            var input = @"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";
            var parsed = new InputLoader().Load("one");

            //Act
            var result = dayOne.Pierwsze(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DayOne_Lepsza()
        {
            //Arrange
            var dayOne = new DayOne();
            var expected = 281;
            var input = @"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";
            var parsed = new InputLoader().Load("one");

            //Act
            var result = dayOne.Drugie(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}