using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TMTT0014212_TMTT0014213_TMTT0014215_Opportunity_VerifyTheUserIsAbleToAddNewSectorOnTheOpportunityPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
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
        public void Opportunity_VerifyTheUserIsAbleToAddNewSectorOnTheOpportunityPage()
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
                    homePage.SearchUserByGlobalSearch(fileTC14212, user);
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

                    int rowCount = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                    for (int oppRow = 2; oppRow <= rowCount; oppRow++)
                    {
                        //Call function to open Add Opportunity Page & Select LOB
                        opportunityHome.ClickOpportunity();
                        string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", oppRow, 25);
                        Console.WriteLine("valRecordType:" + valRecordType);
                        opportunityHome.SelectLOBAndClickContinue(valRecordType);

                        //Validating Title of New Opportunity Page
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                        extentReports.CreateLog(driver.Title + " is displayed ");

                        //Calling AddOpportunities function                
                        string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", oppRow, 3);
                        string value = addOpportunity.AddOpportunities(valJobType, fileTC14212, oppRow);
                        extentReports.CreateLog("Required fields added in order to create an opportunity. ");

                        //Call function to enter Internal Team details
                        clientSubjectsPage.EnterStaffDetailsMultipleRows(fileTC14212, oppRow);
                        extentReports.CreateLog("HL member added to the deal team. ");

                        //Fetch values of Opportunity Number, Name
                        string oppNum = opportunityDetails.GetOppNumber();
                        string oppName = opportunityDetails.GetOpportunityName();

                        //Validating Opportunity created successfully
                        Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                        extentReports.CreateLog("Opportunity with LOB : " + valRecordType + " is created. Opportunity number is : " + oppNum + ". ");

                        //Verify if Opportunity Sector Quick Link is displayed
                        Assert.IsTrue(opportunityDetails.VerifyIfOpportunitySectorQuickLinkIsDisplayed());
                        extentReports.CreateLog("Opportunity Sector Quick Link is displayed on opportunity details page. ");
                        
                        //Create External Primary Contact         
                        string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                        string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                        addOpportunityContact.CreateContact(fileTC14212, valContact, valRecordType, valContactType);
                        extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                        //Click on new Opportunity Sector button
                        opportunityDetails.ClickNewOpportunitySectorButton();

                        //Validating Title of Opportunity Sector Page
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Sector Edit: New Opportunity Sector ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog(driver.Title + " is displayed. ");

                        //Select Coverage Sector Dependency
                        opportunityDetails.SelectCoverageSectorDependency(coverageSectorDependencyName);

                        //Save new Opportunity sector details
                        opportunityDetails.SaveNewOpportunitySectorDetails();
                        string oppSecName = opportunityDetails.GetOpportunitySectorName();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Sector: " + oppSecName + " ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("Opportunity Sector with name: " + oppSecName + " is created successfully. ");

                        //Validating filters functionality is working properly on coverage sector dependency popup
                        Assert.IsTrue(opportunityDetails.VerifyFiltersFunctionalityOnCoverageSectorDependencyPopUp(fileTC14212, coverageSectorDependencyName));
                        extentReports.CreateLog("Filters functionality is working properly on coverage sector dependency popup. ");

                        //Save Opportunity sector details
                        opportunityDetails.SaveNewOpportunitySectorDetails();

                        //Navigate to Opportunity detail page from Opportunity sector detail page
                        opportunityDetails.NavigateToOpportunityDetailPageFromOpportunitySectorDetailPage();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + oppName + " ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("User navigated back to Opportunity detail page. ");

                        //Verify if Opportunity sector is added successfully to a opportunity or not
                        Assert.IsTrue(opportunityDetails.VerifyOpportunitySectorAddedToOpportunityOrNot(oppSecName));
                        extentReports.CreateLog("Opportunity sector: " + oppSecName + " is added successfully to a Opportunity: " + oppName + " ");

                        //Delete Opportunity sector
                        opportunityDetails.DeleteOpportunitySector();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Sectors: Home ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("Opportunity Sector: " + oppSecName + " deleted successfully. ");
                        
                        usersLogin.UserLogOut();

                        //Search Opportunity
                        opportunityHome.SearchOpportunity(oppName);

                        //Delete Opportunity Internal Team
                        opportunityDetails.DeleteInternalTeamOfOpportunity();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("Opportunity internal team deleted successfully. ");

                        //Search Opportunity
                        opportunityHome.SearchOpportunity(oppName);

                        //Delete Opportunity
                        opportunityDetails.DeleteOpportunity();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("Opportunity: " + oppName + " deleted successfully. ");

                        if (oppRow < rowCount)
                        {
                            //Search user by global search
                            homePage.SearchUserByGlobalSearch(fileTC14212, user);
                            extentReports.CreateLog("User " + userPeople + " details are displayed ");

                            //Login user
                            usersLogin.LoginAsSelectedUser();
                            Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains(userName), true);
                        }
                    }

                    //Delete Coverage Sector Dependency
                    opportunityDetails.NavigateToCoverageSectorDependenciesPage();
                    coverageSectorDependenciesHome.NavigateToCoverageSectorDependencyDetailPage(coverageSectorDependencyName);
                    coverageSectorDependenciesDetail.DeleteCoverageSectorDependency();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Coverage Sector Dependencies: Home ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("Coverage sector dependency deleted successfully. ");
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


