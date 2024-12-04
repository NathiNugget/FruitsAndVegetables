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
        

            
        }

        public static ChromeOptions Options { get; set; } = new();
         
        static IWebDriver driver = new ChromeDriver(Options);


        // TODO: Replace URL when running tests
        const string TEST_URL = "http://127.0.0.1:5500";
        const int _maxWaitMillis = 500;

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
            SelectElement selectElement = new SelectElement(dropdown);
            string expected = "Agurk";

            selectElement.SelectByValue(expected);

            //IWebElement selectedElement = driver.FindElement(By.Id("Placeholder")); //TODO: Change Placeholder ID to actual element
            //string expected = "Banan";
            //string actual = selectedElement.Text;
            //Assert.AreEqual(expected, actual);

            Assert.AreEqual(selectElement.WrappedElement.GetAttribute("value"), expected);

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
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            SelectElement selectElement = new SelectElement(dropdown);
            string selectedFood = "Æble";
            selectElement.SelectByValue(selectedFood);

            IWebElement shelfLife = driver.FindElement(By.Id("ShelfLife"));
            Assert.IsNotNull(shelfLife);
        }

        [TestMethod]
        public void ShelfLife_Days()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            SelectElement selectElement = new SelectElement(dropdown);
            string selectedFood = "Agurk";
            selectElement.SelectByValue(selectedFood);

            IWebElement shelflife = driver.FindElement(By.Id("ShelfLife"));
            string expected = "0 dage";
            string actual = shelflife.Text;
            Assert.IsTrue(actual.Contains(expected));
        }

        [TestMethod]
        public void ShelfLife_Hours()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            SelectElement selectElement = new SelectElement(dropdown);
            string selectedFood = "Agurk";
            Thread.Sleep(500);
            selectElement.SelectByValue(selectedFood);

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
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            SelectElement selectElement = new SelectElement(dropdown);
            Assert.IsTrue(selectElement.Options.Count == 3);



        }
        [TestMethod]
        public void FruitsOnly_ValidFruit()
        {
            IWebElement fruitCheckbox = driver.FindElement(By.Id("FruitCheck"));
            fruitCheckbox.Click();
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            Thread.Sleep(500);
            dropdown.Click();
            SelectElement selectElement = new SelectElement(dropdown);
            selectElement.SelectByValue("Æble");
            IWebElement selectedElement = selectElement.SelectedOption;
            Assert.IsNotNull(selectedElement);
        }

        [TestMethod]
        public void VegetablesOnly_InvalidFruit()
        {
            IWebElement vegetableCheckbox = driver.FindElement(By.Id("VegetableCheck"));
            vegetableCheckbox.Click();
            Thread.Sleep(500);
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            Thread.Sleep(500);
            SelectElement selectElement = new SelectElement(dropdown);
            Assert.ThrowsException<NoSuchElementException>(() => selectElement.SelectByValue("Æble")); 
            
        }

        [TestMethod]
        public void VegetablesOnly_ValidVegetable()
        {
            IWebElement vegetableCheckbox = driver.FindElement(By.Id("VegetableCheck"));
            vegetableCheckbox.Click();
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            SelectElement selectElement = new SelectElement(dropdown);
            selectElement.SelectByValue("Agurk");
            IWebElement selectedOption = selectElement.SelectedOption;
            Assert.IsNotNull(selectElement);
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
            SelectElement selectElement = new SelectElement(dropdown);
            string input = "Banan";
            selectElement.SelectByValue(input);

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
            SelectElement selectElement = new SelectElement(dropdown);
            string input = "Æble";
            selectElement.SelectByValue(input);
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
            SelectElement selectElement = new SelectElement(dropdown);
            string input = "Hvidløg";
            selectElement.SelectByValue(input);

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
            SelectElement selectElement = new SelectElement(dropdown);
            string input = "Banan";
            selectElement.SelectByValue(input);

            string expected = "https://www.themealdb.com/images/media/meals/sywswr1511383814.jpg";
            Thread.Sleep(500);
            IWebElement firstRecipe = driver.FindElement(By.Id("Banana Pancakes.img"));
            string actual = firstRecipe.GetDomAttribute("src");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetImageForApple()
        {
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.Click();
            SelectElement selectElement = new SelectElement(dropdown);
            string input = "Æble";
            selectElement.SelectByValue(input);

            string expected = "https://themealdb.com/images/ingredients/Apple.png";
            IWebElement selectedFoodPicture = driver.FindElement(By.Id("ChosenFoodImage"));
            string actual = selectedFoodPicture.GetAttribute("src");
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
        }

    }


}