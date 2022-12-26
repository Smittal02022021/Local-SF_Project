using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.Contact;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class Clonebutton_DisplayedOrNot_Update : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementHomePage engHome = new EngagementHomePage();
        ContactHomePage contactHome = new ContactHomePage();
        CompanyHomePage compHome = new CompanyHomePage();
        UsersLogin usersLogin = new UsersLogin();
        CompanyDetailsPage companyDetailsPage = new CompanyDetailsPage();
        RandomPages Pages = new RandomPages();

        public static string fileT = "CloneOperation1";

        [OneTimeSetUp]

        public void OneTimeSetUp()

        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }


        [Test]
        public void ValidateClonebutton()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function             
                login.LoginApplication();

                //Validate user logged in     
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Calling functions to validate Clone operation
                int rowUsers = ReadExcelData.GetRowCount(excelPath, "Users");
                Console.WriteLine("rowUsers " + rowUsers);

                for (int row = 2; row <= rowUsers; row++)
                {
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    if (valUser.Equals("Drew Koecher"))
                    {
                        //Login as Standard User and validate the user
                        usersLogin.SearchUserAndLogin(valUser);
                        string stdUser = login.ValidateUser();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                        //Validate Clone button is displayed or not on Expense Request Detail page
                        string titleExpense = Pages.ClickExpenseRequestTab();
                        Assert.AreEqual("New Expense Request", titleExpense);
                        extentReports.CreateLog("Page with title : " + titleExpense + " is displayed ");
                        string expClone = Pages.ClickExpenseNumberandValidateCloneButton();

                        if (valUser.Equals("Drew Koecher") || valUser.Equals("Emre Abale") || valUser.Equals("Ayati Arvind"))
                        {
                            Assert.AreEqual("Clone button is displayed", expClone);
                            extentReports.CreateLog(expClone + " on Expense Request Detail page for " + valUser + " ");
                            Pages.CloseAdditionalTab();
                        }
                        else
                        {
                            Assert.AreEqual("Clone button is displayed", expClone);
                            extentReports.CreateLog(expClone + " on Expense Request Detail page for " + valUser + " ");
                            Pages.CloseAdditionalTab();
                        }
                    }

                    else if (valUser.Equals("Emre Abale"))
                    {
                        //Login as Standard FAS User and validate the user
                        usersLogin.SearchUserAndLogin(valUser);
                        string stdUser = login.ValidateUser();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                        //Validate Clone button is displayed or not on Expense Request Detail page
                        string titleExpense = Pages.ClickExpenseRequestTab();
                        Assert.AreEqual("New Expense Request", titleExpense);
                        extentReports.CreateLog("Page with title : " + titleExpense + " is displayed ");
                        string expClone = Pages.ClickExpenseNumberandValidateCloneButton();

                        if (valUser.Equals("Drew Koecher") || valUser.Equals("Emre Abale") || valUser.Equals("Ayati Arvind"))
                        {
                            Assert.AreEqual("Clone button is displayed", expClone);
                            extentReports.CreateLog(expClone + " on Expense Request Detail page for " + valUser + " ");
                            Pages.CloseAdditionalTab();
                        }
                        else
                        {
                            Assert.AreEqual("Clone button is displayed", expClone);
                            extentReports.CreateLog(expClone + " on Expense Request Detail page for " + valUser + " ");
                            Pages.CloseAdditionalTab();
                        }
                    }

                    else
                    {
                        //Login as Standard FR User and validate the user
                        usersLogin.SearchUserAndLogin(valUser);
                        string stdUser = login.ValidateUser();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                    }

                    //Search for any existing Opportunity              
                    opportunityHome.SearchOpportunity(ReadExcelData.ReadData(excelPath, "Opportunity", 1));
                    extentReports.CreateLog("Opportunity found as per entered search criteria ");

                    //Validate Clone button is displayed or not on Opportunity details page
                    string oppClone = opportunityDetails.ValidateCloneButton();
                    if (valUser.Equals("Drew Koecher") || valUser.Equals("Emre Abale") || valUser.Equals("Ayati Arvind"))
                    {
                        Assert.AreEqual("Clone button is not displayed", oppClone);
                        extentReports.CreateLog(oppClone + " on Opportunity details page for " + valUser + " ");
                    }
                    else
                    {
                        Assert.AreEqual("Clone button is displayed", oppClone);
                        extentReports.CreateLog(oppClone + " on Opportunity details page for " + valUser + " ");
                    }

                    //Search for any existing Engagement             
                    engHome.SearchEngagementWithLOB(ReadExcelData.ReadData(excelPath, "Opportunity", 2), ReadExcelData.ReadData(excelPath, "Opportunity", 3));
                    extentReports.CreateLog("Engagement found as per entered search criteria ");

                    //Validate Clone button is displayed or not on Engagement details page
                    string engClone = opportunityDetails.ValidateCloneButton();
                    if (valUser.Equals("Drew Koecher") || valUser.Equals("Emre Abale") || valUser.Equals("Ayati Arvind"))
                    {
                        Assert.AreEqual("Clone button is not displayed", engClone);
                        extentReports.CreateLog(engClone + " on Engagement details page for  " + valUser + " ");
                    }
                    else
                    {
                        Assert.AreEqual("Clone button is displayed", engClone);
                        extentReports.CreateLog(engClone + " on Engagement details page for " + valUser + " ");
                    }

                    //Search for any existing Contact            
                    contactHome.SearchContactWithExternalContact(fileT);
                    extentReports.CreateLog("Contact found as per entered search criteria ");

                    //Validate Clone button is displayed or not on Contact details page
                    string conClone = opportunityDetails.ValidateCloneButton();
                    if (valUser.Equals("Drew Koecher") || valUser.Equals("Emre Abale") || valUser.Equals("Ayati Arvind"))
                    {
                        Assert.AreEqual("Clone button is displayed", conClone);
                        extentReports.CreateLog(conClone + " on Contact details page for " + valUser + " ");
                    }
                    else
                    {
                        Assert.AreEqual("Clone button is not displayed", conClone);
                        extentReports.CreateLog(conClone + " on Contact details page for " + valUser + " ");
                    }

                    //Search for any existing Company              
                    compHome.SearchCompany(fileT, "Operating Company");
                    extentReports.CreateLog("Company found as per entered search criteria ");

                    //Validate Clone button is displayed or not on Company details page
                    string compClone = opportunityDetails.ValidateCloneButton();
                    if (valUser.Equals("Drew Koecher") || valUser.Equals("Emre Abale") || valUser.Equals("Ayati Arvind"))
                    {
                        Assert.AreEqual("Clone button is not displayed", compClone);
                        extentReports.CreateLog(compClone + " on Company details page for " + valUser + " ");

                    }
                    else
                    {
                        Assert.AreEqual("Clone button is displayed", compClone);
                        extentReports.CreateLog(compClone + " on Company details page for " + valUser + " ");
                    }

                    //Validate Clone button is displayed or not on Coverage Team page
                    string titleCov = companyDetailsPage.ClickCoverageTeam();
                    Assert.AreEqual("Coverage Team", titleCov);
                    extentReports.CreateLog("Page with title : " + titleCov + " is displayed ");

                    string coverageClone = opportunityDetails.ValidateCloneButton();
                    if (valUser.Equals("Emre Abale1") || valUser.Equals("Ayati Arvind1"))
                    {
                        Assert.AreEqual("Clone button is not displayed", coverageClone);
                        extentReports.CreateLog(coverageClone + " on Coverage Team detail page for " + valUser + " ");
                    }
                    else
                    {
                        Assert.AreEqual("Clone button is displayed", coverageClone);
                        extentReports.CreateLog(coverageClone + " on Coverage Team detail page for " + valUser + " ");
                    }

                    //Validate Clone button is displayed or not on Company List Member Detail page
                    string titleCompList = companyDetailsPage.ClickCompanyList();
                    Assert.AreEqual("Company List Member Detail", titleCompList);
                    extentReports.CreateLog("Page with title : " + titleCompList + " is displayed ");

                    string compListClone = opportunityDetails.ValidateCloneButton();
                    if (valUser.Equals("Drew Koecher") || valUser.Equals("Emre Abale") || valUser.Equals("Ayati Arvind"))
                    {
                        Assert.AreEqual("Clone button is displayed", compListClone);
                        extentReports.CreateLog(compListClone + " on Company List Member Detail page for " + valUser + " ");
                    }
                    else
                    {
                        Assert.AreEqual("Clone button is displayed", compListClone);
                        extentReports.CreateLog(compListClone + " on Company List Member Detail page for " + valUser + " ");
                    }

                    //Validate Clone button is displayed or not on Campaigns Detail page
                    string titleCampaigns = companyDetailsPage.ClickCampaignsTab();
                    Assert.AreEqual("Campaign Detail", titleCampaigns);
                    extentReports.CreateLog("Page with title : " + titleCampaigns + " is displayed ");

                    string campClone = opportunityDetails.ValidateCloneButton();
                    if (valUser.Equals("Drew Koecher") || valUser.Equals("Emre Abale") || valUser.Equals("Ayati Arvind"))
                    {
                        Assert.AreEqual("Clone button is not displayed", campClone);
                        extentReports.CreateLog(campClone + " on Campaigns Detail page for " + valUser + " ");
                    }
                    else
                    {
                        Assert.AreEqual("Clone button is displayed", campClone);
                        extentReports.CreateLog(campClone + " on Campaigns Detail page for " + valUser + " ");
                    }

                    //Validate Clone button is displayed or not on D&B Company Record Detail page
                    string titleDBCompany = Pages.ClickDBCompanyRecords();
                    Assert.AreEqual("D&B Company Record Detail", titleDBCompany);
                    extentReports.CreateLog("Page with title : " + titleDBCompany + " is displayed ");

                    string dbCompClone = opportunityDetails.ValidateCloneButton();
                    if (valUser.Equals("Drew Koecher") || valUser.Equals("Emre Abale") || valUser.Equals("Ayati Arvind"))
                    {
                        Assert.AreEqual("Clone button is displayed", dbCompClone);
                        extentReports.CreateLog(dbCompClone + " on D&B Company Record Detail page for " + valUser + " ");
                    }
                    else
                    {
                        Assert.AreEqual("Clone button is displayed", dbCompClone);
                        extentReports.CreateLog(dbCompClone + " on D&B Company Record Detail page for " + valUser + " ");
                    }

                    //Validate Clone button is displayed or not on D&B Contacts Record Detail page
                    string titleDBContact = Pages.ClickDBContactRecords();
                    Assert.AreEqual("D&B Contact Record Detail", titleDBContact);
                    extentReports.CreateLog("Page with title : " + titleDBContact + " is displayed ");

                    string dbContactClone = opportunityDetails.ValidateCloneButton();
                    if (valUser.Equals("Drew Koecher") || valUser.Equals("Emre Abale") || valUser.Equals("Ayati Arvind"))
                    {
                        Assert.AreEqual("Clone button is displayed", dbContactClone);
                        extentReports.CreateLog(dbContactClone + " on D&B Contacts Record Detail page for " + valUser + " ");
                    }
                    else
                    {
                        Assert.AreEqual("Clone button is displayed", dbContactClone);
                        extentReports.CreateLog(dbContactClone + " on D&B Contacts Record Detail page for " + valUser + " ");
                    }
                    usersLogin.UserLogOut();
                }
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






