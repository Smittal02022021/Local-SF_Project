using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T1649AdditionalClientSubjectRequiredDefaultPopUp : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1649 = "T1649_AdditionalClientsSubjectRequired.xlsx";
       
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AdditionalClientSubjectPopUp()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1649;

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

                //Calling Opportunity Home function                
                opportunityHome.ClickOpportunity();

                //Calling Click Continue function
                clientSubjectsPage.ClickContinue();

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string opportunityName = addOpportunity.AddOpportunities(valJobType, fileTC1649);

                //Validating title of Additional Client/Subject Required Pop up   
                Assert.AreEqual(clientSubjectsPage.ValidateAdditionalClientSubjectTitle(), "Additional Clients/Subjects Required");
                extentReports.CreateLog("Opportunity details are saved and " + clientSubjectsPage.ValidateAdditionalClientSubjectTitle() + " is displayed ");

                //Clicking Add Client and validating title of Add Additional Client(s) Pop up
                clientSubjectsPage.ClickAddClient();
                Console.WriteLine(clientSubjectsPage.ValidateAdditionalClientTitle());
                Assert.AreEqual(clientSubjectsPage.ValidateAdditionalClientTitle(), "Add Additional Client(s)");
                extentReports.CreateLog(clientSubjectsPage.ValidateAdditionalClientTitle() + " window is displayed ");

                //Calling AddAdditionalClient function and validating message
                clientSubjectsPage.AddAdditionalClient(fileTC1649);
                Assert.AreEqual("Success:" + '\r' + '\n' + "Company Added To Additional Clients", clientSubjectsPage.ValidateMessage());
                extentReports.CreateLog(clientSubjectsPage.ValidateMessage() + " is displayed ");

                //Validate additional clients in Table               
                Assert.AreEqual("True", clientSubjectsPage.ValidateTableDetails());
                extentReports.CreateLog("Client is added in Additional Clients Section ");

                //Calling AddAdditionalSubject function and validating message
                clientSubjectsPage.AddAdditionalSubject(fileTC1649);
                Assert.AreEqual("Success:" + '\r' + '\n' + "Company Added To Additional Subjects", clientSubjectsPage.ValidateSubjectMessage());
                Console.WriteLine("Message: " + clientSubjectsPage.ValidateSubjectMessage());
                extentReports.CreateLog(clientSubjectsPage.ValidateSubjectMessage() + " is displayed ");

                //Validate additional Subject in Table                               
                Assert.AreEqual("True", clientSubjectsPage.ValidateSubjectTableDetails());
                extentReports.CreateLog("Subject is added in Additional Subjects Section ");

                //Call selectClientInterest funtion 
                clientSubjectsPage.selectClientInterest(fileTC1649);

                //Validate title of HL Internal Team page  
                string titleName = clientSubjectsPage.ValidateInternalTeamTitle();
                Assert.AreEqual(opportunityName + " - HL Internal Team", titleName);
                extentReports.CreateLog(titleName + " is displayed ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1649);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityName + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog("Internal Team details are saved and " + driver.Title + " is displayed ");

                //Validate added client in Additional Clients/Subjects section
                string addedCompany = clientSubjectsPage.ValidateAddedClient();      
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1), addedCompany);
                extentReports.CreateLog(addedCompany + " is added in Additional Client/Subject section ");

                //Validate added subject in Additional Clients/Subjects section
                string addedSubject = clientSubjectsPage.ValidateAddedSubject();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2), addedSubject);
                extentReports.CreateLog(addedSubject + " is added in Additional Client/Subject section ");

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


