using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Companies
{
    class T1170_Companies_Contacts_MergeExistingContactsIntoSingleMasterRecord : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ContactEditPage contactEdit = new ContactEditPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyHomePage companyHome = new CompanyHomePage();

        public static string fileT1170 = "T1063_Contacts_AddMultipleContactsToAnExistingCompanyAndValidateSaveAndNewButton";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddMultipleContactsToAnExistingCompanyAndValidateSaveAndNewButton()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT1170;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                // Calling click contact function
                conHome.ClickContact();
                string contactPageHeading = conHome.GetContactPageHeading();
                Assert.AreEqual("Contacts", contactPageHeading);
                extentReports.CreateLog("Contact Page Heading: " + contactPageHeading + " is displayed on click of Contacts tab");

               
                int rowContact = ReadExcelData.GetRowCount(excelPath, "Contact");
                for (int row = 2; row <= 3; row++)
                {
                    conHome.ClickContact();
                    // Calling click add contact function
                    conHome.ClickAddContact();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: Select Contact Record Type ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("user navigate to select contact record type page upon click of add contact button ");

                    // Calling select record type and click continue function
                    conSelectRecord.SelectContactRecordType(fileT1170, row);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("user navigate to create contact page upon click of continue button ");

                    // To create contact
                    createContact.CreateContact(fileT1170, row);
                    string contactDetailHeading = createContact.GetContactDetailsHeading();
                    Assert.AreEqual("Contact Detail", contactDetailHeading);
                    extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon creation of a contact ");
                    string contactType = ReadExcelData.ReadData(excelPath, "ContactTypes", row);
                    if (contactType.Contains("External Contact") || contactType.Contains("Houlihan Employee"))
                    {
                        extentReports.CreateLog(contactDetails.GetContactRecordTypeValue() + " record is created ");
                    }
                    else
                    {
                        extentReports.CreateLog("Distribution Lists record is created ");
                    }

                   
                }
                companyHome.ClickAddCompany();
                companyHome.SearchCompany(fileT1170, "Operating Company");

                //Verified Merge Functionality
                contactDetails.VerifyMergeContactFunctionality();
                extentReports.CreateLog("Verified merge functionality ");
                extentReports.CreateLog("Deletion of Created Contact ");
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
