namespace IntegrationAPITest.E2ETests.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class CheckBloodAmountPage
    {
        private readonly ChromeDriver _driver;
        public const string URI = "http://localhost:4200/app/bloodbank/46/detail";

        public IWebElement Email => _driver.FindElement(By.Id("email"));
        public IWebElement Url => _driver.FindElement(By.Id("url"));
        public IWebElement ApiKey => _driver.FindElement(By.Id("apikey"));
        public IWebElement BloodType => _driver.FindElement(By.Id("bloodtype"));
        public IWebElement BloodAmount => _driver.FindElement(By.Id("bloodamount"));
        public IWebElement CheckButton => _driver.FindElement(By.Id("check"));
        public IWebElement ShowConfig => _driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-detail/mat-card/div[1]/p"));

        public IWebElement Freq => _driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-detail/mat-card/div[2]/fieldset[1]/div/div[1]/mat-form-field/div/div[1]/div/input"));

        public IWebElement toast;
        public IWebElement SaveReport => _driver.FindElement(By.XPath("/html/body/app-root/app-application-main/div/div[2]/div/app-detail/mat-card/div[2]/fieldset[1]/div/div[4]/button"));
        public CheckBloodAmountPage(ChromeDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(URI);
        }

        public Boolean GetToast()
        {
            toast = _driver.FindElement(By.XPath("/html/body/div[2]/div/div/div"));
            if (toast == null)
                return false;
            else
                return true;
        }
    }
}
