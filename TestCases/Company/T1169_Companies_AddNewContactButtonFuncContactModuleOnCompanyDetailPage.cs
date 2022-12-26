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
    class T1169_Companies_AddNewContactButtonFuncContactModuleOnCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1169 = "T1169_Companies_AddNewContactButtonFuncContactModuleOnCompanyDetailPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddNewContactButtonFuncContactModuleOnCompanyDetailPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1169;
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
                int rowUserType = ReadExcelData.GetRowCount(excelPath, "UsersType");
                for (int rows = 2; rows <= rowUserType; rows++)
                {
                    string userType = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", rows, 1);
                    if (userType.Equals("HR"))
                    {
                        // Search standard user by global search
                        string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                        homePage.SearchUserByGlobalSearch(fileTC1169, user);

                        //Verify searched user
                        string userPeople = homePage.GetPeopleOrUserName();
                        string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                        Assert.AreEqual(userPeopleExl, userPeople);
                        extentReports.CreateLog("User " + userPeople + " details are displayed ");

                        //Login as HR user
                        usersLogin.LoginAsSelectedUser();
                        string HRUser = login.ValidateUser();
                        string HRUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                        Assert.AreEqual(HRUserExl.Contains(HRUser), true);
                        extentReports.CreateLog("HR User: " + HRUser + " is able to login ");
                    }
                    int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                    for (int row = 2; row <= rowCompanyName; row++)
                    {
                        // Calling Search Contact function
                        string companyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                        companyHome.SearchCompany(fileTC1169, companyType);
                        



                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 3), companyDetail.GetCompanyDetailsHeading());
                        extentReports.CreateLog("Page with heading: " + companyDetail.GetCompanyDetailsHeading() + " is displayed upon searching company ");

                        //Calling click new contact button on company details page
                        companyDetail.ClickNewContactButton();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: Select Contact Record Type ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("Select contact record type page is displayed upon clicking new contact button ");

                        // Calling ValidateContactRecordType function to validate all types of contact record type
                        conSelectRecord.ValidateContactRecordType(fileTC1169, rows);
                        extentReports.CreateLog("Contact record types -  Distribution List,External Contact and Houlihan Employee are available for company record type - " + companyType + " with user type - "+ userType+" ");
                    }
                    if (userType.Equals("HR"))
                    {
                        usersLogin.UserLogOut();
                        extentReports.CreateLog("Logout from HR user ");
                    }
                }
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
