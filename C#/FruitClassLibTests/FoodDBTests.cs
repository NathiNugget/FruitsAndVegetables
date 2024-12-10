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
        [DataRow( "TestFood", 1, "TestLink", (byte)2, (byte)5, 50.0, 50.0, (byte)4, 60, -20)]

        public void AddFoodDBTest(string name, int foodTypeId, string apiLink, byte spoilDate, byte spoilHours, double idealTemperature, double idealHumidity, byte q10Factor, double maxTemp, double minTemp)
        {
            Food expected = new Food(name, foodTypeId, apiLink, spoilDate, spoilHours, idealTemperature, idealHumidity, q10Factor:q10Factor, maxTemp: maxTemp, minTemp: minTemp);
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
            var expected = 5;
            var actual = _repo.GetAll();

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllFruitFilterTest()
        {
            var expected = 2;
            var actual = _repo.GetAllFiltered(filterFruit: true, filterVegetable: false);
            Assert.AreEqual(expected, actual.Count);
        }
        [TestMethod()]
        public void GetAllVegetableFilterTest()
        {

            var expected = 3;
            var actual = _repo.GetAllFiltered(filterVegetable: true, filterFruit: false);

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllFilterNamesTestPartOfString()
        {
            var expected = 1;
            var actual = _repo.GetAllFiltered(filterName: "ag");

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllFilterNamesTestWholeString()
        {
            var expected = 1;
            var actual = _repo.GetAllFiltered(filterName: "agurk");

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllFilterNamesTestEmptyString_ReturnsAll()
        {
            var expected = 5;
            var actual = _repo.GetAllFiltered(filterName: "");

            Assert.AreEqual(expected, actual.Count);
        }
        [TestMethod()]
        public void GetAllFilterNamesTestCaseLess_Returns1()
        {
            var expected = 1;
            var actual = _repo.GetAllFiltered(filterName: "ÆBLE");

            Assert.AreEqual(expected, actual.Count);
        }
        [TestMethod()]
        public void GetAllFilterNamesTestNull_ReturnsAll()
        {
            var expected = 5;
            var actual = _repo.GetAllFiltered(filterName: null);
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllBothFilterTest()
        {
            var expected = 5;
            var actual = _repo.GetAllFiltered(filterVegetable: true, filterFruit: true);

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllPagnationTest_Get2()
        {
            var expected = 2;
            var actual = _repo.GetAll(offset: 0, count: 2);

            Assert.AreEqual(expected, actual.Count);
        }
        [TestMethod()]
        public void GetAllPagnationTest_GetAll()
        {
            var expected = 5;
            var actual = _repo.GetAll(offset: 0);

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllPagnationTest_Get1From2()
        {
            var expected = 1;
            var actual = _repo.GetAll(offset: 2, count: 1);
            Assert.AreEqual(expected, actual.Count);
        }



        [TestMethod()]
        public void GetAllNamesTest()
        {
            var expected = 5;
            var actual = _repo.GetAllNames();

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod()]
        public void GetAllNamesFruitFilterTest()
        {
            var expected = 2;
            var actual = _repo.GetAllNames(filterFruit: true, filterVegetable: false);

            Assert.AreEqual(expected, actual.Count);
        }
        [TestMethod()]
        public void GetAllNamesVegetableFilterTest()
        {
            var expected = 3;
            var actual = _repo.GetAllNames(filterVegetable: true, filterFruit: false);

            Assert.AreEqual(expected, actual.Count);
        }


        [TestMethod()]
        public void GetAllNamesBothFilterTest()
        {
            var expected = 5;
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