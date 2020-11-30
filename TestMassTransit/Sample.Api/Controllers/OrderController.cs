﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sample.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IRequestClient<SubmitOrder> _submitOrderRequestClient;

        public OrderController(ILogger<OrderController> logger, IRequestClient<SubmitOrder> submitOrderRequestClient)
        {
            _logger = logger;
            _submitOrderRequestClient = submitOrderRequestClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Guid id, string customerNumber)
        {
            (Task<Response<OrderSubmissionAccepted>> accepted, Task<Response<OrderSubmissionRejected>> rejected) = await _submitOrderRequestClient.GetResponse<OrderSubmissionAccepted, OrderSubmissionRejected>(new 
            {
                OrderId = id,
                CustomerNumber = customerNumber,
                InVar.Timestamp

            });
            if (accepted.IsCompletedSuccessfully)
            {
                Response<OrderSubmissionAccepted> response = await accepted;
                return Accepted(response.Message);
            }
            else
            {
                Response<OrderSubmissionRejected> response = await rejected;
                return BadRequest(response.Message);
            }
        }

    }
}