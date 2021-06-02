using System;
using System.Collections.Generic;
using System.Text;

namespace MetaX.Models
{
    public class OrderBook
    {
        public DateTime AcqTime { get; set; }

        public List<Order> Asks { get; set; }
        public List<Order> Bids { get; set; }
    }
}
