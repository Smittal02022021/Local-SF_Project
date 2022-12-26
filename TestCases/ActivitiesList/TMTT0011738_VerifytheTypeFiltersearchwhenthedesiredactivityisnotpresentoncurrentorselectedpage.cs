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
    class TMTT0011738_VerifytheTypeFiltersearchwhenthedesiredactivityisnotpresentoncurrentorselectedpage: BaseClass
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

        public static string fileTC1738 = "TMTT0011738_VerifytheTypeFiltersearchwhenthedesiredactivityisnotpresentoncurrentorselectedpage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);

        }

        [Test]
        public void VerifyActivityFilter()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1738;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                string text=addActivity1.GetActivityCount();

                //int num = integer.parseInt(text);
                extentReports.CreateLog("Activity count:"+text+" ");
                int cnt= int.Parse(text);

                while (cnt <= 10) {
                    string getDateToday = DateTime.Today.ToString("MM/dd/yyyy");
                    conHome.SearchContact(fileTC1738, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                    //addActivity1.AddActivityFromContactDetail(fileTC1503);
                    contactDetails.CreateAnActivity1(fileTC1738, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1), getDateToday);

                    usersLogin.ClickHomeTab();
                    cnt++;
                }
                conHome.SearchContact(fileTC1738, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                addActivity1.VerifyFilter(fileTC1738);
                extentReports.CreateLog("Verified type filter for admin user ");

                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC1738, user);

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
                conHome.SearchContact(fileTC1738, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 3, 1));
                addActivity1.VerifyFilter(fileTC1738);
                extentReports.CreateLog("Verified type filter for standard user ");

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
