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
    class TMTT0005815_TMTT0005820_NBCAndCNBC_ExistingFunctionality : BaseClass
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

        public static string fileTC5815 = "TMTT0005815_NBCAndCNBC_ExistingFunctionality.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ExistingFunctionalities()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC5815;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search Opportunity with NBC Form and navigate to NBC Form
                string oppName = ReadExcelData.ReadData(excelPath, "Users", 2);
                opportunityHome.SearchOpportunity(oppName);
                string title = opportunityDetails.ClickNBCForm();
                Assert.AreEqual("NEW BUSINESS COMMITTEE REVIEW FORM", title);
                extentReports.CreateLog(title + " is displayed upon clicking NBC Form button on Opportunity details page ");

                //--Validate the access of NBC form
                string editNBC = form.ValidateIfFormIsEditable();
                Assert.AreEqual("Form is editable", editNBC);
                extentReports.CreateLog("NBC "+ editNBC + " for admin ");

                //--Validate review section of NBC form
                string secReview = form.ValidateReviewSection();
                Assert.AreEqual("Review", secReview);
                extentReports.CreateLog("Section: " +secReview + " is displayed for admin ");

                //Search Opportunity with CNBC Form and navigate to CNBC Form
                string oppName1 = ReadExcelData.ReadData(excelPath, "Users", 3);
                opportunityHome.SearchOpportunity(oppName1);
                string titleCNBC = opportunityDetails.ClickNBCForm();
                Assert.AreEqual("CAPITAL MARKETS NEW BUSINESS COMMITTEE REVIEW FORM", titleCNBC);
                extentReports.CreateLog(titleCNBC + " is displayed upon clicking NBC Form button on Opportunity details page ");

                //--Validate the access of CNBC form
                string editCNBC = cnbc.ValidateIfFormIsEditable();
                Assert.AreEqual("Form is editable", editCNBC);
                extentReports.CreateLog("CNBC " + editCNBC + " for admin ");

                //--Validate review section of CNBC form
                string secCNBCReview = cnbc.ValidateReviewSectionForAdmin();
                Assert.AreEqual("Review", secCNBCReview);
                extentReports.CreateLog("Section: " + secCNBCReview + " is displayed for admin ");

                //Validate functionality for various users
                int rowJobType = ReadExcelData.GetRowCount(excelPath, "Users");
                Console.WriteLine("rowCount " + rowJobType);

                for (int row = 2; row <= rowJobType; row++)
                {
                    //---Login as Internal Team members of NBC Form and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string valOpp = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    //Search Opportunity with NBC Form and navigate to NBC Form                    
                    opportunityHome.SearchOpportunity(valOpp);
                    string title1 = opportunityDetails.ClickNBCForm();
                    if (valOpp.Equals("Achilles"))
                    {
                        Assert.AreEqual("NEW BUSINESS COMMITTEE REVIEW FORM", title1);
                        extentReports.CreateLog(title1 + " is displayed upon clicking NBC Form button on Opportunity details page ");
                    }
                    else
                    {
                        Assert.AreEqual("CAPITAL MARKETS NEW BUSINESS COMMITTEE REVIEW FORM", title1);
                        extentReports.CreateLog(title1 + " is displayed upon clicking NBC Form button on Opportunity details page ");
                    }

                   
                    //--Validate the read/write access of NBC and CNBC form                   
                    if (valUser.Equals("Jon Harrison") || valUser.Equals("Mark Fisher"))
                    {
                        string editNBCUser = form.ValidateIfFormIsEditable();
                        Assert.AreEqual("Form is editable", editNBCUser);
                        extentReports.CreateLog(editNBCUser + " for Internal team member " + valUser + " ");
                    }
                    else if (valUser.Equals("Meissa Lee"))
                    {
                        string editNBCUser = form.ValidateIfFormIsEditable();
                        Assert.AreEqual("Form is editable", editNBCUser);
                        extentReports.CreateLog(editNBCUser + " for CAO user: " + valUser + " ");
                    }
                    else if (valUser.Equals("Christopher Au") && valOpp.Equals("Achilles"))
                    {
                        string editNBCUser = form.ValidateIfFormIsEditableForPG();
                        Assert.AreEqual("Form is not editable", editNBCUser);
                        extentReports.CreateLog(editNBCUser + " for Public Group user: " + valUser + " ");
                    }

                    else if (valUser.Equals("Christopher Au") && valOpp.Equals("Gresham"))
                    {
                        string editNBCUser = form.ValidateIfCNBCFormIsEditableForPG();
                        Assert.AreEqual("Form is not editable", editNBCUser);
                        extentReports.CreateLog(editNBCUser + " for Public Group user: " + valUser + " ");
                    }
                    else
                    {
                        string editNBCUser = form.ValidateIfFormIsEditable();
                        Assert.AreEqual("Form is editable", editNBCUser);
                        extentReports.CreateLog(editNBCUser + " for delegate of Internal team member " + valUser + " ");
                    }
                
                    //--Validate review section of NBC form
                    string secReviewUser = form.ValidateReviewSection();
                    if (valUser.Equals("Jon Harrison") || valUser.Equals("Mark Fisher"))
                    {
                        Assert.AreEqual("No Review section", secReviewUser);
                        extentReports.CreateLog(secReviewUser + " is displayed for Internal team member " + valUser + " ");
                    }
                    else if (valUser.Equals("Meissa Lee"))
                    {
                        Assert.AreEqual("Review", secReviewUser);
                        extentReports.CreateLog("Section: "+secReviewUser + " is displayed for CAO user: " + valUser + " ");
                    }
                    else if (valUser.Equals("Christopher Au"))
                    {
                        Assert.AreEqual("No Review section", secReviewUser);
                        extentReports.CreateLog(secReviewUser + " is displayed for Public Group user: " + valUser + " ");
                    }
                    else
                    {
                        Assert.AreEqual("No Review section", secReviewUser);
                        extentReports.CreateLog(secReviewUser + " is displayed for delegate of Internal team member " + valUser + " ");
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
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }        
    }
}

    

