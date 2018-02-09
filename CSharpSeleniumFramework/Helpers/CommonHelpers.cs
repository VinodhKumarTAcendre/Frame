using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSharpSeleniumFramework.Helpers
{
    class CommonHelpers
    {
        internal static Random random = new Random();
        public static string BaseFolder()
        {
            string _path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string _actualPath = _path.Substring(0, _path.LastIndexOf("bin"));
            string _projectPath = new Uri(_actualPath).LocalPath;
            return _projectPath;
        }
        public static void CommandExitAllBrowsersAndDrivers()
        {
            List<string> tasks = new List<string>();
            tasks.Add("chromedriver.exe");
            tasks.Add("firefox.exe");
            tasks.Add("iexplorer.exe");
            tasks.Add("IEDriverServer.exe");
            tasks.Add("geckodriver.exe");
            foreach (string s in tasks)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = @"/C Taskkill -f /IM " + s + " /T &";
                    //startInfo.Verb = "runas";
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();

                }
                catch (Exception) { }
            }
        }
        public static string GetRandomString(int length = 6)
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            var str = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return str;
        }
        public static string GetCurrentDateTimeAsString()
        {
            string formatted = DateTime.Now.ToString("ddMMyyhhmm", CultureInfo.InvariantCulture);
            return formatted;
        }
        public static TimeZoneInfo GetTimeZone(string TimeZone)
        {
            TimeZoneInfo cstZone = null;
            if (String.IsNullOrEmpty(TimeZone))
                cstZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            else
                cstZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
            return cstZone;
        }
        public static DateTime GetCurrentDateTimeInclTimeZone()
        {

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, GetTimeZone(""));
        }
        public static DateTime RoundUpTime(DateTime dt, TimeSpan d)
        {
            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
        }
        public static DateTime AddBusinessDays(DateTime current, int days)
        {
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);
                }
                while (current.DayOfWeek == DayOfWeek.Saturday ||
                    current.DayOfWeek == DayOfWeek.Sunday);
            }
            return current;
        }
        public static void InfoLogger(string Value)
        {
            Logger.log.Info(Value);
            ExtentReportsHelper.test.Log(LogStatus.Info, Value);
            Console.WriteLine(Value);
        }
        public static void WarningLogger(string Value)
        {
            Logger.log.Info(Value);
            ExtentReportsHelper.test.Log(LogStatus.Warning, Value);
            Console.WriteLine(Value);
        }
        public static void FatalLogger(string Value)
        {
            Logger.log.Fatal(Value);
            ExtentReportsHelper.test.Log(LogStatus.Fatal, Value);
            Console.WriteLine(Value);
        }
        public static void AutoItUpload(IWebDriver Driver, string FileName)
        {
            string _UploadFolderPath = BaseFolder() + "Library\\UploadFiles\\";
            string fName = _UploadFolderPath + FileName;
            string _FileUploadHeader = string.Empty;
            switch (ConfigHelpers.GetBrowser())
            {
                case Constants.IE:
                    _FileUploadHeader = "Choose File to Upload";
                    break;

                case Constants.CHROME:
                    _FileUploadHeader = "Open";
                    break;

                case Constants.FIREFOX:
                    _FileUploadHeader = "File Upload";
                    break;

                case Constants.MICROSOFT_EDGE:
                    _FileUploadHeader = "Open";
                    break;

                default:
                    throw new NotSupportedException($"{ConfigHelpers.GetBrowser()} is not a supported browser");
            }
            AutoIt.AutoItX.ControlFocus(_FileUploadHeader, "", "Edit1");
            AutoIt.AutoItX.ControlSetText(_FileUploadHeader, "", "Edit1", fName);
            AutoIt.AutoItX.ControlClick(_FileUploadHeader, "", "Button1");
        }
    }
}
