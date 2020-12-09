using System;
using System.Threading.Tasks;
using FluentAssertions;
using MassTransit;
using MassTransit.Testing;
using Sample.Contracts;
using Xunit;

namespace Sample.Components.Tests
{
    public class When_an_order_request_is_consumed
    {

        [Fact]
        public async Task Should_respond_with_acceptance_if_ok()
        {
            var harness = new InMemoryTestHarness();
            ConsumerTestHarness<SubmitOrderConsumer> consumer = harness.Consumer<SubmitOrderConsumer>();

            await harness.Start();

            try
            {
                var orderId = NewId.NextGuid();

                IRequestClient<SubmitOrder> requestClient = await harness.ConnectRequestClient<SubmitOrder>();

                Response<OrderSubmissionAccepted> response = await requestClient.GetResponse<OrderSubmissionAccepted>(new
                {
                    OrderId = orderId,
                    InVar.Timestamp,
                    CustomerNumber = "12345"
                });

                // assert
                response.Message.OrderId.Should().Be(orderId);
                consumer.Consumed.Select<SubmitOrder>().Should().NotBeNullOrEmpty();
                harness.Sent.Select<OrderSubmissionAccepted>().Should().NotBeNullOrEmpty();
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Fact]
        public async Task Should_respond_with_rejected_if_customer_is_test()
        {
            var harness = new InMemoryTestHarness();
            ConsumerTestHarness<SubmitOrderConsumer> consumer = harness.Consumer<SubmitOrderConsumer>();

            await harness.Start();

            try
            {
                Guid orderId = NewId.NextGuid();

                IRequestClient<SubmitOrder> requestClient = await harness.ConnectRequestClient<SubmitOrder>();

                Response<OrderSubmissionRejected> response = await requestClient.GetResponse<OrderSubmissionRejected>(new
                {
                    OrderId = orderId,
                    InVar.Timestamp,
                    CustomerNumber = "TEST12345"
                });

                // assert
                response.Message.OrderId.Should().Be(orderId);
                consumer.Consumed.Select<SubmitOrder>().Should().NotBeNullOrEmpty();
                harness.Sent.Select<OrderSubmissionRejected>().Should().NotBeNullOrEmpty();
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Fact]
        public async Task Should_consume_submit_order_commands()
        {
            var harness = new InMemoryTestHarness { TestTimeout = TimeSpan.FromSeconds(5) };
            ConsumerTestHarness<SubmitOrderConsumer> consumer = harness.Consumer<SubmitOrderConsumer>();

            await harness.Start();

            try
            {
                var orderId = NewId.NextGuid();

                await harness.InputQueueSendEndpoint.Send<SubmitOrder>(new
                {
                    OrderId = orderId,
                    InVar.Timestamp,
                    CustomerNumber = "12345"
                });

                consumer.Consumed.Select<SubmitOrder>().Should().NotBeNullOrEmpty();
                harness.Sent.Select<OrderSubmissionAccepted>().Should().BeEmpty(); // because we do not set a RequestId when not using a request client
                harness.Sent.Select<OrderSubmissionRejected>().Should().BeEmpty();

            }
            finally
            {
                await harness.Stop();
            }
        }

        [Fact]
        public async Task Should_publish_order_submitted_event()
        {
            var harness = new InMemoryTestHarness();
            ConsumerTestHarness<SubmitOrderConsumer> consumer = harness.Consumer<SubmitOrderConsumer>();

            await harness.Start();

            try
            {
                Guid orderId = NewId.NextGuid();

                await harness.InputQueueSendEndpoint.Send<SubmitOrder>(new
                {
                    OrderId = orderId,
                    InVar.Timestamp,
                    CustomerNumber = "12345"
                });

                harness.Published.Select<OrderSubmitted>().Should().NotBeNullOrEmpty();

            }
            finally
            {
                await harness.Stop();
            }
        }

        [Fact]
        public async Task Should_not_publish_order_submitted_event_when_rejected()
        {
            var harness = new InMemoryTestHarness { TestTimeout = TimeSpan.FromSeconds(5) };
            ConsumerTestHarness<SubmitOrderConsumer> consumer = harness.Consumer<SubmitOrderConsumer>();

            await harness.Start();

            try
            {
                var orderId = NewId.NextGuid();

                await harness.InputQueueSendEndpoint.Send<SubmitOrder>(new
                {
                    OrderId = orderId,
                    InVar.Timestamp,
                    CustomerNumber = "TEST12345"
                });

                harness.Published.Select<OrderSubmitted>().Should().BeEmpty();

            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
