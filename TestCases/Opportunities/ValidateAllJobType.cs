using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.UtilityFunctions;
using SalesForce_Project.TestData;
using System;
using SalesForce_Project.Pages.Common;
using System.Collections.Generic;

namespace SalesForce_Project.TestCases.Opportunity
{
    class ValidateAllJobType : BaseClass
    {
       ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        RandomPages pages = new RandomPages();


        public static string fileTC1197 = "TC1197andTC1198AddOpportunityInOpportunityHome.xlsx";

       [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void JobType()
        {
            try
            {
                //Get Test Data file path                
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1197;

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

                //Validate all Job Types  
                //pages.GetJobTypes();
                Assert.IsTrue(pages.ValidateJobTypes(), "Verified that displayed Job Types are same");
                extentReports.CreateLog("Displayed Job Types are as expected ");

                //Validate all Product Lines 
                string blank = pages.GetBlankValue();
                
                pages.GetProductLines();                        
                Assert.IsTrue(pages.ValidateProductLines(), "Verified that displayed Product Lines are same");
                extentReports.CreateLog("Displayed Product Lines are as expected ");

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
