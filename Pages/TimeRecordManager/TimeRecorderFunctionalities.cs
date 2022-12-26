using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.TimeRecordManager
{
    class TimeRecorderFunctionalities : BaseClass
    {
     
        ExtentReport extentReports = new ExtentReport();

        By tabDetailLogs = By.CssSelector("li[id*='view'] > a");
       
        By txtSummaryLogHour = By.XPath("//tr[@class='slds-hint-parent']/td[4]/span");
        By txtDetailLogHour= By.XPath("//tr[@class='slds-hint-parent']/td[4]/div/input");

       
        // Edit the hours in Weekly Entry matrix
        public string EditWeeklyEntryMatrix()
        {


            DateTime Time = DateTime.Now;
            string format = "ddd";
            string week = Time.ToString(format);
            Console.WriteLine(Time.ToString(format));

            switch (Time.ToString(format))
            {
                case "Mon":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).Clear();
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).SendKeys("1");
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[text()='Weekly Entry Matrix']")).Click();
                    Thread.Sleep(20000);
                    driver.Navigate().Refresh();
                    Thread.Sleep(30000);
                    break;

                case "Tue":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).Clear();
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).SendKeys("1");
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[text()='Weekly Entry Matrix']")).Click();
                    Thread.Sleep(20000);
                    driver.Navigate().Refresh();
                    Thread.Sleep(30000);
                    break;

                case "Wed":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).Clear();
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).SendKeys("1");
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[text()='Weekly Entry Matrix']")).Click();
                    Thread.Sleep(20000);
                    driver.Navigate().Refresh();
                    Thread.Sleep(30000);
                    break;


                case "Thu":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).Clear();
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).SendKeys("1");
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[text()='Weekly Entry Matrix']")).Click();
                    Thread.Sleep(20000);
                    driver.Navigate().Refresh();
                    Thread.Sleep(30000);
                    break;


                case "Fri":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).Clear();
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).SendKeys("1");
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[text()='Weekly Entry Matrix']")).Click();
                    Thread.Sleep(20000);
                    driver.Navigate().Refresh();
                    Thread.Sleep(30000);
                    break;


                case "Sat":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).Clear();
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).SendKeys("1");
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[text()='Weekly Entry Matrix']")).Click();
                    Thread.Sleep(20000);
                    driver.Navigate().Refresh();
                    Thread.Sleep(30000);
                    break;

                case "Sun":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).Clear();
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).SendKeys("1");
                    Thread.Sleep(10000);
                    driver.FindElement(By.XPath("//*[text()='Weekly Entry Matrix']")).Click();
                    Thread.Sleep(20000);
                    driver.Navigate().Refresh();
                    Thread.Sleep(30000);
                    break;
            }

                    //for(int i = 2; i<= 8; i++)
                    //{
                    //if (driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td["+i+ "]/div[1]/div/div/div[1]/input[1]")).GetAttribute("value") == "2"){
                    //        driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[" + i + "]/div[1]/div/div/div[1]/input[1]")).Clear();
                    //        Thread.Sleep(10000);
                    //        driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[" + i + "]/div[1]/div/div/div[1]/input[1]")).SendKeys("1");
                    //        Thread.Sleep(10000);
                    //        driver.FindElement(By.XPath("//*[text()='Weekly Entry Matrix']")).Click();
                    //        Thread.Sleep(20000);
                    //        driver.Navigate().Refresh();
                    //        Thread.Sleep(30000);
                    //    }

                                                         
   return week;
            
        
        }
        //Edit the hours in Detail Log Tab
        public void EditDetailsLog()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDetailLogHour);

            //    WebDriverWaits.WaitUntilEleVisible(driver, txttimeclockrecorder);
            driver.FindElement(txtDetailLogHour).Clear();
            Thread.Sleep(1000);
            driver.FindElement(txtDetailLogHour).SendKeys("2");
            Thread.Sleep(1000);
            driver.FindElement(tabDetailLogs).Click();
            driver.Navigate().Refresh();

            Thread.Sleep(10000);

        }
        //Go to Detail Log
        public void GoToDetailLogs()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabDetailLogs);
            driver.FindElement(tabDetailLogs).Click();
            Thread.Sleep(1000);

        }

        //Get the hours from Summary Log Tab
        public string GetSummaryLogsTimeHour()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogHour, 80);
            string SummaryLogEntryHour = driver.FindElement(txtSummaryLogHour).Text;
            return SummaryLogEntryHour;
        }


        //Get the hours from Detail Log Tab
        public string GetDetailLogsTimeHour()
        {
           
            WebDriverWaits.WaitUntilEleVisible(driver, txtDetailLogHour, 80);
            string SummaryLogEntryHour = driver.FindElement(txtDetailLogHour).GetAttribute("value");
            return SummaryLogEntryHour;
        }
    }

        
}