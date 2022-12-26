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
    class T1950_Contacts_ValidationRules_PreventHCMProfileFromEditingInternalContactRecords : BaseClass
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

        public static string fileTC1950 = "T1950_Contacts_ValidationRules_PreventHCMProfileFromEditingInternalContactRecords";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_ValidationRules_PreventHCMProfileFromEditingInternalContactRecords()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1950;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowUserType = ReadExcelData.GetRowCount(excelPath, "UsersType");
                for (int row = 2; row <= rowUserType; row++)
                {
                    if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", row, 1).Equals("HCM"))
                    {
                        // Search standard user by global search
                        string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                        homePage.SearchUserByGlobalSearch(fileTC1950, user);

                        //Verify searched user
                        string userPeople = homePage.GetPeopleOrUserName();
                        string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                        Assert.AreEqual(userPeopleExl, userPeople);
                        extentReports.CreateLog("User " + userPeople + " details are displayed ");

                        //Login as HCM user
                        usersLogin.LoginAsSelectedUser();
                        string HRUser = login.ValidateUser();
                        string HRUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                        Assert.AreEqual(HRUserExl.Contains(HRUser), true);
                        extentReports.CreateLog("HCM User: " + HRUser + " is able to login ");
                    }

                    //Search Houlihan contact
                    conHome.SearchContact(fileTC1950);
                    string contactDetailHeading = createContact.GetContactDetailsHeading();
                    Assert.AreEqual("Contact Detail", contactDetailHeading);
                    extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon search for a contact. ");

                    //Calling Click Edit Contact function
                    contactDetails.ClickEditContactButton();
                    string contactEditHeading = contactEdit.GetContactEditHeading();
                    Assert.AreEqual("Contact Edit", contactEditHeading);
                    extentReports.CreateLog("Page with heading: " + contactEditHeading + " is displayed upon search for a contact. ");

                    //Validate if Office field on Edit Contact page is editable
                    Assert.IsTrue(contactEdit.ValidateOffileFieldEditableForHCMUser());
                    extentReports.CreateLog("Office Field is non-editable for HCM user on edit contacts page. ");
                }

                usersLogin.UserLogOut();
                extentReports.CreateLog("HCM user logged out successfully. ");

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