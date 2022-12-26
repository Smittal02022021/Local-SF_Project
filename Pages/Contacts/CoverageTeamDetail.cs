using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Contact
{
    class CoverageTeamDetail : BaseClass
    {
        By coverageTeamDetailHeading = By.CssSelector("h2[class='mainTitle']");
        By offsiteTemplateEditHeading = By.CssSelector("h2[class='mainTitle']");
        By btnNewOffsiteTemplate = By.CssSelector("input[value='New Offsite Template']");
        By txtCurrentYearStrategy = By.CssSelector("textarea[id*='GbCS3']");
        By txtPotentialRevenue = By.CssSelector("input[id*='GbCSD']");
        By txtRevenueComment = By.CssSelector("textarea[id*='GbCSF']");
        By txtNumberOfDeals = By.CssSelector("textarea[id*='GbCSB']");
        By txtRelationshipMetricsComments = By.CssSelector("textarea[id*='GbCSE']");
        By txtFaceToFace = By.CssSelector("input[id*='GbCS7']");
        By txtTimeSpent = By.CssSelector("input[id*='GbCSK']");
        By txtOtherProposal = By.CssSelector("input[id*='GbCSC']");
        By txtFundName = By.CssSelector("input[id*='GbCS8']");
        By btnCancel = By.CssSelector("td[id='bottomButtonRow'] > input[value='Cancel']");
        By btnSave = By.CssSelector("td[id='bottomButtonRow'] > input[value=' Save ']");
        By OffsiteTemplateRecords = By.CssSelector("div[id*='offsiteTemplateList_body'] > table > tbody > tr:nth-child(2)");
        By offsiteTemplateDetailHeading = By.CssSelector("h2[class='mainTitle']");
        By valCurrentYearStrategy = By.CssSelector("div[id*='00N3100000GbCS3']");
        By valRevenueComments = By.CssSelector("div[id*='00N3100000GbCSF']");
        By valPotentialRevenue = By.CssSelector("div[id*='GbCSD']");
        By valRelationshipMetricsComments = By.CssSelector("div[id*='GbCSE']");
        By valFundName = By.CssSelector("div[id*='GbCS8']");
        By lnkDelete = By.CssSelector("td[class='actionColumn'] > a:nth-child(2)");

     
        //Get heading of coverage team details page
        public string GetCoverageTeamDetailsHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, coverageTeamDetailHeading, 60);
            string headingCoverageTeamDetail = driver.FindElement(coverageTeamDetailHeading).Text;
            return headingCoverageTeamDetail;
        }
        //Click on new offsite template button
        public void ClickNewOffsiteTemplateButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOffsiteTemplate);
            driver.FindElement(btnNewOffsiteTemplate).Click();
        }

        //Get heading of coverage team details page
        public string GetOffsiteTemplateHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, offsiteTemplateEditHeading, 60);
            string headingOffsiteTemplateEdit = driver.FindElement(offsiteTemplateEditHeading).Text;
            return headingOffsiteTemplateEdit;
        }

        //Enter offsite template details function
        public void EnterOffsiteTemplateDetails(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtCurrentYearStrategy, 40);
            driver.FindElement(txtCurrentYearStrategy).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 3));

            WebDriverWaits.WaitUntilEleVisible(driver, txtPotentialRevenue, 40);
            driver.FindElement(txtPotentialRevenue).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 4));

            WebDriverWaits.WaitUntilEleVisible(driver, txtRevenueComment, 40);
            driver.FindElement(txtRevenueComment).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 5));

            WebDriverWaits.WaitUntilEleVisible(driver, txtNumberOfDeals, 40);
            driver.FindElement(txtNumberOfDeals).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 6));

            WebDriverWaits.WaitUntilEleVisible(driver, txtRelationshipMetricsComments, 40);
            driver.FindElement(txtRelationshipMetricsComments).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 7));

            WebDriverWaits.WaitUntilEleVisible(driver, txtFaceToFace, 40);
            driver.FindElement(txtFaceToFace).Clear();
            driver.FindElement(txtFaceToFace).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 8));

            WebDriverWaits.WaitUntilEleVisible(driver, txtTimeSpent, 40);
            driver.FindElement(txtTimeSpent).Clear();
            driver.FindElement(txtTimeSpent).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 9));

            WebDriverWaits.WaitUntilEleVisible(driver, txtOtherProposal, 40);
            driver.FindElement(txtOtherProposal).Clear();
            driver.FindElement(txtOtherProposal).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 10));

            WebDriverWaits.WaitUntilEleVisible(driver, txtFundName, 40);
            driver.FindElement(txtFundName).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 11));
        }

        // Click cancel button function
        public void ClickCancel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();
        }

        public bool ValidateOffsiteTemplateCreation()
        {
            return CustomFunctions.IsElementPresent(driver, OffsiteTemplateRecords);
        }

        // Click save button function
        public void ClickSave()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        // Get heading of offsite template detail page
        public string GetOffsiteTemplateDetailHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, offsiteTemplateDetailHeading, 60);
            string headingOffsiteTemplateDetail = driver.FindElement(offsiteTemplateDetailHeading).Text;
            return headingOffsiteTemplateDetail;
        }

        //Get value of current year strategy from offsite detail page
        public string GetCurrentYearStrategyValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCurrentYearStrategy, 60);
            string valueCurrentYearStr = driver.FindElement(valCurrentYearStrategy).Text;
            return valueCurrentYearStr;
        }

        //Get value of current year strategy from offsite detail page
        public string GetRevenueCommentValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueComments, 60);
            string valueRevenueComments = driver.FindElement(valRevenueComments).Text;
            return valueRevenueComments;
        }

        //Function to get relationship metrics comments
        public string GetRelationshipMetricsComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRelationshipMetricsComments, 60);
            string relationshipMetricsComments = driver.FindElement(valRelationshipMetricsComments).Text;
            return relationshipMetricsComments;
        }

        //Function to get fund name
        public string GetFundName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valFundName, 60);
            string valueFundName = driver.FindElement(valFundName).Text;
            return valueFundName;
        }

        //Delete offsite templates
        public void DeleteOffsiteTemplate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelete);
            driver.FindElement(lnkDelete).Click();
            Thread.Sleep(1000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(1000);
        }
    }
}
