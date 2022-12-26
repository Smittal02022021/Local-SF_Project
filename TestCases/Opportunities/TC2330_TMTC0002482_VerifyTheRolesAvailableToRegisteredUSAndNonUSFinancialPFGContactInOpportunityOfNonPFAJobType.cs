using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T2330_TMTC0002482_VerifyTheRolesAvailableToRegisteredUSAndNonUSFinancialPFGContactInOpportunityOfNonPFAJobType : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();

        public static string ERP = "T2330_TMTC0002482_VerifyTheRolesAvailable1.xlsx";

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
                    Assert.AreEqual("False", chkPrin);
                    extentReports.CreateLog("Principal role checkbox is not displayed ");

                    //Verify Manager role
                    string chkMgr = opportunityDetails.VerifyManagerRole();
                    Assert.AreEqual("False", chkMgr);
                    extentReports.CreateLog("Manager role checkbox is not displayed ");

                    //Verify Associate role
                    string chkAssociate = opportunityDetails.VerifyAssociateRole();
                    Assert.AreEqual("False", chkAssociate);
                    extentReports.CreateLog("Associate role checkbox is not displayed ");

                    //Verify Analyst role
                    string chkAnalyst = opportunityDetails.VerifyAnalystRole();
                    Assert.AreEqual("False", chkAnalyst);
                    extentReports.CreateLog("Analyst role checkbox is not displayed ");

                    //Verify Specialty role
                    string chkSpecialty = opportunityDetails.VerifySpecialtyRole();
                    Assert.AreEqual("True", chkSpecialty);
                    extentReports.CreateLog("Specialty role checkbox is displayed ");

                    //Verify PE/HF role
                    string chkPE = opportunityDetails.VerifyPERole();
                    Assert.AreEqual("False", chkPE);
                    extentReports.CreateLog("PE role checkbox is not displayed ");

                    //Verify Public role
                    string chkPublic = opportunityDetails.VerifyPublicRole();
                    Assert.AreEqual("False", chkPublic);
                    extentReports.CreateLog("Public role checkbox is not displayed ");

                    //Verify Admin role
                    string chkAdmin = opportunityDetails.VerifyAdminRole();
                    Assert.AreEqual("False", chkAdmin);
                    extentReports.CreateLog("Admin role checkbox is not displayed ");

                    //Verify RMS role
                    string chkRMS = opportunityDetails.VerifyRMSRole();
                    Assert.AreEqual("False", chkRMS);
                    extentReports.CreateLog("RMS role checkbox is not displayed ");

                    //Verify Expense role
                    string chkExpense = opportunityDetails.VerifyExpenseOnlyRole();
                    Assert.AreEqual("False", chkExpense);
                    extentReports.CreateLog("Expense role checkbox is not displayed ");

                    //Verify Non Registered role
                    string chkNonReg = opportunityDetails.VerifyNonRegisteredRole();
                    Assert.AreEqual("False", chkNonReg);
                    extentReports.CreateLog("Non Registered role checkbox is not displayed ");

                    if (opp.Equals("Project Neon"))
                    {
                        if (name.Equals("Jeffrey Michelson"))
                        {
                            extentReports.CreateLog("Only Initiator, Seller and Specialty role's checkboxes are displayed for US Opportunity with Non PFA Job Type for Registered US FIN PFG contact ");
                        }
                        else
                        {
                            extentReports.CreateLog("Only Initiator, Seller and Specialty role's checkboxes are displayed for US Opportunity with Non PFA Job Type for Registered Non US FIN PFG contact ");
                        }
                    }
                    else
                    {
                        if (name.Equals("Jeffrey Michelson"))
                        {
                            extentReports.CreateLog("Only Initiator, Seller and Specialty role's checkboxes are displayed for Foreign Opportunity with Non PFA Job Type for Registered US FIN PFG contact ");
                        }
                        else
                        {
                            extentReports.CreateLog("Only Initiator, Seller and Specialty role's checkboxes are displayed for Foreign Opportunity with Non PFA Job Type for Registered Non US FIN PFG contact ");
                        }
                    }
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

    

