using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.GiftLog;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.GiftLog
{
    class T2016_GiftLog_GiftRequestProcess_GiftRequests_VerifyGiftDetailsGetsUpdatedOnRefresh : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();

        public static string fileTC2016 = "T2016_GiftLog_GiftRequestProcess_GiftRequests_VerifyGiftDetailsGetsUpdatedOnRefresh";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GiftRequestProcess_GiftRequests_VerifyGiftDetailsGetsUpdatedOnRefresh()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2016;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC2016, user);

                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as Gift Log User and validate the user
                usersLogin.LoginAsSelectedUser();
                string giftLogUser = login.ValidateUser();
                string giftLogUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(giftLogUserExl.Contains(giftLogUser), true);
                extentReports.CreateLog("Gift Log User: " + giftLogUser + " is able to login ");

                //Navigate to Gift Request page
                giftRequest.GoToGiftRequestPage();
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                //Enter required details in client gift pre- approval page
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC2016);

                //Verify company name
                string actualRecipientCompanyName = giftRequest.GetAvailableRecipientCompany();
                string expectedCompanyName = ReadExcelData.ReadData(excelPath, "GiftLog", 8);
                Assert.AreEqual(expectedCompanyName, actualRecipientCompanyName);
                extentReports.CreateLog("Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table ");

                //Verify recipient contact name
                string actualRecipientContactName = giftRequest.GetAvailableRecipientName();
                string expectedContactName = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(expectedContactName, actualRecipientContactName);
                extentReports.CreateLog("Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table ");

                //Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();
                
                //Verify recipient name
                string selectedRecipientName = giftRequest.GetSelectedRecipientName();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateLog("Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Edit Gift Value
                string editValue = ReadExcelData.ReadData(excelPath, "GiftEdit", 6);
                giftRequest.EnterGiftValue(editValue);
                extentReports.CreateLog("Gift Value is updated to: " + editValue);

                //Click Refresh button
                giftRequest.ClickRefreshButton();
                extentReports.CreateLog("Refresh button is clicked. ");

                //Verify if gift value is updated
                string updatedGiftValue = giftRequest.GetGiftValueInGiftAmtYTD();
                extentReports.CreateLog("Updated gift value: " + updatedGiftValue + " is visible in Selected Recipient(s) table. ");

                //Change currency name
                string editCurrencyName = ReadExcelData.ReadData(excelPath, "GiftEdit", 3);
                giftRequest.SelectCurrencyDrpDown(editCurrencyName);
                extentReports.CreateLog("Currency name is updated to: " + editCurrencyName);

                //Click Refresh button
                giftRequest.ClickRefreshButton();
                extentReports.CreateLog("Refresh button is clicked. ");

                //Verify if Currency amount is updated
                string valueOfGift = giftRequest.GetGiftValueInGiftAmtYTD();
                extentReports.CreateLog("Gift Value: " + valueOfGift + " is updated as per the currency name change in Selected Recipient(s) table. ");

                //Change desire date to next year
                string DesireDate = giftRequest.EnterDesiredDate(364);
                extentReports.CreateLog("Desire Date: " + DesireDate + " entered as next year date. ");

                //Click Refresh button
                giftRequest.ClickRefreshButton();
                extentReports.CreateLog("Refresh button is clicked. ");

                //Verify if Currency amount is still displayed
                string valueOfGift1 = giftRequest.GetGiftValueInGiftTotalNextYear();
                extentReports.CreateLog("Gift Value: " + valueOfGift1 + " is displayed for next year. ");

                usersLogin.UserLogOut();
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