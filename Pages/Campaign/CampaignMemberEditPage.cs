using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class CampaignMemberEditPage : BaseClass
    {
        By btnSave = By.XPath("//input[@title='Save']");
        By lblErrorMsg = By.XPath("//div[@id='errorDiv_ep']");

        By selectResponseMethod = By.XPath("//select[@id='00Ni000000D7NF1']");
        By selectStatus = By.XPath("//select[@id='Status']");
        By txtResponseComments = By.XPath("//textarea[@id='00Ni000000EiETF']");
        
        public string GetErrorMessage()
        {
            driver.FindElement(btnSave).Click();
            Thread.Sleep(2000);
            string error = driver.FindElement(lblErrorMsg).Text;
            return error;
        }

        public void AddCampaignMember(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            driver.FindElement(selectResponseMethod).SendKeys(ReadExcelData.ReadData(excelPath, "CampaignMember", 1));
            driver.FindElement(selectStatus).SendKeys(ReadExcelData.ReadData(excelPath, "CampaignMember", 2));
            driver.FindElement(txtResponseComments).SendKeys(ReadExcelData.ReadData(excelPath, "CampaignMember", 3));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
        }
    }
}
