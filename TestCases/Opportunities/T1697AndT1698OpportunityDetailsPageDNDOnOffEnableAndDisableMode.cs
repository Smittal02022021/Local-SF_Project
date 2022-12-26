using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;


namespace SalesForce_Project.TestCases.Opportunity
{
    class T1697AndT1698OpportunityDetailsPageDNDOnOffEnableAndDisableMode : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AdditionalClientSubjectsPage additional = new AdditionalClientSubjectsPage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC1697 = "T1697AndT1698DNDOnOffEnableAndDisableMode.xlsx";


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void DNDOnOffEnableAndDisableMode()
        {
            try
            {
                //Get Test Data file path 
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1697;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin user " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                //usersLogin.ClickManageUsers();
                //usersLogin.LoginAsStandardUser();
                //
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                //
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("Standard User " + stdUser + " is able to login ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                additional.ClickContinue();

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");


                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                //Calling AddOpportunities function                
                string value = addOpportunity.AddOpportunities(valJobType,fileTC1697);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1697);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Validate DND ON/OFF button
                string btnDND = opportunityDetails.ValidateDNDButton();
                Assert.AreEqual("DND On/Off button is not displayed", btnDND);
                extentReports.CreateLog(btnDND + " for user " + stdUser);

                //Log out from Standard User and Log in as CAO user and validate CAO login
                usersLogin.UserLogOut();
                usersLogin.LoginAsCAOUser();
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("CAO User " + caoUser + " is able to login ");

                //Search for added Opportunity, validate DND ON/OFF button and submit for review
                opportunityHome.SearchOpportunity(value);
                string btnDNDCAO = opportunityDetails.ValidateDNDButton();
                Assert.AreEqual("DND On/Off button is displayed", btnDNDCAO);
                extentReports.CreateLog(btnDNDCAO + " for user " + caoUser);
                opportunityDetails.ClickDNDButton();
                extentReports.CreateLog("DND submitted for review ");

                //Verify lock image with CAO user
                string valLock = opportunityDetails.ValidateLockImage();
                Assert.AreEqual("Lock Image is displayed", valLock);
                extentReports.CreateLog(valLock + " for user " + caoUser);

                //Validate record lock message
                string textMessage = opportunityDetails.ValidateRecordLockMessage();
                string expMessage = "This record is locked. If you need to edit it, contact your admin." + "\r\n\r\n" + "Click here to return to the previous page.";
                Assert.AreEqual(expMessage, textMessage);
                extentReports.CreateLog("Message: " + textMessage + " is displayed ");

                //Validate Approver and Staus in Approval History table
                string txtApprover = opportunityDetails.ValidateActualApprover();
                Assert.AreEqual(txtApprover, "DND Approval Q");
                string txtStatus = opportunityDetails.ValidateOverallStatus();
                Assert.AreEqual(txtStatus, "Pending");
                extentReports.CreateLog("Approver: " + txtApprover + " and Status: " + txtStatus + " is updated in Approval History for DND Submission ");

                //Log out from CAO User and validate admin
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin user " + login.ValidateUser() + " logged in ");

                //Search for created opportunity, reject opportunity and validate CAO user login
                driver.Navigate().Refresh();
                string valSearch = opportunityHome.SearchOpportunity(value);
                Console.WriteLine("result : " + valSearch);
                opportunityDetails.ClickRejectButton();
                extentReports.CreateLog("Admin user rejected DND request ");
                usersLogin.ClickManageUsers();
                usersLogin.LoginAsCAOUser();
                string userCAO = login.ValidateUser();
                Assert.AreEqual(userCAO.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("CAO User " + userCAO + " is able to login ");

                //Search for created opportunity and validate for lock image 
                string output = opportunityHome.SearchOpportunity(value);
                extentReports.CreateLog(output + " with original Opportunity Name ");
                string btnLockImageCAO = opportunityDetails.ValidateLockImage();
                Assert.AreEqual("Lock Image is not displayed", btnLockImageCAO);
                extentReports.CreateLog(btnLockImageCAO + " now for user " + userCAO);

                //Validate rejected status in Approval History table
                string valStatus = opportunityDetails.ValidateOverallStatus();
                Assert.AreEqual("Rejected", valStatus);
                extentReports.CreateLog("Approval History is updated with " + valStatus + " ");

                //Click DND ON/OFF button and accept confirmation alerts
                opportunityDetails.ClickDNDButton();
                extentReports.CreateLog("DND submitted for review again ");

                //Log out from CAO User and validate admin
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin user " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity, approve opportunity and validate Opportunity Name
                driver.Navigate().Refresh();
                opportunityHome.SearchOpportunity(value);
                opportunityDetails.ClickApproveButton();
                string approvedOppName = opportunityDetails.GetOpportunityName();
                extentReports.CreateLog(opportunityDetails.ValidateOpportunityName(approvedOppName));

                //Login again as CAO user and validate intially created opportunity before status update
                usersLogin.ClickManageUsers();
                usersLogin.LoginAsCAOUser();
                extentReports.CreateLog("CAO User " + login.ValidateUser() + " is able to login ");
                string result = opportunityHome.SearchOpportunity(value);
                Assert.AreEqual("No record found", result);
                extentReports.CreateLog(result + " with old opportunity name ");

                //Validate opportunity with updated opportunity name post approval
                string updatedOpp = opportunityHome.UpdateOppAndSearch(approvedOppName);
                Assert.AreEqual("Record found", updatedOpp);
                extentReports.CreateLog(updatedOpp + " with DND - " + approvedOppName);

                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
            }
        }       
    }
}


