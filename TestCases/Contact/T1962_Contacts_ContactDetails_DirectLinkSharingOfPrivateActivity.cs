using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using SalesForce_Project.Pages.ActivitiesList;
using System;

namespace SalesForce_Project.TestCases.Contact
{
    class T1962_Contacts_ContactDetails_DirectLinkSharingOfPrivateActivity : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        AddActivity addActivity = new AddActivity();
        ActivityDetailPage activityDetail = new ActivityDetailPage();
        ActivitiesListDetailPage activityList = new ActivitiesListDetailPage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1962 = "T1962_Contacts_ContactDetails_DirectLinkSharingOfPrivateActivity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_ContactDetails_DirectLinkSharingOfPrivateActivity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1962;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC1962, user);

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

                //Get activity list row count before adding a private activity
                int initialRowCount = activityList.GetActivityListCount();
                extentReports.CreateLog("Initial Activities List count is fetched as: " + initialRowCount + " ");

                // Search for external contact
                conHome.SearchContact(fileTC1962);
                extentReports.CreateLog("Search for an external contact ");

                //Function to add an activity
                addActivity.AddPrivateActivity(fileTC1962);
                extentReports.CreateLog("Private activity added to external contact ");

                //Validate Activity Details heading is visible
                string activityDetailHeading = activityDetail.GetActivityDetailsHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 4), activityDetailHeading);
                extentReports.CreateLog(activityDetail.GetActivityDetailsHeading() + " heading is visible ");
                extentReports.CreateLog("Page with heading: " + activityDetailHeading + " is displayed upon click of Add Activity button ");

                //Validate Activity Type selected is visible on activity detail page
                string activityType = activityDetail.GetActivityTypeValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 1), activityType);
                extentReports.CreateLog("Activity Type: " + activityType + " is displayed on activity detail page ");

                activityDetail.ClickReturn();

                //Click on activities list tab
                activityList.ClickActivitiesListTab();
                extentReports.CreateLog("Activities List Tab is clicked ");

                //Get activity list row count after adding a private activity
                int rowCountAfterActivityAddition = activityList.GetActivityListCount();
                extentReports.CreateLog("Activities List count after addition of private activity is : " + rowCountAfterActivityAddition + " ");

                Assert.AreEqual(rowCountAfterActivityAddition, initialRowCount + 1);
                extentReports.CreateLog("Initial activity list row count increased as expected upon addition of a private activity. ");

                //Verify And Select If DesiredActivityIsVisible
                Assert.IsTrue(activityList.VerifyAndSelectIfDesiredActivityIsVisible(fileTC1962));
                extentReports.CreateLog("Newly added private activity is visible under activity list. ");

                //Validate Activity Subject selected is visible on activity detail page
                string activitySubject = activityDetail.GetActivitySubjectValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 2), activitySubject);
                extentReports.CreateLog("Activity Subject: " + activitySubject + " is displayed on activity detail page upon viewing the newly created activtiy from activity list page. ");

                //Delete the created activity
                activityDetail.DeleteActivity();
                extentReports.CreateLog("Newly added private activity is deleted. ");

                //Click on activities list tab
                activityList.ClickActivitiesListTab();
                extentReports.CreateLog("Activities List Tab is clicked ");

                //Verify the activity list count again after deletion
                int rowCountAfterActivityDeletion = activityList.GetActivityListCount();
                extentReports.CreateLog("Activities List count after activity deletion is : " + rowCountAfterActivityDeletion + " ");

                //Verify the activity deletion
                Assert.AreEqual(rowCountAfterActivityDeletion, initialRowCount);
                extentReports.CreateLog("Initial activity list row count matches with row count after deletion of newely created activity. ");

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