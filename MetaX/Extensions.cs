
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

        /// <summary>
        /// truncate decimals - interwebs solution:)
        /// </summary>
        /// <param name="btc"></param>
        /// <returns></returns>
        public static decimal TruncateToSatoshi(this decimal btc)
        {
            decimal r = Math.Round(btc, 8);

            if (btc > 0 && r > btc)
            {
                return r - new decimal(1, 0, 0, false, 8);
            }
            else if (btc < 0 && r < btc)
            {
                return r + new decimal(1, 0, 0, false, 8);
            }

            return r;
        }
    }
}
