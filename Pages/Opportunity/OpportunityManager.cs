using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Opportunity
{
    class OpportunityManager : BaseClass
    {
        By btnResetFilters = By.CssSelector("input[value='Reset Filters']");
        By comboShowRec = By.CssSelector("select[name*='opportunityOption']");
        By btnApplyFilters = By.CssSelector("input[value='Apply Filters']");
        By comboStage = By.CssSelector("select[name*=':0:j_id132:11']");
        By comboStageFVA = By.CssSelector("select[name*=':0:j_id132:9']");
        By txtPitchDate = By.CssSelector("input[name*=':0:j_id132:5']");
        By txtPitchDateFR = By.CssSelector("input[name*=':0:j_id132:3']");
        By txtRetainer = By.CssSelector("input[name*=':0:j_id132:6']");
        By txtRetainerFVA = By.CssSelector("input[name*=':0:j_id132:5']");
        By txtTxnSize = By.CssSelector("input[name*=':0:j_id132:7']");
        By txtTxnSizeFR = By.CssSelector("input[name*=':0:j_id132:4']");
        By txtContingentFee = By.CssSelector("input[name*=':0:j_id132:8']");
        By txtContingentFeeFR = By.CssSelector("input[name*=':0:j_id132:6']");
        By txtMonthlyFeeFR = By.CssSelector("input[name*=':0:j_id132:8']");
        By txtMonthlyFee = By.CssSelector("input[name*=':0:j_id132:10']");
        By txtOppComments = By.CssSelector("textarea[name*=':0:j_id132:9']");
        By txtOppCommentsFVA = By.CssSelector("textarea[name*=':0:j_id132:7']");
        By comboWinProb = By.CssSelector("select[name*=':0:j_id132:12']");
        By comboWinProbFR = By.CssSelector("select[name*=':0:j_id132:10']");
        By linkOppName = By.CssSelector("span[id*=':0:j_id132:2:j_id154'] > a");
        By linkOppNameFVA = By.CssSelector("span[id*=':0:j_id132:2:j_id154'] > a");
        By linkOppNameFR = By.CssSelector("span[id*=':0:j_id132:1:j_id154'] > a");
        By titleOppDetail = By.XPath("//h2[contains(text(),'Opportunity Detail')]");
        By valSelectedStage = By.CssSelector("select[name*=':0:j_id132:11'] > option:nth-child(2)");
        By valSelectedStageFVA = By.CssSelector("select[name*=':0:j_id132:9'] > option:nth-child(2)");
        By valSelectedWin = By.CssSelector("select[name*=':0:j_id132:12'] > option:nth-child(4)");
        By comboIG = By.CssSelector("select[name*='industryGroupOptionsOpps']");
        By btnOpportunitySort = By.CssSelector(" table[class='ft_rc ui-widget-header'] > thead > tr > th:nth-child(1) > span:nth-child(1) > span:nth-child(1)");

        //To reset filters
        public string ResetFilters(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string valOppComments = CustomFunctions.RandomValue();
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtPitchDate, 120);
            Thread.Sleep(3000);
            driver.FindElement(txtPitchDate).Clear();
            driver.FindElement(txtRetainer).Clear();
            driver.FindElement(txtTxnSize).Clear();
            driver.FindElement(txtContingentFee).Clear();
            driver.FindElement(txtOppComments).SendKeys("Comments:" + valOppComments);
            driver.FindElement(txtPitchDate).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 11));
            driver.FindElement(txtMonthlyFee).Clear();
            driver.FindElement(comboStage).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 1));
            driver.FindElement(comboWinProb).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 4));
            driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 10));
            driver.FindElement(txtTxnSize).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 6)); ;
            driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 8));
            driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 9));
            return valOppComments;
        }

        //To update Stage, Win Probability and fetch the updated value of Stage.
        public string UpdateFieldStageAndWinProb(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboShowRec, 60);            
            driver.FindElement(comboShowRec).SendKeys("CF Opportunities");
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, comboStage, 100);
            driver.FindElement(comboStage).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, comboWinProb,80);
            driver.FindElement(comboWinProb).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 5));
            string valStage = driver.FindElement(valSelectedStage).Text;
            Console.WriteLine(valStage);
            return valStage;
        }
        //To fetch the value of Win Probability
        public string GetWinProb()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSelectedWin, 80);
            string valWinProb = driver.FindElement(valSelectedWin).Text;
            return valWinProb;
        }
        //To click on Opportunity Name
        public string ClickOppName()
        {
            Thread.Sleep(3000);
            driver.FindElement(linkOppName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleOppDetail, 60);
            string title = driver.FindElement(titleOppDetail).Text;
            return title;
        }

        //To click on Opportunity Name - FVA
        public string ClickOppNameFVA()
        {
            Thread.Sleep(3000);
            driver.FindElement(linkOppNameFVA).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleOppDetail, 60);
            string title = driver.FindElement(titleOppDetail).Text;
            return title;
        }

        //To click on Opportunity Name - FR
        public string ClickOppNameFR()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkOppNameFR, 60);
            driver.FindElement(linkOppNameFR).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleOppDetail, 60);
            string title = driver.FindElement(titleOppDetail).Text;
            return title;
        }
        //To select show records as FAS/CF
        public void SelectShowRecords(string Value)
        {

            WebDriverWaits.WaitUntilEleVisible(driver, btnResetFilters, 60);
            driver.FindElement(btnResetFilters).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboShowRec, 60);
            driver.FindElement(comboShowRec).SendKeys(Value);
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(7000);
        }
        //To reset filters with LOB as FAS
        public string ResetFiltersForFAS(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string valOppComments = CustomFunctions.RandomValue();
            string excelPath = dir + file;
            driver.FindElement(comboStageFVA).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 1));
            driver.FindElement(txtOppCommentsFVA).SendKeys("Comments:" + valOppComments);
            driver.FindElement(txtRetainerFVA).Clear();
            driver.FindElement(txtRetainerFVA).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 4));
            return valOppComments;
        }
        //To update Stage and fetch the updated value of Stage.
        public string UpdateFieldStage(string file, string name)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
            WebDriverWaits.WaitUntilEleVisible(driver, comboShowRec, 60);
            driver.FindElement(comboShowRec).SendKeys(name);           
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            if (valUser.Equals("Emre Abale"))
                {
                WebDriverWaits.WaitUntilEleVisible(driver, comboStage, 100);
                driver.FindElement(comboStage).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 5));
                Thread.Sleep(3000);
                string valStage = driver.FindElement(valSelectedStage).Text;
                Console.WriteLine(valStage);
                return valStage;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboStageFVA, 100);
                driver.FindElement(comboStageFVA).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 5));
                Thread.Sleep(3000);
                string valStage = driver.FindElement(valSelectedStageFVA).Text;
                Console.WriteLine(valStage);
                return valStage;
            }
        }
        //To reset filters for LOB FR
        public string ResetFiltersForFR(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string valOppComments = CustomFunctions.RandomValue();
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtPitchDateFR, 120);
            Thread.Sleep(3000);
            driver.FindElement(txtPitchDateFR).Clear();
            driver.FindElement(txtRetainerFVA).Clear();
            driver.FindElement(txtTxnSizeFR).Clear();
            driver.FindElement(txtContingentFeeFR).Clear();
            driver.FindElement(txtOppCommentsFVA).SendKeys("Comments:" + valOppComments);
            driver.FindElement(txtPitchDateFR).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 9));
            driver.FindElement(txtMonthlyFeeFR).Clear();
            driver.FindElement(comboStageFVA).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 1));
            driver.FindElement(comboWinProbFR).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 10));
            //driver.FindElement(txtRetainerFVA).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 3));
            //Thread.Sleep(2000);
            driver.FindElement(txtTxnSizeFR).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 7));
            driver.FindElement(txtContingentFeeFR).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 8));
            driver.FindElement(txtRetainerFVA).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 3));
            driver.FindElement(txtMonthlyFeeFR).SendKeys(ReadExcelData.ReadData(excelPath, "OppManager", 4));
            return valOppComments;
        }
        //To select show records as FR
        public void SelectShowRecordsFR(string Value)
        {

            WebDriverWaits.WaitUntilEleVisible(driver, btnResetFilters, 60);
            driver.FindElement(btnResetFilters).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboShowRec, 60);
            driver.FindElement(comboShowRec).SendKeys(Value);
           // driver.FindElement(comboIG).SendKeys("BUS - Business Services");
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(7000);
        }
        //Sort the Opportunity Id column
        public void SortOppIdCol()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnOpportunitySort, 120);
            driver.FindElement(btnOpportunitySort).Click();
        }
        //Search for opportunity
        public string SearchOpportunity(string Fieldname)
        {
            try
            {
                Thread.Sleep(20000);
                Actions actions = new Actions(driver);
                actions.MoveToElement(driver.FindElement(By.XPath("//table[@class='ft_c']/tbody/tr/td/span/span/span[text()='" + Fieldname + "']")));
                actions.Perform();
                string oppID = driver.FindElement(By.XPath("//table[@class='ft_c']/tbody/tr/td/span/span/span[text()='" + Fieldname + "']")).Text;
                return oppID;
            }
            catch (Exception)
            {
                return "Opportunity does not exist";
            }
        }


    }
}


