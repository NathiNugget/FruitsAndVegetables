using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [DataRow(true, "Banan", false, "Banan.Link", (byte)2, (byte)5, 50.0, 50.0)]

        public void AddFoodDBTest(bool testMode, string name, bool isVegetable, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food expected = new Food(name, isVegetable, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);
            FoodDB mockTest = new FoodDB(testMode);
            Food actual = mockTest.Add(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(true)]
        public void FindByNameTest(bool testMode)
        {
            Food expected = new Food("Æble", false, "Æble.link", (byte)2, (byte)20, 50.0, 50.0);
            FoodDB mockTest = new FoodDB(testMode);
            Food actual = mockTest.FindByName("Æble");
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod()]
        [DataRow(true)]
        public void FindByIsVegetableTest(bool testMode)
        {
            FoodDB mockTest = new FoodDB(testMode);
            foreach (var food in _repo.FindByIsVegetable())
            {
                mockTest.Add(food);
            }

            var expected = _repo.FindByIsVegetable();
            var actual = mockTest.FindByIsVegetable();
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod()]
        [DataRow(true)]
        public void GetAllTest(bool testMode)
        {

            FoodDB mockTest = new FoodDB(testMode);            
            var expected = _repo.GetAll();
            var actual = mockTest.GetAll();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _repo.Nuke();
        }
    }
}