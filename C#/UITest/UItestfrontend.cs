using FruitClassLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UITest
{
    [TestClass]
    public class UItestfrontend
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext test) {
            _repo.Nuke(); 
            _repo.Setup(); 


            
        }
        static FoodDB _repo = new(true); 
        public static ChromeOptions Options { get; set; } = new();
         
        static IWebDriver driver = new ChromeDriver(Options);


        // TODO: Replace URL when running tests
        const string TEST_URL = "http://localhost:5173";
        const int _maxWaitMillis = 2000;

        [TestInitialize]
        public void Setup()
        {
            driver.Navigate().GoToUrl(TEST_URL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(_maxWaitMillis);

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

            IWebElement selectedFood = driver.FindElement(By.Id("Agurk"));
            Assert.IsNotNull(selectedFood);
        }

        [TestMethod]
        public void SelectFoodCorrectName()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            string input = "Agurk";
            dropdown.SendKeys(input);
            Thread.Sleep(500);
            dropdown.SendKeys(Keys.Enter);
            Thread.Sleep(500); 
            string actual = driver.FindElement(By.Id("Agurk")).GetDomAttribute("value"); 
            Assert.AreEqual(input, actual);

        }

        [TestMethod]
        public void FruitVegetableFiltersCanBeClicked()
        {
            IWebElement fruitFilter = driver.FindElement(By.Id("FruitCheck"));
            IWebElement vegetableFilter = driver.FindElement(By.Id("VegetableCheck"));
            fruitFilter.Click();
            vegetableFilter.Click();
            Assert.IsNotNull(fruitFilter);
            Assert.IsNotNull(vegetableFilter);
        }

        public void SelectedFoodElementUpdated()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            SelectElement selectElement = new SelectElement(dropdown);
            string expected = "Banan";
            selectElement.SelectByValue(expected);

            IWebElement selectedElement = driver.FindElement(By.Id("SelectedName")); //TODO: Change Placeholder ID to actual element
            string actual = selectedElement.Text;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShelfLifeNotNull()
        {
            string selectedFood = "Agurk";
            IWebElement elem = driver.FindElement(By.Id(selectedFood));
            IWebElement input = driver.FindElement(By.Id("FoodDropdown"));
            input.SendKeys(selectedFood);
            input.SendKeys(Keys.Return);
            IWebElement shelflife = driver.FindElement(By.Id("ShelfLife"));
            string expected = "0 dage";
            string actual = shelflife.Text;
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void ShelfLife_Days()
        {
            string selectedFood = "Agurk";
            IWebElement elem = driver.FindElement(By.Id(selectedFood));
            IWebElement input = driver.FindElement(By.Id("FoodDropdown"));
            input.SendKeys(selectedFood);
            input.SendKeys(Keys.Return);
            IWebElement shelflife = driver.FindElement(By.Id("ShelfLife"));
            string expected = "0 dage";
            string actual = shelflife.Text;
            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void ShelfLife_Hours()
        {
            string selectedFood = "Agurk";
            IWebElement elem = driver.FindElement(By.Id(selectedFood));
            IWebElement input = driver.FindElement(By.Id("FoodDropdown"));
            input.SendKeys(selectedFood);
            input.SendKeys(Keys.Return);
            IWebElement shelflife = driver.FindElement(By.Id("ShelfLife"));
            string expected = "12 timer";
            string actual = shelflife.Text;
            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void FruitsOnly_InvalidVegetable()
        {
            IWebElement fruitCheckbox = driver.FindElement(By.Id("FruitCheck"));
            fruitCheckbox.Click();
            Thread.Sleep(500);
            IWebElement dropdown = driver.FindElement(By.Id("FoodSuggestions"));
            List<IWebElement> children = driver.FindElements(By.TagName("option")).ToList();

            Assert.IsTrue(dropdown.FindElements(By.TagName("option")).Count == 3);



        }
        [TestMethod]
        public void FruitsOnly_ValidFruit()
        {
            IWebElement fruitCheckbox = driver.FindElement(By.Id("FruitCheck"));
            fruitCheckbox.Click();
            IWebElement input = driver.FindElement(By.Id("FoodSuggestions"));
            var actual = input.FindElements(By.TagName("option")).ToList();
            Assert.IsTrue(actual.Count > 0);
        }

        [TestMethod]
        public void VegetablesOnly_InvalidFruit()
        {

            IWebElement datalist = driver.FindElement(By.Id("FoodSuggestions")); 
            IWebElement fruitCheckbox = driver.FindElement(By.Id("FruitCheck"));
            fruitCheckbox.Click();
            Thread.Sleep(500); 
            IWebElement dropdown = driver.FindElement(By.Id("FoodSuggestions"));

            var actual = datalist.FindElements(By.TagName("option"));
            Assert.IsNull(actual.FirstOrDefault(elem => elem.GetDomAttribute("value") == "Æble")); 
            
        }

        [TestMethod]
        public void VegetablesOnly_ValidVegetable()
        {
            IWebElement fruitCheckbox = driver.FindElement(By.Id("FruitCheck"));
            fruitCheckbox.Click();
            
            IWebElement dropdown = driver.FindElement(By.Id("FoodSuggestions"));
            Thread.Sleep(500);
            var actual = dropdown.FindElements(By.TagName("option"));

            var actuall = actual.First(elem => elem.GetDomAttribute("value") == "Agurk");
            Assert.IsNotNull(actuall);
        }
        
        [TestMethod]
        public void ChartElementNotNull()
        {
            IWebElement TempChart = driver.FindElement(By.Id("tempChart"));
            Assert.IsNotNull(TempChart);
        }


        [TestMethod]
        public void GetRecipeForBanana_FullListShown()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            string input = "Banan";
            dropdown.SendKeys(input);
            Thread.Sleep(500);
            dropdown.SendKeys(Keys.Return);
            string expected = "Banana Pancakes";
            Thread.Sleep(200);
            IWebElement recommendedRecipes = driver.FindElement(By.Id("RecommendedRecipes"));
            IList<IWebElement> elementChildren = recommendedRecipes.FindElements(By.TagName("div"));
            List<string> recipeNames = new();
            foreach (IWebElement elementChild in elementChildren)
            {
                recipeNames.Add(elementChild.FindElement(By.ClassName("RecipeText")).Text);
            }
            Assert.IsTrue(recipeNames.Contains(expected));
        }

        [TestMethod]
        public void GetRecipeForApple_NoRecipes()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            string input = "Æble";
            dropdown.SendKeys(input);
            Thread.Sleep(500);
            dropdown.SendKeys(Keys.Enter);
            string expected = "Ingen opskrifter fundet, find selv på noget! :P"; 
            IWebElement recommendedRecipes = driver.FindElement(By.Id("NoRecipeFound"));
            string acutal = recommendedRecipes.Text; 
            Assert.AreEqual(expected, acutal);
        }

        [TestMethod]
        public void GetRecipeForGarlic_ListTooBig()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            string input = "Hvidløg";
            dropdown.SendKeys(input);
            Thread.Sleep(500);
            dropdown.SendKeys(Keys.Enter); 
            IWebElement recommendedRecipes = driver.FindElement(By.Id("RecommendedRecipes"));
            IList<IWebElement> recipeChildren = recommendedRecipes.FindElements(By.TagName("div"));
            Assert.IsTrue(recipeChildren.Count() == 3);
        }

        [TestMethod]
        public void GetImageForBananaRecipe()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            //Thread.Sleep(200); 
            string input = "Banan";
            dropdown.SendKeys(input);
            Thread.Sleep(500);
            dropdown.SendKeys(Keys.Enter);
            string expected = "https://www.themealdb.com/images/media/meals/sywswr1511383814.jpg";
            IWebElement firstRecipe = driver.FindElement(By.Id("Banana Pancakes.img"));
            string actual = firstRecipe.GetDomAttribute("src");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetImageForApple()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            
            string input = "Æble";
            dropdown.SendKeys(input);
            Thread.Sleep(500);
            dropdown.SendKeys(Keys.Enter);

            string expected = "https://themealdb.com/images/ingredients/Apple.png";
            IWebElement selectedFoodPicture = driver.FindElement(By.Id("ChosenFoodImage"));
            string actual = selectedFoodPicture.GetDomAttribute("src");
            Assert.AreEqual(expected.ToLower(), actual.ToLower());
        }

        //[TestMethod]
        //public void SearchButtonCanBeClicked()
        //{
        //    IWebElement filterBtn = driver.FindElement(By.Id("NameSearchBtn"));
        //    filterBtn.Click();
        //    Assert.IsNotNull(filterBtn);

        //}

        //[TestMethod]
        //public void SearchBoxCanBeClicked()
        //{
        //    IWebElement 
        //    IWebElement nameFilter = driver.FindElement(By.Id("NameSearchBox"));   
        //    nameFilter.Click();
        //    nameFilter.SendKeys("ag");
        //    Thread.Sleep(500);


        //    Assert.IsNotNull(nameFilter);
        //    Assert.IsNotNull(vegetableFilter);
        //}

        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver.Quit();
            _repo.Nuke(); 
        }

    }


}