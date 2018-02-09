using CSharpSeleniumFramework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CSharpSeleniumFramework.WebDriver
{
    class SeleniumWebDriver
    {
        private static IWebDriver _driver;
        internal static IWebDriver WebDriver
        {
            get
            {
                if (_driver != null)
                    return _driver;

                _driver = NewInstance;
                ConfigureDriver();

                return _driver;
            }
        }
        private static IWebDriver NewInstance
        {
            get
            {
                IWebDriver newDriver;

                //if (ConfigHelpers.IsUseBrowserStack())
                //{
                //    BrowserStackDriver bsDriver = new BrowserStackDriver(ScenarioContext.Current);
                //    ScenarioContext.Current["bsDriver"] = bsDriver;

                //    newDriver = bsDriver.Init("parallel", "chrome");
                //    newDriver.Navigate().GoToUrl(ConfigHelpers.GetUrl());
                //    return newDriver;
                //}

                switch (ConfigHelpers.GetBrowser())
                {
                    case Constants.IE:
                        newDriver = new InternetExplorerDriver(new InternetExplorerOptions() { IgnoreZoomLevel = true }) { Url = ConfigHelpers.GetUrl() };
                        break;

                    //case Constants.CHROME:
                    //    string ExePath = CommonHelpers.BaseFolder() + "Resources";
                    //    newDriver = new ChromeDriver(ExePath);
                    //    newDriver.Navigate().GoToUrl(ConfigHelpers.GetUrl());

                    //    break;

                    case Constants.FIREFOX:
                        newDriver = new FirefoxDriver();
                        newDriver.Navigate().GoToUrl(ConfigHelpers.GetUrl());
                        break;

                    case Constants.SAFARI:
                        newDriver = new SafariDriver() { Url = ConfigHelpers.GetUrl() };
                        break;

                    case Constants.MICROSOFT_EDGE:
                        EdgeOptions options = new EdgeOptions();
                        options.PageLoadStrategy = PageLoadStrategy.Eager;
                        newDriver = new EdgeDriver(options);
                        newDriver.Url = ConfigHelpers.GetUrl();
                        break;

                    default:
                        throw new NotSupportedException($"{ConfigHelpers.GetBrowser()} is not a supported browser");
                }
                return newDriver;
            }
        }
        internal static void Quit()
        {
            _driver.Quit();
            _driver = null;
        }
        internal static void NullDriver()
        {
            _driver = null;
        }
        private static void ConfigureDriver()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            _driver.Manage().Window.Maximize();
            _driver.Manage().Window.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        }
        public static void NavigateToUrl(String url)
        {
            var rootUrl = new Uri(url);
            WebDriver.Navigate().GoToUrl(rootUrl);
        }
    }
}
