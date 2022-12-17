namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

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
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return Email != null && Password != null;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }
        
        public void WaitToRedirectToMenuPage()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(MenuPage.URI));
        }
    }
}
