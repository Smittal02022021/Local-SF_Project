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
    class TMTT0007935_Opportunity_AdditionalClientAndSubject_NewOpportunity: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string file7935 = "TMTT0007935_Opportunity_AdditionalClientAndSubject_NewOpportunity1.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AdditionalClientAndSubject_NewOpportunity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + file7935;
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
                 string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 32);
                 usersLogin.SearchUserAndLogin(valUser);
                 string stdUser = login.ValidateUser();
                 Assert.AreEqual(stdUser.Contains(valUser), true);
                 extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function                  
                string value = addOpportunity.AddOpportunities(valJobType,file7935);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(file7935);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Validate added client with new types of Fee Attribution Party and Key Creditor along with additional Client and Subject
                //--Validate Additional Client and Subject
                //--Validate added client in Additional Clients/Subjects section
                 string addedCompany = clientSubjectsPage.ValidateAddedClient();
                 Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1), addedCompany);
                 string addedCompanyType = clientSubjectsPage.ValidateTypeOfAddedClient();
                 Assert.AreEqual("Client", addedCompanyType);
                 string addedCompanyRecType = clientSubjectsPage.ValidateRecTypeOfAddedClient();
                 Assert.AreEqual("Client", addedCompanyRecType);
                 extentReports.CreateLog(addedCompany + " with Type: "+ addedCompanyType+ " and Record Type: " + addedCompanyRecType +" is added in Additional Client/Subject section ");

                //--Validate added subject in Additional Clients/Subjects section
                 string addedSubject = clientSubjectsPage.ValidateAddedSubject();
                 Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2), addedSubject);
                 string addedSubjectType = clientSubjectsPage.ValidateTypeOfAddedSubject();
                 Assert.AreEqual("Subject", addedSubjectType);
                 string addedSubjectRecType = clientSubjectsPage.ValidateRecTypeOfAddedSubject();
                 Assert.AreEqual("Client", addedSubjectRecType);
                 extentReports.CreateLog(addedSubject + " with Type: " + addedSubjectType + " and Record Type: " + addedSubjectRecType+" is added in Additional Client/Subject section ");

                //---Validate added company for Fee Attribution Party
                 string addedFee = opportunityDetails.GetCompanyNameOfFeeAttributionParty();
                 Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1), addedFee);
                 string typeFee = opportunityDetails.GetTypeOfFeeAttributionParty();
                 Assert.AreEqual("Fee Attribution Party", typeFee);
                 string recTypeFee = opportunityDetails.GetRecTypeOfFeeAttributionParty();
                 Assert.AreEqual("Fee Attribution Party", recTypeFee);
                 extentReports.CreateLog("Company with name: "+ addedFee + " with Type: " +typeFee+ " and Record Type as: " +recTypeFee +" is displayed in Additional Clients/Subjects section ");

                 //---Validate added client for Key Creditors
                 string addedKey = opportunityDetails.GetCompanyNameOfKeyCreditor();
                 string typeKey = opportunityDetails.GetTypeOfKeyCreditor();
                 string recTypeKey = opportunityDetails.GetRecTypeOfKeyCreditor();
                    if (valJobType.Equals("Creditor Advisors"))
                    {
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1), addedKey);
                        Assert.AreEqual("Key Creditor", typeKey);
                        Assert.AreEqual("Key Creditor", recTypeKey);
                        extentReports.CreateLog("Company with name: " + addedKey + " with Type: " + typeKey + " and Record Type as: " + recTypeKey + " is displayed in Additional Clients/Subjects section ");
                    }
                
                    else
                    {
                        Assert.AreEqual("No new client exists", addedKey);
                        Assert.AreEqual("Key Creditor", typeKey);
                        Assert.AreEqual("Key Creditor", typeKey);
                        extentReports.CreateLog(addedKey+" for " + typeKey + " and Record Type " + recTypeKey + " exists in Additional Clients/Subjects section ");
                    }                
                  
                    //Update Additional Client and Subject to Yes and validate title of Additional Client/Subject Required Pop up  
                    opportunityDetails.UpdateAdditionalClientandSubject();                    
                    Assert.AreEqual(clientSubjectsPage.ValidateAdditionalClientSubjectTitle(), "Additional Clients/Subjects Required");
                    extentReports.CreateLog(clientSubjectsPage.ValidateAdditionalClientSubjectTitle() + " is displayed upon setting Yes for Additional Client and Subject ");

                    //Clicking Add Client and validating title of Add Additional Client(s) Pop up
                    clientSubjectsPage.ClickAddClient();
                    Console.WriteLine(clientSubjectsPage.ValidateAdditionalClientTitle());
                    Assert.AreEqual(clientSubjectsPage.ValidateAdditionalClientTitle(), "Add Additional Client(s)");
                    extentReports.CreateLog(clientSubjectsPage.ValidateAdditionalClientTitle() + " window is displayed ");

                    //Calling Add AdditionalClient function and validating message
                    clientSubjectsPage.AddAdditionalClient(file7935);
                    Assert.AreEqual("Success:" + '\r' + '\n' + "Company Added To Additional Clients", clientSubjectsPage.ValidateMessage());
                    extentReports.CreateLog(clientSubjectsPage.ValidateMessage() + " is displayed ");

                    //Validate additional clients in Table               
                    Assert.AreEqual("True", clientSubjectsPage.ValidateTableDetails());
                    extentReports.CreateLog("Client is added in Additional Clients Section ");

                    //Calling AddAdditionalSubject function and validating message
                    clientSubjectsPage.AddAdditionalSubject(file7935);
                    Assert.AreEqual("Success:" + '\r' + '\n' + "Company Added To Additional Subjects", clientSubjectsPage.ValidateSubjectMessage());
                    Console.WriteLine("Message: " + clientSubjectsPage.ValidateSubjectMessage());
                    extentReports.CreateLog(clientSubjectsPage.ValidateSubjectMessage() + " is displayed ");

                    //Validate additional Subject in Table                               
                    Assert.AreEqual("True", clientSubjectsPage.ValidateSubjectTableDetails());
                    extentReports.CreateLog("Subject is added in Additional Subjects Section ");

                    //Call selectClientInterest funtion 
                    clientSubjectsPage.selectClientInterest(file7935);

                    //--Validate added subject in Additional Clients/Subjects section while added from additional client Subject Pop Up
                    string addedAddSubject = clientSubjectsPage.ValidateAddedClient();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 35), addedAddSubject);
                    string addedAddSubjectType = clientSubjectsPage.ValidateTypeOfAddedClient();
                    Assert.AreEqual("Subject", addedAddSubjectType);
                    string addedAddSubjectRecType = clientSubjectsPage.ValidateRecTypeOfAddedClient();
                    Assert.AreEqual("Subject", addedAddSubjectRecType);
                    extentReports.CreateLog("Subject with name:"+ addedAddSubject + " with Type: " + addedAddSubjectType + " and Record Type: " + addedAddSubjectRecType + " is added in Additional Client/Subject section ");

                    //--Validate added client in Additional Clients/Subjects section from additional client Subject Pop Up
                    string addedAddCompany = clientSubjectsPage.ValidateAddedSubject();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 33), addedAddCompany);
                    string addedAddCompanyType = clientSubjectsPage.ValidateTypeOfAddedSubject();
                    Assert.AreEqual("Client", addedAddCompanyType);
                    string addedAddCompanyRecType = clientSubjectsPage.ValidateRecTypeOfAddedSubject();
                    Assert.AreEqual("Client", addedAddCompanyRecType);
                    extentReports.CreateLog(addedAddCompany + " with Type: " + addedAddCompanyType + " and Record Type: " + addedAddCompanyRecType + " is added in Additional Client/Subject section ");

                    //--Validate Fee Attribution Party Type for additional client
                    string addCompNameFee = opportunityDetails.GetCompanyNameOfFeeAttributionParty();
                    string addTypeFee = opportunityDetails.GetTypeOfFeeAttributionParty();
                    string addRecTypeFee = opportunityDetails.GetRecTypeOfFeeAttributionParty();
                   
                     Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 33), addCompNameFee);
                     Assert.AreEqual("Fee Attribution Party", addTypeFee);
                     Assert.AreEqual("Fee Attribution Party", addRecTypeFee);
                     extentReports.CreateLog("New client with name: " + addCompNameFee + " is copied with Type: " + addTypeFee + " and Record Type as: " + addRecTypeFee + " is displayed in Additional Clients/Subjects section ");
                    
                    //--Validate Key Creditor Type for additional client
                    string addCompNameKey = opportunityDetails.GetCompanyNameOfKeyCreditor();
                    string addTypeKey = opportunityDetails.GetTypeOfKeyCreditor();
                    string addRecTypeKey = opportunityDetails.GetRecTypeOfKeyCreditor();

                    if (valJobType.Equals("Creditor Advisors"))
                    {

                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 33), addCompNameKey);
                        Assert.AreEqual("Key Creditor", addTypeKey);
                        Assert.AreEqual("Key Creditor", addRecTypeKey);
                        extentReports.CreateLog("New Client with name: " + addCompNameKey + " is copied with Type: " + addTypeKey + " and Record Type as: " + addRecTypeKey + " is displayed in Additional Clients/Subjects section ");

                    }
                    else
                    {
                        Assert.AreEqual("No new client exists", addCompNameKey);
                        Assert.AreEqual("Key Creditor", addTypeKey);
                        Assert.AreEqual("Key Creditor", addRecTypeKey);
                        extentReports.CreateLog(addCompNameKey +" for " + addTypeKey + " and Record Type " + addRecTypeKey + " exists in Additional Clients/Subjects section ");
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
                driver.Quit();
            }
        }        
    }
}




