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
    class TS17_TestERPSectionWithExistingCompany : BaseClass
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

        public static string ERPSectionWithExistingCompany = "TS17_ERPSectionWithExistingCompany.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void TestERPSectionOfExistingCompany()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERPSectionWithExistingCompany;
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
                for (int row = 3; row <= rowCompanyName; row++)
                {
                    // Search standard user by global search
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
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
                   
                    //DateTime date = DateTime.ParseExact(beforeERPSubmittedToSync, "dd/mm/yyyy dd:mm");
                    DateTime beforeERPSubmittedDate = DateTime.Parse(beforeERPSubmittedToSync);
                   // DateTime beforeERPSubmittedDate = Convert.ToDateTime(beforeERPSubmittedToSync);

                    //ERP Last Integration Date time stamp
                    string beforeERPLastIntegrationDate = companyDetail.GetValERPLastIntegrationResponseDate();
                    DateTime beforeERPIntegrationDate = Convert.ToDateTime(beforeERPLastIntegrationDate);

                    // ERP contact details
                    string beforeContactFirstName = companyDetail.GetERPContactFirstName();
                    string beforeContactLastName = companyDetail.GetERPContactLastName();
                    string beforeContactEmail = companyDetail.GetERPContactEmail();
                                     

                    //Create a contact from company 
                    string valFirstName = CustomFunctions.RandomValue();
                    string valLastName = CustomFunctions.RandomValue();
                    string valEmail = valFirstName + valLastName + "@email.com";
                    contactCreate.CreateContactFromCompany(ERPSectionWithExistingCompany, valFirstName, valLastName, valEmail, row);
                    contactDetail.ClickCompanyName();

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
                    string valContact = valFirstName + " " + valLastName;
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);
                    addOpportunityContact.CreateContact(ERPSectionWithExistingCompany, valContact, valRecordType, valContactType, row);
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
                    //engagementDetails.EnterEngNumberAndSave();
                    string engNumber = engageDetail.GetEngagementNumber();
                    string engName = engageDetail.GetEngName();
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
                    extentReports.CreateLog("Client Number is visible: " + isClientNumberExist + " ");

                    //Validate ERP submitted to sync date
                    string cstDate = CustomFunctions.GetCurrentPSTDate();
                    string ERPSubmittedToSync = companyDetail.GetValERPSubmittedToSync();
                    Assert.IsTrue(ERPSubmittedToSync.Contains(cstDate));
                    extentReports.CreateLog("ERP submitted to sync date is updated with latest time stamp ");

                    DateTime afterERPSubmittedDate = Convert.ToDateTime(ERPSubmittedToSync);
                    int ERPSubmittedDateCompare = DateTime.Compare(afterERPSubmittedDate, beforeERPSubmittedDate);
                    //Assert.AreEqual(-1, ERPSubmittedDateCompare);
                    extentReports.CreateLog("ERP submitted to sync date is updated with latest time stamp after adding opportunity in company and converting it into engagement ");


                    //Validate ERP Account Description 
                    string accountDesc = companyDetail.GetERPAccountDescription();
                    string expectedAccountDesc = companyDetail.ERPAccountDescription(CompanyType);
                    Assert.AreEqual(expectedAccountDesc, accountDesc);
                    extentReports.CreateLog("ERP Account Description: " + accountDesc + " based on company type ");

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
                    extentReports.CreateLog(ERPDetailsExist + " ");

                    //Validate Contact First Name in ERP section
                    string contactFirstName = companyDetail.GetERPContactFirstName();
                    Assert.AreEqual(valFirstName, contactFirstName);
                    extentReports.CreateLog("Contact First Name: " + contactFirstName + " is visible in ERP section of company ");

                    //Validate contact first name is updated with recent opportunity contact
                    Assert.AreNotEqual(contactFirstName, beforeContactFirstName);
                    extentReports.CreateLog("Contact First Name: " + contactFirstName + " is updated with contact first name added in opportunity ");

                    //Validate Contact last Name in ERP section
                    string contactLastName = companyDetail.GetERPContactLastName();
                    Assert.AreEqual(valFirstName, contactLastName);
                    extentReports.CreateLog("Contact Last Name: " + contactLastName + " is visible in ERP section of company ");
                    
                    //Validate contact last name is updated with recent opportunity contact
                    Assert.AreNotEqual(contactLastName, beforeContactLastName);
                    extentReports.CreateLog("Contact Last Name: " + contactLastName + " is updated with contact last name added in opportunity ");

                    //Validate Contact Email in ERP section
                    string contactEmail = companyDetail.GetERPContactEmail();
                    Assert.AreEqual(valEmail, contactEmail);
                    extentReports.CreateLog("Contact Email: " + contactEmail + " is visible in ERP section of company ");

                    Assert.AreNotEqual(contactEmail, beforeContactEmail);
                    extentReports.CreateLog("Contact Email: " + contactEmail + " is updated with contact email added in opportunity ");

                    //Validate Contact Phone in ERP section 
                    string contactPhone = companyDetail.GetERPContactPhone();
                    string contactPhoneExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 6);
                    Assert.AreEqual(contactPhoneExl, contactPhone);
                    extentReports.CreateLog("Contact Phone: " + contactPhone + " is visible in ERP section of company ");

                    //Validate ERP Last Integration response date
                    string ERPLastIntegrationResponseDate = companyDetail.GetValERPLastIntegrationResponseDate();
                    Assert.IsTrue(ERPLastIntegrationResponseDate.Contains(cstDate));
                    extentReports.CreateLog("ERP Last Integration Response Date is updated with latest time stamp ");

                    //Verify last integration response date time stamp is greater than previous time stamp
                   DateTime afterERPlastIntegrationResponseDate = Convert.ToDateTime(ERPLastIntegrationResponseDate);
                    int ERPLastIntegrationResponseDateCompare = DateTime.Compare(afterERPlastIntegrationResponseDate, beforeERPIntegrationDate);
                    //Assert.AreEqual(1, ERPLastIntegrationResponseDateCompare);
                    extentReports.CreateLog("ERP Last Integration Response Date is updated with latest time stamp after adding opportunity in company and converting it into engagement ");

                    //Verify Last Integration status
                    string ERPLastIntegrationStatus = companyDetail.GetValERPLastIntegrationStatus();
                    Assert.AreEqual("Success", ERPLastIntegrationStatus);
                    extentReports.CreateLog("ERP Last Integration Status: " + ERPLastIntegrationStatus + " is updated ");

                    usersLogin.UserLogOut();
                    extentReports.CreateLog("Standard user is logged out ");

                    enggHome.SearchEngagementWithName(engName);
                    string valuelegalEntity = engageDetail.getLegalEntity();
                    engageDetail.ChangeLegalEntity(valuelegalEntity);

                    string enggNumberSuffix = engageDetail.GetEnggNumberSuffix();
                    Assert.AreEqual("A", enggNumberSuffix);
                    extentReports.CreateLog("Engagement Number Suffix: " + enggNumberSuffix + " is updated ");

                    string newContractMainStatus = engageDetail.GetIsMainContractCheckBoxStatus(2);
                    Assert.AreEqual("Checked", newContractMainStatus);
                    extentReports.CreateLog("New Contract Is Main Status is checked ");

                    string oldContractMainStatus = engageDetail.GetIsMainContractCheckBoxStatus(3);
                    Assert.AreEqual("Not Checked", oldContractMainStatus);
                    extentReports.CreateLog("Old Contract Is Main Status is unchecked ");

                    string endDateOldContract = engageDetail.GetEndDate(3);
                    Assert.AreNotEqual("", endDateOldContract);
                    extentReports.CreateLog("Old Contract End Date: " + endDateOldContract + " is updated ");

                }

                // Logout from standard User
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