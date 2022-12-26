using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.GiftLog;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.GiftLog
{
    class T1509_VerifysubmittedforfieldslookupisdisplayingthecontactsofRecordTypesHoulihanEmployeewithdetails : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
    LoginPage login = new LoginPage();
    CompanyHomePage companyHome = new CompanyHomePage();
    CompanyDetailsPage companyDetail = new CompanyDetailsPage();
    UsersLogin usersLogin = new UsersLogin();
    HomeMainPage homePage = new HomeMainPage();
    GiftRequestPage giftRequest = new GiftRequestPage();

    public static string fileTC1509 = "T1509_Verifysubmittedforfield'slookupisdisplayingthecontactsofRecordTypesHoulihanEmployeewithdetails";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Initialize();
        ExtentReportHelper();
        ReadJSONData.Generate("Admin_Data.json");
        extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
    }

    [Test]
    public void Verify_submitted_for_fields_look_up_is_displaying_the_contacts_of_Record_Types_Houlihan_Employee_with_details()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1509;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser1 + " is able to login ");


                //Navigate to Gift Request page
                giftRequest.GoToGiftRequestsPage();
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
        Assert.AreEqual("Client Gift Pre-approval", giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                //Verify text displaying on Submitted for look up
                 String txtFrame = giftRequest.TxtfromLookupSubittedFor();
               
                 Assert.AreEqual("Shows only Houlihan Employees.", txtFrame);
                extentReports.CreateLog("Text: "+txtFrame+" is displaying on frame ");

            //Verify Name Checkbox is selected by default
               Assert.AreEqual(giftRequest.VerifyRadioBtnName(), true);
                extentReports.CreateLog("Name checkbox is selected by Default ");

                //Verify when user searched external contacts then zero contacts is retrieved
                 String TxtSrchResults = giftRequest.SrchExternalContct();
                     Assert.AreEqual("Contacts [0]", TxtSrchResults);
                extentReports.CreateLog("Search external contacts retrieved zero contacts ");

                //Verify appropriate results are displayed when user searched with partial name of Houlihan Employee
                string cntemployee = giftRequest.SrchHoulihanEmployee();

                Assert.IsTrue(cntemployee.Contains("Oscar"));
                extentReports.CreateLog("Appropriate results are dispalyed when user searched with partial name of Houlihan Employee ");
                
                //Verify appropriate results are displayed when user searched for title
                String TitleSrchResults = giftRequest.SrchTitle();
                Assert.IsTrue(TitleSrchResults.Contains("Managing Director"));
              //  Assert.AreEqual("Managing Director", TitleSrchResults);
                extentReports.CreateLog("Appropriate results are displayed when user searched for title ");

              //  Thread.Sleep(3000);
                //Verify appropriate results are displayed when user searched for Department
                String DeptSrchResults = giftRequest.SrchDept();
                Assert.AreEqual("FR", DeptSrchResults);
                extentReports.CreateLog("Appropriate results are displayed when user searched for Department ");




                //  Thread.Sleep(300);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}