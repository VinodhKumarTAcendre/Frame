using System;
using System.Xml;

namespace CSharpSeleniumFramework.JIRA
{
    class XMLReading
    {
        public void ReadXMLData()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"D:\Learning\JIRAIntegration\JIRAIntegration\XMLTest.xml");
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/Table/CreateIssue");

            string Project = "", Type = "", TypeName = "", Summary = "", Description = "", Priority = "", Attachment = "";

            foreach (XmlNode node in nodeList)
            {
                Project = node.SelectSingleNode("Project").InnerText;
                Type = node.SelectSingleNode("Type").InnerText;
                TypeName = node.SelectSingleNode("TypeName").InnerText;
                Summary = node.SelectSingleNode("Summary").InnerText;
                Description = node.SelectSingleNode("Description").InnerText;
                Priority = node.SelectSingleNode("Priority").InnerText;
                Attachment = node.SelectSingleNode("Attachment").InnerText;
                Console.WriteLine("\r\n\n" + Project + " -- " + Type + " -- " + TypeName + " -- " + Summary + " -- " + Description + " -- " + Priority + " -- " + Attachment);
            }
        }
    }
}
