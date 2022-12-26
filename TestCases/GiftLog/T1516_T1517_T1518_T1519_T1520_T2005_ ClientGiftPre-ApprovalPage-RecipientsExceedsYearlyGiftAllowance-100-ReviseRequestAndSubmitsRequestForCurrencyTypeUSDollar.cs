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
    class T1516_T1517_T1518_T1519_T1520_T2005_ClientGiftPre_ApprovalPage_RecipientsExceedsYearlyGiftAllowance_100_ReviseRequestAndSubmitsRequestForCurrencyTypeUSDollar: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();

        public static string fileTC1516 = "T1516_GiftLog_ClientGiftPre_ApprovalPage_RecipientsExceedsYearlyGiftAllowance";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClientGiftPre_ApprovalPage_RecipientsExceedsYearlyGiftAllowance()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1516;
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
                homePage.SearchUserByGlobalSearch(fileTC1516, user);

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

                // Enter required details in client gift pre- approval page
                giftRequest.EnterDetailsGiftRequest(fileTC1516);
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
                string labelGiftAmtYTD = giftRequest.GetLabelNewGiftAmtYTD();
                string expectedLabelGiftAmtYTD = ReadExcelData.ReadData(excelPath, "GiftLog", 12);
                Assert.AreEqual(expectedLabelGiftAmtYTD, labelGiftAmtYTD);
                extentReports.CreateLog("Gift Label: " + labelGiftAmtYTD + " is displayed in Selected Recipient(s) table ");
                
                // Verify value of gift
                string valueOfGift = giftRequest.GetGiftValueInGiftAmtYTD();
                string valueOfGiftExl = ReadExcelData.ReadData(excelPath, "GiftLog", 3);
                Assert.AreEqual(valueOfGiftExl, valueOfGift);
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

                //Verify Submit Gift Request button visibile after click on revise request button
                Assert.IsTrue(giftRequest.IsSubmitGiftRequestButtonVisible());
                extentReports.CreateLog("Submit Gift Request button is visible upon click of revise request button ");

                //Verification of landing on Pre-approval page upon Click on Revise Request Button
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of revise request button ");

                //Verification of recipient exceeding yearly allowance for different currency types
                for (int row = 2; row <= 6; row++)
                {

                    //Navigate to Gift Request page
                    giftRequest.GoToGiftRequestsPage();
                    string giftRequestTitle1 = giftRequest.GetGiftRequestPageTitle();
                    Assert.AreEqual(giftRequestTitleExl, giftRequestTitle1);
                    extentReports.CreateLog("Page Title: " + giftRequestTitle1 + " is diplayed upon click of Gift Request link ");

                    // Enter required details in client gift pre- approval page
                    giftRequest.EnterDetailsGiftRequest(fileTC1516);
                    extentReports.CreateLog("details entered ");
                    //Select currency drop down

                    giftRequest.SelectCurrencyDrpDown(ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLog_Currency", row, 1));
                    string giftval = ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLog_Currency", row, 2);

                    
                    giftRequest.EnterGiftValue(giftval);
                    giftRequest.ClickAddRecipient();

                    giftRequest.AddRecipientToSelectedRecipients();
                    string newGIftAmtYTD = giftRequest.GetGiftValueInGiftAmtYTD();
                    extentReports.CreateLog("newGIftAmtYTD: " + newGIftAmtYTD + "is displaying");

                    // Verify currency of gift
                    string currencyCode1 = giftRequest.GetGiftCurrencyCode();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLog_Currency", row, 3), currencyCode1);
                    extentReports.CreateLog("Currency Code: " + currencyCode1 + " is displayed in Selected Recipient(s) table ");

                    //Verification of NewGiftAmtYTD Value turn to red to indicate Currency max limit Exceeded for Current Calendar Year
                    string colorOfGiftValue1 = giftRequest.GetGiftValueColorInGiftAmtYTD();
                    string colorOfGiftValueExl1 = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                    Assert.AreEqual(colorOfGiftValueExl1, colorOfGiftValue1);
                    extentReports.CreateLog("Color Of Gift Value: " + colorOfGiftValue1 + " is displayed in Selected Recipient(s) table ");

                    //Click on submit gift request
                    giftRequest.ClickSubmitGiftRequest();
                    string warningMessage1 = giftRequest.GetWarningMessageOnAmountLimitExceed();
                    //Verification of error message displaying on submit of gift request exceeds yearly gift allowance for currency type euro(not in france)
                    Assert.AreEqual(warningMessageExl, warningMessage1);
                    extentReports.CreateLog("Warning Message: " + warningMessage1 + " is displayed upon submitting a gift request with gift amount exceeding $100 ");

                    //CLick on submit request button
                    giftRequest.ClickSubmitRequestButton();
                    Assert.IsTrue(giftRequest.IsReturnToPreApprovalPageVisible());
                    extentReports.CreateLog("Return to pre approval page button is visible upon click of submit request button ");

                    //Verify congratulation message upon successful gift request completion
                    string congratulationMsg1 = giftRequest.GetCongratulationsMsg();
                    string congratulationMsgEx1l = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                    Assert.AreEqual(congratulationMsgEx1l, congratulationMsg1);
                    extentReports.CreateLog("Congratulations message: " + congratulationMsg1 + " in displayed upon successful submission of gift request ");
                    //Verify currency value
                    string currencyValue1 = giftApprove.GetGiftCurrencyCode();
                    //Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLog_Currency", row, 4), currencyValue1);
                    extentReports.CreateLog("Currency: " + currencyValue1 + " is listed on gift request submission detail page ");

                    string giftValueOnGiftDetail1 = giftApprove.GetGiftValue();
                    // Assert.AreEqual(currencyValue1+" "+ giftval, giftValueOnGiftDetail1);
                    extentReports.CreateLog("Gift Value: " + giftValueOnGiftDetail1 + " is listed on gift request submission detail page ");


                }
                    
               

                //Navigate to Gift Request page
                giftRequest.GoToGiftRequestsPage();

                //Switching From HL Force To GiftLog
                giftRequest.GoToGiftRequestPage();
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon switching from HL Force to Gift Log ");

                // Enter required details in client gift pre- approval page
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC1516);

                //Verify company name
                Assert.AreEqual(expectedCompanyName, actualRecipientCompanyName);
                extentReports.CreateLog("Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table ");

                //Verify recipient contact name
                Assert.AreEqual(expectedContactName, actualRecipientContactName);
                extentReports.CreateLog("Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table ");

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();
                Assert.AreEqual(expectedLabelGiftAmtYTD, labelGiftAmtYTD);
                extentReports.CreateLog("Gift Label: " + labelGiftAmtYTD + " is displayed in Selected Recipient(s) table ");

                //Verify value of gift
                Assert.AreEqual(valueOfGiftExl, valueOfGift);
                extentReports.CreateLog("Gift Value: " + labelGiftAmtYTD + " is displayed in Selected Recipient(s) table ");

                //Verify currency of gift
                Assert.AreEqual("USD", currencyCode);
                extentReports.CreateLog("Currency Code: " + currencyCode + " is displayed in Selected Recipient(s) table ");

                //Verification of NewGiftAmtYTD Value turn to red to indicate Currency max limit Exceeded for Current Calendar Year
                 Assert.AreEqual(colorOfGiftValueExl, colorOfGiftValue);
                extentReports.CreateLog("Color Of Gift Value:"+ colorOfGiftValue+ " is displayed in Selected Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                Assert.AreEqual(warningMessageExl, warningMessage);
                extentReports.CreateLog("Warning Message: " + warningMessage + " is displayed upon submitting a gift request with gift amount exceeding $100 ");

                //Verify revise request button visible
                Assert.IsTrue(giftRequest.IsReviseRequestButtonVisible());
                extentReports.CreateLog("Revise Request button is visible on warning message page ");

                //Verify submit request button visible
                Assert.IsTrue(giftRequest.IsSubmitRequestButtonVisible());
                extentReports.CreateLog("Submit Request button is visible on warning message page ");

                //CLick on submit request button
                giftRequest.ClickSubmitRequestButton();
                Assert.IsTrue(giftRequest.IsReturnToPreApprovalPageVisible());
                extentReports.CreateLog("Return to pre approval page button is visible upon click of submit request button ");

                //Verify congratulation message upon successful gift request completion
                string congratulationMsg = giftRequest.GetCongratulationsMsg();
                string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                //Verify Gift description 
                string giftDescriptionGiftRequestDetail = giftRequest.GetGiftDescriptionOnGiftRequestDetail();
             //   Assert.AreEqual(valGiftNameEntered, giftDescriptionGiftRequestDetail);
                extentReports.CreateLog("Gift Description: " + giftDescriptionGiftRequestDetail + " is listed on gift request submission detail page ");

                //Verify recipient name
                string RecipientOnGiftRequestDetail = giftRequest.GetRecipientNameOnGiftRequestDetail();
                string recipientName = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(recipientName, RecipientOnGiftRequestDetail);
                extentReports.CreateLog("Recipient Name: " + RecipientOnGiftRequestDetail + " is listed on gift request submission detail page ");

                //Verify submitted for
                string submittedForValue = giftApprove.GetvalueSubmittedFor();
                string submittedForValueExl = ReadExcelData.ReadData(excelPath, "GiftLog", 2);
               // Assert.AreEqual(submittedForValueExl, submittedForValue);
                extentReports.CreateLog("Submitted For: " + submittedForValue + " is listed on gift request submission detail page ");

                //Verify currency value
                string currencyValue = giftApprove.GetGiftCurrencyCode();
               // Assert.AreEqual("USD", currencyValue);
                extentReports.CreateLog("Currency: " + currencyValue + " is listed on gift request submission detail page ");

                string giftValueOnGiftDetail = giftApprove.GetGiftValue();
                string giftValueFromExl = ReadExcelData.ReadData(excelPath, "GiftLog", 3);
                //Assert.AreEqual("USD " + giftValueFromExl + "0", giftValueOnGiftDetail);
                extentReports.CreateLog("Gift Value: " + giftValueOnGiftDetail + " is listed on gift request submission detail page ");

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
