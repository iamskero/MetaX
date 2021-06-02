using System;

namespace MetaX
{
    class Program
    {
        /// <summary>
        /// Simple example, no DI, no logging...no nada
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var exchangeData = Parser.ParseNExchanges(@"C:\Users\matej\Desktop\naloge\order_books_data");
            TxProcessor tp = new TxProcessor(exchangeData);

            var buyTxs = tp.FindBestBuy(10.5m);
            Console.WriteLine($"Bought {buyTxs.GetAmountSum()} of BTC for {buyTxs.GetOpSum()}");


            Console.WriteLine("Hello World!");
        }
    }
}
