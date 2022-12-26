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
    class T1747_Companies_CoverageTeam_NewOffsiteTemplate_SaveCancel : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        CoverageTeamDetail coverageTeamDetail = new CoverageTeamDetail();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1747 = "T1747_Companies_CoverageTeam_NewOffsiteTemplate";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CancelAndSaveNewOffsiteTemplateInCoverageTeam()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1747;
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
                homePage.SearchUserByGlobalSearch(fileTC1747, user);
                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as FS user
                usersLogin.LoginAsSelectedUser();
                Thread.Sleep(10000);
                string FSUser = login.ValidateUser();
                string FSUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(FSUserExl.Contains(FSUser), true);
                extentReports.CreateLog("FS User: " + FSUser + " is able to login ");

                // Calling Search Company function
                string companyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                companyHome.SearchCompany(fileTC1747, companyType);
                string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 3), companyDetailHeading);
                extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon searching company ");

                // Calling function to select coverage team officer
                string coverageTeamOfficer = ReadExcelData.ReadData(excelPath, "Company", 4);
                companyDetail.SelectCoverageTeamOfficer();
                string coverageTeamDetailHeading = coverageTeamDetail.GetCoverageTeamDetailsHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "CoverageTeam", 1), coverageTeamDetailHeading);
                extentReports.CreateLog("Page with heading: "+coverageTeamDetailHeading + " is displayed upon selecting coverage team officer from company detail page coverage team section ");

                //CLick on new offsite template button
                coverageTeamDetail.ClickNewOffsiteTemplateButton();
                string offsiteTemplateEditHeading = coverageTeamDetail.GetOffsiteTemplateHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "CoverageTeam", 2), offsiteTemplateEditHeading);
                extentReports.CreateLog("Page with heading: "+offsiteTemplateEditHeading + " is displayed upon click of new offsite template button ");

                //Verify new offsite template is not created
                coverageTeamDetail.EnterOffsiteTemplateDetails(fileTC1747);
                coverageTeamDetail.ClickCancel();
                Assert.IsFalse(coverageTeamDetail.ValidateOffsiteTemplateCreation(),"Offsite template not created ");
                extentReports.CreateLog("Enter offsite template details and click on cancel button did not create a new offsite template ");

                // Creating a new offsite template record
                coverageTeamDetail.ClickNewOffsiteTemplateButton();
                coverageTeamDetail.EnterOffsiteTemplateDetails(fileTC1747);
                coverageTeamDetail.ClickSave();
                extentReports.CreateLog("Enter offsite template details and click on save button created a new offsite template ");

                //Validate offsite template detail heading
                string offsiteTemplateDetailHeading = coverageTeamDetail.GetOffsiteTemplateDetailHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "CoverageTeam", 12), offsiteTemplateDetailHeading);
                extentReports.CreateLog("Page with heading: "+offsiteTemplateDetailHeading + " is displayed ");

                //Validate current year strategy
                string valueCurrentYearStr = coverageTeamDetail.GetCurrentYearStrategyValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "CoverageTeam", 3), valueCurrentYearStr);                
                extentReports.CreateLog("Current year: "+valueCurrentYearStr + " in edit offsite template matches on offsite template detail page ");

                // Validate revenue commments
                string valueRevenueComment = coverageTeamDetail.GetRevenueCommentValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "CoverageTeam", 5), valueRevenueComment);
                extentReports.CreateLog("Revenue comment: "+valueRevenueComment + " in edit offsite template matches on offsite template detail page ");

                //Validate Relationship Metrics Comments
                string valueRelationshipMetComments = coverageTeamDetail.GetRelationshipMetricsComments();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "CoverageTeam", 7), valueRelationshipMetComments);
                extentReports.CreateLog("Relationship Metrics Comment: " + valueRelationshipMetComments + " in edit offsite template matches on offsite template detail page ");

                //Validate Fund Name
                string valueFundName = coverageTeamDetail.GetFundName();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "CoverageTeam",11), valueFundName);
                extentReports.CreateLog("Fund Name: " + valueFundName + " in edit offsite template matches on offsite template detail page ");

                //Logout from FS user
                usersLogin.UserLogOut();
                extentReports.CreateLog("FS user logged out successfully ");

                //Search company for which offsite template is created
                companyHome.SearchCompany(fileTC1747, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                extentReports.CreateLog("Company is searched successfully ");

                // Coverage team officer is selected
                companyDetail.SelectCoverageTeamOfficer();
                extentReports.CreateLog("Coverage Team officer is selected successfully ");

                //Offsite template is deleted
                coverageTeamDetail.DeleteOffsiteTemplate();
                Assert.AreEqual(false, coverageTeamDetail.ValidateOffsiteTemplateCreation());
                extentReports.CreateLog("Offsite template is deleted successfully ");

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
