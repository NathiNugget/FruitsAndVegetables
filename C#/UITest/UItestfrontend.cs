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
            _foodRepo.Nuke(); 
            _foodRepo.Setup();
            _userRepo.Nuke();
            _userRepo.SetUp();
            _readingRepo.Nuke();
            _readingRepo.Setup();
            
        }
        static FoodDB _foodRepo = new(true); 
        static UserDB _userRepo = new(true);
        static ReadingsDB _readingRepo = new(true);
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
            string expected = "4 timer";
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

        [TestMethod]
        public void AdminLogin()
        {
            string usernameInput = "Jacob";
            string passwordInput = "Hahaxd";
            User before = _userRepo.Get(usernameInput, passwordInput);
            IWebElement toggle = driver.FindElement(By.Id("AdminPanelToggle"));
            toggle.Click();
            IWebElement nameField = driver.FindElement(By.Id("AdminUsername"));
            IWebElement passwordField = driver.FindElement(By.Id("AdminPassword"));
            IWebElement loginButton = driver.FindElement(By.Id("LoginButton"));

            nameField.SendKeys(usernameInput);
            passwordField.SendKeys(passwordInput);
            loginButton.Click();
            Thread.Sleep(500);
            User after = _userRepo.Get(usernameInput, passwordInput);
            Thread.Sleep(500);
            IWebElement logoutButton = driver.FindElement(By.Id("LogoutButton"));
            logoutButton.Click();
            Assert.AreNotEqual(before.SessionToken, after.SessionToken);
        }

        [TestMethod]
        public void AdminLogout()
        {
            string usernameInput = "Jacob";
            string passwordInput = "Hahaxd";

            IWebElement toggle = driver.FindElement(By.Id("AdminPanelToggle"));
            toggle.Click();
            IWebElement nameField = driver.FindElement(By.Id("AdminUsername"));
            IWebElement passwordField = driver.FindElement(By.Id("AdminPassword"));
            IWebElement loginButton = driver.FindElement(By.Id("LoginButton"));

            nameField.SendKeys(usernameInput);
            passwordField.SendKeys(passwordInput);
            loginButton.Click();
            Thread.Sleep(800);
            Cookie tokenLoggedIn = driver.Manage().Cookies.GetCookieNamed("sessiontoken");
            //User before = _userRepo.Get(usernameInput, passwordInput);
            IWebElement logoutButton = driver.FindElement(By.Id("LogoutButton"));
            logoutButton.Click();
            Thread.Sleep(800);
            Cookie tokenLoggedOut = driver.Manage().Cookies.GetCookieNamed("sessiontoken");
            string? tokenLoggedOutValue;
            try
            {
                tokenLoggedOutValue = tokenLoggedOut.Value;
            }
            catch (Exception ex)
            {
                tokenLoggedOutValue = null;
            }
       
            
            //User after = _userRepo.Get(usernameInput, passwordInput);
            Assert.AreNotEqual(tokenLoggedIn.Value, tokenLoggedOutValue);
        }

        [TestMethod]
        public void LoginAndSendFormPass()
        {

            string usernameInput = "Jacob";
            string passwordInput = "Hahaxd";
            int newfoodid = 1;
            string newfoodname = "pære";
            string newfoodapi = "peir man";
            byte newspoildate = 20;
            byte newspoilhours = 15;
            double newq10factor = 2;
            double newmintemp = 5;
            double newmaxtemp = 40;
            double newidealtemperature = 20;
            double newidealhumidity = 20;


            IWebElement toggle = driver.FindElement(By.Id("AdminPanelToggle"));
            toggle.Click();
            IWebElement nameField = driver.FindElement(By.Id("AdminUsername"));
            IWebElement passwordField = driver.FindElement(By.Id("AdminPassword"));
            IWebElement loginButton = driver.FindElement(By.Id("LoginButton"));

            nameField.SendKeys(usernameInput);
            passwordField.SendKeys(passwordInput);
            loginButton.Click();
            Thread.Sleep(500);
            IWebElement foodtypeid = driver.FindElement(By.Id("newfoodTypeId"));
            SelectElement selectElement = new SelectElement(foodtypeid);
            IWebElement foodname = driver.FindElement(By.Id("newfoodname"));
            IWebElement foodapi = driver.FindElement(By.Id("newfoodapi"));
            IWebElement spoildate = driver.FindElement(By.Id("newspoildate"));
            IWebElement spoilhours = driver.FindElement(By.Id("spoilhours"));
            IWebElement q10factor = driver.FindElement(By.Id("q10factor"));
            IWebElement mintemp = driver.FindElement(By.Id("mintemp"));
            IWebElement maxtemp = driver.FindElement(By.Id("maxtemp"));
            IWebElement idealtemperature = driver.FindElement(By.Id("idealtemperature"));
            IWebElement idealhumidity = driver.FindElement(By.Id("idealhumidity"));
            IWebElement value1 = driver.FindElement(By.Id("value1"));

            selectElement.SelectByValue("1");
            foodname.SendKeys(newfoodname);
            foodapi.SendKeys(newfoodapi.ToString());
            spoildate.SendKeys(newspoildate.ToString());
            spoilhours.SendKeys(newspoilhours.ToString());
            q10factor.SendKeys(newq10factor.ToString());
            mintemp.SendKeys(newmintemp.ToString());
            maxtemp.SendKeys(newmaxtemp.ToString());
            idealtemperature.SendKeys(newidealtemperature.ToString());
            idealhumidity.SendKeys(newidealhumidity.ToString());


            IWebElement addfoodbutton = driver.FindElement(By.Id("addfoodbutton"));
            addfoodbutton.Click();
            Thread.Sleep(1000);
            IWebElement exit = driver.FindElement(By.Id("exitButton"));
            exit.Click();
            Thread.Sleep(500);
            IWebElement dropdown = driver.FindElement(By.Id("FoodDropdown"));
            dropdown.SendKeys(newfoodname);
            Thread.Sleep(500);
            dropdown.SendKeys(Keys.Enter);
            IWebElement selectedFood = driver.FindElement(By.Id("pære"));
            Assert.IsNotNull(selectedFood);


        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver.Quit();
            _foodRepo.Nuke(); 
        }

    }


}