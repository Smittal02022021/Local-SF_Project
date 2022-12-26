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

namespace SalesForce_Project.TestCases.ERPtoOracle
{
    class TS08_VerifyMultipleContractsCreationByMultipleBillingContactsInEngagement : BaseClass
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

        public static string MultipleContractCreation = "TS08_MultipleContractsInEngagement.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void TestMultipleContractsCreation()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + MultipleContractCreation;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                
                // Search standard user by global search
                string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearch(MultipleContractCreation, user);
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string StandardUser = login.ValidateUser();
                Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1).Contains(StandardUser), true);
                extentReports.CreateLog("Standard User: " + StandardUser + " is able to login ");

                string otherCompanyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2 + 2, 1);

                //string otherComanyType = ReadExcelData.ReadDataMultipleRows(excelPath, "OtherCompany", 2, 1);
                companyHome.SearchCompany(MultipleContractCreation, otherCompanyType);

                //Get contact name from other company
                string contactsName = companyDetail.GetContactNameUnderContactsSection();

                //Search 
                string CompanyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                companyHome.SearchCompany(MultipleContractCreation, CompanyType);

                string contactNameClientCompany = companyDetail.GetContactNameUnderContactsSection();
                //Get Details from ERP section before creating an opportunity and engagement conversion.
                // ERP Submitted to sync time stamp
                string beforeERPSubmittedToSync = companyDetail.GetValERPSubmittedToSync();

                // ERP contact details
                string beforeContactFirstName = companyDetail.GetERPContactFirstName();
                string beforeContactLastName = companyDetail.GetERPContactLastName();
                string beforeContactEmail = companyDetail.GetERPContactEmail();

                // Calling function ClickOpportunityButton to click on Add CF opportunity button
                string HLCompanyValue = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 33);
                string OtherCompanyValue = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 34);
                companyDetail.ClickOpportunityButton(MultipleContractCreation, CompanyType, HLCompanyValue, OtherCompanyValue);

                //Pre -requisite to clear mandatory values on add opportunity page.
                addOpportunity.ClearMandatoryValuesOnAddOpportunity();
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);

                // Calling add opportunity function
                string value = addOpportunity.AddOpportunities(valJobType, MultipleContractCreation, 2);
                Console.WriteLine("value : " + value);
                extentReports.CreateLog("Opportunity is created succcessfully ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(MultipleContractCreation);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact         
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                //string valContact = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 2, 1);
                //string valContact = valFirstName + " " + valLastName;
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 25);
                addOpportunityContact.CreateContact(MultipleContractCreation, contactNameClientCompany, valRecordType, valContactType, 2);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                addOpportunityContact.CreateContact(MultipleContractCreation, contactsName, valRecordType, valContactType, 2);

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForConversion(MultipleContractCreation);
                opportunityDetails.UpdateInternalTeamDetails(MultipleContractCreation);

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();

                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(MultipleContractCreation);
                extentReports.CreateLog("Conflict Check fields are updated ");

                //Login again as Standard User
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1));
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1)), true);
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
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2)), true);
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
                string erpSubmittedToSyncEngg = engageDetail.GetERPSubmittedToSync();
                string erpLegalEntity = engageDetail.GetERPLegalEntity();
                string erpBusinessUnitId = engageDetail.GetERPBusinessUnitId();
                string erpBusinessUnit = engageDetail.GetERPBusinessUnit();
                string erpProjectNumber = engageDetail.GetERPProjectNumber();
                
                string erpId = engageDetail.GetERPId();
                
                //Click primary contract link
                engageDetail.ClickSecondContractLink(2);

                string erpSubmittedToSyncContract = contractDetail.GetERPSubmittedToSync();
                Assert.That(erpSubmittedToSyncEngg, Is.LessThanOrEqualTo(erpSubmittedToSyncContract));
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

                engageDetail.ClickClientLinkForContract(2);

                string erpOrgPartyId = companyDetail.GetERPOrgPartyId();
                string erpBillToAddressId = companyDetail.GetERPBillToAddressId();
                string erpContactId = companyDetail.GetERPContactId();
                string erpShipToAddressId = companyDetail.GetERPShipToAddressId();
                string clientNumber = companyDetail.GetClientNumber();

                companyDetail.ClickPrimaryBillingContact(2);

                string companyNameFromContact = contactDetail.GetCompanyName();
                string contactName = contactDetail.GetContactName();

                //if (row.Equals(2))
                //{
                CustomFunctions.SwitchToWindow(driver, 1);


                string billTo = contractDetail.GetBillTo();
                Assert.AreEqual(companyNameFromContact, billTo);
                extentReports.CreateLog("Bill To: " + billTo + " matches with company of billing contact ");

                string billingContact = contractDetail.GetBillingContact();
                Assert.IsTrue(contactName.Contains(billingContact));
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

                CustomFunctions.SwitchToWindow(driver, 0);

                //Click on non primary contract
                engageDetail.ClickFirstContractLink(2);

                //Update ERP submitted to sync time stamp manually
                contractDetail.UpdateERPSubmittedToSyncManually();

                //Verify Last Integration status of not primary contract
                string ERPContractLastIntegrationStatus = contractDetail.GetERPLastIntegrationStatus();
                Assert.AreEqual("Success", ERPContractLastIntegrationStatus);
                extentReports.CreateLog("ERP Last Integration Status: " + ERPContractLastIntegrationStatus + " is updated ");

                //Verify last integration response date of non primary contract
                string ERPSubmittedToSyncContract = contractDetail.GetERPSubmittedToSync();
                string ERPLastIntegrationResponseDate = contractDetail.GetERPLastIntegrationResponseDate();
                Assert.That(ERPSubmittedToSyncContract, Is.LessThanOrEqualTo(ERPLastIntegrationResponseDate));
                extentReports.CreateLog("ERP Last Integration Response Date: " + ERPLastIntegrationResponseDate + " matched with time stamp of ERP submitted to sync time stamp ");

                usersLogin.UserLogOut();
                extentReports.CreateLog("Standard user is logged out ");

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