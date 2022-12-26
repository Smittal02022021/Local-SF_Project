using OpenQA.Selenium;
using SalesForce_Project.UtilityFunctions;
using System.Threading;

namespace SalesForce_Project.Pages.Common
{
    class UsersLogin : BaseClass
    {
    //dummy changes 3
        By linkSetUp = By.CssSelector("a[id='setupLink']");
        By linkManageUsers = By.CssSelector("[id='Users_font']");
        By linkUsers = By.Id("ManageUsers_font");
        By comboView = By.Id("fcf");
        //By linkLogin = By.CssSelector("a[title*='Login - Record 8 - Standard User, Verifaya']");
        By linkStdUser = By.XPath("//a[contains(text(),'User, SFStandard')]");
        By btnLogin = By.CssSelector("input[title ='Login']");
        By loggedUser = By.XPath("//span[@id='userNavLabel']");
        By linkLogOut = By.CssSelector("a[title='Logout']");
        By linkCAOUser = By.XPath("//a[contains(text(),'User, SFCAO')]");
        By linkFASCAOUser = By.XPath("//a[contains(text(),'User, SFFASCAO')]");
        By linkFRCAOUser = By.XPath("//a[contains(text(),'User, SFFRCAO')]");
        By tabHome = By.CssSelector("a[title*='Home Tab']");
        By linkStdFASUser = By.XPath("//a[contains(text(),'User, SFStdFAS')]");
        By linkStdFRUser = By.XPath("//a[contains(text(),'User, SFStdFR')]");
        By linkFRAccountingUser = By.XPath("//a[contains(text(),'User, SFFRAccounting')]");
        By linkcompUser = By.XPath("//a[contains(text(),'User, SFCompliance')]");
        By txtSearch = By.CssSelector("input[id*='phSearchInput']");
        By listUser = By.CssSelector("div[id*='phSearchInput']>div>ul>li");
        By arrowMenu = By.CssSelector("a[title='User Action Menu']");
        By titleUserDetail = By.CssSelector("a[title='User Detail']");
        By dropDwnForUserDetail = By.CssSelector("a[id='moderatorMutton']");
        By optionUserDetail = By.CssSelector("a[id='USER_DETAIL']");

        public void ClickManageUsers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSetUp);
            driver.FindElement(linkSetUp).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkManageUsers);
            driver.FindElement(linkManageUsers).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkUsers);
            driver.FindElement(linkUsers).Click();
            Thread.Sleep(4000);
            CustomFunctions.SelectValueWithoutSelect(driver, comboView, "Test Automation Users");            
        }

        public void LoginAsStandardUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkStdUser,80);
            driver.FindElement(linkStdUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }
        //To logout from a user
        public void UserLogOut()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, loggedUser, 140);
            driver.FindElement(loggedUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkLogOut, 60);
            driver.FindElement(linkLogOut).Click();
        }
        //To login with another user
        public void LoginAsCAOUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkUsers);
            driver.FindElement(linkUsers).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkCAOUser, 60);
            driver.FindElement(linkCAOUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }
        

        //Login as FAS CAO user
        public void LoginAsFASCAOUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkFASCAOUser);
            driver.FindElement(linkFASCAOUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }

        //Click on Home tab
        public void ClickHomeTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabHome,120);
            driver.FindElement(tabHome).Click();
        }

        //Login as Standard FAS user
        public void LoginAsStandardFASUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkStdFASUser);
            driver.FindElement(linkStdFASUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }

        //Login as Standard FR user
        public void LoginAsStandardFRUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkStdFRUser);
            driver.FindElement(linkStdFRUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }
        //Login as FR CAO user
        public void LoginAsFRCAOUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkFRCAOUser);
            driver.FindElement(linkFRCAOUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }

        //Login as FR Accounting user
        public void LoginAsFRAccountingUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkFRAccountingUser);
            driver.FindElement(linkFRAccountingUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }

        public void LoginAsComplianceUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkcompUser);
            driver.FindElement(linkcompUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }

        public void SearchUserAndLogin(string name)
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearch, 150);
            driver.FindElement(txtSearch).SendKeys(name);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listUser, name);
            WebDriverWaits.WaitUntilEleVisible(driver, arrowMenu, 130);
            driver.FindElement(arrowMenu).Click();
            driver.FindElement(titleUserDetail).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin, 100);
            driver.FindElement(btnLogin).Click();
        }
        //Login as Selected user from global search
        public void LoginAsSelectedUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, dropDwnForUserDetail);
            driver.FindElement(dropDwnForUserDetail).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, optionUserDetail);
            driver.FindElement(optionUserDetail).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
            Thread.Sleep(2000);
        }
        //--------------------
       


    
        
    }
}


