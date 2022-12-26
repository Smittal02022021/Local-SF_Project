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
    class TMTT0005221_TMTT0011420_StartButtonFunctionalities : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
              RefreshButtonFunctionality refreshButton = new RefreshButtonFunctionality();
        TimeRecorderFunctionalities timeRecorder = new TimeRecorderFunctionalities();



        public static string fileTMT5221 = "TMTT0005221_StartButtonFunctionalities";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }


        [Test]
        //camel case,change verify startbutton functionalities and understandable
        public void VerifyStartButtonFunctionalities()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT5221;
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


                //Click Start Button
                refreshButton.ClickStartButton();

                //Verify error message when project is not Selected
              string errMsg=  refreshButton.GetErrorMessageStart();
                //Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Error_Message", 1), errMsg);

                Assert.IsTrue(errMsg.Contains(ReadExcelData.ReadData(excelPath, "Error_Message", 1)), "Error message is dispalying correctly");
                extentReports.CreateLog(" Error message: " + errMsg+" is displaying upon clicking Start Button ");


                //Select Project and Activity from Drop Down
                refreshButton.SelectDropDownProjectandActivity(excelPath);
                extentReports.CreateLog("Selected Project and Activity from Drop down");


                //Verify Start button is enabled when project and activity is selected
                
                Assert.AreEqual(RefreshButtonFunctionality.ButtonStartClickable(), true);
               extentReports.CreateLog("Start button is enabled after selecting Project and Activity Drop down ");

                
                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();
                
                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

               
                // Go to Weekly Entry Matrix
                homePage.ClickTimeRecordManagerTab();

                string selectProject1 = ReadExcelData.ReadData(excelPath, "Project_Title", 1);
                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject1, fileTMT5221);


                string weekDay1 = timeEntry.LogFutureDateHours(fileTMT5221);
                //Get border color of weekly entry
                timeEntry.GetBorderColorTimeEntry(weekDay1);

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

                //Go to Summary Logs
                timeEntry.GoToSummaryLog();
                extentReports.CreateLog("User has navigated to Summary logs ");
                timeEntry.EnterSummaryLogHours(fileTMT5221);

                Assert.IsFalse(timeEntry.VerifySuccessMsgDisplay());

                extentReports.CreateLog("TAG outsourced Contractor is not able to enter more than 24 hours in Summary Log ");
                // Go to Weekly Entry Matrix
                homePage.ClickTimeRecordManagerTab();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");


                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");
               
                //Go to Details log
                timeRecorder.GoToDetailLogs();
                extentReports.CreateLog("User has navigated to details log ");

                timeEntry.EnterSummaryLogHours(fileTMT5221);

                Assert.IsFalse(timeEntry.VerifySuccessMsgDisplay());

                extentReports.CreateLog("TAG outsourced Contractor is not able to enter more than 24 hours in Details Log ");



                // Go to Weekly Entry Matrix
                homePage.ClickTimeRecordManagerTab();
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
