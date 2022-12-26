using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace SalesForce_Project.UtilityFunctions
{
    class CustomFunctions :BaseClass
    {
        //Read Json file
        public static string ReadJson(string key) 
        {
            StreamReader file = File.OpenText("");
            JsonTextReader reader = new JsonTextReader(file);
            JObject data = (JObject)JToken.ReadFrom(reader);
            string value = data[key].ToString();
            return value;
        }

        public static void TableauPopUp()
        {
            /*
             By lnkCompanies = By.CssSelector("a[title*='Companies Tab']");
             By txtCompanyName = By.CssSelector("input[id*='j_id37:nameSearch']");
             By btnCompanySearch = By.CssSelector("div[class='searchButtonPanel'] > center > input[value='Search']");
             By tblResults = By.CssSelector("table[id*='pbtCompanies']");
             By matchedResult = By.CssSelector("td[id*=':pbtCompanies:0:j_id68'] a");
             WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies, 120);
             driver.FindElement(lnkCompanies).Click();
             WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
             driver.FindElement(txtCompanyName).SendKeys("StandardTestCompany");
             WebDriverWaits.WaitUntilEleVisible(driver, btnCompanySearch, 120);

             driver.FindElement(btnCompanySearch).Click();
             WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(1000);

             try
             {
                 string result = driver.FindElement(matchedResult).Displayed.ToString();
                 Console.WriteLine("Search Results :" + result);
                 driver.FindElement(matchedResult).Click();
                 Thread.Sleep(5000);

             }       

             catch (Exception)
             {

             }


             InputSimulator sim = new InputSimulator();
             sim.Keyboard.KeyPress(VirtualKeyCode.TAB);
             sim.Keyboard.KeyPress(VirtualKeyCode.TAB);
             sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
             Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies, 120);
             driver.FindElement(lnkCompanies).Click();
            */

        }
        //Enter Text Function
        public static void EnterText(IWebDriver driver, IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        //Click Function
        public static void Click(IWebDriver driver, IWebElement element)
        {
            element.Click();
        }

        //Select by Value Function
        public static void SelectByValue(IWebDriver driver, IWebElement element,string value)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByValue(value);
        }


        //Select by Text Function
        public static void SelectByText(IWebDriver driver, IWebElement element, string value)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByText(value);
        }


        //Select by Index Function
        public static void SelectByIndex(IWebDriver driver, IWebElement element, int value)
        {
            SelectElement select = new SelectElement(element);
            select.SelectByIndex(value);
        }

        //Action Click Function
        public static void ActionClick(IWebDriver driver, IWebElement element,int timeout=20 )
        {
           new Actions(driver).MoveToElement(element).Click().Build().Perform();
        }

        //Generate random value
        public static string RandomValue()
        {
            DateTime now = DateTime.Now;
            string date = now.ToString("ddmmyyyyHHmmss");
            Console.WriteLine(date);
            return date;
        }

        //Select value from dropdown without using select
        public static void SelectValueWithoutSelect(IWebDriver driver, By by, string value)
        {
            var dropList = driver.FindElements(by);
            for (int i = 0; i < dropList.Count; i++)
            {
                string text = dropList[i].Text;
                if (text.Contains(value))
                {
                    Thread.Sleep(3000);
                    dropList[i].Click();
                    break;
                }
                else if (value.Contains(text))
                {
                    Thread.Sleep(3000);
                    dropList[i].Click();
                    break;
                }
            }
        }

        //Select value from drop down based on entered name without li tag
        public static void SelectValueWithXpath(string Name)
        {
            driver.FindElement(By.XPath("//strong[contains(text(), '" + Name + "')]"));
        }

        public static bool IsElementPresent(IWebDriver driver, By by)
        {
            return driver.FindElements(by).Count != 0;
        }
       
        public static IWebElement SelectValueFromDropdown(IWebDriver driver, string option)
        {
            return driver.FindElement(By.XPath($"//a[normalize-space()='{option}']"));
        }

        public static IWebElement ContactInformationFieldsErrorElement(IWebDriver driver, string name)
        {
            return driver.FindElement(By.XPath($"//*[text()='{name}']/../..//div[@class='errorMsg']"));
        }


        public static string IsElementEditable(IWebDriver driver, By by)
        {
            if (driver.FindElement(by).TagName.Contains("textarea") || driver.FindElement(by).TagName.Contains("input") || driver.FindElement(by).TagName.Contains("select"))
            {
                return "Element is editable";
            }
            else
            {
                return "Element is not editable";
            }
        }

        public static bool isAttributePresent(IWebDriver driver, IWebElement element, string attribute)
        {
            bool result = false;
            try
            {
                string value = element.GetAttribute(attribute);
                if (value != null)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine(attribute + " attribute Not present");
            }
            return result;
        }

        public static bool isTextPresent(IWebDriver driver, IWebElement element)
        {
            bool result = false;
            try
            {
                string value = element.Text;
                if (value != null || value != " ")
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                Console.WriteLine(element + "text not present");
            }
            return result;
        }

        public static bool AreLinksWorking(string url)
        {
            Thread.Sleep(3000);
            System.Net.HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (request != null)
            {
                request.AllowAutoRedirect = true;
                request.KeepAlive = false;
            }
            try
            {
                Thread.Sleep(3000);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("\r\nResponse Status Code is OK and StatusDescription is: {0}", response.StatusDescription);
                    response.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static string GetCurrentPSTDate()
        {
            TimeZoneInfo pacificZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, pacificZone);
            string pstTime = CreatedDate.ToString("d");
            return pstTime;
        }


        //Action Clicks Function 
        public static void ActionClicks(IWebDriver driver, By by, int timeout = 20)
        {
            IWebElement element = driver.FindElement(by);
            new Actions(driver).MoveToElement(element).Click().Build().Perform();
        }
        //Switch to  window
        public static void SwitchToWindow(IWebDriver driver, int value)
        {
            driver.SwitchTo().Window(driver.WindowHandles[value]);
        }
        public static void MouseOver(IWebDriver driver, By by, int timeout = 20)
        {
            IWebElement element = driver.FindElement(by);
            new Actions(driver).MoveToElement(element).Build().Perform();
        }
    }
}
