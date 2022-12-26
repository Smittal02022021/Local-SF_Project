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
    class TMTT0011413_Verify_the_time_entries_should_be_rounded_off_to_1_decimal_place_automatically : BaseClass
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
        
        public static string fileTMT1413= "TMTT0011413_Verify_the_time_entries_should_be_rounded_off_to_1_decimal_place_automatically";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT1413;
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

                //Select Staff Member from the list

                string name= ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);
               
                timeEntry.SelectStaffMember(name);
                string selectedStaffMember= timeEntry.GetSelectedStaffMember();
                string selectedStaffMemberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);
                Assert.AreEqual(selectedStaffMemberExl, selectedStaffMember);
                extentReports.CreateLog("Staff Member Title: " + selectedStaffMember + " is displayed upon click of staff Member link ");

                string selectProject = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject, fileTMT1413);

                timeEntry.LogCurrentDateHours(fileTMT1413);
                Thread.Sleep(3000);
                timeEntry.CompareWeeklyTimeEntry(fileTMT1413);

                Thread.Sleep(3000);
                extentReports.CreateLog("Time Entries are rounded off to 1 decimal place automatically");
                
                timeEntry.SelectStaffMember(name);
                Thread.Sleep(4000);
                timeEntry.GoToWeeklyEntryMatrix();
                Thread.Sleep(4000);
                ///Delete Time Entry Matrix
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
