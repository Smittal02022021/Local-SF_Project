using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1880_FREngagementSummary_HLPostTransactionOpportunities_FieldsAndValues : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1880 = "T1877_HLFinancing_FieldsAndValues";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void HLPostTransactionOpp_FieldsAndValues()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1880;
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

                //Click HL Post-Transaction Opportunities Tab
                summaryPage.ClickHLPostTransOppTab();

                //----Validate fields i.e. Available values, Post-Transaction Staff Roles and Post-Transaction Key External Contact
                //Validate available options                 
                Assert.IsTrue(summaryPage.VerifyAvailableValues(), "Verified that values are same");
                extentReports.CreateLog("Available values are displayed as expected ");

                //----Validate Add Post-Transaction Staff Role window 
                string titleStaffRole = summaryPage.ValidateAddPostTransStaffRoleTitle();
                Assert.AreEqual("Add Post-Transaction Staff Role", titleStaffRole);
                extentReports.CreateLog("Window with name: " + titleStaffRole + " is displayed upon clicking Add Staff Role button ");

                //Validate field Classification on Add Post-Transaction Staff Role window
                string lblClassification = summaryPage.ValidateLabelClassification();
                Assert.AreEqual("Classification", lblClassification);
                extentReports.CreateLog("Field with name: " + lblClassification + " is displayed on Add Post-Transaction Staff Role window ");

                //Verify the values of Classification drop down
                Assert.IsTrue(summaryPage.VerifyClassificationValues(), "Verified that values are same");
                extentReports.CreateLog("Values of Classification drop down is displayed as expected ");

                //Close the window i.e. Add Post-Transaction Staff Role window
                summaryPage.CloseDebtStructureWindow();

                //----Validate Post-Transaction Staff Roles section 
                string secStaffRoles = summaryPage.ValidatePostTransStaffRolesSection();
                Assert.AreEqual("Post-Transaction Staff Roles", secStaffRoles);
                extentReports.CreateLog("Section with name: " + secStaffRoles + " is displayed ");

                //Verify the header values of Post-Transaction Staff Roles section 
                Assert.IsTrue(summaryPage.VerifyHeaderValuesOfStaffRoles(), "Verified that values are same");
                extentReports.CreateLog("Header values of Post-Transaction Staff Roles section is displayed as expected ");

                //----Validate Post-Transaction Key External Contact section 
                string secExtContact = summaryPage.ValidatePostTransExtContactSection();
                Assert.AreEqual("Post-Transaction Key External Contact", secExtContact);
                extentReports.CreateLog("Section with name: " + secExtContact + " is displayed ");

                //Verify the header values of Post-Transaction Key External Contact section 
                Assert.IsTrue(summaryPage.VerifyHeaderValuesOfExtContact(), "Verified that values are same");
                extentReports.CreateLog("Header values of Post-Transaction Key External Contact section is displayed as expected ");

                //----Validate Notes section 
                string secNotes = summaryPage.ValidateNotesSection();
                Assert.AreEqual("Notes", secNotes);
                extentReports.CreateLog("Section with name: " + secNotes + " is displayed ");

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


