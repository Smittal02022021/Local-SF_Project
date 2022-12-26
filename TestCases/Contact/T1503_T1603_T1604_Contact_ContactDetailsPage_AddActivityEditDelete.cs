using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Diagnostics;
using System.Threading;

namespace SalesForce_Project.TestCases.Contact
{
    class T1503_T1603_T1604_Contact_ContactDetailsPage_AddActivityEditDelete : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1503 = "T1503_Contact_ContactDetails_AddEditDeleteActivity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddEditAndDeleteActivity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1503;
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
                homePage.SearchUserByGlobalSearch(fileTC1503, user);

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


                // Loop to iterate Activity Create Edit and Delete for HL and External Contact
              //  int rowContactName = ReadExcelData.GetRowCount(excelPath, "ContactTypes");
                int rowContactName = 2;

                for (int row = 2; row <= rowContactName; row++)
                {
                    // Calling Search Contact function
                    conHome.SearchContact(fileTC1503, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1));
                    string contactDetailHeading = createContact.GetContactDetailsHeading();
                    Assert.AreEqual("Contact Detail", contactDetailHeading);
                    extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon search for a contact ");

                    // Calling Create an activity function
                    if (row.Equals(2))
                    {
                        string getDateToday = DateTime.Today.ToString("MM/dd/yyyy");

                        contactDetails.CreateAnActivity(fileTC1503, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1), getDateToday);
                        extentReports.CreateLog("Activity is created for an external contact ");
                    }
                    else
                    {
                        string getDateTommorow = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
                        contactDetails.CreateAnActivity(fileTC1503, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1), getDateTommorow);
                        extentReports.CreateLog("Activity is created for an houlihan employee  ");
                    }
                    Assert.AreEqual("Contact Detail", contactDetailHeading);
                    extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon creating an activity ");

                    // Calling View Existing Activity function to check details of activity attached to the contact
                    contactDetails.ViewExistingActivity();
                    extentReports.CreateLog("View link is visible and clickable corresponding to the Activity details ");

                    //Validate Activity type from Activity Details page.
                    string activityType = contactDetails.GetActivityTypeText();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 1), activityType);
                    extentReports.CreateLog("Activity Type: " + activityType + " is displayed on activity detail page ");

                    //Validate Activity subject from Activity Details page.
                    string activitySubject = contactDetails.GetActivitySubjectText();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 2), activitySubject);
                    extentReports.CreateLog("Activity Subject: " + activitySubject + " is displayed on activity detail page ");

                    //Return to contact details from activity details
                    contactDetails.ReturnToContactDetailsFromActivityDetails();
                    string contactDetail = createContact.GetContactDetailsHeading();
                    Assert.AreEqual("Contact Detail", contactDetail);
                    extentReports.CreateLog("Page with heading: " + createContact.GetContactDetailsHeading() + " is displayed upon click or return button ");

                    // Edit Existing activity 
                    contactDetails.EditExistingActivity(fileTC1503, 1);
                    //Validate updated Activity type after edit
                    string activityTypeUpdated = contactDetails.GetActivityTypeText();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 3, 1), activityTypeUpdated);
                    extentReports.CreateLog("Activity Type: " + activityTypeUpdated + " is displayed on activity detail page ");

                    // Validate updated activity type after edit
                    string updatedActivitySubject = contactDetails.GetActivitySubjectText();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 3, 2), updatedActivitySubject);
                    extentReports.CreateLog("Activity Subject: " + updatedActivitySubject + " is displayed on activity detail page ");

                    if (row.Equals(2))
                    {
                        //extentReports.CreateLog("entry loop");
                        //Click on home page and verify today's activity
                        contactDetails.VerifyTodaysActivity();
                        //  extentReports.CreateLog("verify today up");
                        string valLatestActivityType = contactDetails.GetTodayActivityTypeText();
                        // extentReports.CreateLog("verify today up");
                        Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 3, 1), valLatestActivityType);
                        extentReports.CreateLog("Today's Latest Activity Type: " + valLatestActivityType + " is displayed on home page under today's activities section  ");
                        //extentReports.CreateLog("verify today up");
                        string valLatestActivitySubject = contactDetails.GetTodayActivitySubjectText();
                        Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 3, 2), valLatestActivitySubject);
                        extentReports.CreateLog("Today's Latest Activity Subject: " + valLatestActivitySubject + " is displayed on home page under today's activities section  ");
                    }
                    else
                    {
                        //Click on home page and verify today's activity
                        contactDetails.VerifyUpcomingActivity();

                        string valLatestActivityType = contactDetails.GetTodayActivityTypeText();
                        Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 3, 1), valLatestActivityType);
                        extentReports.CreateLog("Upcoming's Latest Activity Type: " + valLatestActivityType + " is displayed on home page under upcoming's activities section  ");

                        string valLatestActivitySubject = contactDetails.GetTodayActivitySubjectText();
                        Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 3, 2), valLatestActivitySubject);
                        extentReports.CreateLog("Upcoming's Latest Activity Subject: " + valLatestActivitySubject + " is displayed on home page under upcoming's activities section  ");

                    }

                    CustomFunctions.SwitchToWindow(driver, 0);

                    //Delete Activity
                    conHome.SearchContact(fileTC1503, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1));

                    contactDetails.ViewExistingActivity();

                    // Delete activity
                    contactDetails.DeleteActivity();
                    extentReports.CreateLog("Delete the created activity ");
                    
                    /*
                     if (row.Equals(2))
                     {
                         string chkNoActivity = contactDetails.GetNoActivityDisplayText();
                         Assert.AreEqual("No Activities To Display", chkNoActivity);
                         CustomFunctions.SwitchToWindow(driver, 0);
                         extentReports.CreateLog("Validated that no Activity detail is available after deletion, Activity deleted successfully ");
                     }


                  */
                }
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