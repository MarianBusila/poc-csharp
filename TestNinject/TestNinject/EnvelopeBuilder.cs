using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinject
{
    public class EnvelopeBuilder : IEnvelopeBuilder
    {
        private readonly string color;

        public EnvelopeBuilder(string color)
        {
            this.color = color;
        }
        public void CreateEnvelope()
        {
            Console.WriteLine($"Create {color} envelope");
        }
    }
}
