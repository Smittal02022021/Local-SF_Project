using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TC2329_TMTC0002479_VerifyTheRolesAvailableToNonRegisteredUSAndNonUSFinancialPFGContactInForeignOpportunityDeal : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();

        public static string ERP = "T2329_TMTC0002479_VerifyTheRoles1.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheRolesAvailable()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERP;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                 Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                 extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                              
                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                int users = ReadExcelData.GetRowCount(excelPath, "Users");
                Console.WriteLine("rowCount " + users);

                for (int row = 2; row <= users; row++)

                {
                    string opp = ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 3);
                    string name = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);

                    //Search for the Opportunity and modify Internal team members
                    opportunityHome.SearchOpportunity(opp);
                    string chkInitiator = opportunityDetails.ModifyInternalTeamMembers(name);

                    //------Validate all the roles checkbox
                    //Verify Initiator role
                    Assert.AreEqual("True", chkInitiator);
                    extentReports.CreateLog("Initiator role checkbox is displayed ");

                    //Verify Seller role
                    string chkSeller = opportunityDetails.VerifySellerRole();
                    Assert.AreEqual("True", chkSeller);
                    extentReports.CreateLog("Seller role checkbox is displayed ");

                    //Verify Principal role
                    string chkPrin = opportunityDetails.VerifyPrincipalRole();
                    Assert.AreEqual("True", chkPrin);
                    extentReports.CreateLog("Principal role checkbox is displayed ");

                    //Verify Manager role
                    string chkMgr = opportunityDetails.VerifyManagerRole();
                    Assert.AreEqual("True", chkMgr);
                    extentReports.CreateLog("Manager role checkbox is displayed ");

                    //Verify Associate role
                    string chkAssociate = opportunityDetails.VerifyAssociateRole();
                    Assert.AreEqual("True", chkAssociate);
                    extentReports.CreateLog("Associate role checkbox is displayed ");

                    //Verify Analyst role
                    string chkAnalyst = opportunityDetails.VerifyAnalystRole();
                    Assert.AreEqual("True", chkAnalyst);
                    extentReports.CreateLog("Analyst role checkbox is displayed ");

                    //Verify Specialty role
                    string chkSpecialty = opportunityDetails.VerifySpecialtyRole();
                    Assert.AreEqual("True", chkSpecialty);
                    extentReports.CreateLog("Specialty role checkbox is displayed ");

                    //Verify PE/HF role
                    string chkPE = opportunityDetails.VerifyPERole();
                    Assert.AreEqual("True", chkPE);
                    extentReports.CreateLog("PE role checkbox is displayed ");

                    //Verify Public role
                    string chkPublic = opportunityDetails.VerifyPublicRole();
                    Assert.AreEqual("True", chkPublic);
                    extentReports.CreateLog("Public role checkbox is displayed ");

                    //Verify Admin role
                    string chkAdmin = opportunityDetails.VerifyAdminRole();
                    Assert.AreEqual("True", chkAdmin);
                    extentReports.CreateLog("Admin role checkbox is displayed ");

                    //Verify RMS role
                    string chkRMS = opportunityDetails.VerifyRMSRole();
                    Assert.AreEqual("True", chkRMS);
                    extentReports.CreateLog("RMS role checkbox is displayed ");

                    //Verify Expense role
                    string chkExpense = opportunityDetails.VerifyExpenseOnlyRole();
                    Assert.AreEqual("True", chkExpense);
                    extentReports.CreateLog("Expense role checkbox is displayed ");

                    //Verify Non Registered role
                    string chkNonReg = opportunityDetails.VerifyNonRegisteredRole();
                    Assert.AreEqual("True", chkNonReg);
                    extentReports.CreateLog("Non Registered role checkbox is displayed ");
                    extentReports.CreateLog("All role checkboxes are displayed ");
                                        
                }
                usersLogin.UserLogOut();
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

    

