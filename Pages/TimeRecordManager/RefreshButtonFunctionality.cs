
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.TimeRecordManager
{
    class RefreshButtonFunctionality : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();


        By txtTimeClockRecorder = By.XPath("//*[contains(@title,'Time Clock Recorder')]");
        By txtTimeClockRecorderTitle = By.XPath("//div[@class='slds-text-heading--medium']");
        By selectPrjctDropDown = By.XPath("//*[@class='slds-select']");
        By selectActivityDropDown = By.XPath("//select[@class='slds-input select uiInput uiInputSelect uiInput--default uiInput--select']");
        By btnStart = By.XPath("//button[text()='Start']");
        By btnRefresh = By.XPath("//button[text()='Refresh']");
        By txtClockTimerSecond = By.XPath("//div[@class='clock flip-clock-wrapper']/ul[5]/li[2]/a/div[1]/div[2]");
        By btnReset = By.XPath("//div[@id='existingRecordWarning']/p[4]/button[text()='Reset']");
        By btnResume = By.XPath("//button[text()='Resume']");
        By btnPause = By.XPath("//div[@class='slds-button-group slds-p-top--small']/button[2]");
        By txtTimer = By.XPath("//div[@disabled='disabled']/p/input");
        By btnUpdate = By.XPath("//div[@disabled='disabled']/p/button");
        By txtClockTimerHours = By.XPath("//div[@class='clock flip-clock-wrapper']/ul[2]/li[2]/a/div[1]/div[2]");
        By msgErrorStart = By.XPath("//div[@data-aura-class='uiMessage']/div/div[3]/span");
        By btnFinish = By.XPath("//button[text()='Finish']");

        
        // Go to TIme CLock Recorder Page
        public void GotoTimeClockRecorderPage()
        {
            Thread.Sleep(4000);
            driver.Navigate().Refresh();
            Thread.Sleep(12000);

            driver.FindElement(txtTimeClockRecorder).Click();
            Thread.Sleep(10000);
        }

        //Get the title of TIme Clock Recorder Page
        public string GetTitleTimeClockRecorderPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTimeClockRecorderTitle, 80000);
            string TimeClockRecorderPageTitle = driver.FindElement(txtTimeClockRecorderTitle).Text;
            return TimeClockRecorderPageTitle;
        }

        //Select Project and Activity Drop Down
        public void SelectDropDownProjectandActivity(string excel)
        {
         
            WebDriverWaits.WaitUntilEleVisible(driver, selectPrjctDropDown);
           
            CustomFunctions.SelectByText(driver, driver.FindElement(selectPrjctDropDown), ReadExcelData.ReadData(excel, "Project_Title", 1));
            WebDriverWaits.WaitUntilEleVisible(driver, selectActivityDropDown, 80000);

            CustomFunctions.SelectByText(driver, driver.FindElement(selectActivityDropDown),  ReadExcelData.ReadData(excel, "Project_Title", 2));

        }

        //Click Start Button

        public void ClickStartButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnStart);
            driver.FindElement(btnStart).Click();
          
        }

        //Click Refresh Button

        public void ClickRefreshButton()
        {
            Thread.Sleep(6000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnRefresh);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(10000);
        }

        //Get seconds from Timer
        public string GetSecondsTimer1()
        {

            //Time Required to check second timer
            Thread.Sleep(12000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtClockTimerSecond);
          
            string GetSecondsTimer1 = driver.FindElement(txtClockTimerSecond).Text;
            return GetSecondsTimer1;
        }

        //Click Reset Button when Displayed

        public void ClickResetButton()
        {
            try
            {

                if ((driver.FindElement(btnReset)).Displayed)
                {
                    driver.FindElement(btnReset).Click();
                    Thread.Sleep(4000);
                }
            }
            catch (Exception)
            {

            }
        }

        //Click Resume Button
        public void ClickResumeButton()
        {
           
            WebDriverWaits.WaitUntilEleVisible(driver, btnResume, 60000);
            driver.FindElement(btnResume).Click();
            Thread.Sleep(5000);


        }

        //Click Pause Button
        public void ClickPauseButton()
        {
            Thread.Sleep(8000);
          
            driver.FindElement(btnPause).Click();
          

        }

        //Update Timer
        public void UpdateTimer(string excel)
        {
            
            WebDriverWaits.WaitUntilEleVisible(driver, txtTimer, 600000);
          
            string i=ReadExcelData.ReadData(excel, "Update_Timer", 1).ToString();
            driver.FindElement(txtTimer).SendKeys(i);
         
            WebDriverWaits.WaitUntilEleVisible(driver, btnUpdate);
            driver.FindElement(btnUpdate).Click();

            Thread.Sleep(50000);

        }

        //Get Hours from Timer
        public string GetHoursTimer1()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtClockTimerHours);
            string GetHoursTimer1 = driver.FindElement(txtClockTimerHours).Text;
            return GetHoursTimer1;
        }
        //Get Error Message when project is not Selected
        public string GetErrorMessageStart()
        {           
            var errormsg = driver.FindElement(msgErrorStart);
            var errmsg= errormsg.Text;
            return errmsg;
        }

        //Start Button is clickable or not after selecting project and acitvity drop down

        public static bool ButtonStartClickable() {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(200));
              
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Start']")));
                return true;
            }
            catch (Exception e)
            {

                return false;
            }

        }
        //Check Refresh Button is not displaying
        public static bool HiddenRefreshButton()
        {
            try
            {
                if ((driver.FindElement(By.XPath("//button[text()='Refresh']"))) != null)
                {
                //    extentReports.CreateLog("Refresh button is displaying ");
                    
                }
                return false;
            }
            catch (Exception e)

            {
              //  extentReports.CreateLog("Refresh button is hidden when project is not selected ");
                return true;
            }


        }

        //Click Finish Button
        public void ClickFinishButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClockTimerHours,15000);
            Thread.Sleep(20000);
           driver.FindElement(btnFinish).Click();
            Thread.Sleep(2000);


        }



    } }