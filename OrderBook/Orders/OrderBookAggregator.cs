using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderBook.Orders
{
    public class OrderBookAggregator
    {
        private OrderBook _orderBook;
        private int _lowerBound;
        private int _upperBound;

        public OrderBookAggregator(OrderBook orderBook)
        {
            _orderBook = orderBook;
            _lowerBound = ScanForLowerBound(orderBook);
            _upperBound = ScanForUpperBound(orderBook);
        }

        public string AggregateAndDisplay()
        {
            if (_lowerBound == -1 || _upperBound == -1)
                return "BOOK_IS_EMPTY";

            if (_lowerBound == _upperBound)
                return DisplaySingleOrder(_orderBook.Book[_lowerBound].Peek());

            StringBuilder sb = new StringBuilder();

            // do aggregation
            foreach (int index in Enumerable.Range(_lowerBound, _upperBound).Reverse())
            {
                Queue<Order> pricePointContainer = _orderBook.Book[index];
                int totalVolume = AggregateVolume(pricePointContainer);
                if (index == _orderBook.BidMax)
                {
                    if (_orderBook.AskMin <= _orderBook.MaxPrice)
                    {
                        sb.AppendLine($"Spread : {_orderBook.AskMin - _orderBook.BidMax}");
                    }
                    sb.AppendLine("----------------------");
                }
                if (totalVolume > 0)
                    sb.AppendLine($"Price: {index} | Volume : {totalVolume}");
                if (index == _orderBook.AskMin)
                    sb.AppendLine("----------------------");
                    
            }
            
            return sb.ToString();
        }

        private int ScanForLowerBound(OrderBook orderBook)
        {
            foreach (int index in Enumerable.Range(0, orderBook.MaxPrice))
            {
                Queue<Order> pricePointContainer = orderBook.Book[index];
                if (pricePointContainer.Count > 0)
                    return index;
            }
            return -1;
        }

        private int ScanForUpperBound(OrderBook orderBook)
        {
            foreach (int index in Enumerable.Range(0, orderBook.MaxPrice).Reverse())
            {
                Queue<Order> pricePointContainer = orderBook.Book[index];
                if (pricePointContainer.Count > 0)
                    return index;
            }
            return -1;
        }

        private string DisplaySingleOrder(Order order)
        {
            string buyOrSell = order.Side == 0 ? "BUY" : "SELL";
            return $"{buyOrSell} {order.Size} @ {order.Price}";
        }

        private int AggregateVolume(Queue<Order> orders)
        {
            return orders.Sum(order => order.Size);
        }
    }
}
