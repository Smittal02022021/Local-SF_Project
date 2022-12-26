using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;


namespace SalesForce_Project.TestCases.Companies
{
    class T1144_T1146_Companies_AddNewCompanyWithRecordTypeForCapitalProviderAndOperatingCompany : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        By errPage = By.CssSelector("span[id='theErrorPage:theError']");
        public static string fileTC1144_TC1146 = "T1144_T1146_Companies_AddNewCompanyWithRecordTypeForCapitalProviderAndOperatingCompany.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddNewCompanyWithRecordTypeForCapitalProviderAndOperatingCompany()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1144_TC1146;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Handling salesforce Lightning
                login.HandleSalesforceLightningPage();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");


                // Click Add Company button
                companyHome.ClickAddCompany();

                //Verify company record types and description on company record page
                companySelectRecord.VerifyCompanyRecordTypesandDesc(fileTC1144_TC1146);

                extentReports.CreateLog("Verified company record types and description on company record page");

                //Verify continue and cancel button on company record page
                companySelectRecord.VerifyContinueCancelBtnDisplay();

                extentReports.CreateLog("Verified continue and cancel button on company record page");



                int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    // Search standard user by global search
                    string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                    homePage.SearchUserByGlobalSearch(fileTC1144_TC1146,user);
                    string userPeople = homePage.GetPeopleOrUserName();
                    string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                    Assert.AreEqual(userPeopleExl, userPeople);
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    //Login as standard user
                    usersLogin.LoginAsSelectedUser();
                    string StandardUser = login.ValidateUser();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Users", 1).Contains(StandardUser), true);
                    extentReports.CreateLog("Standard User: " + StandardUser + " is able to login ");

                    // Click Add Company button
                    companyHome.ClickAddCompany();
                    string companyRecordTypePage = companySelectRecord.GetCompanyRecordTypePageHeading();
                    Assert.AreEqual("Select Company Record Type", companyRecordTypePage);
                    extentReports.CreateLog("Page with heading: "+ companyRecordTypePage + " is displayed upon click add company button ");
                    



                    // Select company record type
                    string recordType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companySelectRecord.SelectCompanyRecordType(fileTC1144_TC1146, recordType);
                    string createCompanyPage = createCompany.GetCreateCompanyPageHeading();
                    Assert.AreEqual("Company Create", createCompanyPage);
                    extentReports.CreateLog("Page with heading: "+ createCompanyPage + " is displayed upon selecting company record type ");

                    // Validate company type display as selected 
                    Assert.AreEqual(recordType, createCompany.GetSelectedCompanyType());
                    extentReports.CreateLog("Selected company type: " + createCompany.GetSelectedCompanyType() + " choosen on select company record type page is matching on Company create page ");
                    
                    
                    
                    //Verify required information field error

                    string errmsg=createCompany.errmsgCompanyName();
                    Assert.AreEqual(errmsg, "Error: You must enter a value");
                    extentReports.CreateLog("Error message:"+errmsg+"is displaying for blank input data for company record type");

                    // Create a  company
                    createCompany.AddCompany(fileTC1144_TC1146, row);

                    //Validate company detail heading
                    string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                    string companyDetailHeadingExl = ReadExcelData.ReadData(excelPath, "Company", 8);
                    Assert.AreEqual(companyDetailHeadingExl, companyDetailHeading);
                    extentReports.CreateLog("Page with heading: "+companyDetailHeading + " is displayed upon adding company ");

                    // Validate company name value
                    string companyName = companyDetail.GetCompanyName();
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    Assert.AreEqual(companyNameExl, companyName);
                    extentReports.CreateLog("Company name: "+companyName + " in add company page matches on company details page ");

                    // Validate company location value 
                    string CompanyLocation = companyDetail.GetCompanyLocation();
                    string cityExl = ReadExcelData.ReadData(excelPath, "Company", 5);
                    string stateExl = ReadExcelData.ReadData(excelPath, "Company", 6);
                    Assert.AreEqual(cityExl + ", " + stateExl, CompanyLocation);
                    extentReports.CreateLog("Company Location: "+CompanyLocation + " including city and state in add company page matches on company details page ");

