using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Companies
{
    class TS14_TS15_ValidateERPSubmittedToSyncImpact : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage oppDetails = new OpportunityDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();

        public static string fileERPSubmittedToSync = "SFToOracleIntegration.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void TestERPSubmittedToSyncImpact()
        {
            try
            {              
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileERPSubmittedToSync;
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
                homePage.SearchUserByGlobalSearch(fileERPSubmittedToSync, user);
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string StandardUser = login.ValidateUser();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Users", 1).Contains(StandardUser), true);
                extentReports.CreateLog("Standard User: " + StandardUser + " is able to login ");

                // Click Add Company button
                companyHome.ClickAddCompany();
                string companyRecordTypePage = companySelectRecord.GetCompanyRecordTypePageHeading();
                Assert.AreEqual("Select Company Record Type", companyRecordTypePage);
                extentReports.CreateLog("Page with heading: " + companyRecordTypePage + " is displayed upon click add company button ");

                // Select company record type
                string recordType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 1);
                companySelectRecord.SelectCompanyRecordType(fileERPSubmittedToSync, recordType);
                string createCompanyPage = createCompany.GetCreateCompanyPageHeading();
                Assert.AreEqual("Company Create", createCompanyPage);
                extentReports.CreateLog("Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                // Validate company type display as selected 
                Assert.AreEqual(recordType, createCompany.GetSelectedCompanyType());
                extentReports.CreateLog("Selected company type: " + createCompany.GetSelectedCompanyType() + " choosen on select company record type page is matching on Company create page ");

                // Create a  company
                createCompany.AddCompany(fileERPSubmittedToSync, 3);

                //Validate company detail heading
                string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                string companyDetailHeadingExl = ReadExcelData.ReadData(excelPath, "Company", 8);
                Assert.AreEqual(companyDetailHeadingExl, companyDetailHeading);
                extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon adding company ");

                string valueERPSubmittedToSync = companyDetail.GetValERPSubmittedToSync();
                Assert.AreEqual(" ", valueERPSubmittedToSync);
                extentReports.CreateLog("ERP Submitted To Sync field value: " + valueERPSubmittedToSync + " i.e blank after successful creation of company ");

                // Logout from standard User
                usersLogin.UserLogOut();

                string companyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 1);
                // Calling Search Company function
                companyHome.SearchCompany(fileERPSubmittedToSync, companyType);

                // Click edit button
                companyDetail.ClickEditButton(fileERPSubmittedToSync, companyType);
                companyEdit.UpdateERPSubmittedToSyncField();
                string ERPLastIntegrationResponseDate = companyDetail.GetValERPLastIntegrationResponseDate();
                Assert.AreEqual(" ", ERPLastIntegrationResponseDate);
                extentReports.CreateLog("ERP Last Integration Response Date field value: " + ERPLastIntegrationResponseDate + "  i.e blank after updating ERP submitted to sync field manually ");

                string ERPLastIntegrationStatus = companyDetail.GetValERPLastIntegrationStatus();
                Assert.AreEqual(" ", ERPLastIntegrationStatus);
                extentReports.CreateLog("ERP Last Integration Status field value: " + ERPLastIntegrationStatus + "  i.e blank after updating ERP submitted to sync field manually ");

                string ERPLastIntegrationErrorCode = companyDetail.GetValERPLastIntegrationErrorCode();
                Assert.AreEqual(" ", ERPLastIntegrationErrorCode);
                extentReports.CreateLog("ERP Last Integration Error Code field value: " + ERPLastIntegrationErrorCode + "  i.e blank after updating ERP submitted to sync field manually ");

                string ERPLastIntegrationErrorDescription = companyDetail.GetValERPLastIntegrationErrorDescription();
                Assert.AreEqual(" ", ERPLastIntegrationErrorDescription);
                extentReports.CreateLog("ERP Last Integration Error Description field value: " + ERPLastIntegrationErrorDescription + "  i.e blank after updating ERP submitted to sync field manually ");

                companyDetail.DeleteCompany(fileERPSubmittedToSync, companyType);

                extentReports.CreateLog("Company is deleted successfully ");
                // Logout from standard User
                usersLogin.UserLogOut();
                extentReports.CreateLog("Standard user is logged out ");
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