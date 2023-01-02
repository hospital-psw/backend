namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MenuDoctorPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/appointments";

        IWebElement consiliumTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/app-doctor-sidebar/li[3]"));

        IWebElement treatmentsTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/app-doctor-sidebar/li[2]"));
        public MenuDoctorPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool consiliumTabDisplayed()
        {
            return consiliumTab.Displayed;
        }

        public void consiliumTabClick()
        {
            consiliumTab.Click();
        }

        public bool TreatmentsTabDisplayed()
        {
            return treatmentsTab.Displayed;
        }

        public void TreatmentsTabClick()
        {
            treatmentsTab.Click();
        }

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 45));
            wait.Until(condition =>
            {
                try
                {
                    return consiliumTab.Displayed == true;
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
