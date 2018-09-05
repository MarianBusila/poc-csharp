
namespace TestNinjectTests
{
    using Moq;

    using NUnit.Framework;
    using TestNinject;

    public class MailSenderTest
    {
        private Mock<ILogging> logger;

        private Mock<IEnvelopeBuilder> envelopeBuilder;

        private MailSender target;

        [SetUp]
        public void Setup()
        {
            this.logger = new Mock<ILogging>();
            this.envelopeBuilder = new Mock<IEnvelopeBuilder>();

            this.target = new MailSender(this.logger.Object, this.envelopeBuilder.Object);
        }

        [Test]
        public void Test()
        {
            // assign
            var address = "7th Str.";
            var subject = "Hello";

            // act
            this.target.Send(address, subject);

            // assert
            this.logger.Verify(it => it.Log(It.IsAny<string>()), Times.Never);

        }

        [TearDown]
        public void TearDown()
        {
            
        }

    }
}
