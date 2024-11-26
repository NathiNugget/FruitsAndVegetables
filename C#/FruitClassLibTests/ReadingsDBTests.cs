using Microsoft.VisualStudio.TestTools.UnitTesting;
using FruitClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace FruitClassLib.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass()]
    public class ReadingsDBTests
    {
        private ReadingsDB _repo;

        [TestInitialize]
        public void Setup() {
            _repo = new ReadingsDB(true);
            _repo.Setup(); 
        }


        [TestMethod()]
        [DataRow(true)]
        [DataRow(false)]
        public void ReadingsDBInstantiateTest(bool testMode)
        {
            ReadingsDB mock = new ReadingsDB(testMode);
            Assert.IsNotNull(mock);
        }

        [TestMethod()]
        [DataRow(true, 50.2, 50.2)]
        public void AddReadingTest(bool testMode, double temp, double humidity)
        {
            Reading expected = new Reading(temp, humidity);
            ReadingsDB mockTest = new ReadingsDB(testMode);
            Reading actual = mockTest.Add(expected);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DataRow(0, 5)]
        [DataRow(5, 5)]
        public void GetWithOffsetTest(int offset, int count)
        {
            int expected = count;

            int actual = _repo.Get(offset, count).Count;
            Assert.AreEqual(expected, actual);
        }



        [TestCleanup]
        public void Cleanup() {
            _repo.Nuke(); 
        }


    }
}