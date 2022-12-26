using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;
//using System.Windows.Forms;

namespace SalesForce_Project.Pages
{
    class AdditionalClientSubjectsPage : BaseClass
    {
        By btnContinue = By.CssSelector("input[value='Continue']");
        By winAdditionalClientSubject = By.CssSelector("span[class='ui-dialog-title']");
        By winAdditionalClient = By.Id("ui-id-2");
        By txtSearch = By.CssSelector("input[name*= 'txtSearch']");
        By btnGo = By.CssSelector("input[name*= 'btnGo']");
        By checkCompany = By.CssSelector("input[name*= 'id50']");
        By btnAddSelected = By.CssSelector("input[value= 'Add Selected']");
        By msgSuccess = By.XPath("//div[contains(text(),'Company Added To Additional Clients')]");
        By btnClose = By.XPath("//span[contains(text(),'Add Additional Client(s)')] /following-sibling::button");
        By btnSubClose = By.XPath("//span[contains(text(),'Add Additional Subject(s)')] /following-sibling::button");
        By msgSubject = By.XPath("//div[contains(text(),'Company Added To Additional Subjects')]");
        By additionalCompany = By.XPath("//*[contains(@id,'pbAdditionalClients')] / tr/td/span/a[contains(text(),'Del')]");
        By additionalSubject = By.XPath("//*[contains(@id,'pbAdditionalSubjects')] / tr/td/span/a[contains(text(),'Del')]");
        By comboClientInterest = By.CssSelector("select[name*='hasAdverseClients']");
        By btnSaveClose = By.CssSelector("input[value='Save & Close']");
        By btnAddClient = By.Id("newClient");
        By btnAddSubject = By.Id("newSubject");
        By titleHLTeam = By.CssSelector("h2[class='mainTitle']");
        By txtStaff = By.CssSelector("input[placeholder*='Begin Typing Name']");
        //By checkInitiator = By.CssSelector("input[name*='internalTeam:j_id73:0:j_id75']");

        By checkInitiator = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[2]/input");
        By checkMarketing = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[3]/input");
        By checkSeller = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[4]/input");
        By checkPrincipal = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[5]/input");
        By checkManager = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[6]/input");
        By checkAssociate = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[7]/input");
        By checkAnalyst = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[8]/input");
        By checkSpecialty = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[9]/input");
        By checkPEHF = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[10]/input");
        By checkIntern1 = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[11]/input");
        By checkIntern = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[12]/input");

        By btnSave = By.CssSelector("input[value='Save']");
        By listStaff = By.XPath("/html/body/ul");
        By listStaff2 = By.XPath("(/html/body/ul/li)[2]/a");
        By listGCAMember = By.XPath("//li[@class='ui-menu-item']/a/b/b[text()='Mark']");
        By btnReturnToOppor = By.CssSelector("input[value='Return To Opportunity']");
        By linkCompanyName = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[2]/th/a");
        By linkSubjectName = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[3]/th/a");
        By txtCompanyType = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[2]/td[2]");
        By txtCompanyRecType = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[2]/td[3]");
        By txtSubjectType = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[3]/td[2]");
        By txtSubjectRecType = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[3]/td[3]");

        //public static string fileTC1649 = "T1649_AdditionalClientsSubjectRequired.xlsx";
        //string excelPath = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\" + fileTC1649;

        public void ClickContinue()
        {
            //Calling wait function--Continue button     
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
            driver.FindElement(btnContinue).Submit();
        }


        public string ValidateAdditionalClientSubjectTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, winAdditionalClientSubject);
            IWebElement titleAdditionalClientSubject = driver.FindElement(winAdditionalClientSubject);
            return titleAdditionalClientSubject.Text;
        }
        public void ClickAddClient()
        {
            Thread.Sleep(3000);
            driver.SwitchTo().Frame(4);
            driver.FindElement(btnAddClient).Click();
        }

        public string ValidateAdditionalClientTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, winAdditionalClient, 40);
            IWebElement titleAdditionalClient = driver.FindElement(winAdditionalClient);
            return titleAdditionalClient.Text;
        }
        //To add additional Client
        public void AddAdditionalClient(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.SwitchTo().Frame(0);           
            driver.FindElement(txtSearch).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 33));
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();

            //Calling wait function -- to load search results
            WebDriverWaits.WaitUntilEleVisible(driver, checkCompany, 50);
            driver.FindElement(checkCompany).Click();
            driver.FindElement(btnAddSelected).Click();
        }
        //To validate message while adding additional Client
        public string ValidateMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess);
            return driver.FindElement(msgSuccess).Text;
        }
        //To validate additional Client added in Table
        public string ValidateTableDetails()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(4);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, additionalCompany, 50);
            if (driver.FindElement(additionalCompany).Displayed)
                return "True";
            else
                return "False";
        }

        //To add additional subject
        public void AddAdditionalSubject(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(btnAddSubject).Click();
            driver.SwitchTo().Frame(0);
            driver.FindElement(txtSearch).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 35));
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, checkCompany, 50);
            driver.FindElement(checkCompany).Click();
            driver.FindElement(btnAddSelected).Click();
        }
        //To validate message while adding additional subject
        public string ValidateSubjectMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgSubject);
            return driver.FindElement(msgSubject).Text;
        }
        //To validate additional Subject in Table
        public string ValidateSubjectTableDetails()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(4);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubClose);
            driver.FindElement(btnSubClose).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, additionalSubject, 50);
            if (driver.FindElement(additionalSubject).Displayed)
                return "True";
            else
                return "False";
        }
        //To select interests of client and Save value
        public void selectClientInterest(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver,comboClientInterest);
            IWebElement comboInterest = driver.FindElement(comboClientInterest);
            CustomFunctions.SelectByValue(driver, comboInterest, ReadExcelData.ReadData(excelPath,"AddOpportunity",34));
            driver.FindElement(btnSaveClose).Click();
        }
        //To validate HL Internal Page title
        public string ValidateInternalTeamTitle()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, titleHLTeam, 40);
            IWebElement titleHLInternalTeam = driver.FindElement(titleHLTeam);
            return titleHLInternalTeam.Text;
        }
        //To enter team member details
        public void EnterStaffDetails(string file)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadData(excelPath,"AddOpportunity",14);
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSave).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
        }

        public void EnterStaffDetailsMultipleRows(string file, int row)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "DealTeamMembers", row, 1);
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSave).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
        }

        public void EnterMultipleStaffDetails(string file, int row, int row1, string recordType)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "DealTeamMembers", row, 1);
            string valStaff1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row1, 1);

            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);

            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
            driver.FindElement(txtStaff).SendKeys(valStaff1);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff2, valStaff1);
            Thread.Sleep(2000);

            if (row1 == 3 && recordType == "CF")
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkIntern1, 120);
                driver.FindElement(checkIntern1).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(4000);
            }
            else if (row1 == 3 && recordType == "FVA")
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkIntern, 120);
                driver.FindElement(checkIntern).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(4000);
            }
            else if (row1 == 3 && recordType == "FR")
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkIntern, 120);
                driver.FindElement(checkIntern).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(4000);
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 120);
                driver.FindElement(checkInitiator).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(4000);
            }
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
            Thread.Sleep(2000);
        }

        //To enter team member details
        public void EnterHLAndGCAStaffDetails(string file, int row)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            for (int col=1; col<=2; col++)
            {
                string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "DealTeamMembers", row, col);
                WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
                driver.FindElement(txtStaff).SendKeys(valStaff);
                Thread.Sleep(5000);
                if(col==1)
                {
                    CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
                }
                else if (col==2)
                {
                    CustomFunctions.SelectValueWithoutSelect(driver, listGCAMember, valStaff);
                }
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
                driver.FindElement(checkInitiator).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(10000);
                WebDriverWaits.WaitForPageToLoad(driver, 120);
            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
        }

        public void EnterMembersToDealTeam(string file)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            int rowCount = ReadExcelData.GetRowCount(excelPath, "RateSheetManagement");
            string valStaff = "";
            for (int row = 2; row <= rowCount; row++)
            {
                valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
                driver.FindElement(txtStaff).SendKeys(valStaff);
                Thread.Sleep(5000);

                By staff = By.XPath($"(/html/body/ul/li)[{row-1}]/a");
                CustomFunctions.SelectValueWithoutSelect(driver, staff, valStaff);
                Thread.Sleep(2000);

                switch(row)
                {
                    case 2:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
                        driver.FindElement(checkInitiator).Click();
                        WebDriverWaits.WaitUntilEleVisible(driver, checkPrincipal, 240);
                        driver.FindElement(checkPrincipal).Click();
                        break;
                    case 3:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkManager, 240);
                        driver.FindElement(checkManager).Click();
                        break;
                    case 4:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkSpecialty, 240);
                        driver.FindElement(checkSpecialty).Click();
                        break;
                    case 5:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkMarketing, 240);
                        driver.FindElement(checkMarketing).Click();
                        break;
                    case 6:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkSeller, 240);
                        driver.FindElement(checkSeller).Click();
                        break;
                    case 7:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkAssociate, 240);
                        driver.FindElement(checkAssociate).Click();
                        break;
                    case 8:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkAnalyst, 240);
                        driver.FindElement(checkAnalyst).Click();
                        break;
                    case 9:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkIntern, 240);
                        driver.FindElement(checkIntern).Click();
                        break;
                }

                driver.FindElement(btnSave).Click();
                Thread.Sleep(10000);
                WebDriverWaits.WaitForPageToLoad(driver, 120);
            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
            Thread.Sleep(5000);
        }


        //To validate additional added client in Additional Clients/Subjects section
        public string ValidateAddedClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyName, 50);
            Thread.Sleep(2000);
            string valCompany = driver.FindElement(linkCompanyName).Text;
            return valCompany;
        }
        //To validate additional added subject in additional Clients/Subjects section
        public string ValidateAddedSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSubjectName, 50);
            Thread.Sleep(2000);
            string valSubject = driver.FindElement(linkSubjectName).Text;
            return valSubject;
        }

        //To validate type of additional added client in Additional Clients/Subjects section
        public string ValidateTypeOfAddedClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyType, 50);
            Thread.Sleep(3000);
            string valCompany = driver.FindElement(txtCompanyType).Text;
            return valCompany;
        }

        //To validate rec type of additional added client in Additional Clients/Subjects section
        public string ValidateRecTypeOfAddedClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyRecType, 50);
            Thread.Sleep(2000);
            string valCompany = driver.FindElement(txtCompanyRecType).Text;
            return valCompany;
        }

        //To validate type of additional added subject in Additional Clients/Subjects section
        public string ValidateTypeOfAddedSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSubjectType, 40);
            Thread.Sleep(2000);
            string valType= driver.FindElement(txtSubjectType).Text;
            return valType;
        }

        //To validate rec type of additional added subject in Additional Clients/Subjects section
        public string ValidateRecTypeOfAddedSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSubjectRecType, 40);
            Thread.Sleep(2000);
            string valType = driver.FindElement(txtSubjectRecType).Text;
            return valType;
        }
        public void EnterStaffDetails(string file, int row)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 14);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 90);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSave).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
        }
    }
}

