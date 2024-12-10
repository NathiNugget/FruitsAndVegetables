using FruitClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitClassLibTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow(1, "Marius", "Psarqer46735")]
        public void UserNotNull(int id, string name, string password)
        {
            User actual = new User(name, password, id);
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(1, "Marius", "Psarqer46735")]
        [DataRow(1, "Marius Knudsen", "Psarqer46735")]
        [DataRow(1, "Bo", "Psarqer46735")]
        [DataRow(1, "Marius Knudsenaaaaaaaaaaaaaaaa", "Psarqer46735")]

        public void UserAllowedNames(int id, string name, string password)
        {
            User actual = new User(name, password, id);
        }
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        [DataTestMethod]
        [DataRow(1, "", "Psarqer46735")]
        [DataRow(1, " ", "Psarqer46735")]
        [DataRow(1, "   ", "Psarqer46735")]
        [DataRow(1, "\n", "Psarqer46735")]
        [DataRow(1, "\t", "Psarqer46735")]
        [DataRow(1, "-marius", "Psarqer46735")]
        [DataRow(1, ",marius", "Psarqer46735")]
        [DataRow(1, "´marius", "Psarqer46735")]
        [DataRow(1, "Marius KnudsenaaaaaaaaaaaaaaaaMarius KnudsenaaaaaaaaaaaaaaaaMarius KnudsenaaaaaaaaaaaaaaaaMarius KnudsenaaaaaaaaaaaaaaaaMarius KnudsenaaaaaaaaaaaaaaaaMarius KnudsenaaaaaaaaaaaaaaaaMarius KnudsenaaaaaaaaaaaaaaaaMarius KnudsenaaaaaaaaaaaaaaaaMarius KnudsenaaaaaaaaaaaaaaaaMarius KnudsenaaaaaaaaaaaaaaaaMarius Knudsenaaaaaaaaaaaaaaaa", "Psarqer46735")]

        public void UserNotAllowedNames(int id, string name, string password)
        {
            User actual = new User(name, password, id);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        [DataTestMethod]
        [DataRow(1, null, "Psarqer46735")]
        public void UserNotAllowedNamesNull(int id, string name, string password)
        {
            User actual = new User(name, password, id);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        [DataTestMethod]
        [DataRow(1, "Bo", "")]
        [DataRow(1, "Bo", "-hofdsfsfg")]
        [DataRow(1, "Bo", "dfar")]
        [DataRow(1, "Bo", "asddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasddddddddddddddddddddddddasdddddddddddddddddddddddd")]

        public void UserNotAllowedPasswords(int id, string name, string password)
        {
            User actual = new User(name, password, id);
        }
        [TestMethod]
        [DataTestMethod]
        [DataRow(1, "Bo", "asddddddddddddddddddddddddsddddddddddddddddddddddddsddddddddddddddddddddddddsddddddddddddddddddddddddsddddddddddddddddddddddddsddddddddddddddddddddddddsddddddddddddddddddddddddsddddddddddddddddddddddddsdddddddddddddddddddddddd")]
        [DataRow(1, "Bo", "hofdsfsfg")]
        [DataRow(1, "Bo", "dfard")]

        public void UserAllowedPasswords(int id, string name, string password)
        {
            User actual = new User(name, password, id);
        }
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        [DataTestMethod]
        [DataRow(1, "Bo", null)]
        public void UserNotAllowedPasswordsNull(int id, string name, string password)
        {
            User actual = new User(name, password, id);
        }

    }
}
