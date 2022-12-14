namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MenuPage
    {
        private readonly ChromeDriver _driver;
        public const string URI = "http://localhost:4200/app";

        public IWebElement BloodBanksTab => _driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[1]/app-sidebar/div/ul/li[6]/a"));

        public MenuPage(ChromeDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(URI);
        }
    }
}
