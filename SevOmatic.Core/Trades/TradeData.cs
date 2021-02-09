using System;
using System.Collections.Generic;
using System.Text;

namespace SevOmatic.Core.Trades
{
    public class TradeData
    {
        public List<TradeDataItem> Trades { get; set; }
    }

    public class TradeDataItem
    {
        public string Symbol { get; set; }
        public decimal BuyPrice { get; set; }
    }
}
