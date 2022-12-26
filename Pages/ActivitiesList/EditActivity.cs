using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.ActivitiesList
{
    class EditActivity : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        By btnEdit = By.CssSelector("input[value='Edit']");
        By txtDescription = By.CssSelector("textarea[name*='j_id55']");
        By btnActivitySave = By.CssSelector("td[class='pbButton '] > input:nth-child(1)");
        By btnSendNotification = By.CssSelector("input[value='Send Notification']");
        By txtEmailTemplateDescription = By.XPath("//body");
        By frame = By.XPath("//iframe[@title='Rich Text Editor, j_id0:j_id28:pbSendEmail:j_id46:Body']");
        By btnCancel = By.XPath("//input[@value='Cancel']");


        //Click Activities List Tab
        public void AddDescriptionToExistingActivity(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtDescription);
            driver.FindElement(txtDescription).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7));
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();
        }

        public void ClickSendNotificationBtn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSendNotification);
            driver.FindElement(btnSendNotification).Click();
            Thread.Sleep(3000);
        }

        public string VerifyDescription(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            driver.SwitchTo().Frame(driver.FindElement(frame));
            //Console.WriteLine(driver.FindElement(txtEmailTemplateDescription).Text);

            string str = driver.FindElement(txtEmailTemplateDescription).GetAttribute("innerText");
            string[] st= str.Split(':');
            string st2 = st[8];
            Console.WriteLine(st[8]);
            //extentReports.CreateLog(st[1]);
            //string Desc=driver.FindElement(txtDescription).Text;
            // string DescExcel = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7);
            //Assert.AreEqual(DescExcel, Desc);
            driver.SwitchTo().DefaultContent();
            return st2;
        }

        public void VerifyEmailTemplateInformation(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            driver.SwitchTo().Frame(driver.FindElement(frame));

            string str = driver.FindElement(txtEmailTemplateDescription).GetAttribute("innerText");
            string[] st = str.Split(':');
            string string1 = st[0];
            Console.WriteLine(st[0]);
            string string1exl = ReadExcelData.ReadDataMultipleRows(excelPath, "EmailTemplate", 2, 1);
            Assert.AreEqual(string1, string1exl);
            string string2 = st[1];
            Console.WriteLine(st[1]);
            string string2exl = ReadExcelData.ReadDataMultipleRows(excelPath, "EmailTemplate", 2, 2);
            Console.WriteLine(string2exl);
            Assert.AreEqual(string2, "\r\n"+string2exl);
            
            driver.SwitchTo().DefaultContent();
        }

        public void ClickCancelBtn()
        {
            driver.FindElement(btnCancel).Click();
        }

    }
}