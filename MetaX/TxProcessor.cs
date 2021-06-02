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

        public List<object> FindBestBuy(decimal amountBTC)
        {
            var ordersFromAllExchanges = GetOrdersFromAllExchangesNormalized(false);

            #region orders sorted by price desc
            Func<Order, decimal> sortByPrice = o => o.Price;
            var ordersSortedByPriceDesc = GetSortedOrders(ordersFromAllExchanges, sortByPrice, false);
            #endregion

            return null;
        }

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
        #endregion
    }
}
