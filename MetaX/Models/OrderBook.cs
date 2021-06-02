using System;
using System.Collections.Generic;
using System.Linq;

namespace MetaX.Models
{
    public class OrderBook
    {
        public DateTime AcqTime { get; set; }

        public List<Wrapper> Asks { get; set; }
        public List<Wrapper> Bids { get; set; }


        public List<Order> OrderAsks
        {
            get
            {
                return this.Asks.Select(i => i.Order).ToList();
            }
        }
        public List<Order> OrderBids
        {
            get
            {
                return this.Bids.Select(i => i.Order).ToList();
            }
        }
    }
}
