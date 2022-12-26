using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;


namespace SalesForce_Project.TestCases.Companies
{
    class T2346_Companies_Common_VerifyCoverageTeamOfficerCanBeAddedToCoverageTeams_BySelectingDifferentPrimarySectorValues : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        AddCoverageTeam coverageTeam = new AddCoverageTeam();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC2346 = "T2346_Companies_Common_VerifyCoverageTeamOfficerCanBeAddedToCoverageTeams_BySelectingDifferentPrimarySectorValues.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyCoverageTeamOfficerCanBeAddedToCoverageTeams_BySelectingDifferentPrimarySectorValues()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2346;
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
                homePage.SearchUserByGlobalSearch(fileTC2346, user);

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

                // Calling Search Company function
                companyHome.SearchCompany(fileTC2346, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 3), companyDetailHeading);
                extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon searching company ");

                //Add new coverage team
                coverageTeam.AddNewCoverageTeam(fileTC2346,8);

                //Verify Coverage Team Officer
                string officerName = companyDetail.GetCoverageTeamOfficer();
                string officerNameExl = ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 2);
                Assert.AreEqual(officerNameExl, officerName);
                extentReports.CreateLog("Coverage Team Officer: " + officerName + " is displayed on company detail page under Coverage Team section upon adding a new coverage team ");

                //Verify Coverage Team Level 
                string coverageLevel = companyDetail.GetCoverageLevel();
                string coverageLevelExl = ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 3);
                Assert.AreEqual(coverageLevelExl, coverageLevel);
                extentReports.CreateLog("Coverage Level: " + coverageLevel + " is displayed on company detail page under Coverage Team section upon adding a new coverage team ");

                //Verify Coverage Team Type
                string coverageType = companyDetail.GetCoverageType();
                string coverageTypeExl = ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 4);
                Assert.AreEqual(coverageTypeExl, coverageType);
                extentReports.CreateLog("Coverage Type: " + coverageType + " is displayed on company detail page under Coverage Team section upon adding a new coverage team ");

                int rowCoverage = ReadExcelData.GetRowCount(excelPath, "EditCoverageTeam");
                for (int row = 2; row <= rowCoverage; row++)
                {
                    coverageTeam.EditCoverageTeam(fileTC2346, row);

                   //Verify Coverage Team Type
                    string coverageTypeAfterEdit = companyDetail.GetCoverageType();
                    string coverageTypeAfterEditExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EditCoverageTeam", row, 1);
                    Assert.AreEqual(coverageTypeAfterEditExl, coverageTypeAfterEdit);
                    extentReports.CreateLog("Coverage Type: " + coverageTypeAfterEdit + " is displayed on company detail page under Coverage Team section upon selecting different primary sector values in existing coverage team ");
                }
                usersLogin.UserLogOut();

                // Cleanup code
                companyHome.SearchCompany(fileTC2346, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                companyDetail.DeleteCoverageTeamRecord(fileTC2346, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                //companyDetail.DeleteCoverageSector(fileTC2346, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                //companyHome.SearchCompany(fileTC2346, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                //companyDetail.DeleteCoverageTeamRecord(fileTC2346, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));


                extentReports.CreateLog("Coverage Team is deleted successfully ");


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
