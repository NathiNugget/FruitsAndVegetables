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
        [DataRow("Banana", 1, "link", (byte)1, (byte)1, 50.0, 50.0)]
        public void NotNullTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food expected = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            Assert.IsNotNull(expected);
        }

        [TestMethod()]
        [DataRow("B", 1, "link", (byte)1, (byte)1, 50.0, 50.0)]
        [DataRow("Banananananananananananananana", 1, "link", (byte)1, (byte)1, 50.0, 50.0)]
        public void NameValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mockFood = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            string expected = name;
            string actual = mockFood.Name;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow("", 1, "link", (byte)1, (byte)1, 50.0, 50.0)]
        [DataRow("Bananananananananananananananana", 1, "link", (byte)1, (byte)1, 50.0, 50.0)]
        public void NameNotValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Assert.ThrowsException<ArgumentException>(() => new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity));
        }


        [TestMethod()]
        [DataRow("B", 1, "link", (byte)1, (byte)1, 50.0, 50.0)]
        [DataRow("Banananananananananananananana", 1, "link", (byte)1, (byte)1, 50.0, 50.0)]
        public void ApiLinkValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mockFood = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            string expected = apiLink;
            string actual = mockFood.ApiLink;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow("Banana", 1, "", (byte)1, (byte)1, 50.0, 50.0)]
        public void ApiLinkNotValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity));
        }

        [TestMethod()]
        [DataRow("Banana", 1, "link", (byte)0, (byte)1, 50.0, 50.0)]
        public void SpoilDateValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mockFood = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            byte expected = spoilDate;
            byte actual = mockFood.SpoilDate;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 50.0, 50.0)]
        public void SpoilHoursValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mockFood = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            byte expected = spoilHours;
            byte actual = mockFood.SpoilHours;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, -50.0, 50.0)]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 85.0, 50.0)]
        public void IdealTemperatureValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mockFood = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            double expected = idealTemperature;
            double actual = mockFood.IdealTemperature;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, -51.0, 50.0)]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 86.0, 50.0)]
        public void IdealTemperatureNotValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity));
        }

        [TestMethod]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, -50.0, 100)]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 85.0, 0.0)]
        public void IdealHumidityValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food mockFood = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            double expected = idealHumidity;
            double actual = mockFood.IdealHumidity;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 50.0, 101.0)]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 50.0, -1)]
        public void IdealHumidityNotValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity));
        }



        [TestMethod]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, -50.0, 50.0, -50)]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 85.0, 50.0, 85)]
        public void MinTempValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity, double minTemp)
        {
            Food mockFood = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity, minTemp: minTemp);
            double expected = idealTemperature;
            double actual = mockFood.IdealTemperature;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, -50.0, 50.0, -51)]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 85.0, 50.0, 86)]
        public void MinTempNotValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity, double minTemp)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity, minTemp: minTemp));
        }



        [TestMethod]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, -50.0, 50.0, -50)]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 85.0, 50.0, 85)]
        public void MaxTempValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity, double maxTemp)
        {
            Food mockFood = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity, maxTemp: maxTemp);
            double expected = idealTemperature;
            double actual = mockFood.IdealTemperature;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, -50.0, 50.0, -51)]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 85.0, 50.0, 86)]
        public void MaxTempNotValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity, double maxTemp)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity, maxTemp: maxTemp));
        }




        [TestMethod]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, -50.0, 50.0,1.1)]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 85.0, 50.0, 999)]
        public void Q10ValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity, double q10)
        {
            Food mockFood = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity, q10Factor: q10);
            double expected = idealTemperature;
            double actual = mockFood.IdealTemperature;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, -50.0, 50.0, 0.99)]
        [DataRow("Banana", 1, "link", (byte)1, (byte)0, 85.0, 50.0, -1)]
        public void Q10NotValidTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity, double q10)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity, q10Factor: q10));
        }





    }
}