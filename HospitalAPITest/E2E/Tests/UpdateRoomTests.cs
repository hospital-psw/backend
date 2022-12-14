namespace HospitalAPITest.E2E.Tests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UpdateRoomTests
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.MenuPage menuPage;
        private Pages.UpdateRoomPage updateRoomPage;
    
        public UpdateRoomTests()
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

            loginPage = new Pages.LoginPage(driver);
            loginPage.Navigate();
            Assert.True(loginPage.loginButtonDisplayed());
            Assert.True(loginPage.emailInputDisplayed());
            Assert.True(loginPage.passwordInputDisplayed());
            loginPage.insertEmail("maroko@gmail.com");
            loginPage.insertPassword("123.Auth");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            menuPage = new Pages.MenuPage(driver);
            Assert.True(menuPage.managerTabDisplayed());
            menuPage.managerTabClick();
            updateRoomPage = new Pages.UpdateRoomPage(driver);
            Assert.True(updateRoomPage.buildingDisplayed());
        }

        [Fact]
        public void TestSuccessfulSubmit()
        {
            ChooseParameters();
            updateRoomPage.UpdateRoom();
            Thread.Sleep(1000);
            Assert.Equal(Pages.UpdateRoomPage.URI, driver.Url);
            Dispose();
        }

        private void ChooseParameters()
        {
            updateRoomPage.SelectBuilding();
            updateRoomPage.SelectRoom();
            updateRoomPage.EnsureFormIsDisplayed();
            //updateRoomPage.ZoomOut();
            updateRoomPage.EnableFields();
            //updateRoomPage.insertNumber("009");
            updateRoomPage.insertPurpose("1");
            Thread.Sleep(200);
            
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
