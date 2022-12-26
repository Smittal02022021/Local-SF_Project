using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Contact
{
    class T2144_CreateNewContactWithRecordTypeAsExternalContact_WorkFlow_DoNotEmailAndDoNotSolicit : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC2144 = "T2144_CreateNewContactWithRecordTypeAsExternalContact_WorkFlow_DoNotEmailAndDoNotSolicit";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CreateNewContactWithRecordTypeAsExternalContact_WorkFlow_DoNotEmailAndDoNotSolicit()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2144;
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
                homePage.SearchUserByGlobalSearch(fileTC2144, user);

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

                //Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling click add contact function
                conHome.ClickAddContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("User navigate to add contact page. ");

                //Calling CreateContact function to create new HL contact
                createContact.CreateContact(fileTC2144);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Test ExternalContact ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("New external contact is created. ");

                //Verify if both DoNotSolicitChangeDate & DoNotEmailChangeDate fields are empty or not
                Assert.IsTrue(contactDetails.VerifyIfDoNotSolicitChangeDateAndDoNotEmailChangeDateFieldsAreEmptyOrNot());
                extentReports.CreateLog("Both DoNotSolicitChangeDate & DoNotEmailChangeDate fields are empty upon contact creation. ");

                usersLogin.UserLogOut();

                //Search contact
                conHome.SearchContact(fileTC2144);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: Test ExternalContact ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Edit the contact
                contactDetails.ClickEditContactButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact Edit: Test ExternalContact ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("User has navigated to the external contact edit page. ");

                //Update both DoNotSolicitChangeDate & DoNotEmailChangeDate fields to current date
                contactDetails.UpdateBothDoNotSolicitChangeDateFields();

                //Verify if both DoNotSolicitChangeDate & DoNotEmailChangeDate fields are empty or not
                Assert.IsFalse(contactDetails.VerifyIfDoNotSolicitChangeDateAndDoNotEmailChangeDateFieldsAreEmptyOrNot());
                extentReports.CreateLog("Both DoNotSolicitChangeDate & DoNotEmailChangeDate fields are updated. ");

                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC2144, "External Contact");
                extentReports.CreateLog("External contact is deleted. ");

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