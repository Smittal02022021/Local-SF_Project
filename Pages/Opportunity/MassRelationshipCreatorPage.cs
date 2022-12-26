using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SalesForce_Project.Pages.Opportunity
{
    class MassRelationshipCreatorPage : BaseClass
    {
        By btnManageRelationships = By.CssSelector("td.pbButton>input[value='Manage Relationships']");
        By txtHLContact = By.Name("j_id0:j_id1:j_id2:j_id3:inputTxtId");
        By listHLContact = By.CssSelector("body > ul");
        By rowHeader = By.CssSelector("#tableId > thead > tr:nth-child(2)");
        By radioAllContacts = By.CssSelector("input[checked='checked']");
        By valContactNames = By.CssSelector("#tBodyContact > tr");
        By comboRating = By.CssSelector("td:nth-child(4) > select");
        By radioExternalTeam = By.CssSelector("input[value='External_Team']");
        By radioClientTeam = By.CssSelector("input[value = 'Client_Team']");
        By radioCPContacts = By.CssSelector("input[value = 'Counterparty_Contacts']");
        By colContactName = By.CssSelector("#tBodyContact > tr > td:nth-child(1)");
        By colRating = By.CssSelector("tr[class*='relationshipRow'] > td:nth-child(4)>select");
        By colName = By.XPath("//th[text() = 'Contact Name']");

        public void ClickManageRelationships()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnManageRelationships, 60);
            driver.FindElement(btnManageRelationships).Click();
        }

        public string ValidateContactsTableHeaders(string Name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtHLContact, 60);
            driver.FindElement(txtHLContact).SendKeys(Name);
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, listHLContact, Name);
            WebDriverWaits.WaitUntilEleVisible(driver, rowHeader, 60);
            string headerNames = driver.FindElement(rowHeader).Text;
            return headerNames;
        }
        public bool ValidateRadiobutton()
        {
            bool value = driver.FindElement(radioAllContacts).Selected;
            return value;
        }
        public string ValidateAllContacts()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactNames, 80);
            int count = driver.FindElements(valContactNames).Count;
            Console.WriteLine("Count: " + count);
            string[] names = new string[count];
            for (int row = 1; row <= count; row++)
            {
                string Names = driver.FindElement(By.CssSelector("#tBodyContact > tr:nth-child(" + row + ")>td:nth-child(1)")).Text;
                names[row - 1] = Names;
            }
            string contactNames = string.Join(" ", names);
            return contactNames;
        }
        public void UpdateRating()
        {
            var count = driver.FindElements(valContactNames).Count;
            for (int row = 1; row <= count; row++)
            {
                driver.FindElement(By.CssSelector("tr:nth-child(" + row + ")>td:nth-child(4) > select")).SendKeys("Low");
            }
        }
        public void ClickExternalTeam()
        {

            Thread.Sleep(4000);
                        driver.FindElement(radioExternalTeam).Click();
        }
        public void ClickClientTeam()
        {
            driver.FindElement(radioClientTeam).Click();
        }
        public void ClickCPContacts()
        {
            driver.FindElement(radioCPContacts).Click();
        }

        public string ValidateContactName()
        {
            Thread.Sleep(5000);
            string contactName = driver.FindElement(colContactName).Text;
            return contactName;
        }
        public string ValidateRatings()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, colRating, 90);
            Thread.Sleep(2000);
            SelectElement selectedValue = new SelectElement(driver.FindElement(colRating));
            string contactName = selectedValue.SelectedOption.Text;
            return contactName;
        }
        public IWebElement GetColName()
        {
            IWebElement name = driver.FindElement(colName);
            return name;
        }

        public string ValidateSorting(IWebElement element, string order)
        {
            element.Click();
            IList<IWebElement> rows = driver.FindElements(colContactName);
            int count = rows.Count;
            string[] actualValues = new string[count];
            string[] copyValues = new string[count];
            string resultList = null;
            bool result;

            for (int i = 0; i < count; i++)
            {
                actualValues[i] = rows[i].Text.ToString();
            }
            //Copying values to duplicate array      
            actualValues.CopyTo(copyValues, 0);
            if (order == "Descending")
            {
                //Sorting of array in descending order   
                copyValues = copyValues.OrderByDescending(d => d).ToArray();
                Console.WriteLine("Desc:" + copyValues[2].ToString());
                result = copyValues.SequenceEqual(actualValues);
                resultList = result.ToString();
            }
            else if (order == "Ascending")
            {
                //Sorting of array in ascending order 
                copyValues = copyValues.OrderBy(d => d).ToArray();
                Console.WriteLine("asc:" + copyValues[1].ToString());
                result = copyValues.SequenceEqual(actualValues);
                resultList = result.ToString();
            }
            return resultList;
        }
    }
}


