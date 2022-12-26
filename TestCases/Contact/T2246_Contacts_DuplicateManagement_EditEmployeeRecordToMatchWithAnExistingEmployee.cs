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
    class T2246_Contacts_DuplicateManagement_EditEmployeeRecordToMatchWithAnExistingEmployee : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        ContactCreatePage createContact = new ContactCreatePage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        ReportHomePage reportsHome = new ReportHomePage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC2246 = "T2246_Contacts_DuplicateManagement_EditEmployeeRecordToMatchWithAnExistingEmployee";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_DuplicateManagement_EditEmployeeRecordToMatchWithAnExistingEmployee()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2246;
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
                conHome.SearchContact(fileTC2246);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Test Houlihan ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Get the name and email address of the 1st existing contact
                string contactFirstName = contactDetails.GetContactName().Split(' ')[0].Trim();
                string contactLastName = contactDetails.GetContactName().Split(' ')[1].Trim();
                string contactEmail = contactDetails.GetContactEmail();

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Contact");

                for (int row = 3; row <= rowCount; row++)
                {
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
                    conSelectRecord.SelectContactRecordType(fileTC2246, contactType);
                    extentReports.CreateLog("User navigate to create contact page upon click of continue button ");

                    //Calling CreateContact function to create new HL contact
                    createContact.CreateContact(fileTC2246, row);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Test HL ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("New Houlihan Employee created ");

                    string compName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 1);

                    //Edit the 2nd existing contact
                    contactDetails.ClickEditContactButton();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact Edit: Test HL ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("User has navigated to the HL employee edit page. ");

                    //Update name and email
                    contactDetails.UpdateHLContactDetails(contactFirstName, contactLastName, contactEmail, fileTC2246, row);

                    //Verify if Duplicate Contact is created
                    Assert.IsTrue(conHome.SearchAndValidateDuplicateContactExists(fileTC2246, 2));
                    extentReports.CreateLog("Duplicate HL Contact created for company: " + compName + ". ");

                    //To Delete created contact
                    contactDetails.DeleteDuplicateContact();

                    //Verify duplicate contact is deleted
                    Assert.IsTrue(conHome.SearchAndValidateDuplicateContactDoNotExists(fileTC2246, 2));
                    extentReports.CreateLog("Duplicate HL Contact is deleted. ");

                    usersLogin.UserLogOut();
                    driver.Quit();
                }
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