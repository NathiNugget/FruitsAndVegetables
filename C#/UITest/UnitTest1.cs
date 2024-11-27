using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UITest
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver;
        // TODO: Replace URL when running tests
        const string TEST_URL = "Placeholder";

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
            Thread.Sleep(1000);
            IWebElement spoilTime = driver.FindElement(By.Id("SpoilTime"));
            Assert.IsNotNull(spoilTime);
        }

        [TestMethod]
        public void SelectFoodCorrectName()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            IWebElement selectedFood = driver.FindElement(By.Id("Banan"));
            selectedFood.Click();
            Thread.Sleep(1000);
            IWebElement selectedElement = driver.FindElement(By.Id("Placeholder")); //TODO: Change Placeholder ID to actual element
            string expected = "Banan";
            string actual = selectedElement.Text;
            Assert.AreEqual(expected, actual);
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Dispose();
        }
    }
}