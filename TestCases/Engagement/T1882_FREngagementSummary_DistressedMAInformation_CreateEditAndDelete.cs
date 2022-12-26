using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1882_FREngagementSummary_DistressedMAInformation_CreateEditAndDelete : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1882 = "T1882_DistressedMAInformation_CreateEditAndDelete";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void DistressedMAInformation_CreateEditAndDelete()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1882;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Search engagement with LOB - FR
                string message = engHome.SearchEngagementWithName(ReadExcelData.ReadData(excelPath, "Engagement", 2));
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matches to LOB-FR are found and Engagement Detail page is displayed ");

                //Validate FR Engagement Summary button
                string btnFREngSum = engagementDetails.ValidateFREngSummaryButton();
                Assert.AreEqual("True", btnFREngSum);
                extentReports.CreateLog("FR Engagement Summary button is displayed ");

                //Validate FR Engagement Summary page and its fields
                string titleFRSummary = engagementDetails.ClickFREngSummaryButton();
                Assert.AreEqual("FR Engagement Summary", titleFRSummary);
                extentReports.CreateLog("Page with title: " + titleFRSummary + " is displayed upon clicking FR Engagement Summary button ");

                //Click Financials/Projections Tab
                summaryPage.ClickDistressedMAInfoTab();

                //Click Add Distressed M&A Information button and validate Add Distressed M&A Information window 
                string winTitle = summaryPage.ValidateDistressedMAInfoWindow();
                Assert.AreEqual("Add Distressed M&A Information", winTitle);
                extentReports.CreateLog("Window with title: " + winTitle + " is displayed upon clicking Add Distressed M&A Information button ");

                //Add a Distressed M&A Information record and validate all values of added record
                summaryPage.SaveDistressedMAInfoDetails(fileTC1882);

                //Validate the entered Distressed M&A Information record for all values   
                Assert.IsTrue(summaryPage.ValidateDistressedMARecord(), "Verified that values are same");
                extentReports.CreateLog("Distressed M&A Information record is added with entered values ");

                //Edit the added Distressed M&A Information record
                summaryPage.UpdateDistressedMARecord();

                //Validate updated valued of Distressed M&A Information record
                //Validate value of Minimum Overbid (MM)
                string valMinOverbid = summaryPage.GetMinOverload();
                Assert.AreEqual("USD 20.00", valMinOverbid);
                extentReports.CreateLog("Value of Minimum Overbid (MM) has been updated to: " + valMinOverbid + " ");

                //Validate value of Cash Component (MM)
                string valCashComponent = summaryPage.GetCashComp();
                Assert.AreEqual("USD 20.00", valCashComponent);
                extentReports.CreateLog("Value of Cash Component (MM) has been updated to: " + valCashComponent + " ");

                //Delete the added record and validate that no record is displayed 
                string msgNoRec = summaryPage.DeleteExistingRecord();
                Assert.AreEqual("No records to display", msgNoRec);
                extentReports.CreateLog("Added Distressed M&A Information record has been deleted ");

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



