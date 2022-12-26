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
using System.Threading;

namespace SalesForce_Project.TestCases.Companies
{
    class T1894_CapIQCompanies_CapIQCompaniesDetailPage_AddSalesforceCompany : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactCreatePage createContact = new ContactCreatePage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        CapIQCompaniesHomePage capIQCompanyHome = new CapIQCompaniesHomePage();

        public static string fileTC1894 = "T1894_CapIQCompanies_CapIQCompaniesDetailPage_AddSalesforceCompany";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        //[Retry (2)]
        public void CapIQCompaniesDetailPage_AddSalesforceCompany()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1894;
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
                homePage.SearchUserByGlobalSearch(fileTC1894, user);

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
                
                // To search CapIQ company
                string companyName = ReadExcelData.ReadData(excelPath, "Company", 2);
                capIQCompanyHome.SearchCapIQCompany(companyName);
                string capIQCompanyDetailHeading = ReadExcelData.ReadData(excelPath, "Company", 4);
           //     Assert.AreEqual(capIQCompanyDetailHeading, capIQCompanyHome.GetCapIQCompanyDetailHeading());
                extentReports.CreateLog("Page with heading: " + capIQCompanyHome.GetCapIQCompanyDetailHeading() + " is displayed upon searching company ");

                // Click on Add salesforce company button on CapIQ company detail page
            capIQCompanyHome.ClickAddSalesforceCompanyButton();
                //Validate company edit label text
               Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 5), capIQCompanyHome.GetCompanyEditLabel());
               extentReports.CreateLog("Page with heading: " + capIQCompanyHome.GetCompanyEditLabel() + " is displayed upon click of add salesforce company button ");

                //Validate potential text message
              string potentialText = capIQCompanyHome.GetpotentialMatchText();
               Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 6), potentialText);
               extentReports.CreateLog("No Potential Match Text: " + potentialText + " is displayed upon click of add salesforce company button ");

                //Click on Cancel and Back button
             capIQCompanyHome.ClickCancelAndBackButton();
             string capIQCompanyDetailHead = capIQCompanyHome.GetCapIQCompanyDetailHeading();
              Assert.AreEqual(capIQCompanyDetailHeading, capIQCompanyDetailHead);
             extentReports.CreateLog("Page with heading: " + capIQCompanyDetailHead + " is displayed upon searching company ");

                // Click on Add salesforce company button on CapIQ company detail page
                capIQCompanyHome.ClickAddSalesforceCompanyButton();
                //Validate company edit label text
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 5), capIQCompanyHome.GetCompanyEditLabel());
                extentReports.CreateLog("Page with heading: " + capIQCompanyHome.GetCompanyEditLabel() + " is displayed upon click of add salesforce company button ");

                //Validate basic info compies of CapIQ company to Salesforce company
                capIQCompanyHome.ValidateBasicInfoCopiedOfCapIQToSalesforceComp();
                extentReports.CreateLog("Add salesforce company button is leaading to new company edit page with basic details copied as ticker symbol,business and company information ");

                //To create salesforce company
                capIQCompanyHome.ClickCreateButton();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 3), capIQCompanyHome.GetCompanyDetailHeading());
                extentReports.CreateLog("Page with heading: " + capIQCompanyHome.GetCompanyDetailHeading() + " is displayed upon click of create button ");

                //Validate company name is same as CapIQ company name
                string CompanyName = companyDetail.GetCompanyName();
                string companyNameExl = ReadExcelData.ReadData(excelPath, "Company", 2);
                Assert.AreEqual(companyNameExl, CompanyName);
                extentReports.CreateLog("Company Name: " + CompanyName + " is displayed on company details upon click of create button ");

                // To validate and get salesforce company name created
                string SalesforceCompanyName = capIQCompanyHome.ValidateAndGetCompanyName();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 2), SalesforceCompanyName);
                extentReports.CreateLog("Salesforce company: " + SalesforceCompanyName + " is created successfully ");

                //Validate relationship salesforce company with capiq company
                Assert.IsTrue(capIQCompanyHome.ValidateRelationshipSalesforceCompanyWCapIQCompany());
                extentReports.CreateLog("Salesforce company relationship is established with CapIQ company verified under CapIQ information section of company detail page ");
                Thread.Sleep(2000);

                //Logout from Standard user
                usersLogin.UserLogOut();
                extentReports.CreateLog("Standard user logged out successfully ");

                // To search created salesforce company 
                string CompanyType = ReadExcelData.ReadData(excelPath, "Company", 1);
                companyHome.SearchCompany(fileTC1894, CompanyType);
                extentReports.CreateLog("Created salesforce company is searched and selected ");

                // companyDetail.DeleteSalesforceCompany();
                companyDetail.DeleteCompany(fileTC1894, ReadExcelData.ReadData(excelPath, "Company", 1));
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("Deleted salesforce company ");

                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (WebDriverTimeoutException te)
            {
                extentReports.CreateLog(te.Message);
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