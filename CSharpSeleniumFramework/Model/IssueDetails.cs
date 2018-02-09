using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.Model
{
    class IssueDetails
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        public string Priority { get; set; }
        public string Attachment { get; set; }
        public string Status { get; set; }
        public string Resolution { get; set; }
        public string Assignee { get; set; }
        public string Reporter { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}
