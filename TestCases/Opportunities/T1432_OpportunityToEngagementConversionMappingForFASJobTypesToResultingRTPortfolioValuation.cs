using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T1432_OpportunityToEngagementConversionMappingForFASJobTypesToResultingRTPortfolioValuation : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        SendEmailNotification notification = new SendEmailNotification();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();


        public static string fileTC1432 = "T1432_OpportunityToEngagementConversionMappingForFASJobTypes.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void OpportunityToEngagementConversionMappingForFAS()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1432;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                Console.WriteLine("rowCount " + rowJobType);

                for (int row = 2; row <= rowJobType; row++)
                {

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);

                    //Login as Standard User profile and validate the user
                 usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validate Women Led field and Calling AddOpportunities function      
                string womenLed = addOpportunity.ValidateWomenLedField(valRecordType);
                string secName = addOpportunity.GetAdminSectionName(valRecordType);
                Assert.AreEqual("Women Led", womenLed);
                Assert.AreEqual("Administration", secName);
                extentReports.CreateLog("Field with name: " + womenLed + " is displayed under section: " + secName + " ");
                    
                //Calling AddOpportunities function
                string value = addOpportunity.AddOpportunities(valJobType,fileTC1432);
                Console.WriteLine("value : " + value);
                extentReports.CreateLog("Opportunity : " + value + " is created ");

                //Call function to enter Internal Team details and validate opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1432);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact         
                String valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                String valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(fileTC1432, valContact, valRecordType, valContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForFVAConversion(fileTC1432);
                opportunityDetails.UpdateInternalTeamDetails(fileTC1432);

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileTC1432);                
                extentReports.CreateLog("Conflict Check fields are updated ");

                //Login again as Standard User
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                 //Update Total Anticipated Revenue
                 opportunityDetails.UpdateTotalAnticipatedRevenueForValidations();

                 //Requesting for engagement and validate the success message
                 string msgSuccess = opportunityDetails.ClickRequestEng();
                Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                //Log out of Standard User
                usersLogin.UserLogOut();

                //Login as CAO user to approve the Opportunity
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Approve the Opportunity 
                opportunityDetails.ClickApproveButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog("Opportunity is approved ");

                //Calling function to convert to Engagement
                  opportunityDetails.ClickConvertToEng();

                    //Validate the Engagement name in Engagement details page
                    string engName = engagementDetails.GetEngName();
                    Assert.AreEqual(opportunityNumber, engName);
                    extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");

                    //Validate the value of Stage in Engagement details page
                    string engStage = engagementDetails.GetStage();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Engagement", 1), engStage);
                    extentReports.CreateLog("Value of Stage field is : " + engStage + " for Job Type " + valJobType + " ");

                    //Validate the value of Record Type in Engagement details page
                    string engRecordType = engagementDetails.GetRecordType();
                    Console.WriteLine("engRecordType: " + engRecordType);
                    extentReports.CreateLog("Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                    //Validate the value of HL Entity in Engagement details page
                    string engLegalEntity = engagementDetails.GetLegalEntity();
                    Console.WriteLine("engHLEntity: " + engLegalEntity);
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Engagement", 3), engLegalEntity);
                    extentReports.CreateLog("Value of HL Entity is : " + engLegalEntity + " ");

                    //Validate the section in which Women led field is displayed
                    string lblWomenLed = engagementDetails.ValidateWomenLedField(valJobType);
                    Assert.AreEqual("Women Led", lblWomenLed);
                    string secWomenLed = engagementDetails.GetSectionNameOfWomenLedField(valJobType);
                    Assert.AreEqual("Closing - Document Checklist", secWomenLed);
                    extentReports.CreateLog(lblWomenLed + " field is displayed under section: " + secWomenLed + " ");

                    //Validate the value of Women Led in Engagement details page
                    string engWomenLed = engagementDetails.GetWomenLed();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30), engWomenLed);
                    extentReports.CreateLog("Value of Women Led is : " + engWomenLed + " is same as selected in Opportunity page ");
                    
                    usersLogin.UserLogOut();
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



