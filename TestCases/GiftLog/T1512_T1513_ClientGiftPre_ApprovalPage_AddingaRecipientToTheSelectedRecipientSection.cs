using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.GiftLog;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.GiftLog
{
    class T1512_T1513_GiftLog_ClientGiftPre_ApprovalPage_AddingaRecipientToTheSelectedRecipientSection : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();

        public static string fileTC1513 = "T1513_ClientGiftPre_ApprovalPage_AddingaRecipientToTheSelectedRecipientSection";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClientGiftPre_ApprovalPage_AddingaRecipientToTheSelectedRecipientSection()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1513;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC1513, user);

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
                extentReports.CreateLog("Standard User: " + giftLogUser + " is able to login ");


                //Navigate to Gift Request page
                giftRequest.GoToGiftRequestsPage();
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                // Enter required details in client gift pre- approval page
                giftRequest.EnterDetailsGiftRequest(fileTC1513);

                extentReports.CreateLog("details entered");



                for (int row =2; row <= 5; row++)
                {
                    string comboSelection = ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLogComboBox", row, 2);
                    Console.WriteLine(comboSelection);
                    giftRequest.ClearGiftRecipientsDetails();
                    
                 //   giftRequest.VerifyCompnyNameComboBox(fileTC1513, row, ReadExcelData.ReadData(excelPath, "GiftLogComboBox", row));
                    giftRequest.VerifyCompnyNameComboBox(fileTC1513, comboSelection, ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLogComboBox", row,1));
                    //Verify company name COmbo Box
                    Console.WriteLine("Pass");
                    string actualRecipientCompanyName11 = giftRequest.GetAvailableRecipientCompany();
                    string expectedCompanyName11 = "StandardTestCompany";
                    Assert.AreEqual(expectedCompanyName11, actualRecipientCompanyName11);
                    extentReports.CreateLog("Company Name: " + actualRecipientCompanyName11 + " is listed in Available Recipient(s) table for contains combo box in Cmmpany Name ");

                    giftRequest.ClearGiftRecipientsDetails();
                   // giftRequest.VerifyContactNameComboBox(fileTC1513, row, ReadExcelData.ReadData(excelPath, "GiftLogComboBox", row + 4));
                     giftRequest.VerifyContactNameComboBox(fileTC1513, comboSelection, ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLogComboBox", row+4,1));
                    //Verify contact name Combo Box
                    string actualRecipientContactName11 = giftRequest.GetAvailableRecipientName();
                    string expectedContactName11 = "Test External";
                    Assert.AreEqual(expectedContactName11, actualRecipientContactName11);
                    extentReports.CreateLog("Recipient Name: " + actualRecipientContactName11 + " is listed in Available Recipient(s) table for Contains combo box in Contact Name ");


                    giftRequest.ClearGiftRecipientsDetails();
                    giftRequest.VerifyCompanyContactNameComboBox(fileTC1513, comboSelection,comboSelection, ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLogComboBox", row+8,1), ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLogComboBox", row + 12,1));
                    //  giftRequest.VerifyCompanyContactNameComboBox(fileTC1513, row,row, ReadExcelData.ReadData(excelPath, "GiftLogComboBox", row +8), R ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLogComboBox", row+8,1);
                    //Verify Company and Contact name Combo Box
                    string actualRecipientContactName1 = giftRequest.GetAvailableRecipientName();
                    string actualRecipientCompanyName1 = giftRequest.GetAvailableRecipientCompany();
                   
                    Assert.AreEqual(expectedContactName11, actualRecipientContactName1);
                    Assert.AreEqual(expectedCompanyName11, actualRecipientCompanyName1);
                    extentReports.CreateLog( "Company Name: "+ actualRecipientCompanyName1+  " and Recipient Name: " + actualRecipientContactName1 + " is listed in Available Recipient(s) table for Contains combo box in Contact Name ");
                }
            

               giftRequest.ClearGiftRecipientsDetails();
                string comboSelection1 = ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLogComboBox", 2, 2);
                giftRequest.VerifyCompnyNameComboBox(fileTC1513, comboSelection1, "StandardTestCompany");
               giftRequest.VerifyContactNameComboBox(fileTC1513, comboSelection1, "test external");

                                                                                 



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

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();
               
                //Verify recipient name
                string selectedRecipientName = giftRequest.GetSelectedRecipientName();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateLog("Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Verify company name
                string selectedCompanyName = giftRequest.GetSelectedCompanyName();
                Assert.AreEqual(actualRecipientCompanyName, selectedCompanyName);
                extentReports.CreateLog("Company Name: " + selectedCompanyName + " in selected recipient(s) table matches with available company name listed in Available Recipient(s) table ");

                // Verify removal of recipient from selected recipients
                giftRequest.RemoveRecipientFromSelectedRecipients();
                Assert.IsFalse(giftRequest.IsSelectedRecipientDisplayed());
                extentReports.CreateLog("Recipient details are not displayed in Selected Recipient(s) section after Remove Recipient ");

                // Enter required details in client gift pre- approval page
                giftRequest.EnterDetailsGiftRequest(fileTC1513);
                //Verify company name
                Assert.AreEqual(expectedCompanyName, actualRecipientCompanyName);
                extentReports.CreateLog("Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table ");
                //Verify contact name
                Assert.AreEqual(expectedContactName, actualRecipientContactName);
                extentReports.CreateLog("Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table ");

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();
                //Verify contact name
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateLog("Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");
                //Verify company name
                Assert.AreEqual(actualRecipientCompanyName, selectedCompanyName);
                extentReports.CreateLog("Company Name: " + selectedCompanyName + " in selected recipient(s) table matches with available company name listed in Available Recipient(s) table ");

                //Click on cancel button
                giftRequest.ClickCancelButton();
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of cancel button and gift request is not created ");

                // Enter required details in client gift pre- approval page
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequest(fileTC1513);
                //Verify company name
                Assert.AreEqual(expectedCompanyName, actualRecipientCompanyName);
                extentReports.CreateLog("Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table ");

                //Verify contact name
                Assert.AreEqual(expectedContactName, actualRecipientContactName);
                extentReports.CreateLog("Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table ");

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipients();
                //Verify recipient contact name
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateLog("Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Verify company name
                Assert.AreEqual(actualRecipientCompanyName, selectedCompanyName);
                extentReports.CreateLog("Company Name: " + selectedCompanyName + " in selected recipient(s) table matches with available company name listed in Available Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequest();
                string congratulationMsg = giftRequest.GetCongratulationsMsg();
                string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                //Verify Gift description 
                string giftDescriptionGiftRequestDetail = giftRequest.GetGiftDescriptionOnGiftRequestDetail();
                Assert.AreEqual(valGiftNameEntered, giftDescriptionGiftRequestDetail);
                extentReports.CreateLog("Gift Description: " + giftDescriptionGiftRequestDetail + " is listed on gift request submission detail page ");

                //Verify recipient name
                string RecipientOnGiftRequestDetail = giftRequest.GetRecipientNameOnGiftRequestDetail();
                string recipientName = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(recipientName, RecipientOnGiftRequestDetail);
                extentReports.CreateLog("Recipient Name: " + RecipientOnGiftRequestDetail + " is listed on gift request submission detail page ");

                //Click on return to pre-approval page button
                giftRequest.ClickReturnToPreApprovalPage();
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of return to pre approval page ");

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