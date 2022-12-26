using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;
using Sikuli4Net.sikuli_REST;
using Sikuli4Net.sikuli_UTIL;

namespace SalesForce_Project.Pages.Tableau
{
    class TableauPage : BaseClass
    {
        //ExtentReport extentReports = new ExtentReport();
        By txtUsername = By.XPath("//input[@name='username']");
        By txtPassword = By.XPath("//input[@tb-test-id='textbox-password-input']");
        By btnSignIn = By.XPath("//button[@tb-test-id='button-signin']");

        By dropDownRegion = By.XPath("(//span[text()='(All)'])[12]");
        By checkBoxAllRegion = By.XPath("//a[@title='(All)']/preceding-sibling::input");
        By btnApply = By.XPath("//span[text()='Apply']");
        By frame = By.XPath("//iframe[@title='Data Visualization']");
        By lnkDownload = By.XPath("//div[@title='Download Crosstab']/div");
        By btnDownload = By.XPath("//button[text()='Download']");

        By lblCompanyCountry = By.XPath("//div[@id='00Ni000000DvFsEj_id0_j_id1_ileinner']");

        string dir = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData\";

        Screen screen = new Screen();

        public void LoginintoTableau()
        {
            driver.Navigate().GoToUrl("https://tabdata.hl.com/#/signin?redirect=%2Fsite%2FTEST%2Fviews%2FFSCSponsorDashboardSprint9%2FSponsorReport%3F:iid%3D1&error=42&disableAutoSignin=yes");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtUsername);
            driver.FindElement(txtUsername).SendKeys("Dtaloka0817@hl.com");
            driver.FindElement(txtPassword).SendKeys("Smiles@0222");
            driver.FindElement(btnSignIn).Click();
            Thread.Sleep(2000);
        }

        public void TestLogin(string userNa, string pwd, string login, string emp, string statusVal)
        {
            APILauncher launcher = new APILauncher(true);
            launcher.Start();
            string excelPath = dir + userNa;
            string excelPath1 = dir + pwd;
            string excelPath2 = dir + login;
            string excelPath3 = dir + emp;
            string excelPath4 = dir + statusVal;

            Pattern username = new Pattern(excelPath);
            Pattern password = new Pattern(excelPath1);
            Pattern log = new Pattern(excelPath2);
            Pattern empState = new Pattern(excelPath3);
            Pattern statusValue = new Pattern(excelPath4);


            screen.Wait(username, 10);
            screen.Type(username, "Admin");
            screen.Type(password, "admin123");
            screen.Click(log);
            Thread.Sleep(5000);
            screen.Click(empState);
            Thread.Sleep(5000);
        }

        public void SelectRegion(string file, int row)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, frame);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='Data Visualization']")));
            Thread.Sleep(20000);

            string excelPath = dir + file;
            string regionName = ReadExcelData.ReadDataMultipleRows(excelPath, "Sheet1", row, 1);

            driver.FindElement(dropDownRegion).Click();
            driver.FindElement(checkBoxAllRegion).Click();
            driver.FindElement(By.XPath($"(//a[contains(@title, '{regionName}')]/preceding-sibling::input)")).Click();
            driver.FindElement(btnApply).Click();
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
        }

        public void DownloadExcel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, frame);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='Data Visualization']")));
            Thread.Sleep(20000);

            driver.FindElement(lnkDownload).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnDownload);
            driver.FindElement(btnDownload).Click();
            Thread.Sleep(10000);
        }

        public string ValidateFileName(string fileNam)
        {
            string excelPath = dir + fileNam;
            FileInfo fileInfo = new FileInfo(excelPath);
            string fileName = fileInfo.Name;

            return fileName;
        }

        public void DeleteFile(string fileNam)
        {
            Thread.Sleep(60000);
            string excelPath = dir + fileNam;
            FileInfo fileInfo1 = new FileInfo(excelPath);
            fileInfo1.Delete();
        }

        public bool ValidateFileExists(string fileNam)
        {
            string excelPath = dir + fileNam;
            if (File.Exists(excelPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void OpenSF()
        {
            driver.Navigate().GoToUrl("https://hl--test.my.salesforce.com/");
            WebDriverWaits.WaitForPageToLoad(driver, 60);
        }

        public ArrayList GetStateCountryNameFromMappingSheet(string file1, string file2, int row2)
        {
            string excelPath = dir + file1;
            string regionName = ReadExcelData.ReadDataMultipleRows(excelPath, "Sheet1", row2, 1);

            string excelPath1 = dir + file2;
            int mappingRowCount = ReadExcelData.GetRowCount(excelPath1, "Region_Mapping-forTB");

            ArrayList arlist = new ArrayList();

            for (int mappingRow = 2; mappingRow <= mappingRowCount; mappingRow++)
            {
                string mappingRegion = ReadExcelData.ReadDataMultipleRows(excelPath1, "Region_Mapping-forTB", mappingRow, 2);
                
                if (regionName == mappingRegion)
                {
                    string mappingStateCountry = ReadExcelData.ReadDataMultipleRows(excelPath1, "Region_Mapping-forTB", mappingRow, 1);
                    arlist.Add(mappingStateCountry);
                    continue;
                }
            }
            return arlist;
        }

        public bool MatchCompanyCountryBetweenSFAndMappingSheet(ArrayList countryName)
        {
            string companyCountryInSF = driver.FindElement(lblCompanyCountry).Text;

            int len = countryName.Count;
            bool result = false;

            for (int x=0; x<len; x++)
            {
                if (companyCountryInSF.Contains(Convert.ToString(countryName[x])))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}