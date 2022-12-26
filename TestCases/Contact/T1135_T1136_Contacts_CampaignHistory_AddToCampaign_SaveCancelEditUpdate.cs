using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Contact
{
    class T1135_T1136_Contacts_CampaignHistory_AddToCampaign_SaveCancelEditUpdate : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        CampaignHomePage campHome = new CampaignHomePage();
        NewCampaignPage newCamp = new NewCampaignPage();
        CampaignMemberEditPage camMemEdit = new CampaignMemberEditPage();
        CampaignDetailPage campDetail = new CampaignDetailPage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();

        public static string fileTC1135_TC1136 = "T1135_T1136_Contacts_CampaignHistory_AddToCampaign_SaveCancelEditUpdate";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_CampaignHistory_AddToCampaign_SaveCancelEditUpdate()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1135_TC1136;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Navigate To Parent Campaign creation page
                campHome.NavigateToNewCampaignPage(fileTC1135_TC1136);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Campaign Edit: New Campaign ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Get Campaign record type value displayed on page
                string record = newCamp.GetCampaignRecordTypeValue();

                //Validate if user landed on correct page
                Assert.AreEqual("Parent Campaign", record);
                extentReports.CreateLog("Create new campaign page of record type: " +record+ " is displayed. ");

                //Create new parent campaign
                newCamp.CreateNewParentCampaign(fileTC1135_TC1136);

                //Validate if new parent campaign is created
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Campaign: Test Parent Campaign ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling click add contact function
                conHome.ClickAddContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: Select Contact Record Type ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("User navigate to add contact page. ");

                //Select contact record type
                conSelectRecord.SelectContactRecordType(fileTC1135_TC1136, ReadExcelData.ReadData(excelPath, "ContactTypes", 1));

                //Calling CreateContact function to create new external contact
                createContact.CreateContact(fileTC1135_TC1136);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Test ExternalContact ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("New external contact is created. ");

                //Search external contact
                conHome.SearchContact(fileTC1135_TC1136);
                string contactDetailHeading = createContact.GetContactDetailsHeading();
                Assert.AreEqual("Contact Detail", contactDetailHeading);
                extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon search for a contact ");

                //Click on Add To Campaign button
                contactDetails.ClickAddToCampaignButton();
                extentReports.CreateLog("Add to campaign history popup is open. ");

                //Search for an existing campaign
                contactDetails.SearchAndSelectCampaignName(fileTC1135_TC1136);

                //Validating Title of Campaign Member Edit Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Campaign Member Edit: New Campaign Member ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validate the error message displayed upon clicking Save button
                string err = camMemEdit.GetErrorMessage();
                Assert.AreEqual("Error: Invalid Data.\r\nReview all error messages below to correct your data.", err);
                extentReports.CreateLog("Error message : " + err + " is displayed upon clicking save button. ");

                //Add Campaign Memer
                camMemEdit.AddCampaignMember(fileTC1135_TC1136);

                //Validate Campaign Member page title
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Campaign Member: " + ReadExcelData.ReadData(excelPath, "Contact", 4) + " (" + ReadExcelData.ReadData(excelPath, "Campaign", 2) + ") ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Navigate To Campaign Tab
                campHome.ClickCampaignTab();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Campaigns: Home ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Navigate to Campaign Detail page
                campHome.NavigateToCampaignDetailPage(fileTC1135_TC1136);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Campaign: Test Parent Campaign ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Delete contact from campaign history
                campDetail.DeleteContactFromCampaignHistory();
                extentReports.CreateLog("External Contact is deleted from the campaign history. ");

                //Delete Campaign
                campDetail.DeleteCampaign();
                extentReports.CreateLog("Campaign deleted. ");

                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC1135_TC1136, "External Contact");
                extentReports.CreateLog("External contact is deleted. ");

                usersLogin.UserLogOut();
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
