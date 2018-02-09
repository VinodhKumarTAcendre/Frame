using Atlassian.Jira;
using CSharpSeleniumFramework.Helpers;
using CSharpSeleniumFramework.Model;
using System;

namespace CSharpSeleniumFramework.JIRA
{
    class JIRATool
    {
        private readonly Jira inner_client;
        public JIRATool()
        {
            this.inner_client = Jira.CreateRestClient("URL", "USERNAME", "PASSWORD");
        }
        public async void CreateIssue()
        {
            IssueDetails issueDetails = new IssueDetails();
            Issue issue = inner_client.CreateIssue(JIRAHelpers.GetProject());
            issue.Description =  Constants.TESTCASENAME;
            issue.Summary = issueDetails.Summary;
            issue.Type = issueDetails.Type;
            string epicname = "Epic CSharp";
            issue.CustomFields.Add("Epic Name", epicname);
            issue.Priority = issueDetails.Priority;
            Issue IssueState = await issue.SaveChangesAsync();
            Console.WriteLine("Issue Created Succesfully. Issue ID: {0}", IssueState.Key);
            issueDetails.Key = Convert.ToString(IssueState.Key);
        }

        public void GetIssueDetails(string IssueKey)
        {
            var result = inner_client.Issues.GetIssueAsync(IssueKey).Result;
            Console.WriteLine("**********************************************");
            Console.WriteLine("         {0} - Issue Details        ", IssueKey);
            Console.WriteLine("Project Name: {0}", result.Project);
            Console.WriteLine("Type: {0}", result.Type);
            Console.WriteLine("Status: {0}", result.Status);
            Console.WriteLine("Priority: {0}", result.Priority);
            Console.WriteLine("Resolution: {0}", result.Resolution);
            Console.WriteLine("Assignee: {0}", result.Assignee);
            Console.WriteLine("Reporter: {0}", result.Reporter);
            Console.WriteLine("\nSummary: {0}", result.Summary);
            Console.WriteLine("\nDescription: {0}", result.Description);
            Console.WriteLine("\nCreated Date: {0}", result.Created);
            Console.WriteLine("Updated Date: {0}", result.Updated);
            Console.WriteLine("\n **********************************************");
        }

        public void ChangeIssueDetails(string IssueKey)
        {
            var result = inner_client.Issues.GetIssueAsync(IssueKey).Result;
            Console.WriteLine("**********************************************");
            Console.WriteLine("         {0} - Change Issue Details        ", IssueKey);

            result.Priority = "Low";
            Console.WriteLine("Priority Changed to {0}", result.Priority);

            result.SaveChanges();

            Console.WriteLine("\n **********************************************");
        }
    }
}
