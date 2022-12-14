namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class CheckBloodAmountPage
    {
        private readonly ChromeDriver _driver;
        public const string URI = "http://localhost:4200/bloodbank/46/detail";

        public IWebElement Email => _driver.FindElement(By.Id("email"));
        public IWebElement Url => _driver.FindElement(By.Id("url"));
        public IWebElement ApiKey => _driver.FindElement(By.Id("apikey"));
        public IWebElement BloodType => _driver.FindElement(By.Id("bloodtype"));
        public IWebElement BloodAmount => _driver.FindElement(By.Id("bloodamount"));
        public IWebElement CheckButton => _driver.FindElement(By.Id("check"));


        public CheckBloodAmountPage(ChromeDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(URI);
        }

    }
}
