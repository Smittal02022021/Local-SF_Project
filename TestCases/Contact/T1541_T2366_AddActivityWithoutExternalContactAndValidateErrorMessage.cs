using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Contact
{
    class T1541_T2366_AddActivityWithoutExternalContactAndValidateErrorMessage : BaseClass
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
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC2366 = "T2366_Contact_AddActivityWithoutExternalContact";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddActivityWithoutExternalContact()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2366;
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
                homePage.SearchUserByGlobalSearch(fileTC2366, user);

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

                // Verify mark not external contact error
                addActivity.ValidateMarkNoExternalContactError(fileTC2366);
                string erroMsg = addActivity.GetNoExternalContactErrorMsg();
                Assert.IsTrue(erroMsg.Contains(ReadExcelData.ReadData(excelPath, "Activity", 3)));
                extentReports.CreateLog("Error Message: " + erroMsg + " is validated successfully ");

                //Verify companies discussed required error message
                addActivity.ValidateCompaniesDiscussedRequiredErrorMsg();
                string errorCompaniesDiscussed = addActivity.GetCompaniesDiscussedRequiredErrorMsg();
                Assert.IsTrue(errorCompaniesDiscussed.Contains(ReadExcelData.ReadData(excelPath, "Activity", 5)));
                extentReports.CreateLog("Error Message: " + errorCompaniesDiscussed + " is validated successfully ");

                //Function to add an activity
                string HLAttendee = addActivity.AddAnActivity(fileTC2366);

                //Validate Activity Details heading is visible
                string activityDetailHeading = activityDetail.GetActivityDetailsHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 4), activityDetailHeading);
                extentReports.CreateLog(activityDetail.GetActivityDetailsHeading() + " heading is visible ");
                extentReports.CreateLog("Page with heading: " + activityDetailHeading + " is displayed upon click of Add Activity button ");

                //Validate Activity Type selected is visible on activity detail page
                string activityType = activityDetail.GetActivityTypeValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 1), activityType);
                extentReports.CreateLog("Activity Type: " + activityType + " is displayed on activity detail page ");

                //Validate Activity Subject selected is visible on activity detail page
                string activitySubject = activityDetail.GetActivitySubjectValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 2), activitySubject);
                extentReports.CreateLog("Activity Subject: " + activitySubject + " is displayed on activity detail page ");
                
                //Validate Activity company discussed selected is visible on activity detail page
                string activityCompanyDiscussed = activityDetail.GetCompanyDiscussedNameActivityWithoutExternal();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 6), activityCompanyDiscussed);
                extentReports.CreateLog("Activity Company Discussed: " + activityCompanyDiscussed + " is displayed on activity detail page ");

                //Search HL Attendee of the activity
                conHome.SearchHLAttendee(HLAttendee);
                extentReports.CreateLog("HL Attendee contact is searched and reached to its home page successfully ");

                // Calling Delete created activity function to delete recent created activity
                conHome.DeleteCreatedActivityFromContactHomePage();
                extentReports.CreateLog("Deletion of Created Activity successfully ");

                usersLogin.UserLogOut();
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