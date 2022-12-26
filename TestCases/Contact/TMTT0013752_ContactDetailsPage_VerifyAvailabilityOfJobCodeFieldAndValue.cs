using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Contact
{
    class TMTT0013752_ContactDetailsPage_VerifyAvailabilityOfJobCodeFieldAndValue : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();

        public static string fileTMT13752 = "TMTT0013752_ContactDetailsPage_VerifyAvailabilityOfJobCodeFieldAndValue";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]

        public void ContactDetailsPage_VerifyAvailabilityOfJobCodeFieldAndValue()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT13752;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Search contact
                conHome.SearchContact(fileTMT13752);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Rob Oudman ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Verify if the Job Code field is available on the contact page
                Assert.IsTrue(contactDetails.VerifyIfJobCodeFieldAndValueExistOrNot());
                extentReports.CreateLog("Job code field is available with values synced from Workday on the contact page. ");

                usersLogin.UserLogOut();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}