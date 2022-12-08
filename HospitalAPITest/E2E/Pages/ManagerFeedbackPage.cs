namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ManagerFeedbackPage
    {
        private readonly IWebDriver driver;
        public const string URL = "http://localhost:4200/feedback";

        private IWebElement PublishedButton => driver.FindElement(By.Id("show_published_button"));
        private IWebElement PendingButton => driver.FindElement(By.Id("show_pending_button"));
        public IWebElement Table;
        public IList<WebElement> TableRows;
        public IWebElement Row => driver.FindElement(By.TagName("tr"));
        public IWebElement PublishButton;
        public IWebElement UnpublishButton;

        public ManagerFeedbackPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URL);
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return PublishedButton != null;
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
        public void EnsureDataIsFetched()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return Row != null;
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
        public void EnsurePageSwapped()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    IWebElement title = driver.FindElement(By.TagName("h1"));
                    return title.Text == "PUBLISHED FEEDBACKS";
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

        public void GoToPublishedFeedbacks()
        {
            PublishedButton.Click();
        }
        public void GoToPendingFeedbacks()
        {
            PendingButton.Click();
        }
        public void AcceptFeedback()
        {
            Table = driver.FindElement(By.TagName("table"));
            IWebElement wantedRow = Table.FindElement(By.Id("8"));
            PublishButton = wantedRow.FindElement(By.Id("publish"));
            PublishButton.Click();
        }
        public bool CheckIfApproved()
        {
            IWebElement found = driver.FindElement(By.Id("8"));
            if(found is not null) return true;
            else return false;
        }
        public void UndoChanges()
        {
            EnsurePageSwapped();
            IWebElement row = driver.FindElement(By.Id("8"));
            UnpublishButton = row.FindElement(By.Id("unpublish"));
            UnpublishButton.Click();
        }
    }
}
