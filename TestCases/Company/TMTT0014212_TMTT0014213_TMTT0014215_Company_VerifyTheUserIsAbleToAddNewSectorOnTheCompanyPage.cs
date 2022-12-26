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
    class TMTT0014212_TMTT0014213_TMTT0014215_Company_VerifyTheUserIsAbleToAddNewSectorOnTheCompanyPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        UsersLogin usersLogin = new UsersLogin();
        CoverageSectorDependenciesHomePage coverageSectorDependenciesHome = new CoverageSectorDependenciesHomePage();
        NewCoverageSectorDependenciesPage newCoverageSectorDependencies = new NewCoverageSectorDependenciesPage();
        CoverageSectorDependenciesDetailPage coverageSectorDependenciesDetail = new CoverageSectorDependenciesDetailPage();

        public static string fileTC14212 = "TMTT0014212_VerifyTheUserIsAbleToAddNewSectorOnContactCompanyOpportunityEngagementPages";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Company_VerifyTheUserIsAbleToAddNewSectorOnTheCompanyPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC14212;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Handling salesforce Lightning
                login.HandleSalesforceLightningPage();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int userCount = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= userCount; row++)
                {
                    //Navigate to Coverage Sector Dependencies home page
                    companyDetail.NavigateToCoverageSectorDependenciesPage();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Coverage Sector Dependencies: Home ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " page is displayed ");

                    //Click New Coverage Sector Dependencies Button
                    coverageSectorDependenciesHome.ClickNewCoverageDependenciesButton();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Coverage Sector Dependency Edit: New Coverage Sector Dependency ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " page is displayed ");

                    //Create New coverage Sector Dependency
                    newCoverageSectorDependencies.CreateNewCoverageSectorDependency(fileTC14212);

                    //Fetch the Coverage Sector Dependency Name from its detail page
                    string coverageSectorDependencyName = coverageSectorDependenciesDetail.GetCoverageSectorDependencyName();
                    extentReports.CreateLog("Coverage Sector Dependency Name is: " + coverageSectorDependencyName + " ");

                    //Search user by global search
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    homePage.SearchUserByGlobalSearch(fileTC14212,user);
                    string userPeople = homePage.GetPeopleOrUserName();
                    Assert.AreEqual(user, userPeople);
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    string userName = login.ValidateUser();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains(userName), true);
                    switch (row)
                    {
                        case 2:
                            extentReports.CreateLog("Standard User: " + userName + " is able to login ");
                            break;
                        case 3:
                            extentReports.CreateLog("Marketing User: " + userName + " is able to login ");
                            break;
                        case 4:
                            extentReports.CreateLog("ACE Execution User: " + userName + " is able to login ");
                            break;
                        case 5:
                            extentReports.CreateLog("CF Financial User: " + userName + " is able to login ");
                            break;
                        case 6:
                            extentReports.CreateLog("CAO User: " + userName + " is able to login ");
                            break;
                        case 7:
                            extentReports.CreateLog("Business Operation User: " + userName + " is able to login ");
                            break;
                        case 8:
                            extentReports.CreateLog("System Administrator User: " + userName + " is able to login ");
                            break;
                    }

                    //Click Add Company button
                    companyHome.ClickAddCompany();
                    string companyRecordTypePage = companySelectRecord.GetCompanyRecordTypePageHeading();
                    Assert.AreEqual("Select Company Record Type", companyRecordTypePage);
                    extentReports.CreateLog("Page with heading: "+ companyRecordTypePage + " is displayed upon click add company button ");
                    
                    //Select company record type
                    string recordType = ReadExcelData.ReadData(excelPath, "Company", 1);
                    companySelectRecord.SelectCompanyRecordType(fileTC14212, recordType);
                    string createCompanyPage = createCompany.GetCreateCompanyPageHeading();
                    Assert.AreEqual("Company Create", createCompanyPage);
                    extentReports.CreateLog("Page with heading: "+ createCompanyPage + " is displayed upon selecting company record type ");

                    //Validate company type display as selected 
                    Assert.AreEqual(recordType, createCompany.GetSelectedCompanyType());
                    extentReports.CreateLog("Selected company type: " + createCompany.GetSelectedCompanyType() + " choosen on select company record type page is matching on Company create page ");
                    
                    //Create a company
                    createCompany.AddCompany(fileTC14212, 2);

                    //Validate company detail heading
                    string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                    string companyDetailHeadingExl = ReadExcelData.ReadData(excelPath, "Company", 8);
                    Assert.AreEqual(companyDetailHeadingExl, companyDetailHeading);
                    extentReports.CreateLog("Page with heading: "+companyDetailHeading + " is displayed upon adding company ");

                    //Validate company name value
                    string companyName = companyDetail.GetCompanyName();
                    string companyNameExl = ReadExcelData.ReadData(excelPath, "Company", 2);
                    Assert.AreEqual(companyNameExl, companyName);
                    extentReports.CreateLog("Company name: "+companyName + " in add company page matches on company details page ");

                    //Validate company type value
                    string CompanyType = companyDetail.GetCompanyType();
                    string companyTypeExl = ReadExcelData.ReadData(excelPath, "Company", 1);
                    Assert.AreEqual(companyTypeExl, CompanyType);
                    extentReports.CreateLog("Company Type: "+ CompanyType + " in add company page matches on company details page ");

                    //Verify if Company Sector Quick Link is displayed
                    Assert.IsTrue(companyDetail.VerifyIfCompanySectorQuickLinkIsDisplayed());
                    extentReports.CreateLog("Company Sector Quick Link is displayed on company details page. ");
                    
                    //Click on new Company Sector button
                    companyDetail.ClickNewCompanySectorButton();

                    //Validating Title of Company Sector Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Company Sector Edit: New Company Sector ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed. ");

                    //Select CoverageSectorDependency
                    companyDetail.SelectCoverageSectorDependency(coverageSectorDependencyName);

                    //Save new company sector details
                    companyDetail.SaveNewCompanySectorDetails();
                    string compSecName = companyDetail.GetCompanySectorName();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Company Sector: " + compSecName + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("Company Sector with name: " + compSecName + " is created successfully. ");

                    //Validating filters functionality is working properly on coverage sector dependency popup
                    Assert.IsTrue(companyDetail.VerifyFiltersFunctionalityOnCoverageSectorDependencyPopUp(fileTC14212, coverageSectorDependencyName));
                    extentReports.CreateLog("Filters functionality is working properly on coverage sector dependency popup. ");

                    //Save company sector details
                    companyDetail.SaveNewCompanySectorDetails();

                    //Navigate to company detail page from company sector detail page
                    companyDetail.NavigateToCompanyDetailPageFromCompanySectorDetailPage();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Company: " + companyName + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("User navigated back to company detail page. ");

                    //Verify if company sector is added successfully to a company or not
                    Assert.IsTrue(companyDetail.VerifyCompanySectorAddedToCompanyOrNot(compSecName));
                    extentReports.CreateLog("Company sector: " + compSecName + " is added successfully to a company: " + companyName + " ");

                    //Delete company sector
                    companyDetail.DeleteCompanySector();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Company Sectors: Home ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("Company Sector deleted successfully. ");
                    
                    usersLogin.UserLogOut();

                    //Delete Coverage Sector Dependency
                    companyDetail.NavigateToCoverageSectorDependenciesPage();
                    coverageSectorDependenciesHome.NavigateToCoverageSectorDependencyDetailPage(coverageSectorDependencyName);
                    coverageSectorDependenciesDetail.DeleteCoverageSectorDependency();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Coverage Sector Dependencies: Home ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("Coverage sector dependency deleted successfully. ");
                    
                    //Delete company
                    companyDetail.DeleteCompany(fileTC14212, CompanyType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("Created company is deleted successfully ");
                }

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
