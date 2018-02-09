using NUnit.Framework;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace CSharpSeleniumFramework.Helpers
{
    class MSExcelHelper
    {
        public static string ConnectionString()
        {
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0}; Extended Properties=Excel 12.0;";
        }
        public static DataTable GetData(string ConfigKey, string SheetName)
        {
            var _ExcelSheetName = ConfigurationManager.AppSettings[ConfigKey];
            string _ExcelFolderPath = CommonHelpers.BaseFolder() + "Resources\\";
            string fileName = _ExcelFolderPath + _ExcelSheetName;

            var con = string.Format(ConnectionString(), fileName);
            var queryString = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                using (var connection = new OleDbConnection(con))
                {
                    connection.Open();

                    queryString = string.Format("select * from[" + SheetName + "$]");

                    OleDbCommand oledbCmd = new OleDbCommand(queryString, connection);

                    OleDbDataAdapter da = new OleDbDataAdapter(oledbCmd);
                    da.Fill(dt);
                    connection.Close();
                    da.Dispose();
                }
            }
            catch (OleDbException e)
            {
                Assert.Fail("\nQuery Exception. Query : {0} \n Message : {1} \n StackTrace : {2}", queryString, e.Message, e.StackTrace);
            }
            return dt;
        }
        public static void InsertData(string ConfigKey, string SheetName, string Fields, string InsertiontionValue)
        {
            var _ExcelSheetName = ConfigurationManager.AppSettings["ConfigKey"];
            string fileName = CommonHelpers.BaseFolder() + _ExcelSheetName;
            var con = string.Format(ConnectionString(), fileName);
            var queryString = string.Empty;
            using (var connection = new OleDbConnection(con))
            {
                try
                {
                    connection.Open();
                    queryString = string.Format("insert into [" + SheetName + "$] " + "(" + (Fields) + ") " + "values (" + "'{0}'" + ")", InsertiontionValue);
                    OleDbCommand oledbCmd = new OleDbCommand(queryString, connection);
                    OleDbDataReader oledbReader = oledbCmd.ExecuteReader();
                }
                catch (OleDbException e)
                {
                    Assert.Fail("\nQuery Exception. Query : {0} \n Message : {1} \n StackTrace : {2}", queryString, e.Message, e.StackTrace);
                }
            }
        }
    }
}
