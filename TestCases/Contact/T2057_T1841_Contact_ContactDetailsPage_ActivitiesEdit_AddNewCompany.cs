using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Contact
{
    class T2057_T1841_Contact_ContactDetailsPage_ActivitiesEdit_AddNewCompany : BaseClass
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
        CompanyHomePage comHome = new CompanyHomePage();
        CompanyDetailsPage compDetail = new CompanyDetailsPage();

        public static string fileTC2057 = "T2057_ContactDetailsPage_ActivitiesEdit_AddNewCompany";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2057;
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
                homePage.SearchUserByGlobalSearch(fileTC2057, user);

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
                string HLAttendee = addActivity.AddAnActivityWithNewCompany(fileTC2057);
                extentReports.CreateLog("An activity is created with new company ");

                //Verify internal notes
                string activityInternalNotes = activityDetail.GetAtivityInternalNotes();
                string activityInternalNotesExl = ReadExcelData.ReadData(excelPath, "Activity", 5);
                Assert.AreEqual(activityInternalNotesExl, activityInternalNotes);
                extentReports.CreateLog("Internal notes entered when creating an activity is matching with the internal notes in excel ");

                //Verify company discussed
                string newCompanyDiscussed = activityDetail.GetNewCompanyDiscussed();
                string newCompanyDiscussedExl = ReadExcelData.ReadData(excelPath, "Activity", 8);
                Assert.AreEqual(newCompanyDiscussedExl, newCompanyDiscussed);
                extentReports.CreateLog("New company name entered is matching with the details in Activity Details page ");

                //Click on return button
               // activityDetail.ClickReturn();

                //Search HL Attendee of the activity
                conHome.SearchHLAttendee(HLAttendee);
                extentReports.CreateLog("HL Attendee contact is searched and reached to its home page successfully ");

                contactDetails.EditExistingActivityInternalNotes(fileTC2057);
                //Verify updated internal notes
                string activityUpdatedInternalNotes = activityDetail.GetAtivityInternalNotes();
                string activityUpdatedInternalNotesExl = ReadExcelData.ReadData(excelPath, "Activity", 9);
                Assert.AreEqual(activityUpdatedInternalNotesExl, activityUpdatedInternalNotes);
                extentReports.CreateLog("Internal notes entered when creating an activity is matching with the internal notes in excel ");

                activityDetail.DeleteActivity();
                extentReports.CreateLog("Delete activity ");

                usersLogin.UserLogOut();

                //Delete new company
                comHome.SearchCompany(fileTC2057, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                compDetail.DeleteCompany(fileTC2057, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                extentReports.CreateLog("New company is deleted ");
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