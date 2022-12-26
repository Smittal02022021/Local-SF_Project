using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.GiftLog;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.GiftLog
{
    class T2022_GiftLog_GiftRequestProcess_VerifyTheYearlyGiftAllowanceExceedLimitForTheNewGiftTotalNextYear : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftSubmittedPage giftsSubmit = new GiftSubmittedPage();
        GiftRequestEditPage giftEdit = new GiftRequestEditPage();

        public static string fileTC2022 = "T2022_GiftLog_GiftRequestProcess_VerifyTheYearlyGiftAllowanceExceedLimitForTheNewGiftTotalNextYear";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GiftRequestProcess_VerifyTheYearlyGiftAllowanceExceedLimitForTheNewGiftTotalNextYear()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2022;
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
                homePage.SearchUserByGlobalSearch(fileTC2022, user);

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

                // Enter required details in client gift pre- approval page
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC2022);

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
                
                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();

                //Verify recipient name
                string selectedRecipientName = giftRequest.GetSelectedRecipientName();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateLog("Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                string DesireDate = giftRequest.EnterDesiredDate(350);
                extentReports.CreateLog("Desire Date: " + DesireDate + " entered as next year date. ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                giftApprove.ClickSubmitRequest();
                string congratulationMsg = giftRequest.GetCongratulationsMsg();
                string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                usersLogin.UserLogOut();

                // Search Complaince user by global search
                string userCompliance = ReadExcelData.ReadData(excelPath, "Users", 2);
                homePage.SearchUserByGlobalSearch(fileTC2022, userCompliance);

                //Verify searched user
                string userPeople1 = homePage.GetPeopleOrUserName();
                Assert.AreEqual(userCompliance, userPeople1);
                extentReports.CreateLog("User " + userPeople1 + " details are displayed ");

                //Login as Compliance User and validate the user
                usersLogin.LoginAsSelectedUser();
                extentReports.CreateLog("Compliance User: " + userPeople1 + " is able to login ");

                //Click on approve gifts tab
                giftApprove.ClickApproveGiftsTab();
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastNameForNextYear(fileTC2022);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                //Approve gift
                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                giftApprove.SetApprovalDenialComments();
                giftApprove.ClickApproveSelectedButton();
                extentReports.CreateLog("Approve selected button is clicked successfully ");

                usersLogin.UserLogOut();

                // Search standard user by global search
                homePage.SearchUserByGlobalSearch(fileTC2022, user);

                //Verify searched user
                homePage.GetPeopleOrUserName();
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as Standard User and validate the user
                usersLogin.LoginAsSelectedUser();
                extentReports.CreateLog("Gift Log User: " + giftLogUser + " is able to login ");

                //Navigate to Gift Request page
                giftRequest.GoToGiftRequestPage();
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                // Enter required details in client gift pre- approval page
                string valGiftNameEntered1 = giftRequest.EnterDetailsGiftRequest(fileTC2022);

                string DesireDate1 = giftRequest.EnterDesiredDate(350);
                extentReports.CreateLog("Desire Date: " + DesireDate1 + " entered as next year date. ");

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();
                string labelGiftAmtYTD = giftRequest.GetLabelNewGiftAmtYTD();
                string expectedLabelGiftAmtYTD = ReadExcelData.ReadData(excelPath, "GiftLog", 12);
                Assert.AreEqual(expectedLabelGiftAmtYTD, labelGiftAmtYTD);
                extentReports.CreateLog("Gift Label: " + labelGiftAmtYTD + " is displayed in Selected Recipient(s) table ");

                //Verification of NewGiftAmtYTD Value turn to red to indicate Currency max limit Exceeded for Current Calendar Year
                string colorOfGiftValue = giftRequest.GetGiftValueColorInGiftAmtYTD();
                string colorOfGiftValueExl = ReadExcelData.ReadData(excelPath, "GiftLog", 14);
                Assert.AreEqual(colorOfGiftValueExl, colorOfGiftValue);
                extentReports.CreateLog("Color Of Gift Value: " + colorOfGiftValue + " is displayed in Selected Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                string warningMessage = giftRequest.GetWarningMessageOnAmountLimitExceed();
                string warningMessageExl = ReadExcelData.ReadData(excelPath, "GiftLog", 15);
                Assert.AreEqual(warningMessageExl, warningMessage);
                extentReports.CreateLog("Warning Message: " + warningMessage + " is displayed upon submitting a gift request with gift amount exceeding $100 ");

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