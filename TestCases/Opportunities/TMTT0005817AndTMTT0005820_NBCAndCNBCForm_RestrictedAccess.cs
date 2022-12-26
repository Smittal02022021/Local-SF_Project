using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TMTT0005817AndTMTT0005820_NBCAndCNBCForm_RestrictedAccess  : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        NBCForm form = new NBCForm();
        CNBCForm cnbc = new CNBCForm();

        public static string fileTC5815 = "TMTT0005817_NBCAndCNBCForm_RestrictedAccess1.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void RestrictedAccess()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC5815;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                
                //Validate functionality for various users
                int rowJobType = ReadExcelData.GetRowCount(excelPath, "Users");
                Console.WriteLine("rowCount " + rowJobType);

                for (int row = 2; row <= rowJobType; row++)
                {
                    //---Login as Internal Team members of NBC Form and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string valOpp = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    //Search Opportunity with NBC Form and navigate to NBC Form                    
                    opportunityHome.SearchOpportunity(valOpp);
                    string valNBC = opportunityDetails.ValidateNBCButton();
                    if (valUser.Equals("Emre Abale"))
                    {
                        Assert.AreEqual("NBC Form button is not displayed", valNBC);
                        extentReports.CreateLog(valNBC + " for non deal team CF user: " +valUser +" " );
                    }
                    
                    else if (valUser.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("NBC Form button is not displayed", valNBC);
                        extentReports.CreateLog(valNBC + " for delegates of non deal team CF user: " + valUser +" ");
                    }
                    else
                    {
                        Assert.AreEqual("NBC Form button is not displayed", valNBC);
                        extentReports.CreateLog(valNBC + " for non CF CAO: " + valUser+ " ");
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

    

