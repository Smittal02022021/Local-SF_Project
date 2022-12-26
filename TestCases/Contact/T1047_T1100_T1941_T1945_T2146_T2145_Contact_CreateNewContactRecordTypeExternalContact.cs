using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.Contact
{
    class T1047_T1100_T1941_T1945_T2146_T2145_Contact_CreateNewContactRecordTypeExternalContact : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        ContactHomePage contactHome = new ContactHomePage();
        UsersLogin usersLogin = new UsersLogin();
        SendEmailNotification sendEmail = new SendEmailNotification();
        HomeMainPage homePage = new HomeMainPage();
        ContactEditPage contactEdit = new ContactEditPage();

        public static string fileTC1047 = "T1047_Contact_CreateNewContactRecordTypeExternalContact";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CreateContactTypeExternalContact()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1047;
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
                homePage.SearchUserByGlobalSearch(fileTC1047, user);

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

                // Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling click add contact function
                conHome.ClickAddContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("user navigate to create contact page upon click of add contact button ");
                               
                //Validate FirstName, LastName and CompanyName display with red flag as mandatory fields
                Assert.IsTrue(createContact.ValidateMandatoryFields(), "Validate Mandatory fields");
                extentReports.CreateLog("Validated FirstName, LastName and CompanyName displayed with red flag as mandatory fields ");
                Thread.Sleep(300);

                //Calling click save button function
                createContact.ClickSaveButton();
                Thread.Sleep(300);

                //Validation of company error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver,"Company Name").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Company name error message displayed upon click of save button without entering details ");

                //Validation of first name error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver,"First Name").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("First name error message displayed upon click of save button without entering details ");

                //Validation of last name error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver,"Last Name").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Last name error message displayed upon click of save button without entering details ");

                // To create contact
                createContact.CreateContact(fileTC1047);
                extentReports.CreateLog("External Contact created ");

                // Assertion to validate the company name selected display on contact details page
                string companyName = contactDetails.GetCompanyName();
                Assert.AreEqual(contactDetails.GetCompanyNameFromExcel(fileTC1047), companyName);
                extentReports.CreateLog("Company Name: " + companyName + " in add contact page matches on contact details page");

                // Assertion to validate the contact first and last name selected display on contact details page
                Assert.AreEqual(contactDetails.GetFirstNameFromExcel(fileTC1047) + " " + contactDetails.GetLastNameFromExcel(fileTC1047), contactDetails.GetFirstAndLastName());
                extentReports.CreateLog("First Name and Last Name: "+ contactDetails.GetFirstAndLastName()+ " in add contact page matches on contact details page");

                //Assertion to validate contact record type
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Contact", 7), contactDetails.GetContactRecordTypeValue());
                extentReports.CreateLog("Validation of contact with Record Type " + contactDetails.GetContactRecordTypeValue() + " created with detailed information" +
                    " ,Contact Record type is displayed under system information section ");

                // Search for external contact
                conHome.SearchContact(fileTC1047);
                extentReports.CreateLog("Search for an external contact ");
                string txtCustomLink = contactEdit.GetCustomLinkText();
                Assert.AreEqual("Google Maps", txtCustomLink);
                extentReports.CreateLog("Custom Link: "+ txtCustomLink+" is displaying ");

                contactEdit.VerifyGoogleMapsLink();

                //Verified quick links and Related objects for external contact 
                contactEdit.ValidateQuickLink(fileTC1047);
                extentReports.CreateLog("Verified quick links and Related objects for external contact ");

                //Verify error message is dispalyed when user tries to change company
                contactEdit.EditCompany();
                contactEdit.ClickSaveBtn();
                String errMsg = contactEdit.TxtErrorMessageCompany();
                Assert.AreEqual("Error: Invalid Data.\r\nReview all error messages below to correct your data.\r\nYou do not have rights to move a Contact to another Company.", errMsg);
                extentReports.CreateLog("Error message: "+ errMsg+" is displaying when user tries to change company");

                usersLogin.UserLogOut();
                extentReports.CreateLog("Logout from standard user. ");

                contactHome.SearchContact(fileTC1047);

                //Verify Work Flow Badge Contact-Copy and Delete
                string BadgeFirstNameText = contactEdit.TxtBadgeFirstName();
                Assert.AreEqual(BadgeFirstNameText, " ");
                extentReports.CreateLog("Badge first name field is empty. ");
                
                string BadgeLastNameText = contactEdit.TxtBadgeLastName();
                Assert.AreEqual(BadgeLastNameText, " ");
                extentReports.CreateLog("Badge Last name field is empty. ");
                
                string BadgeCompanyText = contactEdit.TxtBadgeCompany();
                Assert.AreEqual(BadgeCompanyText, " ");
                extentReports.CreateLog("Badge Company field is empty. ");

                contactEdit.EditEventBadgeFields();
                contactEdit.ClickSaveBtn();

                string BadgeFirstNameText1 = contactEdit.TxtBadgeFirstName();
                Assert.AreEqual(BadgeFirstNameText1, ReadExcelData.ReadData(excelPath, "Contact", 2));
                extentReports.CreateLog( "Badge first name: "+BadgeFirstNameText1 + " is displaying ");

                string BadgeLastNameText1 = contactEdit.TxtBadgeLastName();
                Assert.AreEqual(BadgeLastNameText1, ReadExcelData.ReadData(excelPath, "Contact", 3));
                extentReports.CreateLog("Badge Last name: " + BadgeLastNameText1 + " is displaying. ");

                string BadgeCompanyText1 = contactEdit.TxtBadgeCompany();
                Assert.AreEqual(BadgeCompanyText1, ReadExcelData.ReadData(excelPath, "Contact", 1));
                extentReports.CreateLog("Badge Company name: " + BadgeCompanyText1 + " is displaying. ");

                //Edit Event Badge Fields values
                contactEdit.EditEventBadgeFields();
                contactEdit.ClickSaveBtn();

                string BadgeFirstNameText2 = contactEdit.TxtBadgeFirstName();
                Assert.AreEqual(BadgeFirstNameText2, " ");
                extentReports.CreateLog("Badge first name field is empty. ");

                string BadgeLastNameText2 = contactEdit.TxtBadgeLastName();
                Assert.AreEqual(BadgeLastNameText2, " ");
                extentReports.CreateLog("Badge Last name field is empty. ");

                string BadgeCompanyText2 = contactEdit.TxtBadgeCompany();
                Assert.AreEqual(BadgeCompanyText2, " ");
                extentReports.CreateLog("Badge Company field is empty. ");

                //Verify DA, Events, GA and Content CHange Date field validation
                string DealAnnouncementsChangeDateText = contactEdit.TxtDealAnnouncementsChangeDate();
                Assert.AreEqual(DealAnnouncementsChangeDateText, " ");
                extentReports.CreateLog("Deal Announcements Change Date is empty. ");

                string text1 = contactEdit.TxtEventsChangeDate();
                Assert.AreEqual(text1, " ");
                extentReports.CreateLog("Events Change Date field is empty. ");

                string text2 = contactEdit.TxtGeneralAnnouncementsChangeDate();
                Assert.AreEqual(text2, " ");
                extentReports.CreateLog("General Announcements Change Date field is empty. ");

                string text3 = contactEdit.TxtContentChangeDate();
                Assert.AreEqual(text3, " ");
                extentReports.CreateLog("Content Change Date field is empty. ");

                //Edit Subscription Preference Fields values
                contactEdit.EditSubscriptionPreferenceFields();
                contactEdit.ClickSaveBtn();
                extentReports.CreateLog("Subscription Preference Fields are edited. ");

                string getDate = DateTime.Today.AddDays(0).ToString("M/d/yyyy");
                Console.WriteLine(getDate);
                string date = getDate.Replace('-', '/');

                string text4 = contactEdit.TxtDealAnnouncementsChangeDate();
                Assert.AreEqual(text4, date);
                extentReports.CreateLog("Deal Announcements Change Date: "+ text4 + " is displaying. ");

                string text5 = contactEdit.TxtEventsChangeDate();
                Assert.AreEqual(text5, date);
                extentReports.CreateLog("Events Change Date: " + text5 + " is displaying. ");

                string text6 = contactEdit.TxtGeneralAnnouncementsChangeDate();
                Assert.AreEqual(text6, date);
                extentReports.CreateLog("General Announcements Change Date: " + text6 + " is displaying. ");

                string text7 = contactEdit.TxtContentChangeDate();
                Assert.AreEqual(text7, date);
                extentReports.CreateLog("Content Change Date: " + text7 + " is displaying. ");

                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC1047,ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                extentReports.CreateLog("Deletion of Created Contact. ");

                usersLogin.UserLogOut();
            }

            catch(TimeoutException te)
            {
                extentReports.CreateLog(te.Message);
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