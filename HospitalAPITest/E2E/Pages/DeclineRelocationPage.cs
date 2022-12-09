namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit.Sdk;

    public class DeclineRelocationPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/display";
        private IWebElement buildingField => driver.FindElement(By.XPath("//*[@id=\"buildingSelect\"]"));
        private IWebElement buildingFieldBuilding2 => driver.FindElement(By.XPath("//*[@id=\"mat-option-0\"]"));
        private IWebElement declineRelocationButton => driver.FindElement(By.XPath("//*[@id=\"decline-relocation\"]"));
        private IWebElement tabsRelocation;
        private IWebElement tabRelocation;
        private IWebElement declineButton;
        public DeclineRelocationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public bool buildingDisplayed()
        {
            return buildingField.Displayed;
        }
        public bool declineRelocationButtonDisplayed()
        {
            return declineRelocationButton.Displayed;
        }
        public void EnsureTabIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 2000));
            wait.Until(condition =>
            {
                try
                {
                    return driver.FindElement(By.XPath("//*[@id=\"mat-tab-label-0-2\"]/div")) != null;//tabsRelocation != null;//driver.FindElement(By.TagName("//*[@id=\"relocationsLabel\"]"));
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

        public void SelectBuilding()
        {
            buildingField.Click();
            buildingFieldBuilding2.Click();
        }
        public void SelectRoom()
        {
            Actions action = new Actions(driver);
            Thread.Sleep(1000);
            action.MoveByOffset(897, 415).Click().Perform(); //897 415, 539 260
            Thread.Sleep(1000);
        }
        public void SelectTab()
        {
            tabsRelocation = driver.FindElement(By.XPath("//*[@id=\"tabs\"]"));
            tabRelocation = driver.FindElement(By.XPath("//*[@id=\"mat-tab-label-0-2\"]/div"));//driver.FindElement(By.XPath("//*[@id=\"relocationsLabel\"]"));
            tabRelocation.Click();
            Thread.Sleep(1000);

        }

        public void Decline()
        {
            declineButton = driver.FindElement(By.XPath("//*[@id=\"decline-relocation\"]/span[1]"));
            declineButton.Click();
            Thread.Sleep(1000);
        }

        public int RequestsCount()
        {
            return driver.FindElements(By.XPath("//*[@id=\"mat-tab-content-0-2\"]/div/app-relocations/div/table/tbody/tr")).Count;
        }

    }
}
