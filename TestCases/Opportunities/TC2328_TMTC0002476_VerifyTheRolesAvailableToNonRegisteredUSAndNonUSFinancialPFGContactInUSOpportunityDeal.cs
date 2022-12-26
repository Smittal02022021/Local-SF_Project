using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TC2328_TMTC0002476_VerifyTheRolesAvailableToNonRegisteredUSAndNonUSFinancialPFGContactInUSOpportunityDeal : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();

        public static string ERP = "TC2328_TMTC0002476_VerifyTheRolesAvailableToNonRegisteredUSAndNonUS1.xlsx";

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
                    string opp = ReadExcelData.ReadData(excelPath, "Users",3);
                    string contact = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);

                    //Search for the Opportunity and modify Internal team members
                    opportunityHome.SearchOpportunity(opp);
                    string chkInitiator = opportunityDetails.ModifyInternalTeamMembers(contact);

                    //------Validate all the roles checkbox
                    //Verify Initiator role
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkInitiator);
                        extentReports.CreateLog("Initiator role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkInitiator);
                        extentReports.CreateLog("Initiator role checkbox is not displayed ");
                    }

                    //Verify Seller role
                    string chkSeller = opportunityDetails.VerifySellerRole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkSeller);
                        extentReports.CreateLog("Seller role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkSeller);
                        extentReports.CreateLog("Seller role checkbox is not displayed ");
                    }

                    //Verify Principal role
                    string chkPrin = opportunityDetails.VerifyPrincipalRole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkPrin);
                        extentReports.CreateLog("Principal role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkPrin);
                        extentReports.CreateLog("Principal role checkbox is not displayed ");
                    }
                    //Verify Manager role
                    string chkMgr = opportunityDetails.VerifyManagerRole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkMgr);
                        extentReports.CreateLog("Manager role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkMgr);
                        extentReports.CreateLog("Manager role checkbox is not displayed ");
                    }

                    //Verify Associate role
                    string chkAssociate = opportunityDetails.VerifyAssociateRole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkAssociate);
                        extentReports.CreateLog("Associate role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkAssociate);
                        extentReports.CreateLog("Associate role checkbox is not displayed ");
                    }

                    //Verify Analyst role
                    string chkAnalyst = opportunityDetails.VerifyAnalystRole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkAnalyst);
                        extentReports.CreateLog("Analyst role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkAnalyst);
                        extentReports.CreateLog("Analyst role checkbox is not displayed ");
                    }

                    //Verify Specialty role
                    string chkSpecialty = opportunityDetails.VerifySpecialtyRole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkSpecialty);
                        extentReports.CreateLog("Specialty role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkSpecialty);
                        extentReports.CreateLog("Specialty role checkbox is not displayed ");
                    }


                    //Verify PE/HF role
                    string chkPE = opportunityDetails.VerifyPERole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkPE);
                        extentReports.CreateLog("PE role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkPE);
                        extentReports.CreateLog("PE role checkbox is not displayed ");
                    }

                    //Verify Public role
                    string chkPublic = opportunityDetails.VerifyPublicRole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkPublic);
                        extentReports.CreateLog("Public role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkPublic);
                        extentReports.CreateLog("Public role checkbox is not displayed ");
                    }

                    //Verify Admin role
                    string chkAdmin = opportunityDetails.VerifyAdminRole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkAdmin);
                        extentReports.CreateLog("Admin role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkAdmin);
                        extentReports.CreateLog("Admin role checkbox is not displayed ");
                    }
                                       
                    //Verify RMS role
                    string chkRMS = opportunityDetails.VerifyRMSRole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkRMS);
                        extentReports.CreateLog("RMS role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkRMS);
                        extentReports.CreateLog("RMS role checkbox is not displayed ");
                    }

                    //Verify Expense role
                    string chkExpense = opportunityDetails.VerifyExpenseOnlyRole();
                    if (contact.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkExpense);
                        extentReports.CreateLog("Expense role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkExpense);
                        extentReports.CreateLog("Expense role checkbox is not displayed ");
                    }

                    //Verify Non Registered role
                    string chkNonReg = opportunityDetails.VerifyNonRegisteredRole();
                    if (contact.Equals("Alexander Odysseos") || contact.Equals("Faisal Roukbi"))
                    {
                        Assert.AreEqual("True", chkNonReg);
                        extentReports.CreateLog("Non Registered role checkbox is displayed ");
                        if(contact.Equals("Alexander Odysseos"))
                            extentReports.CreateLog("All role checkboxes are displayed for non registered Non US FIN PFG contact in US Opportunity ");
                        else
                            extentReports.CreateLog("Only Non Registered role checkbox is displayed for non registered US FIN PFG contact in US Opportunity ");

                    }
                    else 
                    {
                        Assert.AreEqual("False", chkNonReg);
                        extentReports.CreateLog("Non Registered role checkbox is not displayed ");
                        extentReports.CreateLog("All role checkboxes are displayed for registered Non US PFG contact in Non - US Opportunity ");
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

    

