using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Contact
{
    class T1137_T1138_Contacts_AffiliatedCompany_NewAffiliation_SaveCancelEditUpdate : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactHomePage contactHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        AddAffiliatedCompanies addAffiliated = new AddAffiliatedCompanies();
        CompanyListCreatePage companyListCreate = new CompanyListCreatePage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1137_TC1138 = "T1137_T1138_Contacts_AffiliatedCompany_NewAffiliation_SaveCancelEditUpdate";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_AffiliatedCompanyNewAffiliationSaveAndCancel()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1137_TC1138;
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
                homePage.SearchUserByGlobalSearch(fileTC1137_TC1138, user);

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

                //Search external contact
                conHome.SearchContact(fileTC1137_TC1138);
                string contactDetailHeading = createContact.GetContactDetailsHeading();
                Assert.AreEqual("Contact Detail", contactDetailHeading);
                extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon search for a contact ");

                // Click on new affilation button
                contactDetails.ClickNewAffiliationButton();
                string newAffiliateHeading = addAffiliated.GetNewAffliationCompaniesHeading();
                extentReports.CreateLog("Page with heading: " + newAffiliateHeading + " is displayed upon click of new affiliation button ");

                //Verify Company,Contact,Status and Type required fields in information section 
                Assert.IsTrue(addAffiliated.ValidateMandatoryFields(), "Validated Mandatory Fields");
                extentReports.CreateLog("Validated Company, Contact,Status and Type displayed with red flag as mandatory fields ");

                //Click on save button
                addAffiliated.ClickSaveButton();
                string pageLevelError = addAffiliated.GetPageLevelError();
                Assert.AreEqual("Error: Invalid Data.\r\nReview all error messages below to correct your data.", pageLevelError);
                extentReports.CreateLog("New Affiliation page error message displayed upon click of save button without entering details as: " + pageLevelError + " ");

                //Validation of company name error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Company").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Affiliation Company name error message displayed upon click of save button without entering details ");

                //Validation of company name error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Type").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Affiliation company type error message displayed upon click of save button without entering details ");

                addAffiliated.EnterNewAffilationCompaniesDetails(fileTC1137_TC1138);
                addAffiliated.ClickCancelButton();
                //Validate no affiliate company is created after entering details and click cancel button
                Assert.IsFalse(contactDetails.ValidateNewAffilationCompaniesCreation(),"Affiliation company not created.");
                extentReports.CreateLog("Affiliation company is not created upon entering details and click of cancel button ");

                // Click on new affilation button
                contactDetails.ClickNewAffiliationButton();
                addAffiliated.EnterNewAffilationCompaniesDetails(fileTC1137_TC1138);
                addAffiliated.ClickSaveButton();

                //Validate company name
                string affiliationCompanyName = contactDetails.GetAffiliationCompanyName();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 1), affiliationCompanyName);
                extentReports.CreateLog("Affiliation Company Name: " + affiliationCompanyName + " on contact details page matches with value entered in new affiliation company page ");

                //Validate company status
                string affiliationCompanyStatus = contactDetails.GetAffiliationCompanyStatus();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 3), affiliationCompanyStatus);
                extentReports.CreateLog("Affiliation Company Status: " + affiliationCompanyStatus + " on contact details page matches with value entered in new affiliation company page ");

                //Validate company type
                string affiliationCompanyType = contactDetails.GetAffiliationCompanyType();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 4), affiliationCompanyType);
                extentReports.CreateLog("Affiliation Company Type: " + affiliationCompanyType + " on contact details page matches with value entered in new affiliation company page ");

                //Edit affiliation company 
                addAffiliated.EditNewAffilationCompaniesDetails(fileTC1137_TC1138);
                addAffiliated.ClickSaveAndNewButton();

                string getCompanyName = addAffiliated.GetCompanyNameText();
                Assert.AreEqual("", getCompanyName);
                extentReports.CreateLog("Company name is blank upon edit details of affilation company and click on save and new button ");

                string getContactName = addAffiliated.GetContactNameText();
                Assert.AreEqual("", getContactName);
                extentReports.CreateLog("Contact name is blank upon edit details of affilation company and click on save and new button ");
                
                //Click on cancel button
                addAffiliated.ClickCancelButton();
               
                //Validate company status
                string updatedAffiliationCompanyStatus = contactDetails.GetAffiliationCompanyStatus();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "EditAffiliatedCompany", 1), updatedAffiliationCompanyStatus);
                extentReports.CreateLog("Affiliation Company Status: " + affiliationCompanyStatus + " on contact details page matches with value entered in new affiliation company page ");

                //Validate company type
                string updatedAffiliationCompanyType = contactDetails.GetAffiliationCompanyType();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "EditAffiliatedCompany", 2), updatedAffiliationCompanyType);
                extentReports.CreateLog("Affiliation Company Type: " + affiliationCompanyType + " on contact details page matches with value entered in new affiliation company page ");

                usersLogin.UserLogOut();
                extentReports.CreateLog("Logout from standard user ");

                // Delete affiliated companies
                contactHome.SearchContact(fileTC1137_TC1138);               
                contactDetails.DeleteAffiliatedCompanies(fileTC1137_TC1138, ReadExcelData.ReadData(excelPath, "ContactTypes", 1));
                Assert.IsFalse(contactDetails.ValidateNewAffilationCompaniesCreation(), "Affiliation company is not available after deletion ");
                extentReports.CreateLog("Affiliation company is not available after deletion ");

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
