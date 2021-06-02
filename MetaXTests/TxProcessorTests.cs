using MetaX;
using NUnit.Framework;
using System.Linq;

namespace MetaXTests
{    
    public class TxProcessorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// more of a sanity check if we actually bought as much as we intended
        /// </summary>
        [Test]
        public void FindBestBuy_BuySumEqualsSumOfTxBoughtSum()
        {
            decimal btcToBuy = 10.5m;
            var exchangeData = Parser.ParseNExchanges(@"C:\Users\matej\Desktop\naloge\order_books_data");
            TxProcessor tp = new TxProcessor(exchangeData);

            var buyTxs = tp.FindBestBuy(btcToBuy);

            if (buyTxs.Sum(tx => tx.Amount) == btcToBuy)
                Assert.Pass();
            else 
                Assert.Fail();
        }

        /// <summary>
        /// more of a sanity check if we actually bought as much as we intended
        /// </summary>
        [Test]
        public void FindBestBuy_SellSumEqualsSumOfTxSoldSum()
        {
            decimal btcToSell = 10.5m;
            var exchangeData = Parser.ParseNExchanges(@"C:\Users\matej\Desktop\naloge\order_books_data");
            TxProcessor tp = new TxProcessor(exchangeData);

            var sellTxs = tp.FindBestSell(btcToSell);

            if (sellTxs.Sum(tx => tx.Amount) == btcToSell)
                Assert.Pass();
            else
                Assert.Fail();
        }

        /// <summary>
        /// buying negative should fail
        /// </summary>
        [Test]
        public void FindBestBuy_BuyNegativeThrowsError()
        {
            decimal btcToBuy = -10.5m;
            var exchangeData = Parser.ParseNExchanges(@"C:\Users\matej\Desktop\naloge\order_books_data");
            TxProcessor tp = new TxProcessor(exchangeData);

            try
            {
                var buyTxs = tp.FindBestBuy(btcToBuy);
                Assert.Fail(); //if it passes without error its a bug
            }
            catch
            {
                Assert.Pass();
            }            
        }

        /// <summary>
        /// buying over provided volume should fail
        /// </summary>
        [Test]
        public void FindBestBuy_BuyingOverVolumeShouldFail()
        {
            decimal btcToBuy = 100000000000m;
            var exchangeData = Parser.ParseNExchanges(@"C:\Users\matej\Desktop\naloge\order_books_data");
            TxProcessor tp = new TxProcessor(exchangeData);

            try
            {
                var buyTxs = tp.FindBestBuy(btcToBuy);
                Assert.Fail(); //if it passes without error its a bug
            }
            catch
            {
                Assert.Pass();
            }
        }

        /// <summary>
        /// buying  under 1 sat should fail
        /// </summary>
        [Test]
        public void FindBestBuy_BuyingSmallShouldFail()
        {
            decimal btcToBuy = 0.0000000001m;
            var exchangeData = Parser.ParseNExchanges(@"C:\Users\matej\Desktop\naloge\order_books_data");
            TxProcessor tp = new TxProcessor(exchangeData);

            try
            {
                var buyTxs = tp.FindBestBuy(btcToBuy);
                Assert.Fail(); //if it passes without error its a bug
            }
            catch
            {
                Assert.Pass();
            }
        }
    }
}