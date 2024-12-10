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
            _repo.Nuke();
            _repo.SetUp();
        }
        [TestMethod]
        public void UserDBInstiateTest()
        {
            Assert.IsNotNull(_repo);
        }


        [TestMethod()]
        public void UserDB_AddTest()
        {
            User expected = new User("MariusKnudsen", "sdasfksdfgjgg");
            User actual = _repo.Add(expected);
            Assert.AreEqual(expected.Name, actual.Name);
        }


        [TestMethod()]
        [DataTestMethod]
        [DataRow("Marius", "hehehehe")]
        [DataRow("Nathaniel", "maaguyyy")]
        public void UserDB_GetTest_FoundUser(string name, string password)
        {
            User? actual = _repo.Get(name, password);
            Assert.IsNotNull(actual);
        }


        [TestMethod()]
        [DataTestMethod]
        [DataRow("Mariu", "hehehehe")]
        [DataRow("Nathaniel", "maagyyy")]
        public void UserDB_GetTest_NotFoundUser(string name, string password)
        {
            User? actual = _repo.Get(name, password);
            Assert.IsNull(actual);
        }



        [TestMethod()]
        [DataTestMethod]
        [DataRow("Marius", "hehehehe")]
        [DataRow("Nathaniel", "maaguyyy")]
        public void UserDB_GetNewSessionTokenTest_FoundUser(string name, string password)
        {
            string? actual = _repo.GetNewSessionToken(name, password);
            Assert.IsNotNull(actual);
        }


        [TestMethod()]
        [DataTestMethod]
        [DataRow("Mariu", "hehehehe")]
        [DataRow("Nathaniel", "maagyyy")]
        public void UserDB_GetNewSessionTokenTest_NotFoundUser(string name, string password)
        {
            string? actual = _repo.GetNewSessionToken(name, password);
            Assert.IsNull(actual);
        }
        [TestMethod()]
        [DataTestMethod]
        [DataRow("Nathaniel", "maaguyyy")]
        [DataRow("Jacob", "Hahaxd")]

        public void ValidateTest_Found(string name, string password)
        {
            string? token = _repo.GetNewSessionToken(name, password);
            bool actual = _repo.Validate(token);
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        [DataTestMethod]
        [DataRow("Nathniel", "maaguyyy")]
        [DataRow("Jacob", "Hahxd")]

        public void ValidateTest_NotFound(string name, string password)
        {
            string? token = _repo.GetNewSessionToken(name, password);
            bool actual = _repo.Validate(token);
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        [DataTestMethod]
        [DataRow("fdssfsdfsdf")]
        [DataRow("asfdsfsdfsfg")]

        public void ValidateTest_TokenWrong(string token)
        {
            bool actual = _repo.Validate(token);
            Assert.IsFalse(actual);
        }


        [TestCleanup]
        public void Cleanup()
        {
            _repo.Nuke();
        }
    }
}