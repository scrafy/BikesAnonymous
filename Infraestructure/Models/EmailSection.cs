using System;
using System.Collections.Generic;
using System.Text;

namespace TraversalServices.Models
{
    public class EmailSection
    {
        public string From { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public short SmtpPort { get; set; } 

    }
}
