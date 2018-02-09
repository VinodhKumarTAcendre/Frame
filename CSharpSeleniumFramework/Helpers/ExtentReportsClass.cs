using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace CSharpSeleniumFramework.Helpers
{
    public class ExtentReportsHelper
    {
        public static ExtentReports extent;
        public static ExtentTest test;
        public static string ReportPath = null;
        public static void CreateExtenReportsFile()
        {
            string ReportsFolder = ConfigurationManager.AppSettings["ExtentReportsDir"].ToString();
            string Browser = ConfigurationManager.AppSettings["BROWSER"].ToString();

            string _reportPath = CommonHelpers.BaseFolder() + "Reports\\Output\\" + ReportsFolder;
            if (!Directory.Exists(_reportPath))
            {
                Directory.CreateDirectory(_reportPath);
            }
            string _CurrentDate = Convert.ToString(DateTime.Now);
            string _FileName = _CurrentDate.Replace("/", "_").Replace(":", "_").Replace(" ", "_");
            _reportPath += "\\" + Browser + "_ExtentReport_" + _FileName + ".html";
            ReportPath = _reportPath;
            try
            {
                if (!File.Exists(ReportPath))
                {
                    File.Create(ReportPath).Close();
                }
                extent = new ExtentReports(ReportPath, false);
                extent
                    .AddSystemInfo("Host Name", "Automation Framework")
                    .AddSystemInfo("Environment", "Windows")
                    .AddSystemInfo("Application", "Demo")
                    .AddSystemInfo("Owner", "Vinodh Kumar T");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static string Capture(IWebDriver Driver)
        {
            string CurrentDate = Convert.ToString(DateTime.Now);
            string screenShotName = CurrentDate.Replace("/", "_").Replace(":", "_").Replace(" ", "_");
            ITakesScreenshot ts = (ITakesScreenshot)Driver;
            Screenshot screenshot = ts.GetScreenshot();
            string finalpth = CommonHelpers.BaseFolder() + "Reports\\Output\\ErrorScreenshots\\";
            if (!Directory.Exists(finalpth))
            {
                Directory.CreateDirectory(finalpth);
            }
            finalpth += screenShotName + ".png";
            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Jpeg);
            return localpath;
        }
        public static string GetEmailTemplate()
        {
            string _FolderPath = CommonHelpers.BaseFolder() + "Resources\\EmailTemplate\\";
            string _File = ConfigurationManager.AppSettings["EmailTemplate"];
            string _EmailTemp = _FolderPath + _File;
            string text;
            var fileStream = new FileStream(_EmailTemp, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            return text;
        }
        public static void SendEmail()
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string AttchmentPath = string.Empty;
            string _Receivers = ConfigurationManager.AppSettings["Receivers"].ToString();
            string[] _Receive = _Receivers.Split(',');
            foreach (string EmailId in _Receive)
            {
                mail.To.Add(new MailAddress(EmailId));
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["Sender"]);
            mail.Subject = "Execution Report";
            mail.Body = GetEmailTemplate();
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            AttchmentPath = ReportPath;
            Attachment attachment;
            attachment = new Attachment(AttchmentPath);
            mail.Attachments.Add(attachment);
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["UserId"],
                                                                ConfigurationManager.AppSettings["Password"]);
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
