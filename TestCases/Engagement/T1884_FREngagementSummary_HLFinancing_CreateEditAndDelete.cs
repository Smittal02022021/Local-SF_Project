using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1884_FREngagementSummary_HLFinancing_CreateEditAndDelete : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1884 = "T1884_HLFinancing_CreateEditAndDelete";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void HLFinancing_CreateEditAndDelete()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1884;
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

                //Validate 'Other' Text box is disabled for all values of Financing Type except 'Other' 
                string otherDisabled = summaryPage.ValidateOtherIsDisabledForAllFinTypesExceptOther(fileTC1884);
                Assert.AreEqual("False", otherDisabled);
                extentReports.CreateLog("Text box for 'Other' is disabled when value of Financing Type is not Other ");

                //Validate 'Other' Text box is enabled for 'Other' Financing Type 
                string otherEnabled = summaryPage.ValidateOtherIsEnabledWhenFinTypeIsOther(fileTC1884);
                Assert.AreEqual("True", otherEnabled);
                extentReports.CreateLog("Text box for 'Other' is enabled when value of Financing Type is Other ");

                //Add HL Financing record with all the details
                summaryPage.SaveHLFinancingDetails(fileTC1884);

                //Validate entered HL Financing record for all values i.e. Financing Type, Other, Security Type,Financing Amount(MM) 
                Assert.IsTrue(summaryPage.ValidateHLFinancingRecord(fileTC1884), "Verified that values are same");
                extentReports.CreateLog("HL Financing record is added with entered values is displayed ");

                //Edit the added HL Financing record
                summaryPage.UpdateHLFinancingRecord(fileTC1884);

                //Validate updated valued of HL Financing record
                //Validate value of Financing Type
                string valFinType = summaryPage.GetFinancingType();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "HLFin", 1), valFinType);
                extentReports.CreateLog("Value of Financing Type has been updated to: " + valFinType + " ");

                //Delete the added record and validate that no record is displayed 
                string msgNoRec = summaryPage.DeleteExistingHLFinRecord();
                Assert.AreEqual("No records to display", msgNoRec);
                extentReports.CreateLog("Added HL Financing record has been deleted ");

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


