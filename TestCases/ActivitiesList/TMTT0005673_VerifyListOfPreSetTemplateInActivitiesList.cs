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
    class TMTT0005673_VerifyListOfPreSetTemplateInActivitiesList : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        ActivitiesListDetailPage activityList = new ActivitiesListDetailPage();
        ActivityDetailPage activityDetail = new ActivityDetailPage();

        public static string fileTMTT0005673 = "TMTT0005673_ActivitiesList_VerifyPreSetTemplateList.xlsx";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0005673;
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

                //Click on activities list tab
                activityList.ClickActivitiesListTab();
                extentReports.CreateLog("Activities List Tab is clicked ");

                activityList.ValidatePreSetTemplate(fileTMTT0005673);
                extentReports.CreateLog("All the PreSetTemplates are listed in the dropdown ");


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