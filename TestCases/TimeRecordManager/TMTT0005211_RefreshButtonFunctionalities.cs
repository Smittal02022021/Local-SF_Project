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
    class TMTT0005211_RefreshButtonfunctionalities : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        RefreshButtonFunctionality refreshButton = new RefreshButtonFunctionality();

        public static string fileTMT5211 = "TMTT0005211_RefreshButtonFunctionalities";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyRefreshFfunctionality()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT5211;
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

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("User has deleted the record ");

                //Click on Time Clock Recorder Tab
                refreshButton.GotoTimeClockRecorderPage();

                //Validate Time Record Period Page Title
                string TimeClockRecorderPageTitle = refreshButton.GetTitleTimeClockRecorderPage();
                string TimeClockRecorderPageTitleExl = ReadExcelData.ReadData(excelPath, "Time_Record_Period_Title", 1);
                Assert.AreEqual(TimeClockRecorderPageTitleExl, TimeClockRecorderPageTitle);
                extentReports.CreateLog( TimeClockRecorderPageTitle + " is displayed upon clicking Time Clock Recorder ");
                //Check recorder is reset or not
                refreshButton.ClickResetButton();

                //Verify Refresh button is not displaying when project is not selected
                Assert.AreEqual(RefreshButtonFunctionality.HiddenRefreshButton(), true);
                extentReports.CreateLog("Refresh button is hidden when project is not selected ");

                 //Select Project and Activity from Drop Down
                 refreshButton.SelectDropDownProjectandActivity(excelPath);
                 extentReports.CreateLog("Selected Project and Activity from Drop down");
                
                //Click Start Button
                refreshButton.ClickStartButton();
                extentReports.CreateLog("Click Start button in Timer ");

                //Click Refresh Button
                refreshButton.ClickRefreshButton();
                extentReports.CreateLog("Click Refresh button in Timer ");

                //TMT10009559
                //Get Seconds Text
                int GetSecondsTimer = int.Parse(refreshButton.GetSecondsTimer1());
                Assert.AreNotEqual(0,GetSecondsTimer,"Seconds timer is not working fine ", true);
                extentReports.CreateLog("Timer is resumed upon clicking Refresh Button " );
                             
                //Click on Time Clock Recorder Tab
                refreshButton.GotoTimeClockRecorderPage();
                extentReports.CreateLog("User is navigated to Clock Recorder Page ");

                //Click Resume Button
                refreshButton.ClickResumeButton();
                extentReports.CreateLog("User had clicked on resume button ");
                Thread.Sleep(2000);

                //Click Pause Button
                refreshButton.ClickPauseButton();
                extentReports.CreateLog("User had clicked on Pause button ");

                //Click Refresh Button
                refreshButton.ClickRefreshButton();
                extentReports.CreateLog("Click Refresh button in Timer ");

                //TNT10009582
                //Get Seconds Text
                int GetSecondsTimer1 = int.Parse(refreshButton.GetSecondsTimer1());
                Assert.AreNotEqual(0, GetSecondsTimer1, "Seconds timer is not working fine " , true);
                extentReports.CreateLog("Timer is displaying accurate time upon clicking Refresh Button " );
                          
                //Update Timer
                refreshButton.UpdateTimer(excelPath);
                extentReports.CreateLog("Update the timer ");

                //Click Refresh Button
                refreshButton.ClickRefreshButton();
                extentReports.CreateLog("Click Refresh button in Timer ");
                               
                //TNT10009878
                //Get Hours Text
                int GetHoursTimer = int.Parse(refreshButton.GetHoursTimer1());
                Assert.AreEqual(1, GetHoursTimer, "Hours timer is not working fine ", true);
                extentReports.CreateLog("Added minutes are reflecting after refresh button is clicked ");
             
                //Click Pause Button
                refreshButton.ClickPauseButton();
                extentReports.CreateLog("User had clicked on Pause button ");

                //Click Refresh Button
                refreshButton.ClickRefreshButton();
                refreshButton.ClickRefreshButton();
                extentReports.CreateLog("Double Click Refresh button in Timer ");

                //TMT10009599
                int GetHoursTimer1 = int.Parse(refreshButton.GetHoursTimer1());
                Assert.AreEqual(1, GetHoursTimer1, "Hours timer is not working fine ", true);
                extentReports.CreateLog("Clicking on Refresh button is not setting back the hours or minutes of timer" );
                                             
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
