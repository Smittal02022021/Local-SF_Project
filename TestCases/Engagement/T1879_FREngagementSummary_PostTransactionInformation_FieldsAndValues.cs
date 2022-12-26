using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
namespace SalesForce_Project.TestCases.Engagement
{
    class T1879_FREngagementSummary_PostTransactionInformation_FieldsAndValues : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1879 = "T1877_HLFinancing_FieldsAndValues";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void PostTransactionInformation_FieldsAndValues()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1879;
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

                //Click Post-Transactions Information Tab
                summaryPage.ClickPostTransInfoTab();

                //---Validate fields i.e. Post Transaction Equity Holders,Post-Transaction Board Members and Post-Transaction Debt(MM) sections and other fields
                //----Validate Post Transaction Equity Holders section 
                string secEquityHolders = summaryPage.ValidatePostTransEquitySection();
                Assert.AreEqual("Post-Transaction Equity Holders", secEquityHolders);
                extentReports.CreateLog("Section with name: " + secEquityHolders + " is displayed ");

                //Validate headers i.e. Equity Holder and Percent Ownership under Post Transaction Equity Holders section 
                Assert.IsTrue(summaryPage.VerifyPostTransEquityHoldersHeader(), "Verified that headers are same");
                extentReports.CreateLog("Equity Holder and Percent Ownership is displayed under Post Transaction Equity Holders section ");

                //----Validate Post-Transaction Board Members section 
                string secBoardMembers = summaryPage.ValidatePostTransMembersSection();
                Assert.AreEqual("Post-Transaction Board Members", secBoardMembers);
                extentReports.CreateLog("Section with name: " + secBoardMembers + " is displayed ");

                //Validate headers i.e. Board Member,Company and Has HL Relationship under Post-Transaction Board Members section 
                Assert.IsTrue(summaryPage.VerifyPostTransBoardMembersHeader(), "Verified that headers are same");
                extentReports.CreateLog("Board Member, Company and Has HL Relationship is displayed under Post Transaction Board Members section ");

                //----Validate Add Debt Structure button and its fields----
                string buttonAddDebt = summaryPage.ValidatePostTransAddDebtButton();
                Assert.AreEqual("Add Debt Structure button is displayed", buttonAddDebt);
                extentReports.CreateLog("Button with name: " + buttonAddDebt + " ");

                //Validate title of window i.e. Add New Post-Transaction Debt Structure
                string titleAddDebtStructure = summaryPage.ValidatePostTransAddDebtTitle();
                Assert.AreEqual("Add New Post-Transaction Debt Structure", titleAddDebtStructure);
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

                //Close the window i.e. Add New Post-Transaction Debt Structure
                summaryPage.CloseDebtStructureWindow();

                //----Validate Post-Transaction Debt (MM) section
                string secDebt = summaryPage.ValidatePostTransDebtSection();
                Assert.AreEqual("Post-Transaction Debt (MM)", secDebt);
                extentReports.CreateLog("Section with name: " + secDebt + " is displayed ");

                //Verify the header values of Post-Transaction Debt section
                Assert.IsTrue(summaryPage.VerifyPostTransDebtHeaderValues(), "Verified that values are same");
                extentReports.CreateLog("Header values of Post-Transaction Debt section is displayed as expected ");

                //Validate field Post-Restructuring Total Debt (MM) under Post-Transaction Debt (MM) section
                string lblTotalDebt = summaryPage.ValidateLabelPostRestrucTotalDebt();
                Assert.AreEqual("Post-Restructuring Total Debt (MM)", lblTotalDebt);
                extentReports.CreateLog("Filed with name: " + lblTotalDebt + " is displayed under Post-Transaction Debt (MM) section ");

                //Validate field Net Debt of the Restructured Company (MM) under Post-Transaction Debt (MM) section

                string lblNetDebt = summaryPage.ValidateLabelNetDebtRestrComp();
                Assert.AreEqual("Net Debt of the Restructured Company (MM)", lblNetDebt);
                extentReports.CreateLog("Field with name: " + lblNetDebt + " is displayed under Post-Transaction Debt (MM) section ");

                //Validate field Closing Stock Price under Post-Transaction Debt (MM) section

                string lblStockPrice = summaryPage.ValidateLabelClosingStockPrice();
                Assert.AreEqual("Closing Stock Price (first full closing day post-restructuring)", lblStockPrice);
                extentReports.CreateLog("Field with name: " + lblStockPrice + " is displayed under Post-Transaction Debt (MM) section ");

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



