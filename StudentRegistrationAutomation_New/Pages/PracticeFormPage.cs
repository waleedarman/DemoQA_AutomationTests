using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using StudentRegistrationAutomation_New.Base;
using System.Globalization;

namespace StudentRegistrationAutomation_New.Pages
{
    public class PracticeFormPage : BasePage
    {
        public PracticeFormPage(IWebDriver driver) : base(driver)
        {
        }

        By firstName = By.Id("firstName");
        By lastName = By.Id("lastName");
        By email = By.Id("userEmail");
        By mobile = By.Id("userNumber");
        By address = By.Id("currentAddress");

        By genderMale = By.XPath("//label[text()='Male']");
        By genderFemale = By.XPath("//label[text()='Female']");
        By genderOther = By.XPath("//label[text()='Other']");

        By dateOfBirth = By.Id("dateOfBirthInput");
        By month = By.ClassName("react-datepicker__month-select");
        By year = By.ClassName("react-datepicker__year-select");

        By subject = By.Id("subjectsInput");

        By hobbySports = By.XPath("//label[text()='Sports']");
        By hobbyReading = By.XPath("//label[text()='Reading']");
        By hobbyMusic = By.XPath("//label[text()='Music']");

        By picture = By.Id("uploadPicture");
        By state = By.Id("state");
        By submit = By.Id("submit");

        By confirmationTitle = By.Id("example-modal-sizes-title-lg");

        public void EnterFirstName(string value)
        {
            driver.FindElement(firstName).SendKeys(value);
        }

        public void EnterLastName(string value)
        {
            driver.FindElement(lastName).SendKeys(value);
        }

        public void EnterEmail(string value)
        {
            driver.FindElement(email).SendKeys(value);
        }

        public void EnterMobile(string value)
        {
            driver.FindElement(mobile).SendKeys(value);
        }

        public void EnterAddress(string value)
        {
            driver.FindElement(address).SendKeys(value);
        }

        public void SelectGender(string gender)
        {
            if (gender == "Male")
                driver.FindElement(genderMale).Click();

            if (gender == "Female")
                driver.FindElement(genderFemale).Click();

            if (gender == "Other")
                driver.FindElement(genderOther).Click();
        }

        public void SelectDateOfBirth(string value)
        {
            driver.FindElement(dateOfBirth).Click();

            DateTime date = DateTime.ParseExact(value, "dd MMM yyyy", CultureInfo.InvariantCulture);

            string day = date.Day.ToString();
            string monthName = date.ToString("MMMM", CultureInfo.InvariantCulture);
            string yearNumber = date.Year.ToString();

            SelectElement monthList = new SelectElement(driver.FindElement(month));
            monthList.SelectByText(monthName);

            SelectElement yearList = new SelectElement(driver.FindElement(year));
            yearList.SelectByText(yearNumber);

            By dayOption = By.XPath("//div[contains(@class,'react-datepicker__day') and not(contains(@class,'outside-month')) and text()='" + day + "']");

            wait.Until(d => d.FindElement(dayOption)).Click();
        }

        public void SelectSubject(string value)
        {
            driver.FindElement(subject).SendKeys(value);

            By option = By.XPath("//div[contains(@class,'subjects-auto-complete__option') and text()='" + value + "']");

            wait.Until(d => d.FindElement(option)).Click();
        }

        public void SelectHobby(string hobby)
        {
            if (hobby == "Sports")
                driver.FindElement(hobbySports).Click();

            if (hobby == "Reading")
                driver.FindElement(hobbyReading).Click();

            if (hobby == "Music")
                driver.FindElement(hobbyMusic).Click();
        }

        public void UploadPicture(string path)
        {
            driver.FindElement(picture).SendKeys(path);
        }

        public void SelectState(string value)
        {
            IWebElement stateElement = wait.Until(d => d.FindElement(state));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            js.ExecuteScript("arguments[0].scrollIntoView({block:'center'});", stateElement);

            IWebElement stateInput = wait.Until(d => d.FindElement(By.Id("react-select-3-input")));

            stateInput.SendKeys(value);
            stateInput.SendKeys(Keys.Enter);
        }

        public void SelectCity(string value)
        {
            IWebElement cityInput = wait.Until(d => d.FindElement(By.Id("react-select-4-input")));

            cityInput.SendKeys(value);
            cityInput.SendKeys(Keys.Enter);
        }

        public void ClickSubmit()
        {
            IWebElement button = wait.Until(d => d.FindElement(submit));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            js.ExecuteScript("arguments[0].scrollIntoView({block:'center'});", button);
            js.ExecuteScript("arguments[0].click();", button);
        }

        public string GetConfirmationTitle()
        {
            string text = "";

            wait.Until(d =>
            {
                text = d.FindElement(confirmationTitle).Text;
                return text != "";
            });

            return text;
        }

        public string GetSubmittedValue(string fieldName)
        {
            By value = By.XPath("//td[text()='" + fieldName + "']/following-sibling::td");

            return wait.Until(d => d.FindElement(value)).Text;
        }

        public bool IsFirstNameInvalid()
        {
            return driver.FindElements(By.CssSelector("#firstName:invalid")).Count > 0;
        }

        public bool IsLastNameInvalid()
        {
            return driver.FindElements(By.CssSelector("#lastName:invalid")).Count > 0;
        }

        public bool IsGenderInvalid()
        {
            return driver.FindElements(By.CssSelector("input[name='gender']:invalid")).Count > 0;
        }

        public bool IsMobileInvalid()
        {
            return driver.FindElements(By.CssSelector("#userNumber:invalid")).Count > 0;
        }
    }
}