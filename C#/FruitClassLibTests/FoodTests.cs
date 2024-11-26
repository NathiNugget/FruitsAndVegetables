using Microsoft.VisualStudio.TestTools.UnitTesting;
using FruitClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitClassLib.Tests
{
    [TestClass()]
    public class FoodTests
    {
        [TestMethod()]
        [DataRow("Banana", false, "link", (byte)1, (byte)1, 50.0, 50.0)]
        public void NotNullTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food expected = new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            Assert.IsNotNull(expected);
        }

        [TestMethod()]
        [DataRow("B", false, "link", (byte)1, (byte)1, 50.0, 50.0)]
        [DataRow("Banananananananananananananana", false, "link", (byte)1, (byte)1, 50.0, 50.0)]
        public void NameValidTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mock = new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            string expected = name;
            string actual = mock.Name;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow("", false, "link", (byte)1, (byte)1, 50.0, 50.0)]
        [DataRow("Bananananananananananananananana", false, "link", (byte)1, (byte)1, 50.0, 50.0)]
        public void NameNotValidTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Assert.ThrowsException<ArgumentException>(() => new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity));
        }
        //Evt. Lave test for at tjekke man ikke kan lave dupliket

        [TestMethod()]
        [DataRow("B", false, "link", (byte)1, (byte)1, 50.0, 50.0)]
        [DataRow("Banananananananananananananana", false, "link", (byte)1, (byte)1, 50.0, 50.0)]
        public void ApiLinkValidTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mock = new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            string expected = apiLink;
            string actual = mock.ApiLink;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow("Banana", false, "", (byte)1, (byte)1, 50.0, 50.0)]
        public void ApiLinkNotValidTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity));
        }

        [TestMethod()]
        [DataRow("Banana", false, "link", (byte)0, (byte)1, 50.0, 50.0)]
        public void SpoilDateValidTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mock = new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            byte expected = spoilDate;
            byte actual = mock.SpoilDate;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow("Banana", false, "link", (byte)1, (byte)0, 50.0, 50.0)]
        public void SpoilHoursValidTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mock = new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            byte expected = spoilHours;
            byte actual = mock.SpoilHours;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("Banana", false, "link", (byte)1, (byte)0, -50.0, 50.0)]
        [DataRow("Banana", false, "link", (byte)1, (byte)0, 85.0, 50.0)]
        public void IdealTemperatureValidTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mock = new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            double expected = idealTemperature;
            double actual = mock.IdealTemperature;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("Banana", false, "link", (byte)1, (byte)0, -51.0, 50.0)]
        [DataRow("Banana", false, "link", (byte)1, (byte)0, 86.0, 50.0)]
        public void IdealTemperatureNotValidTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity));
        }

        [TestMethod]
        [DataRow("Banana", false, "link", (byte)1, (byte)0, -50.0, 100)]
        [DataRow("Banana", false, "link", (byte)1, (byte)0, 85.0, 0.0)]
        public void IdealHumidityValidTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mock = new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            double expected = idealHumidity;
            double actual = mock.IdealHumidity;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("Banana", false, "link", (byte)1, (byte)0, 50.0, 101.0)]
        [DataRow("Banana", false, "link", (byte)1, (byte)0, 50.0, -1)]
        public void IdealHumidityNotValidTest(string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity));
        }


    }
}