namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ReconsiderBloodRequestPage
    {
        private readonly IWebDriver _driver;
        public const string URI = "http://localhost:4200/app/blood-request-view";
    
        public ReconsiderBloodRequestPage(IWebDriver driver) 
        {
            _driver = driver;
        }

        public void EnsurePageIsDisplayed()
        {

        }    

    }
}
