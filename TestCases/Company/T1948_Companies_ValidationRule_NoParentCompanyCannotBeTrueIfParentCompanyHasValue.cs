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

namespace SalesForce_Project.TestCases.Companies
{
    class T1948_Companies_ValidationRule_HQCannotBeTrueIfParentCompanyHasValue : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage oppDetails = new OpportunityDetailsPage();
        OpportunityHomePage oppHomePage = new OpportunityHomePage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        CompanyEditPage companyEdit = new CompanyEditPage();

        public static string fileTC1948 = "T1948_Companies_ValidationRule_HQCannotBeTrueIfParentCompanyHasValue";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void HQCannotBeTrueIfParentCompanyHasValue()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1948;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                CustomFunctions.TableauPopUp();
                // Search standard user by global search
                /* string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                 homePage.SearchUserByGlobalSearch(fileTC1948, user);

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
                 */
                int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    // Calling Search Company function
                    companyHome.SearchCompany(fileTC1948, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1));
                    string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 3), companyDetailHeading);
                    extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon searching company ");

                    companyEdit.ValidateUpdatingParentCompanyOnly(fileTC1948);
                    string parentCompany = companyDetail.GetParentCompany();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 4), parentCompany);
                    extentReports.CreateLog("Parent Company: " + parentCompany + " is updated successfully when updated individually without checking HQ ");

                    companyEdit.ValidateCheckingHQCheckBoxOnly(fileTC1948);
                    string HQCheckBoxValue = companyDetail.GetHQCheckBoxValue();
                    Assert.AreEqual("Checked", HQCheckBoxValue);
                    extentReports.CreateLog("HQ checkbox marked as : " + HQCheckBoxValue + " is updated successfully when updated individually without adding parent company ");

                    companyEdit.ValidateErrorHQCanBeTrue(fileTC1948);
                    string errorMsg = companyEdit.GetErrorMsgHQNoTrue();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 5), errorMsg);
                    extentReports.CreateLog("Error Message : " + errorMsg + " is validated upon entering parent company and checking HQ checkbox ");

                }
                    //usersLogin.UserLogOut();
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