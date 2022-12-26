using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T1654AndT1655_Opportunity_AdditionalClient_CancelSaveEditDelete : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage oppHome = new OpportunityHomePage();
        OpportunityDetailsPage oppDetails = new OpportunityDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        AddCounterparty addCounterparty = new AddCounterparty();

        public static string fileTC1654 = "T1672AndT1673_AdditionalClient_CancelSaveEditDelete";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AdditionalClient_CancelSaveEditDelete()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1654;
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
                extentReports.CreateLog("Std User: " + stdUser + " is able to login ");

                //Search engagement with LOB - CF
                string message = oppHome.SearchOpportunity("Simple");
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matches to JobType are found and Opportunity Detail page is displayed ");

                //Get default Company Name of Additional Client
                string defaultCompany= oppDetails.GetCompanyNameOfAdditionalClient();              
                
                //Click on Additional Client button and validate the page
                string titlePage = oppDetails.ClickAdditionalClientButton();
                Assert.AreEqual("New Opportunity Client/Subject", titlePage);
                extentReports.CreateLog("Page with title: " + titlePage + " is displayed upon clicking Additional Client button ");

                //Validate values displayed in Type dropdown
                //Assert.IsTrue(oppDetails.VerifyTypeValues(), "Verified that displayed types are same");
                //extentReports.CreateLog("Displayed Type values are correct ");

                //Verify the Opportunity is populated as default
                string oppName = oppDetails.GetOpportunityFromClientSubject();
                Assert.AreEqual("+Simple", oppName);
                extentReports.CreateLog("Opportunity field is defaulted with value: " + oppName + " which is similar to Opportunity Name");

                //Verify Type is populated as Client
                string clientName = oppDetails.GetTypeFromClientSubject();
                Assert.AreEqual("Client", clientName);
                extentReports.CreateLog("Type is defaulted with value: " + clientName + " ");                               
                
                //Validate cancel functionality of Additional Client
                string name = ReadExcelData.ReadData(excelPath, "Client", 1); 
                string pageOpportunity = oppDetails.ValidateCancelFunctionalityOfAdditionalClient(name);
                Assert.AreEqual("Opportunity Detail", pageOpportunity);
                extentReports.CreateLog("Page with title: " + pageOpportunity + " is displayed upon clicking Cancel button ");

                //Validate that no record is addded upon clicking cancel button
                string company = oppDetails.ValidateIfNewRowGotAdded();
                Assert.AreEqual("Row does not exist", company);
                extentReports.CreateLog("No new record is added upon clicking Cancel button after entering all details ");

                //Validate Save functionality of Additional Client
                oppDetails.ClickAdditionalClientButton();
                string companyName = oppDetails.ValidateSaveFunctionalityOfAdditionalClient(name);
                string type = oppDetails.GetTypeOfAdditionalClient();
                Assert.AreEqual(name, companyName);
                extentReports.CreateLog("New record with company name: " + companyName + " with Type: "+type + " is added upon saving the details ");

                //Validate edit functionality of Additional Client   
                oppDetails.ValidateEditFunctionalityOfAdditionalClient();
                string updatedType = oppDetails.GetPrimaryCheckboxOfAdditionalClient();
                Assert.AreEqual("Checked", updatedType);
                extentReports.CreateLog("Type of added record is updated as : " + updatedType + " upon checking the Primary checkbox ");

                //Logout and update Primary checkbox
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                oppHome.SearchOpportunity("+Simple");
                oppDetails.ValidateEditFunctionalityOfAdditionalClient();
                string updatedPrimary = oppDetails.GetPrimaryCheckboxOfAdditionalClient();
                Assert.AreEqual("Not Checked", updatedPrimary);
                extentReports.CreateLog("Type of added record is updated as : " + updatedType + " upon unchecking the Primary checkbox ");
                
                //Validate delete functionality of Additional Client   
                string prevCompany = oppDetails.ValidateDeleteFunctionalityOfAdditionalClient();
                Assert.AreEqual(defaultCompany, prevCompany);
                extentReports.CreateLog("Added record with company name: "+ companyName+ " is not displayed anymore post deleting it. ");
                                 
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


