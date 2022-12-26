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
    class TMTT0011418_TMTT0011410_VerifyTheTASSupervisorCanOnlyEnterHoursMoreThan24hoursForOneDayForAssociatesWithTitleTAGOutsourcedContractor : BaseClass
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
        
        public static string fileTMT1418= "TMTT0011418_Verify_the_TAS_Supervisor can_only_enter_hours_more_than_24hours_for_one_day_for_Associates_with_Title_TAG_Outsourced_Contractor";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT1418;
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

                string defaultRate = rateSheetMgt.GetDefaultRateForOutsourcedContractorAsPerRateSheet("TAS");
                double DefaultRateForStaff = Convert.ToDouble(defaultRate);

                //Verify the TAS Supervisor can only enter hours more than 24 hours for one day for Associates with Title "TAG Outsourced Contractor"
                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();

                //Select Staff Member from the list
                string name = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);

                timeEntry.SelectStaffMember(name);
                string selectedStaffMember = timeEntry.GetSelectedStaffMember();
                string selectedStaffMemberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);
                Assert.AreEqual(selectedStaffMemberExl, selectedStaffMember);
                extentReports.CreateLog("Staff Member Title: " + selectedStaffMember + " is displayed upon click of staff Member link ");

                string selectProject = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject, fileTMT1418);

                timeEntry.LogCurrentDateHours(fileTMT1418);

                driver.Navigate().Refresh();

                Thread.Sleep(3000);
                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();

                //Select Staff Member from the list

                // string pTagValue = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 3);
                timeEntry.SelectStaffMember(name);
                string selectedStaffMember1 = timeEntry.GetSelectedStaffMember();
                Assert.AreEqual(selectedStaffMemberExl, selectedStaffMember1);
                extentReports.CreateLog("Staff Member Title: " + selectedStaffMember1 + " is displayed upon click of staff Member link ");

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

                //Enter Rate Sheet details
                string engagement = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 1);
                string rateSheet = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 2);
                rateSheetMgt.EnterRateSheet(engagement, rateSheet);
                
                //Verify selected rate sheet
                string selectedRateSheet = rateSheetMgt.GetSelectedRateSheet();
                string selectedRateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 2);
                Assert.AreEqual(selectedRateSheetExl, selectedRateSheet);
                extentReports.CreateLog("Selected Rate Sheet: " + selectedRateSheet + " is displayed upon entering rate sheet details ");
                                 
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

                // Get total amount displayed
                double totalAmountDisplayedInDetailLog = timeEntry.GetTotalAmount();

                //Verify calculated and displayed amount should matches
                Assert.AreEqual(totalAmountCalculated, totalAmountDisplayed);
                extentReports.CreateLog("Total Amount: " + totalAmountDisplayedInDetailLog + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

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