                    // Validate company type value
                    string CompanyType = companyDetail.GetCompanyType();
                    string companyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    Assert.AreEqual(companyTypeExl, CompanyType);
                    extentReports.CreateLog("Company Type: "+CompanyType + " in add company page matches on company details page ");

                    // Click edit button

                    string companyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companyDetail.ClickEditButton(fileTC1144_TC1146, companyType);

                    // Validate company edit heading
                    string companyEditHeading = companyEdit.GetCompanyEditPageHeading();
                    Assert.AreEqual("Company Edit", companyEditHeading);
                    extentReports.CreateLog("Page with heading: "+companyEditHeading + " is displayed upon click edit button ");

                    // Enter values in edit company page
                    companyEdit.EditCompanyDetails(fileTC1144_TC1146, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1));

                    extentReports.CreateLog("Required field values are entered into edit company details page ");
                    
                    if (CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1)))
                    {
                        //Validate company sub type
                        string companySubType = companyDetail.GetCompanySubType();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 9), companySubType);
                        extentReports.CreateLog("Company sub type: "+companySubType + " is displayed on company details page ");
                      
                        //Validate Ownership value
                        string ownership = companyDetail.GetOwnership();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 10), ownership);
                        extentReports.CreateLog("Ownership: "+ownership + " is displayed on company details page ");

                        //Validate parent company value
                        //string parentCompany = companyDetail.GetParentCompany();
                        //Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 11), parentCompany);
                        //extentReports.CreateLog("Parent company: "+parentCompany + " is displayed on company details page ");

                        //Validate industry focus value
                        string industryFocus = companyDetail.GetIndustryFocus();
                        Assert.AreEqual("Alternative Energies", industryFocus);
                        extentReports.CreateLog("Industry Focus: "+industryFocus + " is displayed on company details page ");

                        //Validate geographical focus value
                        string geographicalFocus = companyDetail.GetGeographicalFocus();
                        Assert.AreEqual("Asia", geographicalFocus);
                        extentReports.CreateLog("Company's Geographical focus: "+geographicalFocus + " is displayed on company details page ");

                        //Validate deal preference focus value
                        string dealPreference = companyDetail.GetDealPreference();
                        Assert.AreEqual("Credit - Corporate", dealPreference);
                        extentReports.CreateLog("Deal Preference: "+dealPreference + " is displayed on company details page ");
                    }
                    //Validate description input
                    string descriptionInput = companyDetail.GetDescriptionValue();
                    string descriptionInputExl = ReadExcelData.ReadData(excelPath, "Company", 12);
                    Assert.AreEqual(descriptionInputExl, descriptionInput);
                    extentReports.CreateLog("Company description: "+descriptionInput + " is displayed on company details page ");

                    //Validate capIQCompany selected
                    //string capIQCompany = companyDetail.GetCapIQCompany();
                    //string capIQCompanyExl = ReadExcelData.ReadData(excelPath, "Company", 13);
                    ///Assert.AreEqual(capIQCompanyExl, capIQCompany);
                    //extentReports.CreateLog(capIQCompany + " as company's associated capIQ company value is displayed ");

                   

    
                    // Logout from standard User
                    usersLogin.UserLogOut();
                    extentReports.CreateLog("Standard user is logged out ");

                    //Cleanup 
                    //Search company
                    //string companyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companyHome.SearchCompany(fileTC1144_TC1146, companyType);
                    
                    if (CustomFunctions.IsElementPresent(driver, errPage))
                    {
                        companyHome.SearchCompany(fileTC1144_TC1146, CompanyType);
                    }

                    if (companyType.Equals("Operating Company"))
                    {
                        companyDetail.EditCompanyRecordType(fileTC1144_TC1146);
                    }

    
                    //Delete company
                    companyDetail.DeleteCompany(fileTC1144_TC1146, companyType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("Created company is deleted successfully ");
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
