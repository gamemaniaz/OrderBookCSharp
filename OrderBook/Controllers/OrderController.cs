using System;
using System.Text;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OrderBook.DTO.Orders;
using OrderBook.Services;

namespace OrderBook.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {

        private readonly IDistributedCache _distributedCache;
        private readonly OrderService _orderService;
        private readonly ILog log;

        public OrderController(
            IDistributedCache distributedCache,
            OrderService orderService,
            ILog log
        )
        {
            _distributedCache = distributedCache;
            _orderService = orderService;
            this.log = log;
        }

        [HttpGet]
        public ContentResult Information()
        {
            log.Info("Requesting for App Information");
            string cacheKey = "info";
            var information = _distributedCache.Get(cacheKey);

            if (information != null)
            {
                log.Info("Retrieving App Information from Cache");
                return Content(Encoding.UTF8.GetString(information), "text/html");
            }
            else
            {
                log.Info("Retrieving App Information from File");
                var fileString = System.IO.File
                    .ReadAllText("Resources/Instructions.html", Encoding.UTF8);
                var encodedString = Encoding.UTF8.GetBytes(fileString);
                var options = new DistributedCacheEntryOptions()
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
