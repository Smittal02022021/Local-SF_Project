using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Engagement
{
    class T1827_EngagementDetails_RevenueAccrual_FASRevenueAccrual_StageChange : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementManager engManager = new EngagementManager();

        public static string fileTC1827 = "T1827_EngagementDetails_RevenueAccrual_FASRevenueAccrual";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RevenueAccrual_FASRevenueAccrual_StageChange()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1827;
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

                //Clicking on Engagement Manager link and Validate the title of page
                string titleEngMgr = engHome.ClickEngageManager();
                Assert.AreEqual("Engagement Manager", titleEngMgr);
                extentReports.CreateLog("Page with title: " + titleEngMgr + " is displayed ");

                //Calling function to select Show Records 
                engManager.SelectShowRecords("FVA Engagements");

                //Calling function to reset filters and validate engagement detail after clicking on engagement Name
                engManager.ResetFiltersForRevAccrual("Other FVA");
                extentReports.CreateLog("Filters are reset as per FVA Engagements ");
                string titleEngDetails = engManager.ClickEngageName();
                Assert.AreEqual("Engagement Detail", titleEngDetails);
                extentReports.CreateLog("Page with title: " + titleEngDetails + " is displayed upon clicking the engagement name ");

                //Get Engagament Name
                string engName = engagementDetails.GetEngName();
                Console.WriteLine(engName);

                //Delete existing Revenue Accurals, navigate back to Engagement Manager and update stage
                //engagementDetails.DeleteExistingAccurals();
                //extentReports.CreateLog("No revenue accruals exists ");
                engHome.ClickEngageManager();
                engManager.UpdateStageValue("Performing Analysis");
                extentReports.CreateLog("Stage is updated to Performing Analysis in Enagagement Manager page ");

                //Navigate to engagement details and validate for Revenue Accrual record
                engManager.ClickEngageName();
                string month = engagementDetails.GetMonthFromRevenueAccrualRecord();
                Console.WriteLine(DateTime.Now.ToString("yyyy - M ", CultureInfo.InvariantCulture));
                Assert.AreEqual("2021 - 11", month);
                extentReports.CreateLog("Revenue Accrual record with : " + month + " is created in engagement details ");

                //Logout of user and validate value of Period Accrual Fee matches with Revenue Accrual Fees 
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                engHome.SearchEngagementWithName(engName);
                string periodAccrualFeeNet = engagementDetails.GetPeriodAccrualFeeNetValue();
                string periodAccrualFee = engagementDetails.GetPeriodAccrualValue();
                Assert.AreEqual(periodAccrualFeeNet, "USD " + periodAccrualFee);
                extentReports.CreateLog("Period Accrual Fee in Revenue Accrual record matches with the Period Accrued Fee listed on the Engagement ");

                //Delete existing Revenue Accurals, update stage and validate Revenue Accrual
                engagementDetails.DeleteExistingAccurals();
                extentReports.CreateLog("Created revenue accrual record is deleted successfully ");

                //Login as CAO user
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("CAO User: " + stdUser1 + " is able to login ");
                engHome.SearchEngagementWithName(engName);
                engagementDetails.UpdateStage();
                extentReports.CreateLog("Stage is updated to Retained in engagement details page ");
                string monthRev = engagementDetails.GetMonthFromRevenueAccrualRecord();
                Console.WriteLine(DateTime.Now.ToString("yyyy - M ", CultureInfo.InvariantCulture));
                Assert.AreEqual("2021 - 11", monthRev);
                extentReports.CreateLog("Revenue Accrual record with : " + monthRev + " is created in engagement details ");

                //Validate value of Period Accrual Fee matches with Revenue Accrual Fees
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                engHome.SearchEngagementWithName(engName);
                string periodAccrualFeeNetEng = engagementDetails.GetPeriodAccrualFeeNetValue();
                string periodAccrualFeeEng = engagementDetails.GetPeriodAccrualValue();
                Assert.AreEqual(periodAccrualFeeNetEng, "USD " + periodAccrualFeeEng);
                extentReports.CreateLog("Period Accrual Fee in Revenue Accrual record matches with the Period Accrued Fee listed on the Engagement ");
                                
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


