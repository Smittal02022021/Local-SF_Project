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
    class TMTT0011412_TMTT0011411_TMTT0011417_TMTT0011427_VerifyUserWithTitleTAGOutsourcedContractorIsAbleToEnterMoreThan24hoursForFutureDatesAsWell : BaseClass
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
        
        public static string fileTMT1411= "TMTT0011411_Verify_user_with_Title_TAG_Outsourced_Contractor";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }


        [Test]
        public void VerifyTAGOutsourcedContractorFunctionalities()
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

                //Verify user with Title :TAG Outsourced Contractor is able to enter more than 24 hours for future dates as well

                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();

                string selectProject = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject, fileTMT1411);

                //Edit the Weekly Entry Matrix
                string weekDay = timeEntry.LogFutureDateHours(fileTMT1411);

                driver.Navigate().Refresh();
                Thread.Sleep(30000);
                //string weekDay = timeRecorder.EditWeeklyEntryMatrix();
                extentReports.CreateLog("User has edited the Weekly Entry Matrix: "+weekDay+" ");

                //Go to Summary Logs
                timeEntry.GoToSummaryLog();
                extentReports.CreateLog("User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                string summaryLogTime = timeRecorder.GetSummaryLogsTimeHour();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Update_Hours", 1).ToString(), summaryLogTime);
                extentReports.CreateLog("Hours: "+ summaryLogTime+" is logged in Sumamry logs ");

                //Go to Details log
                timeRecorder.GoToDetailLogs();
                extentReports.CreateLog("User has navigated to details log ");
                //Verify detail logged hours
                string DetailLogTIme3 = timeRecorder.GetDetailLogsTimeHour();
                Double DetailLogTIme3db = Convert.ToDouble(DetailLogTIme3);

                Assert.AreEqual(Convert.ToDouble(ReadExcelData.ReadData(excelPath, "Update_Hours", 1)), DetailLogTIme3db);
                extentReports.CreateLog("TIme displaying in detail log: " + DetailLogTIme3db + " Hours ");

                // Go to Weekly Entry Matrix
                homePage.ClickTimeRecordManagerTab();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

                //Verify the maximum hours of time entries for Title: TAG Outsourced Contractor(limit technically is 1000 hours) Weekly Time Entries, Summary Log, Detail Log

                // Go to Weekly Entry Matrix
                homePage.ClickTimeRecordManagerTab();
                Thread.Sleep(3000);
                string selectProject1 = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject1, fileTMT1411);
                Thread.Sleep(3000);
                //Log 1000 hours
                timeEntry.LogCurrentDateHours(fileTMT1411);

                //Go to Summary Logs
                timeEntry.GoToSummaryLog();
                extentReports.CreateLog("User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                string summaryLogTime1 = timeRecorder.GetSummaryLogsTimeHour();
                string summaryhours = ReadExcelData.ReadData(excelPath, "Update_Timer", 1).ToString();

                Assert.AreEqual(  summaryLogTime1,ReadExcelData.ReadData(excelPath, "Update_Timer", 1).ToString());
                extentReports.CreateLog("Hours: " +  ReadExcelData.ReadData(excelPath, "Update_Timer", 1).ToString() + " is logged in Sumamry logs ");

                //Go to Details log
                timeRecorder.GoToDetailLogs();
                extentReports.CreateLog("User has naigated to details log ");
                //Verify detail logged hours
                string DetailLogTIme = timeRecorder.GetDetailLogsTimeHour();
                Double DetailLogTImedb = Convert.ToDouble(DetailLogTIme);

                Assert.AreEqual(Convert.ToDouble(ReadExcelData.ReadData(excelPath, "Update_Timer", 1)), DetailLogTImedb);
                extentReports.CreateLog("TIme displaying in detail log: " + DetailLogTImedb + " Hours ");

                // Go to Weekly Entry Matrix
                homePage.ClickTimeRecordManagerTab();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

                //Verify the User with Title :TAG Outsourced Contractor is able to enter own hours more than 24 hours ( Weekly Sheet, Summary Log and Detail Log tabs)

                //Go to Summary Logs
                timeEntry.GoToSummaryLog();
                extentReports.CreateLog("User has navigated to Summary logs ");
                timeEntry.EnterSummaryLogHours(fileTMT1411);
                
                //Go to Details log
                timeRecorder.GoToDetailLogs();
                extentReports.CreateLog("User has naigated to details log ");
                //Verify detail logged hours
                string DetailLogTIme1 = timeRecorder.GetDetailLogsTimeHour();
                Double DetailLogTImedb1 = Convert.ToDouble(DetailLogTIme1);

                Assert.AreEqual(Convert.ToDouble(ReadExcelData.ReadData(excelPath, "Update_Hours", 1)), DetailLogTImedb1);
                extentReports.CreateLog("TIme displaying in detail log: " + DetailLogTImedb1 + " Hours ");
                
                // Go to Weekly Entry Matrix
                homePage.ClickTimeRecordManagerTab();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

                //Go to Details log
                timeRecorder.GoToDetailLogs();
                extentReports.CreateLog("User has naigated to details log ");

                timeEntry.EnterSummaryLogHours(fileTMT1411);
                //Go to Summary Logs
                timeEntry.GoToSummaryLog();
                extentReports.CreateLog("User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                string summaryLogTime2 = timeRecorder.GetSummaryLogsTimeHour();
                string summaryhours1 = ReadExcelData.ReadData(excelPath, "Update_Hours", 1).ToString();

                Assert.AreEqual(summaryLogTime2, ReadExcelData.ReadData(excelPath, "Update_Hours", 1).ToString());
                extentReports.CreateLog("Hours: " + ReadExcelData.ReadData(excelPath, "Update_Hours", 1).ToString() + " is logged in Sumamry logs ");

                // Go to Weekly Entry Matrix
                homePage.ClickTimeRecordManagerTab();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");

                Thread.Sleep(2000);
                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

                // Verify the Only Forecast option should be available in  Activity List for future dates

                // Go to Weekly Entry Matrix

                //Verify the Only Forecast option should be available in  Activity List for future dates 
                homePage.ClickTimeRecordManagerTab();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");

                timeEntry.SelectFutureTimePeriod();

                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject, fileTMT1411);

                timeEntry.VerifyActivityDropDownForFuturePeriod(fileTMT1411);
                extentReports.CreateLog("Forecast option is available in  Activity List for future dates");
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
