using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.Contact
{
    class MergeContacts : BaseClass
    {

        By btnSave = By.CssSelector("div.pbHeader input[value='Save']");
        By btnNext = By.CssSelector("div[class='pbBottomButtons'] > input[name='goNext']");
        By step2Title = By.CssSelector("div[class*='brandTertiaryBgr'] > h2");
        By btnMerge = By.CssSelector("div[class='pbTopButtons'] > input[name='save']");

        public int GetMergeReadyContacts()
        {
            IList<IWebElement> companyFinancialList = driver.FindElements(By.CssSelector("table[class='list'] > tbody > tr"));
            return companyFinancialList.Count;
        }

        public bool VerifyMergeReadyContactSelected(int tr)
        {
            By contactList = By.CssSelector($"table[class='list'] > tbody > tr:nth-child({tr}) > th > input");

            bool isChecked = CustomFunctions.isAttributePresent(driver, driver.FindElement(contactList), "checked");
            return isChecked;
        }

        public void ClickNextButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNext);
            driver.FindElement(btnNext).Click();            
        }

        public string GetStepTitleOfMergeContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, step2Title, 60);
            string secondStepTitle = driver.FindElement(step2Title).Text;
            return secondStepTitle;
        }

        public void ClickMergeButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMerge);
            driver.FindElement(btnMerge).Click();            
        }

        public string GetAlertTextAndAccept()
        {
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Thread.Sleep(2000);
            alert.Accept();
            return alertText;
        }

    }
}
