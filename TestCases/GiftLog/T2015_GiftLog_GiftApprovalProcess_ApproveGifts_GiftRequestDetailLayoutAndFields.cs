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
    class T2015_GiftLog_GiftApprovalProcess_ApproveGifts_GiftRequestDetailLayoutAndFields : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();

        public static string fileTC2015 = "T2015_GiftLog_GiftApprovalProcess_ApproveGifts_GiftRequestDetailLayoutAndFields";
       
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GiftLog_GiftApprovalProcess_ApproveGifts_GiftRequestDetailLayoutAndFields()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2015;
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
                homePage.SearchUserByGlobalSearch(fileTC2015, user);

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
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC2015);

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

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                giftApprove.ClickSubmitRequest();
                string congratulationMsg = giftRequest.GetCongratulationsMsg();
                extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                usersLogin.UserLogOut();

                // Search Complaince user by global search
                string userCompliance = ReadExcelData.ReadData(excelPath, "Users", 2);
                homePage.SearchUserByGlobalSearch(fileTC2015, userCompliance);

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
                giftApprove.SearchByRecipientLastName(fileTC2015);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                //Approve the current year gift
                giftApprove.CompareAndClickGiftDesc(valGiftNameEntered);
                extentReports.CreateLog("Gift desc is clicked successfully ");

                //Validate Gift Name field and value
                Assert.IsTrue(giftApprove.VerifyGiftNameInGiftRequestDetails(fileTC2015, valGiftNameEntered));
                extentReports.CreateLog("Gift Name field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Gift Type field and value
                Assert.IsTrue(giftApprove.VerifyGiftTypeInGiftRequestDetails(fileTC2015));
                extentReports.CreateLog("Gift Type field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Recipient For Gift field and value
                Assert.IsTrue(giftApprove.VerifyRecipientForInGiftRequestDetails(fileTC2015));
                extentReports.CreateLog("Recipient For Gift field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Submitted For field and value
                Assert.IsTrue(giftApprove.VerifySubmittedForInGiftRequestDetails(fileTC2015));
                extentReports.CreateLog("Submitted For field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Vendor field and value
                Assert.IsTrue(giftApprove.VerifyVendorInGiftRequestDetails(fileTC2015));
                extentReports.CreateLog("Vendor field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Gift Value field and value
                Assert.IsTrue(giftApprove.VerifyGiftValueInGiftRequestDetails(fileTC2015));
                extentReports.CreateLog("Gift Value field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Currency field and value
                Assert.IsTrue(giftApprove.VerifyCurrencyInGiftRequestDetails(fileTC2015));
                extentReports.CreateLog("Currency field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate HL Relationship field and value
                Assert.IsTrue(giftApprove.VerifyHLRelationshipInGiftRequestDetails(fileTC2015));
                extentReports.CreateLog("HL Relationship field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Reason For Gift field and value
                Assert.IsTrue(giftApprove.VerifyReasonForGiftInGiftRequestDetails(fileTC2015));
                extentReports.CreateLog("Reason For Gift field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Approval Number field and value
                Assert.AreEqual("Pending", giftApprove.GetApprovalNumberFromGiftRequestDetails());
                extentReports.CreateLog("Approval Number field is displayed with value as Pending on approve gifts page. ");

                //Validate Created By field and value
                Assert.IsTrue(giftApprove.VerifyCreatedByInGiftRequestDetails(fileTC2015));
                extentReports.CreateLog("Created By field is displayed with correct value under Gift Request detail section on approve gifts page. ");

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
