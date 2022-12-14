namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ConsiliumsPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/consiliums";

        private IWebElement ScheduleFormButton => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-all-consiliums/div/div"));

        public ConsiliumsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public bool ButtonDisplayed()
        {
            return ScheduleFormButton.Displayed;
        }

        public void ButtonClick()
        {
            ScheduleFormButton.Click();
        }

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 5, 0));
            wait.Until(condition =>
            {
                try
                {
                    return ScheduleFormButton != null;
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
