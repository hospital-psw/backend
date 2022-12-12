namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ScheduleRenovationPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/display";
        private IWebElement buildingField => driver.FindElement(By.XPath("//*[@id=\"buildingSelect\"]"));
        private IWebElement buildingFieldBuilding2 => driver.FindElement(By.XPath("//*[@id=\"mat-option-building\"]"));
        private IWebElement floorField => driver.FindElement(By.XPath("//*[@id=\"floorSelect\"]"));
        private IWebElement floorFieldFloor2 => driver.FindElement(By.XPath("//*[@id=\"mat-option-floor\"]"));

        public ScheduleRenovationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public bool buildingDisplayed()
        {
            return buildingField.Displayed;
        }

        public void SelectBuilding()
        {
            buildingField.Click();
            buildingFieldBuilding2.Click();
        }

        public void SelectFloor()
        {
            floorField.Click();
            floorFieldFloor2.Click();
        }

        public void ClickRenovatioButton()
        {
            driver.FindElement(By.XPath("//*[@id=\"renovation-button\"]")).Click();
        }

        public void SelectRenovationType()
        {
            driver.FindElement(By.XPath("//*[@id=\"mat-radio-button-type\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"next1\"]")).Click();
        }

        public void SelectFirstRoom()
        {
            driver.FindElement(By.XPath("//*[@id=\"selectFirstRoom\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"room0\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"next2\"]")).Click();
        }

        public void SelectDates()
        {
            driver.FindElement(By.XPath("//*[@id=\"datePicker1\"]")).SendKeys("12/26/2022");
            driver.FindElement(By.XPath("//*[@id=\"datePicker2\"]")).SendKeys("12/27/2022");
            driver.FindElement(By.XPath("//*[@id=\"next3\"]")).Click();
        }

        public void EnterDuration()
        {
            driver.FindElement(By.XPath("//*[@id=\"duration\"]")).SendKeys("5");
            driver.FindElement(By.XPath("//*[@id=\"next4\"]")).Click();
        }

        public void SelectStartTime()
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//*[@id=\"startTime0\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"next5\"]")).Click();
        }

        public void EnterNewRoomDetails()
        {
            driver.FindElement(By.XPath("//*[@id=\"newName1\"]")).SendKeys("015");
            driver.FindElement(By.XPath("//*[@id=\"newPurpose1\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"newPurpose1Option\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"newName2\"]")).SendKeys("016");
            driver.FindElement(By.XPath("//*[@id=\"newPurpose2\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"newPurpose2Option\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"schedule\"]")).Click();
            Thread.Sleep(6000);
        }
    }
}
