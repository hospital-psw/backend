namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MenuPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/display";

        IWebElement managerTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/app-manager-sidebar/li[1]/a"));
        IWebElement blockedPatientsTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/li[4]/a"));
        IWebElement managerVacationRequestsTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/app-manager-sidebar/li[2]/a"));
        IWebElement feedbackTab => driver.FindElement(By.Id("feedback"));
        IWebElement consiliumTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/li[9]"));
        public MenuPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public bool managerTabDisplayed()
        {
            return managerTab.Displayed;
        }
        public void managerTabClick()
        {
            managerTab.Click();
        }
        public bool blockedPatientsTabDisplayed()
        {
            return blockedPatientsTab.Displayed;
        }
        public void blockedPatientsTabClick()
        {
            blockedPatientsTab.Click();
        }

        public bool managerVacationRequestsTabDisplayed()
        {
            return managerVacationRequestsTab.Displayed;
        }

        public void managerVacationRequestsTabClick()
        {
            managerVacationRequestsTab.Click();
        }
        public void feedbackTabClick()
        {
            feedbackTab.Click();
        }

        public bool consiliumTabDisplayed()
        {
            return consiliumTab.Displayed;
        }

        public void consiliumTabClick()
        {
            consiliumTab.Click();
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
