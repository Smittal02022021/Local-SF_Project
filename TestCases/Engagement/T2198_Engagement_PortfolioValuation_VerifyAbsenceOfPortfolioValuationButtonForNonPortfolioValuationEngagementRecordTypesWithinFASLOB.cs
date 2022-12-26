using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Engagement
{
    class T2198_Engagement_PortfolioValuation_VerifyAbsenceOfPortfolioValuationButtonForNonPortfolioValuationEngagementRecordTypesWithinFASLOB :BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ValuationPeriods valuationPeriods = new ValuationPeriods();
        public static string fileTC2198 = "T2198_PortfolioValuation_VerifyAbsenceOfPortfolioValuationButton";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

            Initialize();   
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyAbsenceOfPortfolioValuationButton()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2198;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Clicking on Engagement Tab and search for Engagement by entering Job type         
                string message = engHome.SearchEngagementWithJobType(ReadExcelData.ReadData(excelPath, "Engagement", 1));
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //Validate title of Engagement Details page
                string title = engagementDetails.GetTitle();
                Assert.AreEqual("Engagement", title);
                extentReports.CreateLog("Page with title: " + title + " is displayed ");

                //Validate the visibility of Portfolio Valuation button
                string btnPV = engagementDetails.ValidatePortfolioValuationButton();
                Assert.AreEqual("Portfolio Valuation button is displayed", btnPV);
                extentReports.CreateLog(btnPV + " for Job Type " + ReadExcelData.ReadData(excelPath, "Engagement", 1) + " ");

                //Click on Enagagement tab and search for non Portfolio Valuation Engagement
                engagementDetails.ClickEngagementTab();
                string message1 = engHome.SearchEngagementWithJobType(ReadExcelData.ReadData(excelPath, "Engagement", 2));
                Assert.AreEqual("Record found", message1);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //Validate title of Engagement Details page
                string title1 = engagementDetails.GetTitle();
                Assert.AreEqual("Engagement", title1);
                extentReports.CreateLog("Page with title: " + title1 + " is displayed ");

                //Validate the visibility of Portfolio Valuation button
                string btnPV1 = engagementDetails.ValidatePortfolioValuationButton();
                Assert.AreEqual("Portfolio Valuation button is not displayed", btnPV1);
                extentReports.CreateLog(btnPV1 + " for Job Type " + ReadExcelData.ReadData(excelPath, "Engagement", 2) +" ");

                usersLogin.UserLogOut();
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

