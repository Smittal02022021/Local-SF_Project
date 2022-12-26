using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.GiftLog
{
    class GiftApprovePage : BaseClass
    {
        By lnkApproveGifts = By.CssSelector("a[title*='Approve Gifts']");
        By btnApproveSelected = By.CssSelector("td[class*='pbButton'] > input[value='Approve Selected']");
        By btnApprovedSelected = By.CssSelector("td[class='pbButton'] > input[value='Approve Selected']");
        By selectedYear = By.CssSelector("select[id*='yearOptions'] > option[selected='selected']");
        By selectedApprovedStatus = By.CssSelector("select[id*='searchOptions'] > option[selected='selected']");
        By comboApprovedStatus = By.CssSelector("select[id*='searchOptions']");
        By txtAreaRecipientLastName = By.CssSelector("textarea[id*='recipientName']");
        By txtAreaApprovalDenialComments = By.CssSelector("textarea[id*='txtApprovalComment']");
        By labelApprovalDenialComments = By.XPath("//*[@id='j_id0:theForm:rr']/div[2]/div/label");
        By btnDenySelected = By.CssSelector("td[class*='pbButton'] > input[value='Deny Selected']");
        By valApprovedColumnInTable = By.XPath("//*[text()='Approved?']/../../../../tbody/tr[1]/td[13]/span");
        By btnGo = By.CssSelector("input[value='Go']");
        By valGiftDescriptionFromTable = By.CssSelector("a[id*='table:0:j_id46']");
        By GiftDescColLength = By.CssSelector("table[id='j_id0:theForm:rr:table'] > tbody > tr");
        By GiftTableColLength = By.CssSelector("table[id='j_id0:theForm:rr:table'] > thead > tr:nth-child(1) > th");
        By GiftDescCol = By.CssSelector("a[id*='table:j_id47'] > img");
        By valSubmittedFor = By.CssSelector("span[id*='table:0:j_id19']");
        By valueCurrency = By.CssSelector("span[id*='table:0:j_id22']");
        By valueOfGift = By.CssSelector("span[id*='table:0:j_id21']");
        By btnEdit = By.CssSelector("td[class='pbButton'] > input[name='edit']");
        By btnDelete = By.CssSelector("td[id='topButtonRow'] > input[value='Delete']");
        By comboMonth = By.CssSelector("select[id*='monthOptions']");
        By comboYear = By.CssSelector("select[id*='yearOptions']");
        By errorMsgApprovalComment = By.CssSelector("div[id*='j_id6:j_id8']");
        By errorMsgFrApproveGift = By.CssSelector("div[id*='j_id6:j_id8']");
        By txtGiftStatus = By.CssSelector("td[id*='j_id124']>span");
        By btnSubmitRequest = By.XPath("//td[@id='j_id0:j_id2:j_id32:j_id46']/input[@value='Submit Request']");

        By labelGiftName = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(1) > td:nth-child(2)");
        By labelGiftType = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(2) > td:nth-child(2)");
        By labelRecipientForGift = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(3) > td:nth-child(2)");
        By labelSubmittedFor = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(4) > td:nth-child(2)");
        By labelVendor = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(5) > td:nth-child(2)");
        By labelGiftValue = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(6) > td:nth-child(2)");
        By labelCurrency = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(7) > td:nth-child(2)");
        By labelHLRelationship = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(8) > td:nth-child(2)");
        By labelReasonForGift = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(9) > td:nth-child(2)");
        By labelApprovalNumber = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(10) > td:nth-child(2)");
        By labelCreatedBy = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(11) > td:nth-child(2)");
        By labelApproveDate = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(12) > td:nth-child(2)");
        By labelApproved = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(1) > td:nth-child(4)");
        By labelApprovalComment = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(2) > td:nth-child(4)");
        By labelLastModifiedBy = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(11) > td:nth-child(4)");

        public bool VerifyGiftNameInGiftRequestDetails(string file, string giftDesc)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            bool result = false;

            if (driver.FindElement(labelGiftName).Displayed)
            {
                string giftName = driver.FindElement(labelGiftName).Text;
                if (giftName == giftDesc)
                {
                    result=true;
                }
            }
            return result;
        }

        public bool VerifyGiftTypeInGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            bool result = false;

            if (driver.FindElement(labelGiftType).Displayed)
            {
                string giftType = driver.FindElement(labelGiftType).Text;
                if (giftType == ReadExcelData.ReadData(excelPath, "GiftLog", 1))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool VerifyVendorInGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            bool result = false;

            if (driver.FindElement(labelVendor).Displayed)
            {
                string vendor = driver.FindElement(labelVendor).Text;
                if (vendor == ReadExcelData.ReadData(excelPath, "GiftLog", 6))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool VerifyCurrencyInGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            bool result = false;

            if (driver.FindElement(labelCurrency).Displayed)
            {
                string currency = driver.FindElement(labelCurrency).Text;
                if (currency == ReadExcelData.ReadData(excelPath, "GiftLog", 5))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool VerifyHLRelationshipInGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            bool result = false;

            if (driver.FindElement(labelHLRelationship).Displayed)
            {
                string hlRelationship = driver.FindElement(labelHLRelationship).Text;
                if (hlRelationship == ReadExcelData.ReadData(excelPath, "GiftLog", 4))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool VerifyGiftValueInGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            bool result = false;

            if (driver.FindElement(labelGiftValue).Displayed)
            {
                string giftValue = driver.FindElement(labelGiftValue).Text;
                if (giftValue.Contains(ReadExcelData.ReadData(excelPath, "GiftLog", 3)))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool VerifySubmittedForInGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            bool result = false;

            if (driver.FindElement(labelSubmittedFor).Displayed)
            {
                string submittedFor = driver.FindElement(labelSubmittedFor).Text;
                if (submittedFor == ReadExcelData.ReadData(excelPath, "GiftLog", 2))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool VerifyRecipientForInGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            bool result = false;

            if (driver.FindElement(labelRecipientForGift).Displayed)
            {
                string recipientFor = driver.FindElement(labelRecipientForGift).Text;
                if (recipientFor == ReadExcelData.ReadData(excelPath, "GiftLog", 9))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool VerifyReasonForGiftInGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            bool result = false;

            if (driver.FindElement(labelReasonForGift).Displayed)
            {
                string reasonForGift = driver.FindElement(labelReasonForGift).Text;
                if (reasonForGift == ReadExcelData.ReadData(excelPath, "GiftLog", 7))
                {
                    result = true;
                }
            }
            return result;
        }

        public bool VerifyCreatedByInGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            bool result = false;

            if (driver.FindElement(labelCreatedBy).Displayed)
            {
                string createdBy = driver.FindElement(labelCreatedBy).Text;
                if (createdBy.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)))
                {
                    result = true;
                }
            }
            return result;
        }

        public string GetApprovalNumberFromGiftRequestDetails()
        {
            string approvalNum = "";
            if (driver.FindElement(labelApprovalNumber).Displayed)
            {
                approvalNum = driver.FindElement(labelApprovalNumber).Text;
            }
            return approvalNum;
        }

        public void ClickSubmitRequest()
        {
            try
            {
                driver.FindElement(btnSubmitRequest).Click();
                Thread.Sleep(2000);
            }
            catch(Exception e)
            {
                
            }
        }

        public void ClickApproveGiftsTab()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkApproveGifts);
            driver.FindElement(lnkApproveGifts).Click();
        }

        public bool ApproveSelectedButtonVisibility()
        {
            return CustomFunctions.IsElementPresent(driver, btnApproveSelected);
        }

        public void ClickApproveSelectedButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnApproveSelected, 120);
            driver.FindElement(btnApproveSelected).Click();
            Thread.Sleep(3000);
        }

        public bool DenySelectedButtonVisibility()
        {
            return CustomFunctions.IsElementPresent(driver, btnDenySelected);
        }

        public void ClickDenySelectedButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDenySelected);
            driver.FindElement(btnDenySelected).Click();
            Thread.Sleep(3000);
        }

        public bool EditButtonVisibility()
        {
            Thread.Sleep(1000);
            return CustomFunctions.IsElementPresent(driver, btnEdit);
        }

        public bool DeleteButtonVisibility()
        {
            return CustomFunctions.IsElementPresent(driver, btnDelete);
        }

        public void SearchGiftDetails(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaRecipientLastName);
            driver.FindElement(txtAreaRecipientLastName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 1));
        }

        public string GetDefaultSelectedYear()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, selectedYear, 60);
            string DefaultSelectedYear = driver.FindElement(selectedYear).Text;
            return DefaultSelectedYear;
        }

        public string GetDefaultSelectedApprovedStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, selectedApprovedStatus, 60);
            string DefaultApprovedStatus = driver.FindElement(selectedApprovedStatus).Text;
            return DefaultApprovedStatus;
        }

        public bool SearchTextBoxOfRecipientNameVisibility()
        {
            return CustomFunctions.IsElementPresent(driver, txtAreaRecipientLastName);
        }

        public bool TextBoxForApprovalDenialCommentVisibility()
        {
            return CustomFunctions.IsElementPresent(driver, txtAreaApprovalDenialComments);
        }

        public string GetlabelApprovalDenialComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, labelApprovalDenialComments, 60);
            string labelApprovalDenialComment = driver.FindElement(labelApprovalDenialComments).Text;
            return labelApprovalDenialComment;
        }

        public string GetvalueSubmittedFor()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSubmittedFor, 60);
            string valueSubmittedFor = driver.FindElement(valSubmittedFor).Text;
            return valueSubmittedFor;
        }

        public string GetDefaultValuesUnderApprovedColumnInTable()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valApprovedColumnInTable, 60);
            string ApprovedColumnValueInTable = driver.FindElement(valApprovedColumnInTable).Text;
            return ApprovedColumnValueInTable;
        }

        public void SearchByRecipientLastName(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;

            string getMonth = DateTime.Today.ToString("MMM");
            WebDriverWaits.WaitUntilEleVisible(driver, comboMonth);
            driver.FindElement(comboMonth).SendKeys(getMonth);

            string getYear = DateTime.Today.ToString("yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, comboYear);
            driver.FindElement(comboYear).SendKeys(getYear);

            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaRecipientLastName);
            driver.FindElement(txtAreaRecipientLastName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 13));

            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();

            Thread.Sleep(2000);
        }

        public void SearchByRecipientLastNameForNextYear(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;

            string getMonth = DateTime.Today.ToString("MMM");
            WebDriverWaits.WaitUntilEleVisible(driver, comboMonth);
            driver.FindElement(comboMonth).SendKeys(getMonth);


            string getYear = DateTime.Today.AddYears(1).ToString("yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, comboYear);
            driver.FindElement(comboYear).SendKeys(getYear);

            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaRecipientLastName);
            driver.FindElement(txtAreaRecipientLastName).Clear();
            driver.FindElement(txtAreaRecipientLastName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 13));

            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();

            Thread.Sleep(2000);
        }

        public string GetGiftDescriptionFromTable()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftDescriptionFromTable, 60);
            string GiftDescriptionFromTable = driver.FindElement(valGiftDescriptionFromTable).Text;
            return GiftDescriptionFromTable;
        }

        public void CompareGiftDescWithGiftName(string giftName)
        {
            Thread.Sleep(6000);
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            for (int i = 1; i <= totalRows; i++)
            {
                By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                IWebElement descGiftWebElement = driver.FindElement(xyz);

                string descGift = descGiftWebElement.Text;
                if (descGift.Equals(giftName))
                {
                    Console.WriteLine("Gift Description and Gift Name Matches");
                    By checkbox = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(1) > input");
                    IWebElement CheckBoxElement = driver.FindElement(checkbox);
                    Thread.Sleep(1000);
                    CheckBoxElement.Click();
                    Thread.Sleep(1000);
                    descGiftWebElement.Click();
                    Thread.Sleep(1000);
                    break;
                }
            }
        }

        public void CompareAndClickGiftDesc(string giftName)
        {
            Thread.Sleep(2000);
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            for (int i = 1; i <= totalRows; i++)
            {
                By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                IWebElement descGiftWebElement = driver.FindElement(xyz);

                string descGift = descGiftWebElement.Text;
                if (descGift.Equals(giftName))
                {
                    Console.WriteLine("Gift Description and Gift Name Matches");
                    By linkGiftDesc = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                    IWebElement LinkGiftDescElement = driver.FindElement(linkGiftDesc);
                    Thread.Sleep(1000);
                    LinkGiftDescElement.Click();
                    Thread.Sleep(5000);
                    break;
                }
            }
        }

        public string GetPrevYTDValue(string giftName)
        {
            Thread.Sleep(2000);
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            string prevYTDValue = null;
            for (int i = 1; i <= totalRows; i++)
            {
                By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                IWebElement descGiftWebElement = driver.FindElement(xyz);

                string descGift = descGiftWebElement.Text;
                if (descGift.Equals(giftName))
                {
                    By prevYTDLabelValue = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(10)");
                    IWebElement prevYTDElement = driver.FindElement(prevYTDLabelValue);
                    Thread.Sleep(1000);
                    prevYTDValue = prevYTDElement.Text.Split(' ')[1].Trim();
                    break;
                }
            }
            return prevYTDValue;
        }

        public string GetApprovedPrevYTDValue(string giftName)
        {
            Thread.Sleep(2000);
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            string prevYTDValue = null;
            for (int i = 1; i <= totalRows; i++)
            {
                By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                IWebElement descGiftWebElement = driver.FindElement(xyz);

                string descGift = descGiftWebElement.Text;
                if (descGift.Equals(giftName))
                {
                    By prevYTDLabelValue = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(7)");
                    IWebElement prevYTDElement = driver.FindElement(prevYTDLabelValue);
                    Thread.Sleep(1000);
                    prevYTDValue = prevYTDElement.Text.Split(' ')[1].Trim();
                    break;
                }
            }
            return prevYTDValue;
        }

        public string GetNewYTDValue(string giftName)
        {
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            string newYTDValue = null;
            for (int i = 1; i <= totalRows; i++)
            {
                By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                IWebElement descGiftWebElement = driver.FindElement(xyz);

                string descGift = descGiftWebElement.Text;
                if (descGift.Equals(giftName))
                {
                    By newYTDLabelValue = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(12)");
                    IWebElement newYTDElement = driver.FindElement(newYTDLabelValue);
                    Thread.Sleep(1000);
                    newYTDValue = newYTDElement.Text.Split(' ')[3].Trim();
                    break;
                }
            }
            return newYTDValue;
        }

        public string GetApprovedNewYTDValue(string giftName)
        {
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            string newYTDValue = null;
            for (int i = 1; i <= totalRows; i++)
            {
                By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                IWebElement descGiftWebElement = driver.FindElement(xyz);

                string descGift = descGiftWebElement.Text;
                if (descGift.Equals(giftName))
                {
                    By newYTDLabelValue = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(9)");
                    IWebElement newYTDElement = driver.FindElement(newYTDLabelValue);
                    Thread.Sleep(1000);
                    newYTDValue = newYTDElement.Text.Split(' ')[1].Trim();
                    break;
                }
            }
            return newYTDValue;
        }

        public bool SortGiftDetailsTableColumnsByASC(string name)
        {
            bool result = false;

            //Get the table columns count
            IList<IWebElement> element = driver.FindElements(GiftTableColLength);
            int totalColumns = element.Count;

            for (int columnPosition=2; columnPosition<totalColumns; columnPosition++)
            {
                //Get the column name
                By GiftTableCol = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > thead > tr:nth-child(1) > th:nth-child({columnPosition}) > div > a");
                driver.FindElement(GiftTableCol).Click();
                Thread.Sleep(2000);
                string colName = driver.FindElement(GiftTableCol).Text;

                if (name == colName)
                {
                    //Before sort
                    IList<IWebElement> element1 = driver.FindElements(GiftDescColLength);
                    int totalRows = element1.Count;
                    string[] beforSortGiftDesc = new string[totalRows];
                    int i = 0;
                    for (int j = 1; j <= totalRows; j++)
                    {
                        By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({j}) > td:nth-child({columnPosition})");
                        IWebElement descGiftWebElement = driver.FindElement(xyz);

                        beforSortGiftDesc[i] = descGiftWebElement.Text;
                        i++;
                    }

                    Array.Sort(beforSortGiftDesc);

                    By SortIcon = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > thead > tr:nth-child(1) > th:nth-child({columnPosition}) > div > a > img");
                    string sortStatus = driver.FindElement(SortIcon).GetAttribute("alt");

                    if (sortStatus == "Desc")
                    {
                        //Click button to sort results in ASC
                        driver.FindElement(GiftTableCol).Click();
                        Thread.Sleep(2000);

                        //After Ascending Sort
                        string[] afterSortGiftDesc = new string[totalRows];
                        int k = 0;
                        for (int m = 1; m <= totalRows; m++)
                        {
                            By abc = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({m}) > td:nth-child({columnPosition})");
                            IWebElement descGiftWebElement1 = driver.FindElement(abc);

                            afterSortGiftDesc[k] = descGiftWebElement1.Text;
                            k++;
                        }

                        Assert.AreEqual(afterSortGiftDesc, beforSortGiftDesc);
                        result = true;
                    }
                    else
                    {
                        string[] afterSortGiftDesc = new string[totalRows];
                        int a = 0;
                        for (int b = 1; b <= totalRows; b++)
                        {
                            By abc = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({b}) > td:nth-child({columnPosition})");
                            IWebElement descGiftWebElement1 = driver.FindElement(abc);

                            afterSortGiftDesc[a] = descGiftWebElement1.Text;
                            a++;
                        }

                        Assert.AreEqual(afterSortGiftDesc, beforSortGiftDesc);
                        result = true;
                    }
                    break;
                }
            }
            return result;
        }

        public bool SortGiftDetailsTableColumnsByDESC(string name)
        {
            bool result = false;

            //Get the table columns count
            IList<IWebElement> element = driver.FindElements(GiftTableColLength);
            int totalColumns = element.Count;

            for (int columnPosition = 2; columnPosition < totalColumns; columnPosition++)
            {
                //Get the column name
                By GiftTableCol = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > thead > tr:nth-child(1) > th:nth-child({columnPosition}) > div > a");
                //driver.FindElement(GiftTableCol).Click();
                //Thread.Sleep(2000);
                string colName = driver.FindElement(GiftTableCol).Text;

                if (name == colName)
                {
                    //Before sort
                    IList<IWebElement> element1 = driver.FindElements(GiftDescColLength);
                    int totalRows = element1.Count;
                    string[] beforSortGiftDesc = new string[totalRows];
                    int i = 0;
                    for (int j = 1; j <= totalRows; j++)
                    {
                        By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({j}) > td:nth-child({columnPosition})");
                        IWebElement descGiftWebElement = driver.FindElement(xyz);

                        beforSortGiftDesc[i] = descGiftWebElement.Text;
                        i++;
                    }

                    Array.Reverse(beforSortGiftDesc);

                    By SortIcon = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > thead > tr:nth-child(1) > th:nth-child({columnPosition}) > div > a > img");
                    string sortStatus = driver.FindElement(SortIcon).GetAttribute("alt");

                    if (sortStatus == "Desc")
                    {
                        string[] afterSortGiftDesc = new string[totalRows];
                        int k = 0;
                        for (int m = 1; m <= totalRows; m++)
                        {
                            By abc = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({m}) > td:nth-child({columnPosition})");
                            IWebElement descGiftWebElement1 = driver.FindElement(abc);

                            afterSortGiftDesc[k] = descGiftWebElement1.Text;
                            k++;
                        }

                        Assert.AreEqual(afterSortGiftDesc, beforSortGiftDesc);
                        result = true;
                    }
                    else
                    {
                        //Click button to sort results in DESC
                        driver.FindElement(GiftTableCol).Click();
                        Thread.Sleep(2000);

                        string[] afterSortGiftDesc = new string[totalRows];
                        int a = 0;
                        for (int b = 1; b <= totalRows; b++)
                        {
                            By abc = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({b}) > td:nth-child({columnPosition})");
                            IWebElement descGiftWebElement1 = driver.FindElement(abc);

                            afterSortGiftDesc[a] = descGiftWebElement1.Text;
                            a++;
                        }

                        Assert.AreEqual(afterSortGiftDesc, beforSortGiftDesc);
                        result = true;
                    }
                    break;
                }
            }
            return result;
        }

        public bool ValidateGiftDescWithGiftName(string giftName)
        {
            Thread.Sleep(6000);
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            for (int i = 1; i <= totalRows; i++)
            {
                By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                IWebElement descGiftWebElement = driver.FindElement(xyz);

                string descGift = descGiftWebElement.Text;
                if (descGift.Equals(giftName))
                {
                    break;
                }
            }
            return true;
        }

        public string GetGiftCurrencyCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valueCurrency, 60);
            string valCurrency = driver.FindElement(valueCurrency).Text;
            return valCurrency;
        }

        public string GetGiftValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valueOfGift, 60);
            string valOfGift = driver.FindElement(valueOfGift).Text.Split('(')[0].Trim();
            return valOfGift;
        }

        public string GetGiftValueFromPendingGifts(string giftName)
        {
            Thread.Sleep(6000);
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            string giftValue = null;
            for (int i = 1; i <= totalRows; i++)
            {
                By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                IWebElement descGiftWebElement = driver.FindElement(xyz);

                string descGift = descGiftWebElement.Text;
                if (descGift.Equals(giftName))
                {
                    By giftValueLabel = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(11)");
                    IWebElement giftValueElement = driver.FindElement(giftValueLabel);
                    Thread.Sleep(1000);
                    giftValue = giftValueElement.Text.Split(' ')[1].Trim();
                    break;
                }
            }
            return giftValue;
        }

        public string GetGiftValueFromApprovedGifts(string giftName)
        {
            Thread.Sleep(2000);
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            string giftValue = null;
            for (int i = 1; i <= totalRows; i++)
            {
                By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                IWebElement descGiftWebElement = driver.FindElement(xyz);

                string descGift = descGiftWebElement.Text;
                if (descGift.Equals(giftName))
                {
                    By giftValueLabel = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(8)");
                    IWebElement giftValueElement = driver.FindElement(giftValueLabel);
                    Thread.Sleep(1000);
                    giftValue = giftValueElement.Text.Split(' ')[1].Trim();
                    break;
                }
            }
            return giftValue;
        }

        public void SearchByStatus(string file, string status)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            Thread.Sleep(3000);
            string excelPath = dir + file;


            string getMonth = DateTime.Today.ToString("MMM");
            WebDriverWaits.WaitUntilEleVisible(driver, comboMonth);
            driver.FindElement(comboMonth).SendKeys(getMonth);

            WebDriverWaits.WaitUntilEleVisible(driver, comboApprovedStatus);
            driver.FindElement(comboApprovedStatus).SendKeys(status);

            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaRecipientLastName);
            driver.FindElement(txtAreaRecipientLastName).Clear();
            driver.FindElement(txtAreaRecipientLastName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 13));

            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);
        }

        public void SearchByStatusForNextYear(string file, string status)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            Thread.Sleep(3000);
            string excelPath = dir + file;

            string getMonth = DateTime.Today.ToString("MMM");
            WebDriverWaits.WaitUntilEleVisible(driver, comboMonth);
            driver.FindElement(comboMonth).SendKeys(getMonth);

            string getYear = DateTime.Today.AddYears(1).ToString("yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, comboYear);
            driver.FindElement(comboYear).SendKeys(getYear);

            WebDriverWaits.WaitUntilEleVisible(driver, comboApprovedStatus);
            driver.FindElement(comboApprovedStatus).SendKeys(status);

            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaRecipientLastName);
            driver.FindElement(txtAreaRecipientLastName).Clear();
            driver.FindElement(txtAreaRecipientLastName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 13));

            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);
        }

        public string ErrorMsgApprovalComment()
        {
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, errorMsgApprovalComment, 3000);
            string txt = driver.FindElement(errorMsgApprovalComment).Text;
            return txt;
        }

        public void SetApprovalDenialComments()
        {
            driver.FindElement(txtAreaApprovalDenialComments).Clear();
            driver.FindElement(txtAreaApprovalDenialComments).SendKeys("approved");
            Thread.Sleep(2000);
        }

        public string ErrorMsgForApproveGift()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, errorMsgFrApproveGift);
            string text = driver.FindElement(errorMsgFrApproveGift).Text;
            return text;
        }

        public void SelectApprovedStatusCombo(string status)
        {

            WebDriverWaits.WaitUntilEleVisible(driver, comboApprovedStatus);
            driver.FindElement(comboApprovedStatus).SendKeys(status);
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();

        }

        public string GetGiftStatus()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftStatus);
            String txt = driver.FindElement(txtGiftStatus).Text;
            return txt;
        }

        public string GetStatusCompareGiftDescWithGiftName(string giftName)
        {
            string txt = null;
            Thread.Sleep(3000);
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            for (int i = 1; i <= totalRows; i++)
            {
                By xyz = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(2) > a");
                IWebElement descGiftWebElement = driver.FindElement(xyz);

                string descGift = descGiftWebElement.Text;
                if (descGift.Equals(giftName))
                {
                    By status = By.CssSelector($"table[id='j_id0:theForm:rr:table'] > tbody > tr:nth-child({i}) > td:nth-child(10) > span");

                    Console.WriteLine("Gift Description and Gift Name Matches");

                    IWebElement CheckBoxElement = driver.FindElement(status);
                    Thread.Sleep(1000);
                    txt = CheckBoxElement.Text;
                    break;
                }
            }
            return txt;


        }
            
    }
}
