namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentsCalendarPage
    {

        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/appointments";

        IWebElement scheduleButton => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-appointments/div[1]/div[3]/button[1]"));

        public AppointmentsCalendarPage(IWebDriver driver)
        {
            this.driver = driver;
        }



        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return scheduleButton != null;
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

        public void GoToScheduling()
        {
            scheduleButton.Click();
        }

    }
}
