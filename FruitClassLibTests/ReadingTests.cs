using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;

namespace FruitClassLib.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class ReadingTests
    {
        [TestMethod()]
        

        [TestMethod]
        public void CreateTest(int temp, int humidity, DateTime timestamp)
        {
            Reading expected = new Reading(temp, humidity, DateTime time); 

        }
        
    }
}