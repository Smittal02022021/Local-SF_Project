using OpenQA.Selenium;
using SalesForce_Project.UtilityFunctions;
using System.Threading;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

namespace SalesForce_Project.Pages.Common
{
    class MonthlyRevenue : BaseClass
    {
        By dropDownView = By.XPath("//select[@id='fcf']");
        By btnGo = By.XPath("//input[@title='Go!']");
        By colIsCurrent = By.XPath("//td[@class='x-grid3-hd x-grid3-cell x-grid3-td-00Ni000000FnLRS ASC']");
        By imgIsCurrent = By.XPath("//div[@class='x-grid3-cell-inner x-grid3-col-00Ni000000FnLRS']/img");
        By linkMonthlyRevenueControlName = By.XPath("//div[@id='a1r6e000004Sj9R_Name']/a/span");
        By LOBColLength = By.XPath("//div[@id='a1r6e000004Sj9R_00N3100000GbhhF_body']/table/tbody/tr");
        By lblLegacyPeriod = By.XPath("//td[text()='Legacy Period Accrued Fees']");
        By lnkBackToList = By.XPath("//a[text()='Back to List: Monthly Revenue Process Controls']");

        public void SelectMonthlyRevenueProcessControlView()
        {
            if (driver.FindElement(btnGo).Displayed)
            {
                driver.FindElement(dropDownView).SendKeys("All Monthly Revenue Process Controls");
                Thread.Sleep(2000);
            }
            else
            {
                driver.FindElement(dropDownView).SendKeys("All Monthly Revenue Process Controls");
                Thread.Sleep(2000);
            }
        }

        public void SortDataAndGetToCurrentMonthRevenuePage()
        {
            string attValue = driver.FindElement(imgIsCurrent).GetAttribute("alt");
            if (attValue == "Not Checked")
            {
                driver.FindElement(colIsCurrent).Click();
                Thread.Sleep(2000);
                driver.FindElement(linkMonthlyRevenueControlName).Click();
            }
            else
            {
                driver.FindElement(linkMonthlyRevenueControlName).Click();
                Thread.Sleep(2000);
            }
        }

        public void GetToRevenueAccrualsPage(string lob)
        {
            IList<IWebElement> element = driver.FindElements(LOBColLength);
            int totalRows = element.Count;
            for (int i = 2; i <= totalRows; i++)
            {
                By xyz = By.XPath($"//div[@id='a1r6e000004Sj9R_00N3100000GbhhF_body']/table/tbody/tr[{i}]/td[2]");
                IWebElement LOBElement = driver.FindElement(xyz);

                string lobName = LOBElement.Text;
                if (lobName.Equals(lob))
                {
                    Console.WriteLine("LOB Name Matches");
                    By linkAccrualNo = By.XPath($"//div[@id='a1r6e000004Sj9R_00N3100000GbhhF_body']/table/tbody/tr[{i}]/th/a");
                    IWebElement linkAccrualNoElement = driver.FindElement(linkAccrualNo);
                    Thread.Sleep(1000);
                    linkAccrualNoElement.Click();
                    Thread.Sleep(3000);
                    break;
                }
            }
        }

        public string ValidateIfLegacyPeriodAccruedFeesExist()
        {
            if (driver.FindElement(lblLegacyPeriod).Displayed)
            {
                return "Legacy field is displayed. ";
            }
            else
            {
                return "Legacy field is not displayed. ";
            }
        }

        public void ClickBackToRevenueMonthList()
        {
            driver.FindElement(lnkBackToList).Click();
            Thread.Sleep(2000);
        }
    }
}


