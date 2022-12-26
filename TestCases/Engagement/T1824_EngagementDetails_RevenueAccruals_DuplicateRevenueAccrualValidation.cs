using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Engagement
{
    class T1824_EngagementDetails_RevenueAccruals_DuplicateRevenueAccrualValidation : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ValuationPeriods valuationPeriods = new ValuationPeriods();
        public static string fileTC1824 = "T1824_EngagementDetails_RevenueAccruals_DuplicateRevenueAccrualValidation";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AccrualNotMadeUntilEngagementNumberIsAssigned()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1824;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as CAO User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("CAO User: " + stdUser + " is able to login ");

                //Clicking on Engagement Tab and search for Engagement by entering Job type         
                string message =engHome.SearchEngagementWithLOB(ReadExcelData.ReadData(excelPath, "Engagement", 1), ReadExcelData.ReadData(excelPath, "Engagement", 2));
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //Validate title of Engagement Details page
                string title = engagementDetails.GetTitle();
                Assert.AreEqual("Engagement", title);
                extentReports.CreateLog("Page with title: " + title + " is displayed ");

                //Validate if revenue accural for current month exists
                string month = engagementDetails.GetMonthFromRevenueAccrualRecord();
                Console.WriteLine(DateTime.Now.ToString("yyyy - M ", CultureInfo.InvariantCulture));
                Assert.AreEqual("2021 - 11", month);
                extentReports.CreateLog("Revenue Accrual record with : " + month + " exists ");

                //Get the value of Revenue Record Id
                 string ID =engagementDetails.GetRevenueRecordNumber();
                string extID = ID.Substring(35, 15);

                //Click on Add Revenue Accrual button and try to create a new record
                string errorMsg= engagementDetails.AddRevenueAccrual();
                Console.WriteLine(errorMsg);
                Assert.AreEqual("Error: Invalid Data. Review all error messages below to correct your data. Duplicate value on record: "+extID+ " (Related field: External Id)", errorMsg);
                extentReports.CreateLog(errorMsg + " is displayed ");

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
