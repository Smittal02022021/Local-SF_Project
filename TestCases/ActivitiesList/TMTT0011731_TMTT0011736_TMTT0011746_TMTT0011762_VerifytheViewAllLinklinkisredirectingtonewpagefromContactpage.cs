using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.ActivitiesList;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.ActivitiesList
{
    class TMTT0011731_TMTT0011736_TMTT0011746_TMTT0011762_VerifytheViewAllLinklinkisredirectingtonewpagefromContactpage : BaseClass
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
        AddActivity1 addActivity1 = new AddActivity1();
        public static string fileTC1731 = "TMTT0011731_VerifytheViewAllLinklinkisredirectingtonewpagefromContactpage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Activity_View_All()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1731;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                //Verified View All link on home page

                /*
                //Add Activity from View All Link from home page
                addActivity1.AddActivityFromViewAllLink();
                addActivity1.AddActivityFromContactDetail(fileTC1731);
                addActivity1.ClickSaveandReturnBtn();
                driver.Close();
                CustomFunctions.SwitchToWindow(driver, 0);
                driver.SwitchTo().DefaultContent();
                */
                addActivity1.VerifyViewAllLinkOnHomePage();
                extentReports.CreateLog("Verified View all link is displaying on homepage ");
                //Verified View All Link on Home page navigates to Activities page 

                addActivity1.ClickViewAllLinkOnHomePage();
                extentReports.CreateLog("View All Link on Homepage navigates to Activities page ");
                // Search contact
                conHome.SearchContact(fileTC1731);
                extentReports.CreateLog("Search for an contact ");
                //Verified View All link
                addActivity1.VerifyViewAllLink();
                extentReports.CreateLog("Verified View all link is displaying on contact page ");
                //Verified View All Link on contact page navigates to Activities page 
                addActivity1.ClickViewAllLink();
                extentReports.CreateLog("View All Link on contact page navigates to Activities page ");
                //Verified Previous and Next link on activities page
                addActivity1.VerifyPreviousNextLink();
                extentReports.CreateLog("Verified Previous and Next link on activites page ");
                //Verified filters on activities page
                addActivity1.VerifyFilter(fileTC1731);
                extentReports.CreateLog("Verified filters on activites page ");

                
                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC1731, user);

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

                //Verified View All link on home page

                addActivity1.VerifyViewAllLinkOnHomePage();
                extentReports.CreateLog("Verified View all link is displaying on homepage ");

                //Verified View All Link on Home page navigates to Activities page 
                addActivity1.ClickViewAllLinkOnHomePage();
               extentReports.CreateLog("View All Link on Homepage navigates to Activities page ");
                // Search contact
                conHome.SearchContact(fileTC1731);
                extentReports.CreateLog("Search for an contact ");
                addActivity1.VerifyViewAllLink();
                extentReports.CreateLog("Verified View all link is displaying on contact page ");
                //Verified View All link
                addActivity1.ClickViewAllLink();
                extentReports.CreateLog("View All Link on contact page navigates to Activities page ");
                //Verified Previous and Next link on activities page
                addActivity1.VerifyPreviousNextLink();
                extentReports.CreateLog("Verified Previous and Next link on activites page ");
                //Verified filters on activities page
                addActivity1.VerifyFilter(fileTC1731);



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

