using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.Contact
{
    class ContactRelationshipPage : BaseClass
    {
        By valRelationshipTitle = By.CssSelector("h1[class='pageType']");
        By valContactTitle = By.CssSelector("h2[class='pageDescription']");
        By colName = By.CssSelector("a[id*='j_id8:j_id12']");
        By colContactName = By.CssSelector("tbody[id*='relationshipsForm'] >  tr > td:nth-child(1)");
        By btnReturnToContact = By.CssSelector("td[class='pbButton center'] > input");


        
        //Function to get relationship title
        public string GetRelationshipTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRelationshipTitle, 60);
            string titleRelationship = driver.FindElement(valRelationshipTitle).Text;
            return titleRelationship;
        }

        //Function to get contact title
        public string GetContactTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactTitle, 60);
            string titleRelationship = driver.FindElement(valContactTitle).Text;
            return titleRelationship;
        }

        public IWebElement GetColName()
        {
            IWebElement name = driver.FindElement(colName);
            return name;
        }

        //Function to validate sorting
        public string ValidateSorting(IWebElement element, string order)
        {
            element.Click();
            Thread.Sleep(2000);
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
                Console.WriteLine("Desc:" + copyValues[1].ToString());
                result = copyValues.SequenceEqual(actualValues);
                resultList = result.ToString();
            }
            else if (order == "Ascending")
            {
                //Sorting of array in ascending order 
                copyValues = copyValues.OrderBy(d => d).ToArray();
                Console.WriteLine("asc:" + copyValues[0].ToString());
                result = copyValues.SequenceEqual(actualValues);
                resultList = result.ToString();
            }
            return resultList;
        }

        //To Click on Return to Contact button
        public void ClickReturnToContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToContact);
            driver.FindElement(btnReturnToContact).Click();
        }
    }
}
