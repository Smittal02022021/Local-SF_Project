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


namespace SalesForce_Project.TestCases.GiftLog
{
    class T1508_T1511_T1514_ClientGiftPre_ApprovalPage_AddGiftToSelectRecipient_RequiredInformationFieldValidation_AtleastOneRecipientRequired : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();

        public static string fileTC1508_TC1511 = "T1508_T1511_ClientGiftPre_ApprovalPage_AddGiftToSelectRecipient_RequiredInformationFieldValidation_AtleastOneRecipientRequired";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ClientGiftPre_ApprovalPage_AddGiftToSelectRecipient_RequiredInformationFieldValidation_AtleastOneRecipientRequired()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1508_TC1511;
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
                homePage.SearchUserByGlobalSearch(fileTC1508_TC1511, user);

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

                giftRequest.GoToGiftRequestPage();
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                string defaultSubmittedFor = giftRequest.GetDefaultSubmittedForUser();
                Assert.AreEqual(giftLogUser, defaultSubmittedFor);
                extentReports.CreateLog("Default submitted for value is matching with the logged in user ");

                giftRequest.ClickSubmitGiftRequest();

                Assert.IsTrue(giftRequest.ErrorFieldsGiftTypeValueAndVendor(driver, "Gift Type").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Gift Type error message displayed upon click of save button without entering details ");

                Assert.IsTrue(giftRequest.ErrorFieldsGiftNameAndHLRelationship(driver, "Gift Name",1).Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Gift Name error message displayed upon click of save button without entering details ");

                Assert.IsTrue(giftRequest.ErrorFieldsGiftTypeValueAndVendor(driver, "Gift Value").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Gift Value error message displayed upon click of save button without entering details ");

                Assert.IsTrue(giftRequest.ErrorFieldsGiftNameAndHLRelationship(driver, "HL Relationship",2).Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("HL Relationship error message displayed upon click of save button without entering details ");

                Assert.IsTrue(giftRequest.ErrorFieldsGiftTypeValueAndVendor(driver, "Vendor").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Vendor error message displayed upon click of save button without entering details ");

                giftRequest.EnterDetailsGiftRequest(fileTC1508_TC1511);
                extentReports.CreateLog("Gift Details are entered successfully without adding recipients to selected recipients ");
               
                //Click on Submit gift request button
                giftRequest.ClickSubmitGiftRequest();
                extentReports.CreateLog("Click on Submit Gift Request button successfully ");

                //Get error message
                string errorMsg = giftRequest.GetErrorMessageForSelectingAlteastOneRecipient();
                Assert.AreEqual("Error:\r\nYou must select at least one recipient.", errorMsg);
                extentReports.CreateLog("Error Message: "+ errorMsg+" is diplayed upon submittion of gift request without adding atleast one recipient to selected recipients list ");


                String HelpTextGiftType = giftRequest.VerifyHelpTextGiftType();
                Assert.AreEqual("Type of gift", HelpTextGiftType);
                extentReports.CreateLog("Help Text for Gift Type: " + HelpTextGiftType + " is displaying on Gift Pre-Approval Page ");


                String HelpTextSubmittedFor = giftRequest.VerifyHelpTextSubmittedFor();
                Assert.AreEqual("HL Staff member giving gift", HelpTextSubmittedFor);
                extentReports.CreateLog("Help Text for  SUbmitted for: " + HelpTextSubmittedFor + " is displaying on Gift Pre-Approval Page ");

                String HelpTextGiftValue = giftRequest.VerifyHelpTextGiftValue();
                Assert.AreEqual("The gift"+"\\&#39;s market value in the specified currency excluding the cost of tax, shipping, and engraving.", HelpTextGiftValue);
                extentReports.CreateLog("Help Text for Gift Value : " + HelpTextGiftValue + " is displaying on Gift Pre-Approval Page ");

                             

                     String HelpTextDesiredDate = giftRequest.VerifyHelpTextDesiredDate();
                Assert.AreEqual("Date the gift will be purchased by.", HelpTextDesiredDate);
                extentReports.CreateLog("Help Text for Gift Value : " + HelpTextDesiredDate + " is displaying on Gift Pre-Approval Page ");


                String HelpTextCurrency = giftRequest.VerifyHelpTextCurrency();
                Assert.AreEqual("Currency that the gift will be purchased in.", HelpTextCurrency);
                extentReports.CreateLog("Help Text for Gift Value : " + HelpTextCurrency + " is displaying on Gift Pre-Approval Page ");



                String HelpTextVendor = giftRequest.VerifyHelpTextVendor();
                Assert.AreEqual("Vendor to be used for gift.", HelpTextVendor);
                extentReports.CreateLog("Help Text for Gift Value : " + HelpTextVendor + " is displaying on Gift Pre-Approval Page ");


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