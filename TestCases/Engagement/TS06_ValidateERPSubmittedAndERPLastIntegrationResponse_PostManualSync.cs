using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;


namespace SalesForce_Project.TestCases.Engagement
{
    class TS06_ValidateERPSubmittedAndERPLastIntegrationResponse_PostManualSync : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();       
        UsersLogin usersLogin = new UsersLogin();
        EngagementDetailsPage engDetails = new EngagementDetailsPage();             

        public static string ERP = "ERPPostCreationOfOpportunity.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void PostManualSync()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERP;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                 Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                 extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                 string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Login as Standard User and validate the user                
                //usersLogin.SearchUserAndLogin(valUser);
                //string stdUser = login.ValidateUser();
                //Assert.AreEqual(stdUser.Contains(valUser), true);
                //extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Search for an engagement
                engHome.SearchEngagementWithName("FGIC");
                extentReports.CreateLog("Matching record is displayed ");
                                               
                //Get ERP Submitted to Sync, Status, ERP Update DFF checkbox and ERP Last Integration Response Date
                string ERPSubmitted = engDetails.GetERPSubmittedToSync();
                extentReports.CreateLog("ERP Submitted to Sync before update is: " + ERPSubmitted +" ");               
                               
                string ERPResDate = engDetails.GetERPIntegrationResponseDate();                
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + ERPResDate + " is displayed ");

                string ERPStatus = engDetails.GetERPIntegrationStatus();               
                extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatus + " is displayed ");

                //-----Schedule ERP Submitted to Sync manually, validate ERP Update DFF checkbox, ERP Sync Date, Status and Last Integration Status -----
                engDetails.ScheduleERPSyncManually();
                string ERPSubmittedPostSync = engDetails.GetERPSubmittedToSync();
                Assert.AreNotEqual(ERPSubmitted, ERPSubmittedPostSync);
                extentReports.CreateLog("ERP Submitted to Sync : " + ERPSubmittedPostSync + " is updated post scheduling ERP sync ");
                                          
                string ERPResDatePostSync = engDetails.GetERPIntegrationResponseDate();
                Assert.AreNotEqual(ERPResDate, ERPResDatePostSync);
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + ERPResDatePostSync + " is displayed post ERP sync ");

                string ERPStatusPostSync = engDetails.GetERPIntegrationStatus();
                //Assert.AreEqual("Success", ERPStatusPostSync);
                extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusPostSync + " is displayed post ERP sync ");
                               
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

    

