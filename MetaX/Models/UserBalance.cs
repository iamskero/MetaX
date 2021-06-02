using System;
using System.Collections.Generic;
using System.Text;

namespace MetaX.Models
{
    public class UserBalance
    {
        public decimal BTC { get; set; }
        public decimal EUR { get; set; }

        /// <summary>
        /// Data isn't in the file so we're making it up
        /// </summary>
        public UserBalance()
        {
            Random rnd = new Random();

            BTC = (decimal)rnd.NextDouble();
            EUR = (decimal)rnd.NextDouble() * 100000;
        }
    }
}
