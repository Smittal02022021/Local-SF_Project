using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;


namespace SalesForce_Project.TestCases.Opportunity
{
    class T1832andT1835_OpportunityCounterpartiesPage_AddCounterpartiesAndAddFromExistingOpportunity : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddCounterparty addCounterparty = new AddCounterparty();
            
        public static string fileTC1832 = "T1832_OpportunityCounterpartiesPageAddCounterparties";
                
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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1832;
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
                string stdUser= login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Search for opportunity and open the details page
                opportunityHome.SearchOpportunityWithJobTypeAndStge("Buyside","High");
                extentReports.CreateLog("Matching records are displayed ");

                //Validate Counterparties button
                string counterParty= opportunityDetails.ValidateCounterparties();
                Assert.AreEqual("Counterparties button is displayed", counterParty);
                extentReports.CreateLog(counterParty+ " ");

                //Validate Add Counterparty button and navigate back to Opportunity details page
                string pageTitle = addCounterparty.ValidateAddCounterpartiesAndGetPageHeader(fileTC1832);
                Assert.AreEqual("Search for Company", pageTitle);
                extentReports.CreateLog("Page with title: "+pageTitle +" is displayed ");

                //Validation all sections on the page i.e. Filter, Get Companies from existing Opportunity/Company List
                string labelFilter = addCounterparty.ValidateFilterSection();
                Assert.AreEqual("Filter", labelFilter);
                extentReports.CreateLog(labelFilter + " section is displayed ");

                string labelExistingOpp = addCounterparty.ValidateExistingOpportunitySection();
                Assert.AreEqual("Get Companies from existing Opportunity", labelExistingOpp);
                extentReports.CreateLog(labelExistingOpp + " section is displayed ");

                string labelExistingCompany = addCounterparty.ValidateExistingCompanySection();
                Assert.AreEqual("Get Companies from existing Company List", labelExistingCompany);
                extentReports.CreateLog(labelExistingCompany + " section is displayed ");

                //Expand sections i.e. Get Companies from existing Opportunity and validate fields
                string labelOpp = addCounterparty.ValidateFieldsOfExistingOppSection();
                Assert.AreEqual("Opportunity", labelOpp);
                extentReports.CreateLog("Field " + labelOpp + " under Get Companies from existing Opportunity section is displayed ");

                //Add Company from existing opportunity
                string msgSuccess = addCounterparty.AddCompanyFromExistingOpp("2020 Buyside - Imperative Chemical Partners");
                Assert.AreEqual("Selected records were added successfully", msgSuccess);
                extentReports.CreateLog("Message " + msgSuccess + " is displayed upon adding company from exisitng opportunity ");

                //Validate title of Counterparty records page
                string titleCounterParty = addCounterparty.ClickBackAndGetTitle();
                Assert.AreEqual("Edit Counterparty Records", titleCounterParty);
                extentReports.CreateLog("Page with title " + titleCounterParty + "is displayed upon clicking Back button ");

                //Validate if company get added and delete the same
                string value= addCounterparty.ValidateAddedCompanyExists();
                Assert.AreEqual("True", value);
                extentReports.CreateLog("Added company is displayed on Edit Counterparty Records ");

                string message = addCounterparty.DeleteAddedCompany();
                Assert.AreEqual("No records to display", message);
                extentReports.CreateLog("Message :" +message+ " is displayed upon deleting added company ");

                //Expand sections i.e. Get Companies from existing Company List and validate fields
                addCounterparty.AddCounterparties();
                string lblLookUp = addCounterparty.ValidateFieldsOfExistingCompanySection();
                Assert.AreEqual("True", lblLookUp);
                extentReports.CreateLog("LookUp Field under Get Companies from existing Opportunity section is displayed ");
               
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
            }           
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            usersLogin.UserLogOut();
            usersLogin.UserLogOut();
        }
    }
}
