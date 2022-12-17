namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BloodBanksPage
    {
        private readonly ChromeDriver _driver;
        public const string URI = "http://localhost:4200/app/bloodbank";

        public IWebElement FirstBloodBank => _driver.FindElement(By.XPath("html/body/app-root/app-application-main/div/div[2]/div/app-all/mat-card/div/table/tbody/tr[1]/td[1]"));

        public BloodBanksPage(ChromeDriver driver)
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
                    return FirstBloodBank != null;
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

        public void WaitToRedirectToBBDetailsPage()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(BloodBankDetailsPage.URI));
        }
    }
}
