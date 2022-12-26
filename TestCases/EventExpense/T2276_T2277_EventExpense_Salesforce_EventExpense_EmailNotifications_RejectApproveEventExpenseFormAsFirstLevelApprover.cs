using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.EventExpense;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.EventExpense
{
    class T2276_T2277_EventExpense_Salesforce_EventExpense_EmailNotifications_RejectApproveEventExpenseFormAsFirstLevelApprover : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        Outlook outlook = new Outlook();
        CoverageTeamDetail coverageTeamDetail = new CoverageTeamDetail();
        ExpenseRequestCreatePage expReqCreate = new ExpenseRequestCreatePage();
        ExpenseRequestDetailPage expReqDetail = new ExpenseRequestDetailPage();
        ExpenseRequestHomePage expRequest = new ExpenseRequestHomePage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC2276_T2277 = "T2276_T2277_RejectApproveEventExpenseFormAsFirstLevelApprover";
        public static string fileOutlook = "Outlook";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RejectAndApproveEventExpenseFormAsFirstLevelApprover()
        {
            try
            {                
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2276_T2277;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Handling salesforce Lightning
                login.HandleSalesforceLightningPage();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC2276_T2277, user);

                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string standardUser = login.ValidateUser();
                string standardUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(standardUserExl.Contains(standardUser), true);
                extentReports.CreateLog("Standard User: " + standardUser + " is able to login ");

                // Create Expense Request
                expReqCreate.CreateExpenseRequest(fileTC2276_T2277);
                extentReports.CreateLog("Expense request is created successfully ");

                //Validate Requestor value of expense request
                string requestor = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 3);
                Assert.AreEqual(requestor, expReqDetail.GetRequestor());
                extentReports.CreateLog("Requestor value is validated as " + expReqDetail.GetRequestor() + " ");

                //Validate status of expense request created
                Assert.AreEqual("Saved", expReqDetail.GetExpenseRequestStatus(0));
                extentReports.CreateLog("Expense request status is validated as " + expReqDetail.GetExpenseRequestStatus(0) + " after saving an expense request ");

                //Get expense pre approver number
                string expensePreAppNumber = expReqDetail.GetExpensePreApproverNumberFromDetail();

                //Validate event contact
                string eventContact = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 4);
                Assert.AreEqual(eventContact, expReqDetail.GetEventContact());
                extentReports.CreateLog("Expense request event contact is validated as " + expReqDetail.GetEventContact() + " ");

                //Validate product type from event expense detail page
                string productType = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 5);
                Assert.AreEqual(productType, expReqDetail.GetProductType());
                extentReports.CreateLog("Expense request product type is validated as " + expReqDetail.GetProductType() + " ");

                //Validate event name from event expense detail page
                string eventName = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 6);
                Assert.AreEqual(eventName, expReqDetail.GetEventName());
                extentReports.CreateLog("Expense request event name is validated as " + expReqDetail.GetEventName() + " ");

                //Validate event city from event expense detail page
                string eventCity = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 7);
                Assert.AreEqual(eventCity, expReqDetail.GetEventCity());
                extentReports.CreateLog("Expense request event city is validated as " + expReqDetail.GetEventCity() + " ");

                //Validate event LOB from event expense detail page
                string eventLOB = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 1);
                Assert.AreEqual(eventLOB, expReqDetail.GetLOB());
                extentReports.CreateLog("Expense request event LOB is validated as " + expReqDetail.GetLOB() + " ");

                //Validate event type from event expense detail page
                string eventType = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 2);
                Assert.AreEqual(eventType, expReqDetail.GetEventType());
                extentReports.CreateLog("Expense request event type is validated as " + expReqDetail.GetEventType() + " ");

                //Validate event format from event expense detail page
                string eventFormat = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 8);
                Assert.AreEqual(eventFormat, expReqDetail.GetEventFormat());
                extentReports.CreateLog("Expense request event format is validated as " + expReqDetail.GetEventFormat() + " ");

                //Click submit for approval button
                expReqDetail.ClickSubmitForApproval();
                extentReports.CreateLog("Submit for approval button is clicked ");

                // Click back to expense request list button
                expReqDetail.ClickBackToExpenseRequestList(0);
                extentReports.CreateLog("Click Back to expense request list button ");

                //Validate expense request submitted is displayed in list
                string recordStatus = expRequest.CompareNewExpenseRequestInMyRequest(expensePreAppNumber);
                Assert.AreEqual("Event Expense Pre Approver number matches in My Requests", recordStatus);
                extentReports.CreateLog("Verified newly submitted form is available in My requests Tab is successful");

                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateLog("Logout and close the browser ");
                
                //Launch outlook window
                OutLookInitialize();

                //Login into Outlook
                outlook.LoginOutlook(fileOutlook);
                string outlookLabel = outlook.GetLabelOfOutlook();
                Assert.AreEqual("Outlook", outlookLabel);
                extentReports.CreateLog("Verified and Validation is done for User is logged in to outlook ");

                //Selecting Expense Request Approval email
                outlook.SelectExpenseApprovalEmail();
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog("User is redirected to salesforce with "+driver.Title + " is displayed ");

                //Switch to Salesforce from outlook
                login.LoginAsExpenseRequestApprover(fileTC2276_T2277);
                extentReports.CreateLog("Verified and Validation of User being redirected to Event Expense Form upon successful authentication ");
               
                // Validate status of the event request on my request page in the request list
                string requestStat = expReqDetail.GetExpenseRequestStatus(1);
                Assert.AreEqual("Waiting for Approval", requestStat);
                extentReports.CreateLog("Expense request status is validated as " + requestStat + " ");

                //Verify Approve button 
                bool approveBtnStatus = expReqDetail.VerifyApproveButton();
                Assert.IsTrue(approveBtnStatus);
                extentReports.CreateLog("Approve button is available on expense request detail page ");

                //Verify Reject button 
                bool rejectBtnStatus = expReqDetail.VerifyRejectButton();
                Assert.IsTrue(rejectBtnStatus);
                extentReports.CreateLog("Reject button is available on expense request detail page ");

                //Verify request for more information button 
                bool requestForMoreInfoStatus = expReqDetail.VerifyRequestForMoreInformation();
                Assert.IsTrue(requestForMoreInfoStatus);
                extentReports.CreateLog("Request for more information button is available on expense request detail page ");

                //Reject expense request
                expReqDetail.RejectExpenseRequest(1);
                string statusExpenseRequestAfterRejection = expReqDetail.GetExpenseRequestStatus(1);
                Assert.AreEqual("Rejected", statusExpenseRequestAfterRejection);
                extentReports.CreateLog("Rejected expense request and expense request status validated as " + statusExpenseRequestAfterRejection + " ");

                //Validate expense request response under approval history section
                string expenseRequestStatusUnderApprovalHistory = expReqDetail.ValidateResponseUnderApprovalHistory();
                Assert.AreEqual("Rejected", expenseRequestStatusUnderApprovalHistory);
                extentReports.CreateLog("Expense request status under approval history section verified as " + expenseRequestStatusUnderApprovalHistory + " ");

                // Click back to expense request list button
                expReqDetail.ClickBackToExpenseRequestList(1);
                usersLogin.UserLogOut();
                extentReports.CreateLog("First level approver logout from salesforce ");

                //outlook.OutLookLogOut();
                driver.Quit();
               
                //Launch outlook window
                OutLookInitialize();

                //Login into Outlook
                outlook.LoginOutlook(fileOutlook);
                string outlookLabels = outlook.GetLabelOfOutlook();
                Assert.AreEqual("Outlook", outlookLabels);
                extentReports.CreateLog("Verified and Validation is done for User is logged in to outlook ");

                //Verify rejected email recieved on outlook
                string expenseReqNumberFromEmail = outlook.VerifyExpenseRequestForRejectedEmail(0);
                Assert.AreEqual(expensePreAppNumber, expenseReqNumberFromEmail);
                extentReports.CreateLog("Rejection email is recieved with expense RequestNumber "+ expenseReqNumberFromEmail +" ");

               // outlook.OutLookLogOut();
                driver.Quit();

                /**Resubmit rejected expense request for approval ***/
                Initialize();
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                
                // Search standard user by global search
                homePage.SearchUserByGlobalSearch(fileTC2276_T2277, user);

                //Verify searched user
                string people = homePage.GetPeopleOrUserName();
                Assert.AreEqual(userPeopleExl, people);
                extentReports.CreateLog("User " + people + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string StandardUser = login.ValidateUser();
                Assert.AreEqual(standardUserExl.Contains(StandardUser), true);
                extentReports.CreateLog("Standard User: " + StandardUser + " is able to login ");

                //Search expense request using approver number by standard user
                string LOB = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 1);
                expRequest.SearchExpenseRequestByApproverNumber(expensePreAppNumber, eventLOB);
                extentReports.CreateLog("Expense request searched by user ");

                //Validate expense request status on expense detail page after searching by standard user
                string statusExpenseRequestRejected = expReqDetail.GetExpenseRequestStatus(1);
                Assert.AreEqual("Rejected", statusExpenseRequestRejected);
                extentReports.CreateLog("Expense request status validated as " + statusExpenseRequestRejected + " ");

                //Edit expense request
                expReqDetail.EditExpenseRequest(fileTC2276_T2277);
                extentReports.CreateLog("Expense request edit and updated ");

                //Validate updated event city from event expense detail page
                string updatedEventCity = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 17);
                Assert.AreEqual(updatedEventCity, expReqDetail.GetEventCity());
                extentReports.CreateLog("Expense request event city is validated as " + expReqDetail.GetEventCity() + " ");

                //Click on submit for approval button
                expReqDetail.ClickSubmitForApproval();
                extentReports.CreateLog("Expense request resubmitted for approval ");

                //Validate expense request status after resubmitting for approval
                string statusResubmittingForApproval = expReqDetail.GetExpenseRequestStatus(1);
                Assert.AreEqual("Waiting for Approval", statusResubmittingForApproval);
                extentReports.CreateLog("Expense request status validated as " + statusResubmittingForApproval + " ");

                //Click on back to expense request button
                expReqDetail.ClickBackToExpenseRequestList(1);
                usersLogin.UserLogOut();

                extentReports.CreateLog("Event Expense requestor logout after resubmitting the event expense request which was rejected ");
                driver.Quit();
                extentReports.CreateLog("Close Browser ");

                //Launch outlook 
                OutLookInitialize();
                outlook.LoginOutlook(fileOutlook);
                string outlookTitle = outlook.GetLabelOfOutlook();
                Assert.AreEqual("Outlook", outlookTitle);
                extentReports.CreateLog("Verified and Validation is done for User is logged in to outlook ");

                //Select expense request approval email 
                outlook.SelectExpenseApprovalEmail();
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog("User is redirected to salesforce with " + driver.Title + " is displayed ");

                //Login as Expense Request approver
                login.LoginAsExpenseRequestApprover(fileTC2276_T2277);
                extentReports.CreateLog("Login into outlook as first level approver of event expense requested ");

                // Validate status of the event request on my request page in the request list
                string requestStatus = expReqDetail.GetExpenseRequestStatus(1);
                Assert.AreEqual("Waiting for Approval", requestStatus);
                extentReports.CreateLog("Expense request status is verified as " + requestStat + " ");
                
                //Approve expense request
                expReqDetail.ApproveExpenseRequest(1);
                extentReports.CreateLog("Approved expense request ");

                // Validate delete button and event expense response under approval history section
                Assert.AreEqual("Approved", expReqDetail.ValidateDeleteAndApproval());
                extentReports.CreateLog("Validated delete button and Approved value in Approval History section "); 

                // Delete expense request
                expReqDetail.DeleteExpenseRequest();
                //Validate status of expense request
                string statusExpenseRequestAfterDelete = expReqDetail.GetExpenseRequestStatus(1);
                Assert.AreEqual("Deleted", statusExpenseRequestAfterDelete);
                extentReports.CreateLog("The current status of the expense request is " + statusExpenseRequestAfterDelete + " after deletion of expense request ");

                //Return back to expense request list
                expReqDetail.ClickBackToExpenseRequestList(1);
                extentReports.CreateLog("Return back to expense request list page ");
                usersLogin.UserLogOut();
                extentReports.CreateLog("Logout from salesforce as approver of event expense requested ");

               // outlook.OutLookLogOut();
                driver.Quit();

                //Launch outlook 
                OutLookInitialize();
                outlook.LoginOutlook(fileOutlook);
                extentReports.CreateLog("Login into outlook to verfied approved email recieved ");

                //Verify approval email 
                string expenseReqNumberFromApprovedEmail = outlook.VerifyExpenseRequestForApprovedEmail(0);
                Assert.AreEqual(expensePreAppNumber, expenseReqNumberFromApprovedEmail);
                extentReports.CreateLog("Approval email is recieved with expense RequestNumber " + expenseReqNumberFromApprovedEmail + " ");

               // outlook.OutLookLogOut();
                extentReports.CreateLog("Logout from outlook as approver of event expense requested ");
                driver.Quit();

                extentReports.CreateLog("Close the browser ");
            }

            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                //usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}