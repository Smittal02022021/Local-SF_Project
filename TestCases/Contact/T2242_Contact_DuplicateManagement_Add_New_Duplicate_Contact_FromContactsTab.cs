using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Reports;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;

namespace SalesForce_Project.TestCases.Contact
{
    class T2242_Contact_DuplicateManagement_Add_New_Duplicate_Contact_FromContactsTab : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        ContactCreatePage createContact = new ContactCreatePage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        ReportHomePage reportsHome = new ReportHomePage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC2242 = "T2242_Contact_DuplicateManagement_Add_New_Duplicate_Contact_FromContactsTab";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contact_DuplicateManagement_Add_New_Duplicate_Contact_FromContactsTab()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2242;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC2242, user);

                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string standardUser = login.ValidateUser();
                string standardUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(standardUserExl.Contains(standardUser), true);
                extentReports.CreateLog("Standard User: " + standardUser + " is able to login ");

                //Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling click add contact function
                conHome.ClickAddContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("User navigate to create contact page upon click of add contact button ");

                //Try creating duplicate contact
                createContact.CreateContact(fileTC2242);

                //Verify if duplicate message related buttons are visible
                Assert.IsTrue(createContact.ValidateIfSaveAndCancelButtonExists());
                extentReports.CreateLog("Save (Ignore Alert), Save and New (Ignore Alert) and Cancel buttons are visible. ");

                //Validate the warning message displayed on screen
                Assert.IsTrue(createContact.ValidateDuplicateContactWarningMessage());
                extentReports.CreateLog("Correct warning message is displayed upon creation of duplicate contacts. ");

                //Click Cancel button
                createContact.ClickCancelButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Get row count from the contacts sheet
                int rowCount = ReadExcelData.GetRowCount(excelPath, "Contact");

                for (int row = 3; row <= rowCount; row++)
                {
                    //Calling click add contact function
                    conHome.ClickAddContact();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("User navigate to create contact page upon click of add contact button ");

                    //Try creating duplicate contact
                    if (row==3)
                    {
                        createContact.CreateContact(fileTC2242, row);
                    }
                    else
                    {
                        createContact.CreateContactWithAddressDetails(fileTC2242, row);
                    }

                    //Verify if duplicate message related buttons are visible
                    Assert.IsTrue(createContact.ValidateIfSaveAndCancelButtonExists());
                    extentReports.CreateLog("Save (Ignore Alert), Save and New (Ignore Alert) and Cancel buttons are visible. ");

                    //Create duplicate contact
                    createContact.ClickSaveIgnoreAlertButton();
                    string compName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);

                    usersLogin.UserLogOut();

                    //Verify if Duplicate Contact is created
                    Assert.IsTrue(conHome.SearchAndValidateDuplicateContactExists(fileTC2242, row));
                    extentReports.CreateLog("Duplicate Contact created for company: " + compName + ". " );

                    //To Delete created contact
                    contactDetails.DeleteDuplicateContact();

                    //Verify duplicate contact is deleted
                    Assert.IsTrue(conHome.SearchAndValidateDuplicateContactDoNotExists(fileTC2242, row));
                    extentReports.CreateLog("Duplicate Contact is deleted. ");

                    // Search standard user by global search
                    string user1 = ReadExcelData.ReadData(excelPath, "Users", 1);
                    homePage.SearchUserByGlobalSearch(fileTC2242, user1);

                    //Verify searched user
                    string userPeople1 = homePage.GetPeopleOrUserName();
                    string userPeopleExl1 = ReadExcelData.ReadData(excelPath, "Users", 1);
                    Assert.AreEqual(userPeopleExl1, userPeople1);
                    extentReports.CreateLog("User " + userPeople1 + " details are displayed ");

                    //Login as standard user
                    usersLogin.LoginAsSelectedUser();
                    string standardUser1 = login.ValidateUser();
                    string standardUserExl1 = ReadExcelData.ReadData(excelPath, "Users", 1);
                    Assert.AreEqual(standardUserExl.Contains(standardUser1), true);
                    extentReports.CreateLog("Standard User: " + standardUser1 + " is able to login ");

                    //Calling click contact function
                    conHome.ClickContact();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");
                }

                usersLogin.UserLogOut();
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