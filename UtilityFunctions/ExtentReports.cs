using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading;


namespace SalesForce_Project.UtilityFunctions
{
  public class ExtentReport :BaseClass
    {
        public ExtentTest test;
        public static string dir = TestContext.CurrentContext.TestDirectory;

        public void CreateTest(string testName)
        {
            test = extent.CreateTest(testName);
        }

        //To get logs in Extent Reports
        public void CreateLog(string message)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;            
            var errorMessage = TestContext.CurrentContext.Result.Message;
            Status logstatus;
            switch(status)
               {
                case TestStatus.Failed:
                     logstatus = Status.Fail;
                    string screenShotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
                    test.Log(logstatus,  logstatus + "-" + errorMessage);
                    test.Log(logstatus,"SnapShot below: "+test.AddScreenCaptureFromPath(screenShotPath));
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;                    
                    test.Log(logstatus, message + logstatus);
                    break;
                default:
                    logstatus = Status.Pass;                    
                    test.Log(logstatus, message + logstatus);                    
                    break;
            }      
        }

        //Capture Screenshot
        public string Capture(IWebDriver driver,string screenshotName)
        {
            string localpath = "";
            try
            {
                Thread.Sleep(4000);
                ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;
                Screenshot screenshot = takesScreenshot.GetScreenshot();
                string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                var dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\","");
                DirectoryInfo directory = Directory.CreateDirectory(dir + "\\Defects_Screenshots\\");
                string finalPath= path.Substring(0,path.LastIndexOf("bin"))+ "\\Defects_Screenshots\\"+screenshotName + ".png";
                localpath = new Uri(finalPath).LocalPath;
                screenshot.SaveAsFile(localpath);                                   
            }
            catch(Exception e)
            {
                throw (e);
            }
            return localpath;
        }

        internal void CreateLog(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
