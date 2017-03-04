using System;

namespace TestCircuitBreaker
{
    public class CircuitTrippedException : Exception
    {
        public CircuitTrippedException(Exception exception) : base("Operation failed, circuit has tripped.", exception)
        {
        }
    }
}
