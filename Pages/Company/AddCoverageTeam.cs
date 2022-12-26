using OpenQA.Selenium;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Company
{
    class AddCoverageTeam : BaseClass
    {
        By btnNewCoverageTeam = By.CssSelector("input[value='New Coverage Team']");
        By txtCompany = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(1) >td:nth-child(2) > div > span > input");
        By txtOfficer = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(2) >td:nth-child(2) > div > span > input");
        By comboCoverageLevel = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(3) >td:nth-child(2) > div > span > select");
        By comboType = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(4) >td:nth-child(2) > div > span > select");
        By comboPrimarySector = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(5) >td:nth-child(2) > span > span > select");
        By comboSecondarySector = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(6) >td:nth-child(2) > span > span > select");
        By comboTier = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(7) >td:nth-child(2) > div >  span > select");
        By btnSave = By.CssSelector("td[id='bottomButtonRow'] > input[name='save']");
        By btnEdit = By.CssSelector("div[id*='D7bV0_body']> table > tbody > tr > td:nth-child(1)");

       

        public void AddNewCoverageTeam(string file, int tierNumber)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            if (CustomFunctions.IsElementPresent(driver, btnNewCoverageTeam))
            {
                //Click new coverage team button
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewCoverageTeam, 40);
                driver.FindElement(btnNewCoverageTeam).Click();
            }
            // Enter company
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompany, 40);
            driver.FindElement(txtCompany).SendKeys(ReadExcelData.ReadData(excelPath, "AddCoverageTeam",  1));
            Thread.Sleep(3000);
            // Enter officer name
            WebDriverWaits.WaitUntilEleVisible(driver, txtOfficer, 40);
            driver.FindElement(txtOfficer).SendKeys(ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 2));

            // Enter coverage level
            WebDriverWaits.WaitUntilEleVisible(driver, comboCoverageLevel, 40);
            driver.FindElement(comboCoverageLevel).SendKeys(ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 3));

            // Enter coverage type
            WebDriverWaits.WaitUntilEleVisible(driver, comboType, 40);
            driver.FindElement(comboType).SendKeys(ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 4));

           // Enter Tier
            driver.FindElement(By.CssSelector($"table[class='detailList'] > tbody > tr:nth-child({tierNumber}) >td:nth-child(2) > div >  span > select")).SendKeys(ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 7));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        public void EditCoverageTeam(string file,int row)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Click new coverage team button
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            driver.FindElement(btnEdit).Click();
            
            // Enter coverage type
            WebDriverWaits.WaitUntilEleVisible(driver, comboType, 40);
            driver.FindElement(comboType).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "EditCoverageTeam", row, 1));

            //if (CustomFunctions.isAttributePresent(driver, driver.FindElement(comboPrimarySector), "disabled"))
            //{
            //    Console.WriteLine("Element is disabled");                    
            //}
            //else
            //{
            //    // Enter Primary Sector
            //    WebDriverWaits.WaitUntilEleVisible(driver, comboPrimarySector, 40);
            //    driver.FindElement(comboPrimarySector).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "EditCoverageTeam",row, 2));
            //}

            //if (CustomFunctions.isAttributePresent(driver, driver.FindElement(comboSecondarySector), "disabled"))
            //{
            //    Console.WriteLine("Element is disabled");
            //}
            //else
            //{
            //    // Enter Secondary Sector
            //    WebDriverWaits.WaitUntilEleVisible(driver, comboSecondarySector, 40);
            //    driver.FindElement(comboSecondarySector).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "EditCoverageTeam", row, 3));
            //}
            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }
    }
}
