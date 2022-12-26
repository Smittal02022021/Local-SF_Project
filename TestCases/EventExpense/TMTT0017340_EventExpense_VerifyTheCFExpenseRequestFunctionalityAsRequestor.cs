using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.EventExpense;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.EventExpense
{
    class TMTT0017340_EventExpense_VerifyTheCFExpenseRequestFunctionalityAsRequestor : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LVExpenseRequestHomePage lvExpenseRequest = new LVExpenseRequestHomePage();
        LVExpenseRequestCreatePage lvCreateExpRequest = new LVExpenseRequestCreatePage();
        LVExpenseRequestDetailPage lvExpRequestDetail = new LVExpenseRequestDetailPage();

        public static string fileTC17340 = "TMTT0017340_EventExpense_VerifyTheCFExpenseRequestFunctionalityAsRequestor";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheCFExpenseRequestFunctionalityAsRequestor()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC17340;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login. ");

                int userCount = 2; // ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= userCount; row++)
                {
                    string evName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 4);
                    string lobName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 1);
                    string mandatoryFieldErrMsg = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", 2, 12);
                    string errorMessage = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", 2, 13);
                    string updatedEventInfo = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", 2, 15);
                    string approverResp = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", 2, 16);
                    string approverNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", 2, 17);


                    //Search standard user by global search
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    homePage.SearchUserByGlobalSearch(fileTC17340, user);

                    //Verify searched user
                    string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    Assert.AreEqual(userPeopleExl, user);
                    extentReports.CreateLog("User " + user + " details are displayed. ");

                    //Login user
                    usersLogin.LoginAsSelectedUser();

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                    }
                    
                    Assert.IsTrue(login.ValidateUserLightningView(fileTC17340, row));

                    switch (row)
                    {
                        case 2:
                            extentReports.CreateLog("Standard User: " + user + " is able to login into lightning view. ");
                            break;
                        case 3:
                            extentReports.CreateLog("CF Financial User: " + user + " is able to login into lightning view. ");
                            break;
                        case 4:
                            extentReports.CreateLog("CAO User: " + user + " is able to login into lightning view. ");
                            break;
                    }

                    //Click on the Menu button
                    lvHomePage.ClickExpenseRequestMenuButton();

                    //Go to Expense Request Page
                    lvHomePage.SearchItemExpenseRequestLWC("Expense Request(LWC)");
                    Assert.IsTrue(lvExpenseRequest.VerifyIfExpenseRequestPageIsOpenedSuccessfully());
                    extentReports.CreateLog("Expense Request page opened successfully. ");

                    //TC - TMTI0038468 - Verify My Request tab is displayed
                    Assert.IsTrue(lvExpenseRequest.VerifyIfMyRequestTabIsDisplayed());
                    extentReports.CreateLog("My Request tab is displayed upon opening Expense Reuest page. ");

                    //TC - TMTI0038468 -Verify Requests Pending My Approval Tab is displayed
                    Assert.IsTrue(lvExpenseRequest.VerifyIfRequestsPendingMyApprovalTabIsDisplayed());
                    extentReports.CreateLog("Requests Pending My Approval tab is also displayed upon opening Expense Reuest page. ");

                    if (row==4)
                    {
                        //TC - TMTI0038468 - Verify All Requests tab is displayed for CAO User Only
                        Assert.IsTrue(lvExpenseRequest.VerifyIfAllRequestsTabIsDisplayed());
                        extentReports.CreateLog("All Requests tab is displayed upon opening Expense Reuest page. ");
                    }

                    if(row==2)
                    {
                        //TC - TMTI0038469 - Verify required field error while clicking "Create New Expense Form".
                        string error = lvExpenseRequest.GetRequiredFieldErrorUponClickingCreateNewExpenseFormButton();
                        Assert.AreEqual(error, mandatoryFieldErrMsg);
                        extentReports.CreateLog("Error message : " + error + " is prompted for LOB field upon clicking Create New Expense Request button. ");

                        //TC - TMTI0038470 - Verify that on selecting LOB - CF, Event Type field should remove from the screen.
                        Assert.IsFalse(lvExpenseRequest.VerifyEventTypeFieldDisplayedOrNotUponSelectingLOB(lobName));
                        extentReports.CreateLog("Event Type field gets removed from the screen upon LOB selection. ");

                        //TC - TMTI0038471, TMTI0038474 - Verify the system behaviour on clicking "Create New Expense Form" button & validate all the madatory field validation.
                        Assert.IsTrue(lvCreateExpRequest.VerifyRequiredFieldsOnCreateNewExpenseRequestPage(lobName, mandatoryFieldErrMsg));
                        extentReports.CreateLog("All the mandatory fields are displayed as expected on the Create New Expense Request page. ");

                        //TC - TMTI0038476 - Verify that expense request will not be created if logged user is not chosen as requestor or delegate of that requestor.
                        string msg = lvCreateExpRequest.GetErrorMsgDisplayedIfLoggedUserIsNotSelectedAsREquestorOrDelegateOfRequestor(lobName, fileTC17340, row);
                        Assert.AreEqual(errorMessage, msg);
                        extentReports.CreateLog("Error message : " + msg + " is prompted if logged user is not chosen as requestor or delegate of that requestor. ");

                        //TC - TMTI0038475 - Verify the functionality of expense request form on clicking "Cancel" button.
                        Assert.IsTrue(lvCreateExpRequest.VerifyFunctionalityOfExpReqOnClickingCancelButton());
                        extentReports.CreateLog("User lands on the Expense Request home page upon clicking the Cancel button on Create New Expense Request page. ");

                        //TC - TMTI0038477 - Verify that on clicking "Save" button, application creates the expense request with Status marked as "Saved" by logged in user.
                        //TC - TMTI0038478 - Verify the details page of the saved expense request.
                        lvCreateExpRequest.CreateNewExpenseRequestLWC(lobName, fileTC17340, row);
                        Assert.IsTrue(lvExpRequestDetail.VerifyIfExpensePreapprovalNumberIsDisplayed());

                        string expReqpreApprovalNo = lvExpRequestDetail.GetExpensePreapprovalNumber();
                        string eventType = lvExpRequestDetail.GetEventTypeInfo();
                        string eventFormat = lvExpRequestDetail.GetEventFormatInfo();
                        string eventStatus = lvExpRequestDetail.GetEventStatusInfo();

                        extentReports.CreateLog("Expense Request of LOB: " + lobName + " is created successfully with Status as: " + eventStatus + " and Expense Preapproval Number: " + expReqpreApprovalNo +" ");

                        //TC - TMTI0038479 - Verify the Edit functionality from expense request detail page as requestor.
                        lvExpRequestDetail.EditEventInformation(fileTC17340, row);
                        string updatedDescriptionOfOtherCost = lvExpRequestDetail.GetUpdatedDescriptionOfOtherCost();
                        Assert.AreEqual(updatedEventInfo, updatedDescriptionOfOtherCost);
                        extentReports.CreateLog("Edit functionality from expense request detail page is working as expected. ");

                        //TC - TMTI0038480 - Verify all the required fields validation on CF Expense Request form while editing saved requests.
                        Assert.IsTrue(lvExpRequestDetail.VerifyMandatoryFieldErrorMessageWhileTryingToEditEventInfo(fileTC17340, row));
                        extentReports.CreateLog("Required field validations are getting displayed on edit event request detail screen. ");

                        //TC - TMTI0038482 - Verify the functionality of "Clone" button on expense request detail page as requestor.
                        Assert.IsTrue(lvExpRequestDetail.VerifyCloneButtonFunctionality());
                        extentReports.CreateLog("New expense form is openning with pre-filled details from the cloned request. ");

                        lvExpRequestDetail.CreateCloneExpenseRequest();

                        string expReqpreApprovalNo1 = lvExpRequestDetail.GetCloneExpensePreapprovalNumber();
                        string eventStatus1 = lvExpRequestDetail.GetCloneEventStatusInfo();

                        extentReports.CreateLog("Cloned Expense Request of LOB: " + lobName + " is created successfully with Status as: " + eventStatus1 + " and Expense Preapproval Number: " + expReqpreApprovalNo1 + " ");

                        //TC - TMTI0038483 - Verify the "Delete" functionality from expense request detail page as requester.
                        Assert.IsTrue(lvExpRequestDetail.VerifyDeleteExpenseRequestFunctionalityAsRequestor());
                        string eventStatus2 = lvExpRequestDetail.GetCloneEventStatusInfo();
                        extentReports.CreateLog("Expense request is deleted succssfully with status: " + eventStatus2 + " ");

                        //TC - TMTI0038484 - Verify expense detail page on deleting the request as requester.
                        string approverResponse = lvExpRequestDetail.GetApproverResponseFromApprovalHistorySection();
                        Assert.AreEqual(approverResp, approverResponse);
                        string expReqNotes = lvExpRequestDetail.GetNotesFromApprovalHistorySection();
                        Assert.AreEqual(approverNotes, expReqNotes);
                        extentReports.CreateLog("Deleted Expense Request detail page is showing Notes along with approver's response in Expense History. ");
                    }

                    //Logout from SF Lightning View
                    lvHomePage.UserLogoutFromSFLightningView();
                    extentReports.CreateLog("User Logged Out from SF Lightning View. ");
                }

                //TC - End
                usersLogin.UserLogOut();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                driver.Quit();
            }
        }
    }
}