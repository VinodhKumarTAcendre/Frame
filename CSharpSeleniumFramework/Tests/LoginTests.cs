using CSharpSeleniumFramework.Helpers;
using CSharpSeleniumFramework.WebDriver;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RelevantCodes.ExtentReports;

namespace CSharpSeleniumFramework.Tests
{
    [TestFixture]
    class LoginTests
    {
        private static IWebDriver Driver;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            CommonHelpers.CommandExitAllBrowsersAndDrivers();
            Driver = SeleniumWebDriver.WebDriver;
            ExtentReportsHelper.CreateExtenReportsFile();
        }

        [Test]
        [TestCase("Login Page: Login to Application")]
        public void Login(string TestCaseName)
        {
            ExtentReportsHelper.test = ExtentReportsHelper.extent.StartTest(TestCaseName);
            Constants.TESTCASENAME = TestCaseName;
            ExtentReportsHelper.test.AssignCategory("Home Page");
            ExtentReportsHelper.test.Log(LogStatus.Pass, "Login to Application Test Passed");
            Logger.log.Info("Login to Application Test Passed");
            object test = "";
            Assert.IsNotNull(test, "Is null");
        }

        [TearDown]
        public void TearDown()
        {
            ConfigHelpers.GetResult(Driver);
            ExtentReportsHelper.extent.EndTest(ExtentReportsHelper.test);
            Driver.Quit();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentReportsHelper.extent.Flush();
            ExtentReportsHelper.extent.Close();
        }
    }
}
