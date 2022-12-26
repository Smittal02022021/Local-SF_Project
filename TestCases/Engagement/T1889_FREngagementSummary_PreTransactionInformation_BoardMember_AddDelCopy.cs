using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1889_FREngagementSummary_PreTransactionInformation_BoardMember_AddDelCopy : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1889 = "T1877_HLFinancing_FieldsAndValues";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void BoardMember_AddDelCopy()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1889;
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

                //Click Pre-Transactions Information Tab
                summaryPage.ClickPreTransInfoTab();

                //Click on Add Board Member button and save the details
                string buttonAddBoard = summaryPage.ValidateAddBoardMemberButton();
                Assert.AreEqual("Add Board Member button is displayed", buttonAddBoard);
                extentReports.CreateLog("Button with name: " + buttonAddBoard + " ");

                //Validate title of window i.e. Add New Pre-Transaction Board Member
                string titleAddBoardMember = summaryPage.ValidateAddBoardMemberTitle();
                Assert.AreEqual("Add Pre-Transaction Board Member", titleAddBoardMember);
                extentReports.CreateLog("Window with title: " + titleAddBoardMember + " is displayed upon clicking Add Board Member button ");

                //Enter and save Board Member details
                string msgSuccess = summaryPage.SaveBoardMemberDetails();
                Assert.AreEqual("Success:" + '\r' + '\n' + "Selections Saved", msgSuccess);
                extentReports.CreateLog(msgSuccess + " is displayed upon adding Board Member ");

                //Validate the added Board Member details
                string valMember = summaryPage.ValidateAddedBoardMember();
                Assert.AreEqual("Adam Daland", valMember);
                extentReports.CreateLog("Board Member with name: " + valMember + " is added ");

                //Copy the added Board Member details 
                summaryPage.CopyBoardMemberRecord();

                //Click Post-Transactions Information Tab and validate the copied Board Member record
                summaryPage.ClickPostTransInfoTab();
                Assert.IsTrue(summaryPage.ValidateCopiedBoardMemberValues(), "Verified that headers are same");
                extentReports.CreateLog("Copied Board Member values are displayed as expected in Post-Transaction Information tab ");

                //Delete the copied Board Member record from Post Transaction Information tab
                summaryPage.DeleteAndValidatePostBoardMemberRecord();

                //Delete the added Board Member details and Validate the same
                summaryPage.ClickPreTransInfoTab();
                string msgDelete = summaryPage.DeleteAndValidateBoardMemberRecord();
                Assert.AreEqual("No records to display", msgDelete);
                extentReports.CreateLog(msgDelete + " is displayed upon deleting the Board Member record ");

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



