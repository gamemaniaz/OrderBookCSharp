using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderBook.DTO.Orders;
using OrderBook.Services;
using System.Text.Json;

namespace OrderBook.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly OrderService _orderService;

        public OrderController(ILogger<OrderController> logger, OrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet]
        public String Information()
        {
            return "Hello World"; // todo : return some sort of instructions page later
        }

        [HttpGet("{name}")]
        public String OrderBookDisplay(string name)
        {
            return _orderService.DisplayOrderBook(name);
        }

        [HttpPost("Limit")]
        public IActionResult LimitOrder(LimitOrderRequestDto limitOrderRequest)
        {
            string status = _orderService.BookLimitOrder(
                limitOrderRequest.Name,
                limitOrderRequest.Side,
                limitOrderRequest.Size,
                limitOrderRequest.Price,
                limitOrderRequest.Trader
            );
            return new JsonResult(new LimitOrderResponseDto(status));
        }
    }
}
