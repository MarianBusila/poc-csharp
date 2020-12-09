using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MassTransit;
using MassTransit.Testing;
using Sample.Components.StateMachines;
using Sample.Contracts;
using Xunit;

namespace Sample.Components.Tests
{
    public class Submitting_an_order
    {

        [Fact]
        public async Task Should_create_a_state_instance()
        {
            var harness = new InMemoryTestHarness();
            StateMachineSagaTestHarness<OrderState, OrderStateMachine> saga = harness.StateMachineSaga<OrderState, OrderStateMachine>(new OrderStateMachine());

            await harness.Start();

            try
            {
                Guid orderId = NewId.NextGuid();

                await harness.Bus.Publish<OrderSubmitted>(new
                {
                    OrderId = orderId,
                    InVar.Timestamp,
                    CustomerNumber = "12345"
                });

                Guid? instanceId = await saga.Exists(orderId, x => x.Submitted);
                instanceId.Should().Be(orderId);

                OrderState instance = saga.Sagas.Contains(instanceId.Value);
                instance.CustomerNumber.Should().Be("12345");
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Fact]
        public async Task Should_respond_to_status_checks()
        {
            var harness = new InMemoryTestHarness();
            var orderStateMachine = new OrderStateMachine();
            StateMachineSagaTestHarness<OrderState, OrderStateMachine> saga = harness.StateMachineSaga<OrderState, OrderStateMachine>(orderStateMachine);

            await harness.Start();

            try
            {
                Guid orderId = NewId.NextGuid();

                await harness.Bus.Publish<OrderSubmitted>(new
                {
                    OrderId = orderId,
                    InVar.Timestamp,
                    CustomerNumber = "12345"
                });

                Guid? instanceId = await saga.Exists(orderId, x => x.Submitted);
                instanceId.Should().Be(orderId);

                var requestClient = await harness.ConnectRequestClient<CheckOrder>();
                var response = await requestClient.GetResponse<OrderStatus>(new
                {
                    OrderId = orderId
                });

                response.Message.State.Should().Be(orderStateMachine.Submitted.Name);

            }
            finally
            {
                await harness.Stop();
            }
        }

    }
}
