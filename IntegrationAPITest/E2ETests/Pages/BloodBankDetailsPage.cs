namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    public class BloodBankDetailsPage
    {
        private readonly IWebDriver _driver;
        public const string URI = "http://localhost:4200/app/bloodbank/46/detail";

        public IWebElement BloodType => _driver.FindElement(By.Id("bloodtype"));
        public IWebElement BloodAmount => _driver.FindElement(By.Id("bloodamount"));
        public IWebElement APositive => _driver.FindElement(By.XPath("html/body/app-root/div[2]/div[2]/div/div/mat-option"));
        public IWebElement ToastPopup => _driver.FindElement(By.XPath("//div[@class='toast-message']"));

        public IWebElement ShowConfig => _driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-detail/mat-card/div[1]/p"));
        public IWebElement Frequent;
        public IWebElement SaveReport;
        public IWebElement toast;
        public BloodBankDetailsPage(IWebDriver driver)
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
                    return ShowConfig != null;
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

        public Boolean GetToast()
        {
            toast = _driver.FindElement(By.XPath("/html/body/div[1]/div/div/div"));
            if (toast == null)
                return false;
            else
                return true;
        }
        public bool EnsureToastrPopup()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            return ToastPopup != null;
        }

        public void EnsureConfigIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    SaveReport = _driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-detail/mat-card/div[2]/fieldset[1]/div/div[4]/button"));
                    Frequent = _driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-detail/mat-card/div[2]/fieldset[1]/div/div[1]/mat-form-field/div/div[1]/div/input"));
                    return SaveReport != null && Frequent !=null;
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
    }
}
