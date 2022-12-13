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
        public IWebElement Table;
        public IWebElement WantedRow;
        public IWebElement Row;
        public IWebElement PublishButton;
        public IWebElement UnpublishButton;

        public ManagerFeedbackPage(IWebDriver driver)
        {
            this.driver = driver;
        }
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
                    Row = driver.FindElement(By.TagName("tr"));
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
        public void EnsureOnPublishedTab()
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
        public string AcceptFeedback()
        {
            string id;
            Table = driver.FindElement(By.TagName("table"));
            PublishButton = Table.FindElement(By.Id("publish"));
            WantedRow = PublishButton.FindElement(By.XPath("./../../..")); //find parent element to get id so that i can undo changes
            id = WantedRow.GetAttribute("id");
            PublishButton.Click();
            return id;
        }
        public bool CheckIfApproved(string id)
        {
            EnsureOnPublishedTab();
            IWebElement found = driver.FindElement(By.Id(id));
            if (found is not null) return true;
            else return false;
        }
        public void UndoChanges(string id)
        {
            IWebElement row = driver.FindElement(By.Id(id));
            UnpublishButton = row.FindElement(By.Id("unpublish"));
            UnpublishButton.Click();
        }
    }
}
