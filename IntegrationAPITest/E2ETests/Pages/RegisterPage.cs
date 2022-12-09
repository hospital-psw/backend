namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class RegisterPage
    {
        private readonly ChromeDriver _driver;
        public const string URI = "http://localhost:4200/changePassword";

        public IWebElement Email => _driver.FindElement(By.Id("email"));
        public IWebElement Password => _driver.FindElement(By.Id("password"));
        public IWebElement SubmitButton => _driver.FindElement(By.Id("submit"));

        public RegisterPage(ChromeDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(URI);
        }
    }
}
