namespace HospitalAPITest.E2E.Tests
{
    using HospitalAPITest.E2E.Pages;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using System;

    public class ScheduleAppointmentTest : IDisposable
    {

        private readonly IWebDriver driver;
        private Pages.ScheduleAppointmentPage scheduleAppointmentPage;
        public const string URI_APPOINTMENTS = "http://localhost:4200/appointments";


        public ScheduleAppointmentTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");


            driver = new ChromeDriver(options);


            scheduleAppointmentPage = new Pages.ScheduleAppointmentPage(driver);      // create ProductsPage
            scheduleAppointmentPage.Navigate();                            // navigate to url
            scheduleAppointmentPage.EnsurePageIsDisplayed();


        }

        [Fact]
        public void Test()
        {
            ChooseAppointmentParameters();
            scheduleAppointmentPage.Sumbit();
            ChooseAppointment();
            EnsureURLChanged();
            Assert.Equal(URI_APPOINTMENTS, driver.Url);
            Dispose();
        }



        private void ChooseAppointmentParameters()
        {

            scheduleAppointmentPage.SelectDate();
            scheduleAppointmentPage.SelectPatient();
            scheduleAppointmentPage.SelectExaminationType();
        }

        private void ChooseAppointment()
        {
            scheduleAppointmentPage.EnsureCardsAreDisplayed();
            scheduleAppointmentPage.SelectAppointmentCard();
        }

        private void EnsureURLChanged()
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

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
