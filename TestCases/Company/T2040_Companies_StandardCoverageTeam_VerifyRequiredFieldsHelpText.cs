using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Companies
{
    class T2040_Companies_StandardCoverageTeam_VerifyRequiredFieldsHelpText : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactCreatePage createContact = new ContactCreatePage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        AddCoverageTeam coverageTeam = new AddCoverageTeam();

        public static string fileTC2040 = "T2040_Companies_StandardCoverageTeam_VerifyRequiredFieldsHelpText";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyRequiredFieldsHelpTextForStandardCoverageTeam()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2040;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                CustomFunctions.TableauPopUp();
                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC2040, user);

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

                // Calling Search Company function
                companyHome.SearchCompany(fileTC2040, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 3), companyDetailHeading);
                extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon searching company ");

                // Click on new coverage team button
                companyDetail.ClickNewCoverageTeam();
                string coverageTeamEditHeading = companyDetail.GetCoverageTeamEditPageHeading();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 4), coverageTeamEditHeading);
                extentReports.CreateLog("Page with heading: " + coverageTeamEditHeading + " is displayed upon click of new coverage team button ");


                // Validation of fields Company\, Officer\, Coverage Level\, Type and Tier marked with Red Icon 
                companyDetail.ValidateMandatoryFields();
                extentReports.CreateLog("Validation of mandatory fields for Comapany,Officer,Coverage Level,Type and Tier  ");

                //// Pre requisite to validate error message for company,officer,coverage level,coverage type and tier
                companyDetail.ClearTextCompanyName();
                companyDetail.ClickSaveButton();

                //Validation of company error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Company").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Company name error message displayed upon click of save button without entering details ");

                //Validation of officer error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Officer").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Officer error message displayed upon click of save button without entering details ");

                //Validation of coverage level error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Coverage Level").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Coverage level error message displayed upon click of save button without entering details ");

                //Validation of coverage type error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Type").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Coverage type error message displayed upon click of save button without entering details ");

                //Validation of tier error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Tier").Text.Contains("Error: You must enter a value"));
                extentReports.CreateLog("Coverage tier error message displayed upon click of save button without entering details ");

                //Validate availablity of Priority Help tool tip
                Assert.IsTrue(companyDetail.ValidatePriorityHelpObject());
                extentReports.CreateLog("Priority help tooltip is available ");

                // Validate text of priority help 
                Assert.AreEqual("'Highest (Pitch in less than 6 months)\\u003Cbr\\u003EHigh (Pitch 6-12 months)\\u003Cbr\\u003EMedium (Pitch 12-24 months)\\u003Cbr\\u003ELow (Pitch +24 months)');", companyDetail.GetPriorityHelpText());
                extentReports.CreateLog("Priority help tooltip text matches ");

                //Add new coverage team
                coverageTeam.AddNewCoverageTeam(fileTC2040,8);

                //Verify Coverage Team Officer
                string officerName = companyDetail.GetCoverageTeamOfficer();
                string officerNameExl = ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 2);
                Assert.AreEqual(officerNameExl, officerName);
                extentReports.CreateLog("Coverage Team Officer: " + officerName + " is displayed on company detail page under Coverage Team section upon adding a new coverage team ");

                //Verify Coverage Team Level 
                string coverageLevel = companyDetail.GetCoverageLevel();
                string coverageLevelExl = ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 3);
                Assert.AreEqual(coverageLevelExl, coverageLevel);
                extentReports.CreateLog("Coverage Level: " + coverageLevel + " is displayed on company detail page under Coverage Team section upon adding a new coverage team ");

                //Verify Coverage Team Type
                string coverageType = companyDetail.GetCoverageType();
                string coverageTypeExl = ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 4);
                Assert.AreEqual(coverageTypeExl, coverageType);
                extentReports.CreateLog("Coverage Type: " + coverageType + " is displayed on company detail page under Coverage Team section upon adding a new coverage team ");


                usersLogin.UserLogOut();
                // Cleanup code
                companyHome.SearchCompany(fileTC2040, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));

                companyDetail.DeleteCoverageTeamRecord(fileTC2040, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                //This is not available for now 
                //companyDetail.DeleteCoverageSector(fileTC2040, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));

                //companyHome.SearchCompany(fileTC2040, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));
                //This is not available for now
                //companyDetail.DeleteCoverageTeamRecord(fileTC2040, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1));

                extentReports.CreateLog("Coverage Team is deleted successfully ");

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

