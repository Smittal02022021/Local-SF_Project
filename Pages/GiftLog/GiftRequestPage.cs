using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace SalesForce_Project.Pages.GiftLog
{
    class GiftRequestPage : BaseClass
    {
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By linkGiftRequests = By.CssSelector("td[class*='Custom40Block'] > a");
        By valGiftRequestsTitle = By.CssSelector("h2[class='mainTitle']");
        By comboGiftType = By.CssSelector("select[id*='txtGiftType']");
        By txtGiftName = By.CssSelector("input[id*='txtGiftName']");
        By txtGiftValue = By.CssSelector("input[id*='txtGiftValue']");
        By comboHlRelationship = By.CssSelector("select[id*='txtHlRelationship']");
        By comboCurrency = By.CssSelector("select[id*='ddlCurrency']");
        By txtVendor = By.CssSelector("input[id*='txtVendor']");
        By txtReasonForGift = By.CssSelector("textarea[id*='txtReason']");
        By txtCompanyName = By.CssSelector("input[id*='searchTextAccount']");
        By txtContactName = By.CssSelector("input[id*='searchTextContact']");
        By btnSearch = By.CssSelector("input[name*='thePage:theForm:thePageBlock'][value='Search']");

        By valAvailableRecipientName = By.CssSelector("tbody[id*='j_id50:j_id60:table:tb']>tr:nth-child(1)>td:nth-child(2)>span");
        By valRecipientCompanyName = By.CssSelector("tbody[id*='j_id50:j_id60:table:tb']>tr:nth-child(1)>td:nth-child(3)>span");

        By chkBoxOfAvailableRecipient = By.CssSelector("input[name*='table:0:j_id62']");
        By btnAddRecipients = By.CssSelector("input[name*='j_id91:j_id93']");
        //  By valSelectedRecipientName = By.CssSelector("span[id*='table2:0:j_id165']");

        By valSelectedRecipientName = By.CssSelector("tbody[id*='j_id50:table2:tb']>tr>td:nth-child(2)>span");
        By valSelectedCompanyName = By.CssSelector("tbody[id*='j_id50:table2:tb']>tr>td:nth-child(3)>span");

        // By valSelectedCompanyName = By.CssSelector("span[id*='table2:0:j_id166']");
        By chkBoxOfSelectedRecipient = By.CssSelector("input[name*='table2:0:j_id77']");
        //By btnRemoveRecipients = By.CssSelector("input[name*='j_id93:j_id94']");
        By selectedRecipients = By.CssSelector("table[id*='j_id49:table2'] > tbody > tr");
        By btnCancel = By.CssSelector("td[class='pbButtonb '] > input[value='Cancel']");
        By btnSubmitGiftRequest = By.CssSelector("td[class='pbButtonb '] > input[value='Submit Gift Request']");
        By valCongratulationMsg = By.CssSelector("form[id='j_id0:j_id2'] > h1");
        By valRecipientNameGiftDetail = By.CssSelector("span[id*='table:0:j_id19']");
        By valGiftDescription = By.CssSelector("span[id*='table:0:j_id18']");
        By btnReturnToPreApprovalPage = By.CssSelector("input[name*='j_id4:j_id17']");
        By linkGiftLog = By.XPath("//a[normalize-space()='Gift Log']");
        By drpdwnArrow = By.CssSelector("div[id='tsid-arrow']");
        By linkHLForce = By.XPath("//a[normalize-space()='HL Force']");
        By valDrpDwnValue = By.CssSelector("span[id='tsidLabel']");
        By txtSubmittedFor = By.CssSelector("span[class='lookupInput'] >input[id*='txtSubmittedFor']");
        By labelNewGiftAmtYTD = By.CssSelector("div[id*='j_id82header:sortDiv']");
        By valNewGiftAmtYTD = By.CssSelector("td[id*='table2:0:j_id82']");
        By valNewGiftTotalNextYear = By.CssSelector("td[id*='table2:0:j_id86']");
        By valWarningMsgFirstLine = By.CssSelector("form[id='j_id0:j_id2'] > span:nth-child(3)");
        By valWarningMsgNextLine = By.CssSelector("form[id='j_id0:j_id2'] > span:nth-child(5)");
        By btnReviseRequest = By.CssSelector("td[class='pbButton '] > input[value='Revise Request']");
        By btnSubmitRequest = By.CssSelector("td[class='pbButton '] > input[value='Submit Request']");
        By valErrorMsgOnlyOneRecipient = By.CssSelector("div[id*='j_id6:j_id8']");
        By txtDesireDate = By.CssSelector("input[id*='txtDesiredDate']");
        By linkDesireDate = By.XPath("//span[@class='dateFormat']/a");
        By valErrorMsgDesireDate = By.CssSelector("td[class='messageCell'] > div");
        By chkDivideGiftValue = By.CssSelector("input[id*='isDistributed']");
        By chkFirstSelectedRecipient = By.CssSelector("input[name*='0:j_id62']");
        By btnRemoveRecipients = By.CssSelector("input[value*='Remove Recipients']");
        By btnRefresh = By.CssSelector("input[value='Refresh']");


        By clickLookupIcon = By.CssSelector("img.lookupIcon");
        By txtMsgFrame = By.CssSelector("div.messageText");

        By radioBtnName = By.CssSelector("input[value=SEARCH_NAME]");
        By radioBtnAllFields = By.CssSelector("input[value=SEARCH_ALL]");

        By txtSearchBox = By.CssSelector("input#lksrch");
        By btnGo = By.CssSelector("input[name=go]");

        By txtSearchResults = By.CssSelector("#Contact > div:nth-of-type(2) > div > div:nth-of-type(1) > table > tbody > tr:nth-of-type(1) > td:nth-of-type(1) > h3 > span");


        By searchFrame = By.CssSelector("frame#searchFrame");
        By resultFrame = By.CssSelector("frame#resultsFrame");
        By srchHoulihanEmployeeResult = By.CssSelector("tr.dataRow.even.first > th>a:nth-of-type(1)");


        By titleResult = By.CssSelector("tr.dataRow.even.first > td:nth-of-type(1)");
        By deptResult = By.CssSelector("tr.dataRow.even.first > td:nth-of-type(2)");
        //  By SrchCmpnyResults = By.XPath("//tr[@class='dataRow even  first']/td[3]/span");
        By selectCompanyName = By.CssSelector("td[class='data2Col  first '] > span > select");
        By selectContactName = By.CssSelector("td[class='data2Col '] > span > select");
        By helpTxtGiftType = By.CssSelector("div.pbBody > div:nth-of-type(1) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(1) > th:nth-of-type(1) > span > script");
        By helpTxtSubmittedFor = By.CssSelector("div.pbBody > div:nth-of-type(1) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(1) > th:nth-of-type(2) > span > script");
        By helpTxtGiftValue = By.CssSelector("div.pbBody > div:nth-of-type(2) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(2) > th:nth-of-type(1) > span > script");
        By helpTxtDesiredDate = By.CssSelector("div.pbBody > div:nth-of-type(2) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(2) > th:nth-of-type(2) > span > script");
        By helpTxtCurrency = By.CssSelector("div.pbBody > div:nth-of-type(2) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(3) > th:nth-of-type(1) > span > script");
        By helpTxtVendor = By.CssSelector("div.pbBody > div:nth-of-type(2) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(3) > th:nth-of-type(2) > span > script");
        By txtCurrentGiftAmtYTD = By.CssSelector("td[id*='table:0:j_id67']");
        By txtCurrentNextYearGiftAmt = By.CssSelector("td[id*='table:0:j_id71']");
        By selectCurrencyDrpDown = By.CssSelector("select[id*='j_id28:ddlCurrency']");

        By linkGiftRequestTab = By.XPath("//a[text()='Gift Requests']");
        
        public void GoToGiftRequestPage()
        {

            string valueOfDropDwn = driver.FindElement(valDrpDwnValue).Text;
            if (valueOfDropDwn.Equals("HL Force"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, drpdwnArrow, 120);
                driver.FindElement(drpdwnArrow).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, linkGiftLog, 120);
                driver.FindElement(linkGiftLog).Click();
            }
        }

        public void GoToGiftRequestTab()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, linkGiftRequestTab, 120);
            driver.FindElement(linkGiftRequestTab).Click();

            Thread.Sleep(2000);
        }

        public void SwitchFromGiftLogToHLForce()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, drpdwnArrow, 120);
            driver.FindElement(drpdwnArrow).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, linkHLForce, 120);
            driver.FindElement(linkHLForce).Click();
        }
        // Navigate to gift request page
        public void GoToGiftRequestsPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, shwAllTab, 120);
            driver.FindElement(shwAllTab).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, linkGiftRequests, 120);
            driver.FindElement(linkGiftRequests).Click();
        }

        public string GetGiftRequestPageTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftRequestsTitle, 60);
            string selectedCompanyTypeSelected = driver.FindElement(valGiftRequestsTitle).Text;
            return selectedCompanyTypeSelected;
        }

        public bool ValidateFieldsUnderGiftBillingDetailsSection()
        {
            if (driver.FindElement(comboGiftType).Displayed && driver.FindElement(txtSubmittedFor).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidateFieldsUnderGiftDetailsSection()
        {
            if (driver.FindElement(txtGiftName).Displayed && driver.FindElement(txtGiftValue).Displayed && driver.FindElement(comboHlRelationship).Displayed && driver.FindElement(comboCurrency).Displayed && driver.FindElement(txtVendor).Displayed && driver.FindElement(txtReasonForGift).Displayed && driver.FindElement(txtDesireDate).Displayed && driver.FindElement(chkDivideGiftValue).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateFieldsUnderGiftRecipientsSection()
        {
            if (driver.FindElement(txtCompanyName).Displayed && driver.FindElement(txtContactName).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string EnterDetailsGiftRequest(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;


            string excelPath = dir + file;
            // Enter value in gift type
            WebDriverWaits.WaitUntilEleVisible(driver, comboGiftType);
            driver.FindElement(comboGiftType).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 1));

            WebDriverWaits.WaitUntilEleVisible(driver, txtSubmittedFor);
            driver.FindElement(txtSubmittedFor).Clear();
            driver.FindElement(txtSubmittedFor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 2));

            //Enter value of gift name
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftName);
            driver.FindElement(txtGiftName).Clear();
            string valGiftName = CustomFunctions.RandomValue();
            driver.FindElement(txtGiftName).SendKeys(valGiftName);

            // Enter gift value
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 3));

            // Enter HL Relationship
            WebDriverWaits.WaitUntilEleVisible(driver, comboHlRelationship);
            driver.FindElement(comboHlRelationship).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 4));

            WebDriverWaits.WaitUntilEleVisible(driver, comboCurrency);
            driver.FindElement(comboCurrency).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 5));

            //Enter vendor details
            WebDriverWaits.WaitUntilEleVisible(driver, txtVendor);
            driver.FindElement(txtVendor).Clear();
            driver.FindElement(txtVendor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 6));

            //Enter reason for gift
            WebDriverWaits.WaitUntilEleVisible(driver, txtReasonForGift);
            driver.FindElement(txtReasonForGift).Clear();
            driver.FindElement(txtReasonForGift).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 7));

            //Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 8));

            //Enter contact name
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName);
            driver.FindElement(txtContactName).Clear();
            driver.FindElement(txtContactName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 9));

            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

            return valGiftName;
        }

        public string EnterGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            // Enter value in gift type
            WebDriverWaits.WaitUntilEleVisible(driver, comboGiftType);
            driver.FindElement(comboGiftType).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 1));

            WebDriverWaits.WaitUntilEleVisible(driver, txtSubmittedFor);
            driver.FindElement(txtSubmittedFor).Clear();
            driver.FindElement(txtSubmittedFor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 2));

            //Enter value of gift name
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftName);
            driver.FindElement(txtGiftName).Clear();
            string valGiftName = CustomFunctions.RandomValue();
            driver.FindElement(txtGiftName).SendKeys(valGiftName);

            // Enter gift value
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 3));

            // Enter HL Relationship
            WebDriverWaits.WaitUntilEleVisible(driver, comboHlRelationship);
            driver.FindElement(comboHlRelationship).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 4));

            WebDriverWaits.WaitUntilEleVisible(driver, comboCurrency);
            driver.FindElement(comboCurrency).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 5));

            //Enter vendor details
            WebDriverWaits.WaitUntilEleVisible(driver, txtVendor);
            driver.FindElement(txtVendor).Clear();
            driver.FindElement(txtVendor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 6));

            //Click Divide Gift Value
            driver.FindElement(chkDivideGiftValue).Click();
            //Enter reason for gift
            WebDriverWaits.WaitUntilEleVisible(driver, txtReasonForGift);
            driver.FindElement(txtReasonForGift).Clear();
            driver.FindElement(txtReasonForGift).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 7));

            //Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 8));


            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

            return valGiftName;
        }

        public void SetDesiredDateToCurrentDate()
        {
            driver.FindElement(linkDesireDate).Click();
            Thread.Sleep(2000);
        }

        public string EnterDesiredDate(int Days)
        {
            string getDate = DateTime.Today.AddDays(Days).ToString("dd/MM/yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtDesireDate);
            driver.FindElement(txtDesireDate).Clear();
            string newDate = getDate.Replace('-', '/');
            driver.FindElement(txtDesireDate).SendKeys(newDate);

            return newDate;
        }

        public void ClearGiftRecipientsDetails()
        {
            //Clear company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();

            //Clear contact name
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName);
            driver.FindElement(txtContactName).Clear();
        }

        public void ClickRefreshButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRefresh);
            driver.FindElement(btnRefresh).Click();
        }

        public string GetAvailableRecipientName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAvailableRecipientName, 60);
            string AvailableRecipientName = driver.FindElement(valAvailableRecipientName).Text;
            return AvailableRecipientName;
        }

        public string GetAvailableRecipientCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecipientCompanyName, 3000);
            string RecipientCompanyName = driver.FindElement(valRecipientCompanyName).Text;
            return RecipientCompanyName;
        }

        public string GetErrorMessageForSelectingAlteastOneRecipient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valErrorMsgOnlyOneRecipient, 60);
            string ErrorMsgOnlyOneRecipient = driver.FindElement(valErrorMsgOnlyOneRecipient).Text;
            return ErrorMsgOnlyOneRecipient;
        }

        public void AddRecipientToSelectedRecipients()
        {
            //Click check box
            WebDriverWaits.WaitUntilEleVisible(driver, chkBoxOfAvailableRecipient, 240);
            driver.FindElement(chkBoxOfAvailableRecipient).Click();

            //Click add recipients button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRecipients, 120);
            driver.FindElement(btnAddRecipients).Click();
        }

        public void ClickAddRecipient()
        {
            //Click add recipients button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRecipients, 120);
            driver.FindElement(btnAddRecipients).Click();
        }

        public int GetSizeOfAvailableRecipient()
        {
            IList<IWebElement> availableRecipient = driver.FindElements(By.CssSelector("tbody[id*='j_id59:table:tb'] > tr"));
            return availableRecipient.Count;
        }

        public int GetSizeOfSelectedRecipient()
        {
            IList<IWebElement> selectedRecipient = driver.FindElements(By.CssSelector("tbody[id*='j_id49:table2:tb'] > tr"));
            return selectedRecipient.Count;
        }

        public string GetDollarValue(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"td[id*='{num}:j_id81']"), 60);
            string dollarValue = driver.FindElement(By.CssSelector($"td[id*='{num}:j_id81']")).Text.Split(' ')[1].Trim();
            return dollarValue;
        }

        public string GetDollarValueTotalNextYear(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"td[id*='{num}:j_id85']"), 60);
            string dollarValue = driver.FindElement(By.CssSelector($"td[id*='{num}:j_id85']")).Text.Split(' ')[1].Trim();
            return dollarValue;
        }

        public void AddMultipleRecipientToSelectedRecipients(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"input[name*='{num}:j_id61']"), 60);
            driver.FindElement(By.CssSelector($"input[name*='{num}:j_id61']")).Click();
        }

        public string GetSelectedRecipientName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSelectedRecipientName, 150);
            string RecipientCompanyName = driver.FindElement(valSelectedRecipientName).Text;
            return RecipientCompanyName;
        }

        public void RemoveSelectedRecipient()
        {
            driver.FindElement(chkFirstSelectedRecipient).Click();
            driver.FindElement(btnRemoveRecipients).Click();
        }

        public string GetSelectedCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSelectedCompanyName, 60);
            string RecipientCompanyName = driver.FindElement(valSelectedCompanyName).Text;
            return RecipientCompanyName;
        }

        public void RemoveRecipientFromSelectedRecipients()
        {
            //Click check box
            WebDriverWaits.WaitUntilEleVisible(driver, chkBoxOfSelectedRecipient, 120);
            driver.FindElement(chkBoxOfSelectedRecipient).Click();

            //Click add recipients button
            WebDriverWaits.WaitUntilEleVisible(driver, btnRemoveRecipients, 120);
            driver.FindElement(btnRemoveRecipients).Click();
        }

        public bool IsSelectedRecipientDisplayed()
        {
            return CustomFunctions.IsElementPresent(driver, selectedRecipients);
        }

        public bool IsReviseRequestButtonVisible()
        {
            return CustomFunctions.IsElementPresent(driver, btnReviseRequest);
        }

        public bool IsSubmitGiftRequestButtonVisible()
        {
            return CustomFunctions.IsElementPresent(driver, btnSubmitGiftRequest);
        }

        public bool IsReturnToPreApprovalPageVisible()
        {
            return CustomFunctions.IsElementPresent(driver, btnReturnToPreApprovalPage);
        }

        public bool IsSubmitRequestButtonVisible()
        {
            return CustomFunctions.IsElementPresent(driver, btnSubmitRequest);
        }
        public void ClickCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
        }

        public void ClickReviseRequestButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReviseRequest, 120);
            driver.FindElement(btnReviseRequest).Click();
        }

        public void ClickSubmitRequestButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitRequest, 120);
            driver.FindElement(btnSubmitRequest).Click();
        }

        public void ClickSubmitGiftRequest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitGiftRequest, 120);
            driver.FindElement(btnSubmitGiftRequest).Click();
            Thread.Sleep(2000);
        }

        public string GetCongratulationsMsg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCongratulationMsg, 60);
            string CongratulationMsg = driver.FindElement(valCongratulationMsg).GetAttribute("innerText");
            return CongratulationMsg;
        }

        public string GetRecipientNameOnGiftRequestDetail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecipientNameGiftDetail, 60);

            string RecipientNameGiftDetail = driver.FindElement(valRecipientNameGiftDetail).Text;
            return RecipientNameGiftDetail;
        }

        public string GetGiftDescriptionOnGiftRequestDetail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftDescription, 60);
            string giftDescGiftDetail = driver.FindElement(valGiftDescription).Text;
            return giftDescGiftDetail;
        }

        public void ClickReturnToPreApprovalPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToPreApprovalPage, 120);
            driver.FindElement(btnReturnToPreApprovalPage).Click();
        }

        public string GetLabelNewGiftAmtYTD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, labelNewGiftAmtYTD, 60);
            string lblNewGiftAmtYTD = driver.FindElement(labelNewGiftAmtYTD).Text;
            return lblNewGiftAmtYTD;
        }

        public string GetGiftCurrencyCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftAmtYTD, 60);
            string currencyNewGiftAmtYTD = driver.FindElement(valNewGiftAmtYTD).Text.Split(' ')[0].Trim();
            return currencyNewGiftAmtYTD;
        }

        public string GetGiftValueInGiftAmtYTD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftAmtYTD, 60);
            string valueNewGiftAmtYTD = driver.FindElement(valNewGiftAmtYTD).Text.Split(' ')[1].Trim();
            return valueNewGiftAmtYTD;
        }

        public string GetGiftValueInGiftTotalNextYear()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftTotalNextYear, 60);
            //string test = driver.FindElement(valNewGiftTotalNextYear).Text;
            string valueNewGiftTotalNextYear = driver.FindElement(valNewGiftTotalNextYear).Text.Split(' ')[1].Trim();
            return valueNewGiftTotalNextYear;
        }

        public string GetGiftValueColorInGiftAmtYTD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftAmtYTD, 60);
            string colorOfValueNewGiftAmtYTD = driver.FindElement(valNewGiftAmtYTD).GetCssValue("color");
            if (colorOfValueNewGiftAmtYTD.Equals("rgba(255, 0, 0, 1)"))
            {
                return "Red";
            }
            else
            {
                return colorOfValueNewGiftAmtYTD;
            }
        }

        public string GetWarningMessageOnAmountLimitExceed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valWarningMsgFirstLine, 60);
            string WarningMsg1 = driver.FindElement(valWarningMsgFirstLine).Text;

            WebDriverWaits.WaitUntilEleVisible(driver, valWarningMsgNextLine, 60);
            string WarningMsg2 = driver.FindElement(valWarningMsgNextLine).Text;
            string WarningMsg = WarningMsg1 + WarningMsg2;
            return WarningMsg;
        }

        public string GetDefaultSubmittedForUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSubmittedFor, 60);
            string DefaultSubmittedForUser = driver.FindElement(txtSubmittedFor).GetAttribute("value");
            return DefaultSubmittedForUser;
        }

        public string GetDesiredDateErrorMsg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valErrorMsgDesireDate, 60);
            string ErrorMsgDesireDate = driver.FindElement(valErrorMsgDesireDate).Text;
            return ErrorMsgDesireDate;
        }

        public IWebElement ErrorFieldsGiftTypeValueAndVendor(IWebDriver driver, string name)
        {
            return driver.FindElement(By.XPath($"//*[text()='{name}']/../../../td/div/div[@class='errorMsg']"));
        }

        public IWebElement ErrorFieldsGiftNameAndHLRelationship(IWebDriver driver, string name, int td)
        {
            return driver.FindElement(By.XPath($"//*[text()='{name}']/../../td[{td}]/div/div[@class='errorMsg']"));
        }


        //Get Text from Lookup form Submitted for Look up on Pre-Approval Page

        public string TxtfromLookupSubittedFor()
        {
            driver.FindElement(clickLookupIcon).Click();
            //   Thread.Sleep(3000);
            CustomFunctions.SwitchToWindow(driver, 1);
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));

            string browserTitle = driver.Title;

            String Txt = driver.FindElement(txtMsgFrame).Text;
            return Txt;
        }
        //Verify Radion Button is checked by default
        public bool VerifyRadioBtnName()
        {
            string str = driver.FindElement(radioBtnName).GetAttribute("checked");
            Console.WriteLine(str);
            if (str.Equals("true"))
            {

                return true;
            }
            else
            {
                return false;
            }

        }


        //Verify 0 results are displayed when user searched for external contacts
        public string SrchExternalContct()
        {
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("test external");

            driver.FindElement(btnGo).Click();


            driver.SwitchTo().DefaultContent();

            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(txtSearchResults).Text;
            return txt;
            //    try
            //    {
            //        if ((driver.FindElement(By.XPath("//span[contains(text(),'Contacts [0]"))) != null)
            //        {


            //        }
            //        return true;
            //    }
            //    catch (Exception e)

            //    {

            //        return false;
            //    }


            //}
        }
        //Verify appropriate results are dispalyed when user searched with partial name of Houlihan Employee
        public string SrchHoulihanEmployee()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));

            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("oscar");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(srchHoulihanEmployeeResult).Text;
            return txt;

        }

        public string SrchTitle()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(radioBtnAllFields).Click();
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("Managing Director");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(titleResult).Text;
            return txt;
        }

        public string SrchDept()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(radioBtnAllFields).Click();
            driver.FindElement(txtSearchBox).Clear();

            driver.FindElement(txtSearchBox).SendKeys("FR");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(deptResult).Text;
            driver.SwitchTo().DefaultContent();
            CustomFunctions.SwitchToWindow(driver, 0);

            return txt;

        }
        //Verify Company Name Combo Box
        public void VerifyCompnyNameComboBox(string file, string value, string coompanyname)
        {
            CustomFunctions.SelectByText(driver, driver.FindElement(selectCompanyName), value);

            //Enter company name

            driver.FindElement(txtCompanyName).SendKeys(coompanyname);

            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

            CustomFunctions.SelectByIndex(driver, driver.FindElement(selectCompanyName), 0);

        }
        //Verify Contact Name Combo Box
        public void VerifyContactNameComboBox(string file, string value, string contactname)
        {
            CustomFunctions.SelectByText(driver, driver.FindElement(selectContactName), value);

            //Enter contact name


            driver.FindElement(txtContactName).SendKeys(contactname);

            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

        }
        //Verify combination of COmpany and Contact Combo Box
        public void VerifyCompanyContactNameComboBox(string file, string value,string val, string companyname, string contactname)
        {
            CustomFunctions.SelectByText(driver, driver.FindElement(selectContactName), value);
            CustomFunctions.SelectByText(driver, driver.FindElement(selectCompanyName), val);

            //Enter company name

            driver.FindElement(txtCompanyName).SendKeys(companyname);
            //Enter contact name
           
            driver.FindElement(txtContactName).SendKeys(contactname);

            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

            CustomFunctions.SelectByIndex(driver, driver.FindElement(selectContactName), 0);
        }

        public string VerifyHelpTextGiftType()
        {
            string atr = driver.FindElement(helpTxtGiftType).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }
        public string VerifyHelpTextSubmittedFor()
        {
            string atr = driver.FindElement(helpTxtSubmittedFor).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }

        public string VerifyHelpTextGiftValue()
        {
            string atr = driver.FindElement(helpTxtGiftValue).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }

        public string VerifyHelpTextDesiredDate()
        {
            string atr = driver.FindElement(helpTxtDesiredDate).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }

        public string VerifyHelpTextCurrency()
        {
            string atr = driver.FindElement(helpTxtCurrency).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }
        public string VerifyHelpTextVendor()
        {
            string atr = driver.FindElement(helpTxtVendor).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }
        public void EnterGiftValue(string value)
        {
           // string excelPath = dir + file;
            // Enter gift value
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue,120);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(value);
        }
        public string GetCurrentGiftAmtYTD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCurrentGiftAmtYTD, 60);
            string CurrentGiftAmtYTDText = driver.FindElement(txtCurrentGiftAmtYTD).Text.Split(' ')[1].Trim(); ;
            return CurrentGiftAmtYTDText;
        }

        public string GetCurrentNextYearGiftAmt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCurrentNextYearGiftAmt, 60);
            string CurrentNextYearGiftAmtText = driver.FindElement(txtCurrentNextYearGiftAmt).Text.Split(' ')[1].Trim();
            return CurrentNextYearGiftAmtText;
        }

        public void SelectCurrencyDrpDown(string value) {
            WebDriverWaits.WaitUntilEleVisible(driver, selectCurrencyDrpDown, 60);
            CustomFunctions.SelectByText(driver, driver.FindElement(selectCurrencyDrpDown), value);
            

        }
    }
}