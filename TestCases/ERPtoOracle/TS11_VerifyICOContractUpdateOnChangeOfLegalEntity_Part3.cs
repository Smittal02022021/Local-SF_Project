using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.ERPtoOracle
{
    class TS11_VerifyICOContractUpdateOnChangeOfLegalEntity_Part3 : BaseClass
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

        public static string ERPSectionWithExistingCompany = "TS09_VerifyICOContractBasedOnLOBOfLegalEntity.xlsx";
        public static string xlPath = "TS09_EngagementDetails.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void TestICOContractOnLegalEntityChange()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERPSectionWithExistingCompany;
                Console.WriteLine(excelPath);

                string enggExcelPath = ReadJSONData.data.filePaths.testData + xlPath;

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
                    string enggName = ReadExcelData.ReadDataMultipleRows(enggExcelPath, "EngagementNames", row, 1).ToString();

                    enggHome.SearchEngagementWithName(enggName);

                    //string enggNumberSuffix = engageDetail.GetEnggNumberSuffix();
                    //Assert.AreEqual("A", enggNumberSuffix);
                    //extentReports.CreateLog("Engagement Number Suffix: " + enggNumberSuffix + " is updated ");

                    string newICOContractMainStatus = engageDetail.GetIsMainContractCheckBoxStatus(2);
                    Assert.AreEqual("Not Checked", newICOContractMainStatus);
                    extentReports.CreateLog("New ICO Contract Is Main Status is unchecked ");

                    // string newContractMainStatus = engageDetail.GetIsMainContractCheckBoxStatus(3);
                    // Assert.AreEqual("Checked", newICOContractMainStatus);
                    // extentReports.CreateLog("New Contract Is Main Status is checked ");

                    string oldICOContractMainStatus = engageDetail.GetIsMainContractCheckBoxStatus(4);
                    Assert.AreEqual("Not Checked", oldICOContractMainStatus);
                    extentReports.CreateLog("Old ICO Contract Is Main Status is unchecked ");

                    //string oldContractMainStatus = engageDetail.GetIsMainContractCheckBoxStatus(5);
                    //Assert.AreEqual("Not Checked", oldContractMainStatus);
                    //extentReports.CreateLog("Old Contract Is Main Status is unchecked ");

                    string endDateNewICOContract = engageDetail.GetEndDate(2);
                    Assert.AreEqual(" ", endDateNewICOContract);
                    extentReports.CreateLog("New ICO Contract End Date is blank ");

                    string endDateNewContract = engageDetail.GetEndDate(3);
                    Assert.AreEqual(" ", endDateNewContract);
                    extentReports.CreateLog("New Contract End Date is blank ");

                    /*string endDateICOOldContract = engageDetail.GetEndDate(4);
                    string getDate = DateTime.Today.ToString("M/dd/yyyy");
                    Assert.AreEqual(getDate, endDateICOOldContract);
                    extentReports.CreateLog("Old ICO Contract End Date is updated "+ endDateICOOldContract);
                    
                    string endDateOldContract = engageDetail.GetEndDate(5);
                    Assert.AreEqual(getDate, endDateOldContract);
                    extentReports.CreateLog("Old Contract End Date is updated " + endDateICOOldContract);
                    */
                    // }
                    //Delete company
                    usersLogin.UserLogOut();
                }
                // Logout from standard User
                
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}