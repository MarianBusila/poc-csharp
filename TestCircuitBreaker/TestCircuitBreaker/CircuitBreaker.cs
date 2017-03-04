using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace TestCircuitBreaker
{

    public delegate void CircuitBreakerStateChangeEventHandler(CircuitBreakerState state);
    public class CircuitBreaker
    {
        private const int DefaultThreshold = 5;
        private const int DefaultOpenCircuitTimeoutInMilliseconds = 60000;

        private readonly int threshold;
        private readonly Timer openCircuitTimer;
        private readonly IList<Type> ignoredExceptionTypes;
        private int failureCount;
        private CircuitBreakerState state;
        private bool disposed;

        public CircuitBreaker()
            : this(DefaultThreshold, DefaultOpenCircuitTimeoutInMilliseconds)
        {
        }

        public CircuitBreaker(int threshold, int openCircuitTimeoutInMilliseconds)
            : this(threshold, openCircuitTimeoutInMilliseconds, new List<Type>())
        {
        }

        public CircuitBreaker(int threshold, int openCircuitTimeoutInMilliseconds, IList<Type> ignoredExceptionTypes)
        {
            if (threshold <= 0)
            {
                throw new ArgumentOutOfRangeException("threshold", threshold, "The threshold must be greater than 0.");
            }

            if (openCircuitTimeoutInMilliseconds < 1)
            {
                throw new ArgumentOutOfRangeException("openCircuitTimeoutInMilliseconds", openCircuitTimeoutInMilliseconds, "The timeout of an open circuit should be greater than 1 millisecond.");
            }

            if (ignoredExceptionTypes == null)
            {
                throw new ArgumentNullException("ignoredExceptionTypes", "The list of ignored exception type should not be null.");
            }

            this.disposed = false;
            this.threshold = threshold;
            this.failureCount = 0;
            this.state = CircuitBreakerState.Closed;
            this.ignoredExceptionTypes = ignoredExceptionTypes;

            this.openCircuitTimer = new Timer(openCircuitTimeoutInMilliseconds);
            this.openCircuitTimer.Elapsed += this.OpenCircuitTimerElapsed;
        }

        public virtual event CircuitBreakerStateChangeEventHandler StateChanged;

        public CircuitBreakerState State
        {
            get
            {
                return this.state;
            }

            private set
            {
                if (this.state != value)
                {
                    this.state = value;
                    this.InvokeStateChangeEvent();
                }
            }
        }

        public int FailureCount
        {
            get
            {
                return this.failureCount;
            }
        }

        public int Threshold
        {
            get
            {
                return this.threshold;
            }
        }

        public int TimeoutInMilliseconds
        {
            get
            {
                return (int)this.openCircuitTimer.Interval;
            }
        }

        public IList<Type> IgnoredExceptionTypes
        {
            get
            {
                return this.ignoredExceptionTypes;
            }
        }

        public virtual T Execute<T>(Delegate operation, params object[] args)
        {
            return (T)this.Execute(operation, args);
        }

        public virtual object Execute(Delegate operation, params object[] args)
        {
            if (this.State == CircuitBreakerState.Open)
            {
                throw new OpenCircuitException();
            }

            object result;
            try
            {
                result = operation.DynamicInvoke(args);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    // If there is no inner exception, then the exception was caused by the invoker, so throw
                    throw;
                }

                if (this.ignoredExceptionTypes.Contains(ex.InnerException.GetType()))
                {
                    // If exception is one of the ignored types, then throw original exception
                    throw ex.InnerException;
                }

                if (this.State == CircuitBreakerState.HalfOpen)
                {
                    // Operation failed in a half-open state, so reopen circuit
                    this.Trip(ex.InnerException);
                }
                else if (this.failureCount < this.threshold)
                {
                    // Operation failed, so increment failure count and throw exception
                    Interlocked.Increment(ref this.failureCount);
                    Console.WriteLine($"failureCount: {failureCount}");
                }
                else if (this.failureCount >= this.threshold)
                {
                    // Failure count has reached threshold, so trip circuit breaker
                    this.Trip(ex.InnerException);
                }

                throw ex.InnerException;
            }

            if (this.State == CircuitBreakerState.HalfOpen)
            {
                // If operation succeeded without error and circuit breaker is in a half-open state, then reset
                this.Reset();
            }

            if (this.failureCount > 0)
            {
                // Decrement failure count to improve service level
                Interlocked.Decrement(ref this.failureCount);
                Console.WriteLine($"failureCount: {failureCount}");
            }

            return result;
        }

        public virtual void Trip(Exception innerException)
        {
            this.Trip();
            throw new CircuitTrippedException(innerException);
        }

        public virtual void Trip()
        {
            if (this.State != CircuitBreakerState.Open)
            {
                Console.WriteLine("Trip. Changed state to Open");
                this.openCircuitTimer.Start();
                this.State = CircuitBreakerState.Open;
            }
        }

        public virtual void Reset()
        {
            if (this.State != CircuitBreakerState.Closed)
            {
                Console.WriteLine("Reset. Changed state to Closed");
                this.openCircuitTimer.Stop();
                this.State = CircuitBreakerState.Closed;
            }
        }

        private void OpenCircuitTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (this.state == CircuitBreakerState.Open)
            {
                Console.WriteLine("OpenCircuitTimerElapsed. Changed state to HalfOpen");
                this.openCircuitTimer.Stop();
                this.State = CircuitBreakerState.HalfOpen;
            }
        }

        private void InvokeStateChangeEvent()
        {
            var handler = this.StateChanged;
            if (handler != null)
            {
                handler(this.State);
            }
        }
    }
}
