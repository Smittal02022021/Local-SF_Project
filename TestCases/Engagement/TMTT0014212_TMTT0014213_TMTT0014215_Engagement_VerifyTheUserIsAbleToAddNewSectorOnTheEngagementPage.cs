using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Engagement
{
    class TMTT0014212_TMTT0014213_TMTT0014215_Engagement_VerifyTheUserIsAbleToAddNewSectorOnTheEngagementPage : BaseClass
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
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
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
        public void Engagement_VerifyTheUserIsAbleToAddNewSectorOnTheEngagementPage()
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
                        clientSubjectsPage.EnterMultipleStaffDetails(fileTC14212, oppRow, row, valRecordType);
                        extentReports.CreateLog("HL members added to the deal team. ");

                        //Fetch values of Opportunity Number, Name
                        string oppNum = opportunityDetails.GetOppNumber();
                        string oppName = opportunityDetails.GetOpportunityName();

                        //Validating Opportunity created successfully
                        Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                        extentReports.CreateLog("Opportunity with LOB : " + valRecordType + " is created. Opportunity number is : " + oppNum + ". ");

                        //Create External Primary Contact         
                        string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                        string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                        addOpportunityContact.CreateContact(fileTC14212, valContact, valRecordType, valContactType);
                        extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                        //Update required Opportunity fields for conversion and Internal team details
                        if (valRecordType == "CF")
                        {
                            opportunityDetails.UpdateReqFieldsForCFConversionMultipleRows(fileTC14212, oppRow);
                            extentReports.CreateLog("Fields required for converting CF opportunity to engagement are updated. ");
                        }
                        else if (valRecordType == "FVA")
                        {
                            if(row==3)
                            {
                                opportunityDetails.UpdateReqFieldsForFVAConversionMultipleRows1(fileTC14212, oppRow);
                                extentReports.CreateLog("Fields required for converting FVA opportunity to engagement are updated. ");
                            }
                            else
                            {
                                opportunityDetails.UpdateReqFieldsForFVAConversionMultipleRows(fileTC14212, oppRow);
                                extentReports.CreateLog("Fields required for converting FVA opportunity to engagement are updated. ");
                            }
                        }
                        else
                        {
                            opportunityDetails.UpdateReqFieldsForFRConversionMultipleRows(fileTC14212, oppRow);
                            extentReports.CreateLog("Fields required for converting FR opportunity to engagement are updated. ");
                        }

                        //Update internal team details
                        opportunityDetails.UpdateInternalTeamDetails(fileTC14212);
                        extentReports.CreateLog("Deal team member roles are updated. ");

                        //Log out from standard User
                        usersLogin.UserLogOut();

                        //Search for created opportunity
                        opportunityHome.SearchOpportunity(oppName);

                        if(row==3 && valRecordType == "FVA")
                        {
                            opportunityDetails.UpdateTombstonePermissionField();
                        }

                        //update CC and NBC checkboxes 
                        opportunityDetails.UpdateOutcomeDetails(fileTC14212);
                        if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                        {
                            opportunityDetails.UpdateNBCApproval();
                            extentReports.CreateLog("Conflict Check and NBC fields are updated ");
                        }
                        else
                        {
                            extentReports.CreateLog("Conflict Check fields are updated ");
                        }

                        //Update Client and Subject to Accupac bypass EBITDA field validation for JobType- Sellside
                        if (valJobType.Equals("Sellside"))
                        {
                            opportunityDetails.UpdateClientandSubject("Accupac");
                            extentReports.CreateLog("Updated Client and Subject fields ");
                        }
                        else
                        {
                            Console.WriteLine("Not required to update ");
                        }

                        //Login again as Standard User
                        homePage.SearchUserByGlobalSearch(fileTC14212, user);
                        usersLogin.LoginAsSelectedUser();
                        string stdUser1 = login.ValidateUser();
                        Assert.AreEqual(stdUser1.Contains(user), true);
                        extentReports.CreateLog("Standard User: " + stdUser1 + " logged in ");

                        //Search for created opportunity
                        opportunityHome.SearchOpportunity(value);

                        //Requesting for engagement and validate the success message
                        string msgSuccess = opportunityDetails.ClickRequestEng();
                        Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                        extentReports.CreateLog("Request for Engagement success message: " + msgSuccess + " is displayed. ");

                        //Log out of Standard User
                        usersLogin.UserLogOut();

                        //Login as CAO user to approve the Opportunity
                        homePage.SearchUserByGlobalSearch(fileTC14212, ReadExcelData.ReadDataMultipleRows(excelPath, "DealTeamMembers", oppRow, 2));
                        usersLogin.LoginAsSelectedUser();
                        string caoUser = login.ValidateUser();
                        Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "DealTeamMembers", oppRow, 2)), true);
                        extentReports.CreateLog("CAO User: " + caoUser + " logged in ");

                        //Search for created opportunity
                        opportunityHome.SearchOpportunity(value);

                        //Approve the Opportunity 
                        opportunityDetails.ClickApproveButton();
                        extentReports.CreateLog("Opportunity number: " + oppNum + " is approved. ");

                        //Calling function to convert to Engagement
                        opportunityDetails.ClickConvertToEng();
                        extentReports.CreateLog("Opportunity number: " + oppNum + " is successfuly converted into Engagement. ");

                        usersLogin.UserLogOut();

                        //Search user by global search
                        homePage.SearchUserByGlobalSearch(fileTC14212, user);
                        extentReports.CreateLog("User " + userPeople + " details are displayed ");

                        //Login user
                        usersLogin.LoginAsSelectedUser();
                        Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains(userName), true);

                        //Search for created engagement
                        engagementHome.SearchEngagementWithName(oppName);

                        //Verify if Engagement Sector Quick Link is displayed
                        Assert.IsTrue(engagementDetails.VerifyIfEngagementSectorQuickLinkIsDisplayed());
                        extentReports.CreateLog("Engagement Sector Quick Link is displayed on engagement details page. ");

                        //Click on new Engagement Sector button
                        engagementDetails.ClickNewEngagementSectorButton();

                        //Validating Title of Engagement Sector Page
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Engagement Sector Edit: New Engagement Sector ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog(driver.Title + " is displayed. ");

                        //Select Coverage Sector Dependency
                        engagementDetails.SelectCoverageSectorDependency(coverageSectorDependencyName);

                        //Save new Engagement sector details
                        engagementDetails.SaveNewEngagementSectorDetails();
                        string engSecName = engagementDetails.GetEngagementSectorName();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Engagement Sector: " + engSecName + " ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("Engagement Sector with name: " + engSecName + " is created successfully. ");

                        //Validating filters functionality is working properly on coverage sector dependency popup
                        Assert.IsTrue(engagementDetails.VerifyFiltersFunctionalityOnCoverageSectorDependencyPopUp(fileTC14212, coverageSectorDependencyName));
                        extentReports.CreateLog("Filters functionality is working properly on coverage sector dependency popup. ");

                        //Save Engagement sector details
                        engagementDetails.SaveNewEngagementSectorDetails();

                        //Navigate to Engagement detail page from Engagement sector detail page
                        engagementDetails.NavigateToEngagementDetailPageFromEngagementSectorDetailPage();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Engagement: " + oppName + " ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("User navigated back to Engagement detail page. ");

                        //Verify if Engagement sector is added successfully to a Engagement or not
                        Assert.IsTrue(engagementDetails.VerifyEngagementSectorAddedToEngagementOrNot(engSecName));
                        extentReports.CreateLog("Engagement sector: " + engSecName + " is added successfully to a Engagement: " + oppName + " ");

                        //Delete Engagement sector
                        engagementDetails.DeleteEngagementSector();
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Engagement Sectors: Home ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("Engagement Sector: " + engSecName + " deleted successfully. ");
                    }

                    usersLogin.UserLogOut();

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


