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

namespace SalesForce_Project.TestCases.Companies
{
    class T2237_Companies_VerifyDuplicateRuleAccountCreatedByAndDupRecordSetOnCreatingDuplicateCompanyRecord : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        ReportHomePage reportsHome = new ReportHomePage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC2237 = "T2237_Companies_VerifyDuplicateRuleAccountCreatedByAndDupRecordSetOnCreatingDuplicateCompanyRecord";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyDuplicateRuleAccountCreatedByAndDupRecordSetOnCreatingDuplicateCompanyRecord()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2237;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                CustomFunctions.TableauPopUp();
                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC2237, user);

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

                // Click Add Company button
                companyHome.ClickAddCompany();
                string companyRecordTypePage = companySelectRecord.GetCompanyRecordTypePageHeading();
                Assert.AreEqual("Select Company Record Type", companyRecordTypePage);
                extentReports.CreateLog("Page with heading: " + companyRecordTypePage + " is displayed upon click add company button ");

                // Select company record type
                string recordType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                companySelectRecord.SelectCompanyRecordType(fileTC2237, recordType);
                string createCompanyPage = createCompany.GetCreateCompanyPageHeading();
                Assert.AreEqual("Company Create", createCompanyPage);
                extentReports.CreateLog("Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                // Validate company type display as selected 
                Assert.AreEqual(recordType, createCompany.GetSelectedCompanyType());
                extentReports.CreateLog("Selected company type: " + createCompany.GetSelectedCompanyType() + " choosen on select company record type page is matching on Company create page ");

                // Create a  company
                createCompany.AddCompany(fileTC2237, 2);

                // Click Add Company button
                companyHome.ClickAddCompany();
                //string companyRecordTypePage = companySelectRecord.GetCompanyRecordTypePageHeading();
                Assert.AreEqual("Select Company Record Type", companyRecordTypePage);
                extentReports.CreateLog("Page with heading: " + companyRecordTypePage + " is displayed upon click add company button ");

                // Select company record type
                // string recordType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                companySelectRecord.SelectCompanyRecordType(fileTC2237, recordType);
                // string createCompanyPage = createCompany.GetCreateCompanyPageHeading();
                Assert.AreEqual("Company Create", createCompanyPage);
                extentReports.CreateLog("Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                // Validate company type display as selected 
                Assert.AreEqual(recordType, createCompany.GetSelectedCompanyType());
                extentReports.CreateLog("Selected company type: " + createCompany.GetSelectedCompanyType() + " choosen on select company record type page is matching on Company create page ");

                // Create a  company
                createCompany.AddCompany(fileTC2237, 2);
                createCompany.ClickSaveIgnoreAlertButton();
                //Validate company detail heading
                string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 8), companyDetailHeading);
                extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon adding company ");

                // Validate company name value
                string CompanyName = companyDetail.GetCompanyName();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 2), CompanyName);
                extentReports.CreateLog("Company name: " + CompanyName + " in add company page matches on company details page ");

                // Validate company location value 
                string CompanyLocation = companyDetail.GetCompanyLocation();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 5) + ", " + ReadExcelData.ReadData(excelPath, "Company", 6), CompanyLocation);
                extentReports.CreateLog("Company Location: " + CompanyLocation + " including city and state in add company page matches on company details page ");

                // Validate company type value
                string CompanyType = companyDetail.GetCompanyType();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 1), CompanyType);
                extentReports.CreateLog("Company Type: " + CompanyType + " in add company page matches on company details page ");

                string CreatedByUser = companyDetail.GetCreatedBy();
                extentReports.CreateLog("Created By: " + CreatedByUser + " user is picked from company details page ");

                //Logout from standard user
                usersLogin.UserLogOut();
                //Click on Reports tab
                reportsHome.ClickReportsTab();

                //Click on save button on Page settings popup 
                reportsHome.ClickSaveButtonOfPageSettings();

                //Search for the reports folder
                reportsHome.SearchReportsFolder(fileTC2237);
                string dataHygieneFolder = ReadExcelData.ReadData(excelPath, "Company", 9);
                string dataHygieneFolderTitle = reportsHome.GetDataHygieneFolderTitle();
                Assert.AreEqual(dataHygieneFolder, dataHygieneFolderTitle);
                extentReports.CreateLog("Data Hygiene Folder Title: " + dataHygieneFolderTitle + " is displayed upon search of data hygiene folder ");

                reportsHome.SearchReportsAndDashboard(fileTC2237,2);
                reportsHome.ClickNeverUpdateOfPageSettings();
                string reportTitleCreatedBy = reportsHome.GetReportDuplicateRuleAccountTitle();
                string reportTitleCreatedByExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Report", 2, 2);
                Assert.AreEqual(reportTitleCreatedByExl, reportTitleCreatedBy);
                extentReports.CreateLog("Report Title: " + reportTitleCreatedBy + " is maching with the selected report ");

                // Click on hide details button
                reportsHome.clickHideDetails();
                Assert.IsTrue(reportsHome.btnShowDetailsPresent(), "Show details button is present. ");
                extentReports.CreateLog("Show Details button is visible on click of hide details button ");

                reportsHome.GetDuplicateCompaniesList(fileTC2237, "Created By", standardUserExl);
                string CreatedByFullName = reportsHome.GetCreatedByUserAfterShowDetails();
                Assert.AreEqual(CreatedByUser,CreatedByFullName);
                extentReports.CreateLog("Created by: "+ CreatedByFullName+" table is displayed upon searching for list and click on show details ");

                int companyCount = 0;
                int companyColList = reportsHome.GetCompanyColumnList();
                for (int i = 4; i <= companyColList-2; i++)
                {
                    string CompanyNameFromExcel = ReadExcelData.ReadData(excelPath, "Company", 2);
                    if (CompanyNameFromExcel.Equals(reportsHome.GetCompanyValue(i)))
                    {
                        companyCount++;
                        if(companyCount.Equals(2))
                        {
                            Console.WriteLine("Total duplicate companies count with same name: " + companyCount);
                            break;
                        }
                    }
                }
                extentReports.CreateLog("Duplicate companies with same name are displayed in Duplicate Rule Account - Created By report ");

                //Click on Reports tab
                reportsHome.ClickReportsTab();

                reportsHome.SearchReportsFolder(fileTC2237);
                string dataHygieneFolderTitles = reportsHome.GetDataHygieneFolderTitle();
                Assert.AreEqual(dataHygieneFolder, dataHygieneFolderTitles);
                extentReports.CreateLog("Data Hygiene Folder Title: " + dataHygieneFolderTitle + " is displayed upon search of data hygiene folder ");

                reportsHome.SearchReportsAndDashboard(fileTC2237, 3);
                string reportTitleDupRecordSet = reportsHome.GetReportDuplicateRuleAccountTitle();
                string reportTitleDupRecordSetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Report", 3, 2);
                Assert.AreEqual(reportTitleDupRecordSetExl, reportTitleDupRecordSet);
                extentReports.CreateLog("Report Title: " + reportTitleDupRecordSet + " is maching with the selected report ");

                //Dismiss StateCountryPickListWarning popup
                reportsHome.DismissStateCountryPicklistWarningPopup();

                //Click on hide details button
                reportsHome.clickHideDetails();
                Assert.IsTrue(reportsHome.btnShowDetailsPresent(), "Show details button is present. ");
                extentReports.CreateLog("Show Details button is visible on click of hide details button ");

                //Enter date range in to and from date and click on run report button
                reportsHome.ClickRunReport();

                extentReports.CreateLog("Duplicate Record Set is displayed based on the date range entered and upon click of run report button ");

                reportsHome.GetDuplicateCompaniesList(fileTC2237, "Dup Record Set", standardUserExl);
                string GroupByLabel = reportsHome.GetLabelGroupBy();
                Assert.AreEqual("Created Date", GroupByLabel);
                extentReports.CreateLog("Group by: " + GroupByLabel + " is displayed upon searching for list and click on show details button ");

                int dupRecordCount = 0;
                int companyList = reportsHome.GetCompanyColumnList();
                for (int i = 6; i <= companyList-2; i++)
                {
                    string CompanyNameFromExcel = ReadExcelData.ReadData(excelPath, "Company", 2);
                    
                    if (CompanyNameFromExcel.Equals(reportsHome.GetCompanyValue(i)))
                    {
                        dupRecordCount++;
                        if (dupRecordCount.Equals(2))
                        {
                            Console.WriteLine("Total duplicate companies count in dup record set with same name: " + dupRecordCount);
                            break;
                        }
                    }
                }

                extentReports.CreateLog("Duplicate companies with same name are displayed in Duplicate Rule Account - Dup Record Set report ");

                companyHome.SearchCompany(fileTC2237, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                
                companyDetail.DeleteCompany(fileTC2237, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));

                companyHome.SearchCompany(fileTC2237, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                companyDetail.DeleteCompany(fileTC2237, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
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