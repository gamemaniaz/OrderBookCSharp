using System;

namespace OrderBook.Orders
{
    public class OrderBookCollection : IOrderBookCollection
    {
        public OrderBookCollection(params OrderBook[] orderBooks)
        {
            Books = orderBooks;
        }

        private OrderBook[] Books { get; }

        public OrderBook getInstrument(string name)
        {
            return Array.Find(Books, book => book.Name == name);
        }
    }
}
