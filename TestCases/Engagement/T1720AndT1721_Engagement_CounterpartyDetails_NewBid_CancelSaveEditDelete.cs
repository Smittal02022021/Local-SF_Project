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
    class T1720AndT1721_Engagement_CounterpartyDetails_NewBid_CancelSaveEditDelete : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        AddCounterparty addCounterparty = new AddCounterparty();

        public static string fileTC1720 = "T1720AndT1721_NewBid_CancelSaveEditDelete4";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void NewBid_CancelSaveEditDelete()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1720;
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
                extentReports.CreateLog("Std User: " + stdUser + " is able to login ");

                //Search engagement with LOB - FR
                string message = engHome.SearchEngagementWithName("Project Rockaway");
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matches to JobType are found and Engagement Detail page is displayed ");
                
                //Click on Counterparties button and validate the page
                string titleCounterparty = engagementDetails.ClickCounterpartiesButton();
                Assert.AreEqual("Edit Counterparty Records", titleCounterparty);
                extentReports.CreateLog("Page with title: " + titleCounterparty + " is displayed upon clicking Counterparties button ");

                //Click Add Counterparties and validate the page
                string titleSearch = addCounterparty.ClickAddCounterpartiesbutton();
                Assert.AreEqual("Search for Company", titleSearch);
                extentReports.CreateLog(titleSearch + " is displayed upon clicking Add Counterparties button ");

                //Save Counterparty record and validate the success message
                string successMsg = addCounterparty.AddCounterparties();
                Assert.AreEqual("Selected records were added successfully", successMsg);
                extentReports.CreateLog(successMsg + " is displayed upon adding record ");

                //Validate New Bid page
                string pageBid = addCounterparty.ClickNewBidAndValidatePage();
                Assert.AreEqual("Bid Edit", pageBid);
                extentReports.CreateLog("Page with title: "+pageBid +" is displayed upon clicking New Bid button ");

                //Validate cancel functionality of New Bid page
                string pageCounterparty = addCounterparty.ValidateCancelFunctionalityOFBid();
                Assert.AreEqual("Engagement Counterparty", pageCounterparty);
                extentReports.CreateLog("Page with title: " + pageCounterparty + " is displayed upon clicking Cancel button ");

                //Validate No record is addded upon clicking cancel button
                string msgNoRec = addCounterparty.ValidateNoRecordsMessage();
                Assert.AreEqual("No records to display", msgNoRec);
                extentReports.CreateLog("Message: " + msgNoRec + " is displayed upon clicking Cancel button after entering all details ");

                //Validate Save functionality of Bid page
                addCounterparty.ClickNewBidAndValidatePage();
                string value = addCounterparty.SaveBidDetails();
                Assert.AreEqual(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), value);
                extentReports.CreateLog("Bid with date: " + value + " is displayed upon saving the details ");

                //Validate edit functionality of Bid page     
                string date = ReadExcelData.ReadData(excelPath,"Bid", 1);
                string valEdit = addCounterparty.ValidateEditFunctionalityOFBid(date);
                Assert.AreEqual(date, valEdit);
                extentReports.CreateLog("Bid with date: " + valEdit + " is displayed upon editing the details ");

                //Logout of User
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search the same engagement, click on added counterparty 
                engHome.SearchEngagementWithName("Project Rockaway");
                engagementDetails.ClickCounterpartiesButton();
                addCounterparty.ClickDetailsLink();

                //Validate delete functionality of Bid page   
                string msgDel = addCounterparty.ValidateDeleteFunctionalityOFBid();
                Assert.AreEqual("No records to display", msgDel);
                extentReports.CreateLog("Message: " + msgDel + " is displayed upon deleting the Bid details ");

                //Delete the added company
                string delComp = addCounterparty.DeleteAddedCP();
                Assert.AreEqual("No records to display", delComp);
                extentReports.CreateLog("Message: " + delComp + " is displayed upon deleting the added company ");
                
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


