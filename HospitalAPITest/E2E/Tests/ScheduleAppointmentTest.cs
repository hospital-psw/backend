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
        private Pages.LoginPage loginPage;
        private Pages.MenuPage menuPage;
        private Pages.AppointmentsCalendarPage appointmentsCalendarPage;
        public const string URI_APPOINTMENTS = "http://localhost:4200/app/appointments";



        public ScheduleAppointmentTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            
            options.AddArguments("disable-infobars");           
            options.AddArguments("--disable-extensions");       
            options.AddArguments("--disable-gpu");             
            options.AddArguments("--disable-dev-shm-usage");    
            options.AddArguments("--no-sandbox");               
            options.AddArguments("--disable-notifications");


            driver = new ChromeDriver(options);

            loginPage = new Pages.LoginPage(driver);
            loginPage.Navigate();
            loginPage.EnsurePageIsDisplayed();
            loginPage.insertEmail("andrija@example.com");
            loginPage.insertPassword("123.Auth");
            loginPage.SubmitForm();
            loginPage.WaitToRedirectToDoctorsApp();



            appointmentsCalendarPage = new Pages.AppointmentsCalendarPage(driver);
            appointmentsCalendarPage.EnsurePageIsDisplayed();
            appointmentsCalendarPage.GoToScheduling();

            scheduleAppointmentPage = new Pages.ScheduleAppointmentPage(driver);      // create ProductsPage
            scheduleAppointmentPage.EnsurePageIsDisplayed();


        }

        [Fact]
        public void Choose_specific_date()
        {
            ChooseAppointmentParameters();
            scheduleAppointmentPage.Sumbit();
            ChooseAppointment();
            //scheduleAppointmentPage.EnsureToastNotificationAppeared();
            scheduleAppointmentPage.EnsureURLChanged();
            Assert.Equal(URI_APPOINTMENTS, driver.Url);
            Dispose();
        }

        [Fact]
        public void Choose_today()
        {
            ChooseToday();
            scheduleAppointmentPage.Sumbit();
            ChooseAppointment();
            //scheduleAppointmentPage.EnsureToastNotificationAppeared();
            scheduleAppointmentPage.EnsureURLChanged();
            Assert.Equal(URI_APPOINTMENTS, driver.Url);
            Dispose();

        }

        private void ChooseToday()
        {
            scheduleAppointmentPage.SelectTodaysDate();
            scheduleAppointmentPage.SelectPatient();
            scheduleAppointmentPage.SelectExaminationType();
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



        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
