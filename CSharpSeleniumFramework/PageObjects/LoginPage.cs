using CSharpSeleniumFramework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Data;
using System.Linq;

namespace CSharpSeleniumFramework.PageObjects
{
    class LoginPage
    {
        IWebDriver _driver;

        public LoginPage(IWebDriver browser)
        {
            this._driver = browser;
            PageFactory.InitElements(_driver, this);
        }

        //[FindsBy(How = How.Id, Using = "username")]
        //public IWebElement eletxtUsername { get; set; }
        //public void Username(string Value)
        //{
        //    eletxtUsername.SendKeys(Value);
        //    CommonHelpers.InfoLogger(Value + " : entered in 'Username' Text Field");
        //}

        public void login()
        {
            DataTable dt = MSExcelHelper.GetData("ExcelFileName", "Login");

            string _userName = "KeyName = 'Dashboard'";

            if (dt.Rows.Count >= 1)
            {
                DataRow row;
                row = dt.Select(_userName).FirstOrDefault();

                string _KeyName = row.Field<string>("KeyName");
                string _UserName = row.Field<string>("Username");
                string _Password = row.Field<string>("Password");

                Logger.log.Info("Key Name: " + _KeyName);
                Logger.log.Info("Username: " + _UserName);
                Logger.log.Info("Password: " + _Password);
            }
        }
    }
}
