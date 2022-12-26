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
    class T2250_EngagementManager_AbilityToCreateNewRevenueAccrualInEngagementManagerForFREngagement : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementManager engManager = new EngagementManager();
        SendEmailNotification notification = new SendEmailNotification();

        public static string fileTC2250 = "T2250_CreateNewRevenueAccrual_FREngagement";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CreateNewRevenueAccrual_FREngagement()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2250;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Calling funtion to get number of users
                int rowUsers = ReadExcelData.GetRowCount(excelPath, "Users");
                Console.WriteLine("rowUsers " + rowUsers);

                for (int row = 2; row <= rowUsers; row++)
                {
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                     
                    //Login as User and validate the user                        
                     usersLogin.SearchUserAndLogin(valUser);
                     string stdUser = login.ValidateUser();
                     Assert.AreEqual(stdUser.Contains(valUser), true);
                     extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");                    

                    //Clicking on Engagement Manager link and Validate the title of page
                    string titleEngMgr = engHome.ClickEngageManager();
                    Assert.AreEqual("Engagement Manager", titleEngMgr);
                    extentReports.CreateLog("Page with title: " + titleEngMgr + " is displayed ");

                    //Calling function to select Show Records 
                    engManager.SelectShowRecords("FR Engagements");
                    string titleEngDetails = engManager.ClickFREngagementName();
                    Assert.AreEqual("Engagement Detail", titleEngDetails);
                    extentReports.CreateLog("Page with title: " + titleEngDetails + " is displayed upon clicking Engagement Name ");
                    string engName = engagementDetails.GetEngName();

                    //Logout of SFFRCAO user and validate with admin user and delete any existing Revenue Accurals  
                    usersLogin.UserLogOut();
                    extentReports.CreateLog(valUser + " logged out successfully ");
                    engHome.SearchEngagementWithName(engName);
                    engagementDetails.DeleteExistingAccurals();
                    extentReports.CreateLog("No revenue accruals exists on Engagement details page ");

                    //Login as User and validate the user
                     usersLogin.SearchUserAndLogin(valUser);
                     string stdUser1 = login.ValidateUser();
                     Assert.AreEqual(stdUser1.Contains(valUser), true);
                     extentReports.CreateLog("Standard User: " + stdUser1 + " is able to login ");

                    if (valUser.Equals("Simone Vitale"))
                    {
                        engHome.ClickEngageManager();
                        string actualFeeEnabled = engManager.ValidateIfActualMonthlyFeeIsEnabled();
                        Assert.AreEqual("Actual Monthly Fee column is read only", actualFeeEnabled);
                        extentReports.CreateLog(actualFeeEnabled +" for " + valUser +" ");

                        string actualTxnEnabled = engManager.ValidateIfActualTxnFeeIsEnabled();
                        Assert.AreEqual("Actual Transaction Fee column is read only", actualTxnEnabled);
                        extentReports.CreateLog(actualTxnEnabled+" for "+ valUser+ " ");

                        usersLogin.UserLogOut();
                    }
                    else
                    {
                        //Navigate back to Engagement Manager and update Actual Monthly Fee and Actual Transaction Fee
                        engHome.ClickEngageManager();
                        engManager.UpdateActualMonthlyAndTxnFeesValue(ReadExcelData.ReadData(excelPath, "EngMgr", 1), ReadExcelData.ReadData(excelPath, "EngMgr", 2));
                        extentReports.CreateLog("Actual Monthly Fee and Actual Transaction Fee is updated ");

                        //Navigate to engagement details and validate for Revenue Accrual record
                        engManager.ClickFREngagementName();
                        string month = engagementDetails.GetMonthFromRevenueAccrualRecord();
                        //string expDate= 
                        Console.WriteLine(DateTime.Now.ToString("yyyy - M ", CultureInfo.InvariantCulture));
                        Assert.AreEqual("2021 - 11", month);
                        extentReports.CreateLog("Revenue Accrual record with : " + month + " is created ");
                        usersLogin.UserLogOut();
                    }
                }
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




