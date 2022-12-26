using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.ActivitiesList;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.ActivitiesList
{
    class TMTT0011744_TMTT0011743_TMTT0011742_VerifythatActivityDescriptionontheactivityisbeingmappedtoEmailTemplatewithsameformatasActivityDescription: BaseClass
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
        EditActivity editactivity = new EditActivity();
        AddActivity1 addActivity1 = new AddActivity1();
        public static string fileTC1744 = "TMTT0011744_VerifythatActivityDescriptionontheactivityisbeingmappedtoEmailTemplatewithsameformatasActivityDescription";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1744;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                for (int row = 1; row <= 2; row++)
                {
                    // Search standard user by global search
                    string user = ReadExcelData.ReadData(excelPath, "Users", row);
                    homePage.SearchUserByGlobalSearch(fileTC1744, user);

                    //Verify searched user
                    string userPeople = homePage.GetPeopleOrUserName();
                    string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", row);
                    Assert.AreEqual(userPeopleExl, userPeople);
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    //Login as standard user
                    usersLogin.LoginAsSelectedUser();
                    string standardUser = login.ValidateUser();
                    string standardUserExl = ReadExcelData.ReadData(excelPath, "Users", row);
                    Assert.AreEqual(standardUserExl.Contains(standardUser), true);
                    extentReports.CreateLog("Standard User: " + standardUser + " is able to login ");

                    //Function to add an activity
                    string HLAttendee = addActivity.AddAnActivityWithNewContact(fileTC1744);
                    extentReports.CreateLog("An activity is created with new contact ");
                    Thread.Sleep(2000);
                    editactivity.AddDescriptionToExistingActivity(fileTC1744);

                    //editactivity.AddDescriptionToExistingActivity(fileTC1744);
                    editactivity.ClickSendNotificationBtn();
                    string str = editactivity.VerifyDescription(fileTC1744);

                    Assert.AreEqual(str, "\r\ntest\r\ntest1 test2\r\n\r\nInternal Notes");
                    extentReports.CreateLog("Description: " + str + " is displaying ");
                    editactivity.VerifyEmailTemplateInformation(fileTC1744);

                    editactivity.ClickCancelBtn();

                    activityDetail.DeleteActivity();
                    extentReports.CreateLog("Delete activity ");
                    usersLogin.UserLogOut();
                    conHome.ClickContact();
                    conHome.SearchContact(fileTC1744);

                    // To Delete created contact
                    contactDetails.DeleteCreatedContact(fileTC1744, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, row)); 
                }

                // Search standard user by global search
                string user1 = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC1744, user1);

                //Verify searched user
                string userPeople1 = homePage.GetPeopleOrUserName();
                string userPeopleExl1 = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl1, userPeople1);
                extentReports.CreateLog("User " + userPeople1 + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string standardUser1 = login.ValidateUser();
                string standardUserExl1 = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(standardUserExl1.Contains(standardUser1), true);
                extentReports.CreateLog("Standard User: " + standardUser1 + " is able to login ");

                //Function to add an activity
                string HLAttendee1 = addActivity1.AddFutureActivityWithNewContact(fileTC1744);
                extentReports.CreateLog("An activity is created with new contact ");

                contactDetails.VerifyUpcomingActivity();
                addActivity1.ClickViewActivity();

                //Validate Activity type from Activity Details page.
                string followUpActivityType = activityDetail.GetActivityTypeValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 1), followUpActivityType);
                extentReports.CreateLog("Upcoming Activity Type: " + followUpActivityType + " is displayed on activity detail page ");

                //Validate Activity subject from Activity Details page.
                string followUpActivitySubject = activityDetail.GetActivitySubjectValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 2), followUpActivitySubject);
                extentReports.CreateLog("Upcoming Activity Subject: " + followUpActivitySubject + "is displayed on activity detail page ");

                activityDetail.DeleteActivity();
                extentReports.CreateLog("Delete activity ");
                usersLogin.UserLogOut();
                conHome.ClickContact();
                conHome.SearchContact(fileTC1744);

                // To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC1744, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));

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
