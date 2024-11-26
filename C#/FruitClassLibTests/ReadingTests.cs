using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;

namespace FruitClassLib.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class ReadingTests
    {
        [TestMethod]
        [DataRow(20, 50)]
        public void NotNullTest(double temp, double humidity)
        {
            Reading expected = new Reading(temp, humidity);
            Assert.IsNotNull(expected);

        }

        [TestMethod]
        [DataRow(-50, 50)]
        [DataRow(85, 50)]
        public void TemperatureValidTest(double temp, double humidity)
        {
            Reading mock = new Reading(temp, humidity);
            double expected = temp;
            double actual = mock.Temperature;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(-51, 50)]
        [DataRow(86, 50)]
        public void TemperatureNotValidTest(double temp, double humidity)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Reading(temp, humidity));
        }

        [TestMethod]
        [DataRow(50, 0.0)]
        [DataRow(50, 100.0)]
        public void HumidityValidTest(double temp, double humidity)
        {
            Reading mock = new Reading(temp, humidity);
            double expected = humidity;
            double actual = mock.Humidity;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [DataRow(50, 101)]
        [DataRow(50, -1)]
        public void HumidityNotValidTest(double temp, double humidity)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Reading(temp, humidity));
        }

        [TestMethod]
        [DataRow(50, 50, long.MaxValue)]
        [DataRow(50, 50, 0)]
        public void TimeStampValid(double temp, double humidity, long timeParam)
        {
            Reading mock = new Reading(temp, humidity, timestamp: timeParam);
            long expected = timeParam;
            long actual = mock.Timestamp;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow(50, 50, -1)]
        public void TimeStampNotValidTest(double temp, double humidity, long timeParam)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Reading(temp, humidity, timestamp: timeParam));
        }
    }
}