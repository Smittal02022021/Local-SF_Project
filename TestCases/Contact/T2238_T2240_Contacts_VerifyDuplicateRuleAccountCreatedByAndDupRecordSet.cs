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
    class T2238_T2240_Contacts_VerifyDuplicateRuleAccountCreatedByAndDupRecordSetOnCreatingDuplicateContactRecords : BaseClass
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

        public static string fileTC2238 = "T2242_Contacts_VerifyDuplicateRuleAccountCreatedByAndDupRecordSetOnCreatingDuplicateContactRecords";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_VerifyDuplicateRuleAccountCreatedByAndDupRecordSetOnCreatingDuplicateCompanyRecord()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2238;
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
                homePage.SearchUserByGlobalSearch(fileTC2238, user);

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

                //To create contact
                createContact.CreateContact(fileTC2238);//verify if contact is created
                extentReports.CreateLog("1st External Contact created ");

                //Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling click add contact function
                conHome.ClickAddContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("User navigate to create contact page upon click of add contact button ");

                //To create contact
                createContact.CreateContact(fileTC2238);
                createContact.ClickSaveIgnoreAlertButton();
                extentReports.CreateLog("2nd External Contact created ");

                string CreatedByUser = companyDetail.GetCreatedBy();
                extentReports.CreateLog("Created By: " + CreatedByUser + " user is picked from company details page ");

                //Logout from standard user
                usersLogin.UserLogOut();

                //Click on Reports tab
                reportsHome.ClickReportsTab();
                
                //Search for the reports folder
                reportsHome.SearchReportsFolder(fileTC2238);

                string dataHygieneFolder = ReadExcelData.ReadData(excelPath, "Contact", 8);
                string dataHygieneFolderTitle = reportsHome.GetDataHygieneFolderTitle();
                Assert.AreEqual(dataHygieneFolder, dataHygieneFolderTitle);
                extentReports.CreateLog("Data Hygiene Folder Title: " + dataHygieneFolderTitle + " is displayed upon search of data hygiene folder ");

                reportsHome.SearchReportsAndDashboard(fileTC2238,2);
                
                reportsHome.ClickNeverUpdateOfPageSettings();
                string reportTitleCreatedBy = reportsHome.GetReportDuplicateRuleAccountTitle();
                string reportTitleCreatedByExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Report", 2, 2);
                Assert.AreEqual(reportTitleCreatedByExl, reportTitleCreatedBy);
                extentReports.CreateLog("Report Title: " + reportTitleCreatedBy + " is maching with the selected report ");
                
                // Click on hide details button
                reportsHome.clickHideDetails();
                Assert.IsTrue(reportsHome.btnShowDetailsPresent(), "Show details button is present. ");
                extentReports.CreateLog("Show Details button is visible on click of hide details button ");
                
                reportsHome.GetDuplicateContactsList(fileTC2238, "Created By", standardUserExl);
                string CreatedByFullName = reportsHome.GetCreatedByUserAfterShowDetails();
                Assert.AreEqual(CreatedByUser,CreatedByFullName);
                extentReports.CreateLog("Created by: "+ CreatedByFullName+" table is displayed upon searching for list and click on show details ");

                int contactCount = 0;
                int contactColList = reportsHome.GetContactColumnList();
                for (int i = 4; i <= contactColList - 2; i++)
                {
                    string ContactNameFromExcel = ReadExcelData.ReadData(excelPath, "Contact", 4);
                    if (ContactNameFromExcel.Equals(reportsHome.GetContactValue(i)))
                    {
                        contactCount++;
                        if(contactCount.Equals(2))
                        {
                            Console.WriteLine("Total duplicate contacts count with same name: " + contactCount);
                            break;
                        }
                    }
                }
                extentReports.CreateLog("Duplicate contacts with same name are displayed in Duplicate Rule Contact - Created By report ");

                //Click on Reports tab
                reportsHome.ClickReportsTab();
                
                reportsHome.SearchReportsFolder(fileTC2238);
                string dataHygieneFolderTitles = reportsHome.GetDataHygieneFolderTitle();
                Assert.AreEqual(dataHygieneFolder, dataHygieneFolderTitles);
                extentReports.CreateLog("Data Hygiene Folder Title: " + dataHygieneFolderTitle + " is displayed upon search of data hygiene folder ");

                reportsHome.SearchReportsAndDashboard(fileTC2238, 3);
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

                reportsHome.GetDuplicateContactsList(fileTC2238, "Dup Record Set", standardUserExl);
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

                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC2238, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                extentReports.CreateLog("1st Created contact deleted. ");

                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC2238, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                extentReports.CreateLog("2nd Created contact deleted. ");

                //Calling click add contact function
                conHome.ClickAddContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("User navigate to create contact page upon click of add contact button ");

                //To create contact
                createContact.CreateContact(fileTC2238);
                extentReports.CreateLog("3rd External Contact created ");

                //Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling click add contact function
                conHome.ClickAddContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("User navigate to create contact page upon click of add contact button ");

                //To create contact
                createContact.CreateContact(fileTC2238);
                //createContact.ClickSaveIgnoreAlertButton();
                extentReports.CreateLog("4th External Contact created ");

                string CreatedByUser1 = companyDetail.GetCreatedBy();
                extentReports.CreateLog("Created By: " + CreatedByUser1 + " user is picked from company details page ");

                //Click on Reports tab
                reportsHome.ClickReportsTab();

                //Search for the reports folder
                reportsHome.SearchReportsFolder(fileTC2238);

                string dataHygieneFolder1 = ReadExcelData.ReadData(excelPath, "Contact", 8);
                string dataHygieneFolderTitle1 = reportsHome.GetDataHygieneFolderTitle();
                Assert.AreEqual(dataHygieneFolder1, dataHygieneFolderTitle1);
                extentReports.CreateLog("Data Hygiene Folder Title: " + dataHygieneFolderTitle1 + " is displayed upon search of data hygiene folder ");

                reportsHome.SearchReportsAndDashboard(fileTC2238, 2);

                //reportsHome.ClickNeverUpdateOfPageSettings();
                string reportTitleCreatedBy1 = reportsHome.GetReportDuplicateRuleAccountTitle();
                string reportTitleCreatedByExl1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Report", 2, 2);
                Assert.AreEqual(reportTitleCreatedByExl1, reportTitleCreatedBy1);
                extentReports.CreateLog("Report Title: " + reportTitleCreatedBy1 + " is maching with the selected report ");

                // Click on hide details button
                reportsHome.clickHideDetails();
                Assert.IsTrue(reportsHome.btnShowDetailsPresent(), "Show details button is present. ");
                extentReports.CreateLog("Show Details button is visible on click of hide details button ");

                reportsHome.GetDuplicateContactsList(fileTC2238, "Created By", standardUserExl);
                string CreatedByFullName1 = reportsHome.GetCreatedByUserAfterShowDetails();
                Assert.AreEqual(CreatedByUser1, CreatedByFullName1);
                extentReports.CreateLog("Created by: " + CreatedByFullName1 + " table is displayed upon searching for list and click on show details ");

                int contactCount1 = 0;
                int contactColList1 = reportsHome.GetContactColumnList();
                for (int i = 4; i <= contactColList1 - 2; i++)
                {
                    string ContactNameFromExcel1 = ReadExcelData.ReadData(excelPath, "Contact", 4);
                    if (ContactNameFromExcel1.Equals(reportsHome.GetContactValue(i)))
                    {
                        contactCount1++;
                        if (contactCount1.Equals(2))
                        {
                            Console.WriteLine("Total duplicate contacts count with same name: " + contactCount1);
                            break;
                        }
                    }
                }
                extentReports.CreateLog("Duplicate contacts with same name are displayed in Duplicate Rule Contact - Created By report ");

                //Click on Reports tab
                reportsHome.ClickReportsTab();

                reportsHome.SearchReportsFolder(fileTC2238);
                string dataHygieneFolderTitles1 = reportsHome.GetDataHygieneFolderTitle();
                Assert.AreEqual(dataHygieneFolder1, dataHygieneFolderTitles1);
                extentReports.CreateLog("Data Hygiene Folder Title: " + dataHygieneFolderTitle1 + " is displayed upon search of data hygiene folder ");

                reportsHome.SearchReportsAndDashboard(fileTC2238, 3);
                string reportTitleDupRecordSet1 = reportsHome.GetReportDuplicateRuleAccountTitle();
                string reportTitleDupRecordSetExl1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Report", 3, 2);
                Assert.AreEqual(reportTitleDupRecordSetExl1, reportTitleDupRecordSet1);
                extentReports.CreateLog("Report Title: " + reportTitleDupRecordSet1 + " is maching with the selected report ");

                //Click on hide details button
                reportsHome.clickHideDetails();
                Assert.IsTrue(reportsHome.btnShowDetailsPresent(), "Show details button is present. ");
                extentReports.CreateLog("Show Details button is visible on click of hide details button ");

                //Enter date range in to and from date and click on run report button
                reportsHome.ClickRunReport();

                extentReports.CreateLog("Duplicate Record Set is displayed based on the date range entered and upon click of run report button ");

                reportsHome.GetDuplicateContactsList(fileTC2238, "Dup Record Set", standardUserExl);
                string GroupByLabel1 = reportsHome.GetLabelGroupBy();
                Assert.AreEqual("Created Date", GroupByLabel1);
                extentReports.CreateLog("Group by: " + GroupByLabel1 + " is displayed upon searching for list and click on show details button ");

                int dupRecordCount1 = 0;
                int contactList1 = reportsHome.GetContactColumnList();
                for (int i = 6; i <= contactList1 - 2; i++)
                {
                    string ContactNameFromExcel1 = ReadExcelData.ReadData(excelPath, "Contact", 4);

                    if (ContactNameFromExcel1.Equals(reportsHome.GetContactValue(i)))
                    {
                        dupRecordCount1++;
                        if (dupRecordCount1.Equals(2))
                        {
                            Console.WriteLine("Total duplicate contacts count in dup record set with same name: " + dupRecordCount1);
                            break;
                        }
                    }
                }

                extentReports.CreateLog("Duplicate contacts with same name are displayed in Duplicate Rule Contact - Dup Record Set report ");

                //Calling click contact function
                conHome.ClickContact();

                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC2238, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                extentReports.CreateLog("Deletion of Created Contact. ");

                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC2238, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                extentReports.CreateLog("Deletion of Created Contact. ");

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