using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Contact
{
    class T2245_Contacts_DuplicateManagement_AddNewDuplicateEmployeeFromContactsTab : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        ContactEditPage contactEdit = new ContactEditPage();

        public static string fileTC2245 = "T2245_Contacts_DuplicateManagement_AddNewDuplicateEmployeeFromContactsTab";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_DuplicateManagement_AddNewDuplicateEmployeeFromContactsTab()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2245;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin User " + login.ValidateUser() + " is able to login ");

                //Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Search an 1st existing HL employee contact
                conHome.SearchContact(fileTC2245);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Test Houlihan ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling click add contact function
                conHome.ClickAddContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: Select Contact Record Type ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("User navigate to select contact record type page upon click of add contact button ");

                //Calling select record type and click continue function
                string contactType = ReadExcelData.ReadData(excelPath, "Contact", 7);
                conSelectRecord.SelectContactRecordType(fileTC2245, contactType);
                extentReports.CreateLog("User navigate to create contact page upon click of continue button ");

                //Calling CreateContact function to create new HL contact
                createContact.CreateContact(fileTC2245);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Test Houlihan ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("Houlihan Employee created ");

                string compName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 1);

                //Verify if Duplicate Contact is created
                Assert.IsTrue(conHome.SearchAndValidateDuplicateContactExists(fileTC2245, 2));
                extentReports.CreateLog("Duplicate Contact created for company: " + compName + ". ");

                //To Delete created contact
                contactDetails.DeleteDuplicateContact();

                //Verify duplicate contact is deleted
                Assert.IsTrue(conHome.SearchAndValidateDuplicateContactDoNotExists(fileTC2245, 2));
                extentReports.CreateLog("Duplicate Contact is deleted. ");

                usersLogin.UserLogOut();
                driver.Quit();
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