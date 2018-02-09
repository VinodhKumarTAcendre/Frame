using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System;
using System.Configuration;

namespace CSharpSeleniumFramework.Helpers
{
    class ConfigHelpers
    {
        public const string BROWSER = "Browser";
        public const string URL = "Url";
        public static string GetBrowser()
        {
            return ConfigurationManager.AppSettings[BROWSER].ToString();
        }
        public static string GetUrl()
        {
            string url = ConfigurationManager.AppSettings[URL].ToString();
            return url;
        }
        public static void GetResult(IWebDriver Driver)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;
            if (status == TestStatus.Failed)
            {
                string screenShotPath = ExtentReportsHelper.Capture(Driver);
                ExtentReportsHelper.test.Log(LogStatus.Fail, stackTrace + errorMessage);
                ExtentReportsHelper.test.Log(LogStatus.Fail, "Please find the Screenshot below: " + ExtentReportsHelper.test.AddScreenCapture(screenShotPath));

                // If Test is Failed
            }
        }
        public static bool IsUseBrowserStack()
        {
            return Boolean.Parse(ConfigurationManager.AppSettings["UseBrowserStack"].ToString());
        }
    }
}
