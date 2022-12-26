
using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Engagement
{
    class T1975AndT2186_PortfolioValuation_AccrualNotMadeUntilEngagementNumberIsAssigned : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ValuationPeriods valuationPeriods = new ValuationPeriods();
        public static string fileTC2186 = "T2186_PortfolioValuation_AccrualNotMadeUntilEngagementNumberIsAssigned";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2186;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

               //Clicking on Engagement Tab and search for Engagement by entering Job type         
                string message = engHome.SearchEngagementWithName(ReadExcelData.ReadData(excelPath, "Engagement", 1));
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //Validate title of Engagement Details page
                string title = engagementDetails.GetTitle();
                Assert.AreEqual("Engagement", title);
                extentReports.CreateLog("Page with title: " + title + " is displayed ");

                //Get Engagement Name from Engagement
                string engName = engagementDetails.GetEngName();

                //Function to clear Engagement number and save it
                string engNum = engagementDetails.ClearEngNumberAndSave();
                Assert.AreEqual(" ", engNum);
                extentReports.CreateLog("Engagement number is removed successfuly ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Search for same Engagement
                engHome.SearchEngagementWithName(engName);

                //Click on Portfolio Valuation,click Engagement Valuation Period and validate Engagement Valuation Period page
                engagementDetails.ClickPortfolioValuation();
                string titleValPeriod = valuationPeriods.ClickEngValuationPeriod();
                Assert.AreEqual("New Engagement Valuation Period", titleValPeriod);
                extentReports.CreateLog("Page with title: " + titleValPeriod + " is displayed upon clicking New Engagement Valuation Period button ");

                //Enter Engagement Valuation Period details, save it and Validate Engagement Valuation Period Detail page
                valuationPeriods.EnterAndSaveEngValuationDetails();
                string titleValPeriodDetail = valuationPeriods.GetEngValPeriodDetailTitle();
                Assert.AreEqual("Engagement Valuation Period Detail", titleValPeriodDetail);
                extentReports.CreateLog("Page with title: " + titleValPeriodDetail + " is displayed upon saving Engagement Valuation Period details ");

                //Enter Eng Valuation Period Position details and validate entered Valuation Period Position
                string valPeriodPosition = valuationPeriods.EnterPeriodPositionDetails(fileTC2186);
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4), valPeriodPosition);
                extentReports.CreateLog("Engagement Valuation Period Position with name: " + valPeriodPosition + " is added successfully ");

                //Click on added Period Position
                string titlePage = valuationPeriods.ClickAddedValPeriod();
                Assert.AreEqual("Engagement Valuation Position Detail", titlePage);
                extentReports.CreateLog("Page with title: " + titlePage + " is displayed upon clicking on added Valuation Position ");
                valuationPeriods.ClickPositionAndSaveTeamMembers();
                extentReports.CreateLog("Team members details are saved ");

                //Update Status to Completed and validate error messages
                string message1 = valuationPeriods.UpdateStatusAndSave();
                extentReports.CreateLog("Status is updated to Completed, Generate Accrual ");
                Assert.AreEqual("The accrual should not be made until the engagement number is assigned. FVA rule.", message1);
                extentReports.CreateLog("1st error message: " + message1 + " is displayed upon accepting alert ");

                string message2 = valuationPeriods.GetErrorMessage();
                Assert.AreEqual("Please update the stage to generate a revenue accrual.", message2);
                extentReports.CreateLog("2nd error message: " + message2 + " is displayed upon accepting alert ");

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

