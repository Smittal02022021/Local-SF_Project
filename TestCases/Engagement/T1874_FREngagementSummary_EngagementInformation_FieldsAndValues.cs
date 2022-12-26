using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1874_FREngagementSummary_EngagementInformation_FieldsAndValues : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1874 = "T1874_FREngagementSummary_FieldsAndValues";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FREngagementSummary_FieldsAndValues()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1874;
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

                //---Validate fields i.e. Transaction Type,Client Description, Post Transaction Status etc. and its values
                //----Validate Transaction Type label-----
                string lblTxnType = summaryPage.GetLabelTransactionType();
                Assert.AreEqual("Transaction Type", lblTxnType);
                extentReports.CreateLog("Field with name: " + lblTxnType + " is displayed ");

                //----Validate values of Transaction Type combo-----               
                Assert.IsTrue(summaryPage.VerifyTxnTypeValues(), "Verified that displayed Transaction types are same");
                extentReports.CreateLog("Values of Transaction Type field are same as expected ");

                //----Validate Client Description label-----
                string lblClientDesc = summaryPage.GetLabelClientDescription();
                Assert.AreEqual("Client Description*", lblClientDesc);
                extentReports.CreateLog("Field with name: " + lblClientDesc + " is displayed ");

                //----Validate Post Transaction Status label-----
                string lblPostTxnStatus = summaryPage.GetLabelPostTxnStatus();
                Assert.AreEqual("Post Transaction Status", lblPostTxnStatus);
                extentReports.CreateLog("Field with name: " + lblPostTxnStatus + " is displayed ");

                //----Validate values of Post Transaction Status combo-----
                Assert.IsTrue(summaryPage.VerifyPostTxnStatusValues(), "Verified that displayed Transaction Statuses are same");
                extentReports.CreateLog("Values of Post Transaction Status field are same as expected ");

                //----Validate Company Description label-----
                string lblCompDesc = summaryPage.GetLabelCompanyDescription();
                Assert.AreEqual("Company Description", lblCompDesc);
                extentReports.CreateLog("Field with name: " + lblCompDesc + " is displayed ");

                //----Validate Restructuring Transaction Description label-----
                string lblTxnDesc = summaryPage.GetLabelTxnDescription();
                Assert.AreEqual("Restructuring Transaction Description", lblTxnDesc);
                extentReports.CreateLog("Field with name: " + lblTxnDesc + " is displayed ");

                //----Validate the message displayed related to HL Client-----
                string lblMessage = summaryPage.GetMessageDisplayed();
                Assert.AreEqual("* Include specific information regarding HL's client. For example: Official Committee of Unsecured Creditors, Special Committee of the Board, Ad Hoc Noteholders Committee, Ad Hoc Committee of Timber Noteholders, Advisor to 1st Lien Agent.", lblMessage);
                extentReports.CreateLog("Message: " + lblMessage + " is displayed ");

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



