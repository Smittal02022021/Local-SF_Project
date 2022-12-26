using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Threading;

namespace SalesForce_Project.UtilityFunctions
{
    public class Outlook: BaseClass
    {

        By txtEmailOrPhone = By.CssSelector("input[name='loginfmt']");
        By btnNextAndSignIn = By.CssSelector("input[type='submit']");
        By txtPassword = By.CssSelector("input[type='password']");
        By picAnAccount = By.XPath("//*[@id='tilesHolder']/div[1]/div/div/div/div[2]/div");
        By btnSignIn = By.CssSelector("input[type='submit']");
        By btnYest = By.CssSelector("input[type='submit']");
        By nameIcon = By.CssSelector("button[id='mectrl_main_trigger']");//("div[id*='meInitialsButton']");
        By linkSignOut = By.CssSelector("a[id*='signOut']");
        By outlookLabel = By.CssSelector("div[id*='owaBranding_container'] > div > a > span");
        //By searchBox = By.CssSelector("div[class='DqR_QI9wxI9eR6VFTBDcQ'] > input");
        By searchBox = By.XPath("(//div[@class='rclHC']/input)[1]");
        By btnSearch = By.XPath("//*[@id='searchBoxColumnContainerId']/div[1]/button");
        //By btnSearch = By.CssSelector("i[data-icon-name='Search']");
        
        //By recentEmail = By.CssSelector("div[class='BVgxayg_IGpXi5g7S77GK'] > div:nth-child(2)");
        By recentEmail = By.CssSelector("div[class='XG5Jd JtO0E'] > div:nth-child(2)");

        By linkFirstLevelReviewSubmission = By.XPath("//span[contains(text(),'CF_Event123')]/..");
        By linkSecondLevelReviewSubmission = By.XPath("//b[normalize-space()='Review submission:']");
        By expenseRequestNumber = By.XPath("//*[@id='x_topTable']/tbody/tr[3]/td/table/tbody/tr[2]/td/p[1]/font/i[2]/font");
        By expenseRequestNumberApprove = By.XPath("//*[@id='x_topTable']/tbody/tr[3]/td/table/tbody/tr[2]/td/p[2]/font/i[2]/span");
        //*[@id="x_topTable"]/tbody/tr[3]/td/table/tbody/tr[2]/td/p[1]/font/i[2]/font

        By btnSearchScope = By.XPath("//span[@id='searchScopeButtonId-option']");
        By lblScopeInbox = By.XPath("//div[@id='searchScopeButtonId-list']/button[2]");
        By btnFilter = By.XPath("//div[text()='Filter']");
        By filterOptionUnread = By.XPath("//span[text()='Unread']");

        string dir = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData\";

        public void LoginOutlook(string file)
        {
            Thread.Sleep(2000);

            string excelPath = dir + file;
            string username = ReadExcelData.ReadData(excelPath, "UserCredential", 1);
            string password = ReadExcelData.ReadData(excelPath, "UserCredential", 2);
            if (CustomFunctions.IsElementPresent(driver, picAnAccount))
            {
                driver.FindElement(picAnAccount).Click();
                Thread.Sleep(5000);
                driver.FindElement(txtPassword).SendKeys(password);
                driver.FindElement(btnSignIn).Click();
                Thread.Sleep(25000);
                if (CustomFunctions.IsElementPresent(driver, btnYest))
                {
                    driver.FindElement(btnYest).Click();

                }
            }
            else
            if (CustomFunctions.IsElementPresent(driver, txtEmailOrPhone))
            {
                driver.FindElement(txtEmailOrPhone).SendKeys(username);
                driver.FindElement(btnNextAndSignIn).Click();
                Thread.Sleep(8000);
                driver.FindElement(txtPassword).SendKeys(password);
                driver.FindElement(btnSignIn).Click();
                Thread.Sleep(25000);
                if (CustomFunctions.IsElementPresent(driver, btnYest))
                {
                    driver.FindElement(btnYest).Click();

                }
            }
            else
            {
                Console.WriteLine("User is already logged in");
            }
          
        }

        public void SelectExpenseApprovalEmail()
        {
            Thread.Sleep(4000);
            driver.FindElement(searchBox).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearchScope, 120);

            driver.FindElement(btnSearchScope).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblScopeInbox, 120);

