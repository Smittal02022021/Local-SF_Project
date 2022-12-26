using OpenQA.Selenium;
using SalesForce_Project.UtilityFunctions;
using System.Threading;

namespace SalesForce_Project.Pages.HomePage
{
    class LVHomePage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();

        By btnMenu = By.XPath("//button[@class='slds-button slds-show']");
        By lblHLBanker = By.XPath("//span[@title='HL Banker']");
        By btnMenu1 = By.XPath("(//span[@title='HL Banker']/../preceding::button)[8]");
        By txtSearchItems = By.XPath("//input[@placeholder='Search apps and items...']");
        By itemExpenseRequestLWC = By.XPath("(//h3[text()='Items']/following::div/*/span/p/b[text()='Expense Request(LWC)'])[1]");
        By linkLogout = By.XPath("//a[contains(text(),'Log out')]");
        By btnMainSearch = By.XPath("//button[@aria-label='Search']");
        By txtMainSearch = By.XPath("//input[@placeholder='Search...']");
        By userImage = By.XPath("(//span[@data-aura-class='uiImage'])[1]");
        By linkLogOut = By.XPath("//a[text()='Log Out']");
        
        string dir = @"C:\Users\SMittal0207\source\repos\SalesForce_Project\TestData\";

        public void SearchText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtMainSearch).SendKeys("dummyText");
            Thread.Sleep(3000);
        }

        public void ClickExpenseRequestMenuButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMenu, 140);
            driver.FindElement(btnMenu).Click();
            Thread.Sleep(3000);
        }

        public void ClickHomePageMenu()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMenu1, 140);
            driver.FindElement(btnMenu1).Click();
            Thread.Sleep(3000);
        }

        public void SearchItemExpenseRequestLWC(string item)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchItems, 140);
            driver.FindElement(txtSearchItems).SendKeys(item);
            Thread.Sleep(2000);
            driver.FindElement(itemExpenseRequestLWC).Click();
            Thread.Sleep(3000);
        }

        public void UserLogoutFromSFLightningView()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkLogout, 140);
            driver.FindElement(linkLogout).Click();
            Thread.Sleep(4000);
        }

        public void LogoutFromSFLightningAsApprover()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, userImage, 120);
            driver.FindElement(userImage).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkLogOut, 120);
            driver.FindElement(linkLogOut).Click();
            Thread.Sleep(10000);
        }
    }
}