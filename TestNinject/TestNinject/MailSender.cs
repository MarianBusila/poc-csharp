using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinject
{
    class MailSender : IMailSender
    {
        private readonly ILogging logger;

        public MailSender(ILogging logger)
        {
            this.logger = logger;
        }
        public void Send(string toAddress, string subject)
        {            
            string message = string.Format("Sending mail to {0} with subject {1}", toAddress, subject);
            logger.Log(message);
        }
    }
}
