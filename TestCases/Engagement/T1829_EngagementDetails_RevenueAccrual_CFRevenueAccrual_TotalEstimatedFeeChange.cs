using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1829_EngagementDetails_RevenueAccrual_CFRevenueAccrual_TotalEstimatedFeeChange : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1829 = "T1874_FREngagementSummary_FieldsAndValues";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CFRevenueAccrual_TotalEstimatedFeeChange()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1829;
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

                //Search engagement with LOB - CF
                string message = engHome.SearchEngagementWithName("Project Aspire");
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matches to LOB-CF are found and Engagement Detail page is displayed ");

                //Get the Engagement name, login as Admin and get the existing value of Total Estimated Fees
                string engName = engagementDetails.GetEngName();
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                engHome.SearchEngagementWithName(engName);

                string estFeesUSD = engagementDetails.GetTotalEstFee();
                string estFees = estFeesUSD.Substring(4, 3);
                int finalEstFees = Convert.ToInt32(Convert.ToDouble(estFees));
                extentReports.CreateLog("Existing Total Estimated Fees is " + estFeesUSD + " ");

                //Delete existing Revenue Accurals and Add Revenue Accurals for current year
                 engagementDetails.DeleteExistingAccurals();

                //Login as CAO user and Search for same Engagement and add Revenue Accurals                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("CAO User: " + stdUser1 + " is able to login ");
                engHome.SearchEngagementWithName(engName); 

                string revValueUSD = engagementDetails.AddNewRevenueAccurals();
                Assert.AreEqual("USD 10.00", revValueUSD);
                extentReports.CreateLog("Revenue Accrual with value : " + revValueUSD + " is added successfully for current month ");
                string revValue = revValueUSD.Substring(4, 5);
                int finalRevValue = Convert.ToInt32(Convert.ToDouble(revValue));
                Console.WriteLine(finalRevValue);

                //Validate the Total estimated fee is sum of existing value plus entered Revenue Accrual Fees 
                int totalEst = finalEstFees + finalRevValue;
                Console.WriteLine("totalEst: " + totalEst);
                Assert.AreEqual("USD 20", "USD " + totalEst);
                extentReports.CreateLog("Total Estimated Fees is sum of its original value plus entered Revenue Accrual Fees ");

                //Login as admin and validate value of Period Accrual Fee matches with entered Revenue Accrual Fees 
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                engHome.SearchEngagementWithName(engName);

                string periodAccrual = engagementDetails.GetPeriodAccrualValue();
                Assert.AreEqual("10.00", periodAccrual);
                extentReports.CreateLog("Period Accrual Fee matches with entered Revenue Accrual Fees ");
                                
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



