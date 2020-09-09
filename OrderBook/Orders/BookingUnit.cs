using System;
namespace OrderBook.Orders
{
    public class BookingUnit
    {
        public BookingUnit(OrderBook orderBook)
        {
            OrderBookUnit = orderBook;
        }

        private OrderBook OrderBookUnit { get; set; }

        public String LimitOrder(int side, int size, int price, string trader)
        {
            string status = "NOT_EXECUTED";
            // BUY
            if (side == 0)
            {
                // LOOK FOR ORDERS TO FILL FOR EACH PRICE POINT
                while (price >= OrderBookUnit.AskMin)
                {
                    var entries = OrderBookUnit.Book[OrderBookUnit.AskMin];
                    while (entries.Count != 0)
                    {
                        var entry = entries.Peek();
                        if (entry.Size < size)
                        {
                            ExecuteOrder(trader, entry.Trader, price, entry.Size);
                            size -= entry.Size;
                            entries.Dequeue();
                            status = "PARTIALLY_EXECUTED";
                        }
                        else
                        {
                            ExecuteOrder(trader, entry.Trader, price, size);
                            if (entry.Size > size)
                            {
                                entry.Size -= size;
                                status = "PARTIALLY_EXECUTED";
                            }
                            else
                            {
                                entries.Dequeue();
                                status = "FULLY_EXECUTED";
                            }
                            return status;
                        }
                    }
                    OrderBookUnit.AskMin++;
                }
                // NO FILL, ENQUEUE THE ORDER
                OrderBookUnit.Book[price].Enqueue(new Order
                {
                    Price = price,
                    Side = side,
                    Size = size,
                    Trader = trader
                });
                status = "QUEUED";
                if (OrderBookUnit.BidMax < price)
                {
                    OrderBookUnit.BidMax = price;
                }
                return status;
            }
            // SELL
            else
            {
                // LOOK FOR ORDERS TO FILL FOR EACH PRICE POINT
                while (price <= OrderBookUnit.BidMax)
                {
                    var entries = OrderBookUnit.Book[OrderBookUnit.BidMax];
                    while (entries.Count != 0)
                    {
                        var entry = entries.Peek();
                        if (entry.Size < size)
                        {
                            ExecuteOrder(entry.Trader, trader, price, entry.Size);
                            size -= entry.Size;
                            entries.Dequeue();
                            status = "PARTIALLY_EXECUTED";
                        }
                        else
                        {
                            ExecuteOrder(entry.Trader, trader, price, size);
                            if (entry.Size > size)
                            {
                                entry.Size -= size;
                                status = "PARTIALLY_EXECUTED";
                            }
                            else
                            {
                                entries.Dequeue();
                                status = "FULLY_EXECUTED";
                            }
                            return status;
                        }
                    }
                    OrderBookUnit.BidMax--;
                }
                // NO FILL, ENQUEUE THE ORDER
                OrderBookUnit.Book[price].Enqueue(new Order
                {
                    Price = price,
                    Side = side,
                    Size = size,
                    Trader = trader
                });
                status = "QUEUED";
                if (OrderBookUnit.AskMin > price)
                {
                    OrderBookUnit.AskMin = price;
                }
                return status;
            }
        }

        private void ExecuteOrder(string trader, string entryTrader, int price, int size)
        {
            Console.WriteLine($"EXECUTE : {trader} BUY {entryTrader} SELL {size} {OrderBookUnit.Name} @ ${price}");
        }
    }
}
