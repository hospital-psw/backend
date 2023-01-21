namespace HospitalAPITest.E2E.Pages
{
    using Google.Protobuf.WellKnownTypes;
    using MySqlX.XDevAPI.Relational;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ScheduleAppointmentPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/appointments/scheduling";
        public const string URI_APPOINTMENTS = "http://localhost:4200/app/appointments";

        private IWebElement button => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-scheduling/div/app-scheduling-appointment-form/mat-card/div/div[2]/button"));
        private IWebElement examinationTypesField => driver.FindElement(By.XPath("//*[@id=\"examType\"]"));

        private IWebElement examinationTypeGeneral => driver.FindElement(By.XPath("//*[@id=\"mat-option-0\"]"));

        private IWebElement PatientsField => driver.FindElement(By.XPath("//*[@id=\"mat-select-2\"]"));
        private IWebElement SelectedPatient => driver.FindElement(By.XPath("//*[@id=\"mat-option-4\"]"));

        private IWebElement ViewDatesButton => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-scheduling/div/app-scheduling-appointment-form/mat-card/div/div[1]/div[1]/mat-form-field[1]/div/div[1]/div[2]/mat-datepicker-toggle/button"));

        private IWebElement SelectDateButton => driver.FindElement(By.XPath("//*[@id=\"mat-datepicker-0\"]/div/mat-month-view/table/tbody/tr[5]/td[7]/button"));


        private IWebElement SelectAppCard;

        private IWebElement SelectAppointmentButton;

        private IWebElement TodaysDate => driver.FindElement(By.XPath("//button[@class='mat-calendar-body-cell mat-calendar-body-active']"));




        public ScheduleAppointmentPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool ExaminationTypeDisplayed()
        {
            return button.Displayed;
        }


        public void Navigate() => driver.Navigate().GoToUrl(URI);


        public void Sumbit()
        {
            button.Click();
        }

        public void SelectExaminationType()
        {
            examinationTypesField.Click();
            examinationTypeGeneral.Click();
        }

        public void SelectDate()
        {
            ViewDatesButton.Click();
            SelectDateButton.Click();
        }

        public void SelectPatient()
        {

            PatientsField.Click();
            SelectedPatient.Click();
        }

        public void SelectAppointmentCard()
        {
            SelectAppCard = driver.FindElements(By.TagName("app-scheduling-appointment-card")).First();
            SelectAppointmentButton = SelectAppCard.FindElement(By.TagName("button"));
            SelectAppointmentButton.Click();
        }

        public void SelectTodaysDate()
        {
            ViewDatesButton.Click();
            TodaysDate.Click();
        }


        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return button != null;
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

        public void EnsureCardsAreDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return driver.FindElements(By.TagName("app-scheduling-appointment-card")).Count() > 0;
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

        public void EnsureToastNotificationAppeared()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return driver.FindElements(By.XPath("[@id=\"toast-container\"]")) != null;
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

        public void EnsureURLChanged()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return driver.Url == URI_APPOINTMENTS;
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
