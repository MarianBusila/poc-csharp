using System;
using Automatonymous;
using MassTransit.RedisIntegration;
using Sample.Contracts;

namespace Sample.Components.StateMachines
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {

        public OrderStateMachine()
        {
            Event(() => OrderSubmitted, x => x.CorrelateById(m => m.Message.OrderId));

            InstanceState(x => x.CurrentState);

            // initially, when we receive OrderSubmitted event we transition to Submitted state
            Initially(
                When(OrderSubmitted)
                .Then(context =>
                {
                    context.Instance.SubmitDate = context.Data.Timestamp;
                    context.Instance.Updated = DateTime.UtcNow;
                    context.Instance.CustomerNumber = context.Data.CustomerNumber;
                })
                    .TransitionTo(Submitted)
                );

            // if order is already in Submitted state, it should ignore the event OrderSubmitted
            During(Submitted, 
                Ignore(OrderSubmitted));

            // if order is in any state, when OrderSubmitted event is received, we just copy some data in the state, but we do not change the state
            DuringAny(
                When(OrderSubmitted)
                    .Then(context =>
                    {
                        context.Instance.SubmitDate = context.Data.Timestamp;
                        context.Instance.CustomerNumber = context.Data.CustomerNumber;
                    })
                );
        }

        public State Submitted { get; private set; }



        public Event<OrderSubmitted> OrderSubmitted { get; private set; }

    }



    public class OrderState : SagaStateMachineInstance, IVersionedSaga
    {

        public Guid CorrelationId { get; set; }
        public int Version { get; set; }

        public string CurrentState { get; set; }

        public string CustomerNumber { get; set; }

        public DateTime SubmitDate { get; set; }
        public DateTime Updated { get; set; }
        

    }
}

