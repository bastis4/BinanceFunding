using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funding4Dimochka
{
    public class FuturesUsdtFundingRate
    {
        public string FutureUsdtSymbol { get; set; }
        public decimal FundingRate { get; set; }
        public int HistoryPeriod { get; set; } 

        public FuturesUsdtFundingRate(string symbol, decimal funding, int period)
        {
            FutureUsdtSymbol = symbol;
            FundingRate = funding;
            HistoryPeriod = period;
        }

        public FuturesUsdtFundingRate(string symbol, int period)
        {
            FutureUsdtSymbol = symbol;
            HistoryPeriod = period;
        }

    }
}
