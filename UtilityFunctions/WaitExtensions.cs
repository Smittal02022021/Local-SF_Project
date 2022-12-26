using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SalesForce_Project.UtilityFunctions
{
    public class WebDriverWaits
    {
       //To Validate Page Title
       public static bool TitleContains(IWebDriver driver, String Title, int timeout = 150)
        {
           try
                { 
                  WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(timeout));
                 return wait.Until((element) => { return element.Title == Title; });
                }
         catch(TimeoutException)
              {
                     WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                     return wait.Until((element) => { return element.Title == Title; });
              }
       }

        //To check visibility of element
        public static void WaitUntilEleVisible(IWebDriver driver, By element, int timeout=70)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
        }

        //To check visibility of an alert
        public static void WaitUntilAlertVisible(IWebDriver driver, int timeout = 120)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        //To check visibility of button
        public static void WaitUntilClickable(IWebDriver driver, By element, int timeout = 50)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }
        public static void WaitFluent(IWebDriver driver, By element, double polling_interval_in_ms, int timeout = 40)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(10);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(polling_interval_in_ms);
        }
        //To wait until page is ready
        public static void WaitForPageToLoad(IWebDriver driver, int MyDefaultTimeOut)
        {
            new WebDriverWait(driver, TimeSpan.FromMinutes(MyDefaultTimeOut)).Until(
                d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
