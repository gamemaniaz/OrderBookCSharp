namespace OrderBook.DTO.Orders
{
    public class LimitOrderResponseDto
    {
        public LimitOrderResponseDto(string status)
        {
            Status = status;
        }

        public string Status { get; set; }
    }
}
