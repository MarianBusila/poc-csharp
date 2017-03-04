using System;

namespace TestCircuitBreaker
{
    public class OpenCircuitException : Exception
    {
        public OpenCircuitException() : base("Circuit breaker is currently open")
        {
        }
    }
}
