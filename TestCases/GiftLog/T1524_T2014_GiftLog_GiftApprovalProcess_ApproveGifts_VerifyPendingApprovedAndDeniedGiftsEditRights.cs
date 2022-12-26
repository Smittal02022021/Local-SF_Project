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
    class T1524_T2014_GiftLog_GiftApprovalProcess_ApproveGifts_VerifyPendingApprovedAndDeniedGiftsEditRights : BaseClass
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

        public static string fileTC2014 = "T2014_GiftLog_GiftApprovalProcess_ApproveGifts_VerifyPendingApprovedAndDeniedGiftsEditRights";
       
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GiftLog_GiftApprovalProcess_ApproveGifts_VerifyPendingApprovedAndDeniedGiftsEditRights()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2014;
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
                homePage.SearchUserByGlobalSearch(fileTC2014, userCompliance);

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
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                // Enter required details in client gift pre- approval page
                giftRequest.SetDesiredDateToCurrentDate();
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC2014);
                
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

                giftApprove.ClickApproveGiftsTab();
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastName(fileTC2014);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default ");

                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                Thread.Sleep(300);
                extentReports.CreateLog("Gift Description link matches with Gift Name and clicked ");
                
                //Verify edit button is visible
                Assert.IsTrue(giftApprove.EditButtonVisibility());
                extentReports.CreateLog("Edit button is visible in Gift Request Detail section upon click of gift description link ");

                //Verify delete button is not visible
                Assert.IsFalse(giftApprove.DeleteButtonVisibility());
                extentReports.CreateLog("Delete button is not visible in Gift Request Detail section upon click of gift description link ");

                //Click edit button
                giftsSubmit.ClickEditButton();
                string giftEditTitle = giftEdit.GetGiftRequestEditTitle();
                string giftEditTitleExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 8);
                Assert.AreEqual(giftEditTitleExl, giftEditTitle);
                extentReports.CreateLog("Gift Edit Page Title: " + giftEditTitle + " in displayed upon upon click of edit button ");

                //Verify gift name field is editable
                Assert.AreEqual("Element is editable",giftEdit.VerifyGiftNameEditable());
                extentReports.CreateLog("Field: Gift Name is editable on gift request edit page ");

                //Verify gift type field is editable
                Assert.AreEqual("Element is editable", giftEdit.VerifyGiftTypeEditable());
                extentReports.CreateLog("Field: Gift Type is editable on gift request edit page ");

                //Verify currency field is editable
                Assert.AreEqual("Element is editable", giftEdit.VerifyCurrencyEditable());
                extentReports.CreateLog("Field: Currency is editable on gift request edit page ");

                //Verify HL relationship field is editable
                Assert.AreEqual("Element is editable", giftEdit.VerifyHLRelationshipEditable());
                extentReports.CreateLog("Field: HL Relationship is editable on gift request edit page ");

                //Verify vendor field is editable
                Assert.AreEqual("Element is editable", giftEdit.VerifyVendorEditable());
                extentReports.CreateLog("Field: Vendor is editable on gift request edit page ");

                //Verify gift value field is editable
                Assert.AreEqual("Element is editable", giftEdit.VerifyGiftValueEditable());
                extentReports.CreateLog("Field: Gift Value is editable on gift request edit page ");

                //Verify desired date field is editable
                Assert.AreEqual("Element is editable", giftEdit.VerifyDesiredDateEditable());
                extentReports.CreateLog("Field: Desired Date is editable on gift request edit page ");

                //Click on approve gifts tab
                giftApprove.ClickApproveGiftsTab();
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastName(fileTC2014);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                // Compare gift name with gift description available
                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                extentReports.CreateLog("Gift Description link matches with Gift Name and checkbox clicked ");

                // Click on approve selected button
                giftApprove.SetApprovalDenialComments();
                giftApprove.ClickApproveSelectedButton();
                extentReports.CreateLog("Approve selected button is clicked successfully ");

                // Search gifts by apporved status
                giftApprove.SearchByStatus(fileTC2014, "Approved");
                extentReports.CreateLog("Gift List table is displayed with approved status upon search by selecting 'Approved' option in Approved Status ");

                // Compare gift name with gift description available
                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                extentReports.CreateLog("Gift Description link matches with Gift Name and checkbox clicked ");

                //Click edit button
                giftsSubmit.ClickEditButton();
                string giftEditTitle2 = giftEdit.GetGiftRequestEditTitle();
                Assert.AreEqual(giftEditTitleExl, giftEditTitle2);
                extentReports.CreateLog("Gift Edit Page Title: " + giftEditTitle + " in displayed upon upon click of edit button ");
                
                //Verify gift value field is editable 
                Assert.AreEqual("Element is editable", giftEdit.VerifyGiftValueAfterGiftApproveEditable());
                extentReports.CreateLog("Field: Gift Name is editable on gift request edit page ");

                //Verify gift value field is editable 
                Assert.AreEqual("Element is editable", giftEdit.VerifyApporvedDropDownEditable());
                extentReports.CreateLog("Field: Gift Value is editable on gift request edit page ");
                
                //Verify Apporval Comments field is editable
                Assert.AreEqual("Element is editable", giftEdit.VerifyApporvalCommentsEditable());
                extentReports.CreateLog("Field: Apporval Comments is editable on gift request edit page ");

                //Verify gift selected view
                Assert.AreEqual("Approved", giftEdit.GetGiftSelectedView());
                extentReports.CreateLog("Request Edit Page is displayed for the gift selected with Approved View ");

                //Click on Approve Gifts tab
                giftApprove.ClickApproveGiftsTab();
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                // Search gifts by apporved status  
                giftApprove.SearchByStatus(fileTC2014,"Approved");
                extentReports.CreateLog("Gift List table is displayed with approved status upon search by selecting 'Approved' option in Approved Status ");

                // Compare gift name with gift description available
                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                extentReports.CreateLog("Gift Description link matches with Gift Name and checkbox clicked ");

                // Verification of deny selected button
                Assert.IsTrue(giftApprove.DenySelectedButtonVisibility());
                extentReports.CreateLog("Deny Selected Button is displayed ");

                //Click on deny selected button
                giftApprove.SetApprovalDenialComments();
                giftApprove.ClickDenySelectedButton();
                extentReports.CreateLog("Click on Deny Selected Button successfully ");
                
                // Search gifts by denied status  
                giftApprove.SearchByStatus(fileTC2014, "Denied");
                extentReports.CreateLog("Gift List table is displayed with Denied status upon search by selecting 'Denied' option in Approved Status ");
                
                // Compare gift name with gift description available
                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                extentReports.CreateLog("Gift Description link matches with Gift Name and checkbox clicked ");

                //Click edit button
                giftsSubmit.ClickEditButton();
                string giftEditTitle3 = giftEdit.GetGiftRequestEditTitle();
                Assert.AreEqual(giftEditTitleExl, giftEditTitle3);
                extentReports.CreateLog("Gift Edit Page Title: " + giftEditTitle + " in displayed upon upon click of edit button ");

                //Verify approved dropdown is editable
                Assert.AreEqual("Element is editable", giftEdit.VerifyApporvedDropDownEditable());
                extentReports.CreateLog("Field: Approved is editable on gift request edit page ");

                //Verify approval comments is editable
                Assert.AreEqual("Element is editable", giftEdit.VerifyApporvalCommentsEditable());
                extentReports.CreateLog("Field: Approval comments is editable on gift request edit page ");

                //Verify gifts list is displayed with denied view
                Assert.AreEqual("Denied", giftEdit.GetGiftSelectedView());
                extentReports.CreateLog("Request Edit Page is displayed for the gift selected with Denied View ");

                //Navigate to Gift Request page
                giftRequest.GoToGiftRequestsPage();
              
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                // Enter required details in client gift pre- approval page
                giftRequest.SetDesiredDateToCurrentDate();
                string valGiftNameEntered1 = giftRequest.EnterDetailsGiftRequest(fileTC2014);
                giftRequest.EnterGiftValue(ReadExcelData.ReadData(excelPath, "GiftValue", 1));

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();
                
                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                giftRequest.ClickSubmitRequestButton();
                string congratulationMsg1 = giftRequest.GetCongratulationsMsg();
                Assert.AreEqual(congratulationMsgExl, congratulationMsg1);
                extentReports.CreateLog("Congratulations message: " + congratulationMsg1 + " in displayed upon successful submission of gift request ");
                //Click on approve gifts tab
                giftApprove.ClickApproveGiftsTab();
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastName(fileTC2014);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");
                               
                // Click on approve selected button
                giftApprove.ClickApproveSelectedButton();
                extentReports.CreateLog("Approve selected button is clicked successfully ");

                String ErrorMsgApproveGiftText = giftApprove.ErrorMsgForApproveGift();
                Assert.IsTrue(ErrorMsgApproveGiftText.Contains("You must select at least one gift to approve."));
                extentReports.CreateLog("Error message:"+ErrorMsgApproveGiftText+ " is displaying ");

                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered1);
                giftApprove.ClickApproveSelectedButton();

                String ErrorMsgApprovalComment = giftApprove.ErrorMsgApprovalComment();
                extentReports.CreateLog(ErrorMsgApprovalComment);
                Thread.Sleep(1000);
                Assert.IsTrue(ErrorMsgApprovalComment.Contains("You MUST enter an Approval Comment to exceed the yearly limit. Recipients exceeding yearly limit"));
                extentReports.CreateLog("Error message:"+ErrorMsgApprovalComment + " is displaying ");

                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered1);
                giftApprove.SetApprovalDenialComments();
                giftApprove.ClickApproveSelectedButton();

                giftApprove.SearchByStatus(fileTC2014, "Approved");
                
                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered1);

                //Click on deny selected button
                giftApprove.ClickDenySelectedButton();
                extentReports.CreateLog("Click on Deny Selected Button successfully ");
                
                usersLogin.UserLogOut();

                conHome.ClickContact();
                
                conHome.SearchContact(fileTC2014);
                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC2014, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                conHome.ClickContact();
                conHome.ClickAddContact();

                // Calling select record type and click continue function
                string contactType = ReadExcelData.ReadData(excelPath, "Contact", 7);
                conSelectRecord.SelectContactRecordType(fileTC2014, contactType);
                extentReports.CreateLog("user navigate to create contact page upon click of continue button ");

                createContact.CreateContact(fileTC2014);
                extentReports.CreateLog("External Contact created ");
                
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
