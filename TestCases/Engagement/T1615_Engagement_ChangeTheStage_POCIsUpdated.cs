using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Engagement
{
    class T1615_Engagement_ChangeTheStage_POCIsUpdated : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();       

        public static string fileTC1710 = "T1615_Engagement_ChangeTheStage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void EngagementCounterpartyContact_SaveAndCancel()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1710;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Clicking on Engagement Tab and search for Engagement by entering Job type         
                string message = engHome.SearchEngagementWithLOB("FVA", "Opinion Report");
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching with selected LOB and Stage are displayed ");

                //Validate title of Engagement Details page
                string title = engagementDetails.GetTitle();
                Assert.AreEqual("Engagement", title);
                extentReports.CreateLog("Page with title: " + title + " is displayed ");

                //Calling functions to update Record Type, Stage and validate POC
                int rowRecType = ReadExcelData.GetRowCount(excelPath, "RecordType");
                Console.WriteLine("rowUsers " + rowRecType);

                for (int row = 2; row <= rowRecType; row++) 
                {

                    string valType = ReadExcelData.ReadDataMultipleRows(excelPath, "RecordType", row, 2);
                    Console.WriteLine("valType: " +valType);

                    //Update Record Type of Engagement and validate the same
                    string recType = engagementDetails.UpdateRecordType(valType);
                    Assert.AreEqual(valType + " [Change]", recType);
                    extentReports.CreateLog("Record Type of Engagement is updated to : " + recType + " ");

                    for (int stage = 2; stage <= rowRecType; stage++)
                    {
                        string valStage = ReadExcelData.ReadDataMultipleRows(excelPath, "RecordType", stage, 1);
                        Console.WriteLine("valStage: " + valStage);                       

                        //Update stage and validate the same
                        string Stage = engagementDetails.UpdateEngStage(valStage);
                        Assert.AreEqual(valStage, Stage);
                        extentReports.CreateLog("Engagement stage is updated to : " + Stage + " ");

                        //Get the value of Percentage of Completion	
                        string POC = engagementDetails.GetPOCValue();
                        if(valStage.Equals("Closed") || valStage.Equals("Bill/File"))
                        {
                            Assert.AreEqual("100%", POC);
                            extentReports.CreateLog("POC: " +POC + " is displayed for Stage " + valStage + " and Record Type " + valType);
                        }                        

                        else if (valStage.Equals("Performing Analysis") || valStage.Equals("Dead"))
                            {
                            if (valType.Equals("Other FAS"))
                            {
                                Assert.AreEqual("75%", POC);
                                extentReports.CreateLog("POC: " + POC + " is displayed for Stage " + valStage + " and Record Type " + valType);
                            }
                            else if(valType.Equals("Litigation"))
                            {
                                Assert.AreEqual("60%", POC);
                                extentReports.CreateLog("POC: " + POC + " is displayed for Stage " + valStage + " and Record Type " + valType);
                            }
                            else
                            {
                                Assert.AreEqual("50%", POC);
                                extentReports.CreateLog("POC: " + POC + " is displayed for Stage " + valStage + " and Record Type " + valType);                            
                            }
                            }
                       else if (valStage.Equals("Opinion Report"))
                         {
                            Assert.AreEqual("90%", POC);
                            extentReports.CreateLog("POC: " + POC + " is displayed for Stage " + valStage + " and Record Type " + valType);
                        }
                       else if (valStage.Equals("Retained"))
                        {
                            Assert.AreEqual("0%", POC);
                            extentReports.CreateLog("POC: " + POC + " is displayed for Stage " + valStage + " and Record Type " + valType);
                        }
                        else 
                        {
                            Assert.AreEqual("25%", POC);
                            extentReports.CreateLog("POC: " + POC + " is displayed for Stage " + valStage + " and Record Type " + valType);
                        }
                    }                   
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
