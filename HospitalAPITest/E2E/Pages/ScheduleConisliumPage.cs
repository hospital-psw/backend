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
    using System.Xml.Linq;

    public class ScheduleConisliumPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/schedule-consilium";

        private IWebElement RoomSelectionOption => driver.FindElement(By.XPath("//*[@id=\"mat-select-0\"]"));

        private IWebElement SelectedRoom => driver.FindElement(By.XPath("//*[@id=\"mat-option-2\"]"));

        private IWebElement DateRangeSelectionOption => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-schedule-consilium/div/div[1]/app-other-info/div/div[2]/div[2]/mat-form-field/div/div[1]/div/div/mat-datepicker-toggle/button"));

        private IWebElement SelectedFromDate => driver.FindElement(By.XPath("//*[@id=\"mat-datepicker-1\"]/div/mat-month-view/table/tbody/tr[2]/td[1]/button"));

        private IWebElement SelectedToDate => driver.FindElement(By.XPath("//*[@id=\"mat-datepicker-0\"]/div/mat-month-view/table/tbody/tr[2]/td[3]/button"));

        private IWebElement TopicTextArea => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-schedule-consilium/div/div[1]/app-other-info/div/div[2]/textarea"));

        private IEnumerable<IWebElement> SelectedDoctors => driver.FindElements(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-schedule-consilium/div/div[2]/app-doctors-list/div/div[2]/mat-selection-list"));

        private IEnumerable<IWebElement> SelectedSpecializations => driver.FindElements(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-schedule-consilium/div/div[2]/app-specializations-list/div/div[2]/mat-selection-list"));

        private IWebElement ScheduleButton => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-schedule-consilium/div/div[2]/app-button-space/div/div"));

        public ScheduleConisliumPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public bool RoomSelectionDisplayed()
        {
            return RoomSelectionOption.Displayed;
        }

        public bool TextAreaDisplayed()
        {
            return TopicTextArea.Displayed;
        }

        public bool ScheduleButtonDisplayed()
        {
            return ScheduleButton.Displayed;
        }

        public void Submit()
        {
            ScheduleButton.Click();   
        }

        public void SelectRoom()
        {
            RoomSelectionOption.Click();
            SelectedRoom.Click();
        }

        public void SelectDateRange()
        {
            Actions action = new Actions(driver);
            action.MoveToElement(DateRangeSelectionOption).Click();
            SelectedFromDate.Click();
            SelectedToDate.Click();
        }

        public void TypeTopic()
        {
            TopicTextArea.Click();
        }

        public void SelectDoctors()
        {
            SelectedDoctors.ElementAt(0).Click();
            SelectedDoctors.ElementAt(1).Click();
        }

        public void SelectSpecializations()
        {
            SelectedSpecializations.ElementAt(0).Click();
            SelectedSpecializations.ElementAt(1).Click();
        }

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return ScheduleButton != null;
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

    }
}
