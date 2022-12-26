using SalesForce_Project.UtilityFunctions;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using NUnit.Framework;
using SalesForce_Project.Pages.Common;

namespace SalesForce_Project.Pages.Contact
{
    class ContactSelectRecordPage : BaseClass
    {
        By drpdwnSelectRecordType = By.CssSelector("select[id='p3']");
        By btnContinue = By.CssSelector("input[title='Continue']");

        //To Select Houlihan Employee option
        public void SelectContactRecordType(string file, string contactType)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(drpdwnSelectRecordType), contactType);
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
            driver.FindElement(btnContinue).Click();
        }

        //To Select Houlihan Employee option
        public void SelectContactRecordType(string file, int row)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(drpdwnSelectRecordType), ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 4));
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue,1000);
            driver.FindElement(btnContinue).Click();
        }
        //To Click on Continue button
        public void ClickContinue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
            driver.FindElement(btnContinue).Click();
        }

        // To validate the list of contact record type
        public void ValidateContactRecordType(string file, int rows)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IWebElement recordDropdown = driver.FindElement(drpdwnSelectRecordType);
            SelectElement select = new SelectElement(recordDropdown);
            int RowContactRecordType = ReadExcelData.GetRowCount(excelPath, "Contact");
            string usersType = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", rows, 1);
            if (usersType.Equals("Admin"))
            {
                for (int i = 2; i <= RowContactRecordType; i++)
                {
                    IList<IWebElement> options = select.Options;
                    IWebElement contactTypeOption = options[i - 1];
                    Assert.AreEqual(contactTypeOption.Text, ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", i, 1));
                }
            }
            else
            {
                for (int i = 2; i <= RowContactRecordType; i++)
                {
                    IList<IWebElement> options = select.Options;
                    IWebElement contactTypeOption = options[i - 2];
                    Assert.AreEqual(contactTypeOption.Text, ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", i, 1));
                }
            }

        }
    }
}