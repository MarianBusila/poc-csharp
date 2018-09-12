using System;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;

using Library;

namespace JaegerHelloWorld
{
    internal class Hello
    {
        private readonly ITracer _tracer;
        private readonly ILogger<Hello> _logger;

        public Hello(ITracer tracer, ILoggerFactory loggerFactory)
        {
            _tracer = tracer;
            _logger = loggerFactory.CreateLogger<Hello>();
        }

        void SayHello(string helloTo)
        {
            var span = _tracer.BuildSpan("say-hello").Start();
            var helloString = $"Hello, {helloTo}";
            _logger.LogInformation(helloString);
            span.Finish();
        }

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException("Expecting one argument");
            }

            using (var loggerFactory = new LoggerFactory().AddConsole())
            {
                var tracer = Tracer.InitTracer("hello-world", loggerFactory);
                var helloTo = args[0];
                new Hello(tracer, loggerFactory).SayHello(helloTo);
            }

        }
    }
}
