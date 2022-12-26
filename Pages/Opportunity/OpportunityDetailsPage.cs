using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class OpportunityDetailsPage : BaseClass
    {
        By btnEdit = By.CssSelector("input[value=' Edit ']");
        By valOpportunity = By.XPath("//div[contains(@id,'Name')]");
        By btnDNDOnOff = By.CssSelector("input[name='dnd_on_off']");
        By imgLock = By.CssSelector("img[title='Locked']");
        By titleMessage = By.XPath("//span[contains(text(),'Record Locked')]");
        By txtMessage = By.XPath("//*/tr[2]/td");
        By linkHere = By.CssSelector("#bodyCell > table > tbody > tr:nth-child(2) > td > a");
        By tblApprovalHistory = By.CssSelector("div[class='relatedProcessHistory']");
        //By txtStatus = By.CssSelector("div.extraStatusDiv_P>span");
        By txtStatus = By.CssSelector("div[class*='extraStatusDiv']>span");
        By txtApprover = By.CssSelector("[id*='RelatedProcessHistoryList_body'] > table > tbody > tr:nth-child(3) > td:nth-child(5) > a");
        By linkApproveReject = By.CssSelector("[id*='RelatedProcessHistoryList_body']>table>tbody> tr:nth-child(3) > td.actionColumn > a:nth-child(2)");
        By txtComments = By.Id("Comments");
        By btnReject = By.CssSelector("input[title='Reject']");
        By btnApprove = By.CssSelector("input[title='Approve']");
        By txtOpportunityName = By.CssSelector("div[id*='Namej_id0_j_id55']");
        By btnRequestEng = By.CssSelector("input[title='Request Engagement']");
        By msgSuccess = By.CssSelector("div[class='messageText']");
        By valClient = By.CssSelector("div[id*='CF00Ni000000D7zoCj']");
        By valSubject = By.CssSelector("div[id*='CF00Ni000000D80OZj']");
        By valJobType = By.CssSelector("div[id*='00Ni000000D8hWWj']");
        By btnNBCForm = By.Name("nbc_form");
        By btnNBCFormL = By.CssSelector("input[name='nbc_form_c']");
        By titleNBCForm = By.CssSelector(" div.pbBody > table > tbody > tr > td.instructions > p:nth-child(1)");
        By linkRequestDate = By.CssSelector("div.pbBody > div:nth-child(21) > table > tbody > tr:nth-child(3) > td:nth-child(4) > span > span > a");
        By btnSave = By.CssSelector("input[name='save']");
        By chkInternalTeamPrompt = By.CssSelector("input[name*='FnLTz']");
        By chkInitiator = By.XPath("//th[contains(text(),'Initiator')]/following::tbody/tr/td[5]/input");
        By chkPrincipal = By.CssSelector("input[name*='internalTeam:j_id73:2:j_id75']");
        By chkManager = By.CssSelector("input[name*='internalTeam:j_id73:3:j_id75']");
        By btnSaveITTeam = By.CssSelector("input[name*='_id120:j_id121']");
        By listStaff = By.XPath("/html/body/ul");
        By btnReturnToOpp = By.CssSelector("input[value*='Return To Opportunity']");
        By btnConverttoEng = By.CssSelector("input[value='Convert to Engagement']");
        By chkConvertedtoEng = By.CssSelector("input[name*='FaP8F']");
        By comboJobType = By.CssSelector("select[id*= 'hWW']");
        By txtStaff = By.CssSelector("input[placeholder*='Begin Typing Name']");
        By chkUpPrincipal = By.XPath("//th[contains(text(),'Principal')]/following::tbody/tr/td[7]/input");
        By chkUpSeller = By.XPath("//th[contains(text(),'Seller')]/following::tbody/tr/td[6]/input");
        By chkUpManager = By.XPath("//th[contains(text(),'Manager')]/following::tbody/tr/td[8]/input");
        By chkAdmin = By.CssSelector("input[name*='j_id73:9:j_id75']");
        By btnFEIS = By.Name("feis_and_fairness_forms");
        By linkRequestDateFAS = By.CssSelector("div.pbBody > div:nth-child(23) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");
        By btnCounterparties = By.CssSelector(".pbButton > input[title = 'Counterparties']");
        By btnPortfolioValuation = By.CssSelector("input[title='Portfolio Valuation']");
        By lblValuationPeriods = By.CssSelector("div[class='pbBody']> font > b");
        By valStage = By.CssSelector("div[id*='OA']");
        By valPitchDate = By.CssSelector("div[id*='PDj']");
        By valWinProb = By.CssSelector("div[id*='OKj']");
        By valTxnSize = By.CssSelector("div[id*='80P4j']");
        By valRetainer = By.CssSelector("div[id*='DwTdFj']");
        By valMonthlyFee = By.CssSelector("td[id*='00Ni000000FmBzij']>div[id*='FmBzij']");
        By valContingentFee = By.CssSelector("div[id*='GE9j']");
        By rowOppComments = By.CssSelector("div[id*='00Ni000000FnLT7_body']>table>tbody>tr");
        By msgOppComments = By.CssSelector("div[id*='00Ni000000FnLT7_body']>table>tbody>tr>th");
        By valOppComments = By.CssSelector("div[id*='00Ni000000FnLT7_body']>table>tbody>tr[class*='first']>td:nth-child(3)");
        By linkDel = By.CssSelector("td[class='actionColumn']>a[title='Delete - Record 1 - View']");
        By btnClone = By.CssSelector("input[title='Clone']");
        By chkUpMgr = By.CssSelector("input[name*='4:j_id47']");
        By chkUpAssociate = By.XPath("//th[contains(text(),'Associate')]/following::tbody/tr/td[9]/input");
        By chkUpAnalyst = By.XPath("//th[contains(text(),'Analyst')]/following::tbody/tr/td[10]/input");
        By lnkReDisplayRec = By.CssSelector(" table > tbody > tr:nth-child(2) > td > a:nth-child(4)");
        By rowUser = By.XPath("//html/body/span[2]/form/div[1]/div/div/div/div[2]/table/tbody/tr/td[1]/div/label");
        By chkCheckedAdmin = By.CssSelector("input[name*='1:j_id45:9:j_id47']");
        By chkCheckedInitiator = By.CssSelector("input[name*='0:j_id45:0:j_id47']");
        By msgHLIntTeam = By.CssSelector("div[id*='pgfrmId:internalTeam:j']");
        By lnkRecordTypeChange = By.CssSelector("div[id*='RecordTypej_id0_j_id55_ileinner'] > a");
        By comboRecType = By.CssSelector("select[id*='p3']");
        By btnContinue = By.CssSelector("input[value='Continue']");
        By comboLOB = By.CssSelector("select[id*='hW2']");
        By valLOB = By.CssSelector("div[id*='W2j_id0_j_id55_ileinner']");
        By txtMonthlyFee = By.CssSelector("input[name*='FmBzi']");
        By lnkTrialExp = By.CssSelector("div:nth-child(15) > table > tbody > tr:nth-child(2) > td.dataCol.col02 > span > span > a");
        By txtContingentFee = By.CssSelector("input[name*='FkGE9']");
        //By lnkEstClosedDate = By.CssSelector("div:nth-child(25) > table > tbody > tr:nth-child(2) > td.dataCol.col02 > span > span > a");
        //after adding Co-exist
        By lnkEstClosedDate = By.XPath("//input[@id='00Ni000000FnLTw']/following-sibling::span/a");
        
        By comboWomenLed = By.CssSelector("select[name*='RNgW']");
        By comboFairnessOpinion = By.CssSelector("select[id*='GbaZ7']");
        By valRegEng = By.CssSelector("span[id*='id45']>ul");
        By txtSICCode = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000G9CKu']");
        By txtOppDesc = By.CssSelector("textarea[name*='D80Oy']");
        By txtMarketCap = By.CssSelector("input[name*='D80P4']");
        By txtRetainer = By.CssSelector("input[name*='TdF']");
        By txtReferralContact = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D80Oo']");
        By comboConfAgreement = By.CssSelector("select[name*='D8war']");
        By lnkOutcomeDate = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(5) > td:nth-child(4) > span > span > a");
        By comboOutcome = By.CssSelector("select[name*='D8hIa']");
        By lnkDateEngagedCF = By.XPath("//input[@id='00Ni000000FnLTv']/following-sibling::span/a");
        By checkNBCApproved = By.CssSelector("input[name='00Ni000000FmBzh']");
        By txtClient = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D7zoC']");
        By txtSubject = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D80OZ']");
        By valEstFin = By.CssSelector("div[id*='id35']");
        By valEstFinCF = By.CssSelector("div[id*='id35']");
        By comboTombstonePermission = By.CssSelector("select[name='00N5A00000GzTIZ']");
        By txtFee = By.CssSelector("input[name*='FmBzg']");
        By lnkDateEngaged = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(10) > td:nth-child(4) > span > span > a");
        By checkFEISApproved = By.CssSelector("input[name='00Ni000000FmBzh']");
        By lnkOutcomeDateFAS = By.CssSelector("div:nth-child(23) > table > tbody > tr:nth-child(5) > td:nth-child(4) > span > span > a");
        By comboRecordType = By.CssSelector("select[name='00Ni000000D8hW2']");
        By valRecType = By.CssSelector("div[id*='RecordTypej']");
        By valOppNum = By.CssSelector("div[id*='VbIj_id0_j_id55_ileinner']");
        By btnNewComment = By.CssSelector("input[value='New Comment']");
        By comboCommentType = By.CssSelector("select[id*='00Ni000000FnLSo']");
        By txtCommentDesc = By.CssSelector("textarea[id*='FnLSp']");
        By msgError = By.CssSelector("div[class='errorMsg']");
        By valComment = By.CssSelector("div[id*='LT7_body'] > table > tbody > tr.dataRow.even.last.first > td:nth-child(3)");
        By lnkDelComment = By.CssSelector("div[id*='LT7_body']> table > tbody > tr.dataRow.even.last.first > td.actionColumn > a:nth-child(2)");
        By txtDateEngagedCF = By.CssSelector("input[name*='FnLTv']");
        By lnkPitchDateFAS = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(7) > td:nth-child(4) > span > span > a");
        By lnkValuationDate = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(8) > td:nth-child(4) > span > span > a");
        By lnkDateCASignedFAS = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(1) > td:nth-child(4) > span > span > a");
        By lnkDateCAExpiresFAS = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");
        By lnkPitchDateFR = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(6) > td:nth-child(4) > span > span > a");
        By chkDebtConfirmed = By.CssSelector("input[name*='4yQC']");
        By txtTotalDebtHL = By.CssSelector("input[name*='4yQD']");
        By txtClientDesc = By.CssSelector("textarea[name*='4yQ7']");
        By comboLegalAdvisorComp = By.CssSelector("span>select[name*='4yQA']");
        By comboLegalAdvisorHL = By.CssSelector("span>select[name*='4yQB']");
        By comboEUSecurities = By.CssSelector("span>select[name*='4yQ8']");
        By lnkDateCASignedFR = By.CssSelector("div:nth-child(17) > table > tbody > tr:nth-child(1) > td:nth-child(4) > span > span > a");
        By lnkDateCAExpiresFR = By.CssSelector("div:nth-child(17) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");
        By lnkEstimatedClosedDateFR = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(3) > td:nth-child(4) > span > span > a");
        By lnkOutcomeDateFR = By.CssSelector(" div:nth-child(19) > table > tbody > tr:nth-child(5) > td:nth-child(4) > span > span > a");
        By chk2ndInitiator = By.CssSelector("input[name*=':0:j_id75']");
        By valHLEntity = By.CssSelector("div[id*='WRj']");
        By valERPHLEntity = By.CssSelector("div[id*='fYj']");
        By valERPLegalEntityId = By.CssSelector("div[id*='fhj']");
        By valERPSubmittedToSync = By.CssSelector("div[id*='ftj']");
        By valERPProjectNumber = By.CssSelector("div[id*='fpj']");
        By valERPProjectName = By.CssSelector("div[id*='foj']");
        By valERPLOB = By.CssSelector("div[id*='fbj']");
        By valERPIG = By.CssSelector("div[id*='faj']");
        By valERPTemplate = By.CssSelector("div[id*='fxj']");
        By valERPUnitID = By.CssSelector("div[id*='fVj']");
        By valERPUnit = By.CssSelector("div[id*='fWj']");
        By valERPLegalEntityID = By.CssSelector("div[id*='fgj']");
        By valERPEntityCode = By.CssSelector("div[id*='fXj']");
        By valERPLegCode = By.CssSelector("div[id*='fij']");
        By valIG = By.CssSelector("div[id*='T3j']");
        By valERPID = By.CssSelector("div[id*='fZj']");
        By valERPProjStatusCode = By.CssSelector("div[id*='frj']");
        By valERPLastIntStatus = By.CssSelector("div[id*='ffj']");
        By valERPResponseDate = By.CssSelector("div[id*='efej']");
        By valERPError = By.CssSelector("div[id*='fdj']");
        By valERPProductType = By.CssSelector("div[id*='g8j']");
        By valERPProductTypeCode = By.CssSelector("div[id*='fkj']");
        By valERPEmailID = By.CssSelector("div[id*='efj']");
        By checkERPUpdateDFF = By.CssSelector("div[id*='efzj']>img");
        By comboPrimaryOffice = By.CssSelector("select[id*='VIA']");
        By valPrimaryOffice = By.CssSelector("div[id*='VIA']");
        By comboIG = By.CssSelector("select[id*='VT3']");
        By valIGCF = By.CssSelector("div[id*='VT3']");
        By comboSector = By.CssSelector("select[id*='PI']");
        By valSector = By.CssSelector("div[id*='PI']");
        By comboClientOwnership = By.CssSelector("select[id*='d2T']");
        By valClientOwnership = By.CssSelector("div[id*='2Tj']");
        By comboSubjectOwnership = By.CssSelector("select[id*='d2U']");
        By valSubjectOwnership = By.CssSelector("div[id*='2Uj']");
        By lnkSyncDate = By.CssSelector("table > tbody > tr:nth-child(1) > td.dataCol.col02 > span > span > a");
        By btnNewContract = By.CssSelector("input[value='New Contract']");
        By titlePage = By.CssSelector("h2[class='pageDescription']");
        By txtContractName = By.CssSelector("input[id*='Name']");
        By txtBillingContact = By.CssSelector("span>input[id*='CF00N5A00000M0ebh']");
        By lnkOpportunity = By.CssSelector("a[id*='A00000M0ed1']");
        By checkIsMain = By.CssSelector("div[id*='ed1_body']> table > tbody > tr.dataRow.even.last.first > td.dataCell.booleanColumn > img");
        By lnkEditContract = By.CssSelector("div[id*='ed1_body'] > table > tbody > tr.dataRow.even.last.first > td.actionColumn > a:nth-child(1)");
        By titleEditContract = By.CssSelector("h1[class='pageType']");
        By checkSelectedIsMain = By.CssSelector("input[name*='ZS']");
        By checkIsMainContract1 = By.CssSelector("div[id*='ed1_body']> table > tbody > tr.dataRow.even.first > td.dataCell.booleanColumn > img");
        By checkIsMainContract2 = By.CssSelector("div[id*='ed1_body']> table > tbody > tr.dataRow.odd.last > td.dataCell.booleanColumn > img");
        By valContract1 = By.CssSelector("div[id*='ed1_body'] > table > tbody > tr:nth-child(2) > th > a");
        By valContract2 = By.CssSelector("div[id*='ed1_body'] > table > tbody > tr:nth-child(3) > th > a");
        By txtClientContract = By.CssSelector("span>input[id*='CF00N5A00000M0ebj']");
        By btnModifyRoles = By.CssSelector("td[id*='j_id0:j_id1:j_id2:j_id3:pbHLInternalTeam:j_id4:bottom']>a");
        By checkInitiator = By.CssSelector("input[name*='internalTeam:j_id73:0:j_id75']");
        By checkSeller= By.CssSelector("input[name*='internalTeam:j_id73:1:j_id75']");
        By checkPrincipal = By.CssSelector("input[name*='internalTeam:j_id73:2:j_id75']");
        By checkManager = By.CssSelector("input[name*='internalTeam:j_id73:3:j_id75']");
        By checkAssociate = By.CssSelector("input[name*='internalTeam:j_id73:4:j_id75']");
        By checkAnalyst = By.CssSelector("input[name*='internalTeam:j_id73:5:j_id75']");
        By checkSpeciality = By.CssSelector("input[name*='internalTeam:j_id73:6:j_id75']");
        By checkPE = By.CssSelector("input[name*='internalTeam:j_id73:7:j_id75']");
        By checkPublic = By.CssSelector("input[name*='internalTeam:j_id73:8:j_id75']");
        By checkAdmin = By.CssSelector("input[name*='internalTeam:j_id73:9:j_id75']");
        By checkRMS = By.CssSelector("input[name*='internalTeam:j_id73:10:j_id75']");
        By checkExpenseOnly = By.CssSelector("input[name*='internalTeam:j_id73:11:j_id75']");
        By checkNonRegistered = By.CssSelector("input[name*='internalTeam:j_id73:12:j_id75']");
        By lnkEngagement = By.CssSelector("div[id*='zAz_body']>table>tbody>tr.dataRow.even.last.first>th>a");
        By txtAnticipatedRevenue = By.CssSelector("input[name*='zNU']");
        By valDefaultClient = By.CssSelector("div[id*='DuhQp_body'] > table > tbody > tr:nth-child(2)>th>a");
        By txtClientSubject = By.CssSelector("span>input[id*='CF00Ni000000D9DcG']");
        By valNewClient = By.CssSelector("div[id*='p_body'] > table > tbody > tr:nth-child(4)> th > a");
        By valClientType = By.CssSelector("div[id*='uhQp_body'] > table > tbody > tr:nth-child(4)>td:nth-child(3)");
        By lnkEditClient = By.CssSelector("div[id*='hQp_body'] > table > tbody > tr.dataRow.even.last > td.actionColumn > a:nth-child(1)");
        By comboType = By.CssSelector("select[name*='D9DcL']");
        By lnkDelClient = By.CssSelector("div[id*='DuhQp_body'] > table > tbody > tr.dataRow.even.last > td.actionColumn > a:nth-child(2)");
        By btnAdditionalClient = By.CssSelector("input[value*='Additional Client']");
        By titleAdditionalClient = By.CssSelector("h2[class='pageDescription']");
        By comboTypes = By.XPath("//select[@name='00Ni000000D9DcL']/option");
        By valType = By.CssSelector("div:nth-child(3)>table>tbody>tr:nth-child(2)>td:nth-child(2)>span>select>option:nth-child(2)");
        By valPrimary = By.CssSelector("div[id*='DuhQp_body']>table > tbody > tr.dataRow.even.last > td.dataCell.booleanColumn > img");
        By btnCancel = By.CssSelector("input[value='Cancel']");
        By titleOpp = By.CssSelector("h2[class='mainTitle']");
        By valOppClientSubject = By.CssSelector("span>input[id*='CF00Ni000000DuhQp']");
        By checkPrimary = By.CssSelector("input[name*='Dca']");
        By rowAdditional = By.CssSelector("div[id*='DuhQp_body']>table>tbody>tr:nth-child(4)");
        By valPrimaryClient = By.CssSelector("div[id*='DuhQp_body']>table > tbody > tr.dataRow.even.first > td.dataCell.booleanColumn > img");
        By valPrimarySubject = By.CssSelector("div[id*='DuhQp_body']>table > tbody > tr.dataRow.odd.last > td.dataCell.booleanColumn > img");
        By valCompFeeAttrParty = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(4)>th>a");
        By valCompKeyCreditor = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(5)>th>a");
        By valTypeFeeAttrParty = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(4)>td:nth-child(3)");
        By valTypeKeyCreditor = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(5)>td:nth-child(3)");
        By valRecTypeFeeAttrParty = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(4)>td:nth-child(4)");
        By valRecTypeKeyCreditor = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(5)>td:nth-child(4)");
        By comboAdditionalClient = By.CssSelector("select[name*='FmBza']");
        By comboAdditionalSubject = By.CssSelector("select[name*='FmBzb']");
        By valWomenLed = By.CssSelector("div[id*='NgWj_id0_j_id55_ileinner']");
        By txtWomenLed = By.CssSelector("div:nth-child(25)>table>tbody>tr:nth-child(4)>td:nth-child(3)");
        By txtWomenLedFVA = By.CssSelector("div:nth-child(27)>table>tbody>tr:nth-child(3)>td:nth-child(3)");
        By txtWomenLedFR = By.CssSelector("div:nth-child(23)>table>tbody>tr:nth-child(4)>td:nth-child(3)");
        By drpdownWomenLed = By.CssSelector("select[name='00N6e00000MRNgW']");
        By txtEstimatedFees = By.XPath("//*[@id='00N6e00000H0zNU']");
        By valICOContractName = By.CssSelector("div[id*='M0ed1_body'] > table > tbody > tr:nth-child(2) > th > a");
        By txtStagePriority = By.CssSelector("select[id*='D80OA']");
        By valERPContractType = By.CssSelector("div[id*='M0ed1_body'] > table > tbody > tr:nth-child(2) > td:nth-child(4)");
        By valERPLegalEntityName = By.CssSelector("div[id*='M0ed1_body'] > table > tbody > tr:nth-child(2) > td:nth-child(6)");
        By valBillTo = By.CssSelector("div[id*='M0ed1_body'] > table > tbody > tr:nth-child(2) > td:nth-child(8) > a");
        By valContractStartDate = By.CssSelector("div[id*='M0ed1_body'] > table > tbody > tr:nth-child(2) > td:nth-child(9)");

        By valLegalEntity = By.CssSelector("div[id*='CF00N5A00000M0eg5j'] a");
        By valNonPublicInfo = By.CssSelector("div[id*='00Ni000000FaBznj']");
        By valStaffMember = By.CssSelector("div[id*='team:0:j_id7'] > label");
        By valBeneOwnerAndControlPersonForm = By.CssSelector("div[id*='00N5A00000HERR2j']");
       
        By valExternalDisclosureStat = By.CssSelector("div[id*='00Ni000000FlHaPj']");
        By valLineOfBusiness = By.CssSelector("div[id*='00Ni000000D8hW2j']");
        By valAdditionalClient = By.CssSelector("div[id*='00Ni000000FmBzaj']");
        By valAdditionalSubject = By.CssSelector("div[id*='00Ni000000FmBzbj']");
        By valReferralType = By.CssSelector("div[id*='00Ni000000FF5uSj']");
        By btnDeleteOpportunity = By.CssSelector("td[id*='topButtonRow'] > input[value='Delete']");
        By lnkInternalTeam = By.CssSelector("th[class=' dataCell  '] a");
        By btnDeleteInternalTeam = By.CssSelector("td[id='topButtonRow'] > input[value='Delete']");

        By btnMassEditRecords = By.CssSelector("input[name='edit_record']");
        By txtAdditionalClientSubjects = By.CssSelector("h2[class='slds-card__header-title']>span");
        By lnkAdditionalClientSubjects = By.CssSelector("a[id*='DuhQp_link']>span");
        By btnnewAdditionalClientSubject = By.CssSelector("input[name *= 'DuhQp']");
        By inputAdditionalClientSubject = By.CssSelector("input[id = 'CF00Ni000000D9DcG']");

        By checkBoxCoExist = By.CssSelector("div[id*='00N6e00000MRVFOj_id0_j_id55_ileinner'] > img");
        By imputCoExist = By.XPath("//input[@id='00N6e00000MRVFO']");

        By txtDefaultTab = By.XPath("//lightning-tab-bar/ul/li[@title='Public Sensitivity']");

        By btnGo = By.XPath("//input[@type='submit']");
        By btnNewOpportunitySector = By.XPath("//input[@value='New Opportunity Sector']");
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By imgCoverageSectorDependencies = By.CssSelector("img[alt = 'Coverage Sector Dependencies']");
        By imgCoverageSectorDependencyLookUp = By.XPath("//img[@alt='Coverage Sector Dependency Lookup (New Window)']");
        By txtSearchBox = By.XPath("//input[@title='Go!']/preceding::input[1]");
        By linkCoverageSectorDependencyName = By.XPath("//a[@href='#']");
        By btnSaveOpportunitySector = By.XPath("(//input[@title='Save'])[1]");
        By btnDeleteOpportunitySector = By.XPath("(//input[@title='Delete'])[1]");
        By valOpportunitySectorName = By.XPath("//td[contains(text(),'Opportunity Sector')]/following::div[1]");
        By linkOpportunityName = By.XPath("(//td[contains(text(),'Opportunity')])[2]/../td[2]/div/a");
        By linkSectorName = By.XPath("//*/th[contains(text(),'Opportunity Sector')]/following::tr/th/a");
        By btnApplyFilters = By.XPath("//input[@title='Apply Filters']");
        By linkOpportunitySector = By.XPath("//*/span[contains(text(),'Opportunity Sectors')]");
        By inputCoverageType = By.XPath("//input[@id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By inputPrimarySector = By.XPath("//input[@id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By inputSecondarySector = By.XPath("//input[@id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By inputTertiarySector = By.XPath("//input[@id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By btnEditCompCoverageSector = By.XPath("//input[@title='Edit']");

        public bool VerifyIfOpportunitySectorQuickLinkIsDisplayed()
        {
            bool result = false;
            if (driver.FindElement(linkOpportunitySector).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void ClickNewOpportunitySectorButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOpportunitySector, 120);
            driver.FindElement(btnNewOpportunitySector).Click();
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

            //Click on Show Filters link
            //driver.FindElement(linkShowFilters).Click();

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

        public void SaveNewOpportunitySectorDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveOpportunitySector, 120);
            driver.FindElement(btnSaveOpportunitySector).Click();
            Thread.Sleep(2000);
        }

        public string GetOpportunitySectorName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOpportunitySectorName, 120);
            string name = driver.FindElement(valOpportunitySectorName).Text;
            return name;
        }

        public void NavigateToOpportunityDetailPageFromOpportunitySectorDetailPage()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkOpportunityName, 120);
            driver.FindElement(linkOpportunityName).Click();
            Thread.Sleep(2000);
        }

        public void NavigateToCoverageSectorDependenciesPage()
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgCoverageSectorDependencies).Click();
            Thread.Sleep(2000);
        }

        public bool VerifyOpportunitySectorAddedToOpportunityOrNot(string sectorName)
        {
            Thread.Sleep(5000);
            bool result = false;
            if (driver.FindElement(linkSectorName).Text == sectorName)
            {
                result = true;
            }
            return result;
        }

        public void DeleteOpportunitySector()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSectorName, 120);
            driver.FindElement(linkSectorName).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteOpportunitySector, 120);
            driver.FindElement(btnDeleteOpportunitySector).Click();
            Thread.Sleep(2000);

            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(3000);
        }



        public bool ValidateIfTailExpiresFieldIsRequiredWhenRequestingForEngagement()
        {
            driver.FindElement(btnRequestEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRegEng, 80);
            if (driver.FindElement(valRegEng).Displayed)
            {
                string errorMessages = driver.FindElement(valRegEng).Text;
                if (errorMessages.Contains("Estimated Fees - Tail Expires."))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public string VerifyIfCoExistFieldIsEditableOrNot()
        {
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            string enb = driver.FindElement(imputCoExist).GetAttribute("Type");
            if (enb == "hidden")
            {
                driver.FindElement(btnCancel).Click();
                Thread.Sleep(2000);
                return "CoExist field is not editable ";
            }
            else
            {
                driver.FindElement(btnCancel).Click();
                Thread.Sleep(2000);
                return "CoExist field is editable ";
            }
        }

        public string ValidateIfCoExistFieldIsPresentAndCheckedOrNot()
        {
            if (driver.FindElement(checkBoxCoExist).Displayed)
            {
                string value = driver.FindElement(checkBoxCoExist).GetAttribute("alt");
                if (value=="Not Checked")
                {
                    return "CoExist checkbox is displayed and not-checked";
                }
                else
                {
                    return "CoExist checkbox is displayed and checked";
                }
            }
            else
            {
                return "CoExist checkbox is not displayed.";
            }
        }

        //Get Opportunity Number
        public string GetOppNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOppNum);
            string oppNum = driver.FindElement(valOppNum).Text;
            return oppNum;
        }
        //Get LOB
        public string GetLOB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLOB);
            string LOB = driver.FindElement(valLOB).Text;
            return LOB;
        }

        //Get Industry Group
        public string GetIndustryGroup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIG);
            string IG = driver.FindElement(valIG).Text.Substring(0, 3);
            return IG;
        }

        public string ValidateOpportunityDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            string opportunityValue = driver.FindElement(valOpportunity).Text;
            return opportunityValue;
        }

        public string ValidateDNDButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            Thread.Sleep(4000);
            string value = driver.FindElement(btnDNDOnOff).Displayed.ToString();
            Console.WriteLine(value);
            if (value.Equals("True"))
            {
                return "DND On/Off button is displayed";
            }
            else
            {
                return "DND On/Off button is not displayed";
            }
        }
        public void ClickDNDButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDNDOnOff, 80);
            driver.FindElement(btnDNDOnOff).Click();
            Thread.Sleep(3000);
            IAlert alert = driver.SwitchTo().Alert();
            //string txtMessage = alert.Text;
            alert.Accept();
        }

        //To validate lock image
        public string ValidateLockImage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 50);
            // bool valImage = WebDriverWaits.WaitUntilElementIsPresent(driver, imgLock);         
            try
            {
                string valImage = driver.FindElement(imgLock).Displayed.ToString();
                Console.WriteLine("Lock Image: " + valImage);
                return "Lock Image is displayed";
            }
            catch (Exception)
            {
                return "Lock Image is not displayed";
            }
        }
        //To validate record lock message
        public string ValidateRecordLockMessage()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleMessage, 50);
            string lockMessage = driver.FindElement(txtMessage).Text;
            return lockMessage;
        }
        //To validate Approver 
        public string ValidateActualApprover()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkHere);
            driver.FindElement(linkHere).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblApprovalHistory, 80);
            string valApprover = driver.FindElement(txtApprover).Text;
            return valApprover;
        }
        //To validate Status
        public string ValidateOverallStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            string valStatus = driver.FindElement(txtStatus).Text;
            return valStatus;
        }
        //To validate 
        public void ClickRejectButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(linkApproveReject).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtComments, 80);
            driver.FindElement(txtComments).SendKeys("Rejected");
            driver.FindElement(btnReject).Click();
        }

        public void ClickApproveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkApproveReject, 120);
            driver.FindElement(linkApproveReject).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtComments, 100);
            driver.FindElement(txtComments).SendKeys("Approved");
            driver.FindElement(btnApprove).Click();
            WebDriverWaits.WaitForPageToLoad(driver, 120);
        }

        public string GetOpportunityName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 80);
            string oppName = driver.FindElement(txtOpportunityName).Text;
            return oppName;
        }

        public string ValidateOpportunityName(string name)
        {
            if (name.StartsWith("DND"))
            {
                return "Opportunity Name " + name + " is updated with DND - upon approval on DND Submission ";
            }
            else
            {
                return "Opportunity Name " + name + " is not updated with DND - upon approval on DND Submission ";
            }
        }
        public string ClickRequestEng()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnRequestEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 50);
            string msgSubmission = driver.FindElement(msgSuccess).Text;
            return msgSubmission;
        }
        //To get Client Name
        public string GetClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClient, 90);
            string clientName = driver.FindElement(valClient).Text;
            return clientName;
        }
        //To get Subject 
        public string GetSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSubject, 60);
            string subName = driver.FindElement(valSubject).Text;
            return subName;
        }
        //To get Job Type
        public string GetJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valJobType, 70);
            string jobType = driver.FindElement(valJobType).Text;
            return jobType;
        }
        public string ClickNBCForm()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNBCForm, 120);
            driver.FindElement(btnNBCForm).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleNBCForm, 80);
            string title = driver.FindElement(titleNBCForm).Text;
            return title;
        }

        public string ClickNBCFormL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNBCFormL, 120);
            driver.FindElement(btnNBCFormL).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtDefaultTab, 140);
            string title = driver.FindElement(txtDefaultTab).Text;
            return title;
        }

        public void UpdateCC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(linkRequestDate).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
        }
        //To update CC details only
        public void UpdateCCOnly()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(linkRequestDate).Click();
            driver.FindElement(btnSave).Click();
        }
        //To update CC details only for FAS records
        public void UpdateCCOnlyFAS()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(linkRequestDateFAS).Click();
            driver.FindElement(btnSave).Click();
        }

        //Click FEIS button and get title of page
        public string ClickFEISForm()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnFEIS, 120);
            driver.FindElement(btnFEIS).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleNBCForm, 60);
            string title = driver.FindElement(titleNBCForm).Text;
            return title;
        }

        //Enter HL Internal team details
        public void EnterInternalTeamDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 60);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            WebDriverWaits.WaitUntilEleVisible(driver, chkInitiator, 50);
            //Check required checkboxes
            driver.FindElement(chkInitiator).Click();
            driver.FindElement(chkPrincipal).Click();
            driver.FindElement(chkManager).Click();
            driver.FindElement(btnSaveITTeam).Click();

            //Click to return back to Opportunity details
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 60);
            driver.FindElement(btnReturnToOpp).Click();
            Thread.Sleep(2000);
        }

        //To click on Convert to Engagement button
        public void ClickConvertToEng()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnConverttoEng, 80);
            driver.FindElement(btnConverttoEng).Click();
            //WebDriverWaits.WaitUntilAlertVisible(driver, 120);
            Thread.Sleep(30000);
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilAlertVisible(driver, 120);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            //driver.Navigate().Refresh();
            /*Thread.Sleep(1000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagement, 100);
            driver.FindElement(lnkEngagement).Click();*/
            Thread.Sleep(5000);
        }

        //To update Job type
        public string UpdateJobType(string jobType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
            driver.FindElement(comboJobType).SendKeys(jobType);
            driver.FindElement(btnSave).Click();
            string type = driver.FindElement(valJobType).Text;
            return type;
        }
        //To check the checkbox for Converted To Engagement in Opportunity details
        public void CheckConvertedToEng()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, chkConvertedtoEng, 60);
            driver.FindElement(chkConvertedtoEng).Click();
            driver.FindElement(btnSave).Click();
        }
        public string ValidateCounterparties()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCounterparties, 100);
            string exist = driver.FindElement(btnCounterparties).Displayed.ToString();
            if (exist == "True")
            {
                driver.FindElement(btnCounterparties).Click();
                return "Counterparties button is displayed";
            }
            else
            {
                return "Counterparties button is not displayed";
            }
        }

        //To update HL Internal team details
        public void UpdateInternalTeamDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 190);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkUpPrincipal, 130);
                //Check required checkboxes         
                driver.FindElement(chkUpPrincipal).Click();
                driver.FindElement(chkUpSeller).Click();
                driver.FindElement(chkUpManager).Click();
                //driver.FindElement(chkUpMgr).Click();
                driver.FindElement(chkUpAssociate).Click();
                driver.FindElement(chkUpAnalyst).Click();
                driver.FindElement(btnSaveITTeam).Click();

                //Click to return back to Opportunity details
                WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 90);
                driver.FindElement(btnReturnToOpp).Click();
                Thread.Sleep(3000);
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(chkInternalTeamPrompt).Click();
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, chkUpPrincipal, 130);

                //Check required checkboxes         
                driver.FindElement(chkUpPrincipal).Click();
                driver.FindElement(chkUpSeller).Click();
                driver.FindElement(chkUpManager).Click();
                //driver.FindElement(chkUpMgr).Click();
                driver.FindElement(chkUpAssociate).Click();
                driver.FindElement(chkUpAnalyst).Click();

                ////Enter logged in user and assign admin role
                //driver.FindElement(txtStaff).SendKeys(valUser);
                //Thread.Sleep(3000);
                //CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valUser);
                //WebDriverWaits.WaitUntilEleVisible(driver, chkAdmin, 50);
                //driver.FindElement(chkAdmin).Click();
                driver.FindElement(btnSaveITTeam).Click();

                //Click to return back to Opportunity details
                WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 60);
                driver.FindElement(btnReturnToOpp).Click();
            }
        }

        //To update Internal Team Prompt checkbox only
        public void UpdateITPrompt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
        }


        //Validate Portfolio Valuation button and click on it
        public string ClickPortfolioValuation()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnPortfolioValuation, 120);
            driver.FindElement(btnPortfolioValuation).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblValuationPeriods, 60);
            string title = driver.FindElement(lblValuationPeriods).Text;
            return title;
        }

       
        //To get Stage
        public string GetStage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStage, 60);
            Thread.Sleep(2000);
            string stage = driver.FindElement(valStage).Text;
            return stage;
        }
        //To get Pitch Date
        public string GetPitchDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPitchDate, 60);
            string pitchDate = driver.FindElement(valPitchDate).Text;
            return pitchDate;
        }
        //To get Win Probability
        public string GetWinProbability()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valWinProb, 100);
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            string winProb = driver.FindElement(valWinProb).Text;
            return winProb;
        }
        //To get Transaction Size
        public string GetTransactionSize()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTxnSize, 60);
            Thread.Sleep(3000);
            string txnSize = driver.FindElement(valTxnSize).Text;
            return txnSize;
        }
        //To get Retainer
        public string GetRetainer()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRetainer, 90);
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            string retainer = driver.FindElement(valRetainer).Text;
            return retainer;
        }
        //To get Monthly Fee
        public string GetMonthlyFee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valMonthlyFee, 90);
            Thread.Sleep(3000);
            string monthlyFee = driver.FindElement(valMonthlyFee).Text;
            return monthlyFee;
        }
        //To get Contingent Fee
        public string GetContingentFee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContingentFee, 100);
            string contingentFee = driver.FindElement(valContingentFee).Text;
            return contingentFee;
        }
        //To get Opportunity Comments
        public string GetOppComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowOppComments, 90);
            int rowCount = driver.FindElements(rowOppComments).Count;
            Console.WriteLine("rowCount: " + rowCount);
            if (rowCount > 1)
            {
                string oppComments = driver.FindElement(valOppComments).Text;
                return oppComments;
            }
            else
                return "Opportunity comments are not added ";
        }
        //To delete added Opportunity comments
        public string DeleteOppCommentsAndValidateComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkDel, 60);
            driver.FindElement(linkDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgOppComments, 120);
            string msg = driver.FindElement(msgOppComments).Text;
            Console.WriteLine("rowCount: " + msg);
            if (msg.Equals("No records to display"))
            {
                return "No Opportunity comments exist";
            }
            else
                return "Opportunity comments are displayed";
        }
        //To delete any existing Opportunity comments
        public string DeleteExistingComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowOppComments, 90);
            int rowCount = driver.FindElements(rowOppComments).Count;
            Console.WriteLine("rowCount: " + rowCount);
            if (rowCount > 1)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, linkDel, 60);
                driver.FindElement(linkDel).Click();
                Thread.Sleep(2000);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                return "Opportunity's existing comments are deleted ";
            }
            else
                return "No Opportunity comments exist ";
        }


        //To Validate Clone button
        public string ValidateCloneButton()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnClone).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "Clone button is displayed";
                }
                else
                {
                    return "Clone button is not displayed";
                }
            }
            catch (Exception)
            {
                return "Clone button is not displayed";
            }
        }

        //Check if user exists in deal
        public string ValidateUserIfExists()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            try
            {
                Thread.Sleep(2000);
                driver.Navigate().Refresh();
                driver.SwitchTo().Frame(2);
                Thread.Sleep(5000);
                string value1 = driver.FindElement(rowUser).Text;
                driver.SwitchTo().DefaultContent();
                Console.WriteLine(value1);
                if (value1.Equals("Emre Abale"))
                {
                    return "User exists";
                }
                else
                {
                    return "User does not exist";
                }
            }
            catch (Exception)
            {
                return "User does not exist";
            }
        }

        //To remove added user 
        public string RemoveUserFromITTeam()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, chkUpPrincipal, 70);

            driver.FindElement(chkUpPrincipal).Click();
            driver.FindElement(chkUpSeller).Click();
            driver.FindElement(chkUpManager).Click();
            driver.FindElement(chkUpMgr).Click();
            driver.FindElement(chkUpAssociate).Click();
            driver.FindElement(chkUpAnalyst).Click();
            driver.FindElement(chkCheckedInitiator).Click();

            driver.FindElement(txtStaff).SendKeys("Sonika Goyal");
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, "Sonika Goyal");
            WebDriverWaits.WaitUntilEleVisible(driver, chkAdmin, 50);
            driver.FindElement(chk2ndInitiator).Click();

            driver.FindElement(btnSaveITTeam).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgHLIntTeam, 90);
            string message = driver.FindElement(msgHLIntTeam).Text.Replace("\r\n", " ");

            //Click to return back to Opportunity details
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 60);
            driver.FindElement(btnReturnToOpp).Click();
            return message;
        }

        //To update Record Type
        public string UpdateRecordTypeAndLOB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecordTypeChange, 90);
            driver.FindElement(lnkRecordTypeChange).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboRecType, 90);
            driver.FindElement(comboRecType).SendKeys("CF");
            driver.FindElement(btnContinue).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB, 90);
            driver.FindElement(comboLOB).SendKeys("CF");
            driver.FindElement(comboJobType).SendKeys("Negotiated Fairness");
            driver.FindElement(btnSave).Click();
            string LOB = driver.FindElement(valLOB).Text;
            return LOB;
        }

        //To update additional fields when FAS changed to CF
        public void AddEstFeesWithAdmin(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMonthlyFee, 90);
            driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
            driver.FindElement(lnkTrialExp).Click();
            driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
            driver.FindElement(lnkEstClosedDate).Click();
            driver.FindElement(comboFairnessOpinion).SendKeys("No");
            driver.FindElement(comboWomenLed).SendKeys("No");        
            driver.FindElement(btnSave).Click();
        }

        //To Click Request Engagement Button         
        public string ClickRequestEngWithoutDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnRequestEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRegEng, 50);
            string validations = driver.FindElement(valRegEng).Text.Replace("\r\n", ", ").ToString();
            return validations;
        }

        //To update fields to validate CF validations
        public void UpdateFieldsForConversion(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 70);
            driver.FindElement(btnEdit).Click();
            if (driver.FindElement(comboRecordType).Text.Contains("CF"))
            {
                driver.FindElement(lnkDateEngagedCF).Click();
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys("Yes");
            }
            else if(driver.FindElement(comboRecordType).Text.Contains("FVA"))
            {
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(txtFee).Clear();
                driver.FindElement(txtFee).SendKeys("10000");
                Thread.Sleep(2000);
                driver.FindElement(comboWomenLed).SendKeys("Yes");
                driver.FindElement(lnkDateEngaged).Click();
            }
            else
            {               
                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();                
            }
            driver.FindElement(txtSICCode).SendKeys("9999");
            driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
            driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
            driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));
            driver.FindElement(btnSave).Click();
        }

        //To update Client and Subject to company without address details
        public void UpdateClientandSubject(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(txtClient).Clear();
            driver.FindElement(txtClient).SendKeys(name);
            driver.FindElement(txtSubject).Clear();
            driver.FindElement(txtSubject).SendKeys(name);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(2000);
        }

        public void UpdateTombstonePermissionField()
        {
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);

            driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
            driver.FindElement(btnSave).Click();
        }

        //To update Outcome details
        public void UpdateOutcomeDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
            driver.FindElement(btnEdit).Click();
            try
            {
                if (driver.FindElement(comboRecordType).Text.Contains("CF"))
                {
                    driver.FindElement(lnkOutcomeDate).Click();
                }
                else if (driver.FindElement(comboRecordType).Text.Contains("FR"))
                {
                    driver.FindElement(lnkOutcomeDateFR).Click();
                }
                else
                {
                    driver.FindElement(lnkOutcomeDateFAS).Click();
                }
                driver.FindElement(comboOutcome).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 24));
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(1000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 110);
                driver.FindElement(btnEdit).Click();
                if (driver.FindElement(comboRecordType).Text.Contains("CF"))
                {
                    driver.FindElement(lnkOutcomeDate).Click();
                }
                else if (driver.FindElement(comboRecordType).Text.Contains("FR"))
                {
                    driver.FindElement(lnkOutcomeDateFR).Click();
                }
                else
                {
                    driver.FindElement(lnkOutcomeDateFAS).Click();
                }
                driver.FindElement(comboOutcome).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 24));
                driver.FindElement(btnSave).Click();
            }
        }


        //To update NBC Approval
        public void UpdateNBCApproval()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            try
            {
                Thread.Sleep(3000);                
                WebDriverWaits.WaitUntilEleVisible(driver, checkNBCApproved, 150);
                driver.FindElement(checkNBCApproved).Click();                
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
                driver.FindElement(btnEdit).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, checkNBCApproved, 100);
                driver.FindElement(checkNBCApproved).Click();
                Thread.Sleep(2000);
                driver.FindElement(btnSave).Click();
            }
            Thread.Sleep(3000);
        }

        //To Click Request Engagement Button         
        public string GetEstFinValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnRequestEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valEstFin, 100);
            string validations = driver.FindElement(valEstFin).Text.Replace("\r\n", ", ").ToString();
            return validations;
        }

        //To Click Request Engagement Button         
        public string GetEstFinValidationForCF()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnRequestEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valEstFinCF, 100);
            string validations = driver.FindElement(valEstFinCF).Text.Replace("\r\n", ", ").ToString();
            return validations;
        }
        //To update Record Type
        public void UpdateRecordType(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecordTypeChange, 90);
            driver.FindElement(lnkRecordTypeChange).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboRecType, 90);
            driver.FindElement(comboRecType).SendKeys(type);
            driver.FindElement(btnContinue).Click();
        }

        //Click Save
        public void ClickSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }
        //Get Record Type
        public string GetRecType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecType);
            string recType = driver.FindElement(valRecType).Text;
            return recType;
        }

        //Add Opportunity Comments
        public void AddOppComments(string Type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewComment, 90);
            driver.FindElement(btnNewComment).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboCommentType);
            driver.FindElement(comboCommentType).SendKeys(Type);
            driver.FindElement(txtCommentDesc).SendKeys("Testing");
            driver.FindElement(btnSave).Click();
        }

        //Get validatin message while adding comment
        public string GetCommentsSectionMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgError, 70);
            string message = driver.FindElement(msgError).Text;
            return message;
        }

        //Get added comment in Comment section
        public string GetAddedComment()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valComment, 80);
            string comment = driver.FindElement(valComment).Text;
            return comment;
        }

        //Delete added comments
        public string DeleteAddedOppComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelComment, 80);
            driver.FindElement(lnkDelComment).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            return "Opportunity's existing comments are deleted";
        }

        //To updated required opportunity fields of CF opp for conversion to engagement
        public void UpdateReqFieldsForCFConversion(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(2000);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 170);
            //driver.FindElement(btnEdit).Click();
            //Thread.Sleep(2000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 170);
                driver.FindElement(btnEdit).Click();
                Thread.Sleep(2000);
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                Console.WriteLine("txtMarketCap added ");
                driver.FindElement(lnkTrialExp).Click();
                Console.WriteLine("lnkTrialExp added ");
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity",30));
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 80);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(lnkTrialExp).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(btnSave).Click();

            }
        }

        //To updated required opportunity fields of CF opp for conversion to engagement
        public void UpdateReqFieldsForCFConversionMultipleRows(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(2000);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 170);
            //driver.FindElement(btnEdit).Click();
            //Thread.Sleep(2000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 170);
                driver.FindElement(btnEdit).Click();
                Thread.Sleep(2000);
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23));

                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 27));
                Console.WriteLine("txtMarketCap added ");
                driver.FindElement(lnkTrialExp).Click();
                Console.WriteLine("lnkTrialExp added ");
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 80);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23));

                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 27));
                driver.FindElement(lnkTrialExp).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(btnSave).Click();

            }
        }

        //To updated required opportunity fields of FVA opp for conversion to engagement
        public void UpdateReqFieldsForFVAConversion(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            try
            {
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));
                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkValuationDate).Click();
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                //driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 60);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkValuationDate).Click();
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                //driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                Console.WriteLine("lnkDateCAExpiresFAS added ");
                driver.FindElement(btnSave).Click();
            }
        }

        //To updated required opportunity fields of FVA opp for conversion to engagement
        public void UpdateReqFieldsForFVAConversionMultipleRows(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            try
            {
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23));
                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkValuationDate).Click();
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 27));
                //driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 60);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23));

                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkValuationDate).Click();
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 27));
                //driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                Console.WriteLine("lnkDateCAExpiresFAS added ");
                driver.FindElement(btnSave).Click();
            }
        }

        //To updated required opportunity fields of FVA opp for conversion to engagement
        public void UpdateReqFieldsForFVAConversionMultipleRows1(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            try
            {
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23));
                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkValuationDate).Click();
                //driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 27));
                //driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                
            }
        }

        //To update required Opportunity fields of FR opp for conversion to engagement
        public void UpdateReqFieldsForFRConversion(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            try
            {
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));


                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();


                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 60);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();
                driver.FindElement(btnSave).Click();
            }
        }

        //To update required Opportunity fields of FR opp for conversion to engagement
        public void UpdateReqFieldsForFRConversionMultipleRows(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            try
            {
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23));


                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();


                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 60);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23));

                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();
                driver.FindElement(btnSave).Click();
            }
        }

        //Get HL Entity in ERP section
        public string GetHLEntity()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valHLEntity, 100);
            string entity = driver.FindElement(valHLEntity).Text;
            return entity;
        }

        //Get ERP HL Entity in ERP section
        public string GetERPHLEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPHLEntity, 80);
            string ERPEntity = driver.FindElement(valERPHLEntity).Text;
            return ERPEntity;
        }

        //Get ERP Legal Entity in ERP section
        public string GetERPLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityId, 80);
            string ERPEntity = driver.FindElement(valERPLegalEntityId).Text;
            return ERPEntity;
        }

        //Get ERP Submitted To Sync in ERP section
        public string GetERPSubmittedToSync()
        {
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valERPSubmittedToSync, 120);
            string syncDate = driver.FindElement(valERPSubmittedToSync).Text;
            return syncDate;
        }

        //Get ERP ID in ERP section
        public string GetERPID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPID, 80);
            string id = driver.FindElement(valERPID).Text;
            return id;
        }

        //Get ERP Project Status Code in ERP section
        public string GetERPProjStatusCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjStatusCode, 80);
            string code = driver.FindElement(valERPProjStatusCode).Text;
            return code;
        }

        //Get ERP Project Number in ERP section
        public string GetERPProjectNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectNumber, 80);
            string Number = driver.FindElement(valERPProjectNumber).Text;
            return Number;
        }

        //Get ERP Project Name in ERP section
        public string GetERPProjectName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectName, 80);
            string Number = driver.FindElement(valERPProjectName).Text;
            return Number;
        }

        //Get ERP LOB in ERP section
        public string GetERPLOB()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLOB, 80);
            string Number = driver.FindElement(valERPLOB).Text;
            return Number;
        }

        //Get ERP Template
        public string GetERPTemplate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPTemplate, 80);
            string number = driver.FindElement(valERPTemplate).Text;
            return number;
        }

        //Get ERP Business Unit ID
        public string GetERPBusinessUnitID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPUnitID, 80);
            string number = driver.FindElement(valERPUnitID).Text;
            return number;
        }

        //Get ERP Business Unit
        public string GetERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPUnit, 80);
            string unit = driver.FindElement(valERPUnit).Text;
            return unit;
        }

        //Get ERP Legal Entity ID
        public string GetERPLegalEntityID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityID, 80);
            string id = driver.FindElement(valERPLegalEntityID).Text;
            return id;
        }

        //Get ERP Entity Code
        public string GetERPEntityCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPEntityCode, 80);
            string code = driver.FindElement(valERPEntityCode).Text;
            return code;
        }

        //Get ERP Legislation Code
        public string GetERPLegCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegCode, 80);
            string code = driver.FindElement(valERPLegCode).Text;
            return code;
        }

        //Get ERP Industry Group
        public string GetERPIndustryGroup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPIG, 80);
            string IG = driver.FindElement(valERPIG).Text;
            return IG;
        }

        //Get ERP Last Integration Status
        public string GetERPIntegrationStatus()
        {
            Thread.Sleep(8000);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntStatus, 80);
            string status = driver.FindElement(valERPLastIntStatus).Text;
            return status;
        }

        //Get ERP Last Integration Response Date
        public string GetERPIntegrationResponseDate()
        {
            Thread.Sleep(8000);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valERPResponseDate,120);           
            string date = driver.FindElement(valERPResponseDate).Text;
            return date;
        }

        //Get ERP Last Integration Error Description
        public string GetERPIntegrationError()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPError, 80);
            string error = driver.FindElement(valERPError).Text;
            return error;
        }

        //Get ERP Product Type
        public string GetERPProductType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProductType, 80);
            string type = driver.FindElement(valERPProductType).Text;
            return type;
        }

        //Get ERP Product Type Code
        public string GetERPProductTypeCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProductTypeCode, 80);
            string code = driver.FindElement(valERPProductTypeCode).Text;
            return code;
        }

        //Get ERP Principal Manager ID
        public string GetERPEmailID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPEmailID, 80);
            string id = driver.FindElement(valERPEmailID).Text;
            return id;
        }

        //Get ERP Update DFF
        public string GetERPUpdateDFFIfChecked()
        {
            //Thread.Sleep(2000);
            //driver.Navigate().Refresh();            
            WebDriverWaits.WaitUntilEleVisible(driver, checkERPUpdateDFF, 100);
            string value = driver.FindElement(checkERPUpdateDFF).GetAttribute("title");
            if (value.Equals("Not Checked"))
            {
                return "Checkbox is not checked";
            }
            else
            {
                return "Checkbox is checked";
            }
        }

        //Update Primary Office
        public string UpdatePrimaryOffice(string value)
        {
            Thread.Sleep(50000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboPrimaryOffice).SendKeys(value);
            driver.FindElement(btnSave).Click();
            string office = driver.FindElement(valPrimaryOffice).Text;
            return office;
        }

        //Update Industry Group
        public string UpdateIndustryGroup(string group)
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboIG).SendKeys(group);
            driver.FindElement(comboSector).SendKeys("Dental");
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valIG, 100);
            string IG = driver.FindElement(valIG).Text;
            return IG;
        }

        //Update Sector
        public string UpdateSector(string sector)
        {
            Thread.Sleep(70000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboSector).SendKeys(sector);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valSector, 100);
            string Sector = driver.FindElement(valSector).Text;
            return Sector;
        }

        //Update Client Ownership
        public string UpdateClientOwnership(string client)
        {
            Thread.Sleep(70000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboClientOwnership).SendKeys(client);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnership, 120);
            string Client = driver.FindElement(valClientOwnership).Text;
            return Client;
        }

        //Update Subject Ownership
        public string UpdateSubjectOwnership(string subject)
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboSubjectOwnership).SendKeys(subject);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valSubjectOwnership, 100);
            string Subject = driver.FindElement(valSubjectOwnership).Text;
            return Subject;
        }

        //To update Job type for ERP
        public string UpdateJobTypeERP(string jobType)
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
            driver.FindElement(comboJobType).SendKeys(jobType);
            driver.FindElement(btnSave).Click();
            string type = driver.FindElement(valJobType).Text;
            return type;
        }

        //To update ERP Record Type
        public string UpdateRecordTypeAndLOBERP()
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecordTypeChange, 90);
            driver.FindElement(lnkRecordTypeChange).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboRecType, 90);
            driver.FindElement(comboRecType).SendKeys("FVA");
            driver.FindElement(btnContinue).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB, 90);
            driver.FindElement(comboLOB).SendKeys("FVA");
            driver.FindElement(comboJobType).SendKeys("Consulting");
            driver.FindElement(txtFee).SendKeys("10.00");
            driver.FindElement(btnSave).Click();
            string LOB = driver.FindElement(valLOB).Text;
            return LOB;
        }

        //To schedule ERP Submitted to Sync manually
        public void ScheduleERPSyncManually()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkSyncDate, 90);
            driver.FindElement(lnkSyncDate).Click();
            driver.FindElement(btnSave).Click();
        }

        //Click New Contract button
        public string ClickNewContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContract, 90);
            driver.FindElement(btnNewContract).Click();
            string title = driver.FindElement(titlePage).Text;
            return title;
        }

        //Add Contract by not selecting Is Main Contract option
        public string AddContractByNotSelectingIsMainContract(string name, string contact)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContractName, 90);
            driver.FindElement(txtContractName).SendKeys(name);
            driver.FindElement(txtBillingContact).SendKeys(contact);
            driver.FindElement(btnSave).Click();
            string title = driver.FindElement(titlePage).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunity, 80);
            driver.FindElement(lnkOpportunity).Click();
            return title;
        }

        //Validate that Is Main Contract checkbox is checked or not
        public string ValidateIsMainContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkIsMain, 90);
            string main = driver.FindElement(checkIsMain).GetAttribute("title");
            if (main.Equals("Checked"))
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }

        //Click on Edit link of Contract 
        public string ClickEditContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditContract, 90);
            driver.FindElement(lnkEditContract).Click();
            string title = driver.FindElement(titleEditContract).Text;
            return title;
        }

        //Edit Contract by deselecting Is Main Contract option
        public string EditContractByDeselectingIsMainContract()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkSelectedIsMain, 80);
                driver.FindElement(checkSelectedIsMain).Click();
                driver.FindElement(btnSave).Click();
                string title = driver.FindElement(titleEditContract).Text;
                return title;
            }
            catch(Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, checkSelectedIsMain, 80);
                driver.FindElement(checkSelectedIsMain).Click();
                driver.FindElement(btnSave).Click();
                string title = driver.FindElement(titleEditContract).Text;
                return title;
            }
        }

        //Add Contract by selecting Is Main Contract option
        public string AddContractBySelectingIsMainContract(string name, string contact)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContractName, 90);
            driver.FindElement(txtContractName).SendKeys(name);
            driver.FindElement(txtBillingContact).SendKeys(contact);
            driver.FindElement(checkSelectedIsMain).Click();
            driver.FindElement(btnSave).Click();
            string title = driver.FindElement(titlePage).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunity, 80);
            driver.FindElement(lnkOpportunity).Click();
            return title;
        }

        //Validate that Is Main Contract checkbox is checked for earlier contract when multiple contracts are there
        public string ValidateIsMainContractOfNewContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkIsMainContract1, 90);
            string main = driver.FindElement(checkIsMainContract1).GetAttribute("title");
            if (main.Equals("Checked"))
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }

        //Validate that Is Main Contract checkbox is checked or not when multiple contracts are there
        public string ValidateIsMainContractOfOldContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkIsMainContract2, 90);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            string main = driver.FindElement(checkIsMainContract2).GetAttribute("title");
            if (main.Equals("Checked"))
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }

        public string Get1stContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContract1, 100);
            Thread.Sleep(2000);
            string name = driver.FindElement(valContract1).Text;
            return name;
        }

        //Get 2nd contract name
        public string Get2ndContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContract2, 100);
            string name = driver.FindElement(valContract2).Text;
            return name;
        }

        //Add Contract by selecting a Company
        public string AddContractBySelectingACompany(string name, string contact, string client)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContractName, 90);
            driver.FindElement(txtContractName).SendKeys(name);
            driver.FindElement(txtClientContract).SendKeys(client);
            driver.FindElement(txtBillingContact).SendKeys(contact);
            driver.FindElement(btnSave).Click();
            string title = driver.FindElement(titlePage).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunity, 80);
            driver.FindElement(lnkOpportunity).Click();
            return title;
        }

        //Modify Internal Team Members
        public string ModifyInternalTeamMembers(string name)
        {
            driver.SwitchTo().Frame(2);
            WebDriverWaits.WaitUntilEleVisible(driver, btnModifyRoles, 80);
            driver.FindElement(btnModifyRoles).Click(); 
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 80);
            driver.FindElement(txtStaff).SendKeys(name);            
            Thread.Sleep(4000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, name);
            Thread.Sleep(3000);

            try
            {
                if (driver.FindElement(checkInitiator).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }            
        }

        //Validate Seller role
        public string VerifySellerRole()
        {
            try
            {
                if (driver.FindElement(checkSeller).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Principal role
        public string VerifyPrincipalRole()
        {
            try
            {
                if (driver.FindElement(checkPrincipal).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Manager role
        public string VerifyManagerRole()
        {
            try
            {
                if (driver.FindElement(checkManager).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Associate role
        public string VerifyAssociateRole()
        {
            try
            {
                if (driver.FindElement(checkAssociate).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Analyst role
        public string VerifyAnalystRole()
        {
            try
            {
                if (driver.FindElement(checkAnalyst).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Specialty role
        public string VerifySpecialtyRole()
        {
            try
            {
                if (driver.FindElement(checkSpeciality).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate PE/HF role
        public string VerifyPERole()
        {
            try
            {
                if (driver.FindElement(checkPE).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Public role
        public string VerifyPublicRole()
        {
            try
            {
                if (driver.FindElement(checkPublic).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }


        //Validate Admin role
        public string VerifyAdminRole()
        {
            try
            {
                if (driver.FindElement(checkAdmin).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate RMS role
        public string VerifyRMSRole()
        {
            try
            {
                if (driver.FindElement(checkRMS).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Expense Only role
        public string VerifyExpenseOnlyRole()
        {
            try
            {
                if (driver.FindElement(checkExpenseOnly).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }


        //Validate Non Registered role
        public string VerifyNonRegisteredRole()
        {    try
            {
                if (driver.FindElement(checkNonRegistered).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch(Exception)
            {
                return "False";
            }
          
        }

        //To update Total Anticipated Revenue field
        public void UpdateTotalAnticipatedRevenue(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAnticipatedRevenue, 80);
            driver.FindElement(txtAnticipatedRevenue).Clear();
            driver.FindElement(txtAnticipatedRevenue).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
            driver.FindElement(btnSave).Click();
        }

        //To update Total Anticipated Revenue field
        public void UpdateTotalAnticipatedRevenueForValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAnticipatedRevenue, 80);
            driver.FindElement(txtAnticipatedRevenue).SendKeys("10,000");
            driver.FindElement(btnSave).Click();
        }

        //To Validate NBC button
        public string ValidateNBCButton()
        {
            Thread.Sleep(2000);
            try
            {
                string value = driver.FindElement(btnNBCForm).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "NBC Form button is displayed";
                }
                else
                {
                    return "NBC Form button is not displayed";
                }
            }
            catch (Exception)
            {
                return "Form button is not displayed";
            }
        }

        //Get default Company name of Additional Client
        public string GetCompanyNameOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultClient, 120);
            string value = driver.FindElement(valDefaultClient).Text;
            return value;
        }

        //Validate if additional row got added after clicking cancel button
        public string ValidateIfNewRowGotAdded()
        {
            try
            {
                string value = driver.FindElement(rowAdditional).Displayed.ToString();
                Console.WriteLine(value);
                return value;
            }
            catch(Exception)
            {
                return "Row does not exist";
            }           
        }

        // To validate save functionality of Additional client
        public string ValidateSaveFunctionalityOfAdditionalClient(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubject, 80);
            driver.FindElement(txtClientSubject).SendKeys(name);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valNewClient, 100);
            string value = driver.FindElement(valNewClient).Text;
            return value;
        }

        //Get type of added additional client record
        public string GetTypeOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientType, 80);
            string value = driver.FindElement(valClientType).Text;
            return value;
        }

        //Validate Edit functionality of Additional Client
        public void ValidateEditFunctionalityOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditClient, 80);
            driver.FindElement(lnkEditClient).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkPrimary, 80);
            driver.FindElement(checkPrimary).Click();
            driver.FindElement(btnSave).Click();
        }

        //Get checkbox value of added additional client record
        public string GetPrimaryCheckboxOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimary, 80);
            string value = driver.FindElement(valPrimary).GetAttribute("title");
            return value;
        }

        //Validate Delete functionality of Additional Client
        public string ValidateDeleteFunctionalityOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelClient, 80);
            driver.FindElement(lnkDelClient).Click();
            driver.SwitchTo().Alert().Accept();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultClient, 100);
            string name = driver.FindElement(valDefaultClient).Text;
            return name;
        }

        //Click Additional Client button 
        public string ClickAdditionalClientButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdditionalClient, 120);
            driver.FindElement(btnAdditionalClient).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleAdditionalClient, 90);
            string title = driver.FindElement(titleAdditionalClient).Text;
            return title;
        }

        //Validate Type drop down values
        public bool VerifyTypeValues()
        {
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboTypes);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "--None--", "Client", "Subject" };
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

        //Get Type value from Opportunity Client Subject Edit
        public string GetTypeFromClientSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valType, 120);
            string value = driver.FindElement(valType).Text;
            return value;
        }

        // To validate cancel functionality of Additional client
        public string ValidateCancelFunctionalityOfAdditionalClient(string company)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubject, 80);
            driver.FindElement(txtClientSubject).SendKeys(company);
            driver.FindElement(btnCancel).Click();
            string page = driver.FindElement(titleOpp).Text;
            return page;
        }

        //Get Opportunity value from Opportunity Client Subject Edit
        public string GetOpportunityFromClientSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOppClientSubject, 120);
            string value = driver.FindElement(valOppClientSubject).GetAttribute("value");
            return value;
        }

        //Get checkbox value of added Client Company
        public string GetPrimaryCheckboxOfClientCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimaryClient, 80);
            string value = driver.FindElement(valPrimaryClient).GetAttribute("title");
            return value;
        }

        //Get checkbox value of added Subject Company
        public string GetPrimaryCheckboxOfSubjectCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimarySubject, 80);
            string value = driver.FindElement(valPrimarySubject).GetAttribute("title");
            return value;
        }

        //Validate the company name of Fee Attribution Party 
        public string GetCompanyNameOfFeeAttributionParty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompFeeAttrParty, 80);
            string value = driver.FindElement(valCompFeeAttrParty).Text;
            return value;
        }

        //Validate the company name of Key Creditors 
        public string GetCompanyNameOfKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valCompKeyCreditor, 40);
                string value = driver.FindElement(valCompKeyCreditor).Text;
                return value;
            }
            catch(Exception e)
            {
                return "No new client exists";
            }
        }

        //Validate the type of Fee Attribution Party 
        public string GetTypeOfFeeAttributionParty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTypeFeeAttrParty, 80);
            string value = driver.FindElement(valTypeFeeAttrParty).Text;
            return value;
        }

        //Validate the type of Key Creditors 
        public string GetTypeOfKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valTypeKeyCreditor, 40);
                string value = driver.FindElement(valTypeKeyCreditor).Text;
                return value;
            }
            catch(Exception e)
            {
                return "Key Creditor";
            }
        }

        //Validate the Record Type of Fee Attribution Party 
        public string GetRecTypeOfFeeAttributionParty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecTypeFeeAttrParty, 80);
            string value = driver.FindElement(valRecTypeFeeAttrParty).Text;
            return value;
        }

        //Validate the type of Key Creditors 
        public string GetRecTypeOfKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valRecTypeKeyCreditor, 40);
                string value = driver.FindElement(valRecTypeKeyCreditor).Text;
                return value;
            }
            catch (Exception e)
            {
                return "Key Creditor";
            }
        }

        //To update Additional Client and Additional Subject 
        public void UpdateAdditionalClientandSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();          
            driver.FindElement(comboAdditionalClient).SendKeys("Yes");            
            driver.FindElement(comboAdditionalSubject).SendKeys("Yes");
            driver.FindElement(btnSave).Click();
        }

        //Get the value of Women Led
        public string GetWomenLedValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            string value = driver.FindElement(valWomenLed).Text;
            return value;
        }

        //Get the text of Women Led
        public string GetWomenLedText(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            if (name.Equals("Emre Abale"))
            {
                string value = driver.FindElement(txtWomenLed).Text;
                return value;
            }
            else if (name.Equals("Drew Koecher"))
            {
                string value = driver.FindElement(txtWomenLedFVA).Text;
                return value;
            }
            else
            {
                string value = driver.FindElement(txtWomenLedFR).Text;
                return value;
            }
        }

        //To update required Opportunity fields for conversion to engagement
        public void UpdateReqFieldsForConversion(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(1000);

            try
            {
                if ((driver.FindElement(txtEstimatedFees)) != null)
                {

                    driver.FindElement(txtEstimatedFees).SendKeys("10000");
                }

            }
            catch (Exception e)

            {

            }


            try
            {
                if ((driver.FindElement(drpdownWomenLed)) != null)
                {
                    CustomFunctions.SelectByText(driver, driver.FindElement(drpdownWomenLed), "No");

                }

            }
            catch (Exception e)

            {

            }



            driver.FindElement(txtSICCode).SendKeys("9999");
            driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
            driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
            Console.WriteLine("Referral contact added ");
            driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

            if (driver.FindElement(comboRecordType).Text.Contains("CF"))
            {
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                Console.WriteLine("txtMarketCap added ");
                driver.FindElement(lnkTrialExp).Click();
                Console.WriteLine("lnkTrialExp added ");
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                Console.WriteLine("Conti added ");
                driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
            }
            else if (driver.FindElement(comboRecordType).Text.Contains("FVA"))
            {
                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                driver.FindElement(lnkValuationDate).Click();
                Console.WriteLine("lnkValuationDate added ");
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                Console.WriteLine("txtMarketCap added ");
                driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                Console.WriteLine("lnkDateCAExpiresFAS added ");

            }

            else
            {
                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();
            }
            driver.FindElement(btnSave).Click();

            if (CustomFunctions.IsElementPresent(driver, lnkReDisplayRec))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();

                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

                if (driver.FindElement(comboRecordType).Text.Contains("CF"))
                {
                    driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                    driver.FindElement(lnkTrialExp).Click();
                    driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                    driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                    driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                    driver.FindElement(comboFairnessOpinion).SendKeys("No");
                    driver.FindElement(lnkEstClosedDate).Click();
                }
                else if (driver.FindElement(comboRecordType).Text.Contains("FVA"))
                {
                    driver.FindElement(lnkPitchDateFAS).Click();
                    driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                    driver.FindElement(lnkValuationDate).Click();
                    driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                    driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                    driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                    driver.FindElement(lnkDateCASignedFAS).Click();
                    driver.FindElement(lnkDateCAExpiresFAS).Click();
                }

                else
                {
                    driver.FindElement(lnkPitchDateFR).Click();
                    driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                    driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                    driver.FindElement(chkDebtConfirmed).Click();
                    driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                    driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                    driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                    driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                    driver.FindElement(comboEUSecurities).SendKeys("No");
                    driver.FindElement(lnkDateCASignedFR).Click();
                    driver.FindElement(lnkDateCAExpiresFR).Click();
                    //driver.FindElement(lnkOutcomeDateFR).Click();
                    driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                    driver.FindElement(lnkEstimatedClosedDateFR).Click();
                }
                driver.FindElement(btnSave).Click();
            }


        }
        //Get ICO Contract Name

        public string GetICOContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valICOContractName);
            string ICOContractName = driver.FindElement(valICOContractName).Text;
            return ICOContractName;
        }

        //To update Outcome details
        public void UpdateStagePriority(string file, int row)
        {
            if (row.Equals(3))
            {
                ReadJSONData.Generate("Admin_Data.json");
                string dir = ReadJSONData.data.filePaths.testData;
                string excelPath = dir + file;
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(txtStagePriority).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 35));
                driver.FindElement(btnSave).Click();
            }
        }
        //Get ERP Contract TYpe
        public string GetERPContractType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContractType);
            string valueERPContractType = driver.FindElement(valERPContractType).Text;
            return valueERPContractType;
        }
        //Get ERP Legal Entity Name
        public string GetERPLegalEntityName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityName);
            string valueERPLegalEntityName = driver.FindElement(valERPLegalEntityName).Text;
            return valueERPLegalEntityName;
        }
        //Get Bill To Company
        public string GetBillTo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBillTo);
            string valueBillTo = driver.FindElement(valBillTo).Text;
            return valueBillTo;
        }
        //Get ERP Legal Entity Name
        public string GetContractStartDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContractStartDate);
            string valueContractStartDate = driver.FindElement(valContractStartDate).Text;
            return valueContractStartDate;
        }


        //To get Industry group
        public string GetSector()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSector, 100);
            string sector = driver.FindElement(valSector).Text;
            return sector;
        }
        //To get additional client 
        public string GetAdditionalClientBoolValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAdditionalClient, 100);
            string addClient = driver.FindElement(valAdditionalClient).Text;
            return addClient;
        }
        //To get additional subject value 
        public string GetAdditionalSubjectBoolValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAdditionalSubject, 100);
            string addSubject = driver.FindElement(valAdditionalSubject).Text;
            return addSubject;
        }


        //To get additional subject value 
        public string GetReferalTypeValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valReferralType, 100);
            string ReferalType = driver.FindElement(valReferralType).Text;
            return ReferalType;
        }
        //To get non public info value 
        public string GetNonPublicInfoValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNonPublicInfo, 100);
            string nonPublicInfo = driver.FindElement(valNonPublicInfo).Text;
            return nonPublicInfo;
        }
        //To get Bene Owner and Control Person FormValue
        public string GetBeneOwnerAndControlPersonFormValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBeneOwnerAndControlPersonForm, 100);
            string beneOwner = driver.FindElement(valBeneOwnerAndControlPersonForm).Text;
            return beneOwner;
        }

        //To get Bene Owner and Control Person FormValue
        public string GetPrimaryOffice()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimaryOffice, 100);
            string primaryOffice = driver.FindElement(valPrimaryOffice).Text;
            return primaryOffice;
        }
        //To get staff member
        public string GetStaffMember()
        {
            Thread.Sleep(2000);
            driver.SwitchTo().Frame("066i0000004Z1At");
            WebDriverWaits.WaitUntilEleVisible(driver, valStaffMember, 100);
            string staffMember = driver.FindElement(valStaffMember).Text;
            driver.SwitchTo().DefaultContent();
            return staffMember;
        }
        //To get Legal entity
        public string GetLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntity, 100);
            string legalEntity = driver.FindElement(valLegalEntity).Text;
            return legalEntity;
        }
        public void DeleteOpportunity()
        {
            Thread.Sleep(2000);
            CustomFunctions.ActionClicks(driver, btnDeleteOpportunity);
            Thread.Sleep(3000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public void DeleteInternalTeamOfOpportunity()
        {
            Thread.Sleep(2000);
            CustomFunctions.ActionClicks(driver, btnDeleteOpportunity);
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkInternalTeam, 60);
            driver.FindElement(lnkInternalTeam).Click();

            CustomFunctions.ActionClicks(driver, btnDeleteInternalTeam);
            Thread.Sleep(3000);
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        // To get external disclosure status
        public string GetExternalDisclosureStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExternalDisclosureStat, 100);
            string extDisclosureStat = driver.FindElement(valExternalDisclosureStat).Text;
            return extDisclosureStat;
        }
        //To get Line Of Business
        public string GetLineOfBusiness()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLineOfBusiness, 100);
            string LOBvalue = driver.FindElement(valLineOfBusiness).Text;
            return LOBvalue;
        }
      

        
   
    }
}


