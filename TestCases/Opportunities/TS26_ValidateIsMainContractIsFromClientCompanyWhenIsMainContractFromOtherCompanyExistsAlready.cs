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
    class TS26_ValidateIsMainContractIsFromClientCompanyWhenIsMainContractFromOtherCompanyExistsAlready : BaseClass
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

        public static string ERP = "ERPPostCreationOfOpportunity.xlsx";

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

                //Logout of Standard user
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                
                //Search for added opportunity
                opportunityHome.SearchOpportunity(opportunityNumber);

                //Click on New Contract button
                string titleContract = opportunityDetails.ClickNewContract();
                Assert.AreEqual("New Contract",titleContract);
                extentReports.CreateLog("Page with title : " + titleContract+ " is displayed upon clicking on New Contract button ");

                //Add the contract by selecting any company other than Client Company
                string titleDetail =opportunityDetails.AddContractBySelectingACompany("Test Contract", valContact, "Test Enterprises, Inc.");
                Assert.AreEqual("Test Contract", titleDetail);
                extentReports.CreateLog("Contract with name: "+ titleDetail +" is added ");

                //Validate if Is Main Contract checkbox is checked
                string valueIsMain= opportunityDetails.ValidateIsMainContract();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueIsMain);
                extentReports.CreateLog(valueIsMain + " of added Contract even it was not selected while saving the details ");
                             
                //Add one more contract from client company
                opportunityDetails.ClickNewContract();
                string clientComp = ReadExcelData.ReadData(excelPath, "AddOpportunity", 1);
                string titleDetailIsMainTrue = opportunityDetails.AddContractBySelectingACompany("Additional Contract", valContact, clientComp);
                Assert.AreEqual("Additional Contract", titleDetailIsMainTrue);
                extentReports.CreateLog("New Contract: " + titleDetailIsMainTrue + " from client company is added ");

                //Validate if Is Main Contract checkbox is checked for new contract added selecting client company
                string valueNewIsMainCon = opportunityDetails.ValidateIsMainContractOfNewContract();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueNewIsMainCon);
                extentReports.CreateLog(valueNewIsMainCon + " for the contract added from client company ");

                //Validate if Is Main Contract checkbox is checked for previously added contract
                string valueNewIsMainPrev = opportunityDetails.ValidateIsMainContractOfOldContract();
                Assert.AreEqual("Is Main Contract checkbox is not checked", valueNewIsMainPrev);
                extentReports.CreateLog(valueNewIsMainPrev + " for the contract added earlier from company other than client company ");
                               
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

    

