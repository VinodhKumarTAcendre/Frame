using CSharpSeleniumFramework.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSharpSeleniumFramework.Helpers
{
    class BrowserHelpers
    {
        static IWebDriver Driver = SeleniumWebDriver.WebDriver;
        public static bool IsElementExists(By by, int pollingIntervalInSecs = 1,
                                                          int timeoutInSecs = 30)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSecs));
                wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSecs);
                var clickableElement = wait.Until(ExpectedConditions.ElementExists(by));

                if (clickableElement != null)
                    return true;
            }
            catch (Exception) { }
            return false;
        }

        public static IWebElement WaitForElementClickable(By by, int pollingIntervalInSecs = 1,
                                                              int timeoutInSecs = 30)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSecs));
                wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSecs);
                return wait.Until(ExpectedConditions.ElementToBeClickable(by));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IWebElement WaitForElementToBeVisible(By by, int pollingIntervalInSecs = 1,
                                                             int timeoutInSecs = 30)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSecs));
                wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSecs);
                return wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool WaitForElementToBeInvisible(By by, int pollingIntervalInSecs = 1,
                                                     int timeoutInSecs = 30)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSecs));
                wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSecs);
                return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            }
            catch (Exception)
            { return false; }
        }

        public static bool WaitForElementTextToBeInElementValue(By by, string text = "", int pollingIntervalInSecs = 1,
                                                            int timeoutInSecs = 30)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSecs));
                wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSecs);
                return wait.Until(ExpectedConditions.TextToBePresentInElementValue(by, text));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IWebElement WaitUntilElementExists(By by, int pollingIntervalInSecs = 1, int timeoutInSecs = 30)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSecs));
                wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSecs);
                return wait.Until(ExpectedConditions.ElementExists(by));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IWebElement WaitAndSendTextEDGE(string selectorType, string selValue, string inputText,
                                                        int pollingIntervalInSecs = 1, int timeoutInSecs = 30)
        {

            long currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long timeOutInMilliSecs = DateTime.Now.AddSeconds(timeoutInSecs).Ticks / TimeSpan.TicksPerMillisecond;
            IWebElement ele = null;
            bool elementFound = false;

            while (!elementFound && currentTimeInMilliSecs < timeOutInMilliSecs)
            {

                if (selectorType.Equals(Constants.XPATH))
                    ele = Driver.FindElement(By.XPath(selValue));
                else
                    ele = Driver.FindElement(By.CssSelector(selValue));

                if (ele != null)
                {
                    elementFound = true;

                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                    StringBuilder stringBuilder = new StringBuilder();

                    if (selectorType.Equals(Constants.XPATH))
                        stringBuilder.Append("var x = document.evaluate(\"" + selValue + "\", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;");
                    else
                        stringBuilder.Append("var x = document.querySelector(\"" + selValue + "\");");

                    stringBuilder.Append("x.setAttribute('value',\"" + inputText + "\");");
                    js.ExecuteScript(stringBuilder.ToString());
                    break;
                }

                Thread.Sleep(TimeSpan.FromSeconds(pollingIntervalInSecs));
                ele = null;
                currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            }
            return ele;
        }

        public static IWebElement WaitAndClickElementEDGE(string selValue,
                                                        int pollingIntervalInSecs = 1, int timeoutInSecs = 30, string selectorType = Constants.CSSSELECTOR)
        {

            long currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long timeOutInMilliSecs = DateTime.Now.AddSeconds(timeoutInSecs).Ticks / TimeSpan.TicksPerMillisecond;
            IWebElement ele = null;
            bool elementFound = false;

            while (!elementFound && currentTimeInMilliSecs < timeOutInMilliSecs)
            {

                try
                {
                    if (selectorType.Equals(Constants.XPATH))
                        ele = Driver.FindElement(By.XPath(selValue));
                    else
                        ele = Driver.FindElement(By.CssSelector(selValue));

                    if (ele != null)
                    {

                        elementFound = true;

                        IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                        StringBuilder stringBuilder = new StringBuilder();

                        if (selectorType.Equals(Constants.XPATH))
                            stringBuilder.Append("var x = document.evaluate(\"" + selValue + "\", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;");
                        else
                            stringBuilder.Append("var x = document.querySelector(\"" + selValue + "\");");

                        stringBuilder.Append("x.click();");
                        js.ExecuteScript(stringBuilder.ToString());
                        break;
                    }

                    Thread.Sleep(TimeSpan.FromSeconds(pollingIntervalInSecs));
                    ele = null;
                    currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                }
                catch (NoSuchElementException)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(pollingIntervalInSecs));
                    ele = null;
                    currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    continue;
                }
            }
            return ele;
        }

        public static List<IWebElement> WaitForElements(By by, int pollingIntervalInSecs = 1,
                                                                int timeoutInSecs = 30)
        {
            List<IWebElement> elements = null;
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSecs));

                wait.PollingInterval = TimeSpan.FromSeconds(pollingIntervalInSecs);
                wait.Until(ExpectedConditions.ElementIsVisible(by));
                elements = wait.Until<List<IWebElement>>((d) =>
                {
                    return d.FindElements(by).ToList();
                });
            }
            catch (Exception) { elements = null; }
            return elements;
        }

        public static IWebElement[] WaitForElementsUsingWhileLoop(By by, int pollingIntervalInSecs = 1,
                                                               int timeoutInSecs = 30)
        {
            IWebElement[] ele = null;
            long currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long timeOutInMilliSecs = DateTime.Now.AddSeconds(timeoutInSecs).Ticks / TimeSpan.TicksPerMillisecond;
            bool elementFound = false;

            while (!elementFound || currentTimeInMilliSecs < timeOutInMilliSecs)
            {
                try
                {
                    ele = Driver.FindElements(by).ToArray();
                    if (ele != null && ele.Count() > 0)
                        return ele;
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(pollingIntervalInSecs));
                        ele = null;
                        currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                    }
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(pollingIntervalInSecs));
                    ele = null;
                    currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    continue;
                }
            }
            return ele;
        }

        public static IWebElement WaitUsingWhileLoop(By by,
                                                        int pollingIntervalInSecs = 1, int timeoutInSecs = 30)
        {

            long currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long timeOutInMilliSecs = DateTime.Now.AddSeconds(timeoutInSecs).Ticks / TimeSpan.TicksPerMillisecond;
            IWebElement ele = null;
            string text = null;
            bool elementFound = false;

            while (!elementFound && currentTimeInMilliSecs < timeOutInMilliSecs)
            {
                try
                {
                    ele = Driver.FindElement(by);
                    text = ele.Text.ToString();

                    if (!string.IsNullOrEmpty(text))
                        return ele;
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(pollingIntervalInSecs));
                        ele = null; text = null;
                        currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(pollingIntervalInSecs));
                    ele = null; text = null;
                    currentTimeInMilliSecs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    continue;
                }
            }
            return ele;
        }
        public static void Navigate_Back(IWebDriver Driver)
        {
            Driver.Navigate().Back();
        }
        public static void Navigate_Forward(IWebDriver Driver)
        {
            Driver.Navigate().Forward();
        }
        public static void Page_Refresh(IWebDriver Driver)
        {
            Driver.Navigate().Refresh();
        }
        public static void SleepTimeOut(int Value)
        {
            System.Threading.Thread.Sleep(Value);
        }
        public static void IsAlertPresent(IWebDriver Driver, string Action)
        {
            try
            {
                IAlert alert = Driver.SwitchTo().Alert();
                if (Action == "Accept")
                {
                    alert.Accept();
                    Console.WriteLine("Alert is Accepted");
                }
                else if (Action == "Dismiss")
                {
                    alert.Dismiss();
                    Console.WriteLine("Alert is Dismissed");
                }
                else
                {
                    Console.WriteLine("Alert Action is Indefined");
                }
            }
            catch (NoAlertPresentException NoAlert)
            {
                Console.WriteLine("No Alert is Present");
                Console.WriteLine(NoAlert.StackTrace);
            }
        }
        public static string GetAlertText(IWebDriver Driver)
        {
            try
            {
                IAlert alert = Driver.SwitchTo().Alert();
                string _text = alert.Text;
                alert.Accept();
                return _text;
            }
            catch (NoAlertPresentException NoAlert)
            {
                Console.WriteLine("No Alert is Present");
                Console.WriteLine(NoAlert.StackTrace);
            }
            return "No Alert is Present";
        }
        public static void DropDownSelectByText(IWebElement Ddl, string SelectText)
        {
            if (SelectText != "Null")
            {
                SelectElement select = new SelectElement(Ddl);
                select.SelectByText(SelectText);
                CommonHelpers.InfoLogger(SelectText + " - Option Selected from Drop Down");
            }
        }
        public static void SelectCheckBox(IWebElement ele, string Value, string CheckBoxName)
        {
            if (Value == "Yes")
            {
                if (!ele.Selected)
                {
                    ele.Click();
                    CommonHelpers.InfoLogger(CheckBoxName + " : is Checked");
                }
            }
            else
            {
                if (ele.Selected)
                {
                    ele.Click();
                }
                CommonHelpers.InfoLogger(CheckBoxName + " : is UnChecked");
            }
        }
        public static void ScrollTo(IWebDriver Driver, int xPosition = 0, int yPosition = 0)
        {
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            ((IJavaScriptExecutor)Driver).ExecuteScript(js);
        }
        public static void ScrollToView(IWebDriver Driver, IWebElement element)
        {
            if (element.Location.Y > 200)
            {
                ScrollTo(Driver, 0, element.Location.Y - 100); // Make sure element is in the view but below the top navigation pane
            }
        }
        public static void ExplicitWait(IWebDriver Driver, string XPATH)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromMinutes(2));
            Func<IWebDriver, bool> waitForElement = new Func<IWebDriver, bool>((IWebDriver Web) =>
            {
                Web.FindElement(By.XPath(XPATH));
                return true;
            });
            wait.Until(waitForElement);
        }
        public static void MouseOveronElement(IWebDriver Driver, IWebElement ele)
        {
            Actions a = new Actions(Driver);
            a.MoveToElement(ele).Build().Perform();
        }
    }
}
