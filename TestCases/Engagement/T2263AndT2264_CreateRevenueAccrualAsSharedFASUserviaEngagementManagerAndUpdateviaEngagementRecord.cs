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
    class T2263AndT2264_CreateRevenueAccrualAsSharedFASUserviaEngagementManagerAndUpdateviaEngagementRecord : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementManager engManager = new EngagementManager();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();


        public static string fileTC2263 = "T2263_CreateRevenueAccrual";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FASRevenueAccrual_TotalEstimatedFeeChange()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2263;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login "); 

                //Login as standard user                
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function 
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunities(valJobType,fileTC2263);
                Console.WriteLine("value : " + value);
                extentReports.CreateLog("Opportunity : " + value + " is created ");
                                
                //Call function to update HL -Internal Team details
                clientSubjectsPage.EnterStaffDetails(fileTC2263);               

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact         
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(fileTC2263, valContact, valRecordType, valContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForFVAConversion(fileTC2263);
                opportunityDetails.UpdateInternalTeamDetails(fileTC2263);
                extentReports.CreateLog("Internal Team member details are updated ");

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileTC2263);
                extentReports.CreateLog("Conflict Check fields are updated ");

                //Login again as Standard User
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser2 = login.ValidateUser();
                Assert.AreEqual(stdUser2.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser2 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Requesting for engagement and validate the success message
                string msgSuccess = opportunityDetails.ClickRequestEng();
                Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                //Log out of Standard User
                usersLogin.UserLogOut();

                //Login as CAO user to approve the Opportunity
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Approve the Opportunity, convert to Engagment and assign engagment number
                opportunityDetails.ClickApproveButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog("Opportunity is approved ");
                opportunityDetails.ClickConvertToEng();
                //engagementDetails.EnterEngNumberAndSave();
                string engNumber = engagementDetails.GetEngagementNumber();

                //Logout of CAO and login as shared FAS user
                usersLogin.UserLogOut();
                string valSharedUser = ReadExcelData.ReadData(excelPath, "Users",3);
                usersLogin.SearchUserAndLogin(valSharedUser);
                string stdUser3 = login.ValidateUser();
                Assert.AreEqual(stdUser3.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser3 + " is able to login ");

                //Clicking on Engagement Manager link and Validate the title of page
                string titleEngMgr = engHome.ClickEngageManager();
                Assert.AreEqual("Engagement Manager", titleEngMgr);
                extentReports.CreateLog("Page with title: " + titleEngMgr + " is displayed ");

                //Calling function to search by Engagement number and update Total estimate value
                engManager.SearchByEngNumber(engNumber);
                engManager.UpdateTotalEstFeeWithCAOUser("15");
                extentReports.CreateLog("Total Estimate fee is updated ");
                string titleEngDetails = engManager.ClickFREngageName();
                Assert.AreEqual("Engagement Detail", titleEngDetails);
                extentReports.CreateLog("Page with title: " + titleEngDetails + " is displayed upon clicking Engagement Name ");

                //Validate if revenue accural for current month is created
                string month = engagementDetails.GetMonthFromRevenueAccrualRecord();
                Console.WriteLine(DateTime.Now.ToString("yyyy - M ", CultureInfo.InvariantCulture));
                Assert.AreEqual("2021 - 04", month);
                extentReports.CreateLog("Revenue Accrual record with : " + month + " is created ");

                //Update Total Estimate fee value in engagement details page and validate the change reflected in Revenue Accrual record
                engagementDetails.UpdateTotalEstFee("10.00");
                extentReports.CreateLog("Total Estimate Fees is updated in Engagement details page ");
                string estFees = engagementDetails.GetTotalEstFeeFromRevenueAccrualRecord();
                Assert.AreEqual("USD 10.00", estFees);
                extentReports.CreateLog("Updated Total Estimate Fees is reflected in Revenue Accrual record ");

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



