using OpenQA.Selenium;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SalesForce_Project.Pages.Common
{
    class LegalEntityDetail : BaseClass
    {
       
        By valERPLegalEntityId = By.CssSelector("div[id*='ef9']");
        By valERPBusinessUnitId	= By.CssSelector("div[id*='ef4']");
        By valTemplateNumber = By.CssSelector("div[id*='efB']");
        By valERPBusinessUnit = By.CssSelector("div[id*='ef5']");
        By valERPEntityCode = By.CssSelector("div[id*='ef6']");
        By valERPLegislationCode = By.CssSelector("div[id*='efA']");
        

        //Get ERP Legal Entity ID 
        public string GetERPLegalEntityID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityId, 80);
            string id = driver.FindElement(valERPLegalEntityId).Text;
            return id;
        }

        //Get ERP Business Unit ID 
        public string GetERPBusinessUnitID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitId, 80);
            string id = driver.FindElement(valERPBusinessUnitId).Text;
            return id;
        }

        //Get ERP Template number
        public string GetERPTemplateNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTemplateNumber, 80);
            string number = driver.FindElement(valTemplateNumber).Text;
            return number;
        }

        //Get ERP Business Unit
        public string GetERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnit, 80);
            string unit = driver.FindElement(valERPBusinessUnit).Text;
            return unit;
        }

        //Get ERP Entity Code
        public string GetERPEntityCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPEntityCode, 80);
            string code = driver.FindElement(valERPEntityCode).Text;
            return code;
        }

        //Get ERP Legislation Code
        public string GetERPLegislationCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegislationCode, 80);
            string code = driver.FindElement(valERPLegislationCode).Text;
            return code;
        }
     

    }
}
