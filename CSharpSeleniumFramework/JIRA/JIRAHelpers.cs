using System.Configuration;

namespace CSharpSeleniumFramework.JIRA
{
    class JIRAHelpers
    {
        public const string PROJECT = "Project";
        public static string GetProject()
        {
            return ConfigurationManager.AppSettings[PROJECT].ToString();
        }
    }
}
