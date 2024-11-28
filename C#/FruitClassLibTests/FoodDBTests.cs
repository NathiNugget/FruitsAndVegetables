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

        [DataRow( "Banan", false, "Banan.Link", (byte)2, (byte)5, 50.0, 50.0)]

        public void AddFoodDBTest(string name, bool isVegetable, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity)
        {
            Food expected = new Food(name, isVegetable, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity);

            Food actual = _repo.Add(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod()]
        public void FindByNameTest()
        {
            Food expected = new Food("Æble", false, "Æble.link", (byte)2, (byte)20, 50.0, 50.0);
            
            Food actual = _repo.FindByName("Æble");
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod()]
        public void GetAllTest()
        {          
            var expected = 3;
            var actual = _repo.GetAll();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllFruitFilterTest()
        {
            var expected = 1;
            var actual = _repo.GetAll(filterFruit: true, filterVegetable: false);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Count);
        }
        [TestMethod()]
        public void GetAllVegetableFilterTest()
        {
            var expected = 2;
            var actual = _repo.GetAll(filterVegetable: true, filterFruit: false);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllBothFilterTest()
        {
            var expected = 3;
            var actual = _repo.GetAll(filterVegetable: true, filterFruit: true);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Count);
        }


        [TestCleanup]
        public void Cleanup()
        {
            _repo.Nuke();
        }
    }
}