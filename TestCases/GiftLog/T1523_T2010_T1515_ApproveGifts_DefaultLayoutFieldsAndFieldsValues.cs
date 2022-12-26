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
    class T1523_T2010_T1515_ApproveGifts_DefaultLayoutFieldsAndFieldsValues : BaseClass
    {
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        ContactHomePage conHome = new ContactHomePage();
        ContactCreatePage createContact = new ContactCreatePage();

        ContactDetailsPage contactDetails = new ContactDetailsPage();
      
        public static string fileTC1523 = "T1523_GiftLog_ApproveGifts_DefaultLayoutFieldsAndFieldsValues";
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ApproveGifts_DefaultLayoutFieldsAndFieldsValues()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1523;
               
                Console.WriteLine(excelPath);
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC1523, user);

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

                giftRequest.GoToGiftRequestPage();
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                //Enter required details in client gift pre- approval page
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC1523);

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

                //Verify company name
                string selectedCompanyName = giftRequest.GetSelectedCompanyName();
                Assert.AreEqual(actualRecipientCompanyName, selectedCompanyName);
                extentReports.CreateLog("Company Name: " + selectedCompanyName + " in selected recipient(s) table matches with available company name listed in Available Recipient(s) table ");

                //Verify Current GIft Amount YTD
                string CurrentGiftAmtYTD1 = giftRequest.GetCurrentGiftAmtYTD();
                Assert.AreEqual(CurrentGiftAmtYTD1, "00.0");
                extentReports.CreateLog("CurrentGiftAmtYTD: " + CurrentGiftAmtYTD1 + " is displaying");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                string congratulationMsg = giftRequest.GetCongratulationsMsg();
                string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                //Switch from gift log to HL Force
                giftRequest.SwitchFromGiftLogToHLForce();
                usersLogin.UserLogOut();

                //Search compliance user by global search
                string userCompliance = ReadExcelData.ReadData(excelPath, "Users", 2);
                homePage.SearchUserByGlobalSearch(fileTC1523, userCompliance);

                //Verify searched user
                string userPeople2 = homePage.GetPeopleOrUserName();
                string userPeople2Exl = ReadExcelData.ReadData(excelPath, "Users", 2);
                Assert.AreEqual(userPeople2Exl, userPeople2);
                extentReports.CreateLog("User " + userPeople2 + " details are displayed ");

                //Login as Compliance User and validate the user
                usersLogin.LoginAsSelectedUser();
                string complianceUser = login.ValidateUser();
                string trimmedcomplianceUser = complianceUser.TrimEnd('.');
                string complianceUserExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                Assert.AreEqual(complianceUserExl.Contains(trimmedcomplianceUser), true);
                extentReports.CreateLog("Compliance User: " + trimmedcomplianceUser + " is able to login ");
                
                giftRequest.GoToGiftRequestPage();
                giftApprove.ClickApproveGiftsTab();
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                //Verification of default year selected in dropdown
                string getYear = DateTime.Today.ToString("yyyy");
                string defaultSelectedYear = giftApprove.GetDefaultSelectedYear();
                Assert.AreEqual(getYear, defaultSelectedYear);
                extentReports.CreateLog("Year: " + defaultSelectedYear + " is selected as default value in Year dropdown ");
                
                //Verification of default approved status
                string defaultApprovedStatus = giftApprove.GetDefaultSelectedApprovedStatus();
                Assert.AreEqual("Pending", defaultApprovedStatus);
                extentReports.CreateLog("ApprovedStatus: " + defaultApprovedStatus + " is selected as default value in Approved Status dropdown ");

                // Verification of text box for recipient name entry
                Assert.IsTrue(giftApprove.SearchTextBoxOfRecipientNameVisibility(), "Search text box corresponding to recipient name is visible ");
                extentReports.CreateLog("Search text box corresponding to recipient last name is visible ");

                //Verification of text box for approval denial comment
                Assert.IsTrue(giftApprove.TextBoxForApprovalDenialCommentVisibility());
                extentReports.CreateLog("Text box for Approval/Denial comment is visible ");

                //Verification of Approval / Denial Comment
                string labelAppDenialCommentsExl = ReadExcelData.ReadData(excelPath, "GiftLog", 12);
                string labelAppDenialComments = giftApprove.GetlabelApprovalDenialComments();
                Assert.AreEqual(labelAppDenialCommentsExl, giftApprove.GetlabelApprovalDenialComments());
                extentReports.CreateLog("Label: "+ labelAppDenialComments +" is displayed ");

                //Verification of approve selected button
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                extentReports.CreateLog("Approve Selected Button is displayed ");

                // Verification of deny selected button
                Assert.IsTrue(giftApprove.DenySelectedButtonVisibility());
                extentReports.CreateLog("Deny Selected Button is displayed ");

                //Search gift details by approved status
                giftApprove.SelectApprovedStatusCombo("Approved");
                String GiftStatus = giftApprove.GetGiftStatus();
                Assert.AreEqual("Approved", GiftStatus);
                extentReports.CreateLog(GiftStatus + "Filters are working correctly ");

                //Search gift details by denied status
                giftApprove.SelectApprovedStatusCombo("Denied");
                String GiftStatus1 = giftApprove.GetGiftStatus();
                Assert.AreEqual("Denied", GiftStatus1);
                extentReports.CreateLog(GiftStatus1 + "Filters are working correctly ");

                driver.Navigate().Refresh();

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastName(fileTC1523);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default ");

                giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);                
                extentReports.CreateLog("Gift Description link matches with Gift Name and clicked ");

                string ApprovedColumnValueInTable = giftApprove.GetDefaultValuesUnderApprovedColumnInTable();
                Assert.AreEqual(defaultApprovedStatus, ApprovedColumnValueInTable);
                extentReports.CreateLog("Grid details are filtered based on Recipient Last name ");

                giftApprove.ClickApproveSelectedButton();

                usersLogin.UserLogOut();

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser1 + " is able to login ");

                giftRequest.GoToGiftRequestPage();
                string giftRequestTitle1= giftRequest.GetGiftRequestPageTitle();
                string giftRequestTitleExl1 = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl1, giftRequestTitle1);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                //Enter required details in client gift pre- approval page
                string valGiftNameEntered1 = giftRequest.EnterDetailsGiftRequest(fileTC1523);

                //Verify company name
                string actualRecipientCompanyName1 = giftRequest.GetAvailableRecipientCompany();
                string expectedCompanyName1 = ReadExcelData.ReadData(excelPath, "GiftLog", 8);
                Assert.AreEqual(expectedCompanyName1, actualRecipientCompanyName1);
                extentReports.CreateLog("Company Name: " + actualRecipientCompanyName1 + " is listed in Available Recipient(s) table ");

                //Verify recipient contact name
                string actualRecipientContactName1 = giftRequest.GetAvailableRecipientName();
                string expectedContactName1 = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(expectedContactName1, actualRecipientContactName1);
                extentReports.CreateLog("Recipient Name: " + actualRecipientContactName1 + " is listed in Available Recipient(s) table ");

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();

                //Verify recipient name
                string selectedRecipientName1 = giftRequest.GetSelectedRecipientName();
                Assert.AreEqual(actualRecipientContactName1, selectedRecipientName1);
                extentReports.CreateLog("Recipient Name: " + selectedRecipientName1 + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Verify company name
                string selectedCompanyName1 = giftRequest.GetSelectedCompanyName();
                Assert.AreEqual(actualRecipientCompanyName1, selectedCompanyName1);
                extentReports.CreateLog("Company Name: " + selectedCompanyName1 + " in selected recipient(s) table matches with available company name listed in Available Recipient(s) table ");

                //Verify Current GIft Amount YTD
                string CurrentGiftAmtYTD=giftRequest.GetCurrentGiftAmtYTD();
                Assert.AreEqual(CurrentGiftAmtYTD, "USD 100.0");
                extentReports.CreateLog("CurrentGiftAmtYTD: "+ CurrentGiftAmtYTD+" is displaying");
                
                //Updating gift value to exceed current gift value
                giftRequest.EnterGiftValue(ReadExcelData.ReadData(excelPath, "GiftValue", 1));

                //Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                string warningMessage = giftRequest.GetWarningMessageOnAmountLimitExceed();
                string warningMessageExl = ReadExcelData.ReadData(excelPath, "GiftLog", 15);
                Assert.AreEqual(warningMessageExl, warningMessage);
                extentReports.CreateLog("Warning Message: " + warningMessage + " is displayed upon submitting a gift request with gift amount exceeding $100 ");

                usersLogin.UserLogOut();

                conHome.ClickContact();

                conHome.SearchContact(fileTC1523);

                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC1523, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                conHome.ClickContact();
                conHome.ClickAddContact();

                //Calling select record type and click continue function
                string contactType = ReadExcelData.ReadData(excelPath, "Contact", 7);
                conSelectRecord.SelectContactRecordType(fileTC1523, contactType);
                extentReports.CreateLog("user navigate to create contact page upon click of continue button ");

                createContact.CreateContact(fileTC1523);
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