using System.Collections.Generic;


namespace TraversalServices.Models
{
    public class EmailSetting
    {
        public string[] To { get; set; }

        public string[] ToCC { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public Dictionary<string, byte[]> Attachments { get; set; }

        
    }
}