            driver.FindElement(lblScopeInbox).Click();
            Thread.Sleep(4000);

            driver.FindElement(searchBox).SendKeys("Sandbox: Request");
            Thread.Sleep(5000);
            driver.FindElement(searchBox).SendKeys(Keys.Enter);
            Thread.Sleep(5000);

            driver.FindElement(btnFilter).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, filterOptionUnread, 120);
            driver.FindElement(filterOptionUnread).Click();
            Thread.Sleep(4000);

            Thread.Sleep(5000);
            IWebElement element = driver.FindElement(recentEmail);
            element.Click();
            Thread.Sleep(10000);

            driver.FindElement(linkFirstLevelReviewSubmission).Click();
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(10000);
        }

        public string VerifyExpenseRequestForRejectedEmail(int windowId)
        {
            CustomFunctions.SwitchToWindow(driver, 0);
            Thread.Sleep(2000);
            driver.FindElement(searchBox).Clear();

            Thread.Sleep(1000);
            driver.FindElement(searchBox).Click();

            driver.FindElement(searchBox).Clear();
            driver.FindElement(searchBox).SendKeys("Sandbox: Marketing Expense request was rejected");
            Thread.Sleep(1000);
            driver.FindElement(searchBox).SendKeys(Keys.Enter);
            //  driver.FindElement(btnSearch).Click();
            Thread.Sleep(4000);
            IWebElement element = driver.FindElement(recentEmail);
            element.Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, expenseRequestNumber, 60);
            string expRequestNumber = driver.FindElement(expenseRequestNumber).Text;
            return expRequestNumber;
        }

        public string VerifyExpenseRequestForApprovedEmail(int windowId)
        {
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.FindElement(searchBox).Clear();
            driver.FindElement(searchBox).SendKeys("Sandbox: Request for Marketing Expense APPROVED");
            Thread.Sleep(1000);
            driver.FindElement(searchBox).SendKeys(Keys.Enter);
            //driver.FindElement(btnSearch).Click();
            Thread.Sleep(4000);
            IWebElement element = driver.FindElement(recentEmail);
            element.Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, expenseRequestNumberApprove, 60);
            string expRequestNumber = driver.FindElement(expenseRequestNumberApprove).Text.TrimEnd();
            return expRequestNumber;
        }

        public string VerifyExpenseRequestForRequestForMoreInfoEmail(int windowId)
        {
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.FindElement(searchBox).Clear();
            driver.FindElement(searchBox).SendKeys("Sandbox: Marketing Expense request was returned for more information");

            Thread.Sleep(1000);
            //driver.FindElement(btnSearch).Click();
            driver.FindElement(searchBox).SendKeys(Keys.Enter);
            Thread.Sleep(4000);
            IWebElement element = driver.FindElement(recentEmail);
            element.Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, expenseRequestNumber, 60);
            string expRequestNumber = driver.FindElement(expenseRequestNumber).Text;
            return expRequestNumber;
        }
        public void SelectSecondLevelExpenseApprovalEmail()
        {
            driver.FindElement(searchBox).SendKeys("Sandbox: Request for Marketing Expense Approval *Action Required*");
            Thread.Sleep(1000);
            //driver.FindElement(btnSearch).Click();
            driver.FindElement(searchBox).SendKeys(Keys.Enter);
            Thread.Sleep(3000);
            IWebElement element = driver.FindElement(recentEmail);
            element.Click();
            Thread.Sleep(10000);
           driver.FindElement(linkSecondLevelReviewSubmission).Click();
           CustomFunctions.SwitchToWindow(driver, 1);

            Thread.Sleep(10000);
        }

        public string GetLabelOfOutlook()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, outlookLabel, 60);
            string labelOutlook = driver.FindElement(outlookLabel).Text;
            Thread.Sleep(5000);

            return labelOutlook;
        }

        public void OutLookLogOut()
        {
            CustomFunctions.SwitchToWindow(driver, 0);
                
            //Click new coverage team button
            WebDriverWaits.WaitUntilEleVisible(driver, nameIcon, 40);
            driver.FindElement(nameIcon).Click();
            Thread.Sleep(25000);
                
            //Click new coverage team button
            WebDriverWaits.WaitUntilEleVisible(driver, linkSignOut, 40);
            driver.FindElement(linkSignOut).Click();
            Thread.Sleep(12000);
        }
    }    
}
