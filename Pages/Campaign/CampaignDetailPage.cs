using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages
{
    class CampaignDetailPage : BaseClass
    {
        By linkRemoveContact = By.XPath("//a[@title='Remove - Record 1 - Contact']");
        By btnDelete = By.XPath("//input[@title='Delete']");

        public void DeleteContactFromCampaignHistory()
        {
            driver.FindElement(linkRemoveContact).Click();
            Thread.Sleep(5000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(5000);
        }

        public void DeleteCampaign()
        {
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }
    }
}
