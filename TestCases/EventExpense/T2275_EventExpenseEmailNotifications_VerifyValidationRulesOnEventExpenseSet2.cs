using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.EventExpense
{
    class T2275_EventExpenseEmailNotifications_VerifyValidationRulesOnEventExpenseSet2 : BaseClass
    {        
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ExpenseRequestPage expRequest = new ExpenseRequestPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC2275 = "T2275_VerifyValidationRulesOnEventExpenseSet2";
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);            
        }

        [Test]
        public void VerifyValidationRulesOnEventExpenseSet2()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2275;
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

                //Fill details of Event Expense Request and click on Submit button after selecting Marketing Support as Yes
                expRequest.ValidateEventTypeMessage(ReadExcelData.ReadData(excelPath, "EventExp", 1));
                expRequest.ValidateRequestorMessage(ReadExcelData.ReadData(excelPath, "EventExp", 2));                
                expRequest.SaveRequestorDetails(ReadExcelData.ReadData(excelPath, "Users", 1));
                expRequest.ClickBackAndValidatePage();
                expRequest.ValidateEditFeature();
                expRequest.SaveAllValuesOfEventExpense(fileTC2275);
                extentReports.CreateLog("Details are saved in Event Expenese form ");
                expRequest.ClickEditButton();
                string descEnabled= expRequest.SelectMarketingSupport("Yes");
                Assert.AreEqual("True",descEnabled);
                extentReports.CreateLog("Description of Marketing Support field is enabled after selecting Marketing support as Yes ");
                
                //Click on Edit link, enter description of Marketing Support, select Marketing Support as No and validate if description box is disabled
                expRequest.EnterMarketingSupportDesc();
                string descDisabled = expRequest.SelectMarketingSupport("No");
                Assert.AreEqual("False", descDisabled);
                extentReports.CreateLog("Description of Marketing Support field is disabled after selecting Marketing support as No ");
                expRequest.EnterEndDate("11/11/2020");
                expRequest.SubmitEventExpenseRequest();
                string val = expRequest.GetEndDateValidations();
                Assert.AreEqual("Error: End Date should be greater than or equal to Start Date.", val);
                extentReports.CreateLog("Validation: " + val + " is displayed upon entering End Date in past ");

                //Click on Edit button, update End Date, clear Other cost fields and validate displayed messages
                expRequest.ClickEditButton();
                expRequest.ClickEndDateLink();
                expRequest.ClearCostValuesAndSave();
                extentReports.CreateLog("Other Cost field values are cleared and saved blank ");
                expRequest.SubmitEventExpenseRequest();
                string valOtherCost = expRequest.GetOtherCostValidations();
                Assert.AreEqual("Error: Other Cost is Required.", valOtherCost);
                extentReports.CreateLog("Validation: " + valOtherCost + " is displayed upon submitting blank value of Other Cost ");

                //Edit the record, add Other Cost and validation message for Description of Other Cost
                expRequest.ClickEditButton();
                string valDescCost =expRequest.EnterOtherCostAndSave(fileTC2275);
                Assert.AreEqual("Error: Please provide Description of Other Cost.", valDescCost);
                extentReports.CreateLog("Validation: " + valDescCost + " is displayed upon submitting blank value of Description of Other Cost ");
                
                //Edit the record, add Description of Other Cost and validation message for Evaluation Date
                expRequest.ClickEditButton();
                string valEvalDate = expRequest.EnterDescofOtherCostAndSave(fileTC2275);
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "EventExp", 17), valEvalDate);
                extentReports.CreateLog("Validations: " + valEvalDate + " are displayed upon submitting Evaluation Date in past ");

                //Update Evaluation Date, save and Submit the record
                expRequest.ClickEditButton();
                expRequest.UpdateEvalDateAndSubmit();
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
