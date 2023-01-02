namespace HospitalAPITest.E2E.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TreatmentsPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/app/show-treatments";

        private IWebElement StartStationaryTreatment => driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-base-component/div/app-component-button-bar/div/div"));

        private IWebElement Dialog;

        private IWebElement PatientsDiv;

        private IWebElement RoomDiv;

        private IWebElement TextAreaSelect;

        private IWebElement SelectedPatient;

        private IWebElement SelectedRoom;

        private IWebElement CreateButton;

        private IWebElement BackButton;

        private IWebElement ActiveTable => driver.FindElement(By.XPath("//*[@id=\"mat-tab-content-0-0\"]/div/app-first-tab-component/div/div/table"));

        private IEnumerable<IWebElement> Treatments => ActiveTable.FindElements(By.TagName("tr"));

        private IEnumerable<IWebElement> RefreshedTreatments;

        private IEnumerable<IWebElement> Patients;

        public TreatmentsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);

        public bool ButtonDisplayed()
        {
            return StartStationaryTreatment.Displayed;
        }

        public bool DialogDisplayed()
        {
            return Dialog.Displayed;
        }

        public void OpenDialog()
        {
            StartStationaryTreatment.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            Dialog = wait.Until(e => e.FindElement(By.XPath("//*[@id=\"mat-dialog-0\"]")));

            CreateButton = wait.Until(e => e.FindElement(By.XPath("//*[@id=\"mat-dialog-0\"]/app-create-dialog-component/div/app-dialog-content-component/div/div[2]/div[2]")));
            BackButton = driver.FindElement(By.XPath("//*[@id=\"mat-dialog-0\"]/app-create-dialog-component/div/app-dialog-content-component/div/div[2]/div[1]"));
        }

        public void SelectPatient()
        {
            PatientsDiv = driver.FindElement(By.XPath("//*[@id=\"mat-select-4\"]"));
            driver.ExecuteJavaScript("arguments[0].click();", PatientsDiv);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            SelectedPatient = wait.Until(e => e.FindElement(By.XPath("//*[@id=\"mat-option-7\"]")));

            //Patients = PatientsDiv.FindElement(By.TagName("mat-option"));
            driver.ExecuteJavaScript("arguments[0].click();", SelectedPatient);
        }

        public void SelectRoom()
        {
            RoomDiv = driver.FindElement(By.XPath("//*[@id=\"mat-select-6\"]"));
            driver.ExecuteJavaScript("arguments[0].click();", RoomDiv);
            SelectedRoom = driver.FindElement(By.XPath("//*[@id=\"mat-option-20\"]"));
            driver.ExecuteJavaScript("arguments[0].click();", SelectedRoom);
        }

        public void FillReason()
        {
            TextAreaSelect = driver.FindElement(By.XPath("//*[@id=\"mat-dialog-0\"]/app-create-dialog-component/div/app-dialog-content-component/div/div[1]/textarea"));
            TextAreaSelect.SendKeys("Pacijent je dosao sa polomljenom nogom");
        }

        public void CloseDialog()
        {
            BackButton.Click();
        }

        public void Finish()
        {
            CreateButton.Click();
        }

        public bool DialogClosed()
        {
            return Dialog == null;
        }

        public int GetRowCount()
        {
            return Treatments.Count();
        }

        public int GetRefreshedRowCount()
        {
            return RefreshedTreatments.Count();
        }

        public void RefreshTable()
        {
            RefreshedTreatments = ActiveTable.FindElements(By.TagName("tr"));
        }

        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 5, 0));
            wait.Until(condition =>
            {
                try
                {
                    return StartStationaryTreatment != null;
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

        public void EnsureSelectIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return PatientsDiv != null;
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

        public void EnsureDialogIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return Dialog != null;
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
