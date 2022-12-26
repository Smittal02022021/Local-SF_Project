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
    class T2132_T2133_Contact_ContactDetailPageAddValidateInternalMentorActivity : BaseClass
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

        public static string fileTC2132_TC2133 = "T2132_T2133_Contact_ContactDetailPageAddValidateInternalMentorActivity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddAndValidateInternalMentorActivityByContactHomePage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2132_TC2133;
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
                string user = ReadExcelData.ReadData(excelPath, "Users", 2);
                homePage.SearchUserByGlobalSearch(fileTC2132_TC2133, user);

                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string intUser = login.ValidateUser();
                string intUserExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                Assert.AreEqual(intUserExl.Contains(intUser), true);
                extentReports.CreateLog("Internal User: " + intUser + " is able to login ");

                conHome.ValidateMentorMenteeNotSame(fileTC2132_TC2133);

                // Validation of error message Mentor and Mentee cannot be same
                string erroMsg = conHome.GetMenteeMentorErrorMsg();
                Assert.IsTrue(erroMsg.Contains("Mentee and Mentor can not be same"));
                extentReports.CreateLog("Error Message: " + erroMsg + " is validated successfully ");

                conHome.ValidateAlwaysOneToOneMeeting(fileTC2132_TC2133);
                Assert.AreEqual(1, conHome.rowCountsOfMentee());
                extentReports.CreateLog("Validated there is always one to one meeting between a mentor and mentee ");

                // Calling add activity function to add activity from contact home page
                string HLAttendee = addActivity.AddAnActivity(fileTC2132_TC2133);

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
/*
                //Validate Internal mentee selected is visible on activity detail page
                string internalMentee = activityDetail.GetSelectedInternalMenteeContact();
                Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 3), internalMentee);
                extentReports.CreateLog("Internal Mentee: " + internalMentee + " is displayed on activity detail page ");

                //Validate default HL mentor is visible on activity detail page
                string defaultMentor = activityDetail.GetDefaultHLMentorContact();                
                Assert.IsTrue(HLAttendee.Contains(defaultMentor));
                extentReports.CreateLog("Default HL mentor: " + defaultMentor + " as default HL mentor is visible ");

               */
                // string HLAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 3, 1);
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
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }

    
}
}
