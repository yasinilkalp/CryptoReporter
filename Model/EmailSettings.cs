using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoReporter.Model
{
    public class EmailSettings
    {
        public string Recipient { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPass { get; set; }
        public bool IsSSL { get; set; }
    }
}
