using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class TMTT0012117_TMTT0012119_ValidateCoExistFieldForGCATracking : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileGCATracking = "ValidateCoExistFieldForGCATracking.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ValidateCoExistCheckboxWhenBothHLAndGCAMembersInDealTeam()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileGCATracking;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate Admin user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin User " + login.ValidateUser() + " is able to login ");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                Console.WriteLine("rowCount " + rowCount);

                for (int row = 2; row <= rowCount; row++)
                {
                    //Search Standard user by global search
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    homePage.SearchUserByGlobalSearch(fileGCATracking, user);

                    //Verify searched user
                    string userPeople = homePage.GetPeopleOrUserName();
                    string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    Assert.AreEqual(userPeopleExl, userPeople);
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    //Login as standard user
                    usersLogin.LoginAsSelectedUser();
                    string standardUser = login.ValidateUser();
                    string standardUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    Assert.AreEqual(standardUserExl.Contains(standardUser), true);
                    extentReports.CreateLog("Standard User: " + standardUser + " is able to login ");

                    //Call function to open Add Opportunity Page & Select LOB
                    opportunityHome.ClickOpportunity();
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);
                    Console.WriteLine("valRecordType:" + valRecordType);
                    opportunityHome.SelectLOBAndClickContinue(valRecordType);

                    //Validating Title of New Opportunity Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Calling AddOpportunities function                
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string value = addOpportunity.AddOpportunities(valJobType, fileGCATracking, row);
                    extentReports.CreateLog("Required fields added in order to create an opportunity. ");

                    //Call function to enter Internal Team details
                    clientSubjectsPage.EnterHLAndGCAStaffDetails(fileGCATracking, row);
                    extentReports.CreateLog("Both HL and GCA acquired members added to the deal team. ");

                    //Fetch values of Opportunity Number, Name, Client, Subject
                    string oppNum = opportunityDetails.GetOppNumber();
                    string oppName = opportunityDetails.GetOpportunityName();
                    string clientName = opportunityDetails.GetClient();
                    string subjectName = opportunityDetails.GetSubject();

                    //Validating Opportunity created successfully
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with LOB : " + valRecordType + " is created. Opportunity number is : " + oppNum + ". ");

                    //Validate CoExist checkbox exist and checked or not on Opportunity Details page
                    string checkboxValidationResult = opportunityDetails.ValidateIfCoExistFieldIsPresentAndCheckedOrNot();
                    extentReports.CreateLog(checkboxValidationResult + " on Opportunity detail page. ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CreateContact(fileGCATracking, valContact, valRecordType, valContactType);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved. ");

                    //Update required Opportunity fields for conversion and Internal team details
                    if (valRecordType == "CF")
                    {
                        opportunityDetails.UpdateReqFieldsForCFConversionMultipleRows(fileGCATracking, row);
                        extentReports.CreateLog("Fields required for converting CF opportunity to engagement are updated. ");
                    }
                    else if (valRecordType == "FVA")
                    {
                        opportunityDetails.UpdateReqFieldsForFVAConversionMultipleRows(fileGCATracking, row);
                        extentReports.CreateLog("Fields required for converting FVA opportunity to engagement are updated. ");
                    }
                    else
                    {
                        opportunityDetails.UpdateReqFieldsForFRConversionMultipleRows(fileGCATracking, row);
                        extentReports.CreateLog("Fields required for converting FR opportunity to engagement are updated. ");
                    }

                    opportunityDetails.UpdateInternalTeamDetails(fileGCATracking);
                    extentReports.CreateLog("Deal team member roles are updated. ");

                    //Log out from standard User
                    usersLogin.UserLogOut();

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(oppName);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileGCATracking);
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
                    homePage.SearchUserByGlobalSearch(fileGCATracking, user);
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
                    homePage.SearchUserByGlobalSearch(fileGCATracking, ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2));
                    usersLogin.LoginAsSelectedUser();
                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2)), true);
                    extentReports.CreateLog("CAO User: " + caoUser + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Approve the Opportunity 
                    opportunityDetails.ClickApproveButton();
                    extentReports.CreateLog("Opportunity number: " + oppNum + " is approved. ");

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEng();
                    extentReports.CreateLog("Opportunity number: " + oppNum + " is successfuly converted into Engagement. ");

                    //Validate CoExist checkbox exist and checked or not on Engagement Details page
                    string checkboxValidationResult1 = engagementDetails.ValidateIfCoExistFieldIsPresentAndCheckedOrNot();
                    extentReports.CreateLog(checkboxValidationResult1 + " on Engagement detail page. ");

                    usersLogin.UserLogOut();

                    //Login again as Standard User
                    homePage.SearchUserByGlobalSearch(fileGCATracking, user);
                    usersLogin.LoginAsSelectedUser();
                    string stdUser2 = login.ValidateUser();
                    Assert.AreEqual(stdUser2.Contains(user), true);
                    extentReports.CreateLog("Standard User: " + stdUser2 + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Validate CoExist checkbox exist and checked or not on existing Opportunity Details page
                    opportunityDetails.ValidateIfCoExistFieldIsPresentAndCheckedOrNot();
                    extentReports.CreateLog(checkboxValidationResult + " on an existing Opportunity detail page. ");

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


