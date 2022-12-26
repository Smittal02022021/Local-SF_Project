using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;

namespace SalesForce_Project.Pages.Company
{
    class CompanySelectRecordPage : BaseClass
    {
        By headingCompanyRecordType = By.CssSelector("h2[class='pageDescription']");
        By drpdwnSelectRecordType = By.CssSelector("select[id='p3']");
        By btnContinue = By.CssSelector("input[title='Continue']");
        By drpdwnCompanyRecordType = By.CssSelector("select[id='p3']");
        By btnCancel = By.CssSelector("input[title='Cancel']");


        // Get company record type page heading
        public string GetCompanyRecordTypePageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headingCompanyRecordType, 60);
            string headingCompanyRecordTypePage = driver.FindElement(headingCompanyRecordType).Text;
            return headingCompanyRecordTypePage;
        }

        public void SelectCompanyRecordType(string file,string recordType)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(drpdwnSelectRecordType), recordType);
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
            driver.FindElement(btnContinue).Click();
        }

        //Verify Company reocrd types and description
        public void VerifyCompanyRecordTypesandDesc(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IWebElement recordDropdown = driver.FindElement(drpdwnCompanyRecordType);
            SelectElement select = new SelectElement(recordDropdown);
            int CompanyRecordList = ReadExcelData.GetRowCount(excelPath, "CompanyRecordTypes");

            for (int row = 2; row <= CompanyRecordList; row++)
            {
                IList<IWebElement> options = select.Options;
                IWebElement companyRecordTypeOption = options[row - 2];
                string companyRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyRecordTypes", row, 1);

                IWebElement tableCompanyRecordType = driver.FindElement(By.CssSelector("table[class='infoTable recordTypeInfo']>tbody>tr:nth-of-type("+row+")>th"));
                Assert.AreEqual(companyRecordTypeOption.Text, companyRecordTypeExl);
                Assert.AreEqual(tableCompanyRecordType.Text, companyRecordTypeExl);

                IWebElement tableCompanyRecordDesc = driver.FindElement(By.CssSelector("table[class='infoTable recordTypeInfo']>tbody>tr:nth-of-type(" + row + ")>td"));
                string companyRecordDesExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyRecordTypes", row, 2);
                Assert.AreEqual(tableCompanyRecordDesc.Text, companyRecordDesExl);

            }
        }

        //Verify continue and cancel button on company record page
            public void VerifyContinueCancelBtnDisplay() {
               bool res=driver.FindElement(btnContinue).Displayed;
            Assert.IsTrue(res);
            bool res1 = driver.FindElement(btnCancel).Displayed;
            Assert.IsTrue(res1);
        }
        }
    }
