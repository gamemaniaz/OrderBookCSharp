namespace OrderBook.Orders
{
    public class Order
    {
        public int Side { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public string Trader { get; set; }
    }
}
