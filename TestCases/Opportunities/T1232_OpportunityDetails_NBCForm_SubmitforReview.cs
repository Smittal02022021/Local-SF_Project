using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T1232_OpportunityDetails_NBCForm_SubmitforReview : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        NBCForm form = new NBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC1232 = "T1232_NBCFormSubmitforReview.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void NBCFormSubmitforReview()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1232;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                usersLogin.ClickManageUsers();
                usersLogin.LoginAsStandardUser();
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                //Calling AddOpportunities function                
                string value = addOpportunity.AddOpportunities(valJobType,fileTC1232);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1232);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Fetch values of Opportunity Name, Client, Subject and Job Type
                string oppName = opportunityDetails.GetOpportunityName();
                string clientName = opportunityDetails.GetClient();
                string subjectName = opportunityDetails.GetSubject();
                string jobType = opportunityDetails.GetJobType();
                Console.WriteLine(jobType);

                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateInternalTeamDetails(fileTC1232);

                //Click on NBC page and validate title of page
                string title = opportunityDetails.ClickNBCForm();
                Assert.AreEqual("NEW BUSINESS COMMITTEE REVIEW FORM", title);
                extentReports.CreateLog(title + " is displayed ");

                //Validate pre populated fields on NBC form
                string oppNBC = form.ValidateOppName();
                Assert.AreEqual(oppName, oppNBC);
                extentReports.CreateLog("Opportunity Name: " + oppNBC + " in NBC form matches with Opportunity details page ");

                string clientNBC = form.ValidateClient();
                Assert.AreEqual(clientName, clientNBC);
                extentReports.CreateLog("Client Company: " + clientNBC + " in NBC form matches with Opportunity details page ");

                string subjectNBC = form.ValidateSubject();
                Assert.AreEqual(subjectName, subjectNBC);
                extentReports.CreateLog("Subject Company: " + subjectNBC + " in NBC form matches with Opportunity details page ");

                string jobTypeNBC = form.ValidateJobType();
                Assert.AreEqual(jobType, jobTypeNBC);
                extentReports.CreateLog("Job Type: " + jobTypeNBC + " in NBC form matches with Opportunity details page ");

                //Validate validations displayed for mandatory fields
                string validationsList = form.GetFieldsValidations();
                Console.WriteLine(validationsList);
                string expValidations = ReadExcelData.ReadData(excelPath, "NBCForm", 1);
                Assert.AreEqual(expValidations, validationsList);
                extentReports.CreateLog("Validations: " + validationsList + " are displayed ");

                //Click cancel button and accept alert
                form.ClickCancelAndAcceptAlert();

                //Validate visibility of Tabs on checking/unchecking the Toggle Tabs checkbox
                string actualTabs = form.ClickToggleAndValidateTabs();
                string expTabs = ReadExcelData.ReadData(excelPath, "NBCForm", 32);
                Console.WriteLine("Tabs: " + actualTabs);
                Assert.AreEqual(expTabs, actualTabs);
                extentReports.CreateLog("Tabs : " + actualTabs + " are displayed on checking the Toggle Tabs checkbox ");

                string NoTabs = form.ClickToggleAndValidateTabs();
                Assert.AreEqual("Tabs are not displayed", NoTabs);
                extentReports.CreateLog(NoTabs + " on unchecking the Toggle Tabs checkbox ");

                //Log out from standard User and validate admin
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin user " + login.ValidateUser() + " logged in ");

                //Search created opportunity, update conflict check details and open CNBC details page
                string valSearch = opportunityHome.SearchOpportunity(value);
                Console.WriteLine("result : " + valSearch);
                opportunityDetails.UpdateCCOnly();
                opportunityDetails.ClickNBCForm();

                //Call function to enter NBC details
                form.EnterDetailsAndClickSubmit(fileTC1232);

                //Validate title of Email Template page
                string pageTitle = form.ValidateHeader();
                Assert.AreEqual("Send Email", pageTitle);
                extentReports.CreateLog(pageTitle + " is displayed ");

                //Validate Opportunity Name in Email and navigate to Opportunity details page
                string emailOppName = form.GetOppName();
                Assert.AreEqual(oppName, emailOppName);
                extentReports.CreateLog(" Email Template with Opportunity " + emailOppName + " is displayed ");
                usersLogin.UserLogOut();
                driver.Quit();

            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
            }

        }       
    }
}




