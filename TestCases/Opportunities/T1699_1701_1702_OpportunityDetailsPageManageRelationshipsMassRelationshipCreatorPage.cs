using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;


namespace SalesForce_Project.TestCases.Opportunity
{
    class T1699_1701_1702_OpportunityDetailsPageManageRelationshipsMassRelationshipCreatorPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        AddCounterparty addCounterparty = new AddCounterparty();
        MassRelationshipCreatorPage creatorPage = new MassRelationshipCreatorPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();


        public static string fileTC1702 = "T1702ManageRelationshipsMassRelationshipCreatorPage.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ManageRelationshipsMassRelationshipCreator()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1702;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser1 + " is able to login ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunities(valJobType, fileTC1702);                
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1702);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Call function to update HL -Internal Team details, Est Fees values and other required details
                opportunityDetails.UpdateInternalTeamDetails(fileTC1702);
                opportunityDetails.AddEstFeesWithAdmin(fileTC1702);
                opportunityDetails.UpdateFieldsForConversion(fileTC1702);
                extentReports.CreateLog("All required details are updated in Opportunity details ");

                //Create External Primary Contact         
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(fileTC1702, valContact, valRecordType, valContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileTC1702);
                //opportunityDetails.UpdateNBCApproval();
                extentReports.CreateLog("Conflict Check and NBC fields are updated ");

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
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Approve the Opportunity 
                opportunityDetails.ClickApproveButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog("Opportunity is approved ");

                //Log out of CAO User
                usersLogin.UserLogOut();

                //Login again as Standard User
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser3 = login.ValidateUser();
                Assert.AreEqual(stdUser3.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser3 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Check Record type and click Counterparties
                string message = addCounterparty.ValidateAndAddCounterparties();
                Assert.AreEqual("Selected records were added successfully", message);
                extentReports.CreateLog(message + " ");

                //Validating Edit Counterparty Records page and add CounterParty contact
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "List Member Edit ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                string valCPContact = ReadExcelData.ReadData(excelPath, "AddCPContact", 1);
                addCounterparty.AddCounterpartyContact(valCPContact);
                string msgCPContact = addCounterparty.ValidateCPContact();
                Assert.AreEqual("CounterParty Contact added", msgCPContact);
                extentReports.CreateLog(msgCPContact + " ");

                //Click Opportunity, add Opportunity Client Contact and validate
                addCounterparty.ClickOpp();
                string valClientContactType = ReadExcelData.ReadData(excelPath, "AddContact", 7);
                string valclientContact = ReadExcelData.ReadData(excelPath, "AddContact", 6);
                addOpportunityContact.CreateContact(fileTC1702, valclientContact, valRecordType, valClientContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valClientContactType + " Opportunity contact is saved ");

                //search for created opportunity
                 opportunityHome.SearchOpportunity(value);

                //Validate Manage Relationship and validate table headers 
                creatorPage.ClickManageRelationships();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog("Mass Relationship Creator Page is displayed ");
                string valHeaders = creatorPage.ValidateContactsTableHeaders(ReadExcelData.ReadData(excelPath, "AddContact", 8));
                Assert.AreEqual("Contact Name Company Appear On Strength Rating Sync", valHeaders);
                extentReports.CreateLog("Headers: " + valHeaders + " are displayed ");

                //Validate default checked radio button and contacts displayed
                bool btnAllContacts = creatorPage.ValidateRadiobutton();
                Assert.IsTrue(btnAllContacts);
                extentReports.CreateLog("All Contacts radio button is selected " + btnAllContacts + " ");
                string valContactNames = creatorPage.ValidateAllContacts();
                Assert.AreEqual(valCPContact + " " + valContact + " " + valclientContact, valContactNames);
                extentReports.CreateLog("Contact Names: " + valContactNames + " are displayed ");

                //Update the Strength Rating  
                creatorPage.UpdateRating();
                extentReports.CreateLog("Strength ratings are updated ");

                //Get column name and validate sorting of columns
                IWebElement colName = creatorPage.GetColName();
                string descResult = creatorPage.ValidateSorting(colName, "Descending");
                Assert.AreEqual("True", descResult);
                extentReports.CreateLog("Contact Name column is sorted in descending order " + descResult + " ");

                string ascResult = creatorPage.ValidateSorting(colName, "Ascending");
                Assert.AreEqual("True", ascResult);
                extentReports.CreateLog("Contact Name column is sorted in ascending order " + ascResult + " ");

                //Validate details by selecting External Team, Client Team and CP Contact
                creatorPage.ClickExternalTeam();
                string extContactName = creatorPage.ValidateContactName();
                Assert.AreEqual(valContact, extContactName);
                extentReports.CreateLog("External Contact: " + extContactName + " is displayed ");
                string extRating = creatorPage.ValidateRatings();
                Assert.AreEqual("Low", extRating);
                extentReports.CreateLog("External Contact's ratings " + extRating + " is displayed ");

                creatorPage.ClickClientTeam();
                string clientContactName = creatorPage.ValidateContactName();
                Assert.AreEqual(valclientContact, clientContactName);
                extentReports.CreateLog("Client Contact: " + clientContactName + " is displayed ");
                string clientRating = creatorPage.ValidateRatings();
                Assert.AreEqual("Low", clientRating);
                extentReports.CreateLog("Client Contact's ratings " + clientRating + " is displayed ");

                creatorPage.ClickCPContacts();
                string cpContactName = creatorPage.ValidateContactName();
                Assert.AreEqual(valCPContact, cpContactName);
                extentReports.CreateLog("Client Contact: " + cpContactName + " is displayed ");
                string cpRating = creatorPage.ValidateRatings();
                Assert.AreEqual("Low", cpRating);
                extentReports.CreateLog("CP Contact's ratings " + cpRating + " is displayed ");

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


