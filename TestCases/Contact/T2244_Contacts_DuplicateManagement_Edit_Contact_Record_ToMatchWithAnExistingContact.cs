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
    class T2244_Contacts_DuplicateManagement_Edit_Contact_Record_ToMatchWithAnExistingContact : BaseClass
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

        public static string fileTC2244 = "T2244_Contacts_DuplicateManagement_Edit_Contact_Record_ToMatchWithAnExistingContact";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_DuplicateManagement_Edit_Contact_Record_ToMatchWithAnExistingContact()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2244;
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
                homePage.SearchUserByGlobalSearch(fileTC2244, user);

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

                //Search an 1st existing contact
                conHome.SearchContactWithExternalContact(fileTC2244);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Test External ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Get the name and email address of the 1st existing contact
                string contactName1 = contactDetails.GetContactName();
                string contactEmail1 = contactDetails.GetContactEmail();

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Contact");

                for (int row=3; row<=rowCount; row++)
                {
                    if (row==3)
                    {
                        //Calling click contact function
                        conHome.ClickContact();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog(driver.Title + " is displayed ");

                        //Calling click add contact function
                        conHome.ClickAddContact();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("User navigate to create contact page upon click of add contact button ");

                        //Create Contact
                        createContact.CreateContact(fileTC2244, row);

                        usersLogin.UserLogOut();

                        //Search for 2nd existing contact
                        conHome.SearchContactMultipleRows(fileTC2244, row);
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Test Test ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog(driver.Title + " is displayed ");

                        //Edit the 2nd existing contact
                        contactDetails.ClickEditContactButton();

                        //Update name and email
                        contactDetails.UpdateNameAndEmailAddress(contactEmail1, fileTC2244);

                        string CreatedByUser = companyDetail.GetCreatedBy();
                        extentReports.CreateLog("Created By: " + CreatedByUser + " user is picked from company details page ");
                        
                        string compName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);

                        //Verify if Duplicate Contact is created
                        Assert.IsTrue(conHome.SearchAndValidateDuplicateContactExists(fileTC2244, 2));
                        extentReports.CreateLog("Duplicate Contact created for company: " + compName + ". ");

                        //Click on Reports tab
                        reportsHome.ClickReportsTab();

                        //Search for the reports folder
                        reportsHome.SearchReportsFolder(fileTC2244);

                        string dataHygieneFolder = ReadExcelData.ReadData(excelPath, "Contact", 8);
                        string dataHygieneFolderTitle = reportsHome.GetDataHygieneFolderTitle();
                        Assert.AreEqual(dataHygieneFolder, dataHygieneFolderTitle);
                        extentReports.CreateLog("Data Hygiene Folder Title: " + dataHygieneFolderTitle + " is displayed upon search of data hygiene folder ");

                        reportsHome.SearchReportsAndDashboard(fileTC2244, 2);

                        //reportsHome.ClickNeverUpdateOfPageSettings();
                        string reportTitleCreatedBy = reportsHome.GetReportDuplicateRuleAccountTitle();
                        string reportTitleCreatedByExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Report", 2, 2);
                        Assert.AreEqual(reportTitleCreatedByExl, reportTitleCreatedBy);
                        extentReports.CreateLog("Report Title: " + reportTitleCreatedBy + " is maching with the selected report ");

                        // Click on hide details button
                        reportsHome.clickHideDetails();
                        Assert.IsTrue(reportsHome.btnShowDetailsPresent(), "Show details button is present. ");
                        extentReports.CreateLog("Show Details button is visible on click of hide details button ");

                        reportsHome.GetDuplicateContactsList(fileTC2244, "Created By", standardUserExl);
                        string CreatedByFullName = reportsHome.GetCreatedByUserAfterShowDetails();
                        Assert.AreEqual(CreatedByUser, CreatedByFullName);
                        extentReports.CreateLog("Created by: " + CreatedByFullName + " table is displayed upon searching for list and click on show details ");

                        int contactCount = 0;
                        int contactColList = reportsHome.GetContactColumnList();
                        for (int i = 4; i <= contactColList - 2; i++)
                        {
                            string ContactNameFromExcel = ReadExcelData.ReadData(excelPath, "Contact", 4);
                            if (ContactNameFromExcel.Equals(reportsHome.GetContactValue(i)))
                            {
                                contactCount++;
                                if (contactCount.Equals(2))
                                {
                                    Console.WriteLine("Total duplicate contacts count with same name: " + contactCount);
                                    break;
                                }
                            }
                        }
                        extentReports.CreateLog("Duplicate contacts with same name are displayed in Duplicate Rule Contact - Created By report ");

                        //Click on Reports tab
                        reportsHome.ClickReportsTab();

                        reportsHome.SearchReportsFolder(fileTC2244);
                        string dataHygieneFolderTitles = reportsHome.GetDataHygieneFolderTitle();
                        Assert.AreEqual(dataHygieneFolder, dataHygieneFolderTitles);
                        extentReports.CreateLog("Data Hygiene Folder Title: " + dataHygieneFolderTitle + " is displayed upon search of data hygiene folder ");

                        reportsHome.SearchReportsAndDashboard(fileTC2244, 3);
                        string reportTitleDupRecordSet = reportsHome.GetReportDuplicateRuleAccountTitle();
                        string reportTitleDupRecordSetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Report", 3, 2);
                        Assert.AreEqual(reportTitleDupRecordSetExl, reportTitleDupRecordSet);
                        extentReports.CreateLog("Report Title: " + reportTitleDupRecordSet + " is maching with the selected report ");

                        //Click on hide details button
                        reportsHome.clickHideDetails();
                        Assert.IsTrue(reportsHome.btnShowDetailsPresent(), "Show details button is present. ");
                        extentReports.CreateLog("Show Details button is visible on click of hide details button ");

                        //Enter date range in to and from date and click on run report button
                        reportsHome.ClickRunReport();

                        extentReports.CreateLog("Duplicate Record Set is displayed based on the date range entered and upon click of run report button ");

                        reportsHome.GetDuplicateContactsList(fileTC2244, "Dup Record Set", standardUserExl);
                        string GroupByLabel = reportsHome.GetLabelGroupBy();
                        Assert.AreEqual("Created Date", GroupByLabel);
                        extentReports.CreateLog("Group by: " + GroupByLabel + " is displayed upon searching for list and click on show details button ");

                        int dupRecordCount = 0;
                        int contactList = reportsHome.GetContactColumnList();
                        for (int i = 6; i <= contactList - 2; i++)
                        {
                            string ContactNameFromExcel = ReadExcelData.ReadData(excelPath, "Contact", 4);

                            if (ContactNameFromExcel.Equals(reportsHome.GetContactValue(i)))
                            {
                                dupRecordCount++;
                                if (dupRecordCount.Equals(2))
                                {
                                    Console.WriteLine("Total duplicate contacts count in dup record set with same name: " + dupRecordCount);
                                    break;
                                }
                            }
                        }

                        extentReports.CreateLog("Duplicate contacts with same name are displayed in Duplicate Rule Contact - Dup Record Set report ");

                        //Calling click contact function
                        conHome.ClickContact();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog(driver.Title + " is displayed ");

                        //Search an 1st existing contact
                        conHome.SearchContactWithExternalContact(fileTC2244);
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Test External ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog(driver.Title + " is displayed ");

                        //To Delete created contact
                        contactDetails.DeleteDuplicateContact();

                        //Verify duplicate contact is deleted
                        Assert.IsTrue(conHome.SearchAndValidateDuplicateContactDoNotExists(fileTC2244, 2));
                        extentReports.CreateLog("Duplicate Contact is deleted. ");
                    }
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