using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1898_FREngagementSummary_PostTransactionInformation_DebtStructure_AddEditDel : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1898 = "T1891_DebtStructure_AddEditDel";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void DebtStructure_AddEditDel()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1898;
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

                //Click Post-Transactions Information Tab
                summaryPage.ClickPostTransInfoTab();

                //Validate title of window i.e. Add New Post-Transaction Debt Structure
                string titleAddDebtStructure = summaryPage.ValidatePostAddDebtTitle();
                Assert.AreEqual("Add New Post-Transaction Debt Structure", titleAddDebtStructure);
                extentReports.CreateLog("Window with title: " + titleAddDebtStructure + " is displayed upon clicking Add Debt Structure button ");

                //Validate message "*Save Record to Add Key Creditors"
                string msgSaveRecord = summaryPage.ValidateSaveRecordMessage();
                Assert.AreEqual("*Save Record to Add Key Creditors", msgSaveRecord);
                extentReports.CreateLog("Message: " + msgSaveRecord + " is displayed in Add Debt Structure window ");

                //Enter and save Debt Structure details
                string msgSuccess = summaryPage.SaveDebtStructureDetails(fileTC1898);
                Assert.AreEqual("Success:" + '\r' + '\n' + "Record Saved", msgSuccess);
                extentReports.CreateLog(msgSuccess + " is displayed upon adding Debt Structure ");

                //Validate the added Debt Structure details
                Assert.IsTrue(summaryPage.ValidatePostDebtStructureValues(), "Verified that values are same");
                extentReports.CreateLog("Debt Structure values are displayed as expected in Post-Transaction Information tab ");

                //Edit the added Debt Structure details and Validate the same
                string ValSecurityType = summaryPage.UpdateAndValidatePostDebtStructureValue(fileTC1898);
                Assert.AreEqual("Bank Debt (Second Lien)", ValSecurityType);
                extentReports.CreateLog("Security Type is updated to: " + ValSecurityType + " ");

                //Delete the added Debt Structure details and Validate the same
                string msgDelete = summaryPage.DeleteAndValidatePostDebtStructureRecord();
                Assert.AreEqual("No records to display", msgDelete);
                extentReports.CreateLog(msgDelete + " is displayed upon deleting the Debt Structure record ");
                
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



