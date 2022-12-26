using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.ActivitiesList;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.ActivitiesList
{
    class TMTT0005674_VerifyTheActivitiesListFieldsOrColumnOnActivityListPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        ActivitiesListDetailPage activityList = new ActivitiesListDetailPage();
        ActivityDetailPage activityDetail = new ActivityDetailPage();
        ContactDetailsPage contDetail = new ContactDetailsPage();

        ContactCreatePage createContact = new ContactCreatePage();
        public static string fileTMTT0005674 = "TMTT0005674_ActivitiesList_VerifyFieldsOrColumnNames.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyLayoutOfActivitiesList()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0005674;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Handling salesforce Lightning
                //login.HandleSalesforceLightningPage();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTMTT0005674, user);

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

                //Click on activities list tab
                activityList.ClickActivitiesListTab();
                extentReports.CreateLog("Activities List Tab is clicked ");

                //activityList.SelectPreSetTemplate(fileTMTT0005674);
                //extentReports.CreateLog("Select for any pre listed template from dropdown ");
                driver.Navigate().Refresh();
                activityList.VerifyFieldsOrColumnsOnActivitiesList(fileTMTT0005674);
                extentReports.CreateLog("All the columns names are listed correct for Pre Set Templates related table ");

                string getSubjectTagName = activityList.GetTagNameOfSubject();
                Assert.AreNotEqual("a", getSubjectTagName);
                extentReports.CreateLog("Subject link is not a hyperlink text ");

                string contactNameActivityList = activityList.GetContactName();
                activityList.ClickContactName();
                string contactDetailHeading = createContact.GetContactDetailsHeading();
                Assert.AreEqual("Contact Detail", contactDetailHeading);
              
                extentReports.CreateLog("Clicking on contact name navigates to contact detail page ");


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