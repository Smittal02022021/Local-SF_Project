using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TS25_ValidateAllContractsCreatedInOpportunityAreDisplayedInEngagementUponConversion : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();

        public static string ERP = "ERPPostCreationOfOpportunity1.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateIsMainContractCheckboxIsTRUE()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERP;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                 Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                 extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                              
                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");
                
                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunities(valJobType, ERP);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(ERP);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Create External Primary Contact         
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(ERP, valContact, valRecordType, valContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                //Logout of Standard user and login as CAO user
                usersLogin.UserLogOut();
                string valcaoUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valcaoUser);
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(valcaoUser), true);
                extentReports.CreateLog("CAO User: " + caoUser + " is able to login ");

                //Search for added opportunity
                opportunityHome.SearchOpportunity(opportunityNumber);

                //Click on New Contract button
                string titleContract = opportunityDetails.ClickNewContract();
                Assert.AreEqual("New Contract",titleContract);
                extentReports.CreateLog("Page with title : " + titleContract+ " is displayed upon clicking on New Contract button ");

                //Add the contract by not selecting "Is Main Contract" option
                string titleDetail =opportunityDetails.AddContractByNotSelectingIsMainContract("Test Contract", valContact);
                Assert.AreEqual("Test Contract", titleDetail);
                extentReports.CreateLog("Contract with name: "+ titleDetail +" is added ");

                //Validate if Is Main Contract checkbox is checked
                string valueIsMain= opportunityDetails.ValidateIsMainContract();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueIsMain);
                extentReports.CreateLog(valueIsMain + " of added Contract even it was not selected while saving the details ");
                             
                //Add one more contract by selecting Is Main Contract checkbox
                opportunityDetails.ClickNewContract();
                string titleDetailIsMainTrue = opportunityDetails.AddContractBySelectingIsMainContract("Additional Contract", valContact);
                Assert.AreEqual("Additional Contract", titleDetailIsMainTrue);
                extentReports.CreateLog("New Contract with name: " + titleDetailIsMainTrue + " is added ");

                //Validate if Is Main Contract checkbox is checked for new contract
                string valueNewIsMainCon = opportunityDetails.ValidateIsMainContractOfNewContract();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueNewIsMainCon);
                extentReports.CreateLog(valueNewIsMainCon + " for newly added contract ");

                //Logout of CAO user and login as Standard user
                usersLogin.UserLogOut();                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser1 + " logged in again ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityHome.SearchOpportunity(opportunityNumber);
                opportunityDetails.UpdateReqFieldsForCFConversion(ERP);
                opportunityDetails.UpdateInternalTeamDetails(ERP);

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(ERP);
                opportunityDetails.UpdateNBCApproval();
                extentReports.CreateLog("Conflict Check and NBC approval fields are updated ");

                //Login again as Standard User
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser2 = login.ValidateUser();
                Assert.AreEqual(stdUser2.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser2 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Requesting for engagement and validate the success message
                string msgSuccess = opportunityDetails.ClickRequestEng();
                Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                //Log out of Standard User
                usersLogin.UserLogOut();

                //Login as CAO user to approve the Opportunity
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                string caoUser1 = login.ValidateUser();
                Assert.AreEqual(caoUser1.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser1 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Approve the Opportunity 
                opportunityDetails.ClickApproveButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog("Opportunity is approved ");

                //Calling function to convert to Engagement
                opportunityDetails.ClickConvertToEng();

                //Validate the Engagement name in Engagement details page
                string engName = engagementDetails.GetEngName();
                Assert.AreEqual(opportunityNumber, engName);
                extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");

                //Validate that contracts added in Opportunity are displayed in Engagement as well
                string contract1 = engagementDetails.Get1stContractName();
                Assert.AreEqual(titleDetailIsMainTrue, contract1);

                string contract2 = engagementDetails.Get2ndContractName();
                Assert.AreEqual(titleDetail, contract2);
                extentReports.CreateLog(contract1 + " and " +contract2 + " added in Opportunity, are displayed in engagement details page ");

                //Validate Is Main checkbox checked for same contract as it is in Opportunity page
                string IsMainEng = engagementDetails.GetIfIsMainContractChecked();
                Assert.AreEqual("Is Main Contract checkbox is checked", IsMainEng);
                extentReports.CreateLog(valueNewIsMainCon + " for 2nd contract added in Opportunity ");

                //Click on Related Opportunity link and validate that added contracts are displayed post conversion to engagement as well
                string titleOpp = engagementDetails.ClickRelatedOpportunityLink();
                Assert.AreEqual("Opportunity", titleOpp);
                extentReports.CreateLog("Page with title: " + titleOpp +" is displayed upon clicking related Opportunity link in engagement detail page ");

                //Validate that contracts are still displayed in Opportunity post conversion to engagement
                string oppContract1 = opportunityDetails.Get1stContractName();
                Assert.AreEqual(titleDetailIsMainTrue, oppContract1);

                string oppContract2 = opportunityDetails.Get2ndContractName();
                Assert.AreEqual(titleDetail, oppContract2);
                extentReports.CreateLog(oppContract1 + " and " + oppContract2 + " are displayed in Opportunity post conversion to engagement ");
                
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }        
    }
}

    

