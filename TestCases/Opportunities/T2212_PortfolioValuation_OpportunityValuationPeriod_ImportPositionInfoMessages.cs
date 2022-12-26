using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T2212_PortfolioValuation_OpportunityValuationPeriod_ImportPositionInfoMessages : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        ValuationPeriods valPeriods = new ValuationPeriods();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC2214 = "T2214_ImportPositionsWithoutTeamMembers";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ImportPositionsWithoutTeamMembers()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2214;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login again as Standard User
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunities(valJobType,fileTC2214);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC2214);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateInternalTeamDetails(fileTC2214);

                //Validate Portfolio Valuation button, click and validate Valuation periods title
                string titleVP = opportunityDetails.ClickPortfolioValuation();
                Assert.AreEqual(value + " - Valuation Periods", titleVP);
                extentReports.CreateLog("Page with title: " + titleVP + " is displayed upon clicking Portfolio Valuation button ");

                //Validate New Opp Valuation button, click and validate New Opportunity Valuation Period title
                string titleNewOppVP = valPeriods.ClickOppValuationPeriod();
                Assert.AreEqual("New Opportunity Valuation Period", titleNewOppVP);
                extentReports.CreateLog("Page with title: " + titleNewOppVP + " is displayed upon clicking New Opportunity Valuation Period button ");

                //Save Valuation Period details and validate Opportunity Valuation Period Detail page title
                string enteredName = valPeriods.EnterAndSaveOppValuationDetails(fileTC2214);
                string titleOppVal = valPeriods.GetOppValPeriodDetailTitle();
                Assert.AreEqual(enteredName, titleOppVal);
                extentReports.CreateLog("Valuation details are saved and page with title: " + titleOppVal + " is displayed ");

                //Click Import button and validate message "There is no valuation period available to import position."
                valPeriods.ClickImportPositionsButton();
                string noValMessage = valPeriods.GetMessageNoValuationPeriod();
                Assert.AreEqual("There is no valuation period available to import position.", noValMessage);
                extentReports.CreateLog("Message: " + noValMessage + " is displayed upon clicking Import Positions button ");

                //Save Valuation Period details again and click on Import positions button and validate "Please select an Valuation Period"
                valPeriods.ClickOppValuationPeriod();
                valPeriods.EnterAndSaveOppValuationDetails(fileTC2214);
                valPeriods.ClickImportPositionsButton();
                string alertMessage = valPeriods.ValidateAlertMessage();
                Console.WriteLine("alertMessage: " + alertMessage);
                Assert.AreEqual("Please select an Valuation Period", alertMessage);
                extentReports.CreateLog("Message: " + alertMessage + " is displayed ");

                //Select radio button, search and validate message
                string noPositionMessage = valPeriods.ValidateNoPositionAvailableMessage();
                Console.WriteLine("noPositionMessage: " + noPositionMessage);
                Assert.AreEqual("For the selected valuation period, either position is not available or all positions have been imported. Please select a different valuation period to import, or go back to the valuation period and add individual positions manually.", noPositionMessage);
                extentReports.CreateLog("Message: " + noPositionMessage + " is displayed ");

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

    

