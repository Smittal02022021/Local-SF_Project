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
    class T2018_GiftLog_GiftApprovalProcess_ApproveGifts_VerifyThePreviousYTDGiftValueAndNewYTDIsVisibleToApprover : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();

        public static string fileTC2018 = "T2018_GiftLog_GiftApprovalProcess_ApproveGifts_VerifyThePreviousYTDGiftValueAndNewYTDIsVisibleToApprover";
       
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GiftLog_GiftApprovalProcess_ApproveGifts_VerifyThePreviousYTDGiftValueAndNewYTDIsVisibleToApprover()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2018;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                for (int i=1; i<3; i++)
                {
                    if (i==1)
                    {
                        // Search standard user by global search
                        string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                        homePage.SearchUserByGlobalSearch(fileTC2018, user);

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
                        string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC2018);

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

                        double enteredGiftValue = double.Parse(ReadExcelData.ReadData(excelPath, "GiftLog", 3));

                        string currentGift = giftRequest.GetCurrentGiftAmtYTD();
                        double currentGiftValue = double.Parse(currentGift);

                        //Adding recipient from add recipient section to selected recipient section
                        giftRequest.AddRecipientToSelectedRecipients();

                        double result = Math.Round(currentGiftValue + enteredGiftValue, 1);

                        string newGiftValue = giftRequest.GetGiftValueInGiftAmtYTD();
                        double actualnewGiftYTD = double.Parse(newGiftValue);

                        //Click on submit gift request
                        giftRequest.ClickSubmitGiftRequest();
                        giftApprove.ClickSubmitRequest();
                        string congratulationMsg = giftRequest.GetCongratulationsMsg();
                        extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                        usersLogin.UserLogOut();

                        // Search Complaince user by global search
                        string userCompliance = ReadExcelData.ReadData(excelPath, "Users", 2);
                        homePage.SearchUserByGlobalSearch(fileTC2018, userCompliance);

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

                        //Search gift details by recipient last name for current year
                        giftApprove.SearchByRecipientLastName(fileTC2018);
                        extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                        //Get Prev YTD Value
                        string prevYTD = giftApprove.GetPrevYTDValue(valGiftNameEntered);
                        double prevYTDResult = double.Parse(prevYTD);
                        extentReports.CreateLog("Prev YTD Value : " + prevYTDResult + " is displayed for current year gift in pending gifts table. ");

                        //Match Value with current gift Value
                        string giftVal = giftApprove.GetGiftValueFromPendingGifts(valGiftNameEntered);
                        double giftValResult = double.Parse(giftVal);
                        Assert.AreEqual(giftValResult, enteredGiftValue);
                        extentReports.CreateLog("Gift Value: " + giftValResult + " matches with current gift value in the pending gifts table. ");

                        //Get New YTD Value and verify the results
                        string newYTD = giftApprove.GetNewYTDValue(valGiftNameEntered);
                        double newYTDResult = double.Parse(newYTD);

                        double result2 = prevYTDResult + giftValResult;
                        Assert.AreEqual(newYTDResult, result2);
                        extentReports.CreateLog("New YTD: " + newYTDResult + " is the sum of Prev YTD: " + prevYTDResult + " and Gift Value: " + giftValResult + " in pending gifts table. ");

                        //Approve the current year gift
                        giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                        giftApprove.SetApprovalDenialComments();
                        giftApprove.ClickApproveSelectedButton();
                        extentReports.CreateLog("Gift is approved successfully ");

                        //Click on approve gifts tab
                        giftApprove.ClickApproveGiftsTab();

                        //Search gift in approved list
                        giftApprove.SearchByStatus(fileTC2018, "Approved");

                        //Get Prev YTD Value
                        string approvedPrevYTD = giftApprove.GetApprovedPrevYTDValue(valGiftNameEntered);
                        double approvedPrevYTDResult = double.Parse(approvedPrevYTD);
                        extentReports.CreateLog("Prev YTD Value : " + approvedPrevYTDResult + " is displayed for current year approved gift in approved gifts table. ");

                        //Match Value with current gift Value
                        string approvedGiftVal = giftApprove.GetGiftValueFromApprovedGifts(valGiftNameEntered);
                        double approvedGiftValResult = double.Parse(approvedGiftVal);
                        Assert.AreEqual(approvedGiftValResult, enteredGiftValue);
                        extentReports.CreateLog("Gift Value: " + approvedGiftValResult + " matches with current gift value in approved gifts table. ");

                        //Get New YTD Value and verify the results
                        string approvedNewYTD = giftApprove.GetApprovedNewYTDValue(valGiftNameEntered);
                        double approvedNewYTDResult = double.Parse(approvedNewYTD);
                        Assert.AreEqual(approvedPrevYTDResult, approvedNewYTDResult);
                        extentReports.CreateLog("New YTD Value : " + approvedNewYTDResult + " is equal to Prev YTD Value: " + approvedPrevYTDResult + " in approved gifts table. ");

                        usersLogin.UserLogOut();
                    }
                    else if (i==2)
                    {
                        // Search standard user by global search
                        string user1 = ReadExcelData.ReadData(excelPath, "Users", 1);
                        homePage.SearchUserByGlobalSearch(fileTC2018, user1);

                        //Verify searched user
                        string userPeople1 = homePage.GetPeopleOrUserName();
                        string userPeopleExl1 = ReadExcelData.ReadData(excelPath, "Users", 1);
                        Assert.AreEqual(userPeopleExl1, userPeople1);
                        extentReports.CreateLog("User " + userPeople1 + " details are displayed ");

                        //Login as Gift Log User and validate the user
                        usersLogin.LoginAsSelectedUser();
                        string giftLogUser1 = login.ValidateUser();
                        string giftLogUserExl1 = ReadExcelData.ReadData(excelPath, "Users", 1);
                        Assert.AreEqual(giftLogUserExl1.Contains(giftLogUser1), true);
                        extentReports.CreateLog("Gift Log User: " + giftLogUser1 + " is able to login ");

                        //Navigate to Gift Request page
                        giftRequest.GoToGiftRequestTab();
                        extentReports.CreateLog("Gift Request page is diplayed upon click of Gift Request link ");

                        //Enter desire date as next year
                        giftRequest.EnterDesiredDate(360);

                        //Enter required details in client gift pre- approval page
                        string valGiftNameEntered1 = giftRequest.EnterDetailsGiftRequest(fileTC2018);

                        double enteredGiftValue1 = double.Parse(ReadExcelData.ReadData(excelPath, "GiftLog", 3));

                        string currentNextYearGift = giftRequest.GetCurrentNextYearGiftAmt();
                        double currentNextYearGiftValue = double.Parse(currentNextYearGift);

                        //Adding recipient from add recipient section to selected recipient section
                        giftRequest.AddRecipientToSelectedRecipients();

                        double result1 = Math.Round(currentNextYearGiftValue + enteredGiftValue1, 1);

                        string giftValueNextYear = giftRequest.GetGiftValueInGiftTotalNextYear();
                        double actualGiftValueNextYear = double.Parse(giftValueNextYear);

                        //Click on submit gift request
                        giftRequest.ClickSubmitGiftRequest();
                        giftApprove.ClickSubmitRequest();
                        string congratulationMsg1 = giftRequest.GetCongratulationsMsg();
                        extentReports.CreateLog("Congratulations message: " + congratulationMsg1 + " in displayed upon successful submission of gift request ");

                        usersLogin.UserLogOut();

                        // Search Complaince user by global search
                        string userCompliance = ReadExcelData.ReadData(excelPath, "Users", 2);
                        homePage.SearchUserByGlobalSearch(fileTC2018, userCompliance);

                        //Verify searched user
                        string userPeople2 = homePage.GetPeopleOrUserName();
                        Assert.AreEqual(userCompliance, userPeople2);
                        extentReports.CreateLog("User " + userPeople2 + " details are displayed ");

                        //Login as Compliance User and validate the user
                        usersLogin.LoginAsSelectedUser();
                        extentReports.CreateLog("Compliance User: " + userPeople2 + " is able to login ");

                        //Click on approve gifts tab
                        giftApprove.ClickApproveGiftsTab();
                        Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                        extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                        //Search gift details by recipient last name for current year
                        giftApprove.SearchByRecipientLastNameForNextYear(fileTC2018);
                        extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                        //Get Prev YTD Value
                        string prevYTD = giftApprove.GetPrevYTDValue(valGiftNameEntered1);
                        double prevYTDResult = double.Parse(prevYTD);
                        extentReports.CreateLog("Prev YTD Value : " +prevYTDResult+ " is displayed for next year gift in pending gifts table. ");

                        //Match Value with current gift Value
                        string giftVal = giftApprove.GetGiftValueFromPendingGifts(valGiftNameEntered1);
                        double giftValResult = double.Parse(giftVal);
                        Assert.AreEqual(giftValResult, enteredGiftValue1);
                        extentReports.CreateLog("Gift Value: " + giftValResult + " matches with current gift value in pending gifts table. ");

                        //Get New YTD Value and verify the results
                        string newYTD = giftApprove.GetNewYTDValue(valGiftNameEntered1);
                        double newYTDResult = double.Parse(newYTD);

                        double result2 = prevYTDResult + giftValResult;
                        Assert.AreEqual(newYTDResult, result2);
                        extentReports.CreateLog("New YTD: " + newYTDResult + " is the sum of Prev YTD: " + prevYTDResult + " and Gift Value: " + giftValResult + " in pending gifts table. ");

                        //Approve the gift
                        giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered1);
                        giftApprove.SetApprovalDenialComments();
                        giftApprove.ClickApproveSelectedButton();
                        extentReports.CreateLog("Gift is approved successfully ");

                        //Click on approve gifts tab
                        giftApprove.ClickApproveGiftsTab();

                        //Search gift in approved list
                        giftApprove.SearchByStatusForNextYear(fileTC2018, "Approved");

                        //Get Prev YTD Value
                        string approvedPrevYTD = giftApprove.GetApprovedPrevYTDValue(valGiftNameEntered1);
                        double approvedPrevYTDResult = double.Parse(approvedPrevYTD);
                        extentReports.CreateLog("Prev YTD Value : " + approvedPrevYTDResult + " is displayed for approved gift in approved gifts table. ");

                        //Match Value with current gift Value
                        string approvedGiftVal = giftApprove.GetGiftValueFromApprovedGifts(valGiftNameEntered1);
                        double approvedGiftValResult = double.Parse(approvedGiftVal);
                        Assert.AreEqual(approvedGiftValResult, enteredGiftValue1);
                        extentReports.CreateLog("Gift Value: " + approvedGiftValResult + " matches with current gift value in approved gifts table. ");

                        //Get New YTD Value and verify the results
                        string approvedNewYTD = giftApprove.GetApprovedNewYTDValue(valGiftNameEntered1);
                        double approvedNewYTDResult = double.Parse(approvedNewYTD);
                        Assert.AreEqual(approvedPrevYTDResult, approvedNewYTDResult);
                        extentReports.CreateLog("New YTD Value : " + approvedNewYTDResult + " is equal to Prev YTD Value: " + approvedPrevYTDResult + " in approved gifts table. ");

                        usersLogin.UserLogOut();
                    }
                }

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
