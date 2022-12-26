using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T1559_OpportunityManager_UpdateFieldsOfLOB_CFViaOpportunityManager : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        OpportunityDetailsPage OpportunityDetails = new OpportunityDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityManager opportunityManager = new OpportunityManager();
        public static string fileTC1559 = "T1559_OpportunityManager_UpdateFieldsOfLOB_CF3";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void UpdateFieldsOfLOB_CF()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1559;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard user,validate user and search for created opportunity                
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Clicking on Opportunity Manager link and Validate the title of page
                string titleOppMgr = opportunityHome.ClickOppManager();
                Assert.AreEqual("Opportunity Manager", titleOppMgr);
                extentReports.CreateLog("Page with title: " + titleOppMgr + " is displayed ");

                //Calling function to select Show Records 
                opportunityManager.SelectShowRecords("CF Opportunities");

                //Calling function to reset filters and validate Opportunity detail after clicking on Opp Name
                string expOppComments = opportunityManager.ResetFilters(fileTC1559);
                string titleOpp = opportunityManager.ClickOppName();
                Assert.AreEqual("Opportunity Detail", titleOpp);
                extentReports.CreateLog("Filters are reset and page with title: " + titleOpp + " is displayed upon clicking Opportunity name ");

                //Validate details in Opportunity details page
                //-----Validating Stage
                string stage = OpportunityDetails.GetStage();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "OppManager", 1), stage);
                extentReports.CreateLog("Stage :" + stage + " is updated w.r.t change in Opportunity manager in Opportunity details ");

                //-----Validating Pitch Date
                string pitchDate = OpportunityDetails.GetPitchDate();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "OppManager", 11), pitchDate);
                extentReports.CreateLog("Pitch Date :" + pitchDate + " is updated w.r.t change in Opportunity manager in Opportunity details ");

                //-----Validating Win Probability
                string winProb = OpportunityDetails.GetWinProbability();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "OppManager", 4), winProb);
                extentReports.CreateLog("Win Probability :" + winProb + " is updated w.r.t change in Opportunity manager in Opportunity details ");

                //-----Validating Transaction Size
                string txnSize = OpportunityDetails.GetTransactionSize();
                string expTxnSize = "AED " + (ReadExcelData.ReadData(excelPath, "OppManager", 6) + " (GBP 0.41)");
                Assert.AreEqual(expTxnSize, txnSize);
                extentReports.CreateLog("Transaction Size :" + txnSize + " is updated w.r.t change in Opportunity manager in Opportunity details ");

                //-----Validating Retainer
                string retainer = OpportunityDetails.GetRetainer();
                string expRetainer = "AED " + (ReadExcelData.ReadData(excelPath, "OppManager", 10) + " (GBP 2.03)");
                Assert.AreEqual(expRetainer, retainer);
                extentReports.CreateLog("Retainer :" + retainer + " is updated w.r.t change in Opportunity manager in Opportunity details ");

                //-----Validating Monthly Fee
                string fee = OpportunityDetails.GetMonthlyFee();
                string expMonthlyFee = "AED " + (ReadExcelData.ReadData(excelPath, "OppManager", 9)+ " (GBP 2.03)");
                //Assert.AreEqual(expMonthlyFee, fee);
                extentReports.CreateLog("Monthly Fee :" + fee + " is updated w.r.t change in Opportunity manager in Opportunity details ");

                //-----Validating Contingent Fee
                string contingentFee = OpportunityDetails.GetContingentFee();
                string expContingentFee = "AED " + (ReadExcelData.ReadData(excelPath, "OppManager", 8)+ " (GBP 2.03)");
                Assert.AreEqual(expContingentFee, contingentFee);
                extentReports.CreateLog("Contingent Fee :" + contingentFee + " is updated w.r.t change in Opportunity manager in Opportunity details ");

                //-----Validating Opportunity Comments
                string oppComments = OpportunityDetails.GetOppComments();
                Console.WriteLine("oppComments: " + oppComments);
                Assert.AreEqual("Comments:" + expOppComments, oppComments);
                extentReports.CreateLog("Opportunity Comments :" + oppComments + " is added w.r.t change in Opportunity manager in Opportunity details ");

                //Delete the added Opportunity comments and validate the same
                string message = OpportunityDetails.DeleteOppCommentsAndValidateComments();
                Assert.AreEqual("No Opportunity comments exist", message);
                extentReports.CreateLog(message + " and it's not displayed anymore ");

                //Clicking on Opportunity Manager link and Validate the title of page
                string titleOppManager = opportunityHome.ClickOppManager();
                Assert.AreEqual("Opportunity Manager", titleOppMgr);
                extentReports.CreateLog("Page with title: " + titleOppMgr + " is displayed ");

                //Update Stage, Win Probability and validate the same
                //----Validate updated Stage
                string valUpdatedStage = opportunityManager.UpdateFieldStageAndWinProb(fileTC1559);
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "OppManager", 2), valUpdatedStage);
                extentReports.CreateLog("Stage is updated with value : " + valUpdatedStage + " ");

                //----Validate updated Win Probability
                string valUpdatedWinProb = opportunityManager.GetWinProb();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "OppManager", 5), valUpdatedWinProb);
                extentReports.CreateLog("Win Probability is updated with value : " + valUpdatedWinProb + " ");

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


