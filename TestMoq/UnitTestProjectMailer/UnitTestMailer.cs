using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mailer;

namespace UnitTestProjectMailer
{
    [TestClass]
    public class UnitTestDefaultMailer
    {
        [TestMethod]
        public void SendMail()
        {
            //assign
            var mockMailClient = new Mock<IMailClient>();
            mockMailClient.Setup(s => s.Server).Returns("chat.mail.com");
            mockMailClient.Setup(s => s.Port).Returns("1234");
            mockMailClient.Setup(s => s.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //act
            IMailer mailer = new DefaultMailer { From = "marian@gmail.com", To="dest@yahoo.com", Subject = "Using Moq", Body = "Hello world!" };
            var result = mailer.SendMail(mockMailClient.Object);

            //assert
            Assert.IsTrue(result);
            //verify that MailClient's SendMail is called only once
            mockMailClient.Verify(client => client.SendMail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

        }
    }
}
