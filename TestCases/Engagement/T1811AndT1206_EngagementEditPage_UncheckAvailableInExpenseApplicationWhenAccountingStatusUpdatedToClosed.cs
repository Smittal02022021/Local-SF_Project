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
    class T1811AndT1206_EngagementEditPage_UncheckAvailableInExpenseApplicationWhenAccountingStatusUpdatedToClosed : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementManager engManager = new EngagementManager();

        public static string fileTC1811 = "T1873_FRDisplayedForSpecificJobTypes";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void UncheckAvailableInExpenseApplicationWhenAccountingStatusSetToClosed()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1811;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
               
                //Search for required engagement
                engHome.SearchEngagementWithName("Delaware River Solar");
                extentReports.CreateLog("Matching record is found and Engagement Detail page is displayed ");
               
                //Update Accounting Status and Stage to Closed
                engagementDetails.UpdateAccountingStatusAndStage("Closed","Closed");
                string valAccStatus = engagementDetails.GetAccontingStatus();
                Assert.AreEqual("Closed", valAccStatus);
                extentReports.CreateLog("Value of Accounting Status is updated to " +valAccStatus + " ");
                
                //Validate if Available In Expense Application checkbox is checked or not
                string chkValue1 = engagementDetails.ValidateIfExpenseApplicationIsChecked();
                Assert.AreEqual("Expense Application checkbox is not checked", chkValue1);
                extentReports.CreateLog(chkValue1 + "  when the Accounting Status is set to “Closed” on an Engagement ");

                //Update Accounting Status and Stage to Open
                engagementDetails.UpdateAccountingStatusAndStage("Open", "Retained");
                string valAccStatus2 = engagementDetails.GetAccontingStatus();
                Assert.AreEqual("Open", valAccStatus2);
                extentReports.CreateLog("Value of Accounting Status is updated to " + valAccStatus2 +" ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Std User: " + stdUser + " is able to login ");

                engHome.SearchEngagementWithName("Delaware River Solar");
                string ownership = engagementDetails.UpdateClientOwnershipAndDebt("Bank","13.00");
                extentReports.CreateLog("Client ownership and Debt details are updated ");
                Assert.AreEqual("Bank", ownership);                
                //string debt = engagementDetails.GetTotalDebt();
                //Assert.AreEqual("13.00",debt);
                extentReports.CreateLog("Updated Client Ownership details are displayed ");

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
