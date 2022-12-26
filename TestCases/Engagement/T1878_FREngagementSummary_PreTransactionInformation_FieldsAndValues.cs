using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1878_FREngagementSummary_PreTransactionInformation_FieldsAndValues : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1878 = "T1877_HLFinancing_FieldsAndValues";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void PreTransactionInformation_FieldsAndValues()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData +fileTC1878;
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

                //Click Pre-Transactions Information Tab
                summaryPage.ClickPreTransInfoTab();

                //---Validate fields i.e. Pre Transaction Equity Holders,Pre-Transaction Board Members and Pre-Transaction Debt(MM) sections and other fields
                //----Validate Pre Transaction Equity Holders section 
                string secEquityHolders = summaryPage.ValidatePreTransEquitySection();
                Assert.AreEqual("Pre-Transaction Equity Holders", secEquityHolders);
                extentReports.CreateLog("Section with name: " + secEquityHolders + " is displayed ");

                //Validate headers i.e. Equity Holder and Percent Ownership 
                string headerEquity = summaryPage.ValidateEquityHolderHeader();
                Assert.AreEqual("Equity Holder", headerEquity);
                extentReports.CreateLog("Header with name: " + headerEquity + " is displayed under Pre-Transaction Equity Holders section ");

                string headerOwnership = summaryPage.ValidateOwnershipHeader();
                Assert.AreEqual("Percent Ownership", headerOwnership);
                extentReports.CreateLog("Header with name: " + headerOwnership + " is displayed under Pre-Transaction Equity Holders section ");

                //----Validate Pre-Transaction Board Members section 
                string secBoardMembers = summaryPage.ValidatePreTransMembersSection();
                Assert.AreEqual("Pre-Transaction Board Members", secBoardMembers);
                extentReports.CreateLog("Section with name: " + secBoardMembers + " is displayed ");

                //Validate headers i.e. Board Member,Company and Has HL Relationship
                string headerMember = summaryPage.ValidateMemberHeader();
                Assert.AreEqual("Board Member", headerMember);
                extentReports.CreateLog("Header with name: " + headerMember + " is displayed under Pre-Transaction Board Members section ");

                string headerCompany = summaryPage.ValidateCompanyHeader();
                Assert.AreEqual("Company", headerCompany);
                extentReports.CreateLog("Header with name: " + headerCompany + " is displayed under Pre-Transaction Board Members section ");

                string headerRelationship = summaryPage.ValidateHLRelationshipHeader();
                Assert.AreEqual("Has HL Relationship", headerRelationship);
                extentReports.CreateLog("Header with name: " + headerRelationship + " is displayed under Pre-Transaction Board Members section ");

                //----Validate Add Debt Structure button and its fields----
                string buttonAddDebt = summaryPage.ValidateAddDebtStructureButton();
                Assert.AreEqual("Add Debt Structure button is displayed", buttonAddDebt);
                extentReports.CreateLog("Button with name: " + buttonAddDebt + " ");

                //Validate title of window i.e. Add New Pre-Transaction Debt Structure
                string titleAddDebtStructure = summaryPage.ValidateAddDebtTitle();
                Assert.AreEqual("Add New Pre-Transaction Debt Structure", titleAddDebtStructure);
                extentReports.CreateLog("Window with title: " + titleAddDebtStructure + " is displayed upon clicking Add Debt Structure button ");

                //Validate field Security and its values
                string lblSecurity = summaryPage.ValidateLabelSecurityTypes();
                Assert.AreEqual("Security Type", lblSecurity);
                extentReports.CreateLog("Field with name: " + lblSecurity + " is displayed in Add New Pre-Transaction Debt Structure window ");

                //Verify the values of Security Type
                Assert.IsTrue(summaryPage.VerifySecurityTypeValues(), "Verified that values are same");
                extentReports.CreateLog("Values of HL Security Type drop down is displayed as expected ");

                //Validate field Debt Currency and its values
                string lblCurrency = summaryPage.ValidateLabelCurrency();
                Assert.AreEqual("Debt Currency", lblCurrency);
                extentReports.CreateLog("Field with name: " + lblCurrency + " is displayed in Add New Pre-Transaction Debt Structure window ");

                //Verify the values of Debt Currency
                Assert.IsTrue(summaryPage.VerifyCurrencyValues(), "Verified that values are same");
                extentReports.CreateLog("Values of Debt Currency drop down is displayed as expected ");

                //Close the window i.e. Add New Pre-Transaction Debt Structure
                summaryPage.CloseDebtStructureWindow();

                //----Validate Pre-Transaction Debt (MM) section
                string secDebt = summaryPage.ValidatePreTransDebtSection();
                Assert.AreEqual("Pre-Transaction Debt (MM)", secDebt);
                extentReports.CreateLog("Section with name: " + secDebt + " is displayed ");

                //Verify the header values of Pre-Transaction Debt section
                Assert.IsTrue(summaryPage.VerifyPreTransDebtHeaderValues(), "Verified that values are same");
                extentReports.CreateLog("Header values of Pre-Transaction Debt section is displayed as expected ");

                //Validate field Pre Reorganization Constituent Debt under Pre-Transaction Debt (MM) section
                string lblConstDebt = summaryPage.ValidateLabelPreReorgConstDebt();
                Assert.AreEqual("Pre Reorganization Constituent Debt", lblConstDebt);
                extentReports.CreateLog("Filed with name: " + lblConstDebt + " is displayed under Pre-Transaction Debt (MM) section ");

                //Validate Pre-Transaction Debt (MM) section
                string lblTotalDebt = summaryPage.ValidateLabelPreReorgTotal();
                Assert.AreEqual("Pre Reorganized Total Debt", lblTotalDebt);
                extentReports.CreateLog("Field with name: " + lblTotalDebt + " is displayed under Pre-Transaction Debt (MM) section ");

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



