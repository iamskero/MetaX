
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

        public static void PrintTxs(this List<Tx> txs, bool sell = true)
        {
            string verb = sell ? "sold" : "bought";

            txs.ForEach(tx => {
                Console.WriteLine($"Amount of {tx.Amount} {verb} from exchange {tx.ExchangeID} with order {tx.OrderID} at {tx.Price} EUR/BTC");
            });
        }
    }
}
