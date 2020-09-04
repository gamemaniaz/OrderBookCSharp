using System;
namespace OrderBook.DTO.Orders
{
    public class LimitOrderRequestDto
    {
        public string Name { get; set; }
        public int Side { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public string Trader { get; set; }
    }
}
