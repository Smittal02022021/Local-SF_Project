using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TMTT0012115_ValidateLegacyPeriodAccuredFeesFieldOnRevenueAccruals : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        MonthlyRevenue monthlyRevenuePage = new MonthlyRevenue();

        public static string fileLegacyPeriod = "ValidateLegacyPeriodAccuredFeesField.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ValidateLegacyPeriodAccuredFeesFieldOnRevenueAccruals()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileLegacyPeriod;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate Admin user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin User " + login.ValidateUser() + " is able to login ");

                //Search accounting user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileLegacyPeriod, user);

                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as accounting user
                usersLogin.LoginAsSelectedUser();
                string standardUser = login.ValidateUser();
                string standardUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(standardUserExl.Contains(standardUser), true);
                extentReports.CreateLog("Accounting User: " + standardUser + " is able to login ");

                //Navigate to Monthly Revenue Process Control page
                homePage.NavigateToMonthlyRevenueProcessControlsPage();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Monthly Revenue Process Controls: Home ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Click on the view
                monthlyRevenuePage.SelectMonthlyRevenueProcessControlView();

                //Navigate to Revenue Accrual Page
                monthlyRevenuePage.SortDataAndGetToCurrentMonthRevenuePage();
                monthlyRevenuePage.GetToRevenueAccrualsPage("CF");
                extentReports.CreateLog("Current Revenue Accrual page for CF LOB is displayed. ");

                //Validate Legacy Period Accrued Fees field exist or not on Revenue Accrual page
                string result = monthlyRevenuePage.ValidateIfLegacyPeriodAccruedFeesExist();
                extentReports.CreateLog(result);

                monthlyRevenuePage.ClickBackToRevenueMonthList();

                //Navigate to Revenue Accrual Page
                monthlyRevenuePage.SortDataAndGetToCurrentMonthRevenuePage();
                monthlyRevenuePage.GetToRevenueAccrualsPage("FVA");
                extentReports.CreateLog("Current Revenue Accrual page for FVA LOB is displayed. ");

                //Validate Legacy Period Accrued Fees field exist or not on Revenue Accrual page
                string result1 = monthlyRevenuePage.ValidateIfLegacyPeriodAccruedFeesExist();
                extentReports.CreateLog(result1);

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


