using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.Pages.TimeRecordManager;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using SalesForce_Project.Pages.HomePage;
using System;

namespace SalesForce_Project.TestCases.TimeRecordManager
{
    class TMTT0013748_TMTT0013750_TMTT0013751_TMTT0013753_VerifyNewTitleRateSheetAvailability_CalculatedAmountOnTimeRecordManagerAfterAddingRateSheet : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        TimeRecorderFunctionalities timeRecorder = new TimeRecorderFunctionalities();

        public static string fileTMT13748= "TMTT0013748_TMTT0013750_TMTT0013751_TMTT0013753_VerifyNewTitleRateSheetAvailability_CalculatedAmountOnTimeRecordManagerAfterAddingRateSheet";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyNewTitleRateSheetAvailability_CalculatedAmountOnTimeRecordManagerAfterAddingRateSheet()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT13748;
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
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser1 + " is able to login ");

                //Call function to open Add Opportunity Page & Select LOB
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunities(valJobType, fileTMT13748, 2);
                extentReports.CreateLog("Required fields added in order to create an opportunity. ");

                //Call function to enter Internal Team details
                clientSubjectsPage.EnterMembersToDealTeam(fileTMT13748);
                extentReports.CreateLog("Members added to the deal team. ");

                //Fetch values of Opportunity Number, Name, Client, Subject
                string oppNum = opportunityDetails.GetOppNumber();
                string oppName = opportunityDetails.GetOpportunityName();
                string clientName = opportunityDetails.GetClient();
                string subjectName = opportunityDetails.GetSubject();

                //Validating Opportunity created successfully
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with LOB : " + valRecordType + " is created. Opportunity number is : " + oppNum + ". ");

                //Create External Primary Contact         
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(fileTMT13748, valContact, valRecordType, valContactType);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved. ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForFVAConversionMultipleRows(fileTMT13748, 2);
                extentReports.CreateLog("Fields required for converting FVA opportunity to engagement are updated. ");

                //Log out from standard User
                usersLogin.UserLogOut();

                //Search for created opportunity
                opportunityHome.SearchOpportunity(oppName);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileTMT13748);
                if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                {
                    opportunityDetails.UpdateNBCApproval();
                    extentReports.CreateLog("Conflict Check and NBC fields are updated ");
                }
                else
                {
                    extentReports.CreateLog("Conflict Check fields are updated ");
                }

                //Update Client and Subject to Accupac bypass EBITDA field validation for JobType- Sellside
                if (valJobType.Equals("Sellside"))
                {
                    opportunityDetails.UpdateClientandSubject("Accupac");
                    extentReports.CreateLog("Updated Client and Subject fields ");
                }
                else
                {
                    Console.WriteLine("Not required to update ");
                }

                //Login again as Standard User
                usersLogin.SearchUserAndLogin(valUser);
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser1 + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Requesting for engagement and validate the success message
                string msgSuccess = opportunityDetails.ClickRequestEng();
                Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                extentReports.CreateLog("Request for Engagement success message: " + msgSuccess + " is displayed. ");

                //Log out of Standard User
                usersLogin.UserLogOut();

                //Login as CAO user to approve the Opportunity
                homePage.SearchUserByGlobalSearch(fileTMT13748, ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                usersLogin.LoginAsSelectedUser();
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2)), true);
                extentReports.CreateLog("CAO User: " + caoUser + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Approve the Opportunity 
                opportunityDetails.ClickApproveButton();
                extentReports.CreateLog("Opportunity number: " + oppNum + " is approved. ");

                //Calling function to convert to Engagement
                opportunityDetails.ClickConvertToEng();
                extentReports.CreateLog("Opportunity number: " + oppNum + " is successfuly converted into Engagement. ");

                //Get the engagement number
                string engNum = engagementDetails.GetEngagementNumber();

                //Log out of CAO User
                usersLogin.UserLogOut();

                //Login again as Standard User
                usersLogin.SearchUserAndLogin(valUser);
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser1 + " is able to login ");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "RateSheetManagement");

                for (int row = 9; row <= rowCount; row++)
                {
                    //Navigate to Title Rate Sheets page
                    rateSheetMgt.NavigateToTitleRateSheetsPage();
                    extentReports.CreateLog(driver.Title + " page is displayed ");

                    //Click on the new title rate sheet name
                    rateSheetMgt.ClickNewTitleRateSheet(ReadExcelData.ReadData(excelPath, "RateSheetManagement", 3));
                    extentReports.CreateLog("New Title rate sheet is selected. ");

                    //Verify the correct title rate sheet is opened
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Title Rate Sheet: TAS - FY23 - DVC ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " page is displayed ");

                    //Get the default rate of user as per role
                    string userRole = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1);
                    double defaultRate = rateSheetMgt.GetDefaultRateAsPerRole(userRole);
                    extentReports.CreateLog(userRole + " is : USD " + defaultRate + " ");

                    //Click Time Record Manager Tab
                    homePage.ClickTimeRecordManagerTab();
                    extentReports.CreateLog("Time Record Manager page is displayed. ");

                    //Select Staff Member from the list
                    string name = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                    timeEntry.SelectStaffMember(name);
                    string selectedStaffMember = timeEntry.GetSelectedStaffMember();
                    Assert.AreEqual(name, selectedStaffMember);
                    extentReports.CreateLog("Staff Member Title: " + selectedStaffMember + " is displayed upon click of staff Member link ");

                    //Select Project 
                    string projName = "E - " + oppName + "-" + valRecordType + "-" + engNum;
                    timeEntry.SelectProjectWeeklyEntryMatrix(projName, fileTMT13748);

                    //Enter time under enter weekly time matrix
                    timeEntry.LogCurrentDateHours(fileTMT13748);
                    extentReports.CreateLog("Number of hours logged for " + name + " is : " + ReadExcelData.ReadData(excelPath, "Update_Timer", 1) + " ");

                    //Go to Summary Logs
                    timeEntry.GoToSummaryLog();
                    extentReports.CreateLog("User has navigated to Summary logs ");

                    // Get total amount displayed
                    double totalAmountDisplayedInSummaryLog = timeEntry.GetTotalAmount();

                    //Verify displayed amount before entering a new time sheet
                    Assert.AreEqual(0, totalAmountDisplayedInSummaryLog);
                    extentReports.CreateLog("Total amount displayed under Summary logs before entering new time sheet : " + totalAmountDisplayedInSummaryLog + " ");

                    //Go to Details log
                    timeRecorder.GoToDetailLogs();
                    extentReports.CreateLog("User has naigated to details log ");

                    // Get total amount displayed
                    double totalAmountDisplayedInDetailLog = timeEntry.GetTotalAmount();

                    //Verify displayed amount before entering a new time sheet
                    Assert.AreEqual(0, totalAmountDisplayedInDetailLog);
                    extentReports.CreateLog("Total amount displayed under Detail logs before entering new time sheet : " + totalAmountDisplayedInDetailLog + " ");

                    //Enter Rate Sheet details
                    string engagement = oppName + " - " + engNum;
                    string rateSheet = ReadExcelData.ReadData(excelPath, "RateSheetManagement", 3);
                    rateSheetMgt.EnterRateSheet(engagement, rateSheet);

                    //Verify selected rate sheet
                    string selectedRateSheet = rateSheetMgt.GetSelectedRateSheet();
                    Assert.AreEqual(rateSheet, selectedRateSheet);
                    extentReports.CreateLog("Selected Rate Sheet: " + selectedRateSheet + " is displayed upon entering rate sheet details ");

                    //Navigate to Weekly Entry Matrix page
                    timeEntry.GoToWeeklyEntryMatrix();

                    //Go to Summary Logs
                    timeEntry.GoToSummaryLog();
                    extentReports.CreateLog("User has navigated to Summary logs ");

                    //Get Entered Hours displayed
                    double enteredHours = timeEntry.GetEnteredHoursInSummaryLogValue();

                    //Get total amount calculated with default rate and entered hours
                    double totalAmountCalculated = defaultRate * enteredHours;

                    // Get total amount displayed
                    double totalAmountDisplayed = timeEntry.GetTotalAmount();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountCalculated, totalAmountDisplayed);
                    extentReports.CreateLog("Total Amount: USD " + totalAmountDisplayed + " displayed is matching with the calculation based on total hours entered and the default rate based on staff role under summary log tab.  ");

                    //Go to Details log
                    timeRecorder.GoToDetailLogs();
                    extentReports.CreateLog("User has naigated to details log ");

                    //Get total amount displayed
                    double totalAmountDisplayedInDetailLog1 = timeEntry.GetTotalAmount();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountCalculated, totalAmountDisplayedInDetailLog1);
                    extentReports.CreateLog("Total Amount: USD " + totalAmountDisplayedInDetailLog1 + " displayed is matching with the calculation based on total hours entered and the default rate based on staff role under details log tab.  ");

                    //Edit hours in detail log
                    timeEntry.EditEnteredHoursInDetailLog(fileTMT13748);
                    extentReports.CreateLog("Hours logged are edited under detail log section. ");

                    //Navigate to Weekly Entry Matrix page
                    timeEntry.GoToWeeklyEntryMatrix();

                    //Go to Summary Logs
                    timeEntry.GoToSummaryLog();
                    extentReports.CreateLog("User has navigated to Summary logs ");

                    //Get Entered Hours displayed
                    double editedHoursInSummaryLog = timeEntry.GetEnteredHoursInSummaryLogValue();

                    //Get total amount calculated with default rate and entered hours
                    double totalAmountReCalculated = defaultRate * editedHoursInSummaryLog;

                    // Get total amount displayed
                    double totalEditedAmountDisplayedInSummaryLog = timeEntry.GetTotalAmount();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountReCalculated, totalEditedAmountDisplayedInSummaryLog);
                    extentReports.CreateLog("Total Amount: USD " + totalEditedAmountDisplayedInSummaryLog + " displayed is matching with the calculation based on total hours entered and the default rate based on staff role under summary log tab.  ");

                    //Go to Details log
                    timeRecorder.GoToDetailLogs();
                    extentReports.CreateLog("User has naigated to details log ");

                    //Get total amount displayed
                    double totalEditedAmountDisplayedInDetailLog = timeEntry.GetTotalAmount();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountReCalculated, totalEditedAmountDisplayedInDetailLog);
                    extentReports.CreateLog("Total Amount: USD " + totalEditedAmountDisplayedInDetailLog + " displayed is matching with the calculation based on total hours entered and the default rate based on staff role under details log tab.  ");

                    //Delete time entry records from detail log 
                    timeEntry.RemoveRecordFromDetailLogs();
                    extentReports.CreateLog("Deleted record entry successfully from detail log ");

                    //Delete rate sheet
                    rateSheetMgt.DeleteRateSheet(engagement);
                    extentReports.CreateLog("Deleted rate sheet entry successfully after verification ");
                }
                //Log out of Standard User
                usersLogin.UserLogOut();
                extentReports.CreateLog("Standard User has logged out. ");

                //Log out of Admin User
                usersLogin.UserLogOut();
                extentReports.CreateLog("Admin User has logged out. ");
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
