using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.Contacts
{
    class LegalEntityDetailPage: BaseClass
    {
        By valERPBusinessUnit = By.CssSelector("div[id*='M0ef5']");
        By valERPBusinessUnitId = By.CssSelector("div[id*='M0ef4']");

        // Function to get business unit value
        public string GetERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnit, 60);
            string ERPBusinessUnit = driver.FindElement(valERPBusinessUnit).Text;
            return ERPBusinessUnit;
        }

        // Function to get legal entity id
        public string GetERPBusinessUnitId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitId, 60);
            string ERPBusinessUnitId = driver.FindElement(valERPBusinessUnitId).Text;
            return ERPBusinessUnitId;
        }
    }
}
