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

    public class MenuPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/display";

        IWebElement bloodBanksTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/app-manager-sidebar/li[3]/a"));
        IWebElement reconsiderRequestTab => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/app-manager-sidebar/li[9]/a"));

        public MenuPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public bool bloodBanksTabDisplayed()
        {
            return bloodBanksTab.Displayed;
        }

        public void bloodBanksTabClick()
        {
            bloodBanksTab.Click();
        }

        public bool reconsiderRequestTabDisplayed()
        {
            return reconsiderRequestTab.Displayed;
        }

        public void reconsiderRequestTabClick()
        {
            reconsiderRequestTab.Click();
        }
    }
}
