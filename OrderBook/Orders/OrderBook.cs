using System.Collections.Generic;
using System.Linq;

namespace OrderBook.Orders
{
    public class OrderBook
    {

        public OrderBook(string name, int maxPrice)
        {
            Name = name;
            MaxPrice = maxPrice;
            BidMax = 0;
            AskMin = maxPrice + 1;
            Book = Enumerable
                .Range(0, maxPrice)
                .Select(price => new Queue<Order>())
                .ToArray<Queue<Order>>();
        }

        public string Name { get; set; }
        public int MaxPrice { get; set; }
        public int BidMax { get; set; }
        public int AskMin { get; set; }
        public Queue<Order>[] Book { get; set; }
    }
}
