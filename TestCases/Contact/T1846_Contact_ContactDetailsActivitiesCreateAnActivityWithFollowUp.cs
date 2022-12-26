using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.TestCases.Contact
{
    class T1846_Contact_ContactDetailsActivitiesCreateAnActivityWithFollowUp : BaseClass
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

        public static string fileTC1846 = "T1846_Contact_ContactDetailsActivitiesCreateAnActivityWithFollowUpContact";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contact_ContactDetailsActivitiesCreateAnActivityWithFollowUp()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1846;
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
                homePage.SearchUserByGlobalSearch(fileTC1846, user);

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

                string contactType = ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1);
                conHome.SearchContact(fileTC1846, contactType);
                
                string contactDetailHeading = createContact.GetContactDetailsHeading();
                Assert.AreEqual("Contact Detail", contactDetailHeading);
                extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon search for a contact ");

                string defaultExternalAttendeeOnAddActivity = addActivity.GetDefaultExternalAttendee();
                extentReports.CreateLog("External Attendee: "+ defaultExternalAttendeeOnAddActivity +" is added by default on Add Activity page ");

                string defaultHLEmployeeOnAddActivity = addActivity.GetDefaultHLAttendee1();
                extentReports.CreateLog("HL Employee: " + defaultHLEmployeeOnAddActivity + " is added by default on Add Activity page ");

                string FollowUpDate = addActivity.AddActivityFromContactDetail(fileTC1846);
                extentReports.CreateLog("Activity is created successfully with follow up details ");

                //Validate Activity Details heading is visible
                string activityDetailHeading = activityDetail.GetActivityDetailsHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 7), activityDetailHeading);
                extentReports.CreateLog("Page with heading: " + activityDetailHeading + " is displayed upon Adding an Activity ");

                //Validate Activity Type selected is visible on activity detail page
                string activityType = activityDetail.GetActivityTypeValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 1), activityType);
                extentReports.CreateLog("Activity Type: " + activityType + " entered in add activity page matches on activity detail page ");


                //Validate Activity Subject selected is visible on activity detail page
                string activitySubject = activityDetail.GetActivitySubjectValue();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 2), activitySubject);
                extentReports.CreateLog("Activity Subject: " + activitySubject + " entered in add activity page matches on activity detail page ");

                //Validate Activity company discussed selected is visible on activity detail page
                string activityCompanyDiscussed = activityDetail.GetCompanyDiscussedName();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 3), activityCompanyDiscussed);
                extentReports.CreateLog("Activity Company Discussed: " + activityCompanyDiscussed + " entered in add activity page matches on activity detail page ");

                //Validate Activity related opportunity 
                string relatedOpportunity = activityDetail.GetRelatedOpportunities();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 4), relatedOpportunity);
                extentReports.CreateLog("Activity related Opportunity: " + relatedOpportunity + " entered in add activity page matches on activity detail page ");

                // Click on Return button
                activityDetail.ClickReturn();
                Assert.AreEqual("Contact Detail", contactDetailHeading);
                extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon click of return button ");

                contactDetails.ViewExistingActivity();
                extentReports.CreateLog("Click on view link corresponding to followup activity ");

                //Validate Activity type from Activity Details page.
                string followUpActivityType = activityDetail.GetActivityTypeValue();
                Assert.AreEqual("Follow-up "+ ReadExcelData.ReadData(excelPath, "Activity", 5), followUpActivityType);
                extentReports.CreateLog("Follow Up Activity Type: " + followUpActivityType + " is displayed on activity detail page ");

                //Validate Activity subject from Activity Details page.
                string followUpActivitySubject = activityDetail.GetActivitySubjectValue(); 
                Assert.AreEqual("Follow-up: "+ ReadExcelData.ReadData(excelPath, "Activity", 2), followUpActivitySubject);
                extentReports.CreateLog("Follow Up Activity Subject: " + followUpActivitySubject + "is displayed on activity detail page ");

                //Validate Activity description from Activity Details page.
                string activityDesc = activityDetail.GetActivityFollowUpDesc();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Activity", 6), activityDesc);
                extentReports.CreateLog("Follow Up Activity Description: " + activityDesc + "is displayed on activity detail page ");

                string copiedExternalAttendeeOnFollowUp = activityDetail.GetActivityExternalAttendees();
                Assert.AreEqual(defaultExternalAttendeeOnAddActivity, copiedExternalAttendeeOnFollowUp);
                extentReports.CreateLog("External attendee copied over to follow up activity detail successfully ");

                string copiedHLEmployeeOnFollowUp = activityDetail.GetActivityHLAttendees();
                Assert.AreEqual(defaultHLEmployeeOnAddActivity, copiedHLEmployeeOnFollowUp);
                extentReports.CreateLog("HL employee copied over to follow up activity detail successfully ");

                string companiesDiscussedOnFollowUp = activityDetail.GetCompaniesDiscussedInFollowUpActivity();
                Assert.AreEqual(activityCompanyDiscussed, companiesDiscussedOnFollowUp);
                extentReports.CreateLog("Companies discussed copied over to follow up activity detail successfully ");

                string relatedOpportunityOnFollowUp = activityDetail.GetRelatedOpportunityInFollowUpActivity();
                Assert.AreEqual(relatedOpportunity, relatedOpportunityOnFollowUp);
                extentReports.CreateLog("Related Opportunity copied over to follow up activity detail successfully ");

                string HLEmployeeName = defaultHLEmployeeOnAddActivity;
               
               // string HLAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 3, 1);
                conHome.SearchHLAttendee(HLEmployeeName);
                extentReports.CreateLog("HL Attendee contact is searched and reached to its home page successfully ");

                string followUpActivity = contactDetails.GetFollowUpActivityType();
                //Assert.AreEqual("Follow-up " + ReadExcelData.ReadData(excelPath, "Activity", 5) + " (Pending)", followUpActivity);

                Assert.AreEqual("Follow-up " + ReadExcelData.ReadData(excelPath, "Activity", 5) , followUpActivity);
                extentReports.CreateLog("Future Date Follow up are classified with Type: "+ followUpActivity+ " is displayed ");

                //Delete Normal and Follow Up Activity
                contactDetails.ViewExistingActivity();
                contactDetails.DeleteActivity();

                contactDetails.ViewExistingActivity();
                contactDetails.DeleteActivity();

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
