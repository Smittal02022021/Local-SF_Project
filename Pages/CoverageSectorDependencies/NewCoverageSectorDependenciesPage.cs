using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class NewCoverageSectorDependenciesPage : BaseClass
    {
        By txtCoverageType = By.CssSelector("input[id='CF00N6e00000MRMtk']");
        By txtPrimarySector = By.CssSelector("input[id='CF00N6e00000MRMtl']");
        By txtSecondarySector = By.CssSelector("input[id='CF00N6e00000MRMtm']");
        By txtTertiarySector = By.CssSelector("input[id='CF00N6e00000MRMtn']");
        By btnSave = By.XPath("//input[@title='Save']");

        public void CreateNewCoverageSectorDependency(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Add Coverage Type
            driver.FindElement(txtCoverageType).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 1));

            //Add Primary Sector
            driver.FindElement(txtPrimarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 2));

            //Add Secondary Sector
            driver.FindElement(txtSecondarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 3));

            //Add Tertiary Sector
            driver.FindElement(txtTertiarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 4));

            //Click on Save button
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
        }
    }
}
