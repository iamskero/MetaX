namespace MetaX.Models
{
    /// <summary>
    /// Potential TODO: fix deserialization so no wrapper is needed
    /// </summary>
    public class Wrapper
    {
        public Order Order { get; set; }
        
        public Exchange ownerExchange
        {
            set
            {
                Order.ownerExchange = value;
            }
        }
    }
}
