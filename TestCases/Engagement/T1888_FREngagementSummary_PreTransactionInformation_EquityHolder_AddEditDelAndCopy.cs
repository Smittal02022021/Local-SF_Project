using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1888_FREngagementSummary_PreTransactionInformation_EquityHolder_AddEditDelAndCopy : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1888 = "T1877_HLFinancing_FieldsAndValues";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void EquityHolder_AddEditDelAndCopy()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1888;
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

                //Click on Add Equity Holder button 
                string buttonAddEquity = summaryPage.ValidateAddEquityHolderButton();
                Assert.AreEqual("Add Equity Holder button is displayed", buttonAddEquity);
                extentReports.CreateLog("Button with name: " + buttonAddEquity + " ");

                //Validate title of window i.e. Add New Pre-Transaction Equity Holder
                string titleAddEquityHolder = summaryPage.ValidateAddEquityTitle();
                Assert.AreEqual("Add Pre-Transaction Equity Holder", titleAddEquityHolder);
                extentReports.CreateLog("Window with title: " + titleAddEquityHolder + " is displayed upon clicking Add Equity Holder button ");

                //Enter and save Equity Holder details
                string msgSuccess = summaryPage.SaveEquityHolderDetails();
                Assert.AreEqual("Success:" + '\r' + '\n' + "Company Added To Equity Holders", msgSuccess);
                extentReports.CreateLog("Success Message: " + msgSuccess + " is displayed upon adding Equity Holders ");

                //Validate the added Equity Holder value
                string valEquity = summaryPage.ValidateAddedEquityHolderValue();
                Assert.AreEqual("Techno", valEquity);
                extentReports.CreateLog("Equity Holder with name: " + valEquity + " is added ");

                //Edit the added Equity Holder details and Validate the same
                string ValOwnership = summaryPage.UpdateAndValidateEquityHolderValue();
                Assert.AreEqual("10.00%", ValOwnership);
                extentReports.CreateLog("Percent Ownership is updated to: " + ValOwnership + " ");

                //Copy the added Equity Holder details and Validate the same
                summaryPage.CopyEquityHolderRecord();

                //Click Post-Transactions Information Tab and validate the copied Equity Holder record
                summaryPage.ClickPostTransInfoTab();
                Assert.IsTrue(summaryPage.ValidateCopiedEquityHolderValues(), "Verified that headers are same");
                extentReports.CreateLog("Copied Equity Holders values are displayed as expected in Post-Transaction Information tab ");

                //Delete the copied Equity Holder record from Post Transaction Information tab
                summaryPage.DeleteAndValidatePostEquityHolderRecord();

                //Delete the added Equity Holder details and Validate the same
                summaryPage.ClickPreTransInfoTab();
                string msgDelete = summaryPage.DeleteAndValidateEquityHolderRecord();
                Assert.AreEqual("No records to display", msgDelete);
                extentReports.CreateLog(msgDelete + " is displayed upon deleting the Equity Holder record ");

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
