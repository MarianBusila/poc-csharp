using System;
using Automatonymous;
using MassTransit;
using Sample.Contracts;

namespace Sample.Components.StateMachines
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {

        public OrderStateMachine()
        {
            Event(() => OrderSubmitted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderStatusRequested, x =>
            {
                x.CorrelateById(m => m.Message.OrderId);
                x.OnMissingInstance(m => m.ExecuteAsync(async context => 
                { 
                    if(context.RequestId.HasValue)
                    {
                        await context.RespondAsync<OrderNotFound>(new { context.Message.OrderId });
                    }
                }));
            });

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

            // if order is in any state (except initial and final), when OrderSubmitted event is received, we just copy some data in the state, but we do not change the state
            DuringAny(
                When(OrderSubmitted)
                    .Then(context =>
                    {
                        context.Instance.SubmitDate ??= context.Data.Timestamp;
                        context.Instance.CustomerNumber ??= context.Data.CustomerNumber;
                    })
                );

            DuringAny(
                When(OrderStatusRequested)
                .RespondAsync(x => x.Init<OrderStatus>(new
                {
                    OrderId = x.Instance.CorrelationId,
                    State = x.Instance.CurrentState
                })));
        }

        public State Submitted { get; private set; }



        public Event<OrderSubmitted> OrderSubmitted { get; private set; }
        public Event<CheckOrder> OrderStatusRequested { get; private set; }

    }
}

