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
    class T2100_OpportunityDetailsPage_NBC_VerifyTheFunctionalityOfEUOverrideButton : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage(); 
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        NBCForm form = new NBCForm();      

        public static string fileTC2100= "T1232_NBCFormSubmitforReview.xlsx";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2100;
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

                //Search for opportunity and open the details page
                opportunityHome.SearchOpportunityWithJobTypeAndStge("Buyside", "High");
                extentReports.CreateLog("Records matching to selected JobType and Stage are displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();

                //Click on NBC page and validate title of page
                string title = opportunityDetails.ClickNBCForm();
                Assert.AreEqual("NEW BUSINESS COMMITTEE REVIEW FORM", title);
                extentReports.CreateLog(title + " is displayed upon clicking NBC Form button ");

                //Update 1st question as Yes
                string value = form.UpdateFinancialOption("Yes");
                Assert.AreEqual("Yes", value);
                extentReports.CreateLog("1st question is updated with value: " +value +" ");

                //Validate Role text after selecting Yes for 1st question
                string publicRole = form.GetRoleText();
                Assert.AreEqual("*Please add Mark Mikullitz, Rick A. Lacher or Steven Tishman to the Public role in the HL Internal Team section.", publicRole);
                extentReports.CreateLog("Public Role Text : " + publicRole + " is displayed after selecting Yes for the first question ");

                //Validate Suggested Fees 
                string Fees = form.GetSuggestedFees();
                Assert.AreEqual("Based on Public Comps Transaction Size Ranges ($MM) $50-$100 $100-$200 $200-$300 $300-$400 Average Fee($MM) $1.83 $2.63 $4.64 $5.21 Average Percentage 2.50% 1.83% 1.84% 1.50% Median Fee($MM) $1.75 $2.56 $4.50 $4.91 Median Percentage 2.31% 1.80% 1.67% 1.37%", Fees);
                extentReports.CreateLog("Transaction Size Ranges are as expected after selecting Yes for the first question: " + Fees + " ");

                //Log out of standard user and validate admin
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin user " + login.ValidateUser() + " logged in ");

                //Search for same Opportunity and click on NBC form
                string rec = opportunityHome.SearchOpportunity(opportunityNumber);
                Assert.AreEqual("Record found", rec);
                extentReports.CreateLog("Same opportunity is found ");
                opportunityDetails.ClickNBCForm();

                //Click EUOverride, validate Role text and Suggested Fees
                form.ClickEUOverrideButton();

                //Validate Role text after clicking EU Override
                string publicRole1 = form.GetEUOverrideText();
                Assert.AreEqual("*Please inform the EU CF Co-Heads of the Opportunity and place an approved staff member in the Public role in the HL Internal Team section.  Please also consider whether the activity you wish to undertake is regulated financial activity (in which case consideration will need to be given as to the appropriate Houlihan Lokey group company to engage on this Opportunity). Please contact Legal if uncertain.", publicRole1);
                extentReports.CreateLog("Public Role Text : " + publicRole1 + " is displayed after clicking EU Override button ");

                //Validate Suggested Fees after clicking EU Override
                string feesEUOverride = form.GetEUFees();
                Assert.AreEqual("Transaction Size Ranges (£/€MM) 0-50 50-100 100-200 200-300 Base Fee Percentage* 1.50% 1.25% 1.00% 0.75%", feesEUOverride);
                extentReports.CreateLog("Transaction Size Ranges are as expected after clicking EU Override button: " + feesEUOverride);

                //Click EUOverride, validate Role text and Suggested Fees
                form.ClickEUOverrideButton();

                //Validate Role text after clicking EU Override
                string publicRole2 = form.GetRoleText();
                Assert.AreEqual("*Please add Mark Mikullitz, Rick A. Lacher or Steven Tishman to the Public role in the HL Internal Team section.", publicRole2);
                extentReports.CreateLog("Public Role Text : " + publicRole2 + " is displayed after clicking EU Override button again ");

                //Validate Suggested Fees after clicking EU Override
                string feesEUOverride2 = form.GetSuggestedFees();
                Assert.AreEqual("Based on Public Comps Transaction Size Ranges ($MM) $50-$100 $100-$200 $200-$300 $300-$400 Average Fee($MM) $1.83 $2.63 $4.64 $5.21 Average Percentage 2.50% 1.83% 1.84% 1.50% Median Fee($MM) $1.75 $2.56 $4.50 $4.91 Median Percentage 2.31% 1.80% 1.67% 1.37%", feesEUOverride2);
                extentReports.CreateLog("Transaction Size Ranges are as expected after clicking EU Override button again: " + feesEUOverride2);

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

    

