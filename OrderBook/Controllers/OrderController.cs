using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderBook.DTO.Orders;
using OrderBook.Services;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace OrderBook.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IDistributedCache _distributedCache;
        private readonly OrderService _orderService;

        public OrderController(
            ILogger<OrderController> logger,
            IDistributedCache distributedCache,
            OrderService orderService
        )
        {
            _logger = logger;
            _distributedCache = distributedCache;
            _orderService = orderService;
        }

        [HttpGet]
        public ContentResult Information()
        {
            string cacheKey = "info";
            var information = _distributedCache.Get(cacheKey);

            if (information != null)
            {
                Console.WriteLine("Fetch from cache");
                return Content(Encoding.UTF8.GetString(information), "text/html");
            }
            else
            {
                Console.WriteLine("Fetch from file");
                var fileString = System.IO.File.ReadAllText("Resources/Instructions.html", Encoding.UTF8);
                var encodedString = Encoding.UTF8.GetBytes(fileString);
                var options = new DistributedCacheEntryOptions()
                    //.SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .SetAbsoluteExpiration(DateTime.Now.AddSeconds(5));
                _distributedCache.Set(cacheKey, encodedString, options);
                return Content(fileString, "text/html");
            }
        }

        [HttpGet("{name}")]
        public string OrderBookDisplay(string name)
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
