using NUnit.Framework;
using SalesForce_Project.Pages.Tableau;
using SalesForce_Project.Pages;
using SalesForce_Project.TestData;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Diagnostics;
using System.Collections;
using Sikuli4Net.sikuli_REST;

namespace SalesForce_Project.TestCases.Tableau
{
    class TableauPOC : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        TableauPage tableau = new TableauPage();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        CompanyHomePage companyHome = new CompanyHomePage();

        Screen screen = new Screen();

        public static string fileRegionMapping = "RegionMapping_ACE-FSC.xlsx";
        public static string fileTableauReport = "SponsorsList(Summarized).xlsx";
        public static string fileRegion = "Regions.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize1();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void POC()
        {
            //Get path of Test data file
            string excelPath = ReadJSONData.data.filePaths.testData + fileRegion;
            Console.WriteLine(excelPath);

            int rowCount = ReadExcelData.GetRowCount(excelPath, "Sheet1");
            Console.WriteLine("rowCount " + rowCount);

            for (int row = 1; row <= rowCount; row++)
            {
                tableau.LoginintoTableau();
                extentReports.CreateLog("User logged in. ");

                tableau.SelectRegion(fileRegion, row);
                extentReports.CreateLog("Region selected. ");

                tableau.DownloadExcel();
                extentReports.CreateLog("File download button clicked. ");

                try
                {
                    Assert.AreEqual(tableau.ValidateFileName(fileTableauReport), "SponsorsList(Summarized).xlsx");
                    extentReports.CreateLog("File name is correct. ");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    //Open Salesforce application
                    tableau.OpenSF();

                    //Validating Title of Login Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Calling Login function                
                    login.LoginApplication();

                    //Get path of Test data file
                    string excelPath1 = ReadJSONData.data.filePaths.testData + fileTableauReport;

                    //Get row count from the Tableau report
                    int rowCount1 = ReadExcelData.GetRowCount(excelPath1, "Sheet 1");

                    for (int row1 = 42; row1 <= rowCount1; row1++)
                    {
                        //Search Company and go to company detail page
                        string companyName = companyHome.SearchTableauCompany(fileTableauReport, row1);
                        extentReports.CreateLog(companyName + " company detail page is displayed. ");

                        //Get the state/country from mapping sheet
                        ArrayList country = tableau.GetStateCountryNameFromMappingSheet(fileRegion, fileRegionMapping, row);

                        //Match Company Country Between SF And MappingSheet
                        bool matchResult = tableau.MatchCompanyCountryBetweenSFAndMappingSheet(country);
                        if (matchResult==true)
                        {
                            extentReports.CreateLog("Company: " + companyName + " country matches with the mapping sheet. ");
                        }
                        else
                        {
                            extentReports.CreateLog("Company: " + companyName + " country DO NOT match with the mapping sheet. ");
                        }
                    }

                    Process[] AllProcesses = Process.GetProcessesByName("EXCEL");
                    foreach(Process ExcelProcess in AllProcesses)
                    {
                        ExcelProcess.Kill();
                    }

                    //Delete File
                    tableau.DeleteFile(fileTableauReport);
                    extentReports.CreateLog("File Deleted. ");

                    usersLogin.UserLogOut();
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
}