using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.GiftLog;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.GiftLog
{
    class T2020_GiftLog_GiftApprovalProcess_ApproveGifts_VerifyColumnSortingOfTheGiftDetailsTableOnGiftsApprovedTab : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();

        public static string fileTC2020 = "T2020_GiftLog_GiftApprovalProcess_ApproveGifts_VerifyColumnSortingOfTheGiftDetailsTableOnGiftsApprovedTab";
       
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GiftLog_GiftApprovalProcess_ApproveGifts_VerifyColumnSortingOfTheGiftDetailsTableOnGiftsApprovedTab()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2020;
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
                homePage.SearchUserByGlobalSearch(fileTC2020, user);

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

                for (int i=1; i<=2; i++)
                {
                    //Navigate to Gift Request page
                    giftRequest.GoToGiftRequestsPage();
                    string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                    string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                    Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                    extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                    //Enter required details in client gift pre- approval page
                    giftRequest.SetDesiredDateToCurrentDate();
                    string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC2020);

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

                    //Click on submit gift request
                    giftRequest.ClickSubmitGiftRequest();
                    giftApprove.ClickSubmitRequest();
                    string congratulationMsg = giftRequest.GetCongratulationsMsg();
                    string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                    Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                    extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of 1st gift request ");

                    //Navigate to Gift Request page
                    giftRequest.GoToGiftRequestsPage();
                    extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                    //Enter required details in client gift pre- approval page
                    giftRequest.SetDesiredDateToCurrentDate();
                    string valGiftNameEntered1 = giftRequest.EnterDetailsGiftRequest(fileTC2020);
                    extentReports.CreateLog("Gift details entered for a new gift request. ");

                    //Adding recipient from add recipient section to selected recipient section
                    giftRequest.AddRecipientToSelectedRecipients();
                    extentReports.CreateLog("Recipient added. ");

                    //Click on submit gift request
                    giftRequest.ClickSubmitGiftRequest();
                    giftApprove.ClickSubmitRequest();
                    extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of 2nd gift request. ");
                }

                usersLogin.UserLogOut();
                
                //Search Compliance user by global search
                string userCompliance = ReadExcelData.ReadData(excelPath, "Users", 2);
                homePage.SearchUserByGlobalSearch(fileTC2020, userCompliance);

                //Verify searched user
                string userPeople1 = homePage.GetPeopleOrUserName();
                string userPeopleExl1 = ReadExcelData.ReadData(excelPath, "Users", 2);
                Assert.AreEqual(userPeopleExl1, userPeople1);
                extentReports.CreateLog("User " + userPeople1 + " details are displayed ");

                //Login as Compliance User and validate the user
                usersLogin.LoginAsSelectedUser();
                string complianceUser = login.ValidateUser();
                string trimmedcomplianceUser = complianceUser.TrimEnd('.');
                string complianceUserExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                Assert.AreEqual(complianceUserExl.Contains(trimmedcomplianceUser), true);
                extentReports.CreateLog("Compliance User: " + trimmedcomplianceUser + " is able to login ");

                //Click on approve gifts tab
                giftApprove.ClickApproveGiftsTab();
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibility());
                extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastName(fileTC2020);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");
                
                //Sorting By Gift Description column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASC("Gift Description"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Gift Description column successfully. ");

                //Sorting By Gift Description column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("Gift Description"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Gift Description column successfully. ");

                //Sorting By Recipient column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASC("Recipient"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Recipient column successfully. ");

                //Sorting By Recipient column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("Recipient"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Recipient column successfully. ");

                //Sorting By Recipient Company column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASC("Recipient Company"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Recipient Company column successfully. ");

                //Sorting By Recipient Company column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("Recipient Company"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Recipient Company column successfully. ");

                //Sorting By Submitted For column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASC("Submitted For"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Submitted For column successfully. ");

                //Sorting By Submitted For column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("Submitted For"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Submitted For column successfully. ");

                //Sorting By Submitted By column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASC("Submitted By"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Submitted By column successfully. ");

                //Sorting By Submitted By column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("Submitted By"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Submitted By column successfully. ");

                //Sorting By Submitted Date column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASC("Submitted Date"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Submitted Date column successfully. ");

                //Sorting By Submitted Date column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("Submitted Date"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Submitted Date column successfully. ");

                //Sorting By Desired Date column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASC("Desired Date"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Desired Date column successfully. ");

                //Sorting By Desired Date column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("Desired Date"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Desired Date column successfully. ");

                //Sorting By Prev YTD column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASC("Prev YTD"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Prev YTD column successfully. ");

                //Sorting By Prev YTD column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("Prev YTD"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Prev YTD column successfully. ");

                //Sorting By Value column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASC("Value"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Value column successfully. ");

                //Sorting By Value column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("Value"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Value column successfully. ");

                //Sorting By New YTD column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASC("New YTD"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by New YTD column successfully. ");
                
                //Sorting By New YTD column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("New YTD"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by New YTD column successfully. ");

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
