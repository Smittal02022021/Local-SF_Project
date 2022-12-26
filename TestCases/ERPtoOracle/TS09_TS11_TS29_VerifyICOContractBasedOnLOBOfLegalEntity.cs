using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.ERPtoOracle
{

    public class TS09_TS11_TS29_VerifyICOContractBasedOnLOBOfLegalEntity : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage oppDetails = new OpportunityDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        ContactCreatePage contactCreate = new ContactCreatePage();
        ContactDetailsPage contactDetail = new ContactDetailsPage();
        EngagementDetailsPage engageDetail = new EngagementDetailsPage();
        EngagementHomePage enggHome = new EngagementHomePage();

        public static string ERPSectionWithExistingCompany = "TS09_VerifyICOContractBasedOnLOBOfLegalEntity.xlsx";
        public static string xlPath = "TS09_EngagementDetails.xlsx";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void TestICOContractExistingCompany()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERPSectionWithExistingCompany;
                Console.WriteLine(excelPath);

                string enggExcelPath = ReadJSONData.data.filePaths.testData + xlPath;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    // Search standard user by global search
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    //ReadExcelData.SetCellData(enggExcelPath, "EngagementNames", "EnggNames", 2, user);

                    homePage.SearchUserByGlobalSearch(ERPSectionWithExistingCompany, user);
                    string userPeople = homePage.GetPeopleOrUserName();
                    string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    Assert.AreEqual(userPeopleExl, userPeople);
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    //Login as standard user
                    usersLogin.LoginAsSelectedUser();
                    string StandardUser = login.ValidateUser();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains(StandardUser), true);
                    extentReports.CreateLog("Standard User: " + StandardUser + " is able to login ");

                    string CompanyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companyHome.SearchCompany(ERPSectionWithExistingCompany, CompanyType);

                    //Get Details from ERP section before creating an opportunity and engagement conversion.
                    // ERP Submitted to sync time stamp
                    string beforeERPSubmittedToSync = companyDetail.GetValERPSubmittedToSync();
                    extentReports.CreateLog("beforeERPSubmittedToSync" + beforeERPSubmittedToSync);
                   DateTime beforeERPSubmittedDate = Convert.ToDateTime(beforeERPSubmittedToSync);

                   //ERP Last Integration Date time stamp
                   string beforeERPLastIntegrationDate = companyDetail.GetValERPLastIntegrationResponseDate();
                    extentReports.CreateLog("beforeERPLastIntegrationDate" + beforeERPLastIntegrationDate);
                   DateTime beforeERPIntegrationDate = Convert.ToDateTime(beforeERPLastIntegrationDate);

                    // ERP contact details
                    string beforeContactFirstName = companyDetail.GetERPContactFirstName();
                    string beforeContactLastName = companyDetail.GetERPContactLastName();
                    string beforeContactEmail = companyDetail.GetERPContactEmail();


                    string valContact = companyDetail.GetSecondContactNameUnderContactsSection();
                    // Calling function ClickOpportunityButton to click on Add CF opportunity button
                    string HLCompanyValue = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 33);
                    string OtherCompanyValue = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 34);
                    companyDetail.ClickOpportunityButton(ERPSectionWithExistingCompany, CompanyType, HLCompanyValue, OtherCompanyValue);

                    //Pre -requisite to clear mandatory values on add opportunity page.
                    addOpportunity.ClearMandatoryValuesOnAddOpportunity();
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);

                    // Calling add opportunity function
                    string value = addOpportunity.AddOpportunities(valJobType, ERPSectionWithExistingCompany, row);
                    Console.WriteLine("value : " + value);
                    extentReports.CreateLog("Opportunity is created succcessfully ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(ERPSectionWithExistingCompany);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    //string valContact = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 2, 1);
                    // string valContact = valFirstName + " " + valLastName;
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);
                    addOpportunityContact.CreateContact(ERPSectionWithExistingCompany, valContact, valRecordType, valContactType, 2);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForConversion(ERPSectionWithExistingCompany);
                    opportunityDetails.UpdateInternalTeamDetails(ERPSectionWithExistingCompany);

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();

                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(ERPSectionWithExistingCompany);
                    extentReports.CreateLog("Conflict Check fields are updated ");
                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1));
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1)), true);
                    extentReports.CreateLog("User: " + stdUser1 + " logged in ");
                    Thread.Sleep(2000);
                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Requesting for engagement and validate the success message
                    string msgSuccess = opportunityDetails.ClickRequestEng();
                    Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                    //Log out of Standard User
                    usersLogin.UserLogOut();

                    //Login as CAO user to approve the Opportunity
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2));
                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2)), true);
                    extentReports.CreateLog("User: " + caoUser + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Approve the Opportunity 
                    opportunityDetails.ClickApproveButton();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog("Opportunity is approved ");

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEng();
                    string engName = engageDetail.GetEngName();
                    string engNumber = engageDetail.GetEngagementNumber();
                    ReadExcelData.SetCellData(enggExcelPath, "EngagementNames", "EnggNames", row, engName);



                   
                    companyHome.SearchCompany(ERPSectionWithExistingCompany, CompanyType);

                    //Get Details from ERP section before creating an opportunity and engagement conversion.
                    // ERP Submitted to sync time stamp
                    string beforeERPSubmittedToSync1 = companyDetail.GetValERPSubmittedToSync();
                    extentReports.CreateLog("beforeERPSubmittedToSync1" + beforeERPSubmittedToSync1);
                   

                    //ERP Last Integration Date time stamp
                    string beforeERPLastIntegrationDate2 = companyDetail.GetValERPLastIntegrationResponseDate();
                    extentReports.CreateLog("beforeERPLastIntegrationDate2" + beforeERPLastIntegrationDate2);
                   
                    // Logout from standard User
                    usersLogin.UserLogOut();                                      
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