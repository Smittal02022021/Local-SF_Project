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
    class T2021_GiftLog_GiftApprovalProcess_ApproveGifts_ApproveOrDenyMultipleGiftRequests : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();

        public static string fileTC2021 = "T2021_GiftLog_GiftApprovalProcess_ApproveGifts_ApproveOrDenyMultipleGiftRequests";
       
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GiftLog_GiftApprovalProcess_ApproveGifts_ApproveOrDenyMultipleGiftRequests()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2021;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                for (int i=1; i<=2; i++)
                {
                    //Search standard user by global search
                    string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                    homePage.SearchUserByGlobalSearch(fileTC2021, user);

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

                    //Navigate to Gift Request page
                    giftRequest.GoToGiftRequestsPage();
                    string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                    string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                    Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                    extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                    //Enter required details in client gift pre- approval page
                    giftRequest.SetDesiredDateToCurrentDate();
                    string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC2021);

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
                    string valGiftNameEntered1 = giftRequest.EnterDetailsGiftRequest(fileTC2021);
                    extentReports.CreateLog("Gift details entered for a new gift request. ");

                    //Adding recipient from add recipient section to selected recipient section
                    giftRequest.AddRecipientToSelectedRecipients();
                    extentReports.CreateLog("Recipient added. ");

                    //Click on submit gift request
                    giftRequest.ClickSubmitGiftRequest();
                    giftApprove.ClickSubmitRequest();
                    extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of 2nd gift request. ");

                    usersLogin.UserLogOut();

                    //Search Compliance user by global search
                    string userCompliance = ReadExcelData.ReadData(excelPath, "Users", 2);
                    homePage.SearchUserByGlobalSearch(fileTC2021, userCompliance);

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
                    giftApprove.SearchByRecipientLastName(fileTC2021);
                    extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                    //Click on approve selected button and approve multiple gifts at once
                    giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                    giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered1);
                    giftApprove.SetApprovalDenialComments();

                    if (i==1)
                    {
                        giftApprove.ClickApproveSelectedButton();
                        extentReports.CreateLog("Multiple gifts are approved at once by clicking Approve selected button. ");

                        //Search the approved gifts under approved status
                        giftApprove.SearchByStatus(fileTC2021, "Approved");
                        string txtStatus = giftApprove.GetStatusCompareGiftDescWithGiftName(valGiftNameEntered);
                        string txtStatus1 = giftApprove.GetStatusCompareGiftDescWithGiftName(valGiftNameEntered1);

                        //Verification of gift status displaying in Approved list
                        Assert.AreEqual("Approved", txtStatus);
                        extentReports.CreateLog(txtStatus + " is displaying in gift status for 1st gift. ");
                        Assert.AreEqual("Approved", txtStatus1);
                        extentReports.CreateLog(txtStatus1 + " is displaying in gift status for 2nd gift. ");

                        //Click on Deny selected button and deny multiple gifts from approved list at once
                        giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                        giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered1);

                        giftApprove.ClickDenySelectedButton();
                        extentReports.CreateLog("Multiple gifts are denied at once by clicking Deny selected button from approved gifts list. ");

                        //Search the Denied gifts under Denied status
                        giftApprove.SearchByStatus(fileTC2021, "Denied");
                        string txtStatus2 = giftApprove.GetStatusCompareGiftDescWithGiftName(valGiftNameEntered);
                        string txtStatus3 = giftApprove.GetStatusCompareGiftDescWithGiftName(valGiftNameEntered1);

                        //Verification of gift status displaying in Denied list
                        Assert.AreEqual("Denied", txtStatus2);
                        extentReports.CreateLog(txtStatus2 + " is displaying in gift status for 1st gift. ");
                        Assert.AreEqual("Denied", txtStatus3);
                        extentReports.CreateLog(txtStatus3 + " is displaying in gift status for 2nd gift. ");

                        usersLogin.UserLogOut();
                    }
                    else
                    {
                        giftApprove.ClickDenySelectedButton();
                        extentReports.CreateLog("Multiple gifts are denied at once by clicking Deny selected button. ");

                        //Search the approved gifts under approved status
                        giftApprove.SearchByStatus(fileTC2021, "Denied");
                        string txtStatus4 = giftApprove.GetStatusCompareGiftDescWithGiftName(valGiftNameEntered);
                        string txtStatus5 = giftApprove.GetStatusCompareGiftDescWithGiftName(valGiftNameEntered1);

                        //Verification of gift status displaying in Approved list
                        Assert.AreEqual("Denied", txtStatus4);
                        extentReports.CreateLog(txtStatus4 + " is displaying in gift status for 1st gift. ");
                        Assert.AreEqual("Denied", txtStatus5);
                        extentReports.CreateLog(txtStatus5 + " is displaying in gift status for 2nd gift. ");

                        //Click on Approve selected button and approve multiple gifts from approved list at once
                        giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered);
                        giftApprove.CompareGiftDescWithGiftName(valGiftNameEntered1);
                        giftApprove.ClickApproveSelectedButton();
                        extentReports.CreateLog("Multiple gifts are approved at once by clicking Approve selected button from denied gifts list. ");

                        //Search the Approved gifts under Denied status
                        giftApprove.SearchByStatus(fileTC2021, "Approved");
                        string txtStatus6 = giftApprove.GetStatusCompareGiftDescWithGiftName(valGiftNameEntered);
                        string txtStatus7 = giftApprove.GetStatusCompareGiftDescWithGiftName(valGiftNameEntered1);

                        //Verification of gift status displaying in Approved list
                        Assert.AreEqual("Approved", txtStatus6);
                        extentReports.CreateLog(txtStatus6 + " is displaying in gift status for 1st gift. ");
                        Assert.AreEqual("Approved", txtStatus7);
                        extentReports.CreateLog(txtStatus7 + " is displaying in gift status for 2nd gift. ");

                        usersLogin.UserLogOut();
                    }
                }
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
