using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Companies
{
    class T1147_Companies_Common_CompanyEditAndUpdateTheExistingInformation : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1147 = "T1147_Companies_EditAndUpdateTheExistingInformation.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void EditAndUpdateCompanyExistingInformation()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1147;
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
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC1147, user);
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string StandardUser = login.ValidateUser();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Users", 1).Contains(StandardUser), true);
                extentReports.CreateLog("Standard User: " + StandardUser + " is able to login ");

                // Calling Search Company function
                companyHome.SearchCompany(fileTC1147, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
               
                string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 3), companyDetailHeading);
                extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon searching company ");

                // Update existing company details
                companyEdit.UpdateExistingCompanyDetails(fileTC1147);
                extentReports.CreateLog("Existing company details are updated sucessfully. ");

                //Validated updated address
                string companyCompleteAddress = companyDetail.GetCompanyCompleteAddress();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 6) + "\r\n" + ReadExcelData.ReadData(excelPath, "Company", 7)+", " + ReadExcelData.ReadData(excelPath, "Company", 8) +" "+ ReadExcelData.ReadData(excelPath, "Company", 9) + "\r\n"+ ReadExcelData.ReadData(excelPath, "Company", 5), companyCompleteAddress);
                extentReports.CreateLog("Updated Company address: " + companyCompleteAddress + " includes street,city,state,zip code and country in edit company page matches on company details page ");

                //Validate updated phone number
                string companyPhone = companyDetail.GetCompanyPhoneNumber();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 4), companyPhone);
                extentReports.CreateLog("Updated Company phone number: " + companyPhone + " in edit company page matches on company details page ");
               
                //Validate company text description
                string companyDesc = companyDetail.GetCompanyDescription();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 10), companyDesc);
                extentReports.CreateLog("Updated Company description: " + companyDesc + " in edit company page matches on company details page ");

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