using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace StudentRegistrationAutomation_New.Base
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
    }
}