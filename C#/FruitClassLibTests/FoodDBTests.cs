﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using FruitClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FruitClassLib.Tests
{
    [TestClass()]
    public class FoodDBTests
    {
        private FoodDB _repo;

        [TestInitialize] public void Setup() 
        { 
            _repo = new FoodDB(true); 
            _repo.Setup(); 
        }

        [TestMethod()]
        [DataRow(true)]
        [DataRow(false)]
        public void FoodDBInstiateTest(bool testMode)
        {
            FoodDB mock = new FoodDB(testMode);
            Assert.IsNotNull(mock);
        }

        [TestMethod()]
        public void AddFoodDBTest(bool testMode, string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food expected = new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            FoodDB mockTest = new FoodDB(testMode);
            Food actual = mockTest.Add(expected);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(true, "Banan", false, "Banan.Link", (byte)2, (byte)5, 50.0, 50.0)]
        public void FindByNameValidTest(bool testMode, string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food expected = new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            FoodDB mockTest = new FoodDB(testMode);
            Food actual = mockTest.FindByName(expected);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(true, "Cucumber", true, "Cucumber.Link", (byte)2, (byte)5, 50.0, 50.0)]
        public void FindByIsVegValidTest(bool testMode, string name, bool isVeg, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food expected = new Food(name, isVeg, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            FoodDB mockTest = new FoodDB(testMode);
            Food actual = mockTest.FindByIsVeg(expected);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllTest(bool testMode)
        {

            FoodDB mockTest = new FoodDB(testMode);
            foreach (var food in _repo.GetAll())
            {
                mockTest.Add(food);
            }
            var expected = _repo.GetAll();
            var actual = mockTest.GetAll();

            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repo.Nuke();
        }
    }
}