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
    public class UserDBTests
    {
        private UserDB _repo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new UserDB(true);
        }


        [TestMethod()]
        public void UserDBTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ValidateTest()
        {
            Assert.Fail();
        }
    }
}