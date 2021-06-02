
using MetaX.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetaX
{
    public static class Extensions
    {
        public static decimal GetOpSum(this List<Tx> txs)
        {
            return txs.Select(i => i.Amount * i.Price).Sum();
        }

        public static decimal GetAmountSum(this List<Tx> txs)
        {
            return txs.Select(i => i.Amount).Sum();
        }
    }
}
