using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UITest
{
    [TestClass]
    public class UItestfrontend
    {
        IWebDriver driver;
        // TODO: Replace URL when running tests
        const string TEST_URL = "http://127.0.0.1:5500/";
        const int _maxWaitMillis = 500; 

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(TEST_URL);
        }

        [TestMethod]
        public void NewestTempReading()
        {
            IWebElement newestTemp = driver.FindElement(By.Id("NewestTemp"));
            Assert.IsNotNull(newestTemp);
        }

        [TestMethod]
        public void NewestHumidityReading()
        {
            IWebElement newestHumidity = driver.FindElement(By.Id("NewestHumidity"));
            Assert.IsNotNull(newestHumidity);
        }

        [TestMethod]
        public void ClickableDropDown()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            IWebElement selectedFood = driver.FindElement(By.Id("Banan"));
            Assert.IsNotNull(selectedFood);
        }

        [TestMethod]
        public void SelectFood()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            IWebElement selectedFood = driver.FindElement(By.Id("Banan"));
            selectedFood.Click();
            Thread.Sleep(_maxWaitMillis);
            IWebElement spoilTime = driver.FindElement(By.Id("SpoilTime"));
            Assert.IsNotNull(spoilTime);
        }

        [TestMethod]
        public void SelectFoodCorrectName()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            SelectElement selectElement = new SelectElement(dropdown);
            string expected = "Banan"; 
            selectElement.SelectByValue(expected);
            Thread.Sleep(_maxWaitMillis);
            //IWebElement selectedElement = driver.FindElement(By.Id("Placeholder")); //TODO: Change Placeholder ID to actual element
            //string expected = "Banan";
            //string actual = selectedElement.Text;
            //Assert.AreEqual(expected, actual);
            Assert.AreEqual(selectElement.WrappedElement.GetAttribute("value"), expected);    
            
        }

        [TestMethod]
        public void FruitVegetableFiltersCanBeClicked()
        {
            IWebElement fruitFilter = driver.FindElement(By.Id("ShowFruits"));
            IWebElement vegetableFilter = driver.FindElement(By.Id("ShowVegetable"));
            fruitFilter.Click();
            vegetableFilter.Click();
            Assert.IsNotNull(fruitFilter);
            Assert.IsNotNull(vegetableFilter);
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Dispose();
        }
    }
}