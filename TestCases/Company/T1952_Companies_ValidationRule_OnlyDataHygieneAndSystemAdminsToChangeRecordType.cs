using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.Companies
{
    class T1952_Companies_ValidationRule_OnlyDataHygieneAndSystemAdminsToChangeRecordType : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage oppDetails = new OpportunityDetailsPage();
        OpportunityHomePage oppHomePage = new OpportunityHomePage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        UsersLogin usersLogin = new UsersLogin();
        CompanyEditPage companyEdit = new CompanyEditPage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1952 = "T1952_Companies_ValidationRule_OnlyDataHygieneAndSystemAdminsToChangeRecordType";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ValidationRule_OnlyDataHygieneAndSystemAdminsToChangeRecordType()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1952;
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

                int rowUsers = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowUsers; row++)
                {
                    if (ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains("DataHygiene"))
                    {
                        // Search Data Hygiene user by global search
                        string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 2);
                        homePage.SearchUserByGlobalSearch(fileTC1952, user);

                        //Verify searched user
                        string userPeople = homePage.GetPeopleOrUserName();
                        string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 2);
                        Assert.AreEqual(userPeopleExl, userPeople);
                        extentReports.CreateLog("User " + userPeople + " details are displayed ");

                        //Login as Data HygieneUser user
                        usersLogin.LoginAsSelectedUser();
                        string DataHygieneUser = login.ValidateUser();
                        string trimmedDataHygieneUser =  DataHygieneUser.TrimEnd('.');
                        string DataHygieneUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 2);                        
                        Assert.AreEqual(DataHygieneUserExl.Contains(trimmedDataHygieneUser), true);
                        extentReports.CreateLog("DataHygiene User: " + trimmedDataHygieneUser + " is able to login ");
                    }
                    else if (ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains("Standard"))
                    {
                        // Search standard user by global search
                        string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users",4, 2);
                        homePage.SearchUserByGlobalSearch(fileTC1952, user);

                        //Verify searched user
                        string userPeople = homePage.GetPeopleOrUserName();
                        string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 2);
                        Assert.AreEqual(userPeopleExl, userPeople);
                        extentReports.CreateLog("User " + userPeople + " details are displayed ");

                        //Login as standard user
                        usersLogin.LoginAsSelectedUser();
                        string standardUser = login.ValidateUser();
                        string standardUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 2);
                        Assert.AreEqual(standardUserExl.Contains(standardUser), true);
                        extentReports.CreateLog("Standard User: " + standardUser + " is able to login ");
                    }
                    // Calling Search Company function
                    companyHome.SearchCompany(fileTC1952, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                    string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 3), companyDetailHeading);
                    extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon searching company ");

                    string companyTypeFromCompanyEditPage = companyDetail.ChangeCompanyRecordType(fileTC1952);

                    if (ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains("DataHygiene") || ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains("Admin"))
                    {
                        string companyTypeFromCompanyDetailPage = companyDetail.GetCompanyType();
                        Assert.AreEqual(companyTypeFromCompanyEditPage, companyTypeFromCompanyDetailPage);
                        extentReports.CreateLog("System admin is allowed to change company record type ");
                        if (ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains("DataHygiene"))
                        {
                            usersLogin.UserLogOut();
                        }
                    }
                    else
                    {
                        string errorMsg = companyEdit.GetErrorMsgForCompanyRecordTypeChange();
                        Assert.AreEqual("Error: Invalid Data.\r\nReview all error messages below to correct your data.\r\nPlease use Flag Reason field to request a record type change.", errorMsg);

                        extentReports.CreateLog("Error Message: "+ errorMsg+" is displayed upon change of company type by user except system admin or data hygiene ");
                        usersLogin.UserLogOut();
                    }
                }
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