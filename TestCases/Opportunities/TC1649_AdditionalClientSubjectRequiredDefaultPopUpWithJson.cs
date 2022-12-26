using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunity
{
    class T1649AdditionalClientSubjectRequiredDefaultPopUpWithJson : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPageWithJson addOpportunity = new AddOpportunityPageWithJson();
        AdditionalClientSubjectPageWithJson clientSubjectsPage = new AdditionalClientSubjectPageWithJson();
        UsersLogin usersLogin = new UsersLogin();
        public static string fileTC1649 = "T1649_AdditionalClientSubjectRequired.xlsx";
        string excelPath = @"C:\Users\SGoyal0427\source\repos\SalesForce_Project\TestData\" + fileTC1649;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ReadJSONData.Generate("T1649_AdditionalClientSubjectRequired.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AdditionalClientSubjectPopUp()
        {
            try
            {
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                 usersLogin.LoginAsStandardUser();
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadJSONData.data.authentication.stdUser), true);
                extentReports.CreateLog("Standard User " + stdUser + " is able to login ");

                //Calling Opportunity Home function                
                opportunityHome.ClickOpportunity();

                //Calling Click Continue function
                clientSubjectsPage.ClickContinue();

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function                
                string opportunityName = addOpportunity.AddOpportunities(fileTC1649);

                //Validating title of Additional Client/Subject Required Pop up   
                Assert.AreEqual(clientSubjectsPage.ValidateAdditionalClientSubjectTitle(), "Additional Clients/Subjects Required");
                extentReports.CreateLog("Opportunity details are saved and " + clientSubjectsPage.ValidateAdditionalClientSubjectTitle() + " page is displayed ");

                //Clicking Add Client and validating title of Add Additional Client(s) Pop up
                clientSubjectsPage.ClickAddClient();
                Console.WriteLine(clientSubjectsPage.ValidateAdditionalClientTitle());
                Assert.AreEqual(clientSubjectsPage.ValidateAdditionalClientTitle(), "Add Additional Client(s)");
                extentReports.CreateLog(clientSubjectsPage.ValidateAdditionalClientTitle() + " window is displayed ");

                //Calling AddAdditionalClient function and validating message
                clientSubjectsPage.AddAdditionalClient();
                Assert.AreEqual("Success:" + '\r' + '\n' + "Company Added To Additional Clients", clientSubjectsPage.ValidateMessage());
                extentReports.CreateLog(clientSubjectsPage.ValidateMessage() + " is displayed ");

                //Validate additional clients in Table               
                Assert.AreEqual("True", clientSubjectsPage.ValidateTableDetails());
                extentReports.CreateLog("Client is added in Additional Clients Section ");

                //Calling AddAdditionalSubject function and validating message
                clientSubjectsPage.AddAdditionalSubject();
                Assert.AreEqual("Success:" + '\r' + '\n' + "Company Added To Additional Subjects", clientSubjectsPage.ValidateSubjectMessage());
                Console.WriteLine("Message: " + clientSubjectsPage.ValidateSubjectMessage());
                extentReports.CreateLog(clientSubjectsPage.ValidateSubjectMessage() + " is displayed ");

                //Validate additional Subject in Table                               
                Assert.AreEqual("True", clientSubjectsPage.ValidateSubjectTableDetails());
                extentReports.CreateLog("Subject is added in Additional Subjects Section ");

                //Call selectClientInterest funtion 
                clientSubjectsPage.selectClientInterest();

                //Validate title of HL Internal Team page  
                string titleName = clientSubjectsPage.ValidateInternalTeamTitle();
                Assert.AreEqual(opportunityName + " - HL Internal Team", titleName);
                extentReports.CreateLog(titleName + " is displayed ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityName + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validate added client in Additional Clients/Subjects section
                string addedCompany = clientSubjectsPage.VaidateAddedClient();
                Assert.AreEqual(ReadJSONData.data.additionalClientSubject.search, addedCompany);
                extentReports.CreateLog(addedCompany + " is added in Additional Client/Subject section ");

                //Validate added subject in Additional Clients/Subjects section
                string addedSubject = clientSubjectsPage.VaidateAddedSubject();
                Assert.AreEqual(ReadJSONData.data.additionalClientSubject.search, addedSubject);
                extentReports.CreateLog(addedSubject + " is added in Additional Client/Subject section ");

            }
            catch (Exception)
            {
                extentReports.CreateLog("Test ended with");
                ExtentClose();
            }
        }
    }
}

