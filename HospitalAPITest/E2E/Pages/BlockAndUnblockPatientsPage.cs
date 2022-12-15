namespace HospitalAPITest.E2E.Pages
{
    using MySqlX.XDevAPI.Relational;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BlockAndUnblockPatientsPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/blockpatients";

        //private IWebElement TabMalicious => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-block-patients-view/div/mat-tab-group/mat-tab"));
        //private IWebElement TabBlocked => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-block-patients-view/div/mat-tab-group/mat-tab[2]"));
        private IWebElement TabMalicious => driver.FindElement(By.XPath("//*[@id=\"mat-tab-label-0-0\"]"));
        private IWebElement TabBlocked => driver.FindElement(By.XPath("//*[@id=\"mat-tab-label-0-1\"]"));
        private IWebElement TableMalicious => driver.FindElement(By.Id("tableMalicious"));
        private ReadOnlyCollection<IWebElement> MaliciousRows => driver.FindElements(By.XPath("//table[@id='tableMalicious']/tbody/tr"));
        private IWebElement FirstRowMaliciousFirstName => driver.FindElement(By.XPath("//*[@id=\"tableMalicious\"]/tbody/tr[1]/td[1]"));
        private IWebElement FirstRowMaliciousLastName => driver.FindElement(By.XPath("//*[@id=\"tableMalicious\"]/tbody/tr[1]/td[2]"));
        private IWebElement FirstRowMaliciousStrikes => driver.FindElement(By.XPath("//*[@id=\"tableMalicious\"]/tbody/tr[1]/td[3]"));
        private IWebElement ButtonBlock => driver.FindElement(By.XPath("//*[@id=\"tableMalicious\"]/tbody/tr[1]/td[4]/button"));
        private IWebElement ButtonUnblock;// => driver.FindElement(By.XPath("//*[@id=\"tableBlocked\"]/tbody/tr[1]/td[3]/button"));
        private IWebElement TableBlocked;// => driver.FindElement(By.Id("tableBlocked"));
        private ReadOnlyCollection<IWebElement> BlockedRows;// => driver.FindElements(By.XPath("//table[@id='tableBlocked']/tbody/tr"));
        private IWebElement FirstRowBlockedFirstName;// => driver.FindElement(By.XPath("//*[@id=\"tableBlocked\"]/tbody/tr[1]/td[1]"));
        private IWebElement FirstRowBlockedLastName;// => driver.FindElement(By.XPath("//*[@id=\"tableBlocked\"]/tbody/tr[1]/td[2]"));

        public BlockAndUnblockPatientsPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        /*public void EnsureTabMalIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 2000));
            wait.Until(condition =>
            {
                try
                {
                    return TabMalicious != null;
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
        }*/
        public bool EnsureTabMalIsDisplayed()
        {
            return TabMalicious.Displayed;
        }
        public bool BlockButtonDisplayed()
        {
            return ButtonBlock.Displayed;
        }
        public void EnsureTabBlockedIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 2000));
            wait.Until(condition =>
            {
                try
                {
                    return TabBlocked != null;
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

        public void EnsureMalTableIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return MaliciousRows.Count > 0;
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
        public void EnsureBlockedTableIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    BlockedRows = driver.FindElements(By.XPath("//table[@id='tableBlocked']/tbody/tr"));
                    return BlockedRows.Count > 0;
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
        public void SelectBlockedTab()
        {
            TabBlocked.Click();
            Thread.Sleep(2000);
        }
        public void SelectMalTab()
        {
            TabMalicious.Click();
            Thread.Sleep(2000);
        }
        public void ClickBlockButton()
        {
            ButtonBlock.Click();
            Thread.Sleep(3000);
        }
        public void ClickUnblockButton()
        {
            ButtonUnblock = driver.FindElement(By.XPath("//*[@id=\"tableBlocked\"]/tbody/tr[1]/td[3]/button"));
            ButtonUnblock.Click();
            Thread.Sleep(5000);
        }
        public int BlockedPatientsCount()
        {
            return BlockedRows.Count;
        }
        public int MaliciousPatientsCount()
        {
            return MaliciousRows.Count;
        }
        public string GetFirstRowBlockedFirstName()
        {
            return FirstRowBlockedFirstName.Text;
        }
        public string GetFirstRowBlockedLastName()
        {
            return FirstRowBlockedLastName.Text;
        }
        public string GetFirstRowMaliciousFirstName()
        {
            return FirstRowMaliciousFirstName.Text;
        }
        public string GetFirstRowMaliciousLastName()
        {
            return FirstRowMaliciousLastName.Text;
        }
        public string GetFirstRowMaliciousStrikes()
        {
            return FirstRowMaliciousStrikes.Text;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}
