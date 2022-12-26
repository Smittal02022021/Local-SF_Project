using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Contact
{
    class T1048_T1942_T1943_T1944_T1945_T1946_T1951_T1954_T1955_T1957_T1958_Contact_CreateNewContactRecordTypeHoulihanEmployee : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        ContactEditPage contactEdit = new ContactEditPage();

        public static string fileTC1048 = "T1048_Contact_CreateNewContactRecordTypeHoulihanEmployee";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CreateContactTypeHoulihanEmployee()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1048;
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
                for (int row = 2; row <= rowUserType; row++)
                {
                    if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", row, 1).Equals("HR"))
                    {
                        // Search standard user by global search
                        string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                        homePage.SearchUserByGlobalSearch(fileTC1048, user);

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
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    // Calling click add contact function
                    conHome.ClickAddContact();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: Select Contact Record Type ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("user navigate to select contact record type page upon click of add contact button ");

                    // Calling select record type and click continue function
                    string contactType = ReadExcelData.ReadData(excelPath, "Contact", 7);
                    conSelectRecord.SelectContactRecordType(fileTC1048, contactType);
                    extentReports.CreateLog("user navigate to create contact page upon click of continue button ");

                    //Validate FirstName, LastName and CompanyName display with red flag as mandatory fields
                    Assert.IsTrue(createContact.ValidateMandatoryFields(), "Validate Mandatory fields");
                    extentReports.CreateLog("Validated FirstName, LastName and CompanyName displayed with red flag as mandatory fields ");

                    //Calling click save button function
                    createContact.ClickSaveButton();

                    //Validation of company error message
                    Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Company Name").Text.Contains("Error: You must enter a value"));
                    extentReports.CreateLog("Company name error message displayed upon click of save button without entering details ");

                    //Validation of first name error message
                    Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "First Name").Text.Contains("Error: You must enter a value"));
                    extentReports.CreateLog("First name error message displayed upon click of save button without entering details ");

                    //Validation of last name error message
                    Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Last Name").Text.Contains("Error: You must enter a value"));
                    extentReports.CreateLog("Last name error message displayed upon click of save button without entering details ");

                    // Calling CreateContact function to create new HL contact
                    createContact.CreateContact(fileTC1048, row);
                    extentReports.CreateLog("Houlihan Employee created ");

                    // Assertion to validate the company name selected display on contact details page
                    string companyName = contactDetails.GetCompanyName();
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);
                    Assert.AreEqual(companyNameExl, companyName);
                    extentReports.CreateLog("Company Name: " + companyName + " in add contact page matches on contact details page");

                    // Assertion to validate the contact first and last name selected display on contact details page
                    string firstNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                    string lastNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3);
                    string completeName = contactDetails.GetFirstAndLastName();
                    Assert.AreEqual(firstNameExl + " " + lastNameExl, completeName);
                    extentReports.CreateLog("First Name and Last Name: " + completeName + " in add contact page matches on contact details page");

                    //Assertion to validate contact record type
                    string contactRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 7);
                    string contactRecordType = contactDetails.GetContactRecordTypeValue();
                    Assert.AreEqual(contactRecordTypeExl, contactRecordType);
                    extentReports.CreateLog("Validation of contact with Record Type " + contactRecordType + " created with detailed information" +
                        " ,Contact Record type is displayed under system information section ");




                    if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", row, 1).Equals("HR"))
                    {

                        //Verify Validation message is dispalying when HR user tries to edit employee currency
                       // contactEdit.EditContact(fileTC1048, 2, 2);
                       contactEdit.VerifyEmployeeCurrencyValidation("AUD - Australian Dollar");
                        contactEdit.ClickSaveBtn();
                        String errMessage1 = contactEdit.TxtErrorMessageDepartureDate();
                        Assert.AreEqual("Error: Only system administrators can change employee currency", errMessage1);
                        extentReports.CreateLog("Error message: " + errMessage1 + " is displaying when HR user tries to change currency ");
                        contactEdit.ClickCancelBtn();

                        //Verify Validation message is dispalying when HR user tries to edit employee Name
                        contactEdit.VerifyLastNameValidation();
                        contactEdit.ClickSaveBtn();
                        String errMessage2 = contactEdit.TxtErrorMessageDepartureDate();
                        Assert.AreEqual("Error: Only system administrators can change employee name and salutation", errMessage2);
                        extentReports.CreateLog("Error message: " + errMessage2 + " is displaying when HR user tries to change Name ");
                        contactEdit.ClickCancelBtn();

                        usersLogin.UserLogOut();
                        conHome.SearchContact(fileTC1048, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 3, 1));
                        
                       // contactDetails.DeleteCreatedContact(fileTC1048, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 3, 1));
                       // extentReports.CreateLog("Deletion of Created Contact ");
                    }
                    else
                    {                        
                        //Verify error message is displaying when departure date is before the hire date
                        contactEdit.EditContact(fileTC1048, 2, 2);
                       
                        contactEdit.VerifyDepartureDate();
                        contactEdit.ClickSaveBtn();

                        String errMsg = contactEdit.TxtErrorMessageDepartureDate();
                          Assert.AreEqual("Error: Departure Date cannot be earlier than Hire Date", errMsg);
                        extentReports.CreateLog("Error message: " + errMsg + " is displaying when departure date is before the hire date ");
                        contactEdit.VerifyDepartureDateforInactiveEmployee();

                        
                        contactEdit.ClickSaveBtn();
                        //Verify error message is displaying when departure date is not provided for inactive employee
                        string errMsg1 = contactEdit.TxtErrorMessageDepartureDate();
                        Assert.AreEqual("Error: Departure Date is required for Inactive employees hired after 1/1/2009", errMsg1);
                        extentReports.CreateLog("Error message: " + errMsg1 + " is displaying when departure date is required for inactive employees");


                        
                        contactEdit.ClickCancelBtn();
                        //Verify error message for Industry group when LOB is CF
                        contactEdit.VerifyIndustryGroupValidation();
                        contactEdit.ClickSaveBtn();

                        string errMsg2 = contactEdit.TxtErrorMessageDepartureDate();
                        Assert.AreEqual("Error: Industry Group must be selected when LOB is CF", errMsg2);
                        extentReports.CreateLog("Error message: " + errMsg2 + " is displaying when industry group must be selected when LOB is CF ");
                        //Verify PDC Validation when primary PDC contact is inactive
                        contactEdit.ClickCancelBtn();
                        contactEdit.VerifyPDCValidation();
                        contactEdit.ClickSaveBtn();

                        string errMsg3 = contactEdit.TxtErrorMessageDepartureDate();
                        Assert.AreEqual("Error: Primary & Secondary PDC must be active when contact is active", errMsg3);
                        extentReports.CreateLog("Error message: " + errMsg3 + " is displaying when PDC selected is inactive ");

                        //Verify PDC Validation when primary PDC is null

                        contactEdit.ClickCancelBtn();
                        contactEdit.VerifyPrimaryPDCValidation();
                        contactEdit.ClickSaveBtn();

                        string errMsg4 = contactEdit.TxtErrorMessageDepartureDate();
                        Assert.AreEqual("Error: Primary PDC cannot be blank when there is a Secondary PDC", errMsg4);
                        extentReports.CreateLog("Error message: " + errMsg4 + " is displaying when Primary PDC is null ");



                        contactEdit.ClickCancelBtn();
                        //Test case is commented as per Greag comment
                       // contactEdit.VerifyExpenseApproverValidation();

                     
                       // contactEdit.ClickSaveBtn();
                        //string errMsg33 = contactEdit.TxtErrorMessageDepartureDate();

                        // extentReports.CreateLog("Error message: " + "Expense Approver must be active when contact is active" + " is displaying when inactive employee is selected for expense approver");


                        //Verify error message is displaying when flag reason comment is not provided

                        contactEdit.VerifyFlagReasonValidation("Deceased");
                        contactEdit.ClickSaveBtn();
                        string errMessage = contactEdit.TxtErrorMessageDepartureDate();
                        contactEdit.ClickCancelBtn();
                        
                        Assert.AreEqual("Error: Please provide additional information for selected Flag Reason.", errMessage);
                        extentReports.CreateLog("Error message: " + errMessage + " is displaying when flag reason is not selected");


                        //contactDetails.DeleteCreatedContact(fileTC1048, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                       //extentReports.CreateLog("Deletion of Created Contact ");
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