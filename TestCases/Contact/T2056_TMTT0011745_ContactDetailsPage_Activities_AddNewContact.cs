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
    class       
        T2056_Contact_ContactDetailsPage_Activities_AddNewContact : BaseClass
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

        public static string fileTC2056 = "T2056_ContactDetailsPage_Activities_AddNewContact";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]

        public void ContactDetailsPage_Activities_AddNewContact()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2056;
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
                homePage.SearchUserByGlobalSearch(fileTC2056, user);

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

                //Function to add an activity
                string HLAttendee = addActivity.AddAnActivityWithNewContact(fileTC2056);
                extentReports.CreateLog("An activity is created with new contact ");

                string externalContactName = activityDetail.GetExternalContactName();
                string externalFirstName = ReadExcelData.ReadData(excelPath, "Contact", 2);

                string externalLastName = ReadExcelData.ReadData(excelPath, "Contact", 3);
                Assert.AreEqual(externalFirstName + " " + externalLastName, externalContactName);
                extentReports.CreateLog("New contact name details entered is matching with the details in Activity Details page ");

                string externalAttendeeCompanyName = activityDetail.GetExternalAttendeeCompanyName();
                string externalCompanyName = ReadExcelData.ReadData(excelPath, "Contact", 1);
                Assert.AreEqual(externalCompanyName, externalAttendeeCompanyName);
                extentReports.CreateLog("New contact company details entered is matching with the details in Activity Details page ");

                string externalAttendeeEmail = activityDetail.GetExternalAttendeeEmail();
                string externalAttendeeEmailExl = ReadExcelData.ReadData(excelPath, "Contact", 5);
                Assert.AreEqual(externalAttendeeEmailExl, externalAttendeeEmailExl);
                extentReports.CreateLog("New contact email details entered is matching with the details in Activity Details page ");

                string externalAttendeePhone = activityDetail.GetExternalAttendeePhone();
                string externalAttendeePhoneExl = ReadExcelData.ReadData(excelPath, "Contact", 6);
                Assert.AreEqual(externalAttendeePhoneExl, externalAttendeePhone);
                extentReports.CreateLog("New contact phone details entered is matching with the details in Activity Details page ");
                usersLogin.UserLogOut();

                conHome.SearchContact(fileTC2056);
                contactDetails.ViewExistingActivity();


                /* activityDetail.DeleteActivity();
                 extentReports.CreateLog("Delete activity ");*/

                //Validate Activity Details heading is visible
                string activityDetailHeading = activityDetail.GetActivityDetailsHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 4), activityDetailHeading);
                extentReports.CreateLog("Page with heading: " + activityDetailHeading + " is displayed upon Adding an Activity ");

                //Validate Activity Type selected is visible on activity detail page
                string activityType = activityDetail.GetActivityTypeValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 1), activityType);
                extentReports.CreateLog("Activity Type: " + activityType + " entered in add activity page matches on activity detail page ");


                //Validate Activity Subject selected is visible on activity detail page
                string activitySubject = activityDetail.GetActivitySubjectValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 2), activitySubject);
                extentReports.CreateLog("Activity Subject: " + activitySubject + " entered in add activity page matches on activity detail page ");

                string activityCompanyDiscussed = activityDetail.GetCompanyDiscussedName();

                //Validate Activity company discussed selected is visible on activity detail page
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 6), activityCompanyDiscussed);
                extentReports.CreateLog("Activity Company Discussed: " + activityCompanyDiscussed + " entered in add activity page matches on activity detail page ");




                conHome.SearchContact(fileTC2056);


                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC2056, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                extentReports.CreateLog("Deletion of new Contact ");
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