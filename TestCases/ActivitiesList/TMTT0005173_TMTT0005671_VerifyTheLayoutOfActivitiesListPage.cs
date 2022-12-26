using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.ActivitiesList;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.ActivitiesList
{
    class TMTT0005173_TMTT0005671_VerifyTheLayoutOfActivitiesListPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        ActivitiesListDetailPage activityList = new ActivitiesListDetailPage();
        ActivityDetailPage activityDetail = new ActivityDetailPage();

        public static string fileTMTT0005173 = "TMTT0005173_ActivitiesList_VerifyLayoutOfActivitiesList.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyLayoutOfActivitiesList()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0005173;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Handling salesforce Lightning
                //login.HandleSalesforceLightningPage();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
               
                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTMTT0005173, user);

                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string standardUser = login.ValidateUser();
                string standardUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(standardUserExl.Contains(standardUser), true);
                extentReports.CreateLog("Standard User: " + standardUser + " is able to login ");

                //Click on activities list tab
                activityList.ClickActivitiesListTab();
                extentReports.CreateLog("Activities List Tab is clicked ");

                string preSetTemplate = ReadExcelData.ReadData(excelPath, "ActivityList", 5);
                activityList.SelectFirstPresetTemplate(fileTMTT0005173, preSetTemplate);

                //Click refresh button
                activityList.ClickRefreshButton();
                extentReports.CreateLog("Click refresh button ");

                //Verify alphabetical sorting of table details
                activityList.VerifyAlphabeticalSorting();
                extentReports.CreateLog("Verify alphabetical sorting of table details");
                string valEventTaskType = activityList.getEventTaskType();
                string valEventTaskTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 2, 1);
                extentReports.CreateLog("valEventTaskType:"+ valEventTaskType+ "valEventTaskTypeExl");
                Assert.AreEqual(valEventTaskTypeExl, valEventTaskType);
                extentReports.CreateLog("Event Task Type: " + valEventTaskType + " related activities are sorted ");
                
                //Verify alphabetical sorting of table details
                activityList.ClickAlphabetMforSorting();
                string valEventOrTaskType = activityList.getEventTaskType();
                string valEventOrTaskTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 3, 1);
                Assert.AreEqual(valEventOrTaskTypeExl, valEventOrTaskType);
                extentReports.CreateLog("Event Task Type: " + valEventOrTaskType + " related activities are sorted ");

                string activityTypeFromList = activityList.getEventTaskType();
                string activitySubjectFromList = activityList.getActivitySubject();
                string activityPrimaryAttendee = activityList.getPrimaryAttendee();
                string primaryExternalContact = activityList.getPrimaryExternalContact();

                //Verify view link
                activityList.ClickViewLink();

                //Verify activity type from detail
                string activityTypeFromDetail = activityDetail.GetActivityTypeValue();
                Assert.AreEqual(activityTypeFromList, activityTypeFromDetail);
                extentReports.CreateLog("Event Task Type: " + activityTypeFromDetail + " in activity detail page matching with Event Task type in activity list ");

                //Verify activity subject from detail
                string activitySubjectFromDetail = activityDetail.GetActivitySubjectValue();
                Assert.AreEqual(activitySubjectFromList, activitySubjectFromDetail);
                extentReports.CreateLog("Activity subject: " + activitySubjectFromDetail + " in activity detail page matching with activity subject in activity list ");

                //Verify activity HL Attendees
                string activityHLAttendee = activityDetail.GetActivityHLAttendees();
                Assert.AreEqual(activityPrimaryAttendee, activityHLAttendee);
                extentReports.CreateLog("Activity HL Attendee: " + activityHLAttendee + " in activity detail page matching with activity primary attendee in activity list ");

                //Verify activity external attendees
                string activityExternalContact = activityDetail.GetActivityExternalAttendees();
                Assert.AreEqual(primaryExternalContact, activityExternalContact);
                extentReports.CreateLog("Activity External Contact: " + activityExternalContact + " in detail subject page matching with activity primary external contact in activity list ");

                //CustomFunctions.SwitchToWindow(driver, 0)
                activityList.ClickActivitiesListTab();
                bool newTaskBtn = activityList.NewTaskButtonAvailability();
                Assert.AreEqual(false, newTaskBtn);
                extentReports.CreateLog("New Task Button availability on page is: " + newTaskBtn + " ");

                bool EditLinkAction = activityList.EditActionLinkAvailability();
                Assert.AreEqual(false, EditLinkAction);
                extentReports.CreateLog("Edit link under action column availability on page is: " + EditLinkAction + " ");

                bool DeleteLinkAction = activityList.DeleteActionLinkAvailability();
                Assert.AreEqual(false, DeleteLinkAction);
                extentReports.CreateLog("Delete link under action column availability on page is: " + DeleteLinkAction + " ");

                //Click on print link
                activityList.ClickPrintLink();

                CustomFunctions.SwitchToWindow(driver, 1);
                string currentURL = driver.Url;
                string currentURLExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 2, 2);
                Assert.IsTrue(currentURL.Contains(currentURLExl));
                extentReports.CreateLog("Upon click of print link user redirect to: "+ currentURL+" ");

                CustomFunctions.SwitchToWindow(driver, 0);

                //Logout from standard user
                usersLogin.UserLogOut();
                
                //Click on activities list tab
                activityList.ClickActivitiesListTab();
                extentReports.CreateLog("Activities List Tab is clicked ");
                driver.Navigate().Refresh();
                Thread.Sleep(3000);

                //Create New View
                activityList.CreateNewView();

                string getSelectedView = activityList.GetSelectedView();
                string getSelectedViewExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 2, 3);
                Assert.AreEqual(getSelectedViewExl, getSelectedView);
                extentReports.CreateLog("Selected View: "+ getSelectedView +" is new created view ");

                //Edit New View
                activityList.EditNewView();

                string getSelectedUpdatedView = activityList.GetSelectedView();
                string getSelectedUpdatedViewExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 2, 4);
                Assert.AreEqual(getSelectedUpdatedViewExl, getSelectedUpdatedView);
                extentReports.CreateLog("Selected View: " + getSelectedUpdatedView + " is new created view which is updated ");

                //Delete New View
                activityList.DeleteNewView();
                extentReports.CreateLog(" new created view is deleted ");

                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}