namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class BloodBankDetailsPage
    {
        private readonly ChromeDriver _driver;
        public const string URI = "http://localhost:4200/app/bloodbank/46/detail";

        public IWebElement BloodType => _driver.FindElement(By.Id("bloodtype"));
        public IWebElement BloodAmount => _driver.FindElement(By.Id("bloodamount"));
        public IWebElement APositive => _driver.FindElement(By.XPath("html/body/app-root/div[2]/div[2]/div/div/mat-option"));

        public BloodBankDetailsPage(ChromeDriver driver)
        {
            _driver = driver;
        }
    }
}
