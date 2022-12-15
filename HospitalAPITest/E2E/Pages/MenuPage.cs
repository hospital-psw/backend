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


        IWebElement blockedPatientsTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/app-manager-sidebar/li[10]/a"));

        IWebElement managerTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/app-manager-sidebar/li[1]/a"));
        IWebElement managerVacationRequestsTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/app-manager-sidebar/li[2]/a"));
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
