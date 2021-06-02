namespace MetaX.Models
{
    public class Exchange
    {
        public int ID { get; set; }
        public UserBalance UserBalance { get; set; }
        public OrderBook OrderBook { get; set; }
    }
}
