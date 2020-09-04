namespace OrderBook.Orders
{
    public class Order
    {
        public Order(int side, int size, int price, string trader)
        {
            Side = side;
            Size = size;
            Price = price;
            Trader = trader;
        }

        public int Side { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public string Trader { get; set; }
    }
}
