using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Companies;
using SalesForce_Project.Pages.Company;
using SalesForce_Project.Pages.GiftLog;
using SalesForce_Project.Pages.HomePage;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.GiftLog
{
    class T2007_GiftLog_GiftRequestProcess_SplitGiftValueInEqualDistributionForSelectedRecipient : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftSubmittedPage giftsSubmit = new GiftSubmittedPage();
        GiftRequestEditPage giftEdit = new GiftRequestEditPage();

        public static string fileTC2007= "T2007_GiftRequestProcess_SplitGiftValueInEqualDistributionForSelectedRecipient";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void GiftRequestProcess_SplitGiftValueInEqualDistributionForSelectedRecipient()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2007;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC2007, user);

                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login as Gift Log User and validate the user
                usersLogin.LoginAsSelectedUser();
                string giftLogUser = login.ValidateUser();
                string giftLogUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(giftLogUserExl.Contains(giftLogUser), true);
                extentReports.CreateLog("Gift Log User: " + giftLogUser + " is able to login ");

                //Navigate to Gift Request page
                giftRequest.GoToGiftRequestPage();
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                // Enter required details in client gift pre- approval page
                string valGiftNameEntered = giftRequest.EnterGiftRequestDetails(fileTC2007);

                //Add multiple recipients to selected recipients
                int sizeOfAvailableRecipient = giftRequest.GetSizeOfAvailableRecipient();
                for (int i = 1; i <= sizeOfAvailableRecipient; i++)
                {
                    giftRequest.AddMultipleRecipientToSelectedRecipients(i-1);
                }

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.ClickAddRecipient();
                extentReports.CreateLog("Multiple Recipients added to Selected Recipients successfully ");

                int sizeOfSelectedRecipient = giftRequest.GetSizeOfSelectedRecipient();
                string giftValue = ReadExcelData.ReadData(excelPath, "GiftLog", 3);
                double totalValueOfGift = double.Parse(giftValue);
                double divideGiftValue = totalValueOfGift / sizeOfSelectedRecipient;
                double expectedDollarValueSplit = Math.Round(divideGiftValue,1);
                for (int i = 1; i <= sizeOfSelectedRecipient; i++)
                {
                    string value = giftRequest.GetDollarValue(i - 1);
                    double dollarValueSplit = double.Parse(value);
                    Assert.AreEqual(expectedDollarValueSplit, dollarValueSplit);
                }
                extentReports.CreateLog("Total dollar value split equal between the selected recipient ");
                giftRequest.RemoveSelectedRecipient();

                extentReports.CreateLog("One selected recipient is removed from the list ");
                int updatedSizeOfSelectedRecipient = giftRequest.GetSizeOfSelectedRecipient();
                double divideGiftValueWithUpdatedList = totalValueOfGift / updatedSizeOfSelectedRecipient;
                double expectedDollarValueSplitAfterUpdate = Math.Round(divideGiftValueWithUpdatedList, 1);
                for (int i = 1; i <= updatedSizeOfSelectedRecipient; i++)
                {
                    string value = giftRequest.GetDollarValue(i - 1);
                    double dollarValueSplit = double.Parse(value);
                    Assert.AreEqual(expectedDollarValueSplitAfterUpdate, dollarValueSplit);
                }

                extentReports.CreateLog("Total dollar value split equal between the selected recipient after removing one recipient ");

                giftRequest.AddRecipientToSelectedRecipients();
                extentReports.CreateLog("Add one recipient back to selected recipient list ");

                int resetSizeOfSelectedRecipient = giftRequest.GetSizeOfSelectedRecipient();
                double divideGiftValueWithResetList = totalValueOfGift / resetSizeOfSelectedRecipient;
                double expectedDollarValueSplitAfterReset = Math.Round(divideGiftValueWithResetList, 1);
                for (int i = 1; i <= resetSizeOfSelectedRecipient; i++)
                {
                    string value = giftRequest.GetDollarValue(i - 1);
                    double dollarValueSplit = double.Parse(value);
                    Assert.AreEqual(expectedDollarValueSplitAfterReset, dollarValueSplit);
                }
                extentReports.CreateLog("Total dollar value split equal between the selected recipient after adding back one recipient ");

                string DesireDate = giftRequest.EnterDesiredDate(364);
                giftRequest.ClickRefreshButton();

                //int resetSizeOfSelectedRecipient = giftRequest.GetSizeOfSelectedRecipient();
                double divideGiftWithTotalNextYear = totalValueOfGift / resetSizeOfSelectedRecipient;
                double expectedDollarTotalNextYear = Math.Round(divideGiftWithTotalNextYear, 1);
                for (int i = 1; i <= resetSizeOfSelectedRecipient; i++)
                {
                    string value = giftRequest.GetDollarValueTotalNextYear(i - 1);
                    double dollarValueSplit = double.Parse(value);
                    Assert.AreEqual(expectedDollarTotalNextYear, dollarValueSplit);
                }
                extentReports.CreateLog("Total dollar value split is coming equal in total next year ");

                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }    
    }
}