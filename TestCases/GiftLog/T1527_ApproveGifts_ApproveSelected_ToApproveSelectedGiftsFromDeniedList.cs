using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.GiftLog;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.GiftLog
{
    class T1527_ApproveGifts_ApproveSelected_ToApproveSelectedGiftsFromDeniedList : BaseClass
    {
        ContactCreatePage createContact = new ContactCreatePage();
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftSubmittedPage giftsSubmit = new GiftSubmittedPage();
        GiftRequestEditPage giftEdit = new GiftRequestEditPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();

        public static string fileTC1527 = "T1527_ApproveGifts_ApproveSelected_ToApproveSelectedGiftsFromDeniedList";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ApproveGifts_ApproveSelected_ToApproveSelectedGiftsFromDeniedList()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1527;
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
                string userCompliance = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC1527, userCompliance);

                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as Compliance User and validate the user
                usersLogin.LoginAsSelectedUser();
                string complianceUser = login.ValidateUser();
                string trimmedcomplianceUser = complianceUser.TrimEnd('.');
                string complianceUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(complianceUserExl.Contains(trimmedcomplianceUser), true);
                extentReports.CreateLog("Compliance User: " + trimmedcomplianceUser + " is able to login ");

                //Navigate to Gift Request page
                giftRequest.GoToGiftRequestsPage();
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                //Enter required details in client gift pre- approval page
                giftRequest.SetDesiredDateToCurrentDate();
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC1527);

                //Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                giftApprove.ClickSubmitRequest();
                string congratulationMsg = giftRequest.GetCongratulationsMsg();
                string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateLog(congratulationMsg + " message is displayed upon successful submission of gift request. ");
                
                //Click on approve gifts tab
                giftApprove.ClickApproveGiftsTab();
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastName(fileTC1527);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                //Click on Deny selected button without selecting atleast one gift
                giftApprove.ClickDenySelectedButton();

                //validate the error message
                String ErrorMsgDenyGiftText = giftApprove.ErrorMsgForApproveGift();
                Assert.IsTrue(ErrorMsgDenyGiftText.Contains("You must select at least one gift to deny."));
                extentReports.CreateLog("Error message: " + ErrorMsgDenyGiftText + " is displaying. ");

                //Select a gift to Deny
                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);

                //Click on Deny Selected Button to Deny the gift
                giftApprove.SetApprovalDenialComments();
                giftApprove.ClickDenySelectedButton();
                extentReports.CreateLog("Deny selected button is clicked successfully. ");
                
                //Searching the Denied gift under Denied Status
                giftApprove.SearchByStatus(fileTC1527, "Denied");

                //Click on Approve selected button without selecting atleast one gift
                giftApprove.ClickApproveSelectedButton();

                //validate the error message
                String ErrorMsgApproveGiftText = giftApprove.ErrorMsgForApproveGift();
                Assert.IsTrue(ErrorMsgApproveGiftText.Contains("You must select at least one gift to approve."));
                extentReports.CreateLog("Error message: " + ErrorMsgApproveGiftText + " is displaying. ");

                //Select a gift to Approve
                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                giftApprove.ClickApproveSelectedButton();
                extentReports.CreateLog("Gift approved from denied list. ");

                //Searching the Approved gift under Approved Status
                giftApprove.SearchByStatus(fileTC1527, "Approved");

                //Validate if the gift is moved under approved status
                Assert.IsTrue(giftApprove.ValidateGiftDescWithGiftName(valGiftNameEntered));
                extentReports.CreateLog("Gift moved from denied list to approved list successfully. ");

                //Getting the updated status of gift
                string txtStatus = giftApprove.GetStatusCompareGiftDescWithGiftName(valGiftNameEntered);

                //Verification of gift updated status displaying in Approved list
                Assert.AreEqual("Approved", txtStatus);
                extentReports.CreateLog(txtStatus + " is displaying in gift status. ");

                //Navigate to Gift Request page
                giftRequest.GoToGiftRequestsPage();
                string giftRequestTitle1 = giftRequest.GetGiftRequestPageTitle();
                extentReports.CreateLog("Page Title: " + giftRequestTitle1 + " is diplayed upon click of Gift Request link ");

                //Enter required details in client gift pre- approval page
                giftRequest.SetDesiredDateToCurrentDate();
                string valGiftNameEntered1 = giftRequest.EnterDetailsGiftRequest(fileTC1527);
                giftRequest.EnterGiftValue(ReadExcelData.ReadData(excelPath, "GiftValue", 1));

                //Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                giftRequest.ClickSubmitRequestButton();
                string congratulationMsg1 = giftRequest.GetCongratulationsMsg();
                Assert.AreEqual(congratulationMsgExl, congratulationMsg1);
                extentReports.CreateLog(congratulationMsg1 + " in displayed upon successful submission of gift request ");

                //Click on approve gifts tab
                giftApprove.ClickApproveGiftsTab();
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastName(fileTC1527);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                //Click on Deny selected button without selecting atleast one gift
                giftApprove.ClickDenySelectedButton();

                //validate the error message
                String ErrorMsgDenyGiftText1 = giftApprove.ErrorMsgForApproveGift();
                Assert.IsTrue(ErrorMsgDenyGiftText1.Contains("You must select at least one gift to deny."));
                extentReports.CreateLog("Error message: " + ErrorMsgDenyGiftText1 + " is displaying. ");

                //Select a gift to Deny
                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered1);

                //Click on Deny Selected Button to Deny the gift
                giftApprove.SetApprovalDenialComments();
                giftApprove.ClickDenySelectedButton();
                extentReports.CreateLog("Gift exceeding max limit value is denied successfully. ");

                //Searching the Denied gift under Denied Status
                giftApprove.SearchByStatus(fileTC1527, "Denied");

                //Click on Approve selected button without selecting atleast one gift
                giftApprove.ClickApproveSelectedButton();

                //validate the error message
                String ErrorMsgApproveGiftText1 = giftApprove.ErrorMsgForApproveGift();
                Assert.IsTrue(ErrorMsgApproveGiftText1.Contains("You must select at least one gift to approve."));
                extentReports.CreateLog("Error message: " + ErrorMsgApproveGiftText1 + " is displaying. ");

                //Select a gift to Approve
                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered1);
                giftApprove.ClickApproveSelectedButton();
                extentReports.CreateLog("Gift approved from denied list. ");

                //Searching the Approved gift under Approved Status
                giftApprove.SearchByStatus(fileTC1527, "Approved");

                //Validate if the gift is moved under approved status
                Assert.IsTrue(giftApprove.ValidateGiftDescWithGiftName(valGiftNameEntered1));
                extentReports.CreateLog("Gift moved from denied list to approved list successfully. ");

                //Getting the updated status of gift
                string txtStatus1 = giftApprove.GetStatusCompareGiftDescWithGiftName(valGiftNameEntered1);

                //Verification of gift updated status displaying in Approved list
                Assert.AreEqual("Approved", txtStatus1);
                extentReports.CreateLog(txtStatus1 + " is displaying in gift status. ");

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
