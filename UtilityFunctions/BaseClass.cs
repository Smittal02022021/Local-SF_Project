using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace SalesForce_Project.UtilityFunctions
{
    public class BaseClass
    {
        public static IWebDriver driver;
        public static ExtentReports extent;
        public static Actions builder;

        public IWebDriver Initialize()
        {            
            string path = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData";
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=" + path);
            options.AddArguments("profile-directory=Default");
            driver = new ChromeDriver(options);            
            driver.Navigate().GoToUrl("https://hl--test.my.salesforce.com/");
            driver.Manage().Window.Maximize();
            builder = new Actions(driver);
            return driver;
        }

        public IWebDriver Initialize1()
        {
            string path = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData";
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=" + path);
            options.AddArguments("profile-directory=Default");
            options.AddUserProfilePreference("download.default_directory", @"C:\HL\SalesForce_Project\SalesForce_Project\TestData");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://tablab.hl.com/#/signin?redirect=%2Fsite%2FTEST%2Fviews%2FFSCSponsorDashboardSprint9%2FSponsorReport%3F:iid%3D1&error=42&disableAutoSignin=yes");
            driver.Manage().Window.Maximize();
            return driver;
        }

        public IWebDriver TestInitialize()
        {
            string path = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData";
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=" + path);
            options.AddArguments("profile-directory=Default");
            options.AddUserProfilePreference("download.default_directory", @"C:\HL\SalesForce_Project\SalesForce_Project\TestData");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
            driver.Manage().Window.Maximize();
            return driver;
        }

        public static ExtentReports ExtentReportHelper()
        {
            if (extent == null)
            {
                extent = new ExtentReports();
                var htmlReporter = new ExtentHtmlReporter(@"C:\HL\SalesForce_Project\SalesForce_Project\Reports\ExtentReport.html");
                htmlReporter.Config.DocumentTitle="Test Execution Report";
                extent.AttachReporter(htmlReporter);
                extent.AddSystemInfo("Application Under Test", "Salesforce Application");
                extent.AddSystemInfo("Environment", "Test");
                extent.AddSystemInfo("Machine", Environment.MachineName);
            }
            return extent;
        }

        public IWebDriver OutLookInitialize()
        {
            string path = @"C:\HL\SalesForce_Project\SalesForce_Project\TestData\User Data\";
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=" + path);
            options.AddArguments("profile-directory=Default");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://outlook.office.com/");
            driver.Manage().Window.Maximize();

            return driver;
        }

        [OneTimeTearDown]
        public void ExtentClose()
        {
            try
            {
                extent.Flush();
            }
            catch (Exception e)
            {
                throw e;
            }
            driver.Quit();
        }

        public static IWebDriver Driver
        {
            get { return driver; }
        }
    }
}
