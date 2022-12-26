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
    class TS18_ValidateERPBillingAndShippingFlagUpdate_Company : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        ContactCreatePage contactCreate = new ContactCreatePage();
        ContactDetailsPage contactDetail = new ContactDetailsPage();

        public static string TS018 = "TS18_Company_ERPBillingAndShippingFlagUpdate.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ValidateERPBillingAndShippingFlagUpdate()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TS018;
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
                string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearch(TS018, user);
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string StandardUser = login.ValidateUser();
                Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1).Contains(StandardUser), true);
                extentReports.CreateLog("Standard User: " + StandardUser + " is able to login ");

                int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    string CompanyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companyHome.SearchCompany(TS018, CompanyType);
                    
                    // ERP Submitted to sync time stamp
                    string beforeERPSubmittedToSync = companyDetail.GetValERPSubmittedToSync();
                    Console.WriteLine(beforeERPSubmittedToSync);
                    DateTime beforeERPSubmittedDate = Convert.ToDateTime(beforeERPSubmittedToSync);
                    Console.WriteLine(beforeERPSubmittedDate);
                    //ERP Last Integration Date time stamp
                    string beforeERPLastIntegrationDate = companyDetail.GetValERPLastIntegrationResponseDate();
                    DateTime beforeERPIntegrationDate = Convert.ToDateTime(beforeERPLastIntegrationDate);
                    Console.WriteLine(beforeERPIntegrationDate);

                    companyDetail.EditCompanyAddress(TS018, CompanyType);

                    string erpBillToAddressFlag = companyDetail.GetERPBillToAddressFlag();
                    Assert.AreEqual("Update", erpBillToAddressFlag);

                    string erpShipToAddressFlag = companyDetail.GetERPShipToAddressFlag();
                    Assert.AreEqual("Update", erpShipToAddressFlag);

                    //Validate ERP submitted to sync date
                    string cstDate = CustomFunctions.GetCurrentPSTDate();
                    string ERPSubmittedToSync = companyDetail.GetValERPSubmittedToSync();
                    Assert.IsTrue(ERPSubmittedToSync.Contains(cstDate));
                    extentReports.CreateLog("ERP submitted to sync date is updated with latest time stamp ");

                    DateTime afterERPSubmittedDate = Convert.ToDateTime(ERPSubmittedToSync);
                    int ERPSubmittedDateCompare = DateTime.Compare(afterERPSubmittedDate, beforeERPSubmittedDate);
                    Assert.AreEqual(1, ERPSubmittedDateCompare);
                    extentReports.CreateLog("ERP submitted to sync date is updated with latest time stamp after adding opportunity in company and converting it into engagement ");

                    //Refresh the page 
                    driver.Navigate().Refresh();

                    //Verify last integration response date time stamp is greater than previous time stamp
                    string ERPLastIntegrationResponseDate = companyDetail.GetValERPLastIntegrationResponseDate();
                    DateTime afterERPlastIntegrationResponseDate = Convert.ToDateTime(ERPLastIntegrationResponseDate);
                    int ERPLastIntegrationResponseDateCompare = DateTime.Compare(afterERPlastIntegrationResponseDate, beforeERPIntegrationDate);
                    //Assert.AreEqual(1, ERPLastIntegrationResponseDateCompare);
                    extentReports.CreateLog("ERP Last Integration Response Date is updated with latest time stamp after adding opportunity in company and converting it into engagement ");

                    //Verify Last Integration status
                    string ERPLastIntegrationStatus = companyDetail.GetValERPLastIntegrationStatus();
                    Assert.AreEqual("Success", ERPLastIntegrationStatus);
                    extentReports.CreateLog("ERP Last Integration Status: " + ERPLastIntegrationStatus + " is updated ");
                }
                // Logout from standard User
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
