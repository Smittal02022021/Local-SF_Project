using NUnit.Framework;
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
    class T1885_T1886_Companies_CompanyDetailsPage_CompanyFinancials_EditAndDelete : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        AddNewCompanyFinancial companyFinancial = new AddNewCompanyFinancial();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC1886 = "T1886_Companies_CompanyDetailsPage_CompanyFinancials_EditAndDelete.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CompanyDetailsPage_CompanyFinancials_EditAndDelete()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1886;
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

                int rowCompanySheet = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanySheet; row++)
                {
                    // Search standard user by global search
                    string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                    homePage.SearchUserByGlobalSearch(fileTC1886, user);
                  
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
                    companyHome.SearchCompany(fileTC1886, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1));
                    //
                    
                    int rowCompanyFinancialSheet = ReadExcelData.GetRowCount(excelPath, "CompanyFinancial");
                    
                    for (int rows = 2; rows <= rowCompanyFinancialSheet; rows++)
                    {
                        //Click on new company financial button
                        companyDetail.ClickNewCompanyFinancial(fileTC1886, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1));
                        string companyFinancialHeading = companyFinancial.GetNewCompanyFinancialHeading();
                        string companyFinancialHeadingExl = ReadExcelData.ReadData(excelPath, "CompanyFinancial", 2);
                        Assert.AreEqual(companyFinancialHeadingExl, companyFinancialHeading);
                        extentReports.CreateLog("Page with heading: " + companyFinancialHeading + " is displayed upon click of new company financial button ");
                        
                        //Validate prefilled company in company financial
                        string preFilledCompanyFinancial = companyFinancial.GetPreFilledSelectedCompanyName();
                        string SelectedCompanyFinancialExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                        Assert.AreEqual(SelectedCompanyFinancialExl, preFilledCompanyFinancial);
                        extentReports.CreateLog("Prefilled Selected Company: " + preFilledCompanyFinancial + " is displayed upon click of new company financial button ");

                        //Creaate new company financial
                        string asOfDate = companyFinancial.CreateNewCompanyFinancial(fileTC1886, rows);
                        string companyDetailHeadingAfterCompanyFinancial = companyDetail.GetCompanyDetailsHeading();
                        string companyDetailHeading = companyDetail.GetCompanyDetailsHeading();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 3), companyDetailHeading);
                        extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon creating new company financial ");
                        
                        //Validate company financial year
                        string companyFinancialYear = companyDetail.GetCompanyFinancialYear(rows);
                        string companyFinancialYearExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyFinancial", rows, 1);
                        Assert.AreEqual(companyFinancialYearExl, companyFinancialYear);
                        extentReports.CreateLog("Company Financial Year: " + companyFinancialYear + " is displayed in company financial section of company detail page upon creating new company financial ");
                        
                        //Validate date of company financial
                        string companyFinancialAsOfDate = companyDetail.GetAsOfYearFinancialYear(rows);
                        //Assert.AreEqual(asOfDate, companyFinancialAsOfDate);
                        extentReports.CreateLog("As Of Date: " + companyFinancialAsOfDate + " is displayed in company financial section of company detail page upon creating new company financial ");
                        
                    }

                    //Get most recent year from company financial
                    string mostRecentYearCompanyLevel = companyDetail.GetCompanyFinancialYear(2);

                    //Click on new company financial 
                    companyDetail.ClickNewCompanyFinancial(fileTC1886, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1));
                    string mostRecentYearFromNewFinancialCreateDrpDwn = companyFinancial.GetMostRecentYear(2);
                    //Assert.AreEqual(mostRecentYearFromNewFinancialCreateDrpDwn, mostRecentYearCompanyLevel);
                    extentReports.CreateLog("Most Recent year: " + mostRecentYearCompanyLevel + " is displayed to the company level ");

                    //Get third listed year from new financial
                    string thirdListedYearFromNewFinancialCreateDrpDwn  = companyFinancial.GetMostRecentYear(4);

                    companyFinancial.ClickCancelButton();

                    //Get value of Financial year from annual financial
                    string financialYearAnnualFinancial = companyDetail.GetFinancialsYearAnnualFinancial();
                    Assert.AreEqual(mostRecentYearCompanyLevel, financialYearAnnualFinancial);
                    extentReports.CreateLog("Most Recent year: " + financialYearAnnualFinancial + " is displayed to the annual financial and matches with company level ");

                    usersLogin.UserLogOut();
                    extentReports.CreateLog("LogOut from StandardUser ");

                    // Calling Search Company function
                    companyHome.SearchCompany(fileTC1886, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1));

                    //Validate recent year after changing the latest year to oldest year 
                    string secondMostRecentYear = companyFinancial.EditCompanyFinancialFirstRecord(fileTC1886, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1));
                    string topCompanyFinancialYearAfterEdit = companyDetail.GetCompanyFinancialYear(2);
                    Assert.AreEqual(topCompanyFinancialYearAfterEdit, secondMostRecentYear);
                    extentReports.CreateLog("Most Recent year: " + topCompanyFinancialYearAfterEdit + " is displayed at company level after changing the latest year to oldest year ");

                    //Validate annual financial after changing the latest year to oldest year
                    string updatedfinancialYearAnnualFinancial = companyDetail.GetFinancialsYearAnnualFinancial();
                    Assert.AreEqual(secondMostRecentYear, updatedfinancialYearAnnualFinancial);
                    extentReports.CreateLog("Most Recent year: " + topCompanyFinancialYearAfterEdit + " is displayed at annual financial after changing the latest year to oldest year ");

                    //Delete most recent year
                    companyDetail.DeleteCompanyFinancialRecord(fileTC1886, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1));
                    string topCompanyFinancialYearAfterDelete = companyDetail.GetCompanyFinancialYear(2);
                    Assert.AreEqual(topCompanyFinancialYearAfterDelete, thirdListedYearFromNewFinancialCreateDrpDwn);
                    extentReports.CreateLog("Next top Recent year: " + topCompanyFinancialYearAfterDelete + " is displayed at company level after deleting most recent year ");

                    //Validate second most recent year in annual financial
                    string secondUpdatefinancialYearAnnualFinancial = companyDetail.GetFinancialsYearAnnualFinancial();
                    Assert.AreEqual(topCompanyFinancialYearAfterDelete, secondUpdatefinancialYearAnnualFinancial);
                    extentReports.CreateLog("Next top Recent year: " + secondUpdatefinancialYearAnnualFinancial + " is displayed at annual financial after deleting most recent year ");

                    //Delete all records of company finanials created
                    int sizeOfCompanyFinancialList = companyDetail.GetSizeOfCompanyFinancialList();
                    for (int i = 2; i <= sizeOfCompanyFinancialList; i++)
                    {
                        companyDetail.DeleteCompanyFinancialRecord(fileTC1886, ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1));
                        extentReports.CreateLog("Company Financial record is deleted successfully ");
                    }

                    //Validate no record is found after deleting all company financial
                    string noRecordMsg = companyDetail.GetNoRecordTextCompanyFinancial();
                    string noRecordMsgExl = ReadExcelData.ReadData(excelPath, "Company", 4);
                    Assert.AreEqual(noRecordMsgExl, noRecordMsg);
                    extentReports.CreateLog("Message: "+ noRecordMsg+" is displayed after deleting all Company financial records ");

                    //Validate no record is found after deleting all company financial in annual financial 
                    string ValueAfterCompanyFinancialRecordDeletion = companyDetail.GetFinancialsYearAnnualFinancial();
                    Assert.AreEqual("", ValueAfterCompanyFinancialRecordDeletion);
                    extentReports.CreateLog("Annual Financial is displayed as blank after deleting all Company financial records ");

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