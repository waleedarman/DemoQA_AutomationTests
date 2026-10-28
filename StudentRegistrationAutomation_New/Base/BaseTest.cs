using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Interfaces;

namespace StudentRegistrationAutomation_New.Base
{
    public class BaseTest
    {
        protected IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                string folder = Path.Combine(TestContext.CurrentContext.TestDirectory, "Screenshots");
                Directory.CreateDirectory(folder);

                string testName = TestContext.CurrentContext.Test.Name;
                string time = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                string fileName = Path.Combine(folder, testName + "_" + time + ".png");

                screenshot.SaveAsFile(fileName);
            }

            driver.Quit();
            driver.Dispose();
        }
    }
}