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
    class T1845_T1961_T1966_Contact_ContactDetails_Activities_AddEdit_PrivateActivity : BaseClass
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

        public static string fileTC1845 = "T1845_ContactDetails_Activities_AddPrivateActivity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contact_ContactDetails_Activities_PrivateActivity_AddEdit()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1845;
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
                homePage.SearchUserByGlobalSearch(fileTC1845, user);

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

                // Search for external contact
                conHome.SearchContact(fileTC1845);
                extentReports.CreateLog("Search for an external contact ");

                //Function to add an activity
                addActivity.AddPrivateActivity(fileTC1845);
                extentReports.CreateLog("Add a private activity to external contact ");
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

                activityDetail.ClickReturn();
                // Search for external contact
                //conHome.SearchContact(fileTC1845);
                //extentReports.CreateLog("Search for an external contact ");
                int sizeOfActivityLists = contactDetails.ActivityListCount();
                for (int i = 1; i <= sizeOfActivityLists; i++)
                {
                    string editLinkAvailable = contactDetails.GetEditLink(i);
                    if (editLinkAvailable.Equals("Edit"))
                    {
                        string activityAction = contactDetails.GetViewOrPrint(i);
                        Assert.AreEqual("ViewEdit Print", activityAction);
                        extentReports.CreateLog("View Edit and Print links are displayed to HL Attendee ");
                        break;
                    }
                }

                driver.SwitchTo().DefaultContent();
                usersLogin.UserLogOut();
                // Search standard user by global search
                string user2 = ReadExcelData.ReadData(excelPath, "Users", 2);
                homePage.SearchUserByGlobalSearch(fileTC1845, user2);

                //Verify searched user
                string userPeople2 = homePage.GetPeopleOrUserName();
                string userPeopleExl2 = ReadExcelData.ReadData(excelPath, "Users", 2);
                Assert.AreEqual(userPeopleExl2, userPeople2);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string standardUser2 = login.ValidateUser();
                string standardUserExl2 = ReadExcelData.ReadData(excelPath, "Users", 2);
                Assert.AreEqual(standardUserExl2.Contains(standardUser2), true);
                extentReports.CreateLog("Standard User: " + standardUser2 + " is able to login ");

                // Search for external contact
                conHome.SearchContact(fileTC1845);
                extentReports.CreateLog("Search for an external contact ");

                int sizeOfActivityList = contactDetails.ActivityListCount();
                for (int i = 1; i <= sizeOfActivityList; i++)
                {
                    string activityTypes = contactDetails.GetActivityType(i);
                    if (activityTypes.Equals("Private"))
                    {

                        extentReports.CreateLog("The type of activity is " + activityTypes + " ");
                        string activitySub = contactDetails.GetActivitySubject(i);
                        Assert.AreEqual("Private", activitySub);

                        extentReports.CreateLog("The subject of activity is "+ activitySub + " ");
                        string activityDesc = contactDetails.GetActivityDesc(i);
                        Assert.AreEqual("Private", activityDesc);
                        extentReports.CreateLog("The description of activity is " + activityDesc +" ");

                        string activityAction = contactDetails.GetViewOrPrint(i);
                        Assert.AreNotEqual("View", activityAction);
                        extentReports.CreateLog("View links are not displayed ");

                        Assert.AreEqual("Print", activityAction);
                        extentReports.CreateLog("Print link is being displayed ");
                        break;
                    }                  
                }
                driver.SwitchTo().DefaultContent();

                usersLogin.UserLogOut();

                
                driver.SwitchTo().DefaultContent();

                

                homePage.SearchUserByGlobalSearch(fileTC1845, user);

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string standardUser3 = login.ValidateUser();
                string standardUserExl3 = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(standardUserExl3.Contains(standardUser3), true);
                extentReports.CreateLog("Standard User: " + standardUser3 + " is able to login ");

                // Search for external contact
                conHome.SearchContact(fileTC1845);
                extentReports.CreateLog("Search for an external contact ");
                int sizeOfActivityListing = contactDetails.ActivityListCount();
                for (int i = 1; i <= sizeOfActivityListing; i++)
                {
                    string editLinkAvailable = contactDetails.GetEditLink(i);
                    if (editLinkAvailable.Equals("Edit"))
                    {
                        contactDetails.EditExistingActivities(fileTC1845,i);
                        break;
                    }
                }
                extentReports.CreateLog("Edit the existing activity successfully. ");
                usersLogin.UserLogOut();
                // Search standard user by global search
                string user3 = ReadExcelData.ReadData(excelPath, "Users", 2);
                homePage.SearchUserByGlobalSearch(fileTC1845, user2);

                //Verify searched user
                string userPeople3 = homePage.GetPeopleOrUserName();
                string userPeopleExl3 = ReadExcelData.ReadData(excelPath, "Users", 2);
                Assert.AreEqual(userPeopleExl3, userPeople3);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string standardUser4 = login.ValidateUser();
                string standardUserExl4 = ReadExcelData.ReadData(excelPath, "Users", 2);
                Assert.AreEqual(standardUserExl4.Contains(standardUser4), true);
                extentReports.CreateLog("Standard User: " + standardUser4 + " is able to login ");

                // Search for external contact
                conHome.SearchContact(fileTC1845);
                extentReports.CreateLog("Search for an external contact ");

                int sizeOfActivityList2 = contactDetails.ActivityListCount();
                for (int i = 1; i <= sizeOfActivityList2; i++)
                {
                    string activityTypes = contactDetails.GetActivityType(i);
                    if (activityTypes.Equals("Private"))
                    {

                        extentReports.CreateLog("The type of activity is " + activityTypes + " ");
                        string activitySub = contactDetails.GetActivitySubject(i);
                        Assert.AreEqual("Private", activitySub);

                        extentReports.CreateLog("The subject of activity is " + activitySub + " ");
                        string activityDesc = contactDetails.GetActivityDesc(i);
                        Assert.AreEqual("Private", activityDesc);
                        extentReports.CreateLog("The description of activity is " + activityDesc + " ");

                        string activityAction = contactDetails.GetViewOrPrint(i);
                        Assert.AreNotEqual("View", activityAction);
                        extentReports.CreateLog("View links are not displayed ");

                        Assert.AreEqual("Print", activityAction);
                        extentReports.CreateLog(" Print link is being displayed even after the private activity is being edited by the user ");

                        break;
                    }
                 
                }
                driver.SwitchTo().DefaultContent();


               
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