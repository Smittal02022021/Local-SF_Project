using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.EventExpense
{
    class T2274_EventExpenseCommonFeatures_VerifyValidationRulesOnEventExpenseSet1 : BaseClass
    {        
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ExpenseRequestPage expRequest = new ExpenseRequestPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC2274 = "T2274_VerifyValidationRulesOnEventExpenseSet.xlsx";
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);            
        }

        [Test]
        public void VerifyValidationRulesOnEventExpenseSet1()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2274;
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

                //Login as Standard User
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Click on Event Expense Tab
                expRequest.ClickExpenseRequest();

                //Validate error message displayed for LOB, Event Type and Requestor/Delegate
                expRequest.ClickCreateNewExpenseForm();
                string msgLOB=expRequest.ValidateLOBMessage();
                Assert.AreEqual("Error: Please select LOB.",msgLOB);
                extentReports.CreateLog("Message: " +msgLOB + " is displayed ");

                string msgEventType = expRequest.ValidateEventTypeMessage(ReadExcelData.ReadData(excelPath, "EventExp", 1));
                Assert.AreEqual("Error: Please select Event Type.",msgEventType);
                extentReports.CreateLog("Message: " + msgEventType + " is displayed ");

                string msgRequestor = expRequest.ValidateRequestorMessage(ReadExcelData.ReadData(excelPath, "EventExp", 2));
                Assert.AreEqual("Error: Only requestor or their delegate can create Event Expense Form for requestor", msgRequestor);
                extentReports.CreateLog("Message: " + msgRequestor + " is displayed ");

                //Save Requestor details and validate Event Expense details page is displayed
                string lblRequestor= expRequest.SaveRequestorDetails(ReadExcelData.ReadData(excelPath, "Users", 1));
                Assert.AreEqual("Requestor/Host Information", lblRequestor);
                extentReports.CreateLog("Event Expense details page is displayed upon clicking Save button ");

                //Click Back To Expense Request List and validate Event Expense Home page
                string lblNewExp = expRequest.ClickBackAndValidatePage();
                Assert.AreEqual("New Expense Request", lblNewExp);
                extentReports.CreateLog("Event Expense Home page is displayed upon clicking Back To Expense Request List button ");

                //Click on edit link and validate Event Expense Edit details page
                string lblRequestorInfo = expRequest.ValidateEditFeature();
                Assert.AreEqual("Requestor/Host information", lblRequestorInfo);
                extentReports.CreateLog("Event Expense edit page is displayed upon clicking edit link ");

                //Click on cancel button and validate Event Expense details page
                string lblPage = expRequest.ValidateCancelFeature();
                Assert.AreEqual("New Expense Request", lblPage);
                extentReports.CreateLog("Event Expense details page is displayed upon clicking cancel button ");

                //Click Submit without filling mandatory details and validate all validations
                string validationsList = expRequest.ClickSubmitWithoutMandatoryFields();
                Console.WriteLine(validationsList);
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "EventExp", 3), validationsList);
                extentReports.CreateLog("Validations: " + validationsList + " are displayed ");

                //Fill all the mandatory details of Event Expense Request, submit the request and validate the status
                expRequest.ClickReturnToExpense();
                expRequest.SaveAllValuesOfEventExpense(fileTC2274);
                expRequest.SubmitEventExpenseRequest();
                string status = expRequest.GetRequestStatus();
                Assert.AreEqual("Waiting for Approval", status);
                extentReports.CreateLog("Ëvent Expense Request is submitted for approval ");

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
