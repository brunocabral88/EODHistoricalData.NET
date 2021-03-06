﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EODHistoricalData.NET.Tests
{
    [TestClass]
    public class StockPriceDataTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void historical_null_symbol_throws_exception()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken);
            List<HistoricalPrice> prices = client.GetHistoricalPrices(null, null, null);
        }

        [TestMethod]
        public void historical_valid_symbols_returns_prices()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken, true);
            List<HistoricalPrice> prices = client.GetHistoricalPrices(Consts.TestSymbol, null, null);
            Assert.IsTrue(prices.Count > 0);
        }

        [TestMethod]
        public void historical_valid_symbols_with_from_date_returns_prices()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken, true);
            List<HistoricalPrice> prices = client.GetHistoricalPrices(Consts.TestSymbol, Consts.StartDate, null);
            DateTime minDate = prices.Min(x => x.Date).Date;
            Assert.IsTrue(minDate == Consts.StartDate);
        }

        [TestMethod]
        public void historical_valid_symbols_with_to_date_returns_prices()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken, true);
            List<HistoricalPrice> prices = client.GetHistoricalPrices(Consts.TestSymbol, null, Consts.EndDate);
            DateTime maxDate = prices.Max(x => x.Date).Date;
            Assert.IsTrue(maxDate == Consts.EndDate);
        }

        [TestMethod]
        public void historical_valid_symbols_with_from_and_to_date_returns_prices()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken, true);
            List<HistoricalPrice> prices = client.GetHistoricalPrices(Consts.TestSymbol, Consts.StartDate, Consts.EndDate);
            DateTime minDate = prices.Min(x => x.Date).Date;
            DateTime maxDate = prices.Max(x => x.Date).Date;
            Assert.IsTrue(minDate == Consts.StartDate);
            Assert.IsTrue(maxDate == Consts.EndDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void realtime_null_list_throws_exception()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken);
            List<RealTimePrice> prices = client.GetRealTimePrices(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void realtime_null_symbol_throws_exception()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken);
            RealTimePrice prices = client.GetRealTimePrice(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void realtime_empty_list_throws_exception()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken);
            List<RealTimePrice> prices = client.GetRealTimePrices(new string[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void realtime_list_with_null_element_throws_exception()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken);
            List<RealTimePrice> prices = client.GetRealTimePrices(new string[] { null });
        }

        [TestMethod]
        public void realtime_multiple_valid_symbols_return_result()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken, true);
            List<RealTimePrice> prices = client.GetRealTimePrices(Consts.MultipleTestSymbol);
            Assert.IsNotNull(prices);
            Assert.IsTrue(prices.Count == Consts.MultipleTestSymbol.Length);
        }

        [TestMethod]
        public void realtime_valid_symbol_return_result()
        {
            EODHistoricalDataClient client = new EODHistoricalDataClient(Consts.ApiToken, true);
            RealTimePrice price = client.GetRealTimePrice(Consts.TestSymbol);
            Assert.IsNotNull(price);
        }
    }
}
