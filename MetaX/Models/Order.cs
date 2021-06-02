using System;
using System.Collections.Generic;
using System.Text;

namespace MetaX.Models
{
    public class Order
    {
        public string Id { get; set; }

        public DateTime Time { get; set; }

        public string Kind { get; set; }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }
    }
}
