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
    class T1063_Contacts_AddMultipleContactsToAnExistingCompanyAndValidateSaveAndNewButton : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ContactEditPage contactEdit = new ContactEditPage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1063 = "T1063_Contacts_AddMultipleContactsToAnExistingCompanyAndValidateSaveAndNewButton";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddMultipleContactsToAnExistingCompanyAndValidateSaveAndNewButton()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1063;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowUserType = ReadExcelData.GetRowCount(excelPath, "UsersType");
                for (int rows = 2; rows <= rowUserType; rows++)
                {
                    if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", rows, 1).Equals("HR"))
                    {
                        // Search standard user by global search
                        string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                        homePage.SearchUserByGlobalSearch(fileTC1063, user);

                        //Verify searched user
                        string userPeople = homePage.GetPeopleOrUserName();
                        string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                        Assert.AreEqual(userPeopleExl, userPeople);
                        extentReports.CreateLog("User " + userPeople + " details are displayed ");

                        //Login as HR user
                        usersLogin.LoginAsSelectedUser();
                        string HRUser = login.ValidateUser();
                        string HRUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                        Assert.AreEqual(HRUserExl.Contains(HRUser), true);
                        extentReports.CreateLog("HR User: " + HRUser + " is able to login ");
                    }
                    // Calling click contact function
                    conHome.ClickContact();
                    string contactPageHeading = conHome.GetContactPageHeading();
                    Assert.AreEqual("Contacts", contactPageHeading);
                    extentReports.CreateLog("Contact Page Heading: " + contactPageHeading + " is displayed on click of Contacts tab");

                    // Calling click add contact function
                    conHome.ClickAddContact();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: Select Contact Record Type ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("user navigate to select contact record type page upon click of add contact button ");

                    int rowContact = ReadExcelData.GetRowCount(excelPath, "Contact");
                    for (int row = 2; row <= rowContact; row += 2)
                    {
                        // Calling select record type and click continue function
                        conSelectRecord.SelectContactRecordType(fileTC1063, row);
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("user navigate to create contact page upon click of continue button ");

                        // To create contact
                        createContact.CreateContact(fileTC1063, row);
                        string contactDetailHeading = createContact.GetContactDetailsHeading();
                        Assert.AreEqual("Contact Detail", contactDetailHeading);
                        extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon creation of a contact ");
                        string contactType = ReadExcelData.ReadData(excelPath, "ContactTypes", row);
                        if (contactType.Contains("External Contact") || contactType.Contains("Houlihan Employee"))
                        {
                            extentReports.CreateLog(contactDetails.GetContactRecordTypeValue() + " record is created ");
                        }
                        else
                        {
                            extentReports.CreateLog("Distribution Lists record is created ");
                        }
                        //To edit contact
                        contactEdit.EditContact(fileTC1063, row, rows);
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: Select Contact Record Type ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("user reaches to select contact record type page upon editing contact details and click on Save and New button ");

                        // Calling select record type and click continue function
                        conSelectRecord.SelectContactRecordType(fileTC1063, row + 1);
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("user navigate to create contact page upon click of continue button ");

                        // To create contact
                        createContact.CreateContact(fileTC1063, row + 1);
                        Assert.AreEqual("Contact Detail", contactDetailHeading);
                        extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon creation of a contact ");
                        if (contactType.Contains("External Contact") || contactType.Contains("Houlihan Employee"))
                        {
                            extentReports.CreateLog(contactDetails.GetContactRecordTypeValue() + " record is created ");
                        }
                        else
                        {
                            extentReports.CreateLog("Distribution Lists record is created ");
                        }
                        //To edit contact
                        contactEdit.EditContact(fileTC1063, row + 1, rows);
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: Select Contact Record Type ~ Salesforce - Unlimited Edition"), true);
                        extentReports.CreateLog("user reaches to select contact record type page upon editing contact details and click on Save and New button ");
                    }
                    if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", rows, 1).Equals("HR"))
                    {
                        usersLogin.UserLogOut();
                    }
                    int rowContactType = ReadExcelData.GetRowCount(excelPath, "ContactTypes");
                    for (int row = 2; row <= rowContactType; row++)
                    {
                       
                        // Calling Search Contact function
                        string contactType = ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1);
                        conHome.SearchContact(fileTC1063, contactType);
                        string contactDetailHeading = createContact.GetContactDetailsHeading();
                        Assert.AreEqual("Contact Detail", contactDetailHeading);
                        extentReports.CreateLog("Page with heading: " + contactDetailHeading + " is displayed upon search for a contact ");

                        // Assertion to validate the company name selected display on contact details page
                        string companyName = contactDetails.GetCompanyName();
                        Assert.AreEqual(contactDetails.GetCompanyNameFromExcel(fileTC1063), companyName);
                        extentReports.CreateLog("Company Name: " + companyName + " in add contact page matches on contact details page");

                        // Assertion to validate the contact full name selected display on contact details page
                        string getFirstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                        string getMiddleName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 7);
                        string getLastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3);
                        if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", rows, 1).Equals("Admin"))
                        {
                            Assert.AreEqual(getFirstName + " " + getMiddleName + " " + getLastName, contactDetails.GetFirstAndLastName());
                            extentReports.CreateLog("First Name,Middle Name and Last Name: " + contactDetails.GetFirstAndLastName() + " in add contact page matches on contact details page");

                            //Validated  address
                            string contactCompleteAddress = contactDetails.GetContactCompleteAddress();

                            Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 8) + "\r\n" + ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 9) + ", " + ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 10) + " 66212" + "\r\n" + ReadExcelData.ReadData(excelPath, "Contact", 11), contactCompleteAddress);
                            extentReports.CreateLog("Contact address: " + contactCompleteAddress + " including street,city and country in edit contact page matches on contact details page ");

                        }
                        else
                        {
                            Assert.AreEqual(getFirstName + " " + getLastName, contactDetails.GetFirstAndLastName());
                            extentReports.CreateLog("First Name,Middle Name and Last Name: " + contactDetails.GetFirstAndLastName() + " in add contact page matches on contact details page");
                        }
                        
                        if (ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1).Contains("Houlihan Employee") && ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", rows, 1).Equals("Admin"))
                        {
                            string contactStatus = contactDetails.GetContactStatus();
                            Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Contact", 12), contactStatus);
                            extentReports.CreateLog("Contact status: " + contactStatus + " in edit contact page matches on contact details page of HL contact ");

                            string contactOffice = contactDetails.GetContactOffice();
                            Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Contact", 13), contactOffice);
                            extentReports.CreateLog("Contact office: " + contactOffice + " in edit contact page matches on contact details page of HL contact ");

                            string contactPhysicalOffice = contactDetails.GetContactPhysicalOffice();
                            Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Contact", 14), contactPhysicalOffice);
                            extentReports.CreateLog("Contact physical office: " + contactPhysicalOffice + " in edit contact page matches on contact details page of HL contact ");

                            string contactTitle = contactDetails.GetContactTitle();
                            Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Contact", 15), contactTitle);
                            extentReports.CreateLog("Contact Title: " + contactTitle + " in edit contact page matches on contact details page of HL contact ");

                            string contactDept = contactDetails.GetContactDepartment();
                            Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Contact", 16), contactDept);
                            extentReports.CreateLog("Contact Department: " + contactDept + " in edit contact page matches on contact details page of HL contact ");
                        }
                      
                        //To Delete created contact
                        contactDetails.DeleteCreatedContact(fileTC1063, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1));
                        extentReports.CreateLog("Deletion of Created Contact ");
                    }
                }
                usersLogin.UserLogOut();
                driver.Quit();
            }

            /*catch (TimeoutException te)
            {
                extentReports.CreateLog(te.Message);
            }*/

            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}