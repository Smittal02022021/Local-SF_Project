using NUnit.Framework;
using SalesForce_Project.Pages.Tableau;
using SalesForce_Project.Pages;
using SalesForce_Project.TestData;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Diagnostics;
using System.Collections;

namespace SalesForce_Project.TestCases.Tableau
{
    class TestSikuli : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        TableauPage tableau = new TableauPage();
        UsersLogin usersLogin = new UsersLogin();

        public static string fileRegionMapping = "RegionMapping_ACE-FSC.xlsx";
        public static string fileTableauReport = "SponsorsList(Summarized).xlsx";
        public static string fileRegion = "Regions.xlsx";
        public static string username = "Screenshot_Username.png";
        public static string password = "Screenshot_Password.png";
        public static string login = "Screenshot_Login.png";
        public static string empStatus = "Screenshot_EmpStatus.png";
        public static string statusValue = "Screenshot_EnterStatus.png";


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestInitialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void POC()
        {
            try 
            { 
                tableau.TestLogin(username, password, login, empStatus, statusValue);
                extentReports.CreateLog("User logged in. ");
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