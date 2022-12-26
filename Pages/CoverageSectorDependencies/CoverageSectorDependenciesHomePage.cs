using OpenQA.Selenium;
using SalesForce_Project.UtilityFunctions;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class CoverageSectorDependenciesHomePage : BaseClass
    {
        By btnNew = By.XPath("//input[@title='New']");
        By linkCoverageSectorDependencyName = By.XPath("//th[contains(text(),'Coverage Sector Dependency Name')]/following::tr/th/a");

        public void ClickNewCoverageDependenciesButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNew, 120);
            driver.FindElement(btnNew).Click();
            Thread.Sleep(2000);
        }

        public void NavigateToCoverageSectorDependencyDetailPage(string covSectorDependencyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCoverageSectorDependencyName, 120);
            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(2000);
        }
    }
}
