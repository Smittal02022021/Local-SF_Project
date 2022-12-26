using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.Contacts;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.ERPtoOracle
{
    class TS32_VerifyIcoContractDetailOnOpportunity_Part2 : BaseClass
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
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        ContactCreatePage contactCreate = new ContactCreatePage();
        ContactDetailsPage contactDetail = new ContactDetailsPage();
        EngagementDetailsPage engageDetail = new EngagementDetailsPage();
        ContractDetailsPage contractDetail = new ContractDetailsPage();
        EngagementHomePage enggHome = new EngagementHomePage();
        LegalEntityDetailPage legalEntDetail = new LegalEntityDetailPage();

        public static string ERPSectionWithExistingCompany = "TS32_VerifyIcoContractDetailOnOpportunity.xlsx";
        public static string xlPath = "TS32_OpportunityDetails.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void TestICOContractExistingCompany2()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERPSectionWithExistingCompany;
                Console.WriteLine(excelPath);

                string oppExcelPath = ReadJSONData.data.filePaths.testData + xlPath;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    // Search standard user by global search
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);

                    homePage.SearchUserByGlobalSearch(ERPSectionWithExistingCompany, user);
                    string userPeople = homePage.GetPeopleOrUserName();
                    string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    Assert.AreEqual(userPeopleExl, userPeople);
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    //Login as standard user
                    usersLogin.LoginAsSelectedUser();
                    string StandardUser = login.ValidateUser();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains(StandardUser), true);
                    extentReports.CreateLog("Standard User: " + StandardUser + " is able to login ");
                    string oppName = ReadExcelData.ReadDataMultipleRows(oppExcelPath, "OpportunityNames", row, 1).ToString();

                    opportunityHome.SearchOpportunity(oppName);

                    string oppNumber = oppDetails.GetOppNumber();
                    string ICOContractName = oppDetails.GetICOContractName();
                    string ContractType = oppDetails.GetERPContractType();
                    string erpBusinessUnit = oppDetails.GetERPBusinessUnit();
                    string ERPlegalEntityName = oppDetails.GetERPLegalEntityName();
                    string billTo = oppDetails.GetBillTo();
                    string contractStartDate = oppDetails.GetContractStartDate();

                    //Verify Contract Name
                    string ContractName = "ICO - 001 - " + oppNumber;
                    Assert.AreEqual(ContractName, ICOContractName);
                    extentReports.CreateLog("ICO Contract name: " + ICOContractName + " displayed on opportunity detail page ");

                    //Verify Contract Type
                    string Contract_Type = "ICO";
                    Assert.AreEqual(Contract_Type, ContractType);
                    extentReports.CreateLog("Contract Type: " + ContractType + " is displayed on opportunity detail page ");

                    //Verify Business Unit 
                    string businessUnit = "USD US";
                    Assert.AreEqual(businessUnit, erpBusinessUnit);
                    extentReports.CreateLog("Business Unit: " + erpBusinessUnit + " displayed on opportunity detail page ");

                    //Verify Legal Entity 
                    string legalEntityName = "HL, Inc.";
                    Assert.AreEqual(legalEntityName, ERPlegalEntityName);
                    extentReports.CreateLog("Business Unit: " + erpBusinessUnit + " displayed on opportunity detail page ");

                    //Verify Bill To Company
                    string valBillToCompany = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 12);
                    Assert.AreEqual(valBillToCompany, billTo);
                    extentReports.CreateLog("Bill To Company: " + billTo + " displayed on opportunity detail page ");

                    //Verify contract start date
                    string todaysDate = CustomFunctions.GetCurrentPSTDate();
                    Assert.AreEqual(todaysDate, contractStartDate);
                    extentReports.CreateLog("Contract Start Date: " + contractStartDate + " displayed on opportunity detail page ");

                    
                    usersLogin.UserLogOut();
                    extentReports.CreateLog("Standard user is logged out ");
                }
                // Logout from standard User
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