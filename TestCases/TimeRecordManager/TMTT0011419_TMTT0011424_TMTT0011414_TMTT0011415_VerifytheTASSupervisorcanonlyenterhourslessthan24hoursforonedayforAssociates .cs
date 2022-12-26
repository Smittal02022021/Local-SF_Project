using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.TimeRecordManager;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.TimeRecordManager
{
    class TMTT0011419_TMTT0011424_TMTT0011414_TMTT0011415_VerifytheTASSupervisorcanonlyenterhourslessthan24hoursforonedayforAssociates : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        RefreshButtonFunctionality refreshButton = new RefreshButtonFunctionality();
        TimeRecorderFunctionalities timeRecorder = new TimeRecorderFunctionalities();

        By ButtonRefresh = By.XPath("//button[text()='Refresh']");
        
        public static string fileTMT1411= "TMTT0011424_VerifytheTASSupervisorcanonlyenterhourslessthan24hoursforonedayforAssociates";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTASSupervisorFunctionalities()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT1411;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser1 + " is able to login ");

                string defaultRate = rateSheetMgt.GetDefaultRateForAssociateAsPerRateSheet("TAS");
                double DefaultRateForStaff = Convert.ToDouble(defaultRate);

                //Verify TAS Supervisor is able to make own time entries
                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();
                
                string selectProject1 = ReadExcelData.ReadData(excelPath, "Project_Title", 1);

                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject1, fileTMT1411);
                timeEntry.LogCurrentDateHours(fileTMT1411);

                driver.Navigate().Refresh();

                Thread.Sleep(3000);
                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();

                //Go to Summary Logs
                timeEntry.GoToSummaryLog();
               
                extentReports.CreateLog("User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                string summaryLogTime1 = timeRecorder.GetSummaryLogsTimeHour();
                
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Update_Timer", 1).ToString(), summaryLogTime1);
                extentReports.CreateLog("Hours: " + summaryLogTime1 + " is logged in Sumamry logs ");

                //Go to Details log
                timeRecorder.GoToDetailLogs();
                extentReports.CreateLog("User has navigated to details log ");

                //Verify detail logged hours
                string DetailLogTime3 = timeRecorder.GetDetailLogsTimeHour();
                Double DetailLogTime3db = Convert.ToDouble(DetailLogTime3);

                Assert.AreEqual(Convert.ToDouble(ReadExcelData.ReadData(excelPath, "Update_Timer", 1)), DetailLogTime3db);
                extentReports.CreateLog("TIme displaying in detail log: " + DetailLogTime3db + " Hours ");

                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();

                string selectProject2= ReadExcelData.ReadData(excelPath, "Project_Title", 1);
                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject2, fileTMT1411);
                string weekDay = timeEntry.LogFutureDateHours(fileTMT1411);

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

                //Verify the TAS Supervisor can only enter hours less than 24 hours for one day for Associates

                //Select Staff Member from the list
                string name = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);

                // string pTagValue = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 3);
                timeEntry.SelectStaffMember(name);
                string selectedStaffMember = timeEntry.GetSelectedStaffMember();
                string selectedStaffMemberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);
                Assert.AreEqual(selectedStaffMemberExl, selectedStaffMember);
                extentReports.CreateLog("Staff Member Title: " + selectedStaffMember + " is displayed upon click of staff Member link ");

                string selectProject = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject, fileTMT1411);
                
                //Add the Weekly Entry Matrix more than 24 hrs
                string weekDay1 = timeEntry.LogFutureDateHours(fileTMT1411);

                //Get border color of weekly entry
                timeEntry.GetBorderColorTimeEntry(weekDay1);
                
                extentReports.CreateLog("User has color: ");

                //Verify Time Entires provided on Weekly time sheet by TAS Supervisor user is reflecting on Summary and Detail Log tabs for selected associate

                timeEntry.LogCurrentDateHours(fileTMT1411);
                
                driver.Navigate().Refresh();

                Thread.Sleep(3000);
                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();

                //Select Staff Member from the list

                string name1 = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);
                // string pTagValue = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 3);
                timeEntry.SelectStaffMember(name);
                string selectedStaffMember1 = timeEntry.GetSelectedStaffMember();
                string selectedStaffMemberExl1 = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);
                Assert.AreEqual(selectedStaffMemberExl, selectedStaffMember);
                extentReports.CreateLog("Staff Member Title: " + selectedStaffMember + " is displayed upon click of staff Member link ");

                //Go to Summary Logs
                timeEntry.GoToSummaryLog();
                extentReports.CreateLog("User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                string summaryLogTime = timeRecorder.GetSummaryLogsTimeHour();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Update_Timer", 1).ToString(), summaryLogTime);
                extentReports.CreateLog("Hours: " + summaryLogTime + " is logged in Sumamry logs ");

                //Go to Details log
                timeRecorder.GoToDetailLogs();
                extentReports.CreateLog("User has naigated to details log ");
                //Verify detail logged hours
                string DetailLogTIme3 = timeRecorder.GetDetailLogsTimeHour();
                Double DetailLogTIme3db = Convert.ToDouble(DetailLogTIme3);

                Assert.AreEqual(Convert.ToDouble(ReadExcelData.ReadData(excelPath, "Update_Timer", 1)), DetailLogTIme3db);
                extentReports.CreateLog("TIme displaying in detail log: " + DetailLogTIme3db + " Hours ");

                //Go to Weekly Entry Matrix
                timeEntry.GoToWeeklyEntryMatrix();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");

                //Verify the Amount is calculated for enter hours for selected user after adding the time sheet
                //Enter Rate Sheet details
                string engagement = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 1);
                string rateSheet = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 2);
                rateSheetMgt.EnterRateSheet(engagement, rateSheet);
                
                //Verify selected rate sheet
                string selectedRateSheet = rateSheetMgt.GetSelectedRateSheet();
                string selectedRateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 2);
                Assert.AreEqual(selectedRateSheetExl, selectedRateSheet);
                extentReports.CreateLog("Selected Rate Sheet: " + selectedRateSheet + " is displayed upon entering rate sheet details ");

                //Go to WeeklyEntryMatrix
                timeEntry.GoToWeeklyEntryMatrix();

                //Go to Summary Logs
                timeEntry.GoToSummaryLog();
                extentReports.CreateLog("User has navigated to Summary logs ");
                
                //Get Entered Hours displayed
                double enteredHours = timeEntry.GetEnteredHoursInSummaryLogValue();

                //Get total amount calculated with default rate and entered hours
                double totalAmountCalculated = DefaultRateForStaff * enteredHours;

                // Get total amount displayed
                double totalAmountDisplayed = timeEntry.GetTotalAmount();

                //Verify calculated and displayed amount should matches
                Assert.AreEqual(totalAmountCalculated, totalAmountDisplayed);
                extentReports.CreateLog("Total Amount: " + totalAmountDisplayed + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                //Go to Details log
                timeRecorder.GoToDetailLogs();
                extentReports.CreateLog("User has naigated to details log ");

                // Delete time entry records from detail log 
                timeEntry.RemoveRecordFromDetailLogs();
                extentReports.CreateLog("Deleted record entry successfully from detail log ");

                //Delete rate sheet
                rateSheetMgt.DeleteRateSheet(ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 1));
                extentReports.CreateLog("Deleted rate sheet entry successfully after verification ");

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
