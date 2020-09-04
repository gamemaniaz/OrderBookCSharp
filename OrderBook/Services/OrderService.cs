using Microsoft.AspNetCore.Hosting;
using OrderBook.Orders;

namespace OrderBook.Services
{
    public class OrderService
    {
        public OrderService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
            OrderBookCollection = new OrderBookCollection(
                new Orders.OrderBook("AAPL", 1200),
                new Orders.OrderBook("MSFT", 1000)
            );
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        public IOrderBookCollection OrderBookCollection { get; }

        public string BookLimitOrder(string name, int side, int size, int price, string trader)
        {
            Orders.OrderBook orderBook = OrderBookCollection.getInstrument(name);
            BookingUnit bookingUnit = new BookingUnit(orderBook);
            return bookingUnit.LimitOrder(side, size, price, trader);
        }

        public string DisplayOrderBook(string name)
        {
            Orders.OrderBook orderBook = OrderBookCollection.getInstrument(name);
            OrderBookAggregator aggregator = new OrderBookAggregator(orderBook);
            return aggregator.AggregateAndDisplay();
        }
    }
}
