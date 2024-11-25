using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;

namespace FruitClassLib.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class ReadingTests
    {
        [TestMethod]
        [DataRow]
        public void CreateTest(double temp, double humidity, int id)
        {
            Reading expected = new Reading(temp, humidity, id); 
            Assert.IsNotNull(expected);

        }

        [TestMethod]

        
    }
}