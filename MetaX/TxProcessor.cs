using MetaX.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetaX
{
    class TxProcessor
    {
        private List<Exchange> exchangesData;
        public TxProcessor(List<Exchange> ed)
        {
            exchangesData = ed;
        }

        /// <summary>
        /// Finding best buy with for the existing balance of EUR
        /// </summary>
        /// <param name="amountBTC"></param>
        /// <returns></returns>
        public List<Tx> FindBestBuy(decimal amountBTC)
        {
            // asumption is that ordering by price then by our exchange position is the best way to go around this to get most optimal buys

            #region all orders from all exchanges
            var ordersFromAllExchanges = GetOrdersFromAllExchangesNormalized(true);
            #endregion

            #region orders sorted by price asc
            Func<Order, decimal> sortByPrice = o => o.Price;
            var ordersSortedByPriceAsc = GetSortedOrders(ordersFromAllExchanges, sortByPrice, true);
            #endregion

            #region orders sorted then by balance on the exchanges (exchanges of given orders)
            Func<Order, decimal> sortByBalance = o => o.ownerExchange.UserBalance.EUR;
            var ordersSortedByPriceAscAndBalanceDesc = GetSortedOrdersBySecond(ordersSortedByPriceAsc, sortByBalance, false);
            #endregion

            #region actual buying order
            var rez = ordersSortedByPriceAscAndBalanceDesc.ToList();

            // rez.Select(i => new { price = i.Price, exchangeId = i.ownerExchange.ID, balanceEUR = i.ownerExchange.UserBalance.EUR} )
            return TxOperation(rez, amountBTC);
            #endregion            
        }

        /// <summary>
        /// Selling amount of BTC for the highest prices for the existing balance of BTC across exchanges
        /// </summary>
        /// <param name="BTC"></param>
        public List<Tx> FindBestSell(decimal amountBTC)
        {
            #region all orders from all exchanges
            var ordersFromAllExchanges = GetOrdersFromAllExchangesNormalized(false);
            #endregion

            #region orders sorted by price desc
            Func<Order, decimal> sortByPrice = o => o.Price;
            var ordersSortedByPriceDesc = GetSortedOrders(ordersFromAllExchanges, sortByPrice, false);
            #endregion

            #region orders sorted then by balance on the exchanges (exchanges of given orders)
            Func<Order, decimal> sortByBalance = o => o.ownerExchange.UserBalance.BTC;
            var ordersSortedByPriceDescAndBalanceDesc = GetSortedOrdersBySecond(ordersSortedByPriceDesc, sortByBalance, false);
            #endregion

            #region actual buying order
            var rez = ordersSortedByPriceDescAndBalanceDesc.ToList();
            // rez.Select(i => new { price = i.Price, exchangeId = i.ownerExchange.ID, balanceEUR = i.ownerExchange.UserBalance.BTC} )
            return TxOperation(rez, amountBTC);
            #endregion 
        }

        #region buying/spending spree
        /// <summary>
        /// buy up the orders up to the amount
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="amountBTC"></param>
        /// <returns></returns>
        //private List<Tx> Buy(List<Order> orders, decimal amountBTC)
        //{
        //    var toBuy = amountBTC;
        //    var counter = 0;
        //    List<Tx> txs = new List<Tx>();

        //    while (toBuy != 0)
        //    {
        //        var order = orders[counter++];
        //        var availableAmount = order.Amount; //could use stack for poppin this exchange ..as done, but assuming we dont need to track exchange status, no persistence etc

        //        if (toBuy - availableAmount >= 0)
        //            toBuy -= availableAmount;
        //        else toBuy = 0;

        //        var tx = new Tx() { Amount = availableAmount, ExchangeID =  order.ownerExchange.ID, OrderID = order.Id, Price = order.Price };
        //        txs.Add(tx);

        //        if (counter > orders.Count - 1)
        //            throw new Exception("All bought up yo! Bill Gates' spending.");
        //    }

        //    return txs;
        //}

        private List<Tx> TxOperation(List<Order> orders, decimal amountBTC)
        {
            var counter = 0;
            List<Tx> txs = new List<Tx>();

            while (amountBTC != 0)
            {
                var order = orders[counter++];
                var availableAmount = order.Amount; //could use stack for poppin this exchange ..as done, but assuming we dont need to track exchange status, no persistence etc

                decimal amountUsed;
                if (amountBTC - availableAmount >= 0)
                {
                    amountBTC -= availableAmount;
                    amountUsed = availableAmount;
                }
                else
                {
                    amountUsed = amountBTC;
                    amountBTC = 0;

                }

                var tx = new Tx() { Amount = amountUsed, ExchangeID = order.ownerExchange.ID, OrderID = order.Id, Price = order.Price };
                txs.Add(tx);

                if (counter > orders.Count - 1)
                    throw new Exception("Ran out of volume <sad face />");
            }

            return txs;
        }
        #endregion

        /// <summary>
        /// Get the orders from all the exchanges in a single list
        /// Param: asks bool
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Order> GetOrdersFromAllExchangesNormalized(bool asks = true)
        {
            if (asks)
                return exchangesData.Select(i => i.OrderBook.OrderAsks)
                .SelectMany(i => i);
            else
                return exchangesData.Select(i => i.OrderBook.OrderBids)
                .SelectMany(i => i);
        }


        #region private sorting methods
        /// <summary>
        /// Sort orders by ...
        /// TODO?: Could make a class with sorters, or a function that accepts list of sorting delegates or...or...or...
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="sortingFunc"></param>
        /// <param name="sortAsc"></param>
        /// <returns></returns>
        private IOrderedEnumerable<Order> GetSortedOrders(IEnumerable<Order> orders, Func<Order, decimal> sortingFunc, bool sortAsc)
        {
            if (sortAsc)
            {
                return orders.OrderBy(sortingFunc);
            }
            else return orders.OrderByDescending(sortingFunc);
        }

        /// <summary>
        /// Sort ordered enum
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="thenBySortingFunc"></param>
        /// <param name="sortAsc"></param>
        /// <returns></returns>
        private IOrderedEnumerable<Order> GetSortedOrdersBySecond(IOrderedEnumerable<Order> orders, Func<Order, decimal> thenBySortingFunc, bool sortAsc)
        {
            if (sortAsc)
            {
                return orders.ThenBy(thenBySortingFunc);
            }
            else return orders.ThenByDescending(thenBySortingFunc);
        }
        #endregion
    }
}
