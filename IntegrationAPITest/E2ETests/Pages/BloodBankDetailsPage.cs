namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    public class BloodBankDetailsPage
    {
        private readonly ChromeDriver _driver;
        public const string URI = "http://localhost:4200/app/bloodbank/46/detail";

        public IWebElement BloodType => _driver.FindElement(By.Id("bloodtype"));
        public IWebElement BloodAmount => _driver.FindElement(By.Id("bloodamount"));
        public IWebElement APositive => _driver.FindElement(By.XPath("html/body/app-root/div[2]/div[2]/div/div/mat-option"));
        public IWebElement ToastPopup => _driver.FindElement(By.XPath("//div[@class='toast-message']"));

        public BloodBankDetailsPage(ChromeDriver driver)
        {
            _driver = driver;
        }

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return BloodType != null && BloodAmount != null && APositive != null;
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

        public bool EnsureToastrPopup()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            return ToastPopup != null;
        }

    }
}
