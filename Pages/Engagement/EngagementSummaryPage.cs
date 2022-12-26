using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace SalesForce_Project.Pages.Engagement
{
    class EngagementSummaryPage : BaseClass
    {
        By lblTransType = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By comboTxnTypes = By.CssSelector("select[name*='j_id41']>option");
        By lblClientDesc = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(1) > td:nth-child(3) > label");
        By lblPostTxnStatus = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(2) > td:nth-child(1) > label");
        By comboPostTxnStatus = By.CssSelector("select[name*='j_id37:j_id49']>option");
        By lblCompDesc = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(3) > td:nth-child(1) > label");
        By lblTxnDesc = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(3) > td:nth-child(2) > label");
        By lblMessage = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(6) > td > label");
        By tabFinancials = By.CssSelector("td[id*='FinancialsProjections_lbl']");
        By headerFYFinancials = By.CssSelector("table[id*='j_id60:j_id61']>thead>tr[class='headerRow']>th[class='headerRow right']");
        By lblCurrenyFinancials = By.CssSelector("div[id*='j_id60'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By comboCurrencyTypes = By.CssSelector("select[name*='j_id60:j_id89']>option");
        By lblProjections = By.CssSelector("div[id*='j_id60'] > div.pbBody > table > tbody > tr:nth-child(2) > td:nth-child(1) > label");
        By lblLTMProjections = By.CssSelector("div[id*='j_id60'] > div.pbBody > table > tbody > tr:nth-child(3) > td:nth-child(1) > label");
        By rowFinancials = By.CssSelector("tbody[id*='j_id28:j_id60:j_id61:tb']>tr>td:nth-child(2)");
        By tabDistressedMA = By.CssSelector("td[id*='tabDistressedMAInformation_lbl']");
        By headerDistressed = By.CssSelector("table[id*=':salesTransactions']>thead>tr>th>div>label");
        By tabHLFinancing = By.CssSelector("td[id*='tabHLFinancing_lbl']");
        By headerHLFinancing = By.CssSelector("table[id*='financing']>thead>tr>th>div>label");
        By lblTotalFinancing = By.CssSelector("div[id*='j_id28:j_id133'] > div.pbBody > table>tbody>tr:nth-child(1)>td:nth-child(1)>label");
        By lblFinDesc = By.CssSelector("div[id*='j_id28:j_id133'] > div.pbBody > table>tbody>tr:nth-child(2)>td:nth-child(1)>label");
        By btnAddHLFin = By.CssSelector("input[value='Add HL Financing']");
        By titleInsertNewHL = By.CssSelector("span[class*='ui-dialog-title']");
        By lblFinTypes = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(1)>td>label");
        By comboFinTypes = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(1)>td:nth-child(2)>select>option");
        By lblOther = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(2)>td>label");
        By lblSecurityType = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(3)>td>label");
        By comboSecTypes = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(3)>td:nth-child(2)>select>option");
        By lblFinAmount = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(4)>td>label");
        By btnClose = By.CssSelector("button[title='Close']");
        By tabPreTrans = By.CssSelector("td[id*='tabPreTransactionInformation_lbl']");
        By lblPreTransSec = By.CssSelector("div[id*='id162'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By lblEquityHolder = By.CssSelector("table[id*=':preTransactionEquityHolders']>thead>tr>th>div[id*='j_id178header:sortDiv']");
        By lblOwnership = By.CssSelector("table[id *= ':preTransactionEquityHolders']>thead>tr>th>div[id *= 'j_id182header:sortDiv']");
        By lblPreTransMembersSec = By.CssSelector("div[id*='id162'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(2) > label");
        By lblBoardMember = By.CssSelector("table[id*=':preTransactionBoardMembers']>thead>tr>th>div[id*='j_id191header:sortDiv']");
        By lblCompany = By.CssSelector("table[id*=':preTransactionBoardMembers']>thead>tr>th>div[id*='j_id195header:sortDiv']");
        By lblRelationship = By.CssSelector("table[id*=':preTransactionBoardMembers']>thead>tr>th>div[id*='j_id199header:sortDiv']");
        By btnAddDebtStructure = By.CssSelector("input[id='newPreTransactionDebtStructure']");
        By lblSecurityTypes = By.CssSelector("div[id*='id1:j_id28'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By lblCurrency = By.CssSelector("div[id*='id1:j_id28'] > div.pbBody > table > tbody > tr:nth-child(12) > td:nth-child(1) > label");
        By comboSecType = By.CssSelector("select[id*='j_id0:j_id1:j_id28:j_id32']>option");
        By comboDebtCurrency = By.CssSelector("select[id*='j_id0:j_id1:j_id28:j_id72']>option");
        By btnWinClose = By.CssSelector("button[title='Close']");
        By lblPreTransDebtSec = By.CssSelector("div[id*='id162'] > div.pbBody > table > tbody > tr:nth-child(3) > td:nth-child(1) > label");
        By headerPreTransDebt = By.CssSelector("table[id*=':preTransactionDebtStructures']>thead>tr>th>div");
        By lblPreReorgTotalDebt = By.CssSelector(" div[id*='j_id28:j_id162']> div.pbBody > table > tbody > tr:nth-child(5) > td > table > tbody > tr > td:nth-child(3) > label");
        By lblPreReorgConstDebt = By.CssSelector(" div[id*='j_id28:j_id162']> div.pbBody > table > tbody > tr:nth-child(5) > td > table > tbody > tr > td:nth-child(1) > label");
        By tabPostTrans = By.CssSelector("td[id*='tabPostTransactionInformation_lbl']");
        By lblPostTransSec = By.CssSelector("div[id*='postTransactionInformation'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By headerEquityHolders = By.CssSelector("table[id*='postTransactionEquityHolders']>thead>tr>th");
        By lblPostTransMembersSec = By.CssSelector("div[id*='postTransactionInformation'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(2) > label");
        By headerBoardMembers = By.CssSelector("table[id*='postTransactionBoardMembers']>thead>tr>th");
        By btnPostTransAddDebt = By.CssSelector("input[id='newPostTransactionDebtStructure']");
        By lblPostTransDebtSec = By.CssSelector("div[id*='j_id28:postTransactionInformation'] > div.pbBody > table > tbody > tr:nth-child(3) > td:nth-child(1) ");
        By headerPostTransDebt = By.CssSelector("table[id*=':postTransactionInformation:j']>thead>tr>th>div");
        By lblPostTotalDebt = By.CssSelector("div[id*='postTransactionInformation'] > div[class='pbBody'] > table > tbody > tr:nth-child(5) > td > table > tbody > tr > td:nth-child(1) > label");
        By lblNetDebt = By.CssSelector("div[id*='postTransactionInformation'] > div[class='pbBody'] > table > tbody > tr:nth-child(5) > td > table > tbody > tr > td:nth-child(3) > label");
        By lblClosingStock = By.CssSelector("div[id*='postTransactionInformation'] > div[class='pbBody'] > table > tbody > tr:nth-child(5) > td > table > tbody > tr > td:nth-child(5) > label");
        By tabHLPostTrans = By.CssSelector("td[id*='tabHLPostTransactionOpportunities_lbl']");
        By comboAvail = By.CssSelector("select[title*='Opportunities - Available']>optgroup>option");
        By btnAddStaffRole = By.CssSelector("input[value='Add Staff Role']");
        By lblClassification = By.CssSelector("span[id*='staffRoleClassification']>label");
        By comboClassification = By.CssSelector("select[name*='mainForm:dummyStaffRole']>option");
        By lblStaffRolesSec = By.CssSelector("div[id*='j_id336'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(2) > label");
        By headerStaffRoles = By.CssSelector("tbody > tr:nth-child(1) > td:nth-child(2) > span>table>thead>tr>th>div");
        By lblExtContactSec = By.CssSelector("div[id*='j_id336'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(3) > label");
        By headerExtContact = By.CssSelector("tbody > tr:nth-child(1) > td:nth-child(3) > span>table>thead>tr>th>div");
        By lblNotesSec = By.CssSelector("div[id*='j_id336'] > div.pbBody > table > tbody > tr:nth-child(2) > td > label");
        By btnAddDistressed = By.CssSelector("input[value='Add Distressed M&A Information']");
        By txtAssetSale = By.CssSelector("input[name*='saleTransactionName']");
        By lnkDateOfSale = By.CssSelector("div[id*='j_id34'] > div > table > tbody > tr:nth-child(2) > td > span > span > a");
        By txtMinOverbid = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id36']");
        By txtIncOverbid = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id37']");
        By txtBreakUpFee = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id38']");
        By txtDeposit = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id39']");
        By txtCashComp = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id41']");
        By txtStockComp = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id42']");
        By txtLiability = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id43']");
        By txtClaimConv = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id44']");
        By btnSave = By.CssSelector("input[name*='2:bottom:j_id33']");
        By lnkEdit = By.CssSelector("td[id*='salesTransactions:0:j_id104']>a");
        By valMARec = By.CssSelector("tbody[id*='salesTransactions:tb']>tr>td>span");
        By valMinOverbid = By.CssSelector("tbody[id*='salesTransactions:tb']>tr>td:nth-child(5)>span");
        By valCashComp = By.CssSelector("tbody[id*='salesTransactions:tb']>tr>td:nth-child(9)>span");
        By lnkDel = By.CssSelector("td[id*='salesTransactions:0:j_id107']>a");
        By txtMessage = By.CssSelector("div[class='pbBody']>span[id*='j_id101:panSalesTransactions']>label");
        By comboFinancingType = By.CssSelector("select[name*='j_id492:addFinancingType']");
        By txtOther = By.CssSelector("textarea[name*='addFinancingOther']");
        By comboSecurityType = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(3)>td:nth-child(2)>select");
        By txtFinAmmount = By.CssSelector("input[name*='j_id506']");
        By btnHLFinSave = By.CssSelector("input[name*='j_id508']");
        By valHLFinRec = By.CssSelector("tbody[id*='j_id28:j_id133:financing:tb']>tr>td>span");
        By lnkHLFinEdit = By.CssSelector("tbody[id*='j_id133:financing:tb']>tr>td>a[id*='editFinancing']");
        By valFinancingType = By.CssSelector("tbody[id*='financing:tb']>tr>td:nth-child(3)>span");
        By lnkHLFinDel = By.CssSelector("td[id*='financing:0:j_id142']>a");
        By msgHLFin = By.CssSelector("div[class='pbBody']>span[id*='j_id133:panFinancing']>label");
        By comboFinTypeEdit = By.CssSelector("select[name*=':editFinancingType']");
        By btnSaveFinEdit = By.CssSelector("input[name*='3:j_id34']");
        By lnkEditRevenue = By.CssSelector("a[id='editRevenueFinancials']");
        By txtFY1 = By.CssSelector("input[name*='j_id369']");
        By txtFY = By.CssSelector("input[name*='j_id374']");
        By txtLTM = By.CssSelector("input[name*='j_id379']");
        By txtFYPlus1 = By.CssSelector("input[name*='j_id384']");
        By txtFYPlus2 = By.CssSelector("input[name*='j_id389']");
        By txtFYPlus3 = By.CssSelector("input[name*='j_id394']");
        By txtFYPlus4 = By.CssSelector("input[name*='j_id399']");
        By txtFYPlus5 = By.CssSelector("input[name*='j_id404']");
        By btnSaveRevenue = By.CssSelector("input[name*='j_id406']");
        By rowValRevenue = By.CssSelector("tbody[id*='j_id61:tb']>tr:nth-child(1)>td[class='dataCell  right']");
        By lnkEditEBITDA = By.CssSelector("a[id = 'editEBITDAFinancials']");
        By txtFY1EBITDA = By.CssSelector("input[name*='j_id453']");
        By txtFYEBITDA = By.CssSelector("input[name*='j_id458']");
        By txtLTMEBITDA = By.CssSelector("input[name*='j_id463']");
        By txtFYPlus1EBITDA = By.CssSelector("input[name*='j_id468']");
        By txtFYPlus2EBITDA = By.CssSelector("input[name*='j_id473']");
        By txtFYPlus3EBITDA = By.CssSelector("input[name*='j_id478']");
        By txtFYPlus4EBITDA = By.CssSelector("input[name*='j_id483']");
        By txtFYPlus5EBITDA = By.CssSelector("input[name*='j_id488']");
        By btnSaveEBITDA = By.CssSelector("input[name*='j_id490']");
        By rowValEBITDA = By.CssSelector("tbody[id*='j_id61:tb']>tr:nth-child(2)>td[class='dataCell  right']");
        By lnkEditCapex = By.CssSelector("a[id = 'editCapexFinancials']");
        By txtFY1Capex = By.CssSelector("input[name*='j_id411']");
        By txtFYCapex = By.CssSelector("input[name*='j_id416']");
        By txtLTMCapex = By.CssSelector("input[name*='j_id421']");
        By txtFYPlus1Capex = By.CssSelector("input[name*='j_id426']");
        By txtFYPlus2Capex = By.CssSelector("input[name*='j_id431']");
        By txtFYPlus3Capex = By.CssSelector("input[name*='j_id436']");
        By txtFYPlus4Capex = By.CssSelector("input[name*='j_id441']");
        By txtFYPlus5Capex = By.CssSelector("input[name*='j_id446']");
        By btnSaveCapex = By.CssSelector("input[name*='j_id448']");
        By rowValCapex = By.CssSelector("tbody[id*='j_id61:tb']>tr:nth-child(3)>td[class='dataCell  right']");
        By btnAddEquityHolder = By.CssSelector("input[id='newPreTransactionEquityHolder']");
        By txtEquitySearch = By.CssSelector("input[name*='txtSearch']");
        By btnGo = By.CssSelector("input[value='Go']");
        By checkRow = By.CssSelector("input[name*='tblResults:0:j_id50']");
        By btnAddSelected = By.CssSelector("input[value='Add Selected']");
        By msgSuccess = By.CssSelector("div[id*='4:j_id16']");
        By btnEquityClose = By.CssSelector("span[class*='closethick']");
        By valEquityHolder = By.CssSelector("tbody[id*='preTransactionEquityHolders:tb']>tr>td:nth-child(4)");
        By lnkEquityEdit = By.CssSelector("a[id*='editPre']");
        By txtOwnership = By.CssSelector("input[name*='ownershipPercent']");
        By valOwnership = By.CssSelector("td[id*='preTransactionEquityHolders:0:j_id182']");
        By btnEquitySave = By.CssSelector("input[name*='pbMainEdit:j_id41:j_id42']");
        By lnkEquityCopy = By.CssSelector("a[id*='j_id174']");
        By rowEquityHoldersPost = By.CssSelector("tbody[id*='postTransactionEquityHolders:tb']>tr>td");
        By lnkEquityDel = By.CssSelector("td[id*='j_id175']>a");
        By msgEquityDelete = By.CssSelector("span[id*='PreTransactionEquityHolders']>label");
        By btnAddBoardMember = By.CssSelector("input[id='newPreTransactionBoardMember']");
        By checkRowBoard = By.CssSelector("input[name*='tblResults:0:j_id42']");
        By msgSuccessBoard = By.CssSelector("div[id*='10:j_id12']");
        By valBoardMember = By.CssSelector("tbody[id*='preTransactionBoardMembers:tb']>tr>td:nth-child(3)");
        By lnkBoardCopy = By.CssSelector("a[id*='j_id187']");
        By rowBoardMemberPost = By.CssSelector("tbody[id*='postTransactionBoardMembers:tb']>tr>td");
        By lnkBoardDel = By.CssSelector("td[id*='j_id188']>a");
        By msgBoardDelete = By.CssSelector("span[id*='panPreTransactionBoardMembers']>label");
        By msgSaveRec = By.CssSelector("div[id*='1:j_id28']>div[class='pbBody']>table>tbody>tr:nth-child(15)>td>label");
        By comboSecurityTypeDebt = By.CssSelector("select[id*='id32']");
        By txtMaturityDate = By.CssSelector("div[id*='1:j_id28']>div[class='pbBody']>table>tbody>tr:nth-child(2)>td:nth-child(2)>span>span>a");
        By txtInterest = By.CssSelector("input[id*='id40']");
        By txtOID = By.CssSelector("input[id*='id44']");
        By txtAmortization = By.CssSelector("input[id*='id48']");
        By txtCallProvisions = By.CssSelector("input[id*='id52']");
        By txtMandatory = By.CssSelector("input[id*='id56']");
        By txtCovenants = By.CssSelector("textarea[name*='id60']");
        By txtFees = By.CssSelector("input[id*='id64']");
        By txtFacility = By.CssSelector("input[id*='id68']");
        By btnSaveDebt = By.CssSelector("input[name*='id74']");
        By msgSuccessDebt = By.CssSelector("td[class='messageCell']>div");
        By rowDebtStrPre = By.CssSelector("tbody[id*='preTransactionDebtStructures:tb']>tr>td>span");
        By valDebtSecurityType = By.CssSelector("td[id*='preTransactionDebtStructures:0:j_id212']");
        By lnkDebtDel = By.CssSelector("td[id*='j_id209']>a");
        By msgDebtDelete = By.CssSelector("span[id*='panPreTransactionDebtStructures']>label");
        By btnNewLender = By.CssSelector("input[value='New Key Creditor']");
        By txtLenderAmt = By.CssSelector("input[name*=':0:j_id54']");
        By rowLender = By.CssSelector("tbody[id*='id81:tb']>tr>td>span");
        By rowLenderPostDel = By.CssSelector("tbody[id*='id81:tb']>tr");
        By lnkLenderEdit = By.CssSelector("a[id*='editLender']");
        By txtLoanAmt = By.CssSelector("input[name*='loanAmount']");
        By btnSaveLender = By.CssSelector("input[name*='41:j_id42']");
        By valLoanAmt = By.CssSelector("tbody[id*='id81:tb']>tr>td:nth-child(3)>span");
        By lnkLenderDelete = By.CssSelector("a[id*='deleteLender']");
        By btnPostAddEquity = By.CssSelector("input[id='newPostTransactionEquityHolder']");
        By valPostEquityHolder = By.CssSelector("tbody[id*='postTransactionEquityHolders:tb']>tr>td:nth-child(3)");
        By lnkPostEquityEdit = By.CssSelector("a[id*='editPost']");
        By valPostOwnership = By.CssSelector("td[id*='postTransactionEquityHolders:0:j_id266']");
        By lnkPostEquityDel = By.CssSelector("td[id*='j_id259']>a");
        By msgPostEquityDelete = By.CssSelector("span[id*='PostTransactionEquityHolders']>label");
        By btnPostAddBoardMember = By.CssSelector("input[id='newPostTransactionBoardMember']");
        By valPostBoardMember = By.CssSelector("tbody[id*='postTransactionBoardMembers:tb']>tr>td:nth-child(2)");
        By lnkPostBoardDel = By.CssSelector("td[id*='j_id270']>a");
        By msgPostBoardDelete = By.CssSelector("span[id*='panPostTransactionBoardMembers']>label");
        By btnPostAddDebtStructure = By.CssSelector("input[id='newPostTransactionDebtStructure']");
        By rowPostDebtStrPre = By.CssSelector("tbody[id*='postTransactionInformation:j_id288:tb']>tr>td>span");
        By valPostDebtSecurityType = By.CssSelector("td[id*='j_id288:0:j_id300']");
        By lnkPostDebtDel = By.CssSelector("td[id*='j_id292']>a");
        By msgPostDebtDelete = By.CssSelector("span[id*='panPostTransactionDebtStructures']>label");
        By checkStaffRole = By.CssSelector("input[name*='tblResults:0:j_id42']");
        By valStaffRoles = By.CssSelector("tbody[id*='postTransactionStaffRoles:tb']>tr>td>span");
        By lnkStaffRoleDel = By.CssSelector("tbody[id*='postTransactionStaffRoles:tb']>tr>td>a");
        By msgStaffRoleDel = By.CssSelector("tbody>tr>td:nth-child(2)>span[id*='panPostTransactionStaffRoles']>label");
        By txtClassification = By.CssSelector("select[name*='mainForm:dummyStaffRole']");
        By msgSuccessStaff = By.CssSelector("div[id*='10:j_id12']");

        string dir = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\";

        //Get label i.e. Transaction Type 
        public string GetLabelTransactionType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTransType, 90);
            string txtTransType = driver.FindElement(lblTransType).Text;
            return txtTransType;
        }
        //Compare values of Transaction Type
        public bool VerifyTxnTypeValues()
        {
            IReadOnlyCollection<IWebElement> valTxnTypes = driver.FindElements(comboTxnTypes);
            var actualValue = valTxnTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Change of Control", "Debt for Cash", "Debt for Debt", "Debt for Equity", "Reinstatement", "Sale" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Get label i.e. Client Description
        public string GetLabelClientDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblClientDesc, 90);
            string txtClientDesc = driver.FindElement(lblClientDesc).Text;
            return txtClientDesc;
        }

        //Get label i.e. Post Transaction Status

        public string GetLabelPostTxnStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTxnStatus, 90);
            string txtPostTxnStatus = driver.FindElement(lblPostTxnStatus).Text;
            return txtPostTxnStatus;
        }

        //Compare values of Post Transaction Status
        public bool VerifyPostTxnStatusValues()
        {
            IReadOnlyCollection<IWebElement> valTxnStatus = driver.FindElements(comboPostTxnStatus);
            var actualValue = valTxnStatus.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Reorganized - Public", "Reorganized - Private", "Liquidated", "Acquired", "Other", "N/A" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        //Get label i.e. Company Description  
        public string GetLabelCompanyDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompDesc, 90);
            string txtCompDesc = driver.FindElement(lblCompDesc).Text;
            return txtCompDesc;
        }

        //Get label i.e. Restructuring Transaction Description
        public string GetLabelTxnDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTxnDesc, 90);
            string txtCompDesc = driver.FindElement(lblTxnDesc).Text;
            return txtCompDesc;
        }

        //Get message displayed at bottom
        public string GetMessageDisplayed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblMessage, 90);
            string txtMessage = driver.FindElement(lblMessage).Text;
            return txtMessage;
        }

        //To Click Financials/Projections tab
        public void ClickFinancialsTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabFinancials, 90);
            driver.FindElement(tabFinancials).Click();
        }

        //To Validate FY header row of Financials/Projections tab
        public bool VerifyFYHeaderRow()
        {
            IReadOnlyCollection<IWebElement> valTxnStatus = driver.FindElements(headerFYFinancials);
            var actualValue = valTxnStatus.Select(x => x.Text).ToArray();
            string[] expectedValue = { "FY-1", "FY", "LTM", "FY+1", "FY+2", "FY+3", "FY+4", "FY+5" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Get label i.e. Currency Financials are reported in

        public string ValidateLabelCurrenyFin()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCurrenyFinancials, 90);
            string txtCurrencyFin = driver.FindElement(lblCurrenyFinancials).Text;
            return txtCurrencyFin;
        }

        //Compare values of Currency Financials
        public bool VerifyCurrencyFinancialsValues()
        {
            IReadOnlyCollection<IWebElement> valCurTypes = driver.FindElements(comboCurrencyTypes); 
            var actualValue = valCurTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Australian Dollar", "British Pound", "Canadian Dollar", "Chinese Yuan", "Czech Koruna","Danish Krone","Euro", "Hong Kong Dollar", "Indian Rupee", "Israeli Shekel", "Japanese Yen", "Saudi Arabian Riyal", "Singapore Dollar", "Swedish Krona", "Swiss Franc", "U.S. Dollar", "UAE Dirham","Vietnam Dong"};
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Get label i.e. Projections are as of

        public string ValidateLabelProjections()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblProjections, 90);
            string txtProjections = driver.FindElement(lblProjections).Text;
            return txtProjections;
        }

        //Get label i.e. LTM Projections are as of

        public string ValidateLabelLTMProjections()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblLTMProjections, 90);
            string txtLTMProjections = driver.FindElement(lblLTMProjections).Text;
            return txtLTMProjections;
        }
        //Compare row values of Financials/Projections
        public bool ValidateFinancialsRows()
        {
            IReadOnlyCollection<IWebElement> valRows = driver.FindElements(rowFinancials);
            var actualValue = valRows.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Revenue", "EBITDA", "Capex" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To Click Distressed M&A Information tab        
        public void ClickDistressedMAInfoTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabDistressedMA, 90);
            driver.FindElement(tabDistressedMA).Click();
        }

        //To Validate header row of Distressed M&A Information tab        
        public bool VerifyDistressedHeaderValues()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerDistressed);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Asset Sold", "Date of Sale", "Minimum Overbid (MM)", "Incremental Overbid (MM)", "Break Up Fee (MM)", "Deposit (MM)", "Cash Component (MM)", "Stock Component (MM)", "Liability Assumed (MM)", "Claim Conversion (MM)", "Total Sales Price (MM)" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To Click HL Financing tab        
        public void ClickHLFinancingTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabHLFinancing, 90);
            driver.FindElement(tabHLFinancing).Click();
        }
        //To validate HL Financing button
        public string ValidateAddHLFinancing()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddHLFin, 90);
            string btnAddHL = driver.FindElement(btnAddHLFin).Displayed.ToString();
            Console.WriteLine("btnAddHL: " + btnAddHL);
            if (btnAddHL.Equals("True"))
            {
                return "Add HL Financing is displayed";
            }
            else
            {
                return "Add HL Financing is not displayed";
            }
        }

        //To Click Add HL Financing button        
        public string ClickAddHLFinancing()
        {
            driver.FindElement(btnAddHLFin).Click();
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //Validate label i.e. Financing Types  
        public string ValidateLabelFinType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFinTypes, 90);
            string txtFinTypes = driver.FindElement(lblFinTypes).Text;
            return txtFinTypes;
        }

        //To Validate values of Financing Types combo        
        public bool VerifyFinancingTypesValues()
        {
            IReadOnlyCollection<IWebElement> valFinTypes = driver.FindElements(comboFinTypes);
            var actualValue = valFinTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Acquisition", "Credit Facility", "DIP", "Equity", "Exit Facility", "Mezzanine", "New Equity Financing", "Refinancing", "Revolver", "Rollover", "Term Loan", "Other" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate label i.e. Other
        public string ValidateLabelOther()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblOther, 90);
            string txtOther = driver.FindElement(lblOther).Text;
            return txtOther;
        }
        //Validate label i.e. Security Type
        public string ValidateLabelSecurityType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSecurityType, 90);
            string txtSecType = driver.FindElement(lblSecurityType).Text;
            return txtSecType;
        }

        //To Validate values of Security Types combo        
        public bool VerifySecurityTypesValues()
        {
            IReadOnlyCollection<IWebElement> valSecTypes = driver.FindElements(comboSecTypes);
            var actualValue = valSecTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Bank Debt (First Lien) - Revolver", "Bank Debt (First Lien) - Term Loan A", "Bank Debt (First Lien) - Term Loan B", "Bank Debt (First Lien) - Synthetic LC Facility", "Bank Debt (Second Lien)", "Senior Structured Notes", "Capital Leases", "Other Secured Debt", "Mezzanine Debt", "Senior Notes (Unsecured)", "Senior Subordinated Notes (Unsecured)", "Other Unsecured Debt", "Common Equity", "Preferred Equity" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate label i.e. Security Type
        public string ValidateLabelFinAmount()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFinAmount, 90);
            string txtFinAmt = driver.FindElement(lblFinAmount).Text;
            driver.FindElement(btnClose).Click();
            return txtFinAmt;
        }

        //To Validate header row of HL Financing tab        
        public bool VerifyHLFinancingHeaderValues()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerHLFinancing);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Financing Type", "Other", "Security Type", "Financing Amount (MM)" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        //Get label i.e. Total Financing Amount

        public string ValidateLabelTotalFinAmount()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTotalFinancing, 90);
            string txtFinAmt = driver.FindElement(lblTotalFinancing).Text;
            return txtFinAmt;
        }

        //Get label i.e. Financing Description
        public string ValidateLabelFinDesc()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFinDesc, 90);
            string txtFinDesc = driver.FindElement(lblFinDesc).Text;
            return txtFinDesc;
        }

        //To Click Pre-Transaction Information tab        
        public void ClickPreTransInfoTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabPreTrans, 90);
            driver.FindElement(tabPreTrans).Click();
        }

        //To validate Pre-Transaction Equity Holders section        
        public string ValidatePreTransEquitySection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreTransSec, 90);
            string sectionName = driver.FindElement(lblPreTransSec).Text;
            return sectionName;
        }

        //To validate Equity Holder Header      
        public string ValidateEquityHolderHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblEquityHolder, 90);
            string headerName = driver.FindElement(lblEquityHolder).Text;
            return headerName;
        }

        //To validate Percent Ownership Header      
        public string ValidateOwnershipHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblOwnership, 90);
            string headerName = driver.FindElement(lblOwnership).Text;
            return headerName;
        }

        //To validate Pre-Transaction Board Members section        
        public string ValidatePreTransMembersSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreTransMembersSec, 90);
            string sectionName = driver.FindElement(lblPreTransMembersSec).Text;
            return sectionName;
        }

        //To validate Board Member Header
        public string ValidateMemberHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblBoardMember, 90);
            string headerName = driver.FindElement(lblBoardMember).Text;
            return headerName;
        }

        //To validate Company Header
        public string ValidateCompanyHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompany, 90);
            string headerName = driver.FindElement(lblCompany).Text;
            return headerName;
        }

        //To validate HL Relationship Header
        public string ValidateHLRelationshipHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRelationship, 90);
            string headerName = driver.FindElement(lblRelationship).Text;
            return headerName;
        }

        //To validate Add Debt Structure button        
        public string ValidateAddDebtStructureButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddDebtStructure, 90);
            string btnAddHL = driver.FindElement(btnAddDebtStructure).Displayed.ToString();
            Console.WriteLine("btnAddDebtStructure: " + btnAddDebtStructure);
            if (btnAddHL.Equals("True"))
            {
                return "Add Debt Structure button is displayed";
            }
            else
            {
                return "Add HL Financing button is not displayed";
            }
        }

        //To Click Add Debt Structure button        
        public string ValidateAddDebtTitle()
        {
            driver.FindElement(btnAddDebtStructure).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 80);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //Validate label Security Type in Add New Pre-Transaction Debt Structure window       
        public string ValidateLabelSecurityTypes()
        {
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, lblSecurityTypes, 60);
            driver.FindElement(lblSecurityTypes).Click();
            string txtSecurityTypes = driver.FindElement(lblSecurityTypes).Text;
            return txtSecurityTypes;
        }

        //To Validate values of Security Types combo    
        public bool VerifySecurityTypeValues()
        {
            IReadOnlyCollection<IWebElement> valType = driver.FindElements(comboSecType);
            var actualValue = valType.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Bank Debt (First Lien) - Revolver", "Bank Debt (First Lien) - Term Loan A", "Bank Debt (First Lien) - Term Loan B", "Bank Debt (First Lien) - Synthetic LC Facility", "ABL/ABS Facility - First Lien", "LC Facility", "Bank Debt (Second Lien)", "ABL/ABS Facility - Second Lien", "Convertible Notes (Secured)", "Senior Secured Notes", "Capital Leases", "Other Secured Debt", "Mezzanine Debt", "Senior Notes (Unsecured)", "Senior Subordinated Notes (Unsecured)", "Substantially All Assets", "Convertible Notes (Unsecured)", "Other Unsecured Debt" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate label Debt Currency in Add New Pre-Transaction Debt Structure window       
        public string ValidateLabelCurrency()
        {
            driver.FindElement(lblCurrency).Click();
            string txtCurrency = driver.FindElement(lblCurrency).Text;
            return txtCurrency;
        }

        //To Validate values of Debt Currency combo    
        public bool VerifyCurrencyValues()
        {
            IReadOnlyCollection<IWebElement> valCurrencies = driver.FindElements(comboDebtCurrency);
            var actualValue = valCurrencies.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Australian Dollar", "British Pound", "Canadian Dollar", "Chinese Yuan", "Czech Koruna", "Danish Krone", "Euro", "Hong Kong Dollar", "Indian Rupee", "Israeli Shekel", "Japanese Yen", "Saudi Arabian Riyal", "Singapore Dollar", "Swedish Krona", "Swiss Franc", "U.S. Dollar", "UAE Dirham", "Vietnam Dong" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To close the window
        public void CloseDebtStructureWindow()
        {
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnWinClose).Click();
        }

        //To validate Pre-Transaction Debt (MM) section
        public string ValidatePreTransDebtSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreTransDebtSec, 90);
            string sectionName = driver.FindElement(lblPreTransDebtSec).Text;
            return sectionName;
        }

        //To Validate header row of Pre-Transaction Debt(MM) tab        
        public bool VerifyPreTransDebtHeaderValues()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerPreTransDebt);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "", "Security Type", "Key Creditors", "Constituent Debt", "Facility Balance (MM)", "Maturity Date", "Interest", "OID Percent", "Amortization", "Call Provisions / Prepayment Premiums", "Mandatory Prepayments / ECF Sweep", "Covenants", "Fees & Expenses" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To validate field Pre Reorganization Constituent Debt under Pre-Transaction Debt(MM) section
        public string ValidateLabelPreReorgConstDebt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreReorgConstDebt, 90);
            string txtTotalDebt = driver.FindElement(lblPreReorgConstDebt).Text;
            return txtTotalDebt;
        }

        //To validate field Pre Reorganized Total Debt under Pre-Transaction Debt (MM) section
        public string ValidateLabelPreReorgTotal()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreReorgTotalDebt, 90);
            string txtTotalDebt = driver.FindElement(lblPreReorgTotalDebt).Text;
            return txtTotalDebt;
        }

        //To Click Post-Transaction Information tab        
        public void ClickPostTransInfoTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabPostTrans, 90);
            driver.FindElement(tabPostTrans).Click();
        }

        //To validate Post-Transaction Equity Holders section        
        public string ValidatePostTransEquitySection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransSec, 90);
            string sectionName = driver.FindElement(lblPostTransSec).Text;
            return sectionName;
        }

        //To validate headers of Post-Transaction Equity Holders section 
        public bool VerifyPostTransEquityHoldersHeader()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerEquityHolders);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "", "Equity Holder", "Percent Ownership" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        //To validate Post-Transaction Board Members section        
        public string ValidatePostTransMembersSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransMembersSec, 90);
            string sectionName = driver.FindElement(lblPostTransMembersSec).Text;
            return sectionName;
        }

        //To validate headers of Post-Transaction Board Members section 
        public bool VerifyPostTransBoardMembersHeader()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerBoardMembers);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "Board Member", "Company", "Has HL Relationship" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To validate Add Debt Structure button        
        public string ValidatePostTransAddDebtButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnPostTransAddDebt, 90);
            string btnAddHL = driver.FindElement(btnPostTransAddDebt).Displayed.ToString();
            Console.WriteLine("btnPostTransAddDebt: " + btnPostTransAddDebt);
            if (btnAddHL.Equals("True"))
            {
                return "Add Debt Structure button is displayed";
            }
            else
            {
                return "Add HL Financing button is not displayed";
            }
        }

        //To Click Add Debt Structure button        
        public string ValidatePostTransAddDebtTitle()
        {
            driver.FindElement(btnPostTransAddDebt).Click();
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To validate Post-Transaction Debt (MM) section
        public string ValidatePostTransDebtSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransDebtSec, 90);
            string sectionName = driver.FindElement(lblPostTransDebtSec).Text;
            return sectionName;
        }

        //To Validate header row of Post-Transaction Debt(MM) tab        
        public bool VerifyPostTransDebtHeaderValues()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerPostTransDebt);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "", "Key Creditors", "Security Type", "Constituent Debt", "Facility Balance (MM)", "Maturity Date", "Interest", "OID Percent", "Amortization", "Call Provisions / Prepayment Premiums", "Mandatory Prepayments / ECF Sweep", "Covenants", "Fees & Expenses" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        //To validate field Post-Restructuring Total Debt (MM) under Post-Transaction Debt (MM) section
        public string ValidateLabelPostRestrucTotalDebt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTotalDebt, 90);
            string txtTotalDebt = driver.FindElement(lblPostTotalDebt).Text;
            return txtTotalDebt;
        }
        //To validate field Net Debt of the Restructured Company (MM) under Post-Transaction Debt (MM) section
        public string ValidateLabelNetDebtRestrComp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblNetDebt, 90);
            string txtNetDebt = driver.FindElement(lblNetDebt).Text;
            return txtNetDebt;
        }

        //To validate field Closing Stock Price under Post-Transaction Debt (MM) section
        public string ValidateLabelClosingStockPrice()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblClosingStock, 90);
            string txtStockPrice = driver.FindElement(lblClosingStock).Text;
            return txtStockPrice;
        }

        //To Click HL Post-Transaction Opportunities tab        
        public void ClickHLPostTransOppTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabHLPostTrans, 90);
            driver.FindElement(tabHLPostTrans).Click();
        }

        //To validate available values from the list 
        public bool VerifyAvailableValues()
        {
            IReadOnlyCollection<IWebElement> valAval = driver.FindElements(comboAvail);
            var actualValue = valAval.Select(x => x.Text).ToArray();
            string[] expectedValue = { "M&A - Buyside", "M&A - Sellside", "Restructuring", "Valuation", "Financing", "Fairness/Solvency" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate Add Post-Transaction Staff Role window        
        public string ValidateAddPostTransStaffRoleTitle()
        {
            driver.FindElement(btnAddStaffRole).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 60);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //Validate label Classification         
        public string ValidateLabelClassification()
        {
            driver.SwitchTo().Frame(1);
            string txtClassification = driver.FindElement(lblClassification).Text;
            return txtClassification;
        }

        //To Validate values of Classification combo    
        public bool VerifyClassificationValues()
        {
            IReadOnlyCollection<IWebElement> valClassi = driver.FindElements(comboClassification);
            var actualValue = valClassi.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Financing", "FSG", "FVA", "Industry" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        //To validate Post-Transaction Staff Roles section        
        public string ValidatePostTransStaffRolesSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblStaffRolesSec, 90);
            string sectionName = driver.FindElement(lblStaffRolesSec).Text;
            return sectionName;
        }

        //To Validate header values of Post-Transaction Staff Roles section   
        public bool VerifyHeaderValuesOfStaffRoles()
        {
            IReadOnlyCollection<IWebElement> valRoles = driver.FindElements(headerStaffRoles);
            var actualValue = valRoles.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "Name", "Relationship" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To validate Post-Transaction Key External Contact section        
        public string ValidatePostTransExtContactSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExtContactSec, 90);
            string sectionName = driver.FindElement(lblExtContactSec).Text;
            return sectionName;
        }

        //To Validate header values of Post-Transaction Key External Contact section    
        public bool VerifyHeaderValuesOfExtContact()
        {
            IReadOnlyCollection<IWebElement> valContact = driver.FindElements(headerExtContact);
            var actualValue = valContact.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "Name", "Relationship" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To validate Notes section        
        public string ValidateNotesSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblNotesSec, 90);
            string sectionName = driver.FindElement(lblNotesSec).Text;
            return sectionName;
        }

        //To Validate Add Distressed M&A Information window        
        public string ValidateDistressedMAInfoWindow()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddDistressed, 90);
            driver.FindElement(btnAddDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To save details of Add Distressed M&A Information         
        public void SaveDistressedMAInfoDetails(string file)
        {
            string excelPath = dir + file;
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, txtAssetSale, 90);
            driver.FindElement(txtAssetSale).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 1));
            driver.FindElement(lnkDateOfSale).Click();
            driver.FindElement(txtMinOverbid).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 2));
            driver.FindElement(txtIncOverbid).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 3));
            driver.FindElement(txtBreakUpFee).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 4));
            driver.FindElement(txtDeposit).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 5));
            driver.FindElement(txtCashComp).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 6));
            driver.FindElement(txtStockComp).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 7));
            driver.FindElement(txtLiability).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 8));
            driver.FindElement(txtClaimConv).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 9));
            driver.FindElement(btnSave).Click();
        }

        //To Validate values of added Distressed M&A record  
        public bool ValidateDistressedMARecord()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valMARec, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(valMARec);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string value2 = DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine("Date: " + value2);
            string[] expectedValue = { "Test Asset", value2, "USD 10.00", "USD 10.00", "USD 10.00", "USD 10.00", "USD 10.00", "USD 10.00", "USD 10.00", "USD 10.00", "40.00" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To update existing Distressed M&A Information record
        public void UpdateDistressedMARecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 90);
            driver.FindElement(lnkEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            driver.SwitchTo().Frame(1);
            driver.FindElement(txtMinOverbid).Clear();
            driver.FindElement(txtMinOverbid).SendKeys("20");
            driver.FindElement(txtCashComp).Clear();
            driver.FindElement(txtCashComp).SendKeys("20");
            driver.FindElement(btnSave).Click();
        }

        //To get value of Minimum Overload (MM) column
        public string GetMinOverload()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valMARec, 80);
            Thread.Sleep(3000);
            string value = driver.FindElement(valMinOverbid).Text;
            return value;
        }

        //To get value of Cash Component (MM) column
        public string GetCashComp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCashComp, 70);
            Thread.Sleep(3000);
            string value = driver.FindElement(valCashComp).Text;
            return value;
        }

        //To delete the added record and validate the displayed message
        public string DeleteExistingRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDel, 70);
            driver.FindElement(lnkDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMessage, 60);
            string message = driver.FindElement(txtMessage).Text;
            return message;
        }

        //To validate if Other Text box is disabled
        public string ValidateOtherIsDisabledForAllFinTypesExceptOther(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinancingType, 90);
            driver.FindElement(comboFinancingType).SendKeys(ReadExcelData.ReadData(excelPath, "HLFin", 1));
            string value = driver.FindElement(txtOther).Enabled.ToString();
            return value;
        }

        //To validate if Other Text box is enabled   
        public string ValidateOtherIsEnabledWhenFinTypeIsOther(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinancingType, 90);
            Thread.Sleep(2000);
            driver.FindElement(comboFinancingType).SendKeys(ReadExcelData.ReadData(excelPath, "HLFin", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, txtOther, 90);
            string value = driver.FindElement(txtOther).Enabled.ToString();
            return value;
        }

        //To save HL Financing record
        public void SaveHLFinancingDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinancingType, 90);
            driver.FindElement(txtOther).SendKeys("Testing");
            driver.FindElement(comboSecurityType).SendKeys(ReadExcelData.ReadData(excelPath, "HLFin", 3));
            driver.FindElement(txtFinAmmount).SendKeys(ReadExcelData.ReadData(excelPath, "HLFin", 4));
            driver.FindElement(btnHLFinSave).Click();
        }

        //To Validate values of added HL Financing record  
        public bool ValidateHLFinancingRecord(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, valHLFinRec, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(valHLFinRec);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string valFinType = ReadExcelData.ReadData(excelPath, "HLFin", 2);
            string valSecType = ReadExcelData.ReadData(excelPath, "HLFin", 3);
            string[] expectedValue = { valFinType, "Testing", valSecType, "USD 10.00" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To update existing Hl Financing record
        public void UpdateHLFinancingRecord(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkHLFinEdit, 90);
            driver.FindElement(lnkHLFinEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinTypeEdit, 90);
            driver.FindElement(comboFinTypeEdit).SendKeys(ReadExcelData.ReadData(excelPath, "HLFin", 1));
            driver.FindElement(btnSaveFinEdit).Click();
        }

        //To get value of Financing Type column
        public string GetFinancingType()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valFinancingType, 80);
            Thread.Sleep(2000);
            string value = driver.FindElement(valFinancingType).Text;
            return value;
        }

        //To delete the added HL Financing record and validate the displayed message
        public string DeleteExistingHLFinRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkHLFinDel, 70);
            driver.FindElement(lnkHLFinDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgHLFin, 60);
            string message = driver.FindElement(msgHLFin).Text;
            return message;
        }

        //To validate Revenue Financials window
        public string ValidateRevenueFinancialsWindow()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditRevenue, 80);
            driver.FindElement(lnkEditRevenue).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 70);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To save details of Revenue Financials 
        public void SaveRevenueFinancialsDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtFY1, 80);
            driver.FindElement(txtFY1).Clear();
            driver.FindElement(txtFY1).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 1));
            driver.FindElement(txtFY).Clear();
            driver.FindElement(txtFY).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 2));
            driver.FindElement(txtLTM).Clear();
            driver.FindElement(txtLTM).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 3));
            driver.FindElement(txtFYPlus1).Clear();
            driver.FindElement(txtFYPlus1).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 4));
            driver.FindElement(txtFYPlus2).Clear();
            driver.FindElement(txtFYPlus2).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 5));
            driver.FindElement(txtFYPlus3).Clear();
            driver.FindElement(txtFYPlus3).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 6));
            driver.FindElement(txtFYPlus4).Clear();
            driver.FindElement(txtFYPlus4).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 7));
            driver.FindElement(txtFYPlus5).Clear();
            driver.FindElement(txtFYPlus5).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 8));
            driver.FindElement(btnSaveRevenue).Click();
        }

        //To Validate values of added Revenue Financials details
        public bool ValidateRevenueFinDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowValRevenue, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowValRevenue);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To validate EBITDA Financials window
        public string ValidateEBITDAFinancialsWindow()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditEBITDA, 80);
            driver.FindElement(lnkEditEBITDA).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 70);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To save details of EBITDA Financials 
        public void SaveEBITDAFinancialsDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtFY1EBITDA, 80);
            driver.FindElement(txtFY1EBITDA).Clear();
            driver.FindElement(txtFY1EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 1));
            driver.FindElement(txtFYEBITDA).Clear();
            driver.FindElement(txtFYEBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 2));
            driver.FindElement(txtLTMEBITDA).Clear();
            driver.FindElement(txtLTMEBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 3));
            driver.FindElement(txtFYPlus1EBITDA).Clear();
            driver.FindElement(txtFYPlus1EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 4));
            driver.FindElement(txtFYPlus2EBITDA).Clear();
            driver.FindElement(txtFYPlus2EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 5));
            driver.FindElement(txtFYPlus3EBITDA).Clear();
            driver.FindElement(txtFYPlus3EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 6));
            driver.FindElement(txtFYPlus4EBITDA).Clear();
            driver.FindElement(txtFYPlus4EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 7));
            driver.FindElement(txtFYPlus5EBITDA).Clear();
            driver.FindElement(txtFYPlus5EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 8));
            driver.FindElement(btnSaveEBITDA).Click();
        }

        //To Validate values of added EBITDA Financials details
        public bool ValidateEBITDAFinDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowValEBITDA, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowValEBITDA);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To validate Capex Financials window
        public string ValidateCapexFinancialsWindow()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditCapex, 80);
            driver.FindElement(lnkEditCapex).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 70);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To save details of Capex Financials 
        public void SaveCapexFinancialsDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtFY1Capex, 80);
            driver.FindElement(txtFY1Capex).Clear();
            driver.FindElement(txtFY1Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 1));
            driver.FindElement(txtFYCapex).Clear();
            driver.FindElement(txtFYCapex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 2));
            driver.FindElement(txtLTMCapex).Clear();
            driver.FindElement(txtLTMCapex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 3));
            driver.FindElement(txtFYPlus1Capex).Clear();
            driver.FindElement(txtFYPlus1Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 4));
            driver.FindElement(txtFYPlus2Capex).Clear();
            driver.FindElement(txtFYPlus2Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 5));
            driver.FindElement(txtFYPlus3Capex).Clear();
            driver.FindElement(txtFYPlus3Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 6));
            driver.FindElement(txtFYPlus4Capex).Clear();
            driver.FindElement(txtFYPlus4Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 7));
            driver.FindElement(txtFYPlus5Capex).Clear();
            driver.FindElement(txtFYPlus5Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 8));
            driver.FindElement(btnSaveCapex).Click();
        }

        //To Validate values of added Capex Financials details
        public bool ValidateCapexFinDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowValCapex, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowValCapex);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        //To validate Add Equity Holder button        
        public string ValidateAddEquityHolderButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddEquityHolder, 90);
            string btnAddEquity = driver.FindElement(btnAddEquityHolder).Displayed.ToString();
            Console.WriteLine("btnAddEquity: " + btnAddEquity);
            if (btnAddEquity.Equals("True"))
            {
                return "Add Equity Holder button is displayed";
            }
            else
            {
                return "Add Equity Holder button is not displayed";
            }
        }

        //To Click Add Equity Holder button        
        public string ValidateAddEquityTitle()
        {
            driver.FindElement(btnAddEquityHolder).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To Add Equity Holder details        
        public string SaveEquityHolderDetails()
        {
            driver.SwitchTo().Frame(1);
            driver.FindElement(txtEquitySearch).SendKeys("Techno");
            driver.FindElement(btnGo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 90);
            driver.FindElement(checkRow).Click();
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 120);
            string message = driver.FindElement(msgSuccess).Text;
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To validate value of added Equity Holder 
        public string ValidateAddedEquityHolderValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEquityHolder, 90);
            string valEquity = driver.FindElement(valEquityHolder).Text;
            return valEquity;
        }

        //To update added Equity Holder values
        public string UpdateAndValidateEquityHolderValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquityEdit, 90);
            driver.FindElement(lnkEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, txtOwnership, 90);
            driver.FindElement(txtOwnership).Clear();
            driver.FindElement(txtOwnership).SendKeys("10.00");
            driver.FindElement(btnEquitySave).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valOwnership, 120);
            Thread.Sleep(5000);
            string valOwner = driver.FindElement(valOwnership).Text;
            return valOwner;
        }

        //To copy added Equity Holder record
        public void CopyEquityHolderRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquityCopy, 100);
            driver.FindElement(lnkEquityCopy).Click();
            Thread.Sleep(4000);
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }           
        }
        public bool ValidateCopiedEquityHolderValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowEquityHoldersPost, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowEquityHoldersPost);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Edit", "Del", "Techno Coatings, Inc.", "10.00%" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To delete added Equity Holder record
        public string DeleteAndValidateEquityHolderRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquityDel, 90);
            driver.FindElement(lnkEquityDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgEquityDelete, 90);
            string message = driver.FindElement(msgEquityDelete).Text;
            return message;
        }
        //To delete added Equity Holder record in Post Transaction information tab
        public string DeleteAndValidatePostEquityHolderRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostEquityDel, 90);
            driver.FindElement(lnkPostEquityDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgPostEquityDelete, 90);
            string message = driver.FindElement(msgPostEquityDelete).Text;
            return message;
        }


        //To validate Add Board Member button        
        public string ValidateAddBoardMemberButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddBoardMember, 90);
            string btnAddBoard = driver.FindElement(btnAddBoardMember).Displayed.ToString();
            Console.WriteLine("btnAddBoard: " + btnAddBoard);
            if (btnAddBoard.Equals("True"))
            {
                return "Add Board Member button is displayed";
            }
            else
            {
                return "Add Board Member button is not displayed";
            }
        }

        //To Click Add Board Member button        
        public string ValidateAddBoardMemberTitle()
        {
            driver.FindElement(btnAddBoardMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To Add Board Member details        
        public string SaveBoardMemberDetails()
        {
            driver.SwitchTo().Frame(1);
            driver.FindElement(txtEquitySearch).SendKeys("Adam Daland");
            driver.FindElement(btnGo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkRowBoard, 120);
            driver.FindElement(checkRowBoard).Click();
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessBoard, 120);
            string message = driver.FindElement(msgSuccessBoard).Text;
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To validate value of added Board Member in Pre-Transaction Board Members section
        public string ValidateAddedBoardMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBoardMember, 90);
            string valMember = driver.FindElement(valBoardMember).Text;
            return valMember;
        }

        //To copy added Board Member detail
        public void CopyBoardMemberRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBoardCopy, 90);
            driver.FindElement(lnkBoardCopy).Click();
            Thread.Sleep(2000);
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //To Validate values of copied Board Member in Post Transaction Information tab
        public bool ValidateCopiedBoardMemberValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowBoardMemberPost, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowBoardMemberPost);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Del", "Adam Daland", "Investors Management Corp", "" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To delete added Board Member record
        public string DeleteAndValidateBoardMemberRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBoardDel, 90);
            driver.FindElement(lnkBoardDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgBoardDelete, 90);
            string message = driver.FindElement(msgBoardDelete).Text;
            return message;
        }

        //To delete added Board Member record in Post-Transaction Board Members section
        public string DeleteAndValidatePostBoardMemberRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostBoardDel, 90);
            driver.FindElement(lnkPostBoardDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgPostBoardDelete, 90);
            string message = driver.FindElement(msgPostBoardDelete).Text;
            return message;
        }
        //To validate message "*Save Record to Add Lenders"       
        public string ValidateSaveRecordMessage()
        {
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, msgSaveRec, 100);
            string message = driver.FindElement(msgSaveRec).Text;
            return message;
        }

        //To Add Debt Structure details        
        public string SaveDebtStructureDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboSecurityTypeDebt, 120);
            driver.FindElement(comboSecurityTypeDebt).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 1));
            driver.FindElement(txtMaturityDate).Click();
            driver.FindElement(txtInterest).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 2));
            driver.FindElement(txtOID).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 3));
            driver.FindElement(txtAmortization).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 4));
            driver.FindElement(txtCallProvisions).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 5));
            driver.FindElement(txtMandatory).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 6));
            driver.FindElement(txtCovenants).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 7));
            driver.FindElement(txtFees).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 8));
            driver.FindElement(txtFacility).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 9));
            driver.FindElement(btnSaveDebt).Click();
            string message = driver.FindElement(msgSuccessDebt).Text;
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To Validate values of added Debt Structure in Pre Transaction Information tab
        public bool ValidateDebtStructureValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowDebtStrPre, 140);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowDebtStrPre);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string value2 = DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine(value2);
            string[] expectedValue = {"LC Facility", "", "USD 10.00", value2, "10.00", "10.000%", "USD 10.00", "USD 10.00", "USD 10.00", "10.00", "USD 10.00" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        //To update added Debt Structure values
        public string UpdateAndValidateDebtStructureValue(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquityEdit, 90);
            driver.FindElement(lnkEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, comboSecurityTypeDebt, 90);
            driver.FindElement(comboSecurityTypeDebt).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 10));
            driver.FindElement(btnSaveDebt).Click();
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valDebtSecurityType, 90);
            Thread.Sleep(3000);
            string valSecurityType = driver.FindElement(valDebtSecurityType).Text;
            return valSecurityType;
        }

        //To delete added Debt Structure record
        public string DeleteAndValidateDebtStructureRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDebtDel, 100);
            driver.FindElement(lnkDebtDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgDebtDelete, 110);
            string message = driver.FindElement(msgDebtDelete).Text;
            return message;
        }

        //To add lender details
        public string AddLenderDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquityEdit, 90);
            driver.FindElement(lnkEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewLender, 90);
            driver.FindElement(btnNewLender).Click();
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEquitySearch, 90);
            driver.FindElement(txtEquitySearch).SendKeys("ABC");
            driver.FindElement(btnGo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 90);
            driver.FindElement(checkRow).Click();
            driver.FindElement(txtLenderAmt).SendKeys("10");
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 150);
            string message = driver.FindElement(msgSuccess).Text;
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //Validate added lender details
        public bool ValidateLenderValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowLender, 100);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowLender);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = {"ABC", "10.00"};
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To update lender details
        public string EditLenderDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkLenderEdit, 120);
            driver.FindElement(lnkLenderEdit).Click();
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, txtLoanAmt, 90);
            driver.FindElement(txtLoanAmt).Clear();
            driver.FindElement(txtLoanAmt).SendKeys("20");
            driver.FindElement(btnSaveLender).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessDebt, 90);
            string message = driver.FindElement(msgSuccessDebt).Text;
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To get updated value of Loan Amount
        public string GetLoanAmount()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLoanAmt, 90);
            Thread.Sleep(3000);
            string Amount = driver.FindElement(valLoanAmt).Text;           
            return Amount;
        }

        //To delete lender details
        public string DeleteLenderDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkLenderDelete, 90);
            driver.FindElement(lnkLenderDelete).Click();
            Thread.Sleep(4000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(5000);           

            try
            {
                string rowDisplayed = driver.FindElement(rowLenderPostDel).Displayed.ToString();
                Console.WriteLine("rowDisplayed :" + rowDisplayed);               
                return rowDisplayed;
            }
            catch (Exception)
            {                
                return "No row displayed";
            }
        }

        //To close lender page
        public void CloseLenderDetailsPage()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEquityClose, 100);
            driver.FindElement(btnEquityClose).Click();
        }

        //To Click Add Equity Holder button in Post- Transaction Information tab       
        public string ValidatePostAddEquityTitle()
        {
            driver.FindElement(btnPostAddEquity).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }
        //To validate value of added Equity Holder in Post- Transaction Information tab
        public string ValidatePostAddedEquityHolderValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPostEquityHolder, 90);
            string valEquity = driver.FindElement(valPostEquityHolder).Text;
            return valEquity;
        }

        //To update added Equity Holder values in Post Transaction Information tab
        public string UpdateAndValidatePostEquityHolderValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostEquityEdit, 90);
            driver.FindElement(lnkPostEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, txtOwnership, 90);
            driver.FindElement(txtOwnership).Clear();
            driver.FindElement(txtOwnership).SendKeys("10.00");
            driver.FindElement(btnEquitySave).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valPostOwnership, 90);
            Thread.Sleep(5000);            
            string valOwner = driver.FindElement(valPostOwnership).Text;
            return valOwner;
        }

        //To Click Add Board Member button in Post- Transaction Information tab       
        public string ValidatePostAddBoardMemberTitle()
        {
            driver.FindElement(btnPostAddBoardMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }
        //To validate value of added Board Member in Post-Transaction Board Members section
        public string ValidatePostAddedBoardMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPostBoardMember, 90);
            string valMember = driver.FindElement(valPostBoardMember).Text;
            return valMember;
        }

        //To Click Add Debt Structure button        
        public string ValidatePostAddDebtTitle()
        {
            driver.FindElement(btnPostAddDebtStructure).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To Validate values of added Debt Structure in Post Transaction Information tab
        public bool ValidatePostDebtStructureValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowPostDebtStrPre, 100);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowPostDebtStrPre);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string value2 = DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            string[] expectedValue = { "LC Facility", "", "USD 10.00", value2, "10.00", "10.000%", "USD 10.00", "USD 10.00", "USD 10.00", "10.00", "USD 10.00" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To update added Debt Structure values in Post Transaction Information tab
        public string UpdateAndValidatePostDebtStructureValue(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostEquityEdit, 90);
            driver.FindElement(lnkPostEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, comboSecurityTypeDebt, 90);
            driver.FindElement(comboSecurityTypeDebt).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 10));
            driver.FindElement(btnSaveDebt).Click();
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valPostDebtSecurityType, 90);
            Thread.Sleep(3000);
            string valSecurityType = driver.FindElement(valPostDebtSecurityType).Text;
            return valSecurityType;
        }

        //To delete added Debt Structure record in Post Transaction Information    
        public string DeleteAndValidatePostDebtStructureRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostDebtDel, 90);
            driver.FindElement(lnkPostDebtDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgPostDebtDelete, 90);
            string message = driver.FindElement(msgPostDebtDelete).Text;
            return message;
        }

        //To add lender details in Post Transaction Information
        public string AddLenderDetailsPostTrans()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostEquityEdit, 90);
            driver.FindElement(lnkPostEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewLender, 90);
            driver.FindElement(btnNewLender).Click();
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEquitySearch, 90);
            driver.FindElement(txtEquitySearch).SendKeys("ABC");
            driver.FindElement(btnGo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 90);
            driver.FindElement(checkRow).Click();
            driver.FindElement(txtLenderAmt).SendKeys("10");
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 150);
            string message = driver.FindElement(msgSuccess).Text;
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To delete added Debt Structure record
        public string DeleteAndValidateDebtStructureRecordPostTrans()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostDebtDel, 90);
            driver.FindElement(lnkPostDebtDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgPostDebtDelete, 90);
            string message = driver.FindElement(msgPostDebtDelete).Text;
            return message;
        }

        //To save Staff Role       
        public string SaveStaffRoleValues()
        {
            driver.SwitchTo().Frame(1);
            driver.FindElement(txtClassification).SendKeys("Financing");
            driver.FindElement(txtEquitySearch).SendKeys("Sonika Goyal");
            driver.FindElement(btnGo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkStaffRole, 110);
            driver.FindElement(checkStaffRole).Click();
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessStaff, 120);
            string message = driver.FindElement(msgSuccessStaff).Text;
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To Validate added values of Post-Transaction Staff Roles section   
        public bool VerifyAddedValuesOfStaffRoles()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStaffRoles, 100);
            IReadOnlyCollection<IWebElement> valRoles = driver.FindElements(valStaffRoles);
            var actualValue = valRoles.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Sonika Goyal", "Financing" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //To delete added Staff Role record
        public string DeleteAndValidateStaffRoleRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkStaffRoleDel, 90);
            driver.FindElement(lnkStaffRoleDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgStaffRoleDel, 90);
            string message = driver.FindElement(msgStaffRoleDel).Text;
            return message;
        }
    }
}


