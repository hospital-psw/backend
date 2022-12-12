namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DisplayVacationRequestsPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/vacation-requests-display";
        private IWebElement expander => driver.FindElement(By.XPath("//*[@id=\"vacation-request0\"]"));
        private IWebElement expanderWrapper => driver.FindElement(By.ClassName("users-wrapper"));

        public DisplayVacationRequestsPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);



        public int GetExpanderCount()
        {
            Thread.Sleep(6000);
            var childs = expanderWrapper.FindElements(By.XPath("./child::*"));
            return childs.Count();
        }

        public void AcceptVacationRequest()
        {
            Thread.Sleep(6000);
            expander.FindElement(By.XPath("//*[@id=\"button1\"]")).Click();
        }
    }
}
