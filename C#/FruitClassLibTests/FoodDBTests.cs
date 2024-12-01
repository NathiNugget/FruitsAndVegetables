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

            _repo.Nuke();
            _repo.Setup(); 
        }

        [TestMethod()]
        [DataRow(true)]
        [DataRow(false)]
        public void FoodDBInstiateTest(bool testMode)
        {
            Assert.IsNotNull(_repo);
        }

        [TestMethod()]
        [DataRow( "Banan", 1, "Banan.Link", (byte)2, (byte)5, 50.0, 50.0)]

        public void AddFoodDBTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food expected = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            Food actual = _repo.Add(expected);

            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod()]
        [DataRow("Æble", 1, "Æble.link", (byte)2, (byte)20, 50.0, 50.0)]
        public void FindByNameTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food expected = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            Food actual = _repo.FindByName("Æble");

            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod()]
        public void GetAllTest()
        {          
            var expected = 3;
            var actual = _repo.GetAll();

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllFruitFilterTest()
        {
            var expected = 1;
            var actual = _repo.GetAll(filterFruit: true, filterVegetable: false);

            Assert.AreEqual(expected, actual.Count);
        }
        [TestMethod()]
        public void GetAllVegetableFilterTest()
        {
            var expected = 2;
            var actual = _repo.GetAll(filterVegetable: true, filterFruit: false);

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllBothFilterTest()
        {
            var expected = 3;
            var actual = _repo.GetAll(filterVegetable: true, filterFruit: true);

            Assert.AreEqual(expected, actual.Count);
        }



        [TestMethod()]
        public void GetAllNamesTest()
        {
            var expected = 3;
            var actual = _repo.GetAllNames();

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllNamesFruitFilterTest()
        {
            var expected = 1;
            var actual = _repo.GetAllNames(filterFruit: true, filterVegetable: false);

            Assert.AreEqual(expected, actual.Count);
        }
        [TestMethod()]
        public void GetAllNamesVegetableFilterTest()
        {
            var expected = 2;
            var actual = _repo.GetAllNames(filterVegetable: true, filterFruit: false);

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllNamesBothFilterTest()
        {
            var expected = 3;
            var actual = _repo.GetAllNames(filterVegetable: true, filterFruit: true);

            Assert.AreEqual(expected, actual.Count);
        }



        [TestCleanup]
        public void Cleanup()
        {
            _repo.Nuke();
        }
    }
}