using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Companies
{
    class T1176_Companies_AddFVAOpportunityInCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage oppDetails = new OpportunityDetailsPage();
        OpportunityHomePage oppHomePage = new OpportunityHomePage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1176 = "T1176_Companies_AddFVAOpportunityOnCompanyDetailPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddFVAOpportunityInCompanyDetailsPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1176;
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

                //-----Add Cancel Delete FVA opportunity by all types of companies ----//
                int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    // Search standard user by global search
                    string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                    homePage.SearchUserByGlobalSearch(fileTC1176, user);
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
                    string companyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companyHome.SearchCompany(fileTC1176, companyType);
                    string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                    string companyDetailHeadingExl = ReadExcelData.ReadData(excelPath, "Company", 3);
                    Assert.AreEqual(companyDetailHeadingExl, companyDetailHeading);
                    extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon searching company ");

                    // Calling function ClickOpportunityButton to click on Add FVA opportunity button
                    string CompanyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    string HLCompanyValue = ReadExcelData.ReadData(excelPath, "AddOpportunity", 20);
                    string OtherCompanyValue = ReadExcelData.ReadData(excelPath, "AddOpportunity", 21);
                    companyDetail.ClickOpportunityButton(fileTC1176, CompanyType, HLCompanyValue, OtherCompanyValue);

                    //Verify edit opportunity heading page
                    string editOpportunityHeading = addOpportunity.GetEditOpportunityPageHeading();
                    string editOpportunityHeadinExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 16);
                    Assert.AreEqual(editOpportunityHeadinExl, editOpportunityHeading);
                    extentReports.CreateLog("Page with heading:  " + editOpportunityHeading + " is displayed upon clicking opportunity button ");

                    //Validating prefilled opportunity name
                    string opportunityName = addOpportunity.GetPrefilledOpportunityName();
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    Assert.AreEqual(companyNameExl, opportunityName);
                    extentReports.CreateLog("Prefilled opportunity name as " + opportunityName + " is displayed ");

                    //Validating prefilled client name
                    string clientName = addOpportunity.GetPrefilledClientName();
                    Assert.AreEqual(companyNameExl, clientName);
                    extentReports.CreateLog("Prefilled client name as " + clientName + " is displayed ");

                    //Validating prefilled opportunity subject
                    string opportunitySubject = addOpportunity.GetPrefilledOpportunitySubject();
                    Assert.AreEqual(companyNameExl, opportunitySubject);
                    extentReports.CreateLog("Prefilled opportunity subject as " + opportunitySubject + " is displayed ");

                    //Validating prefilled line of business
                    string prefilledLineOfBusiness = addOpportunity.GetPrefilledLineOfBusiness();
                    string LineOfBusinessExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 15);
                    Assert.AreEqual(LineOfBusinessExl, prefilledLineOfBusiness);
                    extentReports.CreateLog("Prefilled opportunity line of business as " + prefilledLineOfBusiness + " is displayed ");
                    
                    //Click on cancel button
                    addOpportunity.ClickCancelButton();
                    Assert.AreEqual(companyDetailHeadingExl, companyDetailHeading);
                    extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon clicking cancel button ");

                    // Calling function ClickOpportunityButton to click on Add FVA opportunity button
                    companyDetail.ClickOpportunityButton(fileTC1176, CompanyType, HLCompanyValue, OtherCompanyValue);

                    //Verify edit opportunity heading
                    string editOppHeadingExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 16);
                    Assert.AreEqual(editOppHeadingExl, editOpportunityHeading);
                    extentReports.CreateLog("Page with heading:  " + editOpportunityHeading + " is displayed upon clicking opportunity button ");

                    //Pre -requisite to clear mandatory values on add opportunity page.
                    addOpportunity.ClearMandatoryValuesOnAddOpportunity();

                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                    // Calling add opportunity function
                    string value = addOpportunity.AddOpportunities(valJobType,fileTC1176);
                    Console.WriteLine("value : " + value);
                    extentReports.CreateLog("Opportunity is created succcessfully ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(fileTC1176);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    // Validate Line of business value
                    string lineOfBusiness = oppDetails.GetLineOfBusiness();
                    string LOBExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 15);
                    Assert.AreEqual(LOBExl, lineOfBusiness);
                    extentReports.CreateLog("Line of Business: " + lineOfBusiness + " entered in add opportunity page matches on Opportunity details page ");

                    // Validate Job type selected
                    string jobType = oppDetails.GetJobType();
                    string jobTypeExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 3);
                    Assert.AreEqual(jobTypeExl, jobType);
                    extentReports.CreateLog("Job Type: " + jobType + " in add opportunity page matches on Opportunity details page ");

                    //Validate Industry group
                    string industryGrp = oppDetails.GetIndustryGroup();
                    string industryGrpExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 4);
                    Assert.AreEqual(industryGrpExl, industryGrp);
                    extentReports.CreateLog("Industry Group: " + industryGrp + " in add opportunity page matches on Opportunity details page ");

                    //Validate sector
                    string sector = oppDetails.GetSector();
                    string sectorExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 5);
                    Assert.AreEqual(sectorExl, sector);
                    extentReports.CreateLog("Sector: " + sector + " in add opportunity page matches on Opportunity details page ");
                    
                    // Validate additonal client 
                    string additionalClient = oppDetails.GetAdditionalClientBoolValue();
                    string additionalClientExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 6);
                    Assert.AreEqual(additionalClientExl, additionalClient);
                    extentReports.CreateLog("Additional Client: " + additionalClient + " in add opportunity page matches on Opportunity details page ");

                    //Validate additional subject
                    string additionalSubject = oppDetails.GetAdditionalSubjectBoolValue();
                    string additionalSubjectExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 7);
                    Assert.AreEqual(additionalSubjectExl, additionalSubject);
                    extentReports.CreateLog("Additional Subject: " + additionalSubject + " in add opportunity page matches on Opportunity details page ");

                    // Validate referal type 
                    string referalType = oppDetails.GetReferalTypeValue();
                    string referalTypeExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 8);
                    Assert.AreEqual(referalTypeExl, referalType);
                    extentReports.CreateLog("Referal Type: " + referalType + " in add opportunity page matches on Opportunity details page ");

                    // Validate non public info value
                    string nonPublicInfo = oppDetails.GetNonPublicInfoValue();
                    string nonPublicInfoExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 9);
                    Assert.AreEqual(nonPublicInfoExl, nonPublicInfo);
                    extentReports.CreateLog("Non Public: " + nonPublicInfo + " in add opportunity page matches on Opportunity details page ");

                    // Validate beneficiary owner info value
                    string beneOwnerInfo = oppDetails.GetBeneOwnerAndControlPersonFormValue();
                    string beneOwnerInfoExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
                    Assert.AreEqual(beneOwnerInfoExl, beneOwnerInfo);
                    extentReports.CreateLog("Beneficial owner and control person form: " + beneOwnerInfo + " in add opportunity page matches on Opportunity details page ");

                    //Validate primary office
                    string primaryOffice = oppDetails.GetPrimaryOffice();
                    string primaryOfficeExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 11);
                    Assert.AreEqual(primaryOfficeExl, primaryOffice);
                    extentReports.CreateLog("Primary office: " + primaryOffice + " as opportunity primary office matches on Opportunity details page ");

                    //Validate legal Entity 
                    string legalEntity = oppDetails.GetLegalEntity();
                    string legalEntityExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 12);
                    Assert.AreEqual(legalEntityExl, legalEntity);
                    extentReports.CreateLog("Legal Entity: " + legalEntity + " as opportunity legal entity matches on Opportunity details page ");
                   
                    //Validate staffMember 
                    string staffMember = oppDetails.GetStaffMember();
                    string staffMemberExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
                    Assert.AreEqual(staffMemberExl, staffMember);
                    extentReports.CreateLog("Staff member " + staffMember + " as Opportunity staff member matches on Opportunity details page ");

                    //LogOut from SF standard user
                    usersLogin.UserLogOut();

                    // Search opportunity with opportunity name
                    oppHomePage.SearchOpportunity(value);
                    string opportunityNames = oppDetails.GetOpportunityName();
                    Assert.AreEqual(value, opportunityNames);
                    extentReports.CreateLog("Get opportunity details by using opportunity name ");

                    //Delete created opportunity including internal team member                    
                    oppDetails.DeleteInternalTeamOfOpportunity();
                    Assert.AreEqual("Salesforce - Unlimited Edition", driver.Title);
                    extentReports.CreateLog("Delete opportunity including staff member ");

                    // Search opportunity with opportunity number again
                    oppHomePage.SearchOpportunity(value);
                    Assert.AreEqual(value, opportunityNames);
                    extentReports.CreateLog("Get opportunity details by using opportunity name ");

                    //Delete opportunity completely
                    oppDetails.DeleteOpportunity();
                    Assert.AreEqual("Salesforce - Unlimited Edition", driver.Title);
                    extentReports.CreateLog("Delete opportunity completely ");
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