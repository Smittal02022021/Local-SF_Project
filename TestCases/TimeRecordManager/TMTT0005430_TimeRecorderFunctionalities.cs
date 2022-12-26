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
    class TMTT0005430_TimeRecorderFunctionalities : BaseClass
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
        
        public static string fileTMT5430= "TMTT0005430_TimeRecorderFunctionalities";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }


        [Test]
        public void VerifyTimeRecorderFunctionalities()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT5430;
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

                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();

                //Click on Time Clock Recorder Tab
                refreshButton.GotoTimeClockRecorderPage();

                //Validate Time Record Period Page Title
                string TimeClockRecorderPageTitle = refreshButton.GetTitleTimeClockRecorderPage();
                string TimeClockRecorderPageTitleExl = ReadExcelData.ReadData(excelPath, "Time_Record_Period_Title", 1);
                Assert.AreEqual(TimeClockRecorderPageTitleExl, TimeClockRecorderPageTitle);
                extentReports.CreateLog(TimeClockRecorderPageTitle + " is displayed upon clicking Time Clock Recorder ");
                //Check recorder is reset or not
                refreshButton.ClickResetButton();

                // TMTI0010266_Already covered in TMTT0005221

                //Select Project and Activity from Drop Down
                refreshButton.SelectDropDownProjectandActivity(excelPath);
                extentReports.CreateLog("Selected Project and Activity from Drop down");
                //TMTI0010006

                //Click Start Button
                refreshButton.ClickStartButton();

                //Update Timer
                refreshButton.UpdateTimer(excelPath);
                extentReports.CreateLog("Update the timer ");
                //Thread.Sleep(60000);

                //Click Finish Button
                refreshButton.ClickFinishButton();
                extentReports.CreateLog("Clicked the finish button ");

                //Go to Weekly Entry Matrix
                timeEntry.GoToWeeklyEntryMatrix();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");
                Thread.Sleep(3000);

                string selectProject1 = ReadExcelData.ReadData(excelPath, "Project_Title", 1);

                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject1, fileTMT5430);
                timeEntry.LogCurrentDateHours(fileTMT5430);

                driver.Navigate().Refresh();
                //Edit the Weekly Entry Matrix
                string weekDay = timeRecorder.EditWeeklyEntryMatrix();
                extentReports.CreateLog("User has edited the Weekly Entry Matrix: "+weekDay+" ");

                //Go to Details log
                timeRecorder.GoToDetailLogs();
                extentReports.CreateLog("User has navigated to details log ");

                string DetailLogTIme = timeRecorder.GetDetailLogsTimeHour();
                Double DetailLogTIme2db = Convert.ToDouble(DetailLogTIme);
                Assert.AreEqual(1, DetailLogTIme2db);
                extentReports.CreateLog("TIme displaying in detail log: "+ DetailLogTIme2db+" Hours ");
                
                //Edit the details log
                timeRecorder.EditDetailsLog();
                extentReports.CreateLog("User has edited details log ");

                //Get hopurs from detail log
                timeRecorder.GoToDetailLogs();
                string DetailLogTIme1 = timeRecorder.GetDetailLogsTimeHour();
                Double DetailLogTIme1db = Convert.ToDouble(DetailLogTIme1);
                Assert.AreEqual(2, DetailLogTIme1db);

                usersLogin.UserLogOut();
                //Login as Standard User and validate the user
                string valUser2 = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser2);
                string stdUser2 = login.ValidateUser();
                Assert.AreEqual(stdUser2.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser2 + " is able to login ");

                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();
              
                //Edit the Weekly Entry Matrix
                string weekDay2 = timeRecorder.EditWeeklyEntryMatrix();
                extentReports.CreateLog("User has edited the Weekly Entry Matrix: " + weekDay2 + " ");

                //Go to Details log
                timeRecorder.GoToDetailLogs();
                extentReports.CreateLog("User has naigated to details log ");
                //Verify detail logged hours
                string DetailLogTIme3 = timeRecorder.GetDetailLogsTimeHour();
                Double DetailLogTIme3db = Convert.ToDouble(DetailLogTIme3);

                Assert.AreEqual(1, DetailLogTIme3db);
                extentReports.CreateLog("TIme displaying in detail log: " + DetailLogTIme3db + " Hours ");

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

                //TMTI0010009
                // Click on Time Clock Recorder Tab
                refreshButton.GotoTimeClockRecorderPage();
                extentReports.CreateLog("User has navigated to Time Clock Recorder Tab");

                //Check recorder is reset or not
                refreshButton.ClickResetButton();

                 //Select Project and Activity from Drop Down
                refreshButton.SelectDropDownProjectandActivity(excelPath);
                extentReports.CreateLog("Selected Project and Activity from Drop down ");

                //Click Start Button
                refreshButton.ClickStartButton();
                extentReports.CreateLog("User has clicked on Start Button ");

                //Click Finish Button
                refreshButton.ClickFinishButton();
                extentReports.CreateLog("Clicked the finish button ");

                //Go to Summary Logs
                timeEntry.GoToSummaryLogs();
                extentReports.CreateLog("User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                string summaryLogTime= timeRecorder.GetSummaryLogsTimeHour();
                //Assert.AreEqual("0.1", summaryLogTime);
                extentReports.CreateLog("Hours are round up to "+summaryLogTime+" even when only a few seconds has been recorded ");

                //Go to Weekly Entry Matrix
                timeEntry.GoToWeeklyEntryMatrix();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

                //TMTI0010319
                // Click on Time Clock Recorder Tab
                refreshButton.GotoTimeClockRecorderPage();
                extentReports.CreateLog("User has navigated to Time Clock Recorder Tab ");

                //Check recorder is reset or not
                refreshButton.ClickResetButton();

                //Select Project and Activity from Drop Down
                refreshButton.SelectDropDownProjectandActivity(excelPath);
                extentReports.CreateLog("Selected Project and Activity from Drop down ");

                //Click Start Button
                refreshButton.ClickStartButton();
                extentReports.CreateLog("User has clicked on Start Button ");

                Thread.Sleep(300000);
                //Click Finish Button
                refreshButton.ClickFinishButton();
                extentReports.CreateLog("Clicked the finish button ");

                //Go to Summary Logs
                timeEntry.GoToSummaryLogs();
                extentReports.CreateLog("User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                string summaryLogTime1 = timeRecorder.GetSummaryLogsTimeHour();
                extentReports.CreateLog("Hours are round up to " + summaryLogTime1 + " even when only a few seconds has been recorded ");

                //Go to Weekly Entry Matrix
                timeEntry.GoToWeeklyEntryMatrix();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

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
