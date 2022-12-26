


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
    class T1103_Contact_CreateNewRelationshipInHoulihanEmpDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1103 = "T1103_CreateNewRelationshipInHoulihanEmployeeDetailPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CreateNewHLRelationshipInHLEmployeeDetailPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1103;
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
                homePage.SearchUserByGlobalSearch(fileTC1103, user);

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
                conHome.SearchContact(fileTC1103);
                string contactDetailHeading = createContact.GetContactDetailsHeading();
                Assert.AreEqual("Contact Detail", contactDetailHeading);
                extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon search for a contact ");

                // Clicking on new relationship button
                contactDetails.ClickNewRelationshipButton();
                string relationshipPageHeading = contactDetails.GetRelationshipPageHeading();
                Assert.AreEqual("Relationship", relationshipPageHeading);
                extentReports.CreateLog("Page with heading: " + relationshipPageHeading + " is displayed upon click on new relationship button on contact details page ");
               
                // Calling CreateRelationship method to create relationship of external contact with HL contact
                string contact = ReadExcelData.ReadData(excelPath, "Relationship", 4);
                contactDetails.CreateRelationship(fileTC1103, contact);
                Assert.AreEqual("Contact Detail", contactDetailHeading);
                extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon creating a relationship ");

                // Validate the entry of External Contact        
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Relationship", 4), contactDetails.GetExternalContactText());
                extentReports.CreateLog("External Contact: " + contactDetails.GetExternalContactText() + " is displayed on Relationship detail page ");

                extentReports.CreateLog("HL contact relationship is created with External Contact name ");

                // Validate the entry of relationship with HL contact name in HL Relationship section
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Relationship", 1), contactDetails.GetHLContactLinkText());
                extentReports.CreateLog("Selected HL Contact: "+ contactDetails.GetHLContactLinkText()+" relationship is displayed under relationship section with external contact ");

                // Validate the entry of relationship with HL contact and its strength rating selected in HL Relationship section
                string strengthRating = contactDetails.GetStrengthRating();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Relationship", 3), strengthRating);
                extentReports.CreateLog("Strength rating: "+ strengthRating+" is displayed in HL Relationship section of contact detail page ");

                // Validate the entry of relationship with HL contact and its relationship type selected in HL Relationship section
                string relationshipType = contactDetails.GetRelationshipType();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Relationship", 2), relationshipType);
                extentReports.CreateLog("Relationship type: " + relationshipType + " is displayed in HL Relationship section of contact detail page ");

                usersLogin.UserLogOut();
                driver.Navigate().Refresh();
                Thread.Sleep(1000);
                

                conHome.SearchContact(fileTC1103);
                // Delete the entry of relationship with HL contact in HL Relationship section of external contact detail page.
                contactDetails.DeleteCreatedRelationship();
                extentReports.CreateLog("Deletion of Created Relationship successfully ");

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