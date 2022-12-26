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
    class TS13_VerifyContractDetailsFromEngagementWithNewCompany : BaseClass
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
        ContractDetailsPage contractDetail = new ContractDetailsPage();

        public static string ERPSectionWithNewCompany = "TS013_ERPInformationOfContractDetalPageWithNewCompany.xlsx";

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
                string excelPath = ReadJSONData.data.filePaths.testData + ERPSectionWithNewCompany;
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
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    homePage.SearchUserByGlobalSearch(ERPSectionWithNewCompany, user);
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
                    companySelectRecord.SelectCompanyRecordType(ERPSectionWithNewCompany, recordType);
                    string createCompanyPage = createCompany.GetCreateCompanyPageHeading();
                    Assert.AreEqual("Company Create", createCompanyPage);
                    extentReports.CreateLog("Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                    // Validate company type display as selected 
                    Assert.AreEqual(recordType, createCompany.GetSelectedCompanyType());
                    extentReports.CreateLog("Selected company type: " + createCompany.GetSelectedCompanyType() + " choosen on select company record type page is matching on Company create page ");

                    // Create a  company
                    createCompany.AddCompany(ERPSectionWithNewCompany, row);
                    CustomFunctions.TableauPopUp();
                    //Validate company detail heading
                    string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                    string companyDetailHeadingExl = ReadExcelData.ReadData(excelPath, "Company", 8);
                    Assert.AreEqual(companyDetailHeadingExl, companyDetailHeading);
                    extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon adding company ");

                    contactCreate.CreateContactFromCompany(ERPSectionWithNewCompany, row);
                    contactDetail.ClickCompanyName();

                    // Calling function ClickOpportunityButton to click on Add CF opportunity button
                    string CompanyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    string HLCompanyValue = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 33);
                    string OtherCompanyValue = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 34);
                    companyDetail.ClickOpportunityButton(ERPSectionWithNewCompany, CompanyType, HLCompanyValue, OtherCompanyValue);

                    //Pre -requisite to clear mandatory values on add opportunity page.
                    addOpportunity.ClearMandatoryValuesOnAddOpportunity();
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);

                    // Calling add opportunity function
                    string value = addOpportunity.AddOpportunities(valJobType, ERPSectionWithNewCompany, row);
                    Console.WriteLine("value : " + value);
                    extentReports.CreateLog("Opportunity is created succcessfully ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(ERPSectionWithNewCompany);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", row, 1);
                    //string valContact = valFirstName + " " + valLastName;
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);
                    addOpportunityContact.CreateContact(ERPSectionWithNewCompany, valContact, valRecordType, valContactType, row);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForConversion(ERPSectionWithNewCompany);
                    opportunityDetails.UpdateInternalTeamDetails(ERPSectionWithNewCompany);

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();

                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(ERPSectionWithNewCompany);
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
                    Thread.Sleep(3000);
                    //engagementDetails.EnterEngNumberAndSave();
                    string engNumber = engageDetail.GetEngagementNumber();
                    string erpSubmittedToSyncEngg = engageDetail.GetERPSubmittedToSync();
                    string erpLegalEntity = engageDetail.GetERPLegalEntity();
                    string erpBusinessUnitId = engageDetail.GetERPBusinessUnitId();
                    string erpBusinessUnit = engageDetail.GetERPBusinessUnit();
                    string erpProjectNumber = engageDetail.GetERPProjectNumber();
                    string erpId = engageDetail.GetERPId();

                    engageDetail.ClickContractLink(row);
                    driver.Navigate().Refresh();
                    Thread.Sleep(10000);
                    string erpSubmittedToSyncContract = contractDetail.GetERPSubmittedToSync();
                    //Assert.AreEqual(erpSubmittedToSyncEngg, erpSubmittedToSyncContract);
                    extentReports.CreateLog("Contract ERP Submitted To Sync DateTime stamp: " + erpSubmittedToSyncContract + " matched with time stamp of engagment ");

                    string erpLegalEntityContract = contractDetail.GetERPLegalEntity();
                    Assert.AreEqual(erpLegalEntity, erpLegalEntityContract);
                    extentReports.CreateLog("Contract ERP Legal Entity: " + erpLegalEntityContract + " matched with legal entity of engagement ");

                    string erpLegalEntityNameContract = contractDetail.GetERPLegalEntity();
                    Assert.AreEqual(erpLegalEntity, erpLegalEntityNameContract);
                    extentReports.CreateLog("Contract ERP Legal Entity Name: " + erpLegalEntityNameContract + " is displayed ");

                    string erpBusinessUnitContract = contractDetail.GetERPBusinessUnit();
                    Assert.AreEqual(erpBusinessUnit, erpBusinessUnitContract);
                    extentReports.CreateLog("Contract ERP Business Unit: " + erpBusinessUnitContract + " is matched with Business Unit of engagement ");

                    string erpBusinessUnitIdContract = contractDetail.GetERPBusinessUnitId();
                    Assert.AreEqual(erpBusinessUnitId, erpBusinessUnitIdContract);
                    extentReports.CreateLog("Contract ERP Business Unit Id: " + erpBusinessUnitContract + " is matched with Business Unit Id of engagement ");

                    string erpExpenditureTypeName = contractDetail.GetERPExpenditureTypeName();
                    Assert.AreEqual("65540-Other Seminars", erpExpenditureTypeName);
                    extentReports.CreateLog("Contract ERP Expenditure Type Name: " + erpExpenditureTypeName + " is displayed ");

                    string erpProjectNumberContract = contractDetail.GetERPProjectNumber();
                    Assert.AreEqual(erpProjectNumber, erpProjectNumberContract);
                    extentReports.CreateLog("Contract ERP Project Number: " + erpProjectNumberContract + " is matched with ERP Project Number of engagement ");

                    string erpOrganization = contractDetail.GetERPOrganization();
                    Assert.AreEqual(erpLegalEntity, erpOrganization);
                    extentReports.CreateLog("Contract ERP Organization: " + erpLegalEntity + " is displayed ");

                    string erpProjectId = contractDetail.GetERPProjectId();
                    Assert.AreEqual(erpId, erpProjectId);
                    extentReports.CreateLog("Contract ERP Project Id: " + erpProjectId + " is matched with ERP Id of engagement ");

                    //Validate ERP submitted to sync date
                    string cstDate = CustomFunctions.GetCurrentPSTDate();
                    string ERPSubmittedToSync = contractDetail.GetERPSubmittedToSync();
                    Assert.IsTrue(ERPSubmittedToSync.Contains(cstDate));
                    extentReports.CreateLog("ERP submitted to sync date is updated with latest time stamp ");

                    //Verify Last Integration status
                    string ERPLastIntegrationStatus = contractDetail.GetERPLastIntegrationStatus();
                    Assert.AreEqual("Success", ERPLastIntegrationStatus);
                    extentReports.CreateLog("ERP Last Integration Status: " + ERPLastIntegrationStatus + " is updated ");

                    engageDetail.ClickClientLinkForContract(row);

                    string erpOrgPartyId = companyDetail.GetERPOrgPartyId();
                    string erpBillToAddressId = companyDetail.GetERPBillToAddressId();
                    string erpContactId = companyDetail.GetERPContactId();
                    string erpShipToAddressId = companyDetail.GetERPShipToAddressId();
                    string clientNumber = companyDetail.GetClientNumber();

                    companyDetail.ClickPrimaryBillingContact(row);
                    string companyNameFromContact = contactDetail.GetCompanyName();
                    string contactName = contactDetail.GetContactName();

                    if (row.Equals(2))
                    {
                        CustomFunctions.SwitchToWindow(driver, 1);
                        driver.Navigate().Refresh();
                    }
                    else
                    {
                        CustomFunctions.SwitchToWindow(driver, 4);
                        driver.Navigate().Refresh();
                    }
                    string billTo = contractDetail.GetBillTo();
                    Assert.AreEqual(companyNameFromContact, billTo);
                    extentReports.CreateLog("Bill To: " + billTo + " matches with company of billing contact ");

                    string billingContact = contractDetail.GetBillingContact();
                    Assert.AreEqual(contactName, billingContact);
                    extentReports.CreateLog("Contract Billing Contact: " + billingContact + " matches with billing contact ");

                    string erpBillToCustomerId = contractDetail.GetBillToCustomerId();
                    Assert.AreEqual(erpOrgPartyId, erpBillToCustomerId);
                    extentReports.CreateLog("Contract Bill to Customer Id: " + erpBillToCustomerId + " matches with erp org party id of company ");

                    string contractErpBillToAddressId = contractDetail.GetERPBillToAddressId();
                    Assert.AreEqual(erpBillToAddressId, contractErpBillToAddressId);
                    extentReports.CreateLog("Contract Bill to Address Id: " + contractErpBillToAddressId + " matches with bill to address id of company ");

                    string contractErpBillToContactId = contractDetail.GetERPBillToContactId();
                    Assert.AreEqual(erpContactId, contractErpBillToContactId);
                    extentReports.CreateLog("Contract Bill to Contact Id: " + contractErpBillToContactId + " matches with bill to contact id of company ");

                    string contractErpShipToCustomerId = contractDetail.GetERPShipToCustomerId();
                    Assert.AreEqual(erpOrgPartyId, contractErpShipToCustomerId);
                    extentReports.CreateLog("Contract ship to customer Id: " + contractErpShipToCustomerId + " matches with erp org party id of ship to company ");

                    string contractErpShipToAddressId = contractDetail.GetERPShipToAddressId();
                    Assert.AreEqual(erpShipToAddressId, contractErpShipToAddressId);
                    extentReports.CreateLog("Contract ship to address Id: " + contractErpShipToAddressId + " matches with erp ship to address id of ship to company ");

                    string contractShipToAccountNumber = contractDetail.GetERPShipToAccountNumber();
                    Assert.AreEqual(clientNumber, contractShipToAccountNumber);
                    extentReports.CreateLog("Contract ERP ship to account number: " + contractShipToAccountNumber + " matches with client number of ship to company ");

                    usersLogin.UserLogOut();
                    extentReports.CreateLog("Standard user is logged out ");

                    companyHome.SearchCompany(ERPSectionWithNewCompany, CompanyType);
                    companyDetail.DeleteCompanyContract(ERPSectionWithNewCompany, CompanyType);
                   //Delete company

                   companyHome.SearchCompany(ERPSectionWithNewCompany, CompanyType);
                   companyDetail.DeleteCompany(ERPSectionWithNewCompany, CompanyType);

                }
                //Delete company

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