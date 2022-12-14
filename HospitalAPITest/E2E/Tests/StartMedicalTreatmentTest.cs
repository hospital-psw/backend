namespace HospitalAPITest.E2E.Tests
{
    using HospitalAPITest.E2E.Pages;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StartMedicalTreatmentTest : IDisposable
    {
        private readonly IWebDriver driver;
        private LoginPage loginPage;
        private MenuDoctorPage menuDoctorPage;
        private TreatmentsPage treatmentsPage;

        public StartMedicalTreatmentTest()
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

            loginPage = new LoginPage(driver);
            loginPage.Navigate();
            Assert.True(loginPage.loginButtonDisplayed());
            Assert.True(loginPage.emailInputDisplayed());
            Assert.True(loginPage.passwordInputDisplayed());
            loginPage.insertEmail("andrija@example.com");
            loginPage.insertPassword("123.Auth");
            loginPage.SubmitForm();
            loginPage.WaitToRedirectToDoctorsApp();

            menuDoctorPage = new MenuDoctorPage(driver);
            menuDoctorPage.EnsurePageIsDisplayed();
            menuDoctorPage.TreatmentsTabClick();

            treatmentsPage = new TreatmentsPage(driver);
            treatmentsPage.EnsurePageIsDisplayed();
            Assert.True(treatmentsPage.ButtonDisplayed());

            treatmentsPage.OpenDialog();
            treatmentsPage.EnsureDialogIsDisplayed();
            
        }

        [Fact]
        public void CreateStationaryTreatment()
        {
            FillTreatmentForm();
            treatmentsPage.Finish();
            treatmentsPage.RefreshTable();
            Assert.Equal(treatmentsPage.GetRowCount(), treatmentsPage.GetRefreshedRowCount());
            //Dispose();
        }

        [Fact]
        public void FailToCreate()
        {
            FailToFill();
            treatmentsPage.Finish();
            Assert.Equal(false, treatmentsPage.DialogClosed());
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        private void FillTreatmentForm()
        {
            treatmentsPage.SelectPatient();
            treatmentsPage.SelectRoom();
            treatmentsPage.FillReason();
        }

        private void FailToFill()
        {
            treatmentsPage.FillReason();
            treatmentsPage.SelectRoom();
        }
    }
}
