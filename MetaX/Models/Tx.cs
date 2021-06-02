using System;
using System.Collections.Generic;
using System.Text;

namespace MetaX.Models
{
    public class Tx
    {
        public decimal Price { get; set; }
        public string OrderID { get; set; }
        public int ExchangeID { get; set; }
        public decimal Amount { get; set; }
    }
}
