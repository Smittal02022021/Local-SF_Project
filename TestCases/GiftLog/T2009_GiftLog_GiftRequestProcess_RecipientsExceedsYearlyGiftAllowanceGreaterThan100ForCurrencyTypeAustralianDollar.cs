using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.GiftLog;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.GiftLog
{
    class T2009_GiftLog_GiftRequestProcess_RecipientsExceedsYearlyGiftAllowanceGreaterThan100ForCurrencyTypeAustralianDollar : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();

        public static string fileTC2009 = "T2009_GiftLog_GiftRequestProcess_RecipientsExceedsYearlyGiftAllowanceGreaterThan100ForCurrencyTypeAustralianDollar";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GiftRequestProcess_RecipientsExceedsYearlyGiftAllowanceGreaterThan100ForCurrencyTypeAustralianDollar()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2009;
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
                homePage.SearchUserByGlobalSearch(fileTC2009, user);

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

                //Switching From HL Force To GiftLog
                giftRequest.GoToGiftRequestPage();
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                //Enter required details in client gift pre- approval page
                giftRequest.EnterDetailsGiftRequest(fileTC2009);

                //Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();
                string labelGiftAmtYTD = giftRequest.GetLabelNewGiftAmtYTD();
                string expectedLabelGiftAmtYTD = ReadExcelData.ReadData(excelPath, "GiftLog", 12);
                Assert.AreEqual(expectedLabelGiftAmtYTD, labelGiftAmtYTD);
                extentReports.CreateLog("Gift Label: " + labelGiftAmtYTD + " is displayed in Selected Recipient(s) table ");
                
                //Verify value of gift
                string valueOfGift = giftRequest.GetGiftValueInGiftAmtYTD();
                extentReports.CreateLog("Gift Value: " + valueOfGift + " is displayed in Selected Recipient(s) table ");

                //Verify currency of gift
                string currencyCode = giftRequest.GetGiftCurrencyCode();
                Assert.AreEqual("USD", currencyCode);
                extentReports.CreateLog("Currency Code: " + currencyCode + " is displayed in Selected Recipient(s) table ");

                //Verification of NewGiftAmtYTD Value turn to red to indicate Currency max limit Exceeded for Current Calendar Year
                string colorOfGiftValue = giftRequest.GetGiftValueColorInGiftAmtYTD();
                string colorOfGiftValueExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                Assert.AreEqual(colorOfGiftValueExl, colorOfGiftValue);
                extentReports.CreateLog("Color Of Gift Value: "+ colorOfGiftValue+" is displayed in Selected Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                string warningMessage = giftRequest.GetWarningMessageOnAmountLimitExceed();
                string warningMessageExl = ReadExcelData.ReadData(excelPath, "GiftLog", 14);
                Assert.AreEqual(warningMessageExl, warningMessage);
                extentReports.CreateLog("Warning Message: "+warningMessage+" is displayed upon submitting a gift request with gift amount exceeding $100 ");

                //Verify revise request button visible
                Assert.IsTrue(giftRequest.IsReviseRequestButtonVisible());
                extentReports.CreateLog("Revise Request button is visible on warning message page ");
               
                //Verify submit request button visible
                Assert.IsTrue(giftRequest.IsSubmitRequestButtonVisible());
                extentReports.CreateLog("Submit Request button is visible on warning message page ");

                //Click on revise request button
                giftRequest.ClickReviseRequestButton();

                //Verification of landing on Pre-approval page upon Click on Revise Request Button
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of revise request button ");

                //Set Desire date to Next Calendar year
                string newDesireDate = giftRequest.EnterDesiredDate(364);
                extentReports.CreateLog("Next Calendar Year date is set to: " + newDesireDate);

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();

                //Verify the warning message
                string warningMessage1 = giftRequest.GetWarningMessageOnAmountLimitExceed();
                ReadExcelData.ReadData(excelPath, "GiftLog", 14);
                Assert.AreEqual(warningMessageExl, warningMessage1);
                extentReports.CreateLog("Warning Message: " + warningMessage1 + " is displayed upon submitting a gift request with gift amount exceeding $100 ");

                //Submit the request even after warning message
                giftApprove.ClickSubmitRequest();
                string congratulationMsg = giftRequest.GetCongratulationsMsg();
                string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateLog(congratulationMsg + " message is displayed upon successful submission of gift request. ");

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
