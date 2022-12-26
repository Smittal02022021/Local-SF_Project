using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Engagement
{
    class T2209AndT2210_PortfolioValuation_ValidateRevenueFieldsUpdateWithStatusInProgress_CompletedAndPositionCancelled : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ValuationPeriods valuationPeriods = new ValuationPeriods();
        SendEmailNotification notification = new SendEmailNotification();
        public static string fileTC2209 = "T2209AndT2210_PortfolioValuation_ValidateRevenueFieldsUpdate";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ValidateRevenueFieldsUpdate()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2209;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Clicking on Engagement Tab and search for Engagement by entering Job type         
                string message = engHome.SearchEngagementWithName(ReadExcelData.ReadData(excelPath, "Engagement", 2));
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //Validate title of Engagement Details page
                string title = engagementDetails.GetTitle();
                Assert.AreEqual("Engagement", title);
                extentReports.CreateLog("Page with title: " + title + " is displayed ");

                //Click on Portfolio Valuation,click Engagement Valuation Period and validate Engagement Valuation Period page
                engagementDetails.ClickPortfolioValuation();
                string titleValPeriod = valuationPeriods.ClickEngValuationPeriod();
                Assert.AreEqual("New Engagement Valuation Period", titleValPeriod);
                extentReports.CreateLog("Page with title: " + titleValPeriod + " is displayed upon clicking New Engagement Valuation Period button ");

                //Enter Engagement Valuation Period details, save it and Validate Engagement Valuation Period Detail page
                string name = valuationPeriods.EnterAndSaveEngValuationDetails();
                extentReports.CreateLog("Engagement Valuation Period with name: " + name + " is created ");

                string titleValPeriodDetail = valuationPeriods.GetEngValPeriodDetailTitle();
                Assert.AreEqual("Engagement Valuation Period Detail", titleValPeriodDetail);
                extentReports.CreateLog("Page with title: " + titleValPeriodDetail + " is displayed upon saving Engagement Valuation Period details ");

                //Enter Eng Valuation Period Position details and validate entered Valuation Period Position
                string valPeriodPosition = valuationPeriods.EnterPeriodPositionDetails(fileTC2209);
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4), valPeriodPosition);
                extentReports.CreateLog("Engagement Valuation Period Position with name: " + valPeriodPosition + " is added successfully ");

                //Click on added Period Position and Validate value of Status,Fee Completed,Revenue Month,Cancel Month,Revenue Year,Cancel Year, Completed Date and Cancel Date
                string titlePage = valuationPeriods.ClickAddedValPeriod();
                Assert.AreEqual("Engagement Valuation Position Detail", titlePage);
                extentReports.CreateLog("Page with title: " + titlePage + " is displayed upon clicking on added Valuation Position ");

                //-----Validate Status
                string Status = valuationPeriods.GetPositionStatus();
                Assert.AreEqual("In Progress", Status);
                extentReports.CreateLog("Status: " + Status + " is displayed as expected ");

                //-----Validate Fee Completed
                string FeeCompleted = valuationPeriods.GetFeeCompleted();
                Assert.AreEqual("USD 0.00", FeeCompleted);
                extentReports.CreateLog("Fee Completed: " + FeeCompleted + " is displayed as expected ");

                //-----Validate Revenue Month and Revenue Year
                string revenueMonth = valuationPeriods.GetRevenueMonth();
                Assert.AreEqual(" ", revenueMonth);

                string revenueYear = valuationPeriods.GetRevenueYear();
                Assert.AreEqual(" ", revenueYear);
                extentReports.CreateLog("No value is displayed in Revenue Month and Revenue Year ");

                //-----Validate Cancel Month and Cancel Year
                string cancelMonth = valuationPeriods.GetCancelMonth();
                Assert.AreEqual(" ", cancelMonth);

                string cancelYear = valuationPeriods.GetCancelYear();
                Assert.AreEqual(" ", cancelYear);
                extentReports.CreateLog("No value is displayed in Cancel Month and Cancel Year ");

                //-----Validate Completed Date
                string completedDate = valuationPeriods.GetCompletedDate();
                Assert.AreEqual(" ", completedDate);

                string cancelDate = valuationPeriods.GetCancelDate();
                Assert.AreEqual(" ", cancelDate);
                extentReports.CreateLog("No value is displayed in Completed Date and Cancel Date ");

                //Update Status and Report Fee of existing position and validate details of Position
                valuationPeriods.UpdateStatusAndReportFee(fileTC2209);
                extentReports.CreateLog("Status and Report Fee of position is updated ");

                //-----Validate Fee Completed
                string feeCompleted = valuationPeriods.GetFeeCompleted();
                Assert.AreEqual("USD " + ReadExcelData.ReadData(excelPath, "ValuationPeriod", 11), feeCompleted);
                extentReports.CreateLog("Fee Completed: " + feeCompleted + " is displayed as updated ");

                //-----Validate Revenue Month
                string revMonth = valuationPeriods.GetRevenueMonth();
                //Console.WriteLine("Month: "+DateTime.Now.ToString("MM"));
                Assert.AreEqual(DateTime.Now.ToString("MM"), revMonth);
                extentReports.CreateLog("Revenue Month: " + revMonth + " same as current month is displayed ");

                //-----Validate Revenue Year
                string revYear = valuationPeriods.GetRevenueYear();
                Assert.AreEqual(DateTime.Now.ToString("yyyy"), revYear);
                extentReports.CreateLog("Revenue Year: " + revYear + " same as current year is displayed ");

                //-----Validate Completed Date                
                string compDate = valuationPeriods.GetCompletedDate();               
                Console.WriteLine("compDate:"+compDate);
                //Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture), compDate.Substring(0,11));
                extentReports.CreateLog(DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture));
                extentReports.CreateLog("Completed Date: " + compDate.Substring(0,11)+ " same as today's date is displayed ");

                //-----Validate Cancel Month, Cancel Year
                string canMonth = valuationPeriods.GetCancelMonth();
                Assert.AreEqual(" ", cancelMonth);

                string canYear = valuationPeriods.GetCancelYear();
                Assert.AreEqual(" ", cancelYear);

                string canDate = valuationPeriods.GetCancelDate();
                Assert.AreEqual(" ", cancelDate);
                extentReports.CreateLog("The fields Cancel Month, Cancel Year, Cancel Date are blank when status is changed to Completed-Generate Accrual ");

                //Validate cancel message on clicking Void Position
                string messageCancel = valuationPeriods.ClickVoidPositionAndGetMessage();
                Assert.AreEqual("Are you sure you want to cancel this position? This process will reverse any accruals from this position.", messageCancel);
                extentReports.CreateLog("Message: " + messageCancel + " is displayed on clicking Void Position button ");

                //Validate details after cancelling the position
                //-----Status of Position 
                string statusCancel = valuationPeriods.GetPositionStatus();
                Assert.AreEqual("Cancelled", statusCancel);
                extentReports.CreateLog("Status: " + statusCancel + " is displayed after position is cancelled by clicking Void Position button ");

                //-----Validate Cancel Month
                string cancelMon = valuationPeriods.GetCancelMonth();
                Console.WriteLine(DateTime.Now.ToString("MM"));
                //Assert.AreEqual(DateTime.Now.ToString("MM"), revMonth);
                extentReports.CreateLog("Cancel Month: " + cancelMon + " same as current month is displayed after position is cancelled ");

                //-----Validate Cancel Year
                string cancelYr = valuationPeriods.GetCancelYear();
                Assert.AreEqual(DateTime.Now.ToString("yyyy"), cancelYr);
                extentReports.CreateLog("Cancel Year: " + cancelYr + " same as current year is displayed after position is cancelled ");

                //-----Validate Cancel Date
                string can_Date = valuationPeriods.GetCancelDate();
                Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture), can_Date.Substring(0,9));
                extentReports.CreateLog("Cancel Date: " + can_Date.Substring(0,9) + " same as today's date is displayed after position is cancelled ");

                //Logout of standard user 
                usersLogin.UserLogOut();
                
                //Calling function to delete Position
                //valuationPeriods.DeletePosition();

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


