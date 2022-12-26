using NUnit.Framework;
using OpenQA.Selenium;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SalesForce_Project.Pages.Contact
{
    class ActivityDetailPage : BaseClass
    {
        By valExternalAttendee = By.XPath("(//h3[text()='External Attendees']/following::div/table/tbody/tr/td/table)[1]/tbody/tr/td[2]/span");
        By headingActivityDetail = By.CssSelector("div[class*='tertiaryPalette'] h3");
        By txtActivityType = By.XPath("(//td[@class='data2Col  first ']/span)[2]");
        By txtActivitySubjectValue = By.XPath("(//td[@class='data2Col ']/span)[1]");
        By selInternalMentee = By.CssSelector("span[id*='j_id69:0:j_id114']");
        By defaultHLMentor = By.CssSelector("span[id*='j_id78:0:j_id119']");
        By activityFollowUpDesc = By.XPath("(//label[text()='Description']/following::td/textarea)[1]");
        By valHLAttendee = By.XPath("(//h3[text()='HL Attendees']/following::div/table/tbody/tr/td/table)[3]/tbody/tr/td[2]/span");
        By valHLAttendee1 = By.XPath("(//h3[text()='HL Attendees']/following::div/table/tbody/tr/td/table)[5]/tbody/tr/td[2]/span");
        By valCompanyDiscussed = By.XPath("(//h3[text()='Companies Discussed']/following::div/table/tbody/tr/td/table)[5]/tbody/tr/td[1]/span");
        By valueCompanyDiscussed = By.CssSelector("tbody[id*='pbsCompaniesDiscussed'] > tr > td:nth-child(1) > span");
        By valRelatedOpportunity = By.CssSelector("span[id*='j_id86:0:j_id117']");
        By valStartDateTime = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(10) > td");
        By valEndDateTime = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(11) > td");
        By valRelatedOpportunities = By.CssSelector("tbody[id*='pbsRelatedOpportunities'] tr > td:nth-child(1) > span");
        By btnReturn = By.CssSelector("td[class='pbButton '] > input[value='Return']");
        By valExternalContact = By.XPath("(//h3[text()='External Attendees']/following::div/table/tbody/tr/td/table)[1]/tbody/tr/td[2]/span");
        By valExternalAttendeeCompanyName = By.CssSelector("span[id*='j_id64:0:j_id110']");
        By valExternalAttendeeEmail = By.CssSelector("span[id*='j_id64:0:j_id111']");
        By valExternalAttendeePhone = By.CssSelector("span[id*='j_id64:0:j_id112']");
        By btnDeleteActivity = By.CssSelector("td[class='pbButton '] > input[value='Delete']");
        By valInternalNotes = By.CssSelector("textarea[name*='id41:j_id45']");
       By valNewCompanyDiscussed = By.CssSelector("span[id*='pbsCompaniesDiscussed']");

        // Function to get activity details heading
        public string GetActivityDetailsHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headingActivityDetail, 60);
            string headingActivity = driver.FindElement(headingActivityDetail).Text;
            return headingActivity;
        }

        public string GetAtivityInternalNotes()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valInternalNotes, 60);
            string internalNotesActivity = driver.FindElement(valInternalNotes).Text;
            return internalNotesActivity;
        }

        // Function to get activity type value
        public string GetExternalContactName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExternalContact, 60);
            string valueExternalContact = driver.FindElement(valExternalContact).Text;
            return valueExternalContact;
        }

        //Get new company discussed 
        public string GetNewCompanyDiscussed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewCompanyDiscussed, 60);
            string valueNewCompanyDiscussed = driver.FindElement(valNewCompanyDiscussed).Text;
            return valueNewCompanyDiscussed;
        }

        public string GetExternalAttendeeCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExternalAttendeeCompanyName, 60);
            string valueExternalAttendeeCompanyName = driver.FindElement(valExternalAttendeeCompanyName).Text;
            return valueExternalAttendeeCompanyName;
        }

        public string GetExternalAttendeeEmail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExternalAttendeeEmail, 60);
            string valueExternalAttendeeEmail = driver.FindElement(valExternalAttendeeEmail).Text;
            return valueExternalAttendeeEmail;
        }

        public void DeleteActivity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteActivity);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnDeleteActivity));

            // driver.FindElement(btnDeleteActivity).Click();
            IAlert alert = driver.SwitchTo().Alert();
            Thread.Sleep(1000);
            alert.Accept();
            Thread.Sleep(1000);
        }

        public string GetExternalAttendeePhone()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExternalAttendeePhone, 60);
            string valueExternalAttendeePhone = driver.FindElement(valExternalAttendeePhone).Text;
            return valueExternalAttendeePhone;
        }

        public string GetActivityTypeValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivityType, 60);
            string activityTypeValue = driver.FindElement(txtActivityType).Text;
            return activityTypeValue;
        }

        public string GetActivitySubjectValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtActivitySubjectValue, 60);
            string activitySubValue = driver.FindElement(txtActivitySubjectValue).Text;
            return activitySubValue;
        }

        //Function to get company discussed name
        public string GetCompanyDiscussedName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyDiscussed, 60);
            string companyDiscussed = driver.FindElement(valCompanyDiscussed).Text;
            return companyDiscussed;
        }
        public string GetCompanyDiscussedNameActivityWithoutExternal()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valueCompanyDiscussed, 60);
            string companyDiscussedActivity = driver.FindElement(valueCompanyDiscussed).Text;
            return companyDiscussedActivity;
        }
        //
        public string GetRelatedOpportunities()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRelatedOpportunities, 60);
            string relatedOpportunity = driver.FindElement(valRelatedOpportunities).Text;
            return relatedOpportunity;
        }

        public string GetActivityFollowUpDesc()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, activityFollowUpDesc, 60);
            string contactDept = driver.FindElement(activityFollowUpDesc).Text;
            return contactDept;
        }

        public string GetActivityStartDateTime()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStartDateTime, 60);
            string startDateTime = driver.FindElement(valStartDateTime).Text;
            return startDateTime;
        }

        public string GetActivityEndDateTime()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEndDateTime, 60);
            string endDateTime = driver.FindElement(valEndDateTime).Text;
            return endDateTime;
        }
        // Funtion to get activity external attendee
        public string GetActivityExternalAttendees()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExternalAttendee, 60);
            string externalAttendee = driver.FindElement(valExternalAttendee).Text;
            return externalAttendee;
        }

        //Function to get activity HL Attendee
        public string GetActivityHLAttendees()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valHLAttendee, 60);
            string HL_Attendee = driver.FindElement(valHLAttendee).Text;
            return HL_Attendee;
        }

        //Function to get activity HL Attendee
        public string GetActivityHLAttendees1()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valHLAttendee1, 60);
            string HL_Attendee = driver.FindElement(valHLAttendee1).Text;
            return HL_Attendee;
        }

        public string GetCompaniesDiscussedInFollowUpActivity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyDiscussed, 60);
            string companyDiscussedFollowUp = driver.FindElement(valCompanyDiscussed).Text;
            return companyDiscussedFollowUp;
        }

        public string GetRelatedOpportunityInFollowUpActivity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRelatedOpportunities, 60);
            string relatedOpportunityFollowUp = driver.FindElement(valRelatedOpportunities).Text;
            return relatedOpportunityFollowUp;
        }

        //Click Return Button
        public void ClickReturn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturn);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnReturn));

          //  driver.FindElement(btnReturn).Click();
        }
    }
}
