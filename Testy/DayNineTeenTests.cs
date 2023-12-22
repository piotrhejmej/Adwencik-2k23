using Adwencik_2k23.Handlers._19;
using Adwencik_2k23.Utils;
using NUnit.Framework.Internal;

namespace Testy
{
    [TestClass]
    public class DaynineteenTests
    {
        [TestMethod]
        public void Daynineteen_Gorsza()
        {
            //Arrange
            var parsed = InputLoader.Load("nineteen");
            var expected = 19114;

            //Act
            var result = DayNineteen.One(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Daynineteen_Lepsza()
        {
            //Arrange
            var parsed = InputLoader.Load("nineteen");
            var expected = 167409079868000;

            //Act
            var result = DayNineteen.Two(parsed);

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}