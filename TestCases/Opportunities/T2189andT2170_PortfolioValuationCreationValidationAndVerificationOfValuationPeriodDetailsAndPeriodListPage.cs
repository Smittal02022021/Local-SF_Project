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
    class T2189andT2170_PortfolioValuationCreationValidationAndVerificationOfValuationPeriodDetailsAndPeriodListPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        ValuationPeriods valPeriods = new ValuationPeriods();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC2189 = "T2189_PortfolioValuationCreation";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AddCounterparties()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2189;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
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
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunities(valJobType,fileTC2189);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC2189);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateInternalTeamDetails(fileTC2189);

                //Validate Portfolio Valuation button, click and validate Valuation periods title
                string titleVP = opportunityDetails.ClickPortfolioValuation();
                Assert.AreEqual(value + " - Valuation Periods", titleVP);
                extentReports.CreateLog("Page with title: " + titleVP + " is displayed ");

                //Validate the message "Currently there are no valuation periods for this Opportunity. To proceed, please create a new valuation period."
                string msgNoVal = valPeriods.GetNoValuationPeriods();
                Assert.AreEqual("Currently there are no valuation periods for this Opportunity. To proceed, please create a new valuation period.", msgNoVal);
                extentReports.CreateLog("Message: " + msgNoVal + " is displayed ");

                //Validate Back to Opportunity button is displayed
                string btnBackToOpp = valPeriods.ValidateBackToOpportunityButton();
                Assert.AreEqual("Back To Opportunity", btnBackToOpp);
                extentReports.CreateLog("Button: " + btnBackToOpp + " is displayed on Valuation Periods page ");

                //Click Back to Opportunity and validate Opportunity details page
                string titleOppDetails = valPeriods.ClickBackToOppAndValidatePageTitle();
                Assert.AreEqual("Opportunity Detail", titleOppDetails);
                extentReports.CreateLog("Page with title: " + titleOppDetails + " is displayed upon clicking Back To Opportunity button ");

                //Validate New Opp Valuation button, click and validate New Opportunity Valuation Period title
                opportunityDetails.ClickPortfolioValuation();
                string titleNewOppVP = valPeriods.ClickOppValuationPeriod();
                Assert.AreEqual("New Opportunity Valuation Period", titleNewOppVP);
                extentReports.CreateLog("Page with title: " + titleNewOppVP + " is displayed ");

                //Save Valuation Period details and validate Opportunity Valuation Period Detail page title
                string enteredName = valPeriods.EnterAndSaveOppValuationDetails(fileTC2189);
                string titleOppVal = valPeriods.GetOppValPeriodDetailTitle();
                Assert.AreEqual(enteredName, titleOppVal);
                extentReports.CreateLog("Valuation details are saved and page with title: " + titleOppVal + " is displayed ");

                //Click on Back To Opp Valuation Period List button and validate the added Valuation Period
                string title = valPeriods.ClickBackToOppValAndGetTitle();
                Assert.AreEqual(opportunityNumber+ " - Valuation Periods", title);
                extentReports.CreateLog("Page with title: " + title + " is displayed upon clicking Back To Opp Valuation Period List button ");

                //Validate the added Valuation Period details
                string valVPName = valPeriods.GetVPName();
                Assert.AreEqual(enteredName, valVPName);
                string valValuationDate = valPeriods.GetValuationDate();
                Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture), valValuationDate);
                extentReports.CreateLog("Valuation Period with name: " +valVPName +" and Valuation Date : "+valValuationDate+" is added ");

                //Validate Cancel button's functionality
                valPeriods.ClickOnAddedValuation();
                string msgDisplayed = valPeriods.EnterPeriodPositionDetailsAndClickCancel(fileTC2189);
                Assert.AreEqual("No records to display", msgDisplayed);
                extentReports.CreateLog("Cancel button is working as expected ");

                //Validate error message "Notes is required if Asset Classes is 'Other' in Valuation Period Position page
                string valMessage = valPeriods.ValidateErrorMessage(fileTC2189);
                Assert.AreEqual("Error:\r\nNotes is required if Asset Classes is 'Other'.", valMessage);
                extentReports.CreateLog("Error message: " + valMessage + " is displayed ");

                //Enter all required details, Save and validate record exists in Opp Valuation Period Position section
                bool tableDisplayed = valPeriods.EnterValPeriodPositionDetails(fileTC2189);
                Assert.IsTrue(tableDisplayed);
                extentReports.CreateLog("Details are saved and record exists in Opp Valuation Period Positions section ");

                //Validate details of records displayed in Opp Valuation Period Position section
                string expName = ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4);
                string actualName = valPeriods.GetName();
                Assert.AreEqual(expName, actualName);
                extentReports.CreateLog("Name: " + actualName + " matches to the entered name ");

                //Validate Company
                string actualCompName = valPeriods.GetCompanyName();
                Assert.AreEqual(expName, actualCompName);
                extentReports.CreateLog("Company: " + actualCompName + " matches to the entered company ");

                //Validate Industry Group
                string expIG = ReadExcelData.ReadData(excelPath, "ValuationPeriod", 8);
                string actualIG = valPeriods.GetIG();
                Assert.AreEqual(expIG, actualIG);
                extentReports.CreateLog("Industry Group: " + actualIG + " matches to the entered Industry Group ");

                //Validate Asset
                string expAsset = ReadExcelData.ReadData(excelPath, "ValuationPeriod", 13);
                string actualAsset = valPeriods.GetAsset();
                Assert.AreEqual(expAsset, actualAsset);
                extentReports.CreateLog("Asset: " + actualAsset + " matches to the entered asset ");

                //Validate Status
                string actualStatus = valPeriods.GetStatus();
                Assert.AreEqual("In Progress", actualStatus);
                extentReports.CreateLog("Status: " + actualStatus + " is displayed as expected ");

                //Validate Opportunity Valuation Position Detail page after clicking on Position Name
                string titleValPeriodPositon = valPeriods.ValidateOppValPositionDetailTitle();
                Assert.AreEqual("Opp Valuation Period Position", titleValPeriodPositon);
                extentReports.CreateLog("Page with title: " + titleValPeriodPositon + " is displayed upon clicking Position Name");

                //Validate Opportunity Valuation Period Detail page after clicking on Back To Valuation Period
                string titlePeriodDetail = valPeriods.ValidateOppValPeriodDetailTitle();
                Assert.AreEqual("Opportunity Valuation Period Detail", titlePeriodDetail);
                extentReports.CreateLog("Page with title: " + titlePeriodDetail + " is displayed upon clicking on Back To Valuation Period button");

                //Update details of Opp Valuation Period Position 
                valPeriods.UpdateOppValPeriodPositionDetails(fileTC2189);

                //Validate updated details of records displayed in Opp Valuation Period Position section
                //----Validate updated Asset
                string expUpdAsset = ReadExcelData.ReadData(excelPath, "ValuationPeriod", 10);
                string actualUpdAsset = valPeriods.GetAsset();
                Assert.AreEqual(expUpdAsset, actualUpdAsset);
                extentReports.CreateLog("Asset: " + actualUpdAsset + " matches to the updated asset ");

                //Validate the delete link for logged user
                string msgSection = valPeriods.ValidateRecAfterDelete();
                Assert.AreEqual("Delete link is not displayed", msgSection);
                extentReports.CreateLog(msgSection + " " + "user: " + " " + stdUser + " ");

                //Log out of standard user and validate admin
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin user " + login.ValidateUser() + " logged in ");

                //Search for same opportunity, validate delete link, click it and validate records in Period Position
                string valSearch = opportunityHome.SearchOpportunity(value);
                opportunityDetails.ClickPortfolioValuation();
                valPeriods.ClickAddedValPeriod();
                string msgDelete = valPeriods.ValidateRecAfterDelete();
                Assert.AreEqual("No records to display", msgDelete);
                extentReports.CreateLog("Record is deleted and " + " " + msgDelete + " message is displayed ");

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

