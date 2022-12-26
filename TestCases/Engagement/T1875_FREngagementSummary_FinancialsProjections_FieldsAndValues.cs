using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1875_FREngagementSummary_FinancialsProjections_FieldsAndValues : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        SendEmailNotification notification = new SendEmailNotification();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1875 = "T1875_FinancialsProjections_FieldsAndValues";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FinancialsProjections_FieldsAndValues()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1875;
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

                //Search engagement with LOB - FR
                string message = engHome.SearchEngagementWithName(ReadExcelData.ReadData(excelPath, "Engagement", 2));
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matches to LOB-FR are found and Engagement Detail page is displayed ");

                //Validate FR Engagement Summary button
                string btnFREngSum = engagementDetails.ValidateFREngSummaryButton();
                Assert.AreEqual("True", btnFREngSum);
                extentReports.CreateLog("FR Engagement Summary button is displayed ");

                //Validate FR Engagement Summary page and its fields
                string titleFRSummary = engagementDetails.ClickFREngSummaryButton();
                Assert.AreEqual("FR Engagement Summary", titleFRSummary);
                extentReports.CreateLog("Page with title: " + titleFRSummary + " is displayed upon clicking FR Engagement Summary button ");

                //Click Financials/Projections Tab
                summaryPage.ClickFinancialsTab();

                //---Validate fields i.e. Header row, Currency Financials, Projections, LTM Projections

                //----Validate Header row with FY-----                
                Assert.IsTrue(summaryPage.VerifyFYHeaderRow(), "Verified that header row is same");
                extentReports.CreateLog("Header row of Financials/Projections is displayed as expected ");

                //----Validate row values of Financials/Projections
                Assert.IsTrue(summaryPage.ValidateFinancialsRows(), "Verified that displayed rows are same");
                extentReports.CreateLog("Values of Financials rows are same as expected ");

                //----Validate "Currency Financials are reported in" label----

                string lblCurrencyFinancials = summaryPage.ValidateLabelCurrenyFin();
                Assert.AreEqual("Currency Financials are reported in", lblCurrencyFinancials);
                extentReports.CreateLog("Field with name: " + lblCurrencyFinancials + " is displayed ");

                //----Validate values of Currency Financials are reported in combo-----
                Assert.IsTrue(summaryPage.VerifyCurrencyFinancialsValues(), "Verified that displayed currencies are same");
                extentReports.CreateLog("Values of Currency Financials field are same as expected ");

                //----Validate "Projections are as of" label-----
                string lblProjections = summaryPage.ValidateLabelProjections();
                Assert.AreEqual("Projections are as of", lblProjections);
                extentReports.CreateLog("Field with name: " + lblProjections + " is displayed ");

                //----Validate "LTM Projections are as of" label-----
                string lblLTMProjections = summaryPage.ValidateLabelLTMProjections();
                Assert.AreEqual("LTM Projections are as of", lblLTMProjections);
                extentReports.CreateLog("Field with name: " + lblLTMProjections + " is displayed ");

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


