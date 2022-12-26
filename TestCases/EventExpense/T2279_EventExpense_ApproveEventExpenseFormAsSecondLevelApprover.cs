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
    class T2279_EventExpense_ApproveEventExpenseFormAsSecondLevelApprover : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        CoverageTeamDetail coverageTeamDetail = new CoverageTeamDetail();
        ExpenseRequestCreatePage expReqCreate = new ExpenseRequestCreatePage();
        ExpenseRequestDetailPage expReqDetail = new ExpenseRequestDetailPage();
        ExpenseRequestHomePage expRequest = new ExpenseRequestHomePage();
        UsersLogin usersLogin = new UsersLogin();
        Outlook outlook = new Outlook();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC2279 = "T2279_ApproveEventExpenseFormAsSecondLevelApprover";
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
        public void ApproveEventExpenseFormAsSecondLevelApprover()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2279;
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
                homePage.SearchUserByGlobalSearch(fileTC2279, user);

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
                expReqCreate.CreateExpenseRequest(fileTC2279);
                extentReports.CreateLog("Expense request is created successfully ");

                //Validate Requestor value of expense request
                string requestor = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 3);
                Assert.AreEqual(requestor, expReqDetail.GetRequestor());
                extentReports.CreateLog("Requestor value is validated as " + expReqDetail.GetRequestor() + " ");

                //Validate status of expense request created
                Assert.AreEqual("Saved", expReqDetail.GetExpenseRequestStatus(0));
                extentReports.CreateLog("Expense request status is validated as " + expReqDetail.GetExpenseRequestStatus(0) + " ");

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
                extentReports.CreateLog("User is redirected to salesforce with " + driver.Title + " is displayed ");

                //Switch to Salesforce from outlook
                login.LoginAsFirstLevelExpenseRequest(fileTC2279);
                extentReports.CreateLog("Verified and Validation of User being redirected to Event Expense Form upon successful authentication ");

                // Validate status of the event request on my request page in the request list
                string requestStat = expReqDetail.GetExpenseRequestStatus(1);
                Assert.AreEqual("Waiting for Approval", requestStat);
                extentReports.CreateLog("Expense request status is validated as " + requestStat + " ");

                //Approve expense request
                expReqDetail.ApproveExpenseRequest(1);
                extentReports.CreateLog("Approved expense request ");

                // Validate delete button and event expense response under approval history section
                Assert.AreEqual("Approved", expReqDetail.ValidateDeleteAndApprovalDualApprover());
                extentReports.CreateLog("Validated delete button and Approved value in Approval History section ");

                expReqDetail.ClickBackToExpenseRequestList(1);
                extentReports.CreateLog("Click Back to expense request list button ");
                usersLogin.UserLogOut();

                extentReports.CreateLog("Logout from salesforce as first level approver of event expense requested ");
                //outlook.OutLookLogOut();
                extentReports.CreateLog("Logout from outlook as first level approver of event expense requested ");

                driver.Quit();
                extentReports.CreateLog("Logout and close the browser ");

                OutLookInitialize();
                outlook.LoginOutlook(fileOutlook);
                extentReports.CreateLog("Verified and Validation is done for User being redirected to salesforce login ");
                outlook.SelectSecondLevelExpenseApprovalEmail();

                login.LoginAsExpenseRequestApprover(fileTC2279);
                extentReports.CreateLog("Verified and Validation of User being redirected to Event Expense Form upon successful authentication ");
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
                extentReports.CreateLog("Logout from salesforce as second level approver of event expense requested ");
                //outlook.OutLookLogOut();
                extentReports.CreateLog("Logout from outlook as second level approver of event expense requested ");
                driver.Quit();

                //Launch outlook 
                OutLookInitialize();
                outlook.LoginOutlook(fileOutlook);
                extentReports.CreateLog("Login into outlook to verfied approved email recieved ");

                //Verify approval email 
                string expenseReqNumberFromApprovedEmail = outlook.VerifyExpenseRequestForApprovedEmail(0);
                Assert.AreEqual(expensePreAppNumber, expenseReqNumberFromApprovedEmail);
                extentReports.CreateLog("Approval email is recieved with expense RequestNumber " + expenseReqNumberFromApprovedEmail + " ");

                //outlook.OutLookLogOut();
                extentReports.CreateLog("Logout from outlook as approver of event expense requested ");
                driver.Quit();
                extentReports.CreateLog("Close the browser ");               
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