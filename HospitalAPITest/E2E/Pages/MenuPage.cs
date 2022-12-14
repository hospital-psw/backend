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
        public const string URI = "http://localhost:4200/app";

        IWebElement managerTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/li[3]/a"));
        IWebElement blockedPatientsTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/li[4]/a"));
        IWebElement managerVacationRequestsTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/li[6]/a"));
        IWebElement feedbackTab => driver.FindElement(By.Id("feedback"));
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
    }
}
