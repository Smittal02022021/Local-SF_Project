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
    class T2295_TMTC0001638_ValidateToAddNegativeNumbersToPeriodAccruedFeeToFASEngagementFromEngagementManager : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementManager engManager = new EngagementManager();

        public static string fileTC2295 = "T2295_TMTC0001638_ValidateToAddNegativeNumbersToPeriodAccruedFee";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2295;
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
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

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
                string engName = engagementDetails.GetEngName();
                string estFees = engagementDetails.GetTotalEstFeeFAS();

                //Logout of CAO user and validate with admin user and delete any existing Revenue Accurals  
                usersLogin.UserLogOut();
                extentReports.CreateLog(caoUser + " logged out successfully ");
                engHome.SearchEngagementWithName(engName);
                engagementDetails.DeleteExistingAccurals();
                extentReports.CreateLog("No revenue accruals exists on Engagement details page ");

                //Login as CAO User and validate the user
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                string caoUser1 = login.ValidateUser();
                Assert.AreEqual(caoUser1.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser1 + " logged in ");

                //Navigate back to Engagement Manager and update Period Accrual fee with negative value                
                engHome.ClickEngageManager();
                string fees = ReadExcelData.ReadData(excelPath, "EngMgr", 1);
                engManager.UpdateRevAccrualFeesValue(fees);
                extentReports.CreateLog("Period Accrual Fees is updated to: "+ fees +" ");
            
                //Navigate to engagement details and validate if Revenue Accrual record exists
                engManager.ClickEngageName();
                string message =engagementDetails.GetRevAccrualMessage();                
                Assert.AreEqual("No records to display", message);
                extentReports.CreateLog("No revenue Accrual record is created for current month in Engagement details ");
                           
                //Get Total Est. Fees and compare if it remains unchanged
                string totalEstFees = engagementDetails.GetTotalEstFeeFAS();
                Assert.AreEqual(estFees, totalEstFees);
                extentReports.CreateLog("Total Estimated Fee in engagement details remains unchanged ");

                //To update value of Period Accrual fee to a positive value               
                engHome.ClickEngageManager();
                engManager.UpdateRevAccrualFeesValue(ReadExcelData.ReadData(excelPath, "EngMgr", 2));
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
