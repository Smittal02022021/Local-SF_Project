using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.TimeRecordManager;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.TimeRecordManager
{
    class T2286_T2287_TMTC0001544_TMTC0001548_VerifyRateGenerationToStaffWithDifferentTitleInSummaryAndDetailsLogTab : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();

        public static string fileTC2286_TC2287 = "T2286_T2287_TMTC0001544_TMTC0001548_VerifyRateGenerationToStaffWithDifferentTitleInSummaryAndDetailsLogTab";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyRateGenerationToStaffMemberAreDeterminedThroughTheirTitles()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2286_TC2287;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Handling salesforce Lightning
                //login.HandleSalesforceLightningPage();
                string TimeRecordManagerUser = login.ValidateUser();
                //Validate user logged in          
                //Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                  
                //Login as supervisor
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC2286_TC2287, user);
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                usersLogin.LoginAsSelectedUser();
                TimeRecordManagerUser = login.ValidateUser();
                //string TimeRecordManagerUser = login.ValidateUser();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Users", 1).Contains(TimeRecordManagerUser), true);
                extentReports.CreateLog("Time Record Manager User: " + TimeRecordManagerUser + " is able to login ");

                int rowRateSheet = ReadExcelData.GetRowCount(excelPath, "RateSheetManagement");
                for (int row = 2; row <= rowRateSheet; row++)
                {
                    //Click Time Record Manager Tab
                    homePage.ClickTimeRecordManagerTab();
                    string timeRecordManagerTitle = timeEntry.GetTimeRecordManagerTitle();
                    string timeRecordManagerTitleExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 5);
                    Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagerTitle);
                    extentReports.CreateLog("Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");
                    
                    //Select Staff Member from the list
                    string name = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 1);
                    timeEntry.SelectStaffMember(name);
                    string selectedStaffMember = timeEntry.GetSelectedStaffMember();
                    string selectedStaffMemberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 1);
                    Assert.AreEqual(selectedStaffMemberExl, selectedStaffMember);
                    extentReports.CreateLog("Staff Member Title: " + selectedStaffMember + " is displayed upon click of staff Member link ");

                    //Enter Rate Sheet details
                    string engagement = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1);
                    string rateSheet = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                    rateSheetMgt.EnterRateSheet(engagement, rateSheet);

                    //Verify selected rate sheet
                    string selectedRateSheet = rateSheetMgt.GetSelectedRateSheet();
                    string selectedRateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                    Assert.AreEqual(selectedRateSheetExl, selectedRateSheet);
                    extentReports.CreateLog("Selected Rate Sheet: " + selectedRateSheet + " is displayed upon entering rate sheet details ");

                    //Enter summary log details
                    timeEntry.EnterSummaryLogs1(ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1), fileTC2286_TC2287);
                    timeEntry.ClickAddButton();

                    //Verify project or engagement
                    string valueProjectOrEngagment = timeEntry.GetProjectOrEngagementValue();
                    string valueProjectOrEngagmentExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    if (valueProjectOrEngagmentExl.Contains(valueProjectOrEngagment))
                    {
                        extentReports.CreateLog("Project Or Engagement: " + valueProjectOrEngagment + " is displayed upon entering time entry in summary log ");
                    }

                    //Verify entered hours 
                    string valueEnteredHours = timeEntry.GetEnteredHoursInSummaryLog();
                    string valueEnteredHoursExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                    Assert.AreEqual(valueEnteredHoursExl, valueEnteredHours);
                    extentReports.CreateLog("Entered Hours: " + valueEnteredHours + " is displayed upon entering time entry in summary log ");

                    //Verify default dollar based on the title of the staff member
                    string valueDefaultDollarBasedOnTitle = timeEntry.GetDefaultRateForStaff();
                    string valueDefaultDollarBasedOnTitleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 4);
                    Assert.AreEqual(valueDefaultDollarBasedOnTitleExl, valueDefaultDollarBasedOnTitle);
                    extentReports.CreateLog("Default Dollar Based On title amount: " + valueDefaultDollarBasedOnTitle + " is displayed upon entering time entry in summary log ");

                    //Get default rate displayed
                    double DefaultRateForStaff = timeEntry.GetDefaultRateForStaffValue();

                    //Get Entered Hours displayed
                    double enteredHours = timeEntry.GetEnteredHoursInSummaryLogValue();

                    // Get total amount calculated with default rate and entered hours
                    double totalAmountCalculated = DefaultRateForStaff * enteredHours;

                    // Get total amount displayed
                    double totalAmountDisplayed = timeEntry.GetTotalAmount();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountCalculated, totalAmountDisplayed);
                    extentReports.CreateLog("Total Amount: " + totalAmountDisplayed + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                    //Delete Time Entry
                    timeEntry.DeleteTimeEntry();
                    extentReports.CreateLog("Deleted time entry successfully after verification ");
                    
                    // Enter detail logs
                    timeEntry.EnterDetailLogs1(ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 1), fileTC2286_TC2287);
                    timeEntry.ClickAddButton();

                    //Verify project or engagement
                    string valueProjectOrEngagmentInDetailLog = timeEntry.GetProjectOrEngagementValue();
                    string valueProjectOrEngagmentForDetailLogExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 1);

                    if (valueProjectOrEngagmentForDetailLogExl.Contains(valueProjectOrEngagmentInDetailLog))
                    {
                        extentReports.CreateLog("Project Or Engagement: " + valueProjectOrEngagment + " is displayed upon entering time entry in summary log ");
                    }
                   
                    //Verify entered hours 
                    string valueEnteredHoursInDetailLogs = timeEntry.GetEnteredHoursInDetailLogs();
                    string valueEnteredHoursInExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 2);
                    Assert.AreEqual(valueEnteredHoursInExl, valueEnteredHoursInDetailLogs);
                    extentReports.CreateLog("Entered Hours: " + valueEnteredHoursInDetailLogs + " is displayed upon entering time entry in detail log ");

                    //Verify default dollar based on the title of the staff member
                    string valueDefaultDollarDetailLogs = timeEntry.GetDefaultRateForStaffInDetailLogs();
                    string valueDefaultDollarDetailLogsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 4);
                    Assert.AreEqual(valueDefaultDollarDetailLogsExl, valueDefaultDollarDetailLogs);
                    extentReports.CreateLog("Default Dollar Based On title amount: " + valueDefaultDollarDetailLogs + " is displayed upon entering time entry in detail log ");

                    //Get default rate displayed
                    double DefaultRateForStaffDetailLogs = timeEntry.GetDefaultRateForStaffValueInDetailLogs();

                    //Get Entered Hours displayed
                    double enteredHoursDetailLogs = timeEntry.GetEnteredHoursInDetailLogValue();

                    // Get total amount calculated with default rate and entered hours
                    double totalAmount = DefaultRateForStaffDetailLogs * enteredHoursDetailLogs;

                    // Get total amount displayed
                    double totalAmountDisplayedInDetailLog = timeEntry.GetTotalAmount();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountCalculated, totalAmountDisplayed);
                    extentReports.CreateLog("Total Amount: " + totalAmountDisplayed + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                    // Delete time entry records from detail log 
                    timeEntry.RemoveRecordFromDetailLogs();
                    extentReports.CreateLog("Deleted record entry successfully from detail log ");

                    //Delete rate sheet
                    rateSheetMgt.DeleteRateSheet(ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1));
                    extentReports.CreateLog("Deleted rate sheet entry successfully after verification ");
                }
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