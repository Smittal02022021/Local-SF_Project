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
    class T2012_GiftLog_GiftRequestProcess_GiftSubmitted_GiftRequestDetail_VerifyEditAndDeleteFunctionality : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        GiftSubmittedPage giftsSubmit = new GiftSubmittedPage();
        GiftRequestEditPage giftEdit = new GiftRequestEditPage();

        public static string fileTC2012 = "T2012_GiftLog_GiftRequestProcess_GiftSubmitted_GiftRequestDetail_VerifyEditAndDeleteFunctionality";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GiftRequestProcess_GiftSubmitted_GiftRequestDetail_VerifyEditAndDeleteFunctionality()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2012;
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
                homePage.SearchUserByGlobalSearch(fileTC2012, user);

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
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC2012);

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
                
                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                giftApprove.ClickSubmitRequest();
                string congratulationMsg = giftRequest.GetCongratulationsMsg();
                string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                giftsSubmit.GoToGiftsSubmittedPage();
                giftsSubmit.CompareAndClickGiftDescWithGiftName(valGiftNameEntered);

                string giftDetailTitle = giftsSubmit.GetGiftRequestDetailTitle();
                string giftDetailTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 12);
                Assert.AreEqual(giftDetailTitleExl, giftDetailTitle);
                extentReports.CreateLog("Gift Detail Title: " + giftDetailTitle + " in displayed upon click of Gift description link ");

                giftsSubmit.ClickEditButton();
                string giftEditTitle = giftEdit.GetGiftRequestEditTitle();
                string giftEditTitleExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 8);
                Assert.AreEqual(giftEditTitleExl, giftEditTitle);
                extentReports.CreateLog("Gift Edit Page Title: " + giftEditTitle + " in displayed upon upon click of edit button ");

                Assert.IsTrue(giftEdit.ValidateMandatoryFields());
                extentReports.CreateLog("Verification of on the Edit page -the fields “Gift Name, Gift Type, HL Relationship, Gift Value, Desired Date , Vendor, Reason For Gift, “ are displayed as required fields ");

                string UpdatedGiftNameEntered = giftEdit.EnterDetailsGiftEditRequest(fileTC2012);
                
                string successMsg = giftEdit.GetSuccessGiftUpdateMessage();
                Assert.AreEqual("Success:\r\nYour Gift has been updated.", successMsg);
                extentReports.CreateLog("Success Message: "+ successMsg+" is displayed upon successful editing gift details ");

                giftEdit.ClickCancelButton();
                Assert.IsTrue(giftsSubmit.tableListPresent());
                extentReports.CreateLog("Gift Submitted Page is displayed with table upon click of cancel button ");

                giftsSubmit.CompareAndClickGiftDescWithGiftName(UpdatedGiftNameEntered);

                string giftDetailTitles = giftsSubmit.GetGiftRequestDetailTitle();
                Assert.AreEqual(giftDetailTitleExl, giftDetailTitles);
                extentReports.CreateLog("Gift Detail Title: " + giftDetailTitles + " in displayed upon click of Gift description link ");

                string giftTypeEdit = giftsSubmit.GetGiftTypeUpdatedValue();
                string giftTypeEditExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 1);
                Assert.AreEqual(giftTypeEditExl, giftTypeEdit);
                extentReports.CreateLog("Updated Gift Type: " + giftTypeEdit + " is matching with the value of gift type entered on gift edit page ");

                string giftHLRelationship = giftsSubmit.GetHLRelationshipUpdatedValue();
                string giftHLRelationshipExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 4);
                Assert.AreEqual(giftHLRelationshipExl, giftHLRelationship);
                extentReports.CreateLog("Updated HL Relationship: " + giftHLRelationship + " is matching with the value of HL Relationship entered on gift edit page ");

                string giftValueEdit = giftsSubmit.GetGiftValueUpdated();
                string giftValueEditExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 6);
                Assert.AreEqual("USD "+giftValueEditExl, giftValueEdit);
                extentReports.CreateLog("Updated gift value: " + giftValueEdit + " is matching with the value of Gift value entered on gift edit page ");

                string giftReasonEdit = giftsSubmit.GetGiftReasonUpdatedValue();
                string giftReasonEditExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 5);
                Assert.AreEqual(giftReasonEditExl, giftReasonEdit);
                extentReports.CreateLog("Updated gift reason: " + giftReasonEdit + " is matching with the value of Gift reason entered on gift edit page ");

                giftsSubmit.ClickEditButton();
                string giftEditTitle2 = giftEdit.GetGiftRequestEditTitle();
                Assert.AreEqual(giftEditTitleExl, giftEditTitle2);
                extentReports.CreateLog("Gift Edit Page Title: " + giftEditTitle2 + " in displayed upon upon click of edit button ");

                giftEdit.ClickNewGiftRequest();
                string newGiftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                Assert.AreEqual(giftRequestTitleExl, newGiftRequestTitle);
                extentReports.CreateLog("Page Title: " + newGiftRequestTitle + " is displayed upon click of new gift request button ");

                giftsSubmit.GoToGiftsSubmittedPage();
                giftsSubmit.CompareAndClickGiftDescWithGiftName(UpdatedGiftNameEntered);
                string giftDetailsTitle = giftsSubmit.GetGiftRequestDetailTitle();
                Assert.AreEqual(giftDetailTitleExl, giftDetailsTitle);
                extentReports.CreateLog("Gift Detail Title: " + giftDetailTitle + " in displayed upon click of Gift description link ");

                giftsSubmit.DeleteGiftSubmitted();
                string giftRequestTitles = giftRequest.GetGiftRequestPageTitle();
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitles);
                extentReports.CreateLog("Page Title: " + giftRequestTitles + " is displayed upon deletion of Gift Request ");


                giftsSubmit.GoToGiftsSubmittedPage();
                giftsSubmit.CompareAndClickGiftDescWithGiftName(UpdatedGiftNameEntered);
                extentReports.CreateLog("Selected Gift is not listed under Gift Submitted as part of delete functionality ");

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