namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UpdateRoomPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/display";
        private IWebElement buildingField => driver.FindElement(By.XPath("//*[@id=\"buildingSelect\"]"));
        private IWebElement buildingFieldBuilding2 => driver.FindElement(By.XPath("//*[@id=\"mat-option-building\"]"));
        private IWebElement editButton;
        private IWebElement submitButton;
        private IWebElement numberInput;
        private IWebElement purposeInput;
        private IWebElement canvas => driver.FindElement(By.XPath("//*[@id=\"canvas - holder\"]/canvas"));
        public string Title => driver.Title;

        public UpdateRoomPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);
        //menjati!!!!
        public void EnsureFormIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 2000));
            wait.Until(condition =>
            {
                try
                {
                    return driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-view-rooms/div/div[2]/app-show-room-details/div/form/div/div[3]/button")) != null;//tabsRelocation != null;//driver.FindElement(By.TagName("//*[@id=\"relocationsLabel\"]"));
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

        public bool buildingDisplayed()
        {
            return buildingField.Displayed;
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
            action.MoveByOffset(788, 402).Click().Perform(); //897 415, 539 260
            /*IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollBy(0,250)");*/
            Thread.Sleep(1000);
        }

        public void ZoomOut()
        {
            Actions action = new Actions(driver);
            action.MoveByOffset(897, 415).ScrollByAmount(50, 50);
        }

        public bool numberInputDisplayed()
        {
            return numberInput.Displayed;
        }
        public bool purposeInputDisplayed()
        {
            return purposeInput.Displayed;
        }

        public bool editButtonDisplayed()
        {
            return editButton.Displayed;
        }

        public bool submitButtonDisplayed()
        {
            return submitButton.Displayed;
        }

        public void insertNumber(string number)
        {
            numberInput.SendKeys(number);
        }
        public void insertPurpose(string purpose)
        {
            purposeInput.SendKeys(purpose);
        }
        public void EnableFields()
        {
            editButton = driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-view-rooms/div/div[2]/app-show-room-details/div/form/div/div[3]/button"));
            editButton.Click();
            Thread.Sleep(1000);
            //numberInput = driver.FindElement(By.XPath("//*[@id=\"mat - input - 7\"]"));
            purposeInput = driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-view-rooms/div/div[2]/app-show-room-details/div/form/div/div[1]/mat-form-field[2]/div/div[1]/div/input"));
        }

        public void UpdateRoom()
        {
            submitButton = driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-view-rooms/div/div[2]/app-show-room-details/div/form/div/div[3]/input"));
            submitButton.Click();
            Thread.Sleep(1000);
        }

        public string GetNumber()
        {
            return numberInput.Text;
        }
        public string GetPurpose()
        {
            return purposeInput.Text;
        }
    }
}
