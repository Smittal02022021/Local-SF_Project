using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TMTT0013005_TMTT0013007_TMTT0013008_TMTT0013009_TMTT0013010_TMTT0013011_TMTT0013013_TMTT0013015_ValidateTailExpires : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        CNBCForm form = new CNBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();


        public static string fileTCTailExpires = "TMT_ValidateTailExpires.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyTailExpiresFieldValidations()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTCTailExpires;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunitiesWithStageVerballyEngaged(valJobType, fileTCTailExpires);
                Console.WriteLine("value : " + value);

                //Verify TC = TMTT0013008 --> Verify Tail Expires Field should not be required while creating new Opportunity with LOB: CF and Stage: Verbally Engaged, Legal Entity other than HL Capital
                Assert.IsFalse(addOpportunity.ValidateIfTailExpiresFieldIsRequiredOrNot());
                extentReports.CreateLog("Tail Expires Field is not required while creating new Opportunity with LOB: CF and Stage: Verbally Engaged, Legal Entity other than HL Capital. ");

                //Call function to save Opportunity with LOB: CF and Stage: Verbally Engaged, Legal Entity as HL Capital
                //string JobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                addOpportunity.ChangeLegalEntityFieldToHLCapitalToCreateOpportunityWithStageVerballyEngaged(valJobType, fileTCTailExpires);
                extentReports.CreateLog("Legal Entity changes to HL Capital. ");

                //Verify TC = TMTT0013009 --> Verify the required fields should not populate for opportunity with LOB: CF and Stage: Verbally Engaged and Legal Entity is HL Capital while creating new opportunity.
                extentReports.CreateLog("Required fields do not populate for opportunity with LOB: CF and Stage: Verbally Engaged and Legal Entity is HL Capital while creating new opportunity. ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTCTailExpires);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string oppNum = opportunityDetails.GetOppNumber();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + oppNum + " is created. ");

                //Fetch values of Opportunity Name, Client, Subject and Job Type
                string oppName = opportunityDetails.GetOpportunityName();
                string clientName = opportunityDetails.GetClient();
                string subjectName = opportunityDetails.GetSubject();
                string jobType = opportunityDetails.GetJobType();
                Console.WriteLine(jobType);

                /*Verify TC = TMTT0013007 --> Verify the Tail Expires is a required field when user tries to convert
                an opportunity with LOB: CF and Stage: Verbally Engaged and Legal Entity is HL Capital into Engagement*/

                Assert.IsTrue(opportunityDetails.ValidateIfTailExpiresFieldIsRequiredWhenRequestingForEngagement());
                extentReports.CreateLog("Tail Expires is a required field when user tries to convert an opportunity with LOB: CF and Stage: Verbally Engaged and Legal Entity is HL Capital into Engagement. ");

                //Search for the opportunity again
                opportunityHome.SearchOpportunity(oppName);

                //Update the opportunity details
                opportunityDetails.UpdateClientandSubject("A + K Agency GmbH");
                extentReports.CreateLog("Opportunity with number : " + oppNum + " is updated successfully. ");

                //Verify Tail Expires field is not required upon updating anopportunity
                Assert.IsFalse(addOpportunity.ValidateIfTailExpiresFieldIsRequiredOrNot());
                extentReports.CreateLog("Required fields do not populate for opportunity with LOB: CF and Stage: Verbally Engaged and Legal Entity is HL Capital upon updating an existing opportunity. ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                // Call function to add new opportunity with LOB = other than Verbally engaged and Legal Entity = HL Capital
                addOpportunity.AddOpportunities(valJobType, fileTCTailExpires);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTCTailExpires);

                //Validating Opportunity details page 
                string oppNum1 = opportunityDetails.GetOppNumber();
                string oppName1 = opportunityDetails.GetOpportunityName();
                extentReports.CreateLog("Opportunity with number : " + oppNum1 + " is created. ");

                //Verify the required fields should not populate for opportunity with LOB: CF and Stage: Other than Verbally Engaged and Legal Entity is HL Capital while creating new opportunity.
                extentReports.CreateLog("Required fields do not populate for opportunity with LOB: CF and Stage: Other than Verbally Engaged and Legal Entity is HL Capital while creating new opportunity. ");

                Assert.IsTrue(opportunityDetails.ValidateIfTailExpiresFieldIsRequiredWhenRequestingForEngagement());
                extentReports.CreateLog("Tail Expires is a required field when user tries to convert an opportunity with LOB: CF and Stage: Other than Verbally Engaged and Legal Entity is HL Capital into Engagement. ");

                //Search for the opportunity again
                opportunityHome.SearchOpportunity(oppName1);

                //Update the opportunity details
                opportunityDetails.UpdateClientandSubject("A + K Agency GmbH");
                extentReports.CreateLog("Opportunity with number : " + oppNum1 + " is updated successfully. ");

                //Verify Tail Expires field is not required upon updating anopportunity
                Assert.IsFalse(addOpportunity.ValidateIfTailExpiresFieldIsRequiredOrNot());
                extentReports.CreateLog("Required fields do not populate for opportunity with LOB: CF and Stage: Other than Verbally Engaged and Legal Entity is HL Capital upon updating an existing opportunity. ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                // Call function to add new opportunity with LOB != Verbally engaged and Legal Entity ! = HL Capital
                addOpportunity.AddOpportunitiesWithLegalEntityOtherThanHLCapital(valJobType, fileTCTailExpires);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTCTailExpires);

                //Validating Opportunity details page 
                string oppNum2 = opportunityDetails.GetOppNumber();
                string oppName2 = opportunityDetails.GetOpportunityName();
                extentReports.CreateLog("Opportunity with number : " + oppNum2 + " is created. ");

                //Verify the required fields should not populate for opportunity with LOB: CF and Stage: != Verbally Engaged and Legal Entity != HL Capital while creating new opportunity.
                extentReports.CreateLog("Required fields do not populate for opportunity with LOB: CF and Stage: Other than Verbally Engaged and Legal Entity is other than HL Capital while creating new opportunity. ");

                Assert.IsTrue(opportunityDetails.ValidateIfTailExpiresFieldIsRequiredWhenRequestingForEngagement());
                extentReports.CreateLog("Tail Expires is a required field when user tries to convert an opportunity with LOB: CF and Stage: Other than Verbally Engaged and Legal Entity is other than HL Capital into Engagement. ");

                //Search for the opportunity again
                opportunityHome.SearchOpportunity(oppName2);

                //Update the opportunity details
                opportunityDetails.UpdateClientandSubject("A + K Agency GmbH");
                extentReports.CreateLog("Opportunity with number : " + oppNum2 + " is updated successfully. ");

                //Verify Tail Expires field is not required upon updating anopportunity
                Assert.IsFalse(addOpportunity.ValidateIfTailExpiresFieldIsRequiredOrNot());
                extentReports.CreateLog("Required fields do not populate for opportunity with LOB: CF and Stage: Other than Verbally Engaged and Legal Entity is other than HL Capital upon updating an existing opportunity. ");

                //Log out from standard User and validate admin
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin user " + login.ValidateUser() + " logged in ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Add Opportunity with LOB: CF and Stage: Verbally Engaged, Legal Entity other than HL Capital using Admin User 
                addOpportunity.AddOpportunitiesWithStageVerballyEngaged(valJobType, fileTCTailExpires);
                addOpportunity.UpdateMissingFieldsForOpportunityWithStageVerballyEngagedAndLegalEntityOtherThanHLCapital(valJobType, fileTCTailExpires);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTCTailExpires);

                //Validating Opportunity details page 
                string oppNum3 = opportunityDetails.GetOppNumber();
                string oppName3 = opportunityDetails.GetOpportunityName();
                extentReports.CreateLog("Opportunity with number : " + oppNum3 + " is created. ");

                //Verify Tail Expires Field should not be required while creating new Opportunity with LOB: CF and Stage: Verbally Engaged, Legal Entity other than HL Capital
                Assert.IsFalse(addOpportunity.ValidateIfTailExpiresFieldIsRequiredOrNot());

                Assert.IsTrue(opportunityDetails.ValidateIfTailExpiresFieldIsRequiredWhenRequestingForEngagement());
                extentReports.CreateLog("Tail Expires is a required field when user tries to convert an opportunity with LOB: CF and Stage: Verbally Engaged and Legal Entity is other than HL Capital into Engagement. ");

                //Search for the opportunity again
                opportunityHome.SearchOpportunity(oppName3);

                //Update the opportunity details
                opportunityDetails.UpdateClientandSubject("A + K Agency GmbH");
                extentReports.CreateLog("Opportunity with number : " + oppNum3 + " is updated successfully. ");

                //Verify Tail Expires field is not required upon updating anopportunity
                Assert.IsFalse(addOpportunity.ValidateIfTailExpiresFieldIsRequiredOrNot());
                extentReports.CreateLog("Required fields do not populate for opportunity with LOB: CF and Stage: Other than Verbally Engaged and Legal Entity is other than HL Capital upon updating an existing opportunity. ");

                //Delete Created Opportunity
                opportunityHome.SearchOpportunity(oppName);
                opportunityDetails.DeleteInternalTeamOfOpportunity();
                opportunityHome.SearchOpportunity(oppName);
                opportunityDetails.DeleteOpportunity();
                extentReports.CreateLog("Opportunity with number : " + oppNum + " is deleted successfully. ");

                //Delete Created Opportunity
                opportunityHome.SearchOpportunity(oppName1);
                opportunityDetails.DeleteInternalTeamOfOpportunity();
                opportunityHome.SearchOpportunity(oppName1);
                opportunityDetails.DeleteOpportunity();
                extentReports.CreateLog("Opportunity with number : " + oppNum1 + " is deleted successfully. ");

                //Delete Created Opportunity
                opportunityHome.SearchOpportunity(oppName2);
                opportunityDetails.DeleteInternalTeamOfOpportunity();
                opportunityHome.SearchOpportunity(oppName2);
                opportunityDetails.DeleteOpportunity();
                extentReports.CreateLog("Opportunity with number : " + oppNum2 + " is deleted successfully. ");

                //Delete Created Opportunity
                opportunityHome.SearchOpportunity(oppName3);
                opportunityDetails.DeleteInternalTeamOfOpportunity();
                opportunityHome.SearchOpportunity(oppName3);
                opportunityDetails.DeleteOpportunity();
                extentReports.CreateLog("Opportunity with number : " + oppNum3 + " is deleted successfully. ");

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
