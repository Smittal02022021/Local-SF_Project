using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.TestCases.Contact
{
    class T1549_Contacts_ContactsDetailsPage_HLRelationship_SortableView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        MassRelationshipCreatorPage creatorPage = new MassRelationshipCreatorPage();
        ContactRelationshipPage contactRelationship = new ContactRelationshipPage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1549 = "T1549_Contacts_ContactsDetailsPage_HLRelationship_SortableView";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ContactsDetailsPage_HLRelationship_SortableView()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1549;
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
                homePage.SearchUserByGlobalSearch(fileTC1549, user);

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


                // Calling Search contact
                conHome.SearchContact(fileTC1549);
                string contactDetailHeading = createContact.GetContactDetailsHeading();
                Assert.AreEqual("Contact Detail", contactDetailHeading);
                extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon search for a contact ");

                // Click on sortable view button under HL Relationship
                contactDetails.ClickSortableViewBtnUnderHLRelationship();
                
                //Verify relationship page title
                string relationshipPageTitle = contactRelationship.GetRelationshipTitle();
                Assert.AreEqual("Relationships", relationshipPageTitle);
                extentReports.CreateLog("Page with heading: " + relationshipPageTitle + " is displayed upon click of Sortable View Button under HL Relationship ");

                //Verify contact name with Relationship as title
                string contactPageTitle = contactRelationship.GetContactTitle();
                string contactFirstName = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string contactLastName = ReadExcelData.ReadData(excelPath, "Contact", 3);
                Assert.AreEqual(contactFirstName + " " + contactLastName, contactPageTitle);
                extentReports.CreateLog("Relationship of the Contact: " + contactPageTitle + " is displayed upon click of Sortable View Button under HL Relationship ");

                //Get column name and validate sorting of columns
                IWebElement colName = contactRelationship.GetColName();            
                string descResult = contactRelationship.ValidateSorting(colName, "Descending");
                Assert.AreEqual("True", descResult);
                extentReports.CreateLog("Contact Name column is sorted in descending order " + descResult + " ");
                
                //Get column name and validate sorting of columns
                IWebElement colName2 = contactRelationship.GetColName();
                string ascResult = contactRelationship.ValidateSorting(colName2, "Ascending");
                Assert.AreEqual("True", ascResult);
                extentReports.CreateLog("Contact Name column is sorted in ascending order " + ascResult + " ");

                //Click on Return To Contact Button
                contactRelationship.ClickReturnToContact();
                Assert.AreEqual("Contact Detail", contactDetailHeading);
                extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon click of return to contact button ");

                //string contactDetailHeading = contactDetails.GetContactDetailTitle();
                usersLogin.UserLogOut();
                Thread.Sleep(1000);
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