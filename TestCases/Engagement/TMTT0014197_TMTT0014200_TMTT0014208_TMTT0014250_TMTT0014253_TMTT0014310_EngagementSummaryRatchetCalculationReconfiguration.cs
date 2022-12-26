using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Engagement
{
    class TMTT0014197_TMTT0014200_TMTT0014208_TMTT0014250_TMTT0014253_TMTT0014310_EngagementSummaryRatchetCalculationReconfiguration : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        CFEngagementSummaryPage cFEngagementSummary = new CFEngagementSummaryPage();

        public static string fileTC18552 = "TMTT0014197_EngagementSummaryRatchetCalculationReconfiguration";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void EngagementSummaryRatchetCalculationReconfiguration()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC18552;
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
                
                //Search user by global search
                string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearch(fileTC18552, user);
                string userPeople = homePage.GetPeopleOrUserName();
                Assert.AreEqual(user, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login user
                usersLogin.LoginAsSelectedUser();
                string userName = login.ValidateUser();
                Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1).Contains(userName), true);
                extentReports.CreateLog("Standard User: " + userName + " is able to login ");
                
                int rowCount = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int oppRow = 2; oppRow <= rowCount; oppRow++)
                {
                    //Call function to open Add Opportunity Page & Select LOB
                    opportunityHome.ClickOpportunity();
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", oppRow, 25);
                    Console.WriteLine("valRecordType:" + valRecordType);
                    opportunityHome.SelectLOBAndClickContinue(valRecordType);

                    //Validating Title of New Opportunity Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Calling AddOpportunities function                
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", oppRow, 3);
                    string value = addOpportunity.AddOpportunities(valJobType, fileTC18552, oppRow);
                    extentReports.CreateLog("Required fields added in order to create an opportunity. ");

                    //Call function to enter Internal Team details
                    clientSubjectsPage.EnterStaffDetailsMultipleRows(fileTC18552, 2);
                    extentReports.CreateLog("HL member added to the deal team. ");

                    //Fetch values of Opportunity Number, Name
                    string oppNum = opportunityDetails.GetOppNumber();
                    string oppName = opportunityDetails.GetOpportunityName();

                    //Validating Opportunity created successfully
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with LOB : " + valRecordType + " is created. Opportunity number is : " + oppNum + ". ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CreateContact(fileTC18552, valContact, valRecordType, valContactType);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    if (valRecordType == "CF")
                    {
                        opportunityDetails.UpdateReqFieldsForCFConversionMultipleRows(fileTC18552, oppRow);
                        extentReports.CreateLog("Fields required for converting CF opportunity to engagement are updated. ");
                    }

                    //Update internal team details
                    opportunityDetails.UpdateInternalTeamDetails(fileTC18552);
                    extentReports.CreateLog("Deal team member roles are updated. ");

                    //Log out from standard User
                    usersLogin.UserLogOut();

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(oppName);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTC18552);
                    if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateNBCApproval();
                        extentReports.CreateLog("Conflict Check and NBC fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateLog("Conflict Check fields are updated ");
                    }

                    //Update Client and Subject to Accupac bypass EBITDA field validation for JobType- Sellside
                    if (valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateClientandSubject("Accupac");
                        extentReports.CreateLog("Updated Client and Subject fields ");
                    }
                    else
                    {
                        Console.WriteLine("Not required to update ");
                    }

                    //Login again as Standard User
                    homePage.SearchUserByGlobalSearch(fileTC18552, user);
                    usersLogin.LoginAsSelectedUser();
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(user), true);
                    extentReports.CreateLog("Standard User: " + stdUser1 + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Requesting for engagement and validate the success message
                    string msgSuccess = opportunityDetails.ClickRequestEng();
                    Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                    extentReports.CreateLog("Request for Engagement success message: " + msgSuccess + " is displayed. ");

                    //Log out of Standard User
                    usersLogin.UserLogOut();

                    //Login as CAO user to approve the Opportunity
                    homePage.SearchUserByGlobalSearch(fileTC18552, ReadExcelData.ReadDataMultipleRows(excelPath, "DealTeamMembers", 2, 2));
                    usersLogin.LoginAsSelectedUser();
                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "DealTeamMembers", 2, 2)), true);
                    extentReports.CreateLog("CAO User: " + caoUser + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Approve the Opportunity 
                    opportunityDetails.ClickApproveButton();
                    extentReports.CreateLog("Opportunity number: " + oppNum + " is approved. ");

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEng();
                    extentReports.CreateLog("Opportunity number: " + oppNum + " is successfuly converted into Engagement. ");

                    usersLogin.UserLogOut();

                    //Search user by global search
                    homePage.SearchUserByGlobalSearch(fileTC18552, user);
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1).Contains(userName), true);

                    //Search for created engagement
                    engagementHome.SearchEngagementWithName(oppName);

                    //Navigate to CF Engagement Summary Page
                    string title = engagementDetails.NavigateToCFEngagementSummaryPage();
                    Assert.AreEqual(oppName, title);
                    extentReports.CreateLog("Engagement summary page is displayed for engagement : " + title + " ");
                    
                    int userCount = ReadExcelData.GetRowCount(excelPath, "Users");
                    for (int row = 2; row <= 2; row++)
                    {
                        if (row==3)
                        {
                            
                            //Search user by global search
                            string user1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                            homePage.SearchUserByGlobalSearch(fileTC18552, user1);
                            string userPeople1 = homePage.GetPeopleOrUserName();
                            Assert.AreEqual(user1, userPeople1);
                            extentReports.CreateLog("User " + userPeople1 + " details are displayed ");

                            //Login user
                            usersLogin.LoginAsSelectedUser();
                            string role = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                            extentReports.CreateLog(role + " User: " + userPeople1 + " is able to login. ");

                            //Navigate to CF Engagement Summary Page
                            cFEngagementSummary.NavigateToCFEngSummaryPage(oppName);
                            Assert.AreEqual(oppName, title);
                            extentReports.CreateLog("Engagement summary page is displayed for engagement : " + title + " ");
                            
                        }

                        //Click on Engagement Dynamic Section
                        cFEngagementSummary.ClickEngagementDynamicsSection();
                        
                        //Verify sections, fields
                        Assert.IsTrue(cFEngagementSummary.VerifySectionsExistsUnderEngagementDynamicsSection());
                        extentReports.CreateLog("All sections are displayed correctly under Engagement Dynamic Sections. ");

                        //Verify Fee Subject To Contingent Fees Help Text
                        string helpText = cFEngagementSummary.GetFeeSubjectToContingentFeesHelpText();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "HelpText", 1), helpText);
                        extentReports.CreateLog("Help text displayed for Fee Subject To Contingent Fees is: " + helpText + " ");

                        //Verify Total Credits Help Text
                        string helpText1 = cFEngagementSummary.GetTotalCreditsHelpText();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "HelpText", 2), helpText1);
                        extentReports.CreateLog("Help text displayed for Total Credits is: " + helpText1 + " ");

                        //Verify Totals (Actual Amount) Help Text
                        string helpText2 = cFEngagementSummary.GetTotalsHelpText();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "HelpText", 4), helpText2);
                        extentReports.CreateLog("Help text displayed for Totals (Actual Amount) is: " + helpText2 + " ");

                        //Verify Incentive Structure (Actual Amount) Help Text
                        string helpText3 = cFEngagementSummary.GetIncentiveStructureHelpText();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "HelpText", 5), helpText3);
                        extentReports.CreateLog("Help text displayed for Incentive Structure (Actual Amount) is: " + helpText3 + " ");

                        //Verify Transaction Value for Fee Calc (Actual) Help Text
                        string helpText4 = cFEngagementSummary.GetTransactionValueForFeeCalcHelpText();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "HelpText", 3), helpText4);
                        extentReports.CreateLog("Help text displayed for Transaction Value for Fee Calc (Actual) is: " + helpText4 + " ");

                        //Verify Transaction Fee Help Text
                        string helpText5 = cFEngagementSummary.GetTransactionFeeHelpText();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "HelpText", 6), helpText5);
                        extentReports.CreateLog("Help text displayed for Transaction Fee is: " + helpText5 + " ");

                        //Verify Fees (Actual Amount) Help Text
                        string helpText6 = cFEngagementSummary.GetFeesHelpText();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "HelpText", 7), helpText6);
                        extentReports.CreateLog("Help text displayed for Fees (Actual Amount) is: " + helpText6 + " ");

                        //Enter Fees under Fee (Actual Amountz) section
                        cFEngagementSummary.EnterFeesUnderFeeSection(fileTC18552);
                        extentReports.CreateLog("All fees under Fee (Actual Amount) section are entered successfully. ");

                        //Verify the Calculation of Total Fee For Fee (Actual Amount) Section When Crediable Are Unchecked
                        Assert.IsTrue(cFEngagementSummary.VerifyCalculationForFeeSectionWhenCrediableAreUnchecked());
                        extentReports.CreateLog("Calculation of Total Fee For Fee (Actual Amount) Section When Crediable Are Unchecked is correct. ");

                        //Verify the Calculation of Total Fee For Fee (Actual Amount) Section When Crediable Are Checked
                        Assert.IsTrue(cFEngagementSummary.VerifyCalculationForFeeSectionWhenCrediableAreChecked());
                        extentReports.CreateLog("Calculation of Total Fee For Fee (Actual Amount) Section When Crediable Are Checked is correct. ");
                        
                        //Verify the Calculation of Total Fee For Transaction Fee Calc (Actual Amount) Section When Incentive Structure Section is Empty
                        Assert.IsTrue(cFEngagementSummary.VerifyCalculationForTransactionFeeCalcWhenIncentiveStructureIsEmpty(fileTC18552));
                        extentReports.CreateLog("The Transaction Fee Calc (Actual Amount) total is NOT added to the Total section as the Incentive Structure Section is Empty. ");
                        
                        //Verify Calculation For Transaction (Actual Amount) Section With Different Fee Types
                        int feeTypeCount = ReadExcelData.GetRowCount(excelPath, "Transaction(Actual Amount)");
                        double totalPaymentClosingFee = cFEngagementSummary.GetactualPaymentClosingFeeBeforeEditing();
                        for (int feeTypeRows = 2; feeTypeRows <= feeTypeCount; feeTypeRows++)
                        {
                            string feeType = ReadExcelData.ReadDataMultipleRows(excelPath, "Transaction(Actual Amount)", feeTypeRows, 1);
                            Assert.IsTrue(cFEngagementSummary.VerifyCalculationForTransactionSectionWithDifferentFeeTypes(fileTC18552, feeType, feeTypeRows, totalPaymentClosingFee));
                            extentReports.CreateLog("Calculation of Total Fee For Transaction (Actual Amount) Section is correct when Fee Type: " + feeType + " ");
                        }
                        
                        //Verification of Validation rules for First Ratchet Amount
                        Assert.IsTrue(cFEngagementSummary.VerifyValidationRulesForBlankValuesOfFirstRatchetAmount(fileTC18552));
                        extentReports.CreateLog("Validation rules for the blank values of First Ratchet Amount are displayed correctly. ");

                        //Verification of Validation rules for Second Ratchet Amount
                        Assert.IsTrue(cFEngagementSummary.VerifyValidationRulesForBlankValuesOfSecondRatchetAmount(fileTC18552));
                        extentReports.CreateLog("Validation rules for the blank values of Second Ratchet Amount are displayed correctly. ");

                        //Verification of Validation rules for Third Ratchet Amount
                        Assert.IsTrue(cFEngagementSummary.VerifyValidationRulesForBlankValuesOfThirdRatchetAmount(fileTC18552));
                        extentReports.CreateLog("Validation rules for the blank values of Third Ratchet Amount are displayed correctly. ");

                        //Verification of Validation rules for Fourth Ratchet Amount
                        Assert.IsTrue(cFEngagementSummary.VerifyValidationRulesForBlankValuesOfFourthRatchetAmount(fileTC18552));
                        extentReports.CreateLog("Validation rules for the blank values of Fourth Ratchet Amount are displayed correctly. ");

                        //Verification of Validation rules When First Ratchet From Amount > First Ratchet To Amount
                        Assert.IsTrue(cFEngagementSummary.VerifyValidationRulesWhenFirstRatchetFromAmountIsGreaterThanFirstRatchetToAmount(fileTC18552));
                        extentReports.CreateLog("Validation rules when First Ratchet From Amount > First Ratchet To Amount are displayed correctly. ");

                        //Verification of Validation rules When Second Ratchet From Amount > Second Ratchet To Amount
                        Assert.IsTrue(cFEngagementSummary.VerifyValidationRulesWhenSecondRatchetFromAmountIsGreaterThanSecondRatchetToAmount(fileTC18552));
                        extentReports.CreateLog("Validation rules when Second Ratchet From Amount > Second Ratchet To Amount are displayed correctly. ");

                        //Verification of Validation rules When Third Ratchet From Amount > Third Ratchet To Amount
                        Assert.IsTrue(cFEngagementSummary.VerifyValidationRulesWhenThirdRatchetFromAmountIsGreaterThanThirdRatchetToAmount(fileTC18552));
                        extentReports.CreateLog("Validation rules when Third Ratchet From Amount > Third Ratchet To Amount are displayed correctly. ");

                        //Verification of Validation rules When Fourth Ratchet From Amount > Fourth Ratchet To Amount
                        Assert.IsTrue(cFEngagementSummary.VerifyValidationRulesWhenFourthRatchetFromAmountIsGreaterThanFourthRatchetToAmount(fileTC18552));
                        extentReports.CreateLog("Validation rules when Fourth Ratchet From Amount > Fourth Ratchet To Amount are displayed correctly. ");

                        int incentiveStrucCount = ReadExcelData.GetRowCount(excelPath, "IncentiveStructure");
                        
                        //Verify Calculation For Incentive Structure (Actual Amount) Section for First Ratchet Fee
                        double totalFee = cFEngagementSummary.GetactualTotalFeeBeforeEditing();
                        totalPaymentClosingFee = cFEngagementSummary.GetactualPaymentClosingFeeBeforeEditing();
                        for (int incStrucRows = 2; incStrucRows <= incentiveStrucCount; incStrucRows++)
                        {
                            Assert.IsTrue(cFEngagementSummary.VerifyCalculationForFirstRatchetIncentiveStructure(fileTC18552, incStrucRows, totalFee, totalPaymentClosingFee));
                            switch(incStrucRows)
                            {
                                case 2:
                                    extentReports.CreateLog("Calculation of Total Fee when First Ratchet To Amount < Threshold Value is correct. ");
                                    break;
                                case 3:
                                    extentReports.CreateLog("Calculation of Total Fee when First Ratchet To Amount = Threshold Value is correct. ");
                                    break;
                                case 4:
                                    extentReports.CreateLog("Calculation of Total Fee when First Ratchet To Amount > Threshold Value is correct. ");
                                    break;
                            }
                        }

                        //Verify Calculation For Incentive Structure (Actual Amount) Section for Second Ratchet Fee
                        totalFee = cFEngagementSummary.GetactualTotalFeeBeforeEditing();
                        totalPaymentClosingFee = cFEngagementSummary.GetactualPaymentClosingFeeBeforeEditing();
                        for (int incStrucRows = 2; incStrucRows <= incentiveStrucCount; incStrucRows++)
                        {
                            Assert.IsTrue(cFEngagementSummary.VerifyCalculationForSecondRatchetIncentiveStructure(fileTC18552, incStrucRows, totalFee, totalPaymentClosingFee));
                            switch (incStrucRows)
                            {
                                case 2:
                                    extentReports.CreateLog("Calculation of Total Fee when Second Ratchet To Amount < Threshold Value is correct. ");
                                    break;
                                case 3:
                                    extentReports.CreateLog("Calculation of Total Fee when Second Ratchet To Amount = Threshold Value is correct. ");
                                    break;
                                case 4:
                                    extentReports.CreateLog("Calculation of Total Fee when Second Ratchet To Amount > Threshold Value is correct. ");
                                    break;
                            }
                        }

                        //Verify Calculation For Incentive Structure (Actual Amount) Section for Third Ratchet Fee
                        totalFee = cFEngagementSummary.GetactualTotalFeeBeforeEditing();
                        totalPaymentClosingFee = cFEngagementSummary.GetactualPaymentClosingFeeBeforeEditing();
                        for (int incStrucRows = 2; incStrucRows <= incentiveStrucCount; incStrucRows++)
                        {
                            Assert.IsTrue(cFEngagementSummary.VerifyCalculationForThirdRatchetIncentiveStructure(fileTC18552, incStrucRows, totalFee, totalPaymentClosingFee));
                            switch (incStrucRows)
                            {
                                case 2:
                                    extentReports.CreateLog("Calculation of Total Fee when Third Ratchet To Amount < Threshold Value is correct. ");
                                    break;
                                case 3:
                                    extentReports.CreateLog("Calculation of Total Fee when Third Ratchet To Amount = Threshold Value is correct. ");
                                    break;
                                case 4:
                                    extentReports.CreateLog("Calculation of Total Fee when Third Ratchet To Amount > Threshold Value is correct. ");
                                    break;
                            }
                        }
                        /*
                        //Verify Calculation For Incentive Structure (Actual Amount) Section for Fourth Ratchet Fee
                        totalFee = cFEngagementSummary.GetactualTotalFeeBeforeEditing();
                        totalPaymentClosingFee = cFEngagementSummary.GetactualPaymentClosingFeeBeforeEditing();
                        for (int incStrucRows = 2; incStrucRows <= incentiveStrucCount; incStrucRows++)
                        {
                            Assert.IsTrue(cFEngagementSummary.VerifyCalculationForFourthRatchetIncentiveStructure(fileTC18552, incStrucRows, totalFee, totalPaymentClosingFee));
                            switch (incStrucRows)
                            {
                                case 2:
                                    extentReports.CreateLog("Calculation of Total Fee when Fourth Ratchet To Amount < Threshold Value is correct. ");
                                    break;
                                case 3:
                                    extentReports.CreateLog("Calculation of Total Fee when Fourth Ratchet To Amount = Threshold Value is correct. ");
                                    break;
                                case 4:
                                    extentReports.CreateLog("Calculation of Total Fee when Fourth Ratchet To Amount > Threshold Value is correct. ");
                                    break;
                            }
                        }
                        */
                        //Verify Calculation For Incentive Structure (Actual Amount) Section for Final Ratchet Fee
                        totalFee = cFEngagementSummary.GetactualTotalFeeBeforeEditing();
                        totalPaymentClosingFee = cFEngagementSummary.GetactualPaymentClosingFeeBeforeEditing();
                        for (int incStrucRows = 2; incStrucRows <= incentiveStrucCount; incStrucRows++)
                        {
                            Assert.IsTrue(cFEngagementSummary.VerifyCalculationForFinalRatchetIncentiveStructure(fileTC18552, incStrucRows, totalFee, totalPaymentClosingFee));
                            switch (incStrucRows)
                            {
                                case 2:
                                    extentReports.CreateLog("Calculation of Total Fee when Final Ratchet Amount < Threshold Value is correct. ");
                                    break;
                                case 3:
                                    extentReports.CreateLog("Calculation of Total Fee when Final Ratchet Amount = Threshold Value is correct. ");
                                    break;
                                case 4:
                                    extentReports.CreateLog("Calculation of Total Fee when Final Ratchet Amount > Threshold Value is correct. ");
                                    break;
                            }
                        }

                        if (row==2)
                        {
                            driver.Close();
                            CustomFunctions.SwitchToWindow(driver, 0);
                            //usersLogin.UserLogOut();
                        }
                    }
                }

                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                driver.Quit();
            }
        }        
    }
}


