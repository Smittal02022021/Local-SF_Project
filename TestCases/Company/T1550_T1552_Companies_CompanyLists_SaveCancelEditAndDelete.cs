using NUnit.Framework;
using OpenQA.Selenium;
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
    class T1550_T1552_Companies_CompanyLists_SaveCancelEditAndDelete : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        CompanyListHomePage companyListHome = new CompanyListHomePage();
        CompanyListDetailPage companyListDetail = new CompanyListDetailPage();
        CompanyListCreatePage companyListCreate = new CompanyListCreatePage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1550_TC1552 = "T1550_T1552_Companies_NewCompanyListSaveCancelEditAndDelete";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void SaveCancelEditAndDeleteCompanyList()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1550_TC1552;
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
                homePage.SearchUserByGlobalSearch(fileTC1550_TC1552, user);
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

                companyListHome.VerifyPageAndFieldErrorMessage();
                string pageLevelError = companyListCreate.GetPageLevelError();
                Assert.AreEqual("Error: Invalid Data.\r\nReview all error messages below to correct your data.", pageLevelError);
                extentReports.CreateLog("Company list name page error message: " + pageLevelError+" is displayed upon click of save button without entering details ");

                //Validation of company list name error message
                string companyListError = CustomFunctions.ContactInformationFieldsErrorElement(driver, "Company List Name").Text;
                Assert.IsTrue(companyListError.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Company list name field error message: " + companyListError + " us displayed upon click of save button without entering details ");

                //Verify cancel button functionality
                companyListCreate.VerifyCancelFunctionality(fileTC1550_TC1552);
                bool validateCompanyListNotCreatedOnClickOfCancel = companyListHome.ValidateAvailabilityOfCompanyListAfterCancel();
                Assert.AreEqual(false, validateCompanyListNotCreatedOnClickOfCancel);
                extentReports.CreateLog("Company list is not created upon entering the details and click of cancel button and verified the visibility as: " + validateCompanyListNotCreatedOnClickOfCancel + " ");

                //Create new Company List
                companyListHome.AddCompanyList(fileTC1550_TC1552);
                string companyName = companyListDetail.GetCompanyName();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 1), companyName);
                extentReports.CreateLog("Company Name: " + companyName + " in add company list page matches on company list details page  ");

                //Search Company List
                companyListHome.NavigateToCompanyListPage();
                char companyListNameFirstChar = companyListHome.GetCompanyListNameFirstCharacter();
                Assert.AreEqual('C', companyListNameFirstChar);
                extentReports.CreateLog("Company List Names contains first character as:  "+ companyListNameFirstChar +" upon selecting that alphabet ");

                //Verify new created company list
                bool availabilityOfNewCompanyList = companyListHome.ValidateNewCreatedCompanyList();
                Assert.AreEqual(true, availabilityOfNewCompanyList);
                extentReports.CreateLog("New Company List is available in the company lists and verified availability as: "+availabilityOfNewCompanyList+" ");

                //Edit Company List
                companyListHome.EditCompanyList(fileTC1550_TC1552);
                string updatedCompanyListName = companyListHome.GetUpdatedCompanyList();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 2), updatedCompanyListName);
                extentReports.CreateLog("Company List updated name: " + updatedCompanyListName + " in edit company list pages matches on company list details page ");

                //Delete Company List
                companyListDetail.DeleteCompanyList();
                bool validateDeletionCompanList = companyListHome.ValidateDeletionOfCompanyList();
                Assert.AreEqual(false, validateDeletionCompanList);
                extentReports.CreateLog("Created and updated company list are deleted from company lists and return availablility in the list as: "+ validateDeletionCompanList + " after deletion ");

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