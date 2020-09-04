using System;
namespace OrderBook.Orders
{
    public interface IOrderBookCollection
    {
        OrderBook getInstrument(string name);
    }
}
