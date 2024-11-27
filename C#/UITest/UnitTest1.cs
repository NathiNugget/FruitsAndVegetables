using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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

        [TestCleanup]
        public void Cleanup()
        {
            driver.Dispose();
        }
    }
}