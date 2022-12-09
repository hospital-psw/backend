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
        public const string URI = "http://localhost:4200/appointments";

        IWebElement managerTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/li[3]/a"));
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

        public void WaitForTab()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(DeclineRelocationPage.URI));
        }
    }
}
