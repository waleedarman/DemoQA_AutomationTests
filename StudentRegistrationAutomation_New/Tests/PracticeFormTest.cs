using StudentRegistrationAutomation_New.Base;
using StudentRegistrationAutomation_New.Models;
using StudentRegistrationAutomation_New.Pages;
using StudentRegistrationAutomation_New.Utils;
using System.Globalization;

namespace StudentRegistrationAutomation_New.Tests
{
    public class PracticeFormTest : BaseTest
    {
        public static IEnumerable<Student> GetStudents()
        {
            string filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "students.xlsx");

            return ExcelUtils.ReadStudents(filePath);
        }

        [TestCaseSource(nameof(GetStudents))]
        public void SubmitStudentForm(Student student)
        {
            PracticeFormPage form = new PracticeFormPage(driver);

            form.EnterFirstName(student.FirstName);
            form.EnterLastName(student.LastName);
            form.EnterEmail(student.Email);
            form.SelectGender(student.Gender);
            form.EnterMobile(student.Mobile);
            form.SelectDateOfBirth(student.DateOfBirth);
            form.SelectSubject(student.Subject);
            form.SelectHobby(student.Hobby);

            string picturePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", student.Picture);

            form.UploadPicture(picturePath);
            form.EnterAddress(student.Address);
            form.SelectState(student.State);
            form.SelectCity(student.City);
            form.ClickSubmit();

            Assert.That(form.GetConfirmationTitle(), Is.EqualTo("Thanks for submitting the form"));

            Assert.That(
                form.GetSubmittedValue("Student Name"),
                Is.EqualTo(student.FirstName + " " + student.LastName)
            );

            Assert.That(form.GetSubmittedValue("Student Email"), Is.EqualTo(student.Email));
            Assert.That(form.GetSubmittedValue("Gender"), Is.EqualTo(student.Gender));
            Assert.That(form.GetSubmittedValue("Mobile"), Is.EqualTo(student.Mobile));

            DateTime date = DateTime.ParseExact(student.DateOfBirth, "dd MMM yyyy", CultureInfo.InvariantCulture);
            string expectedDate = date.ToString("dd MMMM,yyyy", CultureInfo.InvariantCulture);

            Assert.That(form.GetSubmittedValue("Date of Birth"), Is.EqualTo(expectedDate));
            Assert.That(form.GetSubmittedValue("Subjects"), Is.EqualTo(student.Subject));
            Assert.That(form.GetSubmittedValue("Hobbies"), Is.EqualTo(student.Hobby));
            Assert.That(form.GetSubmittedValue("Picture"), Is.EqualTo(student.Picture));
            Assert.That(form.GetSubmittedValue("Address"), Is.EqualTo(student.Address));

            Assert.That(
                form.GetSubmittedValue("State and City"),
                Is.EqualTo(student.State + " " + student.City)
            );
        }

        [Test]
        public void SubmitEmptyForm()
        {
            PracticeFormPage form = new PracticeFormPage(driver);

            form.ClickSubmit();

            Assert.That(form.IsFirstNameInvalid(), Is.True);
            Assert.That(form.IsLastNameInvalid(), Is.True);
            Assert.That(form.IsGenderInvalid(), Is.True);
            Assert.That(form.IsMobileInvalid(), Is.True);
        }
    }
}