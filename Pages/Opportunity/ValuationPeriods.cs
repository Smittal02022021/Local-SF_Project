using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.Pages.Opportunity
{
    class ValuationPeriods : BaseClass
    {
        By btnNewOppValPeriod = By.CssSelector("input[value='New Opportunity Valuation Period']");
        By lblNewOppValPeriod = By.CssSelector("h2[class='pageDescription']");
        By txtName = By.CssSelector("input[name*='id30']");
        By comboFrequency = By.CssSelector("select[name*='id31']");
        By txtSummary = By.CssSelector("textarea[name*='id32']");
        By lnkValDate = By.CssSelector("tbody > tr:nth-child(3) > td:nth-child(2) > div > span > span > a");
        By btnSave = By.CssSelector("input[value='Save']");
        By lblOppValPeriodDetail1 = By.CssSelector("h2[class='pageDescription']");
        By lblOppValPeriodDetail = By.CssSelector("h2[class='mainTitle']");
        By btnNewOppValPeriodPosition = By.CssSelector("input[value='New Opp Valuation Period Position']");
        By btnCompany = By.CssSelector("span[class='lookupInput']>input[id*='CompanyField']");
        By comboAssetClasses = By.CssSelector("select[name*='id79']");
        By comboUpdAssetClasses = By.CssSelector("select[name*='id38']");
        By txtNotes = By.CssSelector("textarea[name*='id86']");
        By txtReportFee = By.CssSelector("input[name*='id85']");
        By txtUpdReportFee = By.CssSelector("input[name*='id40']");
        By comboPositionIG = By.CssSelector("select[name*='PositionIG']");
        By comboPositionSector = By.CssSelector("select[name*='PositionS']");
        By btnCancel = By.CssSelector("input[value='Cancel']");
        By msgPeriodSection = By.CssSelector("span[id*='RelatedRecordPanelId']>label");
        By msgError = By.CssSelector("div[class='message errorM3']>table>tbody>tr>td[class='messageCell']>div");
        By btnImportPositions = By.CssSelector("input[value='Import Positions']");
        By lnkEdit = By.CssSelector("tbody[id*='j_id94:pbtableId2:tb']>tr>td>a");
        By valName = By.CssSelector("td[id*='id134']>a");
        By valComp = By.CssSelector("td[id*='id137']>span>a");
        By valIG = By.CssSelector("td[id*='id138']>span");
        By valAsset = By.CssSelector("td[id*='id142']>span");
        By valFee = By.CssSelector("td[id*='id143']>span");
        By valStatus = By.CssSelector("td[id*='id144']>span");
        By lblValPeriodPosition = By.CssSelector("h1[class='pageType']");
        By btnBackToValPeriod = By.CssSelector("input[name*='BtnsId:Onbacktovp']");
        By lnkEditPosition = By.CssSelector("a[name*='id128']>font");
        By lnkDel = By.CssSelector("a[name*='id132']>font");
        By btnAddTeamMember = By.CssSelector("input[value='Add New Team Member']");
        By btnSaveTeamMember = By.CssSelector("input[value='Save Team Members']");
        By btnBackToOppValPeriodList = By.CssSelector("input[value='Back To Opp Valuation Period List']");
        By radioValPeriod = By.CssSelector("input[name*='myselection']");
        By btnSearchValPeriodPosition = By.CssSelector("input[value='Search Valuation Period for Positions']");
        By radioImportPositionsWithoutTM = By.CssSelector("input[value='Import Positions Without Team Members']");
        By checkPosition = By.CssSelector("input[id*='myCheckbox']");
        By btnSaveAndBack = By.CssSelector("input[id*='139']");
        By msgTeamMembers = By.CssSelector("div[class='messageText']");
        By valPeriod = By.CssSelector("td[id*=':j_id82'] > a");
        By valStaff1st = By.CssSelector("td[id*='id174']>select>option");
        By valStaff = By.CssSelector("td[id*='id174']");
        By valRole = By.CssSelector("td[id*='id179']");
        By btnBack = By.CssSelector("input[value*='Back']");
        By msgNoValPeriod = By.CssSelector("div[id*='id40']");
        By btnBackToOpp = By.CssSelector("input[value*='Back To Opportunity']");
        By titleOppDetails = By.CssSelector("div[id*='id55'] > div.pbHeader > table > tbody > tr > td.pbTitle > h2");
        By titleValPeriods = By.CssSelector("div>font>b");
        By txtVPName = By.CssSelector("td[id *= 'id82'] > a");
        By valVPDate = By.CssSelector("td[id*='id87']>span");
        By valVPName = By.CssSelector("td[id *= 'id82']");

        public string ClickOppValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriod, 60);
            driver.FindElement(btnNewOppValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblNewOppValPeriod, 60);
            string title = driver.FindElement(lblNewOppValPeriod).Text;
            return title;
        }

        //Get No Valuation Periods message
        public string GetNoValuationPeriods()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgNoValPeriod, 70);
            string message = driver.FindElement(msgNoValPeriod).Text;
            return message;
        }

        //Validate Back To Opportunity button 
        public string ValidateBackToOpportunityButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOpp, 120);
            string value = driver.FindElement(btnBackToOpp).Displayed.ToString();
            Console.WriteLine(value);
            if (value.Equals("True"))
            {
                return "Back To Opportunity";
            }
            else
            {
                return "No Back To Opportunity";
            }
        }

        //Click Back to Opp button and get title of page
        public string ClickBackToOppAndValidatePageTitle()
        {
            driver.FindElement(btnBackToOpp).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleOppDetails, 70);
            string title = driver.FindElement(titleOppDetails).Text;
            return title;
        }

        //Enter all details and save it.
        public string EnterAndSaveOppValuationDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string Name = CustomFunctions.RandomValue();
            driver.FindElement(txtName).SendKeys(Name);
            //driver.FindElement(comboFrequency).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 2));
            //driver.FindElement(txtSummary).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 3));
            driver.FindElement(lnkValDate).Click();
            driver.FindElement(btnSave).Click();
            return Name;
        }

        public string GetOppValPeriodDetailTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions, 60);
            string title = driver.FindElement(lblOppValPeriodDetail1).Text;
            return title;
        }

        //Click on Back To Opp Valuation Period List button and validate title of Page
        public string ClickBackToOppValAndGetTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOppValPeriodList, 60);
            driver.FindElement(btnBackToOppValPeriodList).Click();
            string title = driver.FindElement(titleValPeriods).Text;
            return title;
        }

        //Get added VP Name
        public string GetVPName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtVPName, 60);
            string value = driver.FindElement(txtVPName).Text;
            return value;
        }


        //Get added Valuation Date
        public string GetValuationDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valVPDate, 60);
            string value = driver.FindElement(valVPDate).Text;
            return value;
        }

        //Click on added added Valuation Period
        public void ClickOnAddedValuation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtVPName, 60);
            driver.FindElement(txtVPName).Click();
        }

        //Enter Opp Valuation Period Position details and click cancel button
        public string EnterPeriodPositionDetailsAndClickCancel(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriodPosition, 60);
            driver.FindElement(btnNewOppValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(btnCompany).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4));
            driver.FindElement(comboAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 5));
            //driver.FindElement(txtNotes).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 6));
            //driver.FindElement(txtReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 7));            
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriodPosition, 60);
            string value = driver.FindElement(msgPeriodSection).Text.ToString();
            return value;
        }

        //Validate error message when Asset Classes is selected as Other
        public string ValidateErrorMessage(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriodPosition, 60);
            driver.FindElement(btnNewOppValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(btnCompany).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4));
            driver.FindElement(comboAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 5));
            driver.FindElement(comboPositionIG).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 8));
            driver.FindElement(comboPositionSector).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 9));
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgError, 80);
            string message = driver.FindElement(msgError).Text.ToString();
            return message;
        }

        //Enter all Opp Valuation Period Position details
        public bool EnterValPeriodPositionDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            //driver.FindElement(txtNotes).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 6));
            //driver.FindElement(txtReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 7));
            driver.FindElement(comboAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 13));
            driver.FindElement(comboPositionIG).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 8));
            driver.FindElement(comboPositionSector).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 9));
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriodPosition, 60);
            bool value = driver.FindElement(lnkEdit).Displayed;
            return value;
        }

        //Get value of Name 
        public string GetName()
        {
            string name = driver.FindElement(valName).Text.ToString();
            return name;
        }
        //Get value of Company 
        public string GetCompanyName()
        {
            string compName = driver.FindElement(valComp).Text.ToString();
            return compName;
        }
        //Get value of IG 
        public string GetIG()
        {
            string industryGroup = driver.FindElement(valIG).Text.ToString();
            return industryGroup;
        }
        //Get value of Assert 
        public string GetAsset()
        {
            string asset = driver.FindElement(valAsset).Text.ToString();
            return asset;
        }
        //Get value of Report Fee 
        public string GetFee()
        {
            string fee = driver.FindElement(valFee).Text.ToString();
            return fee;
        }
        //Get value of Status
        public string GetStatus()
        {
            string status = driver.FindElement(valStatus).Text.ToString();
            return status;
        }
        //Validate Opportunity Valuation Position Detail page title
        public string ValidateOppValPositionDetailTitle()
        {
            driver.FindElement(valName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblValPeriodPosition, 60);
            string titleValPosition = driver.FindElement(lblValPeriodPosition).Text.ToString();
            return titleValPosition;
        }
        //Validate Opportunity Valuation Position Detail page title
        public string ValidateOppValPeriodDetailTitle()
        {
            driver.FindElement(btnBackToValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblOppValPeriodDetail, 60);
            string titlePeriodDetail = driver.FindElement(lblOppValPeriodDetail).Text.ToString();
            return titlePeriodDetail;
        }

        //To Update Opp Valuation Period Position details
        public void UpdateOppValPeriodPositionDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(lnkEditPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(comboUpdAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 10));
            //driver.FindElement(txtUpdReportFee).Clear();
            //driver.FindElement(txtUpdReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 11));
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToValPeriod, 60);
            driver.FindElement(btnBackToValPeriod).Click();
        }
        //Click delete link and validate record in Opp Valuation Period Position
        public string ValidateRecAfterDelete()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 60);
            try
            {
                string lnkDisplayed = driver.FindElement(lnkDel).Displayed.ToString();
                Console.WriteLine("Delete link is displayed");
                driver.FindElement(lnkDel).Click();
                Thread.Sleep(2000);
                driver.SwitchTo().Alert().Accept();
                WebDriverWaits.WaitUntilEleVisible(driver, msgPeriodSection, 80);
                string value = driver.FindElement(msgPeriodSection).Text.ToString();
                return value;
            }
            catch (Exception)
            {
                return "Delete link is not displayed";
            }
        }
        //Click on added Valuation Period
        public void ClickAddedValPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPeriod, 80);
            driver.FindElement(valPeriod).Click();
        }

        //To add team members and save it. 
        public void ClickPositionAndSaveTeamMembers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valName, 60);
            driver.FindElement(valName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddTeamMember, 80);
            driver.FindElement(btnAddTeamMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamMember, 80);
            driver.FindElement(btnSaveTeamMember).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnBackToValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOppValPeriodList, 80);
            driver.FindElement(btnBackToOppValPeriodList).Click();
        }
        //To get Staff Name 
        public string GetStaffDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valName, 60);
            driver.FindElement(valName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddTeamMember, 80);
            driver.FindElement(btnAddTeamMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valStaff1st, 80);
            string valStaffName = driver.FindElement(valStaff1st).Text;
            return valStaffName;
        }
        //To get Staff Name 
        public string ValidateStaffName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStaff, 80);
            string valStaffName = driver.FindElement(valStaff).Text;
            return valStaffName;
        }

        //To get role. 
        public string GetRole()
        {
            string valueRole = driver.FindElement(valRole).Text;
            return valueRole;
        }
        //To save team members
        public void SaveTeamMembers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamMember, 80);
            driver.FindElement(btnSaveTeamMember).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnBackToValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOppValPeriodList, 80);
            driver.FindElement(btnBackToOppValPeriodList).Click();
        }
        //To import positions without team members
        public string ImportPositionsWithoutTM()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions, 100);
            driver.FindElement(btnImportPositions).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, radioValPeriod, 60);
            driver.FindElement(radioValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearchValPeriodPosition, 60);
            driver.FindElement(btnSearchValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, radioImportPositionsWithoutTM, 60);
            driver.FindElement(radioImportPositionsWithoutTM).Click();
            driver.FindElement(btnSave).Click();
            //Accept the alert pop up
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, checkPosition, 60);
            driver.FindElement(checkPosition).Click();
            driver.FindElement(btnSaveAndBack).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblOppValPeriodDetail, 80);
            string title = driver.FindElement(lblOppValPeriodDetail).Text;
            return title;
        }
        //To import positions without team members
        public string ImportPositionsWithTM()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions, 100);
            driver.FindElement(btnImportPositions).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, radioValPeriod, 60);
            driver.FindElement(radioValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearchValPeriodPosition, 60);
            driver.FindElement(btnSearchValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(btnSave).Click();
            //Accept the alert pop up
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, checkPosition, 60);
            driver.FindElement(checkPosition).Click();
            driver.FindElement(btnSaveAndBack).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblOppValPeriodDetail, 80);
            string title = driver.FindElement(lblOppValPeriodDetail).Text;
            return title;
        }

        //To get message of Opp Valuation Period Team Members
        public string GetMessageTeamMembers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgTeamMembers, 60);
            string message = driver.FindElement(msgTeamMembers).Text;
            return message;
        }

        //Click on Import Positions
        public void ClickImportPositionsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions);
            driver.FindElement(btnImportPositions).Click();
        }

        //To get No Valuation period message
        public string GetMessageNoValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgTeamMembers, 80);
            string message = driver.FindElement(msgTeamMembers).Text;
            driver.FindElement(btnBack).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOppValPeriodList, 80);
            driver.FindElement(btnBackToOppValPeriodList).Click();
            return message;
        }

        //To get No Valuation period message
        public string ValidateAlertMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearchValPeriodPosition, 80);
            driver.FindElement(btnSearchValPeriodPosition).Click();
            try
            {
                Thread.Sleep(2000);
                driver.SwitchTo().Alert().Accept();
                return "Please select an Valuation Period";
            }
            catch (Exception)
            {
                return "No message";
            }
        }

        //To get No Valuation period message
        public string ValidateNoPositionAvailableMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, radioValPeriod, 80);
            driver.FindElement(radioValPeriod).Click();
            driver.FindElement(btnSearchValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgTeamMembers, 80);
            string message = driver.FindElement(msgTeamMembers).Text;
            driver.FindElement(btnBack).Click();
            return message;
        }
    }
}


