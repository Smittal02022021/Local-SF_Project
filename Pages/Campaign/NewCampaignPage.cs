using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class NewCampaignPage : BaseClass
    {
        By valCampRecordType = By.XPath("//label[text()='Campaign Record Type']/following::td[1]");
        By txtCampaignName = By.CssSelector("input[id='cpn1']");

        By selectLOB = By.XPath("//select[@title='Lines of Business - Available']/optgroup/option");
        By selectIndustryGroup = By.XPath("//select[@title='Industry Groups - Available']/optgroup/option");
        By selectHLSubGroup = By.XPath("//select[@title='HL Sub-Group - Available']/optgroup/option");

        By linkAddLOB = By.XPath("//a[@tabindex=3]");
        By linkAddIndGrp = By.XPath("//a[@tabindex=7]");
        By linkAddHLSubGrp = By.XPath("//a[@tabindex=12]");

        By checkboxActive = By.CssSelector("input[id='cpn16']");
        By btnSave = By.XPath("//input[@title='Save']");

        //string dir = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData\";

        public string GetCampaignRecordTypeValue()
        {
            string recordType = driver.FindElement(valCampRecordType).Text;
            return recordType;
        }

        public void CreateNewParentCampaign(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Add Campaign Name
            driver.FindElement(txtCampaignName).SendKeys(ReadExcelData.ReadData(excelPath, "Campaign", 2));

            //Get LOB options Count
            IList<IWebElement> elementLOB = driver.FindElements(selectLOB);
            int lobOptionsCount = elementLOB.Count;

            //select LOB
            for (int i = 1; i <= lobOptionsCount; i++)
            {
                if(driver.FindElement(By.XPath($"//select[@title='Lines of Business - Available']/optgroup/option[{i}]")).Text == ReadExcelData.ReadData(excelPath, "Campaign", 3))
                {
                    driver.FindElement(By.XPath($"//select[@title='Lines of Business - Available']/optgroup/option[{i}]")).Click();
                    driver.FindElement(linkAddLOB).Click();
                    break;
                }
            }

            //Get Industry Group options Count
            IList<IWebElement> elementIndGrp = driver.FindElements(selectIndustryGroup);
            int indGrpOptionsCount = elementIndGrp.Count;

            //select Industry Group
            for (int j = 1; j <= indGrpOptionsCount; j++)
            {
                if (driver.FindElement(By.XPath($"//select[@title='Industry Groups - Available']/optgroup/option[{j}]")).Text == ReadExcelData.ReadData(excelPath, "Campaign", 4))
                {
                    driver.FindElement(By.XPath($"//select[@title='Industry Groups - Available']/optgroup/option[{j}]")).Click();
                    driver.FindElement(linkAddIndGrp).Click();
                    break;
                }
            }

            //Get HL Sub Group options Count
            IList<IWebElement> elementHLSubGrp = driver.FindElements(selectHLSubGroup);
            int hlSubGrpOptionsCount = elementHLSubGrp.Count;

            //select Industry Group
            for (int k = 1; k <= hlSubGrpOptionsCount; k++)
            {
                if (driver.FindElement(By.XPath($"//select[@title='HL Sub-Group - Available']/optgroup/option[{k}]")).Text == ReadExcelData.ReadData(excelPath, "Campaign", 5))
                {
                    driver.FindElement(By.XPath($"//select[@title='HL Sub-Group - Available']/optgroup/option[{k}]")).Click();
                    driver.FindElement(linkAddHLSubGrp).Click();
                    break;
                }
            }

            driver.FindElement(checkboxActive).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }
    }
}
