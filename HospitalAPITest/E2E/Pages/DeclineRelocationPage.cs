﻿namespace HospitalAPITest.E2E.Pages
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

        private IWebElement canvas => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-view-rooms/div/div/div[2]/canvas"));
        private IWebElement button => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-view-rooms/div/div[2]/app-show-room-details/div/app-tabs-details/mat-tab-group/mat-tab[3]/app-relocations/div/table/ng-container[6]/button"));
        private IWebElement buildingField => driver.FindElement(By.XPath("//*[@id=\"buildingSelect\"]"));
        private ReadOnlyCollection<IWebElement>  buildingFields => driver.FindElements(By.XPath("//*[@id=\"buildingSelect\"]/mat-option"));
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
            //_ = driver.Manage().Timeouts().ImplicitWait;
            buildingFieldBuilding2.Click();
        }
        public void SelectRoom()
        {
            Actions action = new Actions(driver);
            Thread.Sleep(1000);
            action.MoveByOffset(897, 415).Click().Perform(); //897 415, 539 260
            Thread.Sleep(1000);
            //action.Click();
        }
        public void SelectTab()
        {
            tabsRelocation = driver.FindElement(By.XPath("//*[@id=\"tabs\"]"));
            tabRelocation =  driver.FindElement(By.XPath("//*[@id=\"mat-tab-label-0-2\"]/div"));//driver.FindElement(By.XPath("//*[@id=\"relocationsLabel\"]"));
            tabRelocation.Click();
            Thread.Sleep(1000);
        }

        public void Decline()
        {
            declineButton = driver.FindElement(By.XPath("//*[@id=\"decline-relocation\"]/span[1]"));
            declineButton.Click();
        }

        public int RequestsCount()
        {
            return driver.FindElements(By.XPath("//*[@id=\"mat-tab-content-0-2\"]/div/app-relocations/div/table/tbody/tr")).Count;
        }

    }
}