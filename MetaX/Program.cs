using System;

namespace MetaX
{
    class Program
    {
        /// <summary>
        /// Simple example, no DI, no logging, no unit/int tests...no nada
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Params are \r\n1.exchange data file path (orderbooks)\r\n2.amount of BTC you want to buy\r\n3.amount of BTC you want to sell\r\nAny potential buy or sell values under value of satoshi will be omitted - decimals will be cut!");

            string filename;
            decimal btcToBuy;
            decimal btcToSell;

            if (args.Length == 0)
            {
                filename = @"C:\Users\matej\Desktop\naloge\order_books_data";
                btcToBuy = 100.5m;
                btcToSell = 100.5m;
                Console.WriteLine($"Manual tests - Taking default filepath of {filename}\r\nand default buy of {btcToBuy} BTC\r\nand default sell of {btcToSell} BTC");
            }
            else
            {
                filename = args[0];
                btcToBuy = Math.Abs((decimal.Parse(args[1]).TruncateToSatoshi())); // cant have negatives or under sat
                btcToSell = Math.Abs((decimal.Parse(args[2]).TruncateToSatoshi())); // cant have negatives or under sat
            }

            try
            {
                var exchangeData = Parser.ParseNExchanges(filename);
                TxProcessor tp = new TxProcessor(exchangeData);

                var buyTxs = tp.FindBestBuy(btcToBuy);
                Console.WriteLine($"Bought {buyTxs.GetAmountSum()} of BTC for {decimal.Round(buyTxs.GetOpSum(), 2)} €");

                var txsSell = tp.FindBestSell(btcToSell);
                Console.WriteLine($"Sold {txsSell.GetAmountSum()} of BTC for {decimal.Round(txsSell.GetOpSum(), 2)} €");

                #region tx printout
                Console.WriteLine("Press b for buying details or s for selling details, or anything else to exit");

                string key = "";
                while (key != "x")
                {
                    key = Console.ReadLine();

                    if (key == "b")
                    {
                        buyTxs.PrintTxs(false);
                    }
                    else if (key == "s")
                    {
                        txsSell.PrintTxs();
                    }
                }
                
                #endregion
            }
            catch (Exception e)
            {                
                Console.WriteLine(e);
            }

        }
    }
}
