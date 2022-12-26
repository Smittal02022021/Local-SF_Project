using NUnit.Framework;
using System.IO;
using Newtonsoft.Json;

namespace SalesForce_Project.TestData
{
    class ReadJSONData
    {
        public static string dir = TestContext.CurrentContext.TestDirectory + @"\TestData\";
        public static string file = null;
        public static Data data { get; set; }

        public static void Generate(string jsonFileName = "Login.json")
        {
            file = dir + jsonFileName;
            if (Directory.Exists(dir))
            {
                data = JsonConvert.DeserializeObject<Data>(File.ReadAllText(file));
            }
        }

        public class Data
        {
            public Authentication authentication { get; set; }
            public EventExpenseFirstApproverAuthentication ApproverAuthentication { get; set; }
            public FilePaths filePaths { get; set; }
            public OpportunityTypes opportunityTypes { get; set; }
            public AddOpportunityDetails addOpportunityDetails { get; set; }
            public AdditionalClientSubject additionalClientSubject { get; set; }
        }

        public class Authentication
        {
            public string username { get; set; }
            public string password { get; set; }
            public string loggedUser { get; set; }
            public string stdUser { get; set; }
        }

        public class EventExpenseFirstApproverAuthentication
        {
            public string username { get; set; }
            public string password { get; set; }
            public string loggedUser { get; set; }
            public string stdUser { get; set; }
        }

        public class FilePaths
        {
            public string testData { get; set; }           
        }

        public class AddOpportunityDetails
        {
            public string username { get; set; }
            public string password { get; set; }
            public string loggedUser { get; set; }
            public string opportunityName { get; set; }
            public string client { get; set; }
            public string subject { get; set; }
            public string jobType { get; set; }
            public string industryGroup { get; set; }
            public string sector { get; set; }
            public string additionalClient { get; set; }
            public string additionalSubject { get; set; }
            public string referralType { get; set; }
            public string nonPublicInfo { get; set; }
            public string beneficialOwner { get; set; }
            public string primaryOffice { get; set; }
            public string legalEntity { get; set; }
            public string disclosureStatus { get; set; }
        }

        public class OpportunityTypes
        {
            public string username { get; set; }
            public string password { get; set; }
            public string loggedUser { get; set; }
            public string title { get; set; }
        }

        public class AdditionalClientSubject
        {
            public string search { get; set; }
            public string clientInterest { get; set; }
            public string staff { get; set; }
        }
    }
}
