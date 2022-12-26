using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1877_FREngagementSummary_HLFinancing_FieldsAndValues : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1877 = "T1877_HLFinancing_FieldsAndValues";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void HLFinancing_FieldsAndValues()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1877;
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
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Search engagement with LOB - FR
                string message = engHome.SearchEngagementWithName(ReadExcelData.ReadData(excelPath, "Engagement", 2));
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matches to LOB-FR are found and Engagement Detail page is displayed ");

                //Validate FR Engagement Summary button
                string btnFREngSum = engagementDetails.ValidateFREngSummaryButton();
                Assert.AreEqual("True", btnFREngSum);
                extentReports.CreateLog("FR Engagement Summary button is displayed ");

                //Validate FR Engagement Summary page and its fields
                string titleFRSummary = engagementDetails.ClickFREngSummaryButton();
                Assert.AreEqual("FR Engagement Summary", titleFRSummary);
                extentReports.CreateLog("Page with title: " + titleFRSummary + " is displayed upon clicking FR Engagement Summary button ");

                //Click HL Financing Tab
                summaryPage.ClickHLFinancingTab();

                //---Validate fields i.e. Header row, Total Financing Amount,Financing Description
                //Validate Add HL Financing button 
                string btnDisplayed = summaryPage.ValidateAddHLFinancing();
                Assert.AreEqual("Add HL Financing is displayed", btnDisplayed);
                extentReports.CreateLog("Button with name: " + btnDisplayed + " ");

                //Click Add HL Financing button and validate Insert new HL Financing window
                string winTitle = summaryPage.ClickAddHLFinancing();
                Assert.AreEqual("Insert New HL Financing", winTitle);
                extentReports.CreateLog("Window with title: " + winTitle + " is displayed ");

                //Validate fields of Insert New HL Financing window i.e. Financing Type, Other, Security Type,Financing Amount(MM) 
                //Validate label "Financing Type" 
                string lblFinType = summaryPage.ValidateLabelFinType();
                Assert.AreEqual("Financing Type", lblFinType);
                extentReports.CreateLog("Field with name: " + lblFinType + " is displayed ");

                //Validate values of Financing Type combo
                Assert.IsTrue(summaryPage.VerifyFinancingTypesValues(), "Verified that values are same");
                extentReports.CreateLog("Values of Financing Type combo are displayed as expected ");

                //Validate label "Other" 
                string lblOther = summaryPage.ValidateLabelOther();
                Assert.AreEqual("Other", lblOther);
                extentReports.CreateLog("Field with name: " + lblOther + " is displayed ");

                //Validate label "Security Type" 
                string lblSecType = summaryPage.ValidateLabelSecurityType();
                Assert.AreEqual("Security Type", lblSecType);
                extentReports.CreateLog("Field with name: " + lblSecType + " is displayed ");

                //Validate values of Security Type combo
                Assert.IsTrue(summaryPage.VerifySecurityTypesValues(), "Verified that values are same");
                extentReports.CreateLog("Values of Security Type combo are displayed as expected ");

                //Validate label "Financing Amount" 
                string lblFinAmount = summaryPage.ValidateLabelFinAmount();
                Assert.AreEqual("Financing Amount (MM)", lblFinAmount);
                extentReports.CreateLog("Field with name: " + lblFinAmount + " is displayed ");

                //Validate Header row with all header values-----                
                Assert.IsTrue(summaryPage.VerifyHLFinancingHeaderValues(), "Verified that header row is same");
                extentReports.CreateLog("Header row of HL Financing tab is displayed as expected ");

                //Validate label "Total Financing Amount" 
                string lblTotalFin = summaryPage.ValidateLabelTotalFinAmount();
                Assert.AreEqual("Total Financing Amount", lblTotalFin);
                extentReports.CreateLog("Field with name: " + lblTotalFin + " is displayed ");

                //----Validate "Financing Description" label----

                string lblFinDesc = summaryPage.ValidateLabelFinDesc();
                Assert.AreEqual("Financing Description", lblFinDesc);
                extentReports.CreateLog("Field with name: " + lblFinDesc + " is displayed ");

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


