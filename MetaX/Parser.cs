using System.Collections.Generic;
using System.IO;
using System.Linq;
using MetaX.Models;
using System.Text.Json;

namespace MetaX
{
    public static class Parser
    {
        /// <summary>
        /// Parse n exchanges
        /// </summary>
        /// <param name="fileLoc"></param>
        /// <param name="count"></param>
        public static List<Exchange> ParseNExchanges(string fileLoc, int count = 10)
        {
            if (File.Exists(fileLoc))
            {
                List<Exchange> exchangesData = new List<Exchange>();

                File.ReadLines(fileLoc)
                    .Take(count)
                    .ToList()
                    .ForEach(l =>
                    {
                        var stringWithOrderBookInfo = l.Substring(l.IndexOf("{"));
                        var orderBook = JsonSerializer.Deserialize<OrderBook>(stringWithOrderBookInfo);
                        var exchange = new Exchange { UserBalance = new UserBalance(), OrderBook = orderBook };

                        #region CRD, possible TODO: find more optimal way, like separate list with ids or smth
                        orderBook.Asks.ForEach(w => w.ownerExchange = exchange);
                        orderBook.Bids.ForEach(w => w.ownerExchange = exchange);
                        #endregion

                        exchangesData.Add(exchange);
                    });

                return exchangesData;
            }
            else throw new FileNotFoundException("Wrong file path");

        }
    }
}
