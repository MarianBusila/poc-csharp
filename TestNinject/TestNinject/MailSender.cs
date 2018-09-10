using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinject
{
    public class MailSender : IMailSender
    {
        private readonly ILogging logger;

        private readonly IEnvelopeBuilder enveloperBuilder;

        public MailSender(ILogging logger, IEnvelopeBuilder enveloperBuilder)
        {
            this.logger = logger;
            this.enveloperBuilder = enveloperBuilder;
        }
        public void Send(string toAddress, string subject)
        {
            try
            {
                this.enveloperBuilder.CreateEnvelope();
                string message = string.Format("Sending mail to {0} with subject {1}", toAddress, subject);
                logger.Log(message);
            }
            catch (Exception)
            {
                
                throw;
            }           
        }
    }
}
