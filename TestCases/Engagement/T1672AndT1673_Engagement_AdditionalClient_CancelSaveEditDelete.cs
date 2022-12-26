using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Engagement
{
    class T1672AndT1673_Engagement_AdditionalClient_CancelSaveEditDelete : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        AddCounterparty addCounterparty = new AddCounterparty();

        public static string fileTC1720 = "T1672AndT1673_AdditionalClient_CancelSaveEditDelete";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1720;
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
                string message = engHome.SearchEngagementWithName("Project Ryder");
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matches to JobType are found and Engagement Detail page is displayed ");

                //Get default Company Name of Additional Client
                string defaultCompany= engagementDetails.GetCompanyNameOfAdditionalClient();              
                
                //Click on Additional Client button and validate the page
                string titlePage = engagementDetails.ClickAdditionalClientButton();
                Assert.AreEqual("New Engagement Client/Subject", titlePage);
                extentReports.CreateLog("Page with title: " + titlePage + " is displayed upon clicking Additional Client button ");

                //Validate values displayed in Type dropdown
                Assert.IsTrue(engagementDetails.VerifyTypeValues(), "Verified that displayed types are same");
                extentReports.CreateLog("Displayed Type values are correct ");

                //Verify the engagement is populated as default
                string engName = engagementDetails.GetEngagementFromClientSubject();
                Assert.AreEqual("Project Ryder", engName);
                extentReports.CreateLog("Engagement field is defaulted with value: " + engName + " which is similar to Engagement Name");

                //Verify Type is populated as Client
                string clientName = engagementDetails.GetTypeFromClientSubject();
                Assert.AreEqual("Client", clientName);
                extentReports.CreateLog("Type is defaulted with value: " + clientName + " ");                               
                
                //Validate cancel functionality of Additional Client
                string name = ReadExcelData.ReadData(excelPath, "Client", 1); ;
                string pageEngagement = engagementDetails.ValidateCancelFunctionalityOfAdditionalClient(name);
                Assert.AreEqual("Engagement Detail", pageEngagement);
                extentReports.CreateLog("Page with title: " + pageEngagement + " is displayed upon clicking Cancel button ");

                //Validate that no record is addded upon clicking cancel button
                string company = engagementDetails.GetCompanyNameOfAdditionalClient();
                Assert.AreEqual(defaultCompany, company);
                extentReports.CreateLog("No new record is added upon clicking Cancel button after entering all details ");

                //Validate Save functionality of Additional Client
                engagementDetails.ClickAdditionalClientButton();
                string companyName = engagementDetails.ValidateSaveFunctionalityOfAdditionalClient(name);
                string type = engagementDetails.GetTypeOfAdditionalClient();
                Assert.AreEqual(name, companyName);
                extentReports.CreateLog("New record with company name: " + companyName + " with Type: "+type + "is added upon saving the details ");

                //Validate edit functionality of Additional Client   
                engagementDetails.ValidateEditFunctionalityOfAdditionalClient();
                string updatedType = engagementDetails.GetTypeOfAdditionalClient();
                Assert.AreEqual("Subject", updatedType);
                extentReports.CreateLog("Type of added record is updated as : " + updatedType + " upon editing the details ");

                //Log out and validate admin user
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                engHome.SearchEngagementWithName("Project Ryder");

                //Validate delete functionality of Additional Client   
                string prevCompany = engagementDetails.ValidateDeleteFunctionalityOfAdditionalClient();
                Assert.AreEqual(defaultCompany, prevCompany);
                extentReports.CreateLog("Added record with company name: "+ companyName+ " is not displayed anymore post deleting it. ");
                                   
               //usersLogin.UserLogOut();
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


