using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Engagement
{
    class T1710AndT1422_EngagementCounterpartyDetails_EngagementCounterpartyContact_SaveAndCancelAndAddFromExistingEngagement : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ValuationPeriods valuationPeriods = new ValuationPeriods();
        AddCounterparty addCounterparty = new AddCounterparty();

        public static string fileTC1710 = "T1710_EngagementCounterpartyContact_SaveAndCancel";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void EngagementCounterpartyContact_SaveAndCancel()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1710;
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

                //Clicking on Engagement Tab and search for Engagement by entering Job type         
                string message =engHome.SearchEngagementWithName("Project Rockaway");
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //Validate title of Engagement Details page
                string title = engagementDetails.GetTitle();
                Assert.AreEqual("Engagement", title);
                extentReports.CreateLog("Page with title: " + title + " is displayed ");

                //Click on Counterparties button and validate the page
                string titleCounterparty = engagementDetails.ClickCounterpartiesButton();
                Assert.AreEqual("Edit Counterparty Records", titleCounterparty);
                extentReports.CreateLog("Page with title: " + titleCounterparty + " is displayed upon clicking Counterparties button ");

                //Click Add Counterparties and validate the page
                string titleSearch = addCounterparty.ClickAddCounterpartiesbutton();
                Assert.AreEqual("Search for Company", titleSearch);
                extentReports.CreateLog(titleSearch + " is displayed upon clicking Add Counterparties button ");

                //Save Counterparty record and validate the success message
                string successMsg = addCounterparty.AddCounterparties();
                Assert.AreEqual("Selected records were added successfully", successMsg);
                extentReports.CreateLog(successMsg + " is displayed upon adding record ");

                //Validate page after clicking New Engagement Counterparty Çontact button
                string titleCPCon =addCounterparty.ClickAddCounterPartyContact();
                 Assert.AreEqual("Engagement Counterparty Contact Search", titleCPCon);
                 extentReports.CreateLog(titleCPCon + " is displayed upon clicking New Engagement Counterparty Çontact button ");

                //Validate cancel functionality while adding Counterparty contact
                string cancelMessage = addCounterparty.ValidateCancelFunctionalityOFCPContact();
                 Assert.AreEqual("Record is not addded", cancelMessage);
                 extentReports.CreateLog(cancelMessage + " upon clicking cancel button ");

                //Validate error message when none of the record is selected
                string valMessage = addCounterparty.ValidateErrorMessage();
                Assert.AreEqual("Error: Please select at least one contact before save.", valMessage);
                extentReports.CreateLog(valMessage + " is displayed upon clicking Save button with blank input values ");

                //Validate Save functionality while adding Counterparty contact
                string saveMessage = addCounterparty.SaveCounterpartyContact();
                Assert.AreEqual("Success: Selected records were added successfully.", saveMessage);
                extentReports.CreateLog(saveMessage + " upon clicking Save button ");

                //Delete the added record
                string delMessage = addCounterparty.DeleteAddedCP();
                Assert.AreEqual("No records to display", delMessage);
                extentReports.CreateLog(delMessage + " is displayed upon clicking delete ");

                //Click on Add Counterparty and add company from existing Engagement
                addCounterparty.ClickAddCounterpartiesbutton();
                string msgSuccess = addCounterparty.AddCompanyFromExistingEng("a093100000uRva5");
                Assert.AreEqual("Selected records were added successfully", msgSuccess);
                extentReports.CreateLog("Message " + msgSuccess + " is displayed upon adding company from exisitng Engagement ");

                //Validate title of Counterparty records page
                string titleCounterParty = addCounterparty.ClickBackAndGetTitle();
                Assert.AreEqual("Edit Counterparty Records", titleCounterParty);
                extentReports.CreateLog("Page with title " + titleCounterParty + "is displayed upon clicking Back button ");

                //Validate if company get added and delete the same
                string value = addCounterparty.ValidateAddedCompanyExists();
                Assert.AreEqual("True", value);
                extentReports.CreateLog("Added company is displayed on Edit Counterparty Records ");

                string msgDelete = addCounterparty.DeleteAddedCompany();
                Assert.AreEqual("No records to display", msgDelete);
                extentReports.CreateLog("Message :" + msgDelete + " is displayed upon deleting added company ");
                
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
