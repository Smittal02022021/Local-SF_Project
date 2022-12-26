using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.Contact
{
    class ContactDetailsPage : BaseClass
    {
        ContactHomePage conHome = new ContactHomePage();
        ExtentReport extentReports = new ExtentReport();

        By txtEmail = By.CssSelector("input[id='con15']");
        By txtFirstName = By.CssSelector("input[id='name_firstcon2']");
        By txtLastName = By.CssSelector("input[id='name_lastcon2']");

        By contactRecordType = By.XPath("//*[text()='System Information']/.. //following-sibling::div //*[text()='Contact Record Type']/..//div[@id='RecordTypej_id0_j_id1_ileinner']");
        By btnNewRelationship = By.CssSelector("div[class='pbHeader'] input[value='New Relationship']");
        By headingRelationship = By.CssSelector("td[class='pbTitle'] h2");
        By defaultExternalContact = By.CssSelector("a[id*='D731P']");
        By errPage = By.CssSelector("span[id='theErrorPage:theError']");
        By comboRelationshipType = By.CssSelector("select[id*='j_id35']");
        By comboStrengthRating = By.CssSelector("select[id*='j_id39']");
        By btnSave = By.CssSelector("div.pbHeader input[value='Save']");
        By btnSave1 = By.CssSelector("input[title='Save']");
        By valCreatedContactName = By.CssSelector("div[id*='con2j_id0']");
        By txtSelectedCompanyName = By.CssSelector("div[id*='con4j_id0']");
        By lnkCreatedCompanyName = By.CssSelector("div[id*='con4j_id0'] > a");
        By btnDeleteOnCompanyPage = By.CssSelector("td[class='pbButton'] > input[value='Delete']");
        By txtHLContact = By.CssSelector("div[class='requiredInput'] > span input");
        By txtExternalContact = By.CssSelector("td[class='dataCol '] > span[class='lookupInput'] input");
        By lnkDeleteRelationship = By.CssSelector("td[class='actionColumn'] > a:nth-child(2)");
        By lnkHLContact = By.CssSelector("tr[class*='dataRow even last first'] td:nth-child(3) a");
        By txtExternalContactFromDetail = By.CssSelector("div[id='con2j_id0_j_id1_ileinner']");
        By txtStrengthRatingResult = By.CssSelector("div[id*='D731P_body'] > table > tbody > tr:nth-child(2) > td:nth-child(4)");
        By txtRelationshipTypeResult = By.CssSelector("div[id*='_body']>table>tbody>tr:nth-child(2)>td:nth-child(9)");
        By btnAddActivity = By.CssSelector("a[id*='btnAddActivity']");
        By comboAcitivityType = By.CssSelector("select[id*='j_id31:eventType']");
        By txtActivitySubject = By.CssSelector("div[class='requiredInput'] > input[id*='j_id53:eventSubject']");
        By btnActivitySave = By.CssSelector("td[class='pbButton '] > input:nth-child(1)");
        By btnReturnToContactDetail = By.CssSelector("td[class='pbButton '] > input[value='Return']");
        By lnkActivities = By.CssSelector("span[id='activityList_link']");
        By lnkViewActivity = By.CssSelector("span[id*='0:j_id18']>a:nth-of-type(1)");
        By lnkEditActivity = By.CssSelector("td[class='dataCell top '] > span[id*='pbtActivities:0:j_id18'] a:nth-child(2)");
        By txtActivityType = By.CssSelector("span[id$='id41:j_id42']");
        By txtActivitySubjectOnDetail = By.CssSelector("td[class='data2Col '] > span[id*='j_id41:j_id43']");
        By btnDeleteActivity = By.CssSelector("td[class='pbButton '] > input[value = 'Delete']");
        By txtNoActivity = By.CssSelector("span[id*='pbActivityLog:noActivities'] center h2");
        By chkNoExternalContact = By.CssSelector("input[id*='j_id113:chkNoExtnContact']");
        By txtAddCompanyDiscussed = By.CssSelector("input[id='j_id0:frmActivityEvent:activityedit:pbsCompaniesDiscussed:j_id181:inputTxtId']");
        By selAddCompanyDiscussed = By.XPath("//*[contains(text(),'Lookup Company')]//ancestor::body//ul[3]");
        By txtCompaniesPageTitle = By.CssSelector("h2[class='pageDescription']");
        By btnDeleteContact = By.CssSelector("td[class='pbButton'] > input[value='Delete']");
        By valContactMailingAddress = By.CssSelector("div[id*='con19j_id0']");
        By valContactStatus = By.CssSelector("div[id*='00Ni000000D7LLC']");
        By selectContactStatus = By.CssSelector("select[id*='00Ni000000D7LLC']");
        By valContactOffice = By.CssSelector("div[id*='00Ni000000Fjq9rj']");
        By selectContactOffice = By.CssSelector("select[id*='00Ni000000Fjq9r']");
        By valContactPhysicalOffice = By.CssSelector("div[id*='00N3100000Gb67T']");
        By selectContactPhysicalOffice = By.CssSelector("select[id*='00N3100000Gb67T']");
        By valContactTitle = By.CssSelector("div[id*='con5j_id0']");
        By inputContactTitle = By.CssSelector("input[id*='con5']");
        By valContactDepartment = By.CssSelector("div[id*='con6j_id0']");
        By inputContactDepartment = By.CssSelector("input[id*='con6']");
        By selectExpenseApplication = By.CssSelector("select[id*='00N3100000Gb675']");
        By inputHireDate = By.CssSelector("input[id*='00N3100000Gb67A']");
        By activityFollowUpDesc = By.CssSelector("span[id*='j_id40:j_id46']");
        By activityTypeFollowup = By.CssSelector("span[id*='pbtActivities:0:j_id38'] >label");
        By btnNewAffiliation = By.CssSelector("input[value='New Affiliation']");
        By headingNewAffiliate = By.CssSelector("h2[class='pageDescription']");
        By AffiliationCompanyRecord = By.CssSelector("div[id*='00Ni000000D735q_body'] > table > tbody > tr:nth-child(2)");
        By AffiliationCompany = By.CssSelector("div[id*='00Ni000000D735q_body'] > table > tbody > tr");
        By linkDeleteAffiliationCompany = By.CssSelector("div[id*='00Ni000000D735q_body'] > table  > tbody > tr:nth-child(2) > td[class='actionColumn'] > a:nth-child(2)");
        By linkEditAffiliationCompany = By.CssSelector("div[id*='00Ni000000D735q_body'] > table  > tbody > tr:nth-child(2) > td[class='actionColumn'] > a:nth-child(1)");
        By valAffiliationCompanyName = By.CssSelector("div[id*='00Ni000000D735q_body'] > table >tbody > tr:nth-child(2) > td:nth-child(3)>a");
        By valAffiliationCompanyStatus = By.CssSelector("div[id*='00Ni000000D735q_body']  > table >tbody > tr:nth-child(2) > td:nth-child(5)");
        By valAffiliationCompanyType = By.CssSelector("div[id*='00Ni000000D735q_body']  > table >tbody > tr:nth-child(2) > td:nth-child(6)");
        By btnSortableView = By.CssSelector("input[value='Sortable View']");
        By valContactDetailHeading = By.CssSelector("div[class*='bDetailBlock'] td[class='pbTitle'] > h2");
        By linkCompanyName = By.XPath("//*[text()='Company Name']/.. //td[2]//div//a");
        By txtHLAttendee = By.CssSelector("input[id*='inputEmployeeId'][type='text']");
        By selHLAttendee = By.XPath("/html/body/ul[7]/li");
        By txtActivityStartDate = By.CssSelector("input[id*='startDate']");
        By comboActivityStartTime = By.CssSelector("select[id*='startTime']");
        By comboActivityEndTime = By.CssSelector("select[id*='endTime']");
        By chkHLAttendeePrimary = By.CssSelector("tbody[id*='pbsHLEmployees'] > tr:nth-child(2) > td:nth-child(2) > input");
        By valName = By.XPath("//*[text()='Name']/.. //td[2]//div");
        By valEmail = By.CssSelector("div[id=con15j_id0_j_id1_ileinner] > a");
        By valLegalEntity = By.CssSelector("div[id*='M0ebMj'] > a");
        By valRevenueAllocation = By.CssSelector("div[id*='GbDqzj']");
        By activitiesList = By.CssSelector("tbody[id*='pbActivityLog:pbtActivities:tb'] > tr");
        By tabHome = By.CssSelector("a[title='Home Tab']");
     // By btnTodaysActivities = By.CssSelector("#j_id0\\:j_id1\\:j_id2\\:j_id3\\:pbActivityLog\\:j_id9\\:j_id10");//("input[id*='j_id9:j_id10']");
        By btnTodaysActivities = By.CssSelector("#j_id0\\:j_id1\\:j_id2\\:j_id3\\:pbActivityLog\\:j_id9\\:j_id10");
        By valTodayActivityType = By.CssSelector("td[id*='0:j_id37'] > span>label");
        By valTodayActivitySubject = By.CssSelector("span[id*='0:j_id73'] > label");
        By btnUpcomingActivities = By.CssSelector("input[id*='j_id9:j_id11']");
        By txtInternalNotes = By.CssSelector("textarea[name*='activitydetails:j_id56']");
        By txtExternalContactActivityPage = By.CssSelector("input[id$='inputContactId']");
        By txtExistingExternalContactActivityPage = By.CssSelector("td[id*='j_id103']>a");//
        By btnMergeContacts = By.CssSelector("input[value='Merge Contacts']");
        By txtStep1MergeContact = By.CssSelector("div[class*='brandTertiaryBgr']>h2");
        //By checkboxFirstContact = By.CssSelector("input[value='00379000001Qo5d']");
        //By checkboxSecondContact = By.CssSelector("input[value = '00379000001Qo6W']");

       // By checkboxFirstContact = By.XPath("//td[text()='testsummer@email.com']//parent::tr/th/input");
        //By checkboxSecondContact = By.XPath("//td[text()='testwinter@email.//com']//parent::tr/th/input");
        By checkboxFirstContact = By.XPath("//input[@title='Select row 3']");
        By checkboxSecondContact = By.XPath("//input[@title='Select row 4']");
        By btnNext = By.CssSelector("input[title='Next']");
        By btnMerge = By.CssSelector("input[title = 'Merge']");
        By btnEdit = By.CssSelector("input[title='Edit']");

        By valDoNotSolicitChangeDate = By.CssSelector("div[id='00Ni000000F2ON9j_id0_j_id1_ileinner']");
        By valDoNotEmailChangeDate = By.CssSelector("div[id='00Ni000000F2OMzj_id0_j_id1_ileinner']");
        By inputDoNotSolicitChangeDate = By.CssSelector("input[id='00Ni000000F2ON9']");
        By inputDoNotEmailChangeDate = By.CssSelector("input[id='00Ni000000F2OMz']");

        By imgDoNotSolicit = By.XPath("//*[contains(text(),'Do Not Solicit')]/following::td/div/img");
        By imgManualOptOut = By.XPath("//*[contains(text(),'Manual Opt Out')]/following::td/div/img");

        By checkboxDoNotSolicit = By.XPath("//*[contains(text(),'Do Not Solicit')]/following::td/input");
        By checkboxManualOptOut = By.XPath("//*[contains(text(),'Manual Opt Out')]/following::td/input");

        By lblJobCode = By.XPath("//*[text()='Job Code']");
        By lblJobCodeValue = By.XPath("//*[text()='Job Code']/following::div[@id='00N8L000000OH2Rj_id0_j_id1_ileinner']");

        By btnAddToCampaignHistory = By.XPath("//input[@name='addCampaign']");
        By txtSearchCampaign = By.XPath("//input[@id='lksrch']");
        By btnGo = By.XPath("//input[@type='submit']");
        By linkCampaignResult = By.XPath("//tr[@class='dataRow even last first']/th/a");


        By btnNewContactSector = By.XPath("//input[@value='New Contact Sector']");
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By imgCoverageSectorDependencies = By.CssSelector("img[alt = 'Coverage Sector Dependencies']");
        By imgCoverageSectorDependencyLookUp = By.XPath("//img[@alt='Coverage Sector Dependency Lookup (New Window)']");
        By txtSearchBox = By.XPath("//input[@title='Go!']/preceding::input[1]");
        By linkCoverageSectorDependencyName = By.XPath("//a[@href='#']");
        By btnSaveContactSector = By.XPath("(//input[@title='Save'])[1]");
        By btnDeleteContactSector = By.XPath("(//input[@title='Delete'])[1]");
        By valContactSectorName = By.XPath("//td[contains(text(),'Contact Sector')]/following::div[1]");
        By linkContactName = By.XPath("(//td[contains(text(),'Contact')])[2]/../td[2]/div/a");
        By linkSectorName = By.XPath("//table/tbody/tr[2]/th/a");
        By txtCoverageType = By.CssSelector("input[id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By txtPrimarySector = By.CssSelector("input[id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By txtSecondarySector = By.CssSelector("input[id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By txtTertiarySector = By.CssSelector("input[id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By linkShowAllResults = By.XPath("//a[contains(text(),'Show all results')]");
        By linkContactSector = By.XPath("//span[@class='count'][contains(text(),'0')]/preceding::span[contains(text(),'Contact Sectors')]");
        By inputCoverageType = By.XPath("//input[@id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By inputPrimarySector = By.XPath("//input[@id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By inputSecondarySector = By.XPath("//input[@id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By inputTertiarySector = By.XPath("//input[@id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By btnApplyFilters = By.XPath("//input[@title='Apply Filters']");
        By btnEditCompCoverageSector = By.XPath("//input[@title='Edit']");

        public bool VerifyIfContactSectorQuickLinkIsDisplayed()
        {
            bool result = false;
            if (driver.FindElement(linkContactSector).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void ClickNewContactSectorButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContactSector, 120);
            driver.FindElement(btnNewContactSector).Click();
            Thread.Sleep(2000);
        }

        public void SelectCoverageSectorDependency(string covSectorDependencyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            //Enter dependency name
            driver.FindElement(txtSearchBox).SendKeys(covSectorDependencyName);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);

            driver.SwitchTo().DefaultContent();

            //Enter results frame & click on the result
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);
            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(4000);

            //Switch back to original window
            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public void SelectCoverageSectorDependencyForContactSector(string covSectorDependencyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            //Enter dependency name
            driver.FindElement(txtSearchBox).SendKeys(covSectorDependencyName);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);

            driver.SwitchTo().DefaultContent();

            //Enter results frame & click on the result
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);

            driver.FindElement(linkShowAllResults).Click();
            Thread.Sleep(2000);
            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(4000);

            //Switch back to original window
            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public bool VerifyFiltersFunctionalityOnCoverageSectorDependencyPopUp(string file, string covSectorDependencyName)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Click Edit button on Company Sector detail page 
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditCompCoverageSector, 120);
            driver.FindElement(btnEditCompCoverageSector).Click();
            Thread.Sleep(2000);

            //Click on Coverage Sector Dependency LookUp icon
            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            //Clear Search box
            driver.FindElement(txtSearchBox).Clear();

            driver.SwitchTo().DefaultContent();

            //Enter results frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);

            driver.FindElement(linkShowAllResults).Click();
            Thread.Sleep(2000);

            //Enter filter values
            driver.FindElement(inputCoverageType).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 1));
            driver.FindElement(inputPrimarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 2));
            driver.FindElement(inputSecondarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 3));
            driver.FindElement(inputTertiarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 4));

            //Click on Apply filters button
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000);

            if (driver.FindElement(linkCoverageSectorDependencyName).Text == covSectorDependencyName)
            {
                //Select the desired dependency name from the result
                driver.FindElement(linkCoverageSectorDependencyName).Click();
                Thread.Sleep(4000);

                //Switch back to original window
                CustomFunctions.SwitchToWindow(driver, 0);

                result = true;
            }
            return result;
        }

        public void SelectCoverageSectorDependencyByFilters(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter results frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);

            //Apply filters
            driver.FindElement(txtCoverageType).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 1));
            driver.FindElement(txtPrimarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 2));
            driver.FindElement(txtSecondarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 3));
            driver.FindElement(txtTertiarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 4));
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000);

            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(4000);

            //Switch back to original window
            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public void SaveNewContactSectorDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveContactSector, 120);
            driver.FindElement(btnSaveContactSector).Click();
            Thread.Sleep(2000);
        }

        public string GetContactSectorName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactSectorName, 120);
            string name = driver.FindElement(valContactSectorName).Text;
            return name;
        }

        public void NavigateToContactDetailPageFromContactSectorDetailPage()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkContactName, 120);
            driver.FindElement(linkContactName).Click();
            Thread.Sleep(2000);
        }

        public void NavigateToCoverageSectorDependenciesPage()
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgCoverageSectorDependencies).Click();
            Thread.Sleep(2000);
        }

        public bool VerifyContactSectorAddedToContactOrNot(string sectorName)
        {
            Thread.Sleep(5000);
            bool result = false;
            if (driver.FindElement(linkSectorName).Text == sectorName)
            {
                result = true;
            }
            return result;
        }

        public void DeleteContactSector()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSectorName, 120);
            driver.FindElement(linkSectorName).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteContactSector, 120);
            driver.FindElement(btnDeleteContactSector).Click();
            Thread.Sleep(2000);

            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(3000);
        }

        //Verify if both DoNotSolicitChangeDate & DoNotEmailChangeDate fields are empty or not
        public bool VerifyIfDoNotSolicitChangeDateAndDoNotEmailChangeDateFieldsAreEmptyOrNot()
        {
            bool result = false;
            if (driver.FindElement(valDoNotSolicitChangeDate).Text == " " && driver.FindElement(valDoNotEmailChangeDate).Text == " ")
            {
                result = true;
            }
            return result;
        }

        //Verify if both DoNotSolicit & ManualOptOut fields are checked or not
        public bool VerifyIfDoNotSolicitAndManualOptOutFieldsAreCheckedOrNot()
        {
            bool result = false;
            if (driver.FindElement(imgDoNotSolicit).GetAttribute("title") == "Not Checked" && driver.FindElement(imgManualOptOut).GetAttribute("title") == "Not Checked")
            {
                result = true;
            }
            return result;
        }

        public void UpdateBothDoNotSolicitChangeDateFields()
        {
            driver.FindElement(inputDoNotSolicitChangeDate).SendKeys(DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/'));
            driver.FindElement(inputDoNotEmailChangeDate).SendKeys(DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/'));
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave1, 120);
            driver.FindElement(btnSave1).Click();

            Thread.Sleep(4000);
        }

        public void UpdateBothDoNotSolicitAndManualOptOutCheckboxes()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkboxDoNotSolicit, 120);
            driver.FindElement(checkboxDoNotSolicit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, checkboxManualOptOut, 120);
            driver.FindElement(checkboxManualOptOut).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave1, 120);
            driver.FindElement(btnSave1).Click();

            Thread.Sleep(4000);
        }

        // Validate Created ContactRecordType value
        public string GetContactRecordTypeValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, contactRecordType, 120);
            string valContactRecord = driver.FindElement(contactRecordType).Text.Split('[')[0].Trim();
            return valContactRecord;
        }

        public void ClickEditContactButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
        }

        public void UpdateNameAndEmailAddress(string email, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName, 120);
            driver.FindElement(txtFirstName).Clear();
            driver.FindElement(txtFirstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));

            WebDriverWaits.WaitUntilEleVisible(driver, txtLastName, 120);
            driver.FindElement(txtLastName).Clear();
            driver.FindElement(txtLastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));

            WebDriverWaits.WaitUntilEleVisible(driver, txtEmail, 120);
            driver.FindElement(txtEmail).Clear();
            driver.FindElement(txtEmail).SendKeys(email);

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave1, 120);
            driver.FindElement(btnSave1).Click();

            Thread.Sleep(2000);
        }

        public void UpdateHLContactDetails(string firstName, string lastName, string email, string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, txtFirstName, 120);
            driver.FindElement(txtFirstName).Clear();
            driver.FindElement(txtFirstName).SendKeys(firstName);

            WebDriverWaits.WaitUntilEleVisible(driver, txtLastName, 120);
            driver.FindElement(txtLastName).Clear();
            driver.FindElement(txtLastName).SendKeys(lastName);

            WebDriverWaits.WaitUntilEleVisible(driver, txtEmail, 120);
            driver.FindElement(txtEmail).Clear();
            driver.FindElement(txtEmail).SendKeys(email);

            driver.FindElement(selectContactStatus).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 12));
            driver.FindElement(selectContactOffice).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 13));
            driver.FindElement(selectContactPhysicalOffice).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 14));
            driver.FindElement(inputContactTitle).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 15));
            driver.FindElement(inputContactDepartment).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 16));
            driver.FindElement(selectExpenseApplication).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 17));
            driver.FindElement(inputHireDate).SendKeys(DateTime.Now.ToString("MM/dd/yyyy").Replace('-','/'));

            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave1, 120);
            driver.FindElement(btnSave1).Click();

            Thread.Sleep(4000);
        }

        public string GetRevenueAllocationValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueAllocation, 120);
            string valueRevenueAllocation = driver.FindElement(valRevenueAllocation).Text;
            return valueRevenueAllocation;
        }

        public void ClickNewRelationshipButton()
        {
            Thread.Sleep(2000);
            CustomFunctions.ActionClicks(driver, btnNewRelationship, 20);
        }

        public void ClickCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyName);
            driver.FindElement(linkCompanyName).Click();
            Thread.Sleep(3000);

        }
        public string GetRelationshipPageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headingRelationship, 60);
            string headingcontactDetail = driver.FindElement(headingRelationship).Text;
            return headingcontactDetail;
        }
        public string GetCompanyNameFromExcel(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valCompanyName = ReadExcelData.ReadData(excelPath, "Contact", 1);
            return valCompanyName;
        }

        public string GetFirstNameFromExcel(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valFirstName = ReadExcelData.ReadData(excelPath, "Contact", 2);
            return valFirstName;
        }

        public string GetLastNameFromExcel(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valLastName = ReadExcelData.ReadData(excelPath, "Contact", 3);
            return valLastName;
        }
        public string GetMiddleNameFromExcel(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valLastName = ReadExcelData.ReadData(excelPath, "Contact", 5);
            return valLastName;
        }

        public string GetFirstAndLastName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCreatedContactName, 60);
            string contactName = driver.FindElement(valCreatedContactName).Text;
            return contactName;
        }

        public string GetCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSelectedCompanyName, 60);
            string companyName = driver.FindElement(txtSelectedCompanyName).Text;
            return companyName;
        }

        public bool ValidateDefaultExternalContact()
        {
            return CustomFunctions.IsElementPresent(driver, defaultExternalContact);
        }

        // Create relationship from contact
        public void CreateRelationship(string file, string contact)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtHLContact);
            driver.FindElement(txtHLContact).SendKeys(ReadExcelData.ReadData(excelPath, "Relationship", 1));

            CustomFunctions.SelectByText(driver, driver.FindElement(comboRelationshipType), ReadExcelData.ReadData(excelPath, "Relationship", 2));
            CustomFunctions.SelectByText(driver, driver.FindElement(comboStrengthRating), ReadExcelData.ReadData(excelPath, "Relationship", 3));
            if (contact != null)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtExternalContact);
                driver.FindElement(txtExternalContact).SendKeys(ReadExcelData.ReadData(excelPath, "Relationship", 4));
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }


        public string GetHLContactLinkText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkHLContact, 60);
            string HLContactRelationshipName = driver.FindElement(lnkHLContact).Text;
            return HLContactRelationshipName;
        }

        public string GetExternalContactText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtExternalContactFromDetail, 60);
            string ExternalContactRelationshipName = driver.FindElement(txtExternalContactFromDetail).Text;
            return ExternalContactRelationshipName;
        }
        public string GetStrengthRating()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtStrengthRatingResult, 60);
            string SelectedStrengthRating = driver.FindElement(txtStrengthRatingResult).Text;
            return SelectedStrengthRating;
        }

        public string GetRelationshipType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtRelationshipTypeResult, 60);
            string SelectedRelationshipType = driver.FindElement(txtRelationshipTypeResult).Text;
            return SelectedRelationshipType;
        }

        // Delete Relationship
        public void DeleteCreatedRelationship()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteRelationship);
            driver.FindElement(lnkDeleteRelationship).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        public void SearchAndSelectCampaignName(string file)
        {
            Thread.Sleep(5000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//select")).SendKeys("All Campaigns");
            driver.FindElement(txtSearchCampaign).SendKeys(ReadExcelData.ReadData(excelPath, "Campaign", 2));
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);
            driver.FindElement(linkCampaignResult).Click();
            Thread.Sleep(4000);
            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public void CreateAnActivity1(string file, string ContactType, string startDate)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(2000);
            driver.SwitchTo().Frame("066i0000004ZLbF");
            driver.FindElement(btnAddActivity).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            string excelPath = dir + file;

            //Enter Activity Type
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 1));
            Thread.Sleep(3000);
            //Enter Activity Subject
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 2));

            //Select Activity Start Date
            driver.FindElement(txtActivityStartDate).Clear();
            driver.FindElement(txtActivityStartDate).SendKeys(startDate);

            //Select Activity Start time
            CustomFunctions.SelectByIndex(driver, driver.FindElement(comboActivityStartTime), 18);
            //Select Activity End Time 
            CustomFunctions.SelectByIndex(driver, driver.FindElement(comboActivityEndTime), 37);

            if (ContactType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 3, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtExternalContactActivityPage, 3000);
                driver.FindElement(txtExternalContactActivityPage).SendKeys("Test External");
                WebDriverWaits.WaitUntilEleVisible(driver, By.XPath("/html/body/ul[6]/li/a"), 30000);
                driver.FindElement(By.XPath("/html/body/ul[6]/li/a")).Click();
                // Add HL Attendee in case of internal contact activity
                string HLAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 2) + " " + ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 3);
                WebDriverWaits.WaitUntilEleVisible(driver, txtHLAttendee);
                driver.FindElement(txtHLAttendee).SendKeys(HLAttendee);
                Thread.Sleep(3000);
                driver.FindElement(selHLAttendee).Click();
                Thread.Sleep(4000);

                //Make new selected HLAttendee as primary by clicking on checkbox
                WebDriverWaits.WaitUntilEleVisible(driver, chkHLAttendeePrimary);
                driver.FindElement(chkHLAttendeePrimary).Click();

                //Add company discussed
                WebDriverWaits.WaitUntilEleVisible(driver, txtAddCompanyDiscussed);
                driver.FindElement(txtAddCompanyDiscussed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 1));
                Thread.Sleep(3000);
                CustomFunctions.SelectValueWithoutSelect(driver, selAddCompanyDiscussed, "Houlihan");
                Thread.Sleep(4000);
            }
            else if (ContactType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 4, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkNoExternalContact);
                driver.FindElement(chkNoExternalContact).Click();

                // Add HL Attendee in case of internal contact activity
                string HLAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 4, 2) + " " + ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 4, 3);
                WebDriverWaits.WaitUntilEleVisible(driver, txtHLAttendee);
                driver.FindElement(txtHLAttendee).SendKeys(HLAttendee);
                Thread.Sleep(3000);
                driver.FindElement(selHLAttendee).Click();
                Thread.Sleep(4000);

                //Make new selected HLAttendee as primary by clicking on checkbox
                WebDriverWaits.WaitUntilEleVisible(driver, chkHLAttendeePrimary);
                driver.FindElement(chkHLAttendeePrimary).Click();

                //Add company discussed
                WebDriverWaits.WaitUntilEleVisible(driver, txtAddCompanyDiscussed);
                driver.FindElement(txtAddCompanyDiscussed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 1));
                Thread.Sleep(3000);
                CustomFunctions.SelectValueWithoutSelect(driver, selAddCompanyDiscussed, "Houlihan");
                Thread.Sleep(4000);
            }
            //Click on save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();

            //CLick on return button 
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToContactDetail);
            driver.FindElement(btnReturnToContactDetail).Click();
            Thread.Sleep(4000);
        }
        public void CreateAnActivity(string file, string ContactType,string startDate)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(2000);
            driver.SwitchTo().Frame("066i0000004ZLbF");
            driver.FindElement(btnAddActivity).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            string excelPath = dir + file;

            //Enter Activity Type
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 1));
            Thread.Sleep(3000);
            //Enter Activity Subject
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 2));

            //Select Activity Start Date
            driver.FindElement(txtActivityStartDate).Clear();
            driver.FindElement(txtActivityStartDate).SendKeys(startDate);

            //Select Activity Start time
            CustomFunctions.SelectByIndex(driver, driver.FindElement(comboActivityStartTime), 18);
            //Select Activity End Time 
            CustomFunctions.SelectByIndex(driver, driver.FindElement(comboActivityEndTime), 37);

            if (ContactType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 3, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtExternalContactActivityPage, 3000);
                driver.FindElement(txtExternalContactActivityPage).SendKeys("Test External");
                WebDriverWaits.WaitUntilEleVisible(driver, By.XPath("/html/body/ul[6]/li/a"), 30000);
                driver.FindElement(By.XPath("/html/body/ul[6]/li/a")).Click();
                // Add HL Attendee in case of internal contact activity
                string HLAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 2) + " " + ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 3);
                WebDriverWaits.WaitUntilEleVisible(driver, txtHLAttendee);
                driver.FindElement(txtHLAttendee).SendKeys(HLAttendee);
                Thread.Sleep(3000);
                driver.FindElement(selHLAttendee).Click();
                Thread.Sleep(4000);

                //Make new selected HLAttendee as primary by clicking on checkbox
                WebDriverWaits.WaitUntilEleVisible(driver, chkHLAttendeePrimary);
                driver.FindElement(chkHLAttendeePrimary).Click();

                //Add company discussed
                WebDriverWaits.WaitUntilEleVisible(driver, txtAddCompanyDiscussed);
                driver.FindElement(txtAddCompanyDiscussed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 1));
                Thread.Sleep(3000);
                CustomFunctions.SelectValueWithoutSelect(driver, selAddCompanyDiscussed, "Houlihan");
                Thread.Sleep(4000);
            }
            else if (ContactType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 4, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkNoExternalContact);
                driver.FindElement(chkNoExternalContact).Click();

                // Add HL Attendee in case of internal contact activity
                string HLAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 4, 2) + " " + ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 4, 3);
                WebDriverWaits.WaitUntilEleVisible(driver, txtHLAttendee);
                driver.FindElement(txtHLAttendee).SendKeys(HLAttendee);
                Thread.Sleep(3000);
                driver.FindElement(selHLAttendee).Click();
                Thread.Sleep(4000);

                //Make new selected HLAttendee as primary by clicking on checkbox
                WebDriverWaits.WaitUntilEleVisible(driver, chkHLAttendeePrimary);
                driver.FindElement(chkHLAttendeePrimary).Click();

                //Add company discussed
                WebDriverWaits.WaitUntilEleVisible(driver, txtAddCompanyDiscussed);
                driver.FindElement(txtAddCompanyDiscussed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 3, 1));
                Thread.Sleep(3000);
                CustomFunctions.SelectValueWithoutSelect(driver, selAddCompanyDiscussed, "Houlihan");
                Thread.Sleep(4000);
            }
            //Click on save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();

            //CLick on return button 
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToContactDetail);
            driver.FindElement(btnReturnToContactDetail).Click();
            Thread.Sleep(4000);
        }

        
        public void VerifyPrivateActivityDetail()
        {
            Thread.Sleep(2000);
            driver.SwitchTo().Frame("066i0000004ZLbF");
        }

        public int ActivityListCount()
        {
            Thread.Sleep(3000);
            driver.SwitchTo().Frame("066i0000004ZLbF");

            IList<IWebElement> availableRecipient = driver.FindElements(activitiesList);
            return availableRecipient.Count;
        }

        public string GetActivityType(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(3) > span"), 60);
            string activityType = driver.FindElement(By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(3) > span")).Text;
            return activityType;
        }

        public string GetEditLink(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(1) > span > a:nth-child(2)"), 60);
            string editVal = driver.FindElement(By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(1) > span > a:nth-child(2)")).Text;
            return editVal;
        }

        public string GetActivitySubject(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(6) > span"), 60);
            string activitySubject = driver.FindElement(By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(6) > span")).Text;
            return activitySubject;
        }

        public string GetActivityDesc(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(7) > span"), 60);
            string activityDesc = driver.FindElement(By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(7) > span")).Text;
            return activityDesc;
        }

        public string GetViewOrPrint(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(1) > span"), 60);
            string activityDesc = driver.FindElement(By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(1) > span")).Text;
            return activityDesc;
        }

        public void ReturnToContactDetailsFromActivityDetails()
        {
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToContactDetail);
            driver.FindElement(btnReturnToContactDetail).Click();
        }
        public void ViewExistingActivity()
        {
            CustomFunctions.MouseOver(driver, lnkActivities);
            driver.SwitchTo().Frame("iframeContentId");
            if (CustomFunctions.IsElementPresent(driver, lnkViewActivity))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkViewActivity);
                driver.FindElement(lnkViewActivity).Click();
            }
            driver.SwitchTo().DefaultContent();
        }

        public void VerifyTodaysActivity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabHome, 60);
            driver.FindElement(tabHome).Click();
            driver.SwitchTo().Frame("066i0000004Z1AR");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnTodaysActivities);
            driver.FindElement(btnTodaysActivities).Click();
        }

        public void VerifyUpcomingActivity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabHome, 60);
            driver.FindElement(tabHome).Click();
            driver.SwitchTo().Frame("066i0000004Z1AR");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnUpcomingActivities);
            driver.FindElement(btnUpcomingActivities).Click();
        }

        public string GetActivityTypeText()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, txtActivityType, 60);
            //driver.Navigate().Refresh();
            string SelectedActivityType = driver.FindElement(txtActivityType).Text;
            return SelectedActivityType;
        }

        public string GetTodayActivityTypeText()
        {
            driver.Navigate().Refresh();

            driver.SwitchTo().Frame("066i0000004Z1AR");
            WebDriverWaits.WaitUntilEleVisible(driver, valTodayActivityType, 60);
            string TodayActivityType = driver.FindElement(valTodayActivityType).Text;
            return TodayActivityType;
        }

        public string GetTodayActivitySubjectText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTodayActivitySubject, 60);
            string TodayActivitySubject = driver.FindElement(valTodayActivitySubject).Text;
            return TodayActivitySubject;
        }

        public string GetActivitySubjectText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubjectOnDetail, 60);
            string EnteredActivitySubjectLine = driver.FindElement(txtActivitySubjectOnDetail).Text;
            return EnteredActivitySubjectLine;
        }

        public void EditExistingActivity(string file,int num)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(4000);
            CustomFunctions.MouseOver(driver, lnkActivities);
            driver.SwitchTo().Frame("iframeContentId");

            //WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(3) > span"), 60);
            //string activityType = driver.FindElement(By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(3) > span")).Text;
            //return activityType;
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(1) > span > a:nth-child(2)"),60);
            driver.FindElement(By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(1) > span > a:nth-child(2)")).Click();
            driver.SwitchTo().DefaultContent();

            Thread.Sleep(2000);
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 3, 1));
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(Keys.Control + "a");
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 3, 2));
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();
        }

        public void EditExistingActivities(string file, int num)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(1) > span > a:nth-child(2)"), 60);
            driver.FindElement(By.CssSelector($"tbody[id*='pbActivityLog:pbtActivities:tb'] > tr:nth-child({num}) > td:nth-child(1) > span > a:nth-child(2)")).Click();
            driver.SwitchTo().DefaultContent();

            Thread.Sleep(2000);
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(comboAcitivityType), ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 3, 1));
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubject);
            driver.FindElement(txtActivitySubject).SendKeys(Keys.Control + "a");
            driver.FindElement(txtActivitySubject).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 3, 2));
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();
        }

        public void EditExistingActivityInternalNotes(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(4000);
            CustomFunctions.MouseOver(driver, lnkActivities);
            driver.SwitchTo().Frame("iframeContentId");

            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditActivity);
            driver.FindElement(lnkEditActivity).Click();
            driver.SwitchTo().DefaultContent();

            Thread.Sleep(5000);
            string excelPath = dir + file;
            driver.FindElement(txtInternalNotes).SendKeys(Keys.Control + "a");

            driver.FindElement(txtInternalNotes).SendKeys(ReadExcelData.ReadData(excelPath, "Activity", 9));
            // Console.WriteLine(driver.FindElement(txtExternalContactActivityPage).GetAttribute("value"));
            
            /*if ((driver.FindElement(txtExistingExternalContactActivityPage)).Equals(null))

            {
                driver.FindElement(txtExternalContactActivityPage).SendKeys("Test External");

                WebDriverWaits.WaitUntilEleVisible(driver, By.XPath("/html/body/ul[6]/li/a"), 30000);
                driver.FindElement(By.XPath("/html/body/ul[6]/li/a")).Click();
            }
                      
            else if ((driver.FindElement(txtExistingExternalContactActivityPage)) != null)
            {

            }*/
            
            //Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitySave);
            driver.FindElement(btnActivitySave).Click();
        }

        public void DeleteActivity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteActivity);
            driver.FindElement(btnDeleteActivity).Click();

            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(4000);
        }

        public string GetNoActivityDisplayText()
        {
            CustomFunctions.MouseOver(driver, lnkActivities);
            driver.SwitchTo().Frame("iframeContentId");
            WebDriverWaits.WaitUntilEleVisible(driver, txtNoActivity, 60);
            string NoActivityDisplay = driver.FindElement(txtNoActivity).Text;

            return NoActivityDisplay;
        }

        public void DeleteNewCompanyCreatedWithContact(string file)
        {
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                conHome.SearchContact(file);
            }
            CustomFunctions.ActionClicks(driver, lnkCreatedCompanyName, 20);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteOnCompanyPage);
            driver.FindElement(btnDeleteOnCompanyPage).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        //Function to get companies page title
        public string GetCompaniesPageTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompaniesPageTitle, 60);
            string EnteredActivitySubjectLine = driver.FindElement(txtCompaniesPageTitle).Text;
            return EnteredActivitySubjectLine;
        }

        //Function to get companies page title
        public string GetContactDetailTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactDetailHeading, 60);
            string EnteredActivitySubjectLine = driver.FindElement(valContactDetailHeading).Text;
            return EnteredActivitySubjectLine;
        }

        //Function to get contact mailing address
        public string GetContactCompleteAddress()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactMailingAddress, 60);
            string contactAddress = driver.FindElement(valContactMailingAddress).Text;
            return contactAddress;
        }

        //Function to delete created contact
        public void DeleteCreatedContact(string file, string contactType)
        {
            /*
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                conHome.SearchContact(file, contactType);
            }
            */
            conHome.SearchContact(file, contactType);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteContact);
            driver.FindElement(btnDeleteContact).Click();
            IAlert alert = driver.SwitchTo().Alert();
            Thread.Sleep(500);
            alert.Accept();
            Thread.Sleep(2000);
        }

        //Function to delete duplicate contact
        public void DeleteDuplicateContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteContact);
            driver.FindElement(btnDeleteContact).Click();
            IAlert alert = driver.SwitchTo().Alert();
            Thread.Sleep(500);
            alert.Accept();
            Thread.Sleep(2000);
        }

        public string GetContactStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactStatus, 60);
            string contactStatus = driver.FindElement(valContactStatus).Text;
            return contactStatus;
        }

        public string GetContactOffice()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactOffice, 60);
            string contactOffice = driver.FindElement(valContactOffice).Text;
            return contactOffice;
        }

        public string GetContactPhysicalOffice()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactPhysicalOffice, 60);
            string contactPhysicalOffice = driver.FindElement(valContactPhysicalOffice).Text;
            return contactPhysicalOffice;
        }

        public string GetContactTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactTitle, 60);
            string contactTitle = driver.FindElement(valContactTitle).Text;
            return contactTitle;
        }

        public string GetContactDepartment()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactDepartment, 60);
            string contactDept = driver.FindElement(valContactDepartment).Text;
            return contactDept;
        }

        public string GetFollowUpActivityType()
        {
            CustomFunctions.MouseOver(driver, lnkActivities);
            driver.SwitchTo().Frame("iframeContentId");

            WebDriverWaits.WaitUntilEleVisible(driver, activityTypeFollowup);
            string followUpType = driver.FindElement(activityTypeFollowup).Text;
            driver.SwitchTo().DefaultContent();
            return followUpType;
        }

        public void ClickNewAffiliationButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewAffiliation);
            driver.FindElement(btnNewAffiliation).Click();
        }

        public void ClickAddToCampaignButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddToCampaignHistory);
            driver.FindElement(btnAddToCampaignHistory).Click();
        }

        public bool ValidateNewAffilationCompaniesCreation()
        {
            return CustomFunctions.IsElementPresent(driver, AffiliationCompanyRecord);
        }

        /*    public void PreRequisiteAffiliateCompanies()
            {
                IList<IWebElement> list = driver.FindElements(By.CssSelector("div[id *= '00Ni000000D735q_body'] > table > tbody > tr");
                for(int i = 2; i < list.Count)

                if (CustomFunctions.IsElementPresent(driver, AffiliationCompanyRecord))
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, linkDeleteAffiliationCompany);
                    driver.FindElement(linkDeleteAffiliationCompany).Click();
                    IAlert alert = driver.SwitchTo().Alert();
                    Thread.Sleep(1000);
                    alert.Accept();
                    Thread.Sleep(1000);                
                }
            }*/

        //Function to get affiliation Company name
        public string GetAffiliationCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAffiliationCompanyName, 60);
            string affiliationCompanyName = driver.FindElement(valAffiliationCompanyName).Text;
            return affiliationCompanyName;
        }

        // Function to get affiliation company status
        public string GetAffiliationCompanyStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAffiliationCompanyStatus, 60);
            string affiliationCompanyStatus = driver.FindElement(valAffiliationCompanyStatus).Text;
            return affiliationCompanyStatus;
        }

        // Function to get affiliation company type
        public string GetAffiliationCompanyType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAffiliationCompanyType, 60);
            string affiliationCompanyType = driver.FindElement(valAffiliationCompanyType).Text;
            return affiliationCompanyType;
        }

        // Function to delete affiliated companies
        public void DeleteAffiliatedCompanies(string file, string contactType)
        {
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                conHome.SearchContact(file, contactType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, linkDeleteAffiliationCompany);
            driver.FindElement(linkDeleteAffiliationCompany).Click();
            IAlert alert = driver.SwitchTo().Alert();
            Thread.Sleep(1000);
            alert.Accept();
            Thread.Sleep(1000);
        }

        // Click sortable view button
        public void ClickSortableViewBtnUnderHLRelationship()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSortableView);
            driver.FindElement(btnSortableView).Click();
        }

        public string GetContactName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valName, 60);
            string valueName = driver.FindElement(valName).Text;
            return valueName;
        }

        public string GetContactEmail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEmail, 60);
            string valueEmail = driver.FindElement(valEmail).Text;
            return valueEmail;
        }

        public string GetLegalEntityName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntity, 60);
            string valueLegalEntity = driver.FindElement(valLegalEntity).Text;
            return valueLegalEntity;
        }

        public void ClickLegalEntity(int row)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntity, 60);
            driver.FindElement(valLegalEntity).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 3);
                driver.Navigate().Refresh();
            }
            else if (row.Equals(3))
            {
                CustomFunctions.SwitchToWindow(driver, 6);
                driver.Navigate().Refresh();
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 9);
                driver.Navigate().Refresh();
            }
        }

        public void VerifyMergeContactFunctionality()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMergeContacts, 60);
            driver.FindElement(btnMergeContacts).Click();
            string txt= driver.FindElement(txtStep1MergeContact).Text;
            Console.WriteLine(txt);
           
            driver.FindElement(checkboxFirstContact).Click();
            driver.FindElement(checkboxSecondContact).Click();
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnNext, 60);
            driver.FindElement(btnNext).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnMerge, 60);
            driver.FindElement(btnMerge).Click();
            IAlert alert = driver.SwitchTo().Alert();
            string txtalert=alert.Text;
            Assert.AreEqual("These records will be merged into one record using the selected values. Merging can't be undone. Proceed with the record merge?", txtalert);
            alert.Accept();
        }

        public bool VerifyIfJobCodeFieldAndValueExistOrNot()
        {
            bool result = false;
            if (driver.FindElement(lblJobCode).Text.Equals("Job Code") && driver.FindElement(lblJobCodeValue).Displayed)
            {
                result = true;
            }
            return result;
        }
    }
}