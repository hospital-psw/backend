namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class LoginPage
    {
        private readonly ChromeDriver _driver;
        public const string URI = "http://localhost:4200/";

        public IWebElement Email => _driver.FindElement(By.Name("email"));
        public IWebElement Password => _driver.FindElement(By.Name("password"));

        public IWebElement SubmitButton => _driver.FindElement(By.ClassName("submit-button"));

        public LoginPage(ChromeDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(URI);
        }
    }
}
