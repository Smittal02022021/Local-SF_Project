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
    class T1828_EngagementDetails_RevenueAccrual_FASRevenueAccrual_TotalEstimatedFeeChange : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementManager engManager = new EngagementManager();

        public static string fileTC1828 = "T1828_FASRevenueAccrual_TotalEstimatedFeeChange";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FASRevenueAccrual_TotalEstimatedFeeChange()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1828;
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
                extentReports.CreateLog("Page with title: " + titleEngDetails + " is displayed upon clicking Engagement Name ");

                //Get Engagament Name
                string engName = engagementDetails.GetEngName();
                Console.WriteLine(engName);

                //Delete existing Revenue Accurals 
                //engagementDetails.DeleteExistingAccurals();
                //extentReports.CreateLog("No revenue accruals exists on Engagement details page ");

                //Navigate back to Engagement Manager and update Total Est Fee
                engHome.ClickEngageManager();
                engManager.UpdateTotalEstFeeWithCAOUser("10.00");
                extentReports.CreateLog("Total Estimated Fees value is updated in Engagement details page ");

                //Navigate to engagement details and validate for Revenue Accrual record   
                engManager.ClickEngageName();
                string month = engagementDetails.GetMonthFromRevenueAccrualRecord();
                Console.WriteLine(DateTime.Now.ToString("yyyy - M ", CultureInfo.InvariantCulture));
                Assert.AreEqual("2021 - 11", month);
                extentReports.CreateLog("Revenue Accrual record with : " + month + " is created ");

                //Logout of FAS CAO user and validate with admin user
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                engHome.SearchEngagementWithName(engName);

                //Validate value of Period Accrual Fee matches with entered Revenue Accrual Fees 
                string AccrualFeeNet = engagementDetails.GetPeriodAccrualFeeNetValue();
                string AccrualFee = engagementDetails.GetPeriodAccrualValueFAS();
                Assert.AreEqual(AccrualFeeNet, "USD "+AccrualFee);
                extentReports.CreateLog("Period Accrual Fee in Revenue Accrual record matches with the Period Accrued Fee listed on the Engagement ");

                //Validate value of Total Est Fee in Revenue Accrual record matches with value in engagement details 
                string EstFeeAccrual = engagementDetails.GetTotalEstFeeFromRevenueAccrualRecord();
                string EstFee = engagementDetails.GetTotalEstFeeFAS();
                Assert.AreEqual(EstFeeAccrual, EstFee);
                extentReports.CreateLog("Total Est Fee in Revenue Accrual record matches with the Total Est Fee listed on the Engagement ");
                
                //Delete existing Revenue Accurals and Add Revenue Accurals for current year
                engagementDetails.DeleteExistingAccurals();

                //Login as FAS CAO User and validate the user
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("CAO User: " + stdUser1 + " is able to login ");

                //Get the Engagement name and update value of Total Estimated Fees
                engHome.ClickEngageManager();
                engManager.ClickEngageName();                
                engagementDetails.UpdateTotalEstFee("10.00");
                string valTotalEstFee = engagementDetails.GetTotalEstFeeFAS();
                extentReports.CreateLog("Total Estimated Fees value is updated : " + valTotalEstFee +" on engagement details page ");

                //Verify that Revenue Accrual record is created with same value of Total Estimated Fee
                string valEstFees = engagementDetails.GetTotalEstFeeFromRevenueAccrualRecord();
                Assert.AreEqual(valTotalEstFee, valEstFees);
                extentReports.CreateLog("Revenue Accrual record is created with same value of Total Estimated Fee for current month ");

                //Logout of FAS CAO user and validate with admin user
                usersLogin.UserLogOut();
                extentReports.CreateLog("FAS CAO user logged out successfully ");
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");            
                                engHome.SearchEngagementWithName(engName);

                //Validate value of Period Accrual Fee matches with entered Revenue Accrual Fees 
                string periodAccrualFeeNet = engagementDetails.GetPeriodAccrualFeeNetValue();
                string periodAccrualFee = engagementDetails.GetPeriodAccrualValueFAS();
                Assert.AreEqual(periodAccrualFeeNet, "USD "+periodAccrualFee);
                extentReports.CreateLog("Period Accrual Fee in Revenue Accrual record matches with the Period Accrued Fee listed on the Engagement ");

                //Validate value of Total Est Fee in Revenue Accrual record matches with value in engagement details 
                string valEstFeeAccrual = engagementDetails.GetTotalEstFeeFromRevenueAccrualRecord();
                string valEstFee = engagementDetails.GetTotalEstFeeFAS();
                Assert.AreEqual(valEstFeeAccrual, valEstFee);
                extentReports.CreateLog("Total Est Fee in Revenue Accrual record matches with the Total Est Fee listed on the Engagement ");
                                
                //Update value of Total Est Fee in engagament details
                engagementDetails.UpdateTotalEstFee("5.00");

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


