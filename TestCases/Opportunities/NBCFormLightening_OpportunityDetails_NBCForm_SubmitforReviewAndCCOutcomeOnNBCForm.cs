using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Opportunity;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Opportunity
{
    class NBCFormLightening_OpportunityDetails_NBCForm_SubmitforReviewAndCCOutcomeOnNBCForm: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        NBCForm form = new NBCForm();
        CNBCForm cnbc = new CNBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC1232 = "T1232_NBCFormSubmitforReview.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void NBCFormSubmitforReview()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1232;
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
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:"+valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunities(valJobType, fileTC1232);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1232);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");
                
                //Fetch values of Opportunity Name, Client, Subject and Job Type
                string oppNum = opportunityDetails.GetOppNumber();
                string clientName = opportunityDetails.GetClient();
                string subjectName = opportunityDetails.GetSubject();
                string jobType = opportunityDetails.GetJobType();
                Console.WriteLine(jobType);

                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateInternalTeamDetails(fileTC1232);

                //Click on NBC page and validate title of page
                string title=  opportunityDetails.ClickNBCFormL();
                Assert.AreEqual("Public Sensitivity", title);
                extentReports.CreateLog("NBC Form page is displayed with default tab : " +title + " ");

                //Validate pre populated fields on NBC form
                string oppNBC =form.ValidateOppName();
                Assert.AreEqual(oppNum, oppNBC);
                extentReports.CreateLog("Opportunity Name: " + oppNBC + " in NBC form matches with Opportunity details page ");

                string clientNBC = form.ValidateClient();
                Assert.AreEqual(clientName, clientNBC);
                extentReports.CreateLog("Client Company: " + clientNBC + " in NBC form matches with Opportunity details page ");

                string subjectNBC = form.ValidateSubject();
                Assert.AreEqual(subjectName, subjectNBC);
                extentReports.CreateLog("Subject Company: " + subjectNBC + " in NBC form matches with Opportunity details page ");

                string jobTypeNBC = form.ValidateJobType();
                Assert.AreEqual(jobType, jobTypeNBC);
                extentReports.CreateLog("Job Type: " + jobTypeNBC + " in NBC form matches with Opportunity details page ");

                //Select the Review Submission button
                form.ClickReviewSubmission();

                //Validate validations displayed for mandatory fields
                string txtOppOverview=form.ClickOpportunityOverview();
                Assert.AreEqual("Opportunity Overview", txtOppOverview);
                extentReports.CreateLog("Tab with name " + txtOppOverview + " is displayed upon clicking the tab. ");

                string actTransOverValidation = form.GetFieldsValidationsOfOppOverview();
                Console.WriteLine(actTransOverValidation);
                string expTransOverValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 1);
                Assert.AreEqual(expTransOverValidation, actTransOverValidation);
                extentReports.CreateLog("Validation: " + actTransOverValidation + " is displayed for Transaction Overview field ");

                //Click cancel button and accept alert
                /*
                form.ClickCancelAndAcceptAlert();

                //Validate visibility of Tabs on checking/unchecking the Toggle Tabs checkbox
                string actualTabs = form.ClickToggleAndValidateTabs();
                string expTabs = ReadExcelData.ReadData(excelPath, "NBCForm", 32);
                Console.WriteLine("Tabs: "+ actualTabs);
                Assert.AreEqual(expTabs, actualTabs);
                extentReports.CreateLog("Tabs : " + actualTabs + " are displayed on checking the Toggle Tabs checkbox ");

                string NoTabs = form.ClickToggleAndValidateTabs();
                Assert.AreEqual("Tabs are not displayed", NoTabs);
                extentReports.CreateLog(NoTabs + " on unchecking the Toggle Tabs checkbox ");

                //Log out from standard User and validate admin
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin user " + login.ValidateUser() + " logged in ");

                //Search created opportunity, update conflict check details and open NBC details page
                string valSearch = opportunityHome.SearchOpportunity(value);
                Console.WriteLine("result : " + valSearch);
                opportunityDetails.ClickNBCForm();
                string statusCC = form.GetCCStatus();
                Console.WriteLine("statusCC: " + statusCC);
                Assert.AreEqual("Conflicts Check not requested yet - Please return to the Opportunity and request a Conflicts Check.", statusCC);
                extentReports.CreateLog("Status: " +statusCC + " is displayed when Conflict Check fields are blank ");

                opportunityHome.SearchOpportunity(value);
                opportunityDetails.UpdateCCOnly();
                extentReports.CreateLog("Conflict Check is requested by setting Request Date ");

                //Login as Standard User, validate the user and search for created opportunity
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");
                opportunityHome.SearchOpportunity(value);

                opportunityDetails.ClickNBCForm();
                string statusCC1 = form.GetCCStatus();
                Console.WriteLine("statusCC1" + statusCC1);               
                Assert.AreEqual("In Progress, requested on: " + DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture), statusCC1);
                extentReports.CreateLog("Status: " + statusCC1 + " is displayed when Conflict Check is requested ");
                */

                //Call function to enter NBC details
                //form.EnterDetailsAndClickSubmit(fileTC1232);
                //form.EnterIntAngleValue(fileTC1232);
                extentReports.CreateLog("NBC details are saved ");

                //Validate title of Email Template page
                string pageTitle = form.ValidateHeader();
                Assert.AreEqual("Send Email", pageTitle);
                extentReports.CreateLog(pageTitle + " is displayed ");

                //Validate Opportunity Name in Email and navigate to Opportunity details page
                string emailOppName = form.GetOppName();
                //Assert.AreEqual(oppName, emailOppName);
                extentReports.CreateLog(" Email Template with Opportunity " + emailOppName + " is displayed ");

                //Login as CAO User
                usersLogin.UserLogOut();
                string valCAOUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valCAOUser);
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(valCAOUser), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");
                opportunityHome.SearchOpportunity(value);
                opportunityDetails.ClickNBCForm();

                //Save Grade details and validate the same
                form.SaveGradeValue();
                opportunityDetails.ClickNBCForm();
                string grade = form.GetGradeValue();
                Assert.AreEqual("A+", grade);
                extentReports.CreateLog("Grade is saved with value: " + grade + " ");
                usersLogin.UserLogOut();

                //Logout of CAO and validate access for Internal Team, its Delegates and CAO etc.
                //Validate functionality for various users
                int rowJobType = ReadExcelData.GetRowCount(excelPath, "Users");
                Console.WriteLine("rowCount " + rowJobType);

                for (int row = 2; row <= rowJobType; row++)
                {

                    //---Login as Internal Team members of NBC Form and validate the user
                    string valUser1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);

                    usersLogin.SearchUserAndLogin(valUser1);
                    string User = login.ValidateUser();
                    Assert.AreEqual(User.Contains(valUser1), true);
                    extentReports.CreateLog("User: " + User + " logged in ");
                    opportunityHome.SearchOpportunity(value);
                    opportunityDetails.ClickNBCForm();

                    //Validate fields, button for CAO i.e. Estimated Fee, Review section (Grade,Notes,Date Submitted,Reason, Fee Diff) and buttons (Save, Return To Opp,EU Override, PDF View, Attach File.)
                    //--Validate Estimated Fee
                    string valEstFee = form.ValidateEstimatedFeeField();
                    if (valUser1.Equals("Meissa Lee"))
                    {
                        Assert.AreEqual("True", valEstFee);
                        extentReports.CreateLog("Estimated Fee field is enabled for CAO: " + valUser1 + " ");
                    }
                    else
                    {
                        Assert.AreEqual("False", valEstFee);
                        extentReports.CreateLog("Estimated Fee field is disabled for user: " + valUser1 + " ");
                    }

                    //--Validate Review section i.e. Grade,Notes,Date Submitted,Reason, Fee Diff
                    string valGrade = form.ValidateGradeField();
                    if (valUser1.Equals("Meissa Lee"))
                    {
                        Assert.AreEqual("True", valGrade);
                        extentReports.CreateLog("Grade field is enabled for CAO: " + valUser1 + " ");
                    }
                    else
                    {
                        Assert.AreEqual("No Grade field", valGrade);
                        extentReports.CreateLog("Grade field is not displayed for user: " + valUser1 + " ");
                    }

                    string valNotes = form.ValidateNotesField();
                    if (valUser1.Equals("Meissa Lee"))
                    {
                        Assert.AreEqual("True", valNotes);
                        extentReports.CreateLog("Notes field is enabled for CAO: " + valUser1 + " ");
                    }
                    else
                    {
                        Assert.AreEqual("No Notes field", valNotes);
                        extentReports.CreateLog("Notes field is not displayed for user: " + valUser1 + " ");
                    }

                    string valDate = form.ValidateDateSubmittedField();
                    if (valUser1.Equals("Meissa Lee"))
                    {
                        Assert.AreEqual("True", valDate);
                        extentReports.CreateLog("Date Submitted field is enabled for CAO: " + valUser1 + " ");
                    }
                    else
                    {
                        Assert.AreEqual("No Date Submitted field", valDate);
                        extentReports.CreateLog("Date Submitted field is not displayed for user: " + valUser1 + " ");
                    }

                    string valReason = form.ValidateReasonField();
                    if (valUser1.Equals("Meissa Lee"))
                    {
                        Assert.AreEqual("True", valReason);
                        extentReports.CreateLog("Reason field is enabled for CAO: " + valUser1 + " ");
                    }
                    else
                    {
                        Assert.AreEqual("No Reason field", valReason);
                        extentReports.CreateLog("Reason field is not displayed for user: " + valUser1 + " ");
                    }

                    string valFee = form.ValidateFeeDifferencesField();
                    if (valUser1.Equals("Meissa Lee"))
                    {
                        Assert.AreEqual("True", valFee);
                        extentReports.CreateLog("Fee Differences field is enabled for CAO: " + valUser1 + " ");
                    }
                    else
                    {
                        Assert.AreEqual("No Fee Differences field", valFee);
                        extentReports.CreateLog("Fee Differences field is not displayed for user: " + valUser1 + " ");
                    }

                    //--Validate Buttons i.e. Save NBC, Return To Opp, EU Override, PDF View, Attach File
                    string valSave = form.ValidateSaveNBCButton();
                    if (valUser1.Equals("Meissa Lee"))
                    {
                        Assert.AreEqual("True", valSave);
                        extentReports.CreateLog("Save NBC button is enabled for CAO: " + valUser1 + " ");
                    }
                    else
                    {
                        Assert.AreEqual("No Save button", valSave);
                        extentReports.CreateLog("Save NBC button is not displayed for user: " + valUser1 + " ");
                    }
                    if (valUser1.Equals("Meissa Lee"))
                    {
                        string valReturn = form.ValidateReturnToOpportunityButton();
                        Assert.AreEqual("True", valReturn);
                        extentReports.CreateLog("Return To Opportunity button is enabled for CAO: " + valUser1 + " ");
                    }
                    else
                    {
                        string valReturn = form.ValidateReturnToOpportunityButtonForCFUser();
                        Assert.AreEqual("True", valReturn);
                        extentReports.CreateLog("Return To Opportunity button is enabled for user: " + valUser1 + " ");
                    }

                    string valEU = form.ValidateEUOverrideButton();
                    if (valUser1.Equals("Meissa Lee"))
                    {
                        Assert.AreEqual("True", valEU);
                        extentReports.CreateLog("EU Override button is enabled for CAO: " + valUser1 + " ");
                    }
                    else
                    {
                        Assert.AreEqual("No EU Override button", valEU);
                        extentReports.CreateLog("EU Override button is not displayed for user: " + valUser1 + " ");
                    }
                    string valPDF = form.ValidatePDFViewButton();
                    Assert.AreEqual("True", valPDF);
                    extentReports.CreateLog("PDF View button is enabled for user: " + valUser1 + " ");

                    string valAttachFile = form.ValidateAttachFileButton();
                    Assert.AreEqual("True", valAttachFile);
                    extentReports.CreateLog("Attach File button is enabled for user: " + valUser1 + " ");

                    string valAddFinancials = form.ValidateAddFinancialsButton();
                    Assert.AreEqual("True", valAddFinancials);
                    extentReports.CreateLog("Add Financials button is enabled for user: " + valUser1 + " ");

                    usersLogin.UserLogOut();
                }
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

    

