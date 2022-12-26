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

namespace SalesForce_Project.TestCases.Companies
{
    class TS16_TestERPSectionWithNewCompany : BaseClass
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

        public static string ERPSectionTestWithNewCompany = "TS16_ERPSectionWithNewCompany.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void TestERPSectionOfNewCompany()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERPSectionTestWithNewCompany;
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
                int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    // Search standard user by global search
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row,1);
                    homePage.SearchUserByGlobalSearch(ERPSectionTestWithNewCompany, user);
                    string userPeople = homePage.GetPeopleOrUserName();
                    string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    Assert.AreEqual(userPeopleExl, userPeople);
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    //Login as standard user
                    usersLogin.LoginAsSelectedUser();
                    string StandardUser = login.ValidateUser();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains(StandardUser), true);
                    extentReports.CreateLog("Standard User: " + StandardUser + " is able to login ");

                    // Click Add Company button
                    companyHome.ClickAddCompany();
                    string companyRecordTypePage = companySelectRecord.GetCompanyRecordTypePageHeading();
                    Assert.AreEqual("Select Company Record Type", companyRecordTypePage);
                    extentReports.CreateLog("Page with heading: " + companyRecordTypePage + " is displayed upon click add company button ");

                    // Select company record type
                    string recordType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companySelectRecord.SelectCompanyRecordType(ERPSectionTestWithNewCompany, recordType);
                    string createCompanyPage = createCompany.GetCreateCompanyPageHeading();
                    Assert.AreEqual("Company Create", createCompanyPage);
                    extentReports.CreateLog("Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                    // Validate company type display as selected 
                    Assert.AreEqual(recordType, createCompany.GetSelectedCompanyType());
                    extentReports.CreateLog("Selected company type: " + createCompany.GetSelectedCompanyType() + " choosen on select company record type page is matching on Company create page ");

                    // Create a  company
                    createCompany.AddCompany(ERPSectionTestWithNewCompany, row);

                    //Validate company detail heading
                    string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                    string companyDetailHeadingExl = ReadExcelData.ReadData(excelPath, "Company", 8);
                    Assert.AreEqual(companyDetailHeadingExl, companyDetailHeading);
                    extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon adding company ");

                    contactCreate.CreateContactFromCompany(ERPSectionTestWithNewCompany, row);
                    contactDetail.ClickCompanyName();

                    // Calling function ClickOpportunityButton to click on Add CF opportunity button
                    string CompanyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    string HLCompanyValue = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 33);
                    string OtherCompanyValue = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 34);
                    companyDetail.ClickOpportunityButton(ERPSectionTestWithNewCompany, CompanyType, HLCompanyValue, OtherCompanyValue);

                    //Pre -requisite to clear mandatory values on add opportunity page.
                    addOpportunity.ClearMandatoryValuesOnAddOpportunity();
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);

                    // Calling add opportunity function
                    string value = addOpportunity.AddOpportunities(valJobType, ERPSectionTestWithNewCompany, row);
                    Console.WriteLine("value : " + value);
                    extentReports.CreateLog("Opportunity is created succcessfully ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(ERPSectionTestWithNewCompany);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact",row, 1);
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);
                    addOpportunityContact.CreateContact(ERPSectionTestWithNewCompany, valContact, valRecordType, valContactType, 2);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForConversion(ERPSectionTestWithNewCompany);
                    opportunityDetails.UpdateInternalTeamDetails(ERPSectionTestWithNewCompany);

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();

                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(ERPSectionTestWithNewCompany);
                    extentReports.CreateLog("Conflict Check fields are updated ");

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 1));
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 1)), true);
                    extentReports.CreateLog("User: " + stdUser1 + " logged in ");

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
                    driver.Navigate().Refresh();
                    Thread.Sleep(6000);
                    //engagementDetails.EnterEngNumberAndSave();
                    string engNumber = engageDetail.GetEngagementNumber();

                    //Click on client link for company detail
                    engageDetail.ClickClientLink();
                    //Refresh the page 
                    driver.Navigate().Refresh();
                    Thread.Sleep(3000);
                    driver.Navigate().Refresh();

                    driver.Navigate().Refresh();

                    // Validate Client Number 
                    bool isClientNumberExist = companyDetail.IsClientNumberPresent();
                    Assert.IsTrue(isClientNumberExist);
                    extentReports.CreateLog("Client Number is visible: "+isClientNumberExist +" ");

                    //Validate ERP submitted to sync date
                    string cstDate = CustomFunctions.GetCurrentPSTDate();
                    string ERPSubmittedToSync = companyDetail.GetValERPSubmittedToSync();
                    //Assert.IsTrue(ERPSubmittedToSync.Contains(cstDate));
                    extentReports.CreateLog("ERP submitted to sync date is updated with latest time stamp ");

                    //Validate ERP Account Description 
                    string accountDesc = companyDetail.GetERPAccountDescription();
                    string expectedAccountDesc = companyDetail.ERPAccountDescription(CompanyType);
                    Assert.AreEqual(expectedAccountDesc, accountDesc);
                    extentReports.CreateLog("ERP Account Description: "+accountDesc +" based on company type ");

                    //Validate ERP customer type
                    string customerType = companyDetail.GetERPCustomerType();
                    string expectedCustomerType = companyDetail.ERPCustomerType(CompanyType);
                    Assert.AreEqual(expectedCustomerType, customerType);
                    extentReports.CreateLog("ERP Customer Type: " + customerType + " based on company type ");

                    //Validate ERP customer type description
                    string customerTypeDesc = companyDetail.GetERPCustomerTypeDescription();
                    string expectedCustomerTypeDesc = companyDetail.ERPCustomerTypeDescription(CompanyType);
                    Assert.AreEqual(expectedCustomerTypeDesc, customerTypeDesc);
                    extentReports.CreateLog("ERP Customer Type Description: " + customerTypeDesc + " based on company type ");

                    //Verify ERP Oracle fields populated or not
                    string ERPDetailsExist = companyDetail.CheckERPOracleDetailsExists();
                    Assert.AreEqual("ERP Oracle details exists", ERPDetailsExist);
                    extentReports.CreateLog(ERPDetailsExist+ " ");

                    //Validate Contact First Name in ERP section
                    string contactFirstName = companyDetail.GetERPContactFirstName();
                    string contactFirstNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                    Assert.AreEqual(contactFirstNameExl, contactFirstName);
                    extentReports.CreateLog("Contact First Name: " + contactFirstName + " is visible in ERP section of company ");

                    //Validate Contact last Name in ERP section
                    string contactLastName = companyDetail.GetERPContactLastName();
                    string contactLastNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row,3);
                    Assert.AreEqual(contactLastNameExl, contactLastName);
                    extentReports.CreateLog("Contact Last Name: " + contactLastName + " is visible in ERP section of company ");

                    //Validate Contact Email in ERP section
                    string contactEmail = companyDetail.GetERPContactEmail();
                    string contactEmailExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 5);
                    Assert.AreEqual(contactEmailExl, contactEmail);
                    extentReports.CreateLog("Contact Email: " + contactEmail + " is visible in ERP section of company ");

                    //Validate Contact Phone in ERP section 
                    string contactPhone = companyDetail.GetERPContactPhone();
                    string contactPhoneExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 6);
                    Assert.AreEqual(contactPhoneExl, contactPhone);
                    extentReports.CreateLog("Contact Phone: " + contactPhone + " is visible in ERP section of company ");

                    //Validate ERP Last Integration response date
                   string ERPLastIntegrationResponseDate = companyDetail.GetValERPLastIntegrationResponseDate();
                   // Assert.IsTrue(ERPLastIntegrationResponseDate.Contains(cstDate));
                    extentReports.CreateLog("ERP Last Integration Response Date is updated with latest time stamp ");

                    string ERPLastIntegrationStatus = companyDetail.GetValERPLastIntegrationStatus();
                    Assert.AreEqual("Success", ERPLastIntegrationStatus);
                    extentReports.CreateLog("ERP Last Integration Status: "+ERPLastIntegrationStatus+" is updated ");

                    usersLogin.UserLogOut();
                    //Delete contract
                    companyHome.SearchCompany(ERPSectionTestWithNewCompany, CompanyType);
                    companyDetail.DeleteCompanyContract(ERPSectionTestWithNewCompany, CompanyType);
                    //Delete company

                    companyHome.SearchCompany(ERPSectionTestWithNewCompany, CompanyType);
                    companyDetail.DeleteCompany(ERPSectionTestWithNewCompany, CompanyType);
                }
                // Logout from standard User
                usersLogin.UserLogOut();
                extentReports.CreateLog("Standard user is logged out ");
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